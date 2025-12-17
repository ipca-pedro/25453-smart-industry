using System;
using System.Collections.Generic;
using System.Web.Http;
using SmartFactory.Data;
using SmartFactory.Models;

namespace SmartFactory.API.Controllers
{
    public class RulesController : ApiController
    {
        private DbManager db = new DbManager();

        // GET: api/rules
        // Retorna a lista de todas as regras da tabela public.machine_rules
        public IEnumerable<MachineRule> Get()
        {
            return db.GetRules();
        }

        // POST: api/rules
        // Recebe um objeto MachineRule no corpo da requisição (JSON)
        public string Post([FromBody] MachineRule regra)
        {
            if (regra == null) return "Dados inválidos.";

            return db.CreateRule(regra);
        }

        // PUT: api/rules/5
        // Atualiza uma regra específica baseada no ID da URL
        public string Put(int id, [FromBody] MachineRule regra)
        {
            if (regra == null) return "Dados inválidos.";

            // Corrigido: Usando os nomes reais das colunas da tabela machine_rules
            // ThresholdValue corresponde a threshold_value no SQL
            // RuleName corresponde a rule_name no SQL
            return db.UpdateRule(id, regra.ThresholdValue, regra.RuleName);
        }

        // DELETE: api/rules/5
        public string Delete(int id)
        {
            return db.DeleteRule(id);
        }
    }
}