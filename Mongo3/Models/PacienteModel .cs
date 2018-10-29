using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;




namespace Mongo3.Models
{
    public class PacienteModel
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement("Cedula")]
        public string cedula { get; set; }
        [BsonElement("Nombre")]
        public string Nombre { get; set; }
        [BsonElement("Apellido1")]
        public string Apellido1 { get; set; }
        [BsonElement("Apellido2")]
        public string Apellido2 { get; set; }
        [BsonElement("Nacimiento")]
        public string Nacimiento { get; set; }
        [BsonElement("TipoSangre")]
        public string TipoSangre { get; set; }
        [BsonElement("Nacionalidad")]
        public string Nacionalidada { get; set; }
        [BsonElement("Residencia")]
        public string Residencia { get; set; }
        [BsonElement("Telefonos")]
        public double[] Telefonos { get; set; }
        [BsonElement("Contraseña")]
        public string Contraseña { get; set; }

    }
}