using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace RobotService
{
    // --- COLE A INTERFACE AQUI ---
    [ServiceContract]
    public interface IMachineService
    {
        [OperationContract]
        List<SensorData> GetCurrentSensors();

        [OperationContract]
        List<MachineRule> GetAllRules();

        [OperationContract]
        string CreateNewRule(MachineRule newRule);
    }

    // --- COLE OS DTOs (SensorData e MachineRule) AQUI ---
    [DataContract]
    public class SensorData
    {
        [DataMember] public int Id { get; set; }
        [DataMember] public string Tipo { get; set; }
        [DataMember] public double Valor { get; set; }
    }

    [DataContract]
    public class MachineRule
    {
        [DataMember] public int RuleId { get; set; }
        [DataMember] public string Descricao { get; set; }
        [DataMember] public double LimiteAtivacao { get; set; }
    }
}