using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using WebAPIDB.Models;
using System.Web.Http;

namespace WebAPIDB.Controllers
{
    public class ClientesController : ApiController
    {
        private ClienteEntities db = new ClienteEntities();

        [HttpGet]
        // GET: Clientes
        public IEnumerable<Cliente> Get()
        {
            using (ClienteEntities Cliente_Entities = new ClienteEntities())
            {
                return Cliente_Entities.Cliente.ToList();

            }

        }

        [HttpGet]
        // GET: Clientes/Details/5
        public Cliente Get(int? id)
        {
            using (ClienteEntities Cliente_Entities = new ClienteEntities())
            {
                return Cliente_Entities.Cliente.FirstOrDefault(e => e.IDCliente == id);

            }
        }

        // Graba nuevos registros en la base de datos
        [HttpPost]
        public IHttpActionResult AgregaCliente([FromBody] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.Cliente.Add(cliente);
                db.SaveChanges();
                return Ok(cliente);
            }
            else {
                return BadRequest();
            }


        }

        [HttpPut]
        public IHttpActionResult ActualizarCliente(int id, [FromBody] Cliente cliente) {
            if (ModelState.IsValid)
            {
                var ClienteExiste = db.Cliente.Count(c => c.IDCliente == id) > 0;

                if (ClienteExiste)
                {
                    db.Entry(cliente).State = EntityState.Modified;
                    db.SaveChanges();

                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            else {
                return BadRequest();
            }
        }

        //Borra un registro (api/cliente/1)
        [HttpDelete]
        public IHttpActionResult EliminarCliente(int id)
        {
            var cliente = db.Cliente.Find(id);

            if (cliente != null)
            {
                db.Cliente.Remove(cliente);
                db.SaveChanges();

                return Ok(cliente);
            }
            else {
                return NotFound();
            }
        }





    }
}
