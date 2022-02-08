using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

using System;
using System.Collections.Generic;

namespace LeakedAccountApi.Models
{
    public class LeakedAccount
    {
        [BsonId]
        [BsonElement("_id")]
        public ObjectId Id { get; set; }

        [BsonElement("email")]
        [BsonRepresentation(BsonType.String)]
        public string Email { get; set; }

        [BsonElement("passwords")]
        public List<string> Passwords { get; set; }

        [BsonElement("integrationTime")]
        [BsonDateTimeOptions(Representation = BsonType.DateTime)]
        public DateTime integrationTime { get; set; }
    }
}
