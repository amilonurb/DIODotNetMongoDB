using DIODotNetMongo.Data;
using DIODotNetMongo.Data.Collections;
using DIODotNetMongo.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Linq;

namespace DIODotNetMongo.Controllers
{
    [Route("{controller}")]
    [ApiController]
    public class InfectadoController : ControllerBase
    {
        private readonly MongoDBContext _mongo;
        private readonly IMongoCollection<Infectado> _collection;

        public InfectadoController(MongoDBContext mongo)
        {
            _mongo = mongo;
            _collection = _mongo.DB.GetCollection<Infectado>(typeof(Infectado).Name.ToLower());
        }

        [HttpPost]
        public ActionResult SalvarInfectado([FromBody] InfectadoDto dto)
        {
            var infectado = new Infectado(dto.DataNascimento, dto.Sexo, dto.Latitude, dto.Longitude);
            _collection.InsertOne(infectado);
            return Created("", "Infectado adicionado com sucesso");
        }

        [HttpGet]
        public ActionResult ObterInfectados()
        {
            var infectados = _collection.Find(Builders<Infectado>.Filter.Empty).ToList();
            return Ok(infectados);
        }
    }
}