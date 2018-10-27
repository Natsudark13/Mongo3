using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;




namespace Mongo3.Models
{
    public class CentroAtencionModel
    {
        [BsonId]
        public Double Id { get; set; }
        [BsonElement("Nombre")]
        public string Nombre { get; set; }
        [BsonElement("Ubicacion")]
        public string Ubicacion { get; set; }
        [BsonElement("CapacidadMaxima")]
        public Double CapacidadMaxima { get; set; }
        [BsonElement("Tipo")]
        public string Tipo { get; set; }

    }
}