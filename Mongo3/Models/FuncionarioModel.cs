using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;




namespace Mongo3.Models
{
    public class FuncionarioModel
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement("Nombre")]
        public string Nombre { get; set; }
        [BsonElement("Apellido1")]
        public string Apellido1 { get; set; }
        [BsonElement("Apellido2")]
        public string Apellido2 { get; set; }
        [BsonElement("TipoFuncionario")]
        public string TipoFuncionario { get; set; }
        [BsonElement("FechaIngreso")]
        public string FechaIngreso { get; set; }
        [BsonElement("Area")]
        public string Area { get; set; }
        [BsonElement("Contraseña")]
        public string Contraseña { get; set; }

    }
}