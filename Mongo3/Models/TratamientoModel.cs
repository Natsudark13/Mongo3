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
    public class TratamientoModel
    {
        [BsonId]
        public ObjectId _id { get; set; }
        [BsonElement("ID")]
        public string ID { get; set; }
        [BsonElement("Nombre")]
        public string Nombre { get; set; }
        [BsonElement("Tipo")]
        public string Tipo { get; set; }
        [BsonElement("Dosis")]
        public string Dosis { get; set; }
        [BsonElement("Monto")]
        public double Monto { get; set; }
    }
}