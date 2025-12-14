using System.Collections.Generic;
using System.ServiceModel;
using SmartFactory.Models;

namespace RobotService
{
    [ServiceContract]
    public interface IMachineService
    {
        // Note que o retorno é SensorData[] (Array) e NÃO Task<...>
        [OperationContract]
        SensorData[] GetCurrentSensors();

        [OperationContract]
        MachineRule[] GetAllRules();

        // O nome é apenas CreateNewRule. O retorno é string. NADA de Task.
        [OperationContract]
        string CreateNewRule(MachineRule newRule);
    }
}