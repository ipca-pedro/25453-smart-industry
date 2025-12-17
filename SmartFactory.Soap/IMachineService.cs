using System.Collections.Generic;
using System.ServiceModel;
using SmartFactory.Models; // <--- OBRIGATÓRIO: Usa os teus modelos oficiais

namespace RobotService
{
    [ServiceContract]
    public interface IMachineService
    {
        // Mudamos de List<> para Array [] para o WCF Test Client não dar erro
        [OperationContract]
        SensorData[] GetCurrentSensors();

        [OperationContract]
        MachineRule[] GetAllRules();

        [OperationContract]
        string CreateNewRule(MachineRule newRule);
    }

    // NÃO definas classes aqui em baixo!
}