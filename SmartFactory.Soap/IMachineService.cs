using System.Collections.Generic;
using System.ServiceModel;
using SmartFactory.Models; 

namespace RobotService
{
    [ServiceContract]
    public interface IMachineService
    {
        // Usamos Array [] porque o WCF Test Client gosta mais do que List<>
        [OperationContract]
        SensorData[] GetCurrentSensors();

        [OperationContract]
        MachineRule[] GetAllRules();

        [OperationContract]
        string CreateNewRule(MachineRule newRule);
    }

    // NÃO definas classes SensorData aqui em baixo! Elas já vêm do SmartFactory.Models
}