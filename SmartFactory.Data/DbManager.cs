using System;
using System.Collections.Generic;
using Npgsql;
using SmartFactory.Models;

namespace SmartFactory.Data
{
    public class DbManager
    {
        private static readonly string _connString = "Host=localhost;Port=5432;Database=iotdb;Username=admin;Password=changeme";

        // --- 1. LEITURA DE SENSORES (Dashboard REST) ---
        public List<SensorData> GetLatestReadings()
        {
            var list = new List<SensorData>();
            try
            {
                using (var conn = new NpgsqlConnection(_connString))
                {
                    conn.Open();
                    // Query Hardcoded para os dados do Projeto 1
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
            }
            catch (Exception ex) { throw new Exception("Erro ao ler sensores: " + ex.Message); }
            return list;
        }

        // --- 2. CRUD: LER REGRAS (SELECT) ---
        public List<MachineRule> GetRules()
        {
            var list = new List<MachineRule>();
            using (var conn = new NpgsqlConnection(_connString))
            {
                conn.Open();
                string sql = "SELECT * FROM public.machine_rules ORDER BY id ASC";
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

        // --- 3. CRUD: CRIAR REGRA (INSERT) ---
        public bool CreateRule(MachineRule rule)
        {
            using (var conn = new NpgsqlConnection(_connString))
            {
                conn.Open();
                string sql = "INSERT INTO public.machine_rules (target_sensor_id, rule_name, threshold_value, condition_type, action_command, is_active) " +
                             "VALUES (@sensor, @name, @val, @cond, @act, @active)";
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("sensor", rule.TargetSensorId);
                    cmd.Parameters.AddWithValue("name", rule.RuleName);
                    cmd.Parameters.AddWithValue("val", rule.ThresholdValue);
                    cmd.Parameters.AddWithValue("cond", rule.ConditionType);
                    cmd.Parameters.AddWithValue("act", rule.ActionCommand);
                    cmd.Parameters.AddWithValue("active", rule.IsActive);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        // --- 4. CRUD: ATUALIZAÇÃO E INTERVENÇÃO (UPDATE + LOG) ---
        // Este método é o coração da tua WinApp (Inputs Manuais)
        public bool ExecuteManualIntervention(int ruleId, double newThreshold, string machineName)
        {
            using (var conn = new NpgsqlConnection(_connString))
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        // 4a. Update Hardcoded
                        string sqlUpdate = "UPDATE public.machine_rules SET threshold_value = @val WHERE id = @id";
                        using (var cmd = new NpgsqlCommand(sqlUpdate, conn))
                        {
                            cmd.Parameters.AddWithValue("val", newThreshold);
                            cmd.Parameters.AddWithValue("id", ruleId);
                            cmd.ExecuteNonQuery();
                        }

                        // 4b. Insert Log Hardcoded (Audit Trail)
                        string sqlLog = "INSERT INTO public.machine_logs (machine_name, event_description, event_timestamp) " +
                                        "VALUES (@name, @desc, CURRENT_TIMESTAMP)";
                        using (var cmd = new NpgsqlCommand(sqlLog, conn))
                        {
                            cmd.Parameters.AddWithValue("name", machineName);
                            cmd.Parameters.AddWithValue("desc", $"Ajuste Manual: Performance/Threshold alterado para {newThreshold}");
                            cmd.ExecuteNonQuery();
                        }

                        trans.Commit();
                        return true;
                    }
                    catch
                    {
                        trans.Rollback();
                        return false;
                    }
                }
            }
        }

        // --- 5. CRUD: APAGAR REGRA (DELETE) ---
        public bool DeleteRule(int id)
        {
            using (var conn = new NpgsqlConnection(_connString))
            {
                conn.Open();
                string sql = "DELETE FROM public.machine_rules WHERE id = @id";
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("id", id);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
    }
}