using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace M101DotNet
{
    class Program
    {
        static void Main(string[] args)
        {
            // Console.WriteLine("Hello World!");
            // var doc = new BsonDocument {};
            // doc.Add("age", 30);
            // doc["profession"] = "hacker";

            // var nestedArray = new BsonArray();
            // nestedArray.Add(new BsonDocument("color", "red"));
            // nestedArray.Add(new BsonDocument("year", 2018));
            // doc.Add("array", nestedArray);

            // Console.WriteLine(doc["array"][1]["year"]);
            // Console.WriteLine(doc);
            
            MainAsync(args).Wait();
            Console.ReadLine();
        }

        static async Task MainAsync(string[] agrs)
        {

            var conventionPack = new ConventionPack();
            conventionPack.Add(new CamelCaseElementNameConvention());
            ConventionRegistry.Register("camelCase", conventionPack, t => true);
            await Task.Delay(500);

            var connectionString = "mongodb://localhost:27017";
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase("test");

            //var col = db.GetCollection<Person>("people");

            /* Insert document */
            // var person = new Person
            // {
            //     Name = "John",

            //     Age = 30,
            //     Colors = new List<string> {"red", "blue"},
            //     Pets = new List<Pet> {new Pet {Name="Fluffy", Type="Pig"}},
            //     ExtraElements = new BsonDocument("anotherName", "anotherValue")
            // };
            // await col.InsertOneAsync(person);
            // // Print document
            // using (var writer = new JsonWriter(Console.Out))
            // {
            //     BsonSerializer.Serialize(writer, person);
            // }

            /* Insert one and insert many Bson document */
            // var col = db.GetCollection<BsonDocument>("people");
            // var doc = new BsonDocument
            // {
            //     {"Name", "Smith"},
            //     {"Age", 30},
            //     {"Profession", "Hacker"}
            // };            

            // var doc2 = new BsonDocument
            // {
            //     {"SomethingElse", true}
            // };

            // await col.InsertOneAsync(doc);
            // await col.InsertManyAsync(new [] {doc, doc2});
            /* ********************************************** */

            /* Find and print document version 1 */
            // var col = db.GetCollection<BsonDocument>("people");
            // using (var cursor = await col.Find(new BsonDocument()).ToCursorAsync())
            // {
            //     while(await cursor.MoveNextAsync())
            //     {
            //         foreach(var doc in cursor.Current)
            //         {
            //             Console.WriteLine(doc);
            //         }
            //     }
            // }

            /* Find and print document version 2 */
            var col = db.GetCollection<BsonDocument>("people");
            await col.Find(new BsonDocument())
                .ForEachAsync(doc => Console.WriteLine(doc));

        }

        class Person
        {
            public ObjectId Id { get; set; }
            [BsonElement("name")]
            public string Name { get; set; }
            [BsonRepresentation(BsonType.String)]
            public int Age { get; set; }
            public List<string> Colors { get; set; }
            public List<Pet> Pets { get; set; }
            public BsonDocument ExtraElements { get; set; }

        }

        class Pet
        {
            public string Name { get; set; }
            public string Type { get; set; }
        }
    }
}
