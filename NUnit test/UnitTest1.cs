using NUnit.Framework;
using System.Data.SQLite;
using LorenaTest;
using System.Collections.Generic;

namespace NUnit_test
{
    public class Tests
    {
        private DB _db;
        private List<Salon> _salonList;
        private Dictionary<string, string> _salonDictionary;
        private string _connectionString;

        [SetUp]
        public void Setup()
        {
            _connectionString = "Data Source=Test.db;Version=3;";
            _db = new DB(_connectionString);


            _salonDictionary = new Dictionary<string, string>
            {
                { "Миасс" , null },
                { "Амелия" , "Миасс" },
                {"Тест1", "Амелия" },
                {"Тест2", "Миасс"  },
                {"Курган", null }

            };


            _salonList = new List<Salon>
            {
                new Salon("Миасс", 4, false, "", null),
                new Salon("Амелия", 5, true, "", null),
                new Salon("Тест1", 2, true, "", null),
                new Salon("Тест2", 0, true, "", null),
                new Salon("Курган", 11, false, "", null)
            };

        }


        /// <summary>
        /// Создание новой базы данных и таблиц в ней
        /// </summary>
        [Test]
        public void CreateDB()
        {
            _db.Create();
        }


        /// <summary>
        /// Добавление новых данных в базу
        /// </summary>
        [Test]
        public void Insert()
        {
            Salon miass = new Salon(
                "Миасс",
                4,
                false,
                "",
                null
                );
            _db.Insert( miass );

            Salon amelia = new Salon(
                "Амелия",
                5,
                true,
                "салон Амелия",
                _db.SelectParentId("Миасс")
                );

            _db.Insert( amelia );
            
        }

        [Test]
        public void InsertSalonList()
        {
            foreach(var salon in _salonList)
            {
                _db.Insert(salon);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void InsertParentIdInDB()
        {
           
            foreach(var kv in _salonDictionary)
            {
                _db.UpdateParentId(kv.Key, kv.Value);
            }
            
        }


        /// <summary>
        /// Проверка ограничения в 124 символа для описания
        /// </summary>
        [TestCase(1)]
        [TestCase(50)]
        [TestCase(124)]
        [TestCase(255)]
        public void UpdateDescription(int lengthDescription)
        {
            string query = "UPDATE Salon SET Description = @text WHERE Id = 1";
            using (var connect = new SQLiteConnection(_connectionString))
            {
                connect.Open();
                string st = new string('A', lengthDescription);
                using (var command = new SQLiteCommand(query, connect))
                {
                     command.Parameters.AddWithValue("@text", st);
                     command.ExecuteNonQuery();
                }
            }
        }
    }
}