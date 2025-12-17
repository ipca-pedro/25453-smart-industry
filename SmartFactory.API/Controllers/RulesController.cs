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

        // GET: api/rules (Lista todas)
        public IEnumerable<MachineRule> Get()
        {
            return db.GetRules();
        }

        // POST: api/rules (Cria nova)
        // O parametro [FromBody] obriga a ler o JSON enviado
        public string Post([FromBody] MachineRule regra)
        {
            return db.CreateRule(regra);
        }

        // PUT: api/rules/5 (Atualiza)
        public string Put(int id, [FromBody] MachineRule regra)
        {
            // Nota: O objeto 'regra' vem do JSON, o 'id' vem do URL
            return db.UpdateRule(id, regra.LimiteAtivacao, regra.Descricao);
        }

        // DELETE: api/rules/5 (Apaga)
        public string Delete(int id)
        {
            return db.DeleteRule(id);
        }
    }
}