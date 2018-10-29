using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;




namespace Mongo3.Models
{
    public class CitasModel
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement("Cedula")]
        public string cedula { get; set; }
        [BsonElement("Especialidad")]
        public string Especialidad { get; set; }
        [BsonElement("Fecha")]
        public DateTime Fecha { get; set; }
        [BsonElement("Hora")]
        public string Hora { get; set; }
        [BsonElement("Observacion")]
        public string Observacion { get; set; }
        [BsonElement("Estado")]
        public string Estado { get; set; }
    }
}