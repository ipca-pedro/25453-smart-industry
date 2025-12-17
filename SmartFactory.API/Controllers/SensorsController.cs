using System;
using System.Collections.Generic;
using System.Web.Http;
using SmartFactory.Data;
using SmartFactory.Models;

namespace SmartFactory.API.Controllers
{
    public class SensorsController : ApiController
    {
        // Declaração clássica da variável local da classe
        private DbManager db = new DbManager();

        // GET: api/sensors
        public IEnumerable<SensorData> Get()
        {
            try
            {
                // Chama o método usando 'db' diretamente
                return db.GetLatestReadings();
            }
            catch (Exception ex)
            {
                // Em caso de erro, devolve uma lista vazia
                return new List<SensorData>();
            }
        }
    }
}