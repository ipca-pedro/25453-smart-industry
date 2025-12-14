using System;
using System.Collections.Generic;
using Npgsql; // O Driver do Postgres
using SmartFactory.Models;

namespace SmartFactory.Data
{
    public class DbManager
    {
        // ATENÇÃO: Confirma a password/user do teu Docker
        private readonly string _connString = "Host=localhost;Port=5432;Database=iotdb;Username=admin;Password=changeme";

        // --- 1. LER DADOS DOS SENSORES (Do TP1) ---
        public List<SensorData> GetLatestReadings()
        {
            var list = new List<SensorData>();

            try
            {
                using (var conn = new NpgsqlConnection(_connString))
                {
                    conn.Open();
                    // Vai buscar a leitura mais recente de cada sensor distinto
                    string sql = "SELECT DISTINCT ON (sensor) sensor, \"Polo\", \"Valor\", \"Unidade\", \"DataHora\" " +
                                 "FROM public.dados_sensores_limpos " +
                                 "ORDER BY sensor, \"DataHora\" DESC";

                    using (var cmd = new NpgsqlCommand(sql, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new SensorData
                            {
                                SensorId = reader["sensor"].ToString(),
                                Polo = reader["Polo"].ToString(),
                                Valor = Convert.ToDouble(reader["Valor"]),
                                Unidade = reader["Unidade"].ToString(),
                                DataHora = Convert.ToDateTime(reader["DataHora"])
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Em produção deves usar Logs reais. Aqui atiramos o erro para ver no WCF Test Client.
                throw new Exception("Erro ao ler BD: " + ex.Message);
            }
            return list;
        }

        // --- 2. LER REGRAS (SELECT) ---
        public List<MachineRule> GetRules()
        {
            var list = new List<MachineRule>();
            using (var conn = new NpgsqlConnection(_connString))
            {
                conn.Open();
                string sql = "SELECT * FROM public.machine_rules";

                using (var cmd = new NpgsqlCommand(sql, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new MachineRule
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            TargetSensorId = reader["target_sensor_id"].ToString(),
                            RuleName = reader["rule_name"].ToString(),
                            ThresholdValue = Convert.ToDouble(reader["threshold_value"]),
                            ConditionType = reader["condition_type"].ToString(),
                            ActionCommand = reader["action_command"].ToString(),
                            IsActive = Convert.ToBoolean(reader["is_active"])
                        });
                    }
                }
            }
            return list;
        }

        // --- 3. CRIAR REGRA (INSERT) ---
        public string CreateRule(MachineRule rule)
        {
            using (var conn = new NpgsqlConnection(_connString))
            {
                conn.Open();
                string sql = "INSERT INTO public.machine_rules (target_sensor_id, rule_name, threshold_value, condition_type, action_command) " +
                             "VALUES (@sensor, @name, @val, @cond, @act)";

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("sensor", rule.TargetSensorId);
                    cmd.Parameters.AddWithValue("name", rule.RuleName);
                    cmd.Parameters.AddWithValue("val", rule.ThresholdValue);
                    cmd.Parameters.AddWithValue("cond", rule.ConditionType);
                    cmd.Parameters.AddWithValue("act", rule.ActionCommand);

                    cmd.ExecuteNonQuery();
                }
            }
            return "Regra criada com sucesso!";
        }
    }
}