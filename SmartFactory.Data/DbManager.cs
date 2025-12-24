using System;
using System.Collections.Generic;
using Npgsql;
using SmartFactory.Models;

namespace SmartFactory.Data
{
    public class DbManager
    {
        private static readonly string _connString = "Host=localhost;Port=5432;Database=iotdb;Username=admin;Password=changeme";

        // 1. SELECT SENSORES (REST)
        public List<SensorData> GetLatestReadings()
        {
            var list = new List<SensorData>();
            using (var conn = new NpgsqlConnection(_connString))
            {
                conn.Open();
                // Query para ir buscar a última leitura de cada sensor
                string sql = "SELECT DISTINCT ON (sensor) sensor, \"Polo\", \"Valor\", \"Unidade\", \"DataHora\" " +
                             "FROM public.dados_sensores_limpos ORDER BY sensor, \"DataHora\" DESC";

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
            return list;
        }

        // 2. INTERVENÇÃO MANUAL TRANSACIONAL (SOAP) - O "Coração" do projeto
        public bool ExecuteManualIntervention(int ruleId, double newThreshold, string machineName)
        {
            using (var conn = new NpgsqlConnection(_connString))
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        // Update na regra (Hardcoded)
                        string sqlUpdate = "UPDATE public.machine_rules SET threshold_value = @val WHERE id = @id";
                        using (var cmd = new NpgsqlCommand(sqlUpdate, conn))
                        {
                            cmd.Parameters.AddWithValue("val", newThreshold);
                            cmd.Parameters.AddWithValue("id", ruleId);
                            cmd.ExecuteNonQuery();
                        }

                        // Inserir Log de Auditoria (Hardcoded)
                        // Repara: no teu SQL a tabela é 'machine_logs'
                        string sqlLog = "INSERT INTO public.machine_logs (machine_name, event_description) " +
                                        "VALUES (@name, @desc)";
                        using (var cmd = new NpgsqlCommand(sqlLog, conn))
                        {
                            cmd.Parameters.AddWithValue("name", machineName);
                            cmd.Parameters.AddWithValue("desc", $"Ajuste Manual via WinApp: Threshold mudado para {newThreshold}");
                            cmd.ExecuteNonQuery();
                        }

                        trans.Commit();
                        return true;
                    }
                    catch { trans.Rollback(); return false; }
                }
            }
        }

        // 3. CRUD COMPLETO: RESTO DAS REGRAS
        public List<MachineRule> GetRules()
        {
            var list = new List<MachineRule>();
            using (var conn = new NpgsqlConnection(_connString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("SELECT * FROM public.machine_rules", conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new MachineRule
                        {
                            Id = (int)reader["id"],
                            TargetSensorId = reader["target_sensor_id"].ToString(),
                            RuleName = reader["rule_name"].ToString(),
                            ThresholdValue = Convert.ToDouble(reader["threshold_value"]),
                            ConditionType = reader["condition_type"].ToString(),
                            ActionCommand = reader["action_command"].ToString(),
                            IsActive = (bool)reader["is_active"]
                        });
                    }
                }
            }
            return list;
        }

       
    }
}