

using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Simple_Inventory_Management_System;

public class Product
{
    [BsonId]
    public ObjectId ProductID { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}
