using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;

namespace Mongo3.Models
{
    //[BsonIgnoreExtraElements]
    public class DiagnosticoModel
    {
        [BsonId]
        public Double Id { get; set; }
        [BsonElement("Nombre")]
        public string Nombre { get; set; }
        [BsonElement("Descripcion")]
        public string Descripcion { get; set; }
        [BsonElement("Sintomas")]
        public string Sintomas { get; set; }
        [BsonElement("Tratamiento")]
        public string[] Tratamiento { get; set; }
    }
}