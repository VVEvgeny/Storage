using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;

namespace mongodb
{
    class Program
    {
        class Person
        {
            public ObjectId Id { get; set; }
            public string Name { get; set; }
            public string Surname { get; set; }
            public int Age { get; set; }
            public Company Company { get; set; }
            public List<string> Languages { get; set; }
        }

        class Employee
        {
            public string Name { get; set; }
            public int Age { get; set; }
        }

        class Company
        {
            public string Name { get; set; }
        }

        static void Main(string[] args)
        {
            //mongodb://[username:password@]hostname[:port][/[database][?options]]
            //mongodb://user:pass@localhost/db1?authSource=userDb
            string connectionString = "mongodb://localhost:27017";



            MongoClient client = new MongoClient(connectionString);
            //GetDatabaseNames(client).Wait();
            //GetCollectionsNames(client).Wait();
            //SaveDocs().Wait();
            //FindDocs().Wait();
            //UpdatePerson().Wait();


            //UploadFileAsync().Wait();
            //DownloadFileAsync().Wait();
            //FindFileAsync().Wait();
            //ReplaceFileAsync().Wait();
            DeleteFileAsync().Wait();

            //Console.ReadLine();
        }

        private static async Task DeleteFileAsync()
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("test");
            IGridFSBucket gridFS = new GridFSBucket(database);
            var builder = new FilterDefinitionBuilder<GridFSFileInfo>();
            var filter = Builders<GridFSFileInfo>.Filter.Eq<string>(info => info.Filename, "cat.jpg");
            var fileInfos = await gridFS.FindAsync(filter);
            var fileInfo = fileInfos.FirstOrDefault();

            await gridFS.DeleteAsync(fileInfo.Id);
        }
        private static async Task ReplaceFileAsync()
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("test");
            IGridFSBucket gridFS = new GridFSBucket(database);
            using (Stream fs = new FileStream("D:\\forest.jpg", FileMode.Open))
            {
                await gridFS.UploadFromStreamAsync(
                    "cat.jpg",
                    fs,
                    new GridFSUploadOptions { Metadata = new BsonDocument("filename", "cat.jpg") });
            }
        }
        private static async Task FindFileAsync()
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("test");
            IGridFSBucket gridFS = new GridFSBucket(database);
            // создаем фильтр для поиска
            var filter = Builders<GridFSFileInfo>.Filter.Eq<string>(info => info.Filename, "cat.jpg");
            // находим все файлы
            var fileInfos = await gridFS.FindAsync(filter);
            // получаем первый файл
            var fileInfo = fileInfos.FirstOrDefault();
            // выводим его id
            Console.WriteLine("id = {0}", fileInfo.Id);
        }
        private static async Task DownloadFileAsync()
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("test");
            IGridFSBucket gridFS = new GridFSBucket(database);

            using (Stream fs = new FileStream("D:\\cat_new.jpg", FileMode.OpenOrCreate))
            {
                await gridFS.DownloadToStreamByNameAsync("cat.jpg", fs);
            }
        }
        private static async Task UploadFileAsync()
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("test");

            IGridFSBucket gridFS = new GridFSBucket(database);
            using (Stream fs = new FileStream("D:\\cat.jpg", FileMode.Open))
            {
                ObjectId id = await gridFS.UploadFromStreamAsync("cat.jpg", fs);
                Console.WriteLine("id файла: {0}", id.ToString());
            }
        }


        private static async Task UpdatePerson()
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("test");
            var collection = database.GetCollection<BsonDocument>("people");
            /*var result = await collection.ReplaceOneAsync(new BsonDocument("Name", "Bob"),
                new BsonDocument
                {
                    {"Name", "Robert"},
                    {"Age", 40},
                    {"Languages", new BsonArray(new[] {"english", "spanish"})},
                    {
                        "Company", new BsonDocument
                        {
                            {"Name", "Bob&Ron Inc."}
                        }
                    }
                },
                new UpdateOptions {IsUpsert = true});
            */
            /*var result = await collection.UpdateOneAsync(
                new BsonDocument("Name", "Tom K."),
                new BsonDocument("$set", new BsonDocument("Age", 28)));*/
            /*var result = await collection.UpdateOneAsync(
                new BsonDocument("Name", "Robert"),
                new BsonDocument("$inc", new BsonDocument("Age", 2)));*/

            // параметр фильтрации 
            var filter = Builders<BsonDocument>.Filter.Eq("Name", "Tom");
            // параметр обновления
            var update = Builders<BsonDocument>.Update.Set("Age", 30);
            var result = await collection.UpdateOneAsync(filter, update);

            Console.WriteLine("Найдено по соответствию: {0}; обновлено: {1}",
                result.MatchedCount, result.ModifiedCount);
            var people = await collection.Find(new BsonDocument()).ToListAsync();
            foreach (var p in people)
                Console.WriteLine(p);
        }

        private static async Task FindDocs()
        {
            string connectionString = "mongodb://localhost";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("test");
            var collection = database.GetCollection<BsonDocument>("people");
            var filter = new BsonDocument();
            //var filter = new BsonDocument("Age", new BsonDocument("$gt", 31));
            /*
            var filter = new BsonDocument("$and", new BsonArray
            {
                new BsonDocument("Age", new BsonDocument("$gt", 31)),
                new BsonDocument("Name", "Bill")
            });
            */
            //var filter = Builders<BsonDocument>.Filter.Eq("Name", "Bill");
            //var builder = Builders<BsonDocument>.Filter;
            //var filter = builder.Eq("Name", "Bill") | builder.Eq("Name", "Tom");


            /*
            using (var cursor = await collection.FindAsync(filter))
            {
                while (await cursor.MoveNextAsync())
                {
                    var people = cursor.Current;
                    foreach (var doc in people)
                    {
                        Console.WriteLine(doc);
                    }
                }
            }
            */
            //var people = await collection.Find(filter).ToListAsync();
            /*
            var collection = database.GetCollection<Person>("people");
            var people = await collection.Find(x => x.Name == "Bill" && x.Age > 30).ToListAsync();
            foreach (var p in people)
            {
                Console.WriteLine("{0} - {1}", p.Name, p.Age);
            }
            */
            //var people = await collection.Find(new BsonDocument()).Sort("{Age:1}").ToListAsync();
            //var people = await collection.Find(filter).Skip(2).Limit(3).ToListAsync();
            //var people = await collection.Find(new BsonDocument()).Project("{Name:1, Age:1, _id:0}").ToListAsync();
            /*
            var people = await collection.Find(new BsonDocument())
                .Project(Builders<BsonDocument>.Projection.Include("Name").Include("Age").Exclude("_id"))
                .ToListAsync();
            */
            /*
            var projection = Builders<Person>.Projection.Expression(p => new Employee { Name = p.Name, Age = p.Age });
            var people = await collection.Find(filter).Project<Employee>(projection).ToListAsync();
            */
            //var people = await collection.Aggregate().Group(new BsonDocument { { "_id", "$Age" }, { "count", new BsonDocument("$sum", 1) } }).ToListAsync();
            var people = await collection.Aggregate()
                .Match(new BsonDocument {{"Name", "Tom"}, {"Company.Name", "Microsoft"}})
                .ToListAsync();
            foreach (var doc in people)
            {
                Console.WriteLine(doc);
            }
        }

        private static async Task SaveDocs()
        {
            string connectionString = "mongodb://localhost";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("test");
            var collection = database.GetCollection<BsonDocument>("people");
            BsonDocument person1 = new BsonDocument
            {
                {"Name", "Bill"},
                {"Age", 32},
                {"Languages", new BsonArray {"english", "german"}}
            };
            BsonDocument person2 = new BsonDocument
            {
                {"Name", "Steve"},
                {"Age", 31},
                {"Languages", new BsonArray {"english", "french"}}
            };
            await collection.InsertManyAsync(new[] {person1, person2});
        }

        private static async Task GetCollectionsNames(MongoClient client)
        {
            using (var cursor = await client.ListDatabasesAsync())
            {
                var dbs = await cursor.ToListAsync();
                foreach (var db in dbs)
                {
                    Console.WriteLine("В базе данных {0} имеются следующие коллекции:", db["name"]);

                    IMongoDatabase database = client.GetDatabase(db["name"].ToString());

                    using (var collCursor = await database.ListCollectionsAsync())
                    {
                        var colls = await collCursor.ToListAsync();
                        foreach (var col in colls)
                        {
                            Console.WriteLine(col["name"]);
                        }
                    }
                    Console.WriteLine();
                }
            }
        }

        private static async Task GetDatabaseNames(MongoClient client)
        {
            using (var cursor = await client.ListDatabasesAsync())
            {
                var databaseDocuments = await cursor.ToListAsync();
                foreach (var databaseDocument in databaseDocuments)
                {
                    Console.WriteLine(databaseDocument["name"]);
                }
            }
        }
    }
}
