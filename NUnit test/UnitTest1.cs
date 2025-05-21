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
                { "�����" , null },
                { "������" , "�����" },
                {"����1", "������" },
                {"����2", "�����"  },
                {"������", null }

            };


            _salonList = new List<Salon>
            {
                new Salon("�����", 4, false, "", null),
                new Salon("������", 5, true, "", null),
                new Salon("����1", 2, true, "", null),
                new Salon("����2", 0, true, "", null),
                new Salon("������", 11, false, "", null)
            };

        }


        /// <summary>
        /// �������� ����� ���� ������ � ������ � ���
        /// </summary>
        [Test]
        public void CreateDB()
        {
            _db.Create();
        }


        /// <summary>
        /// ���������� ����� ������ � ����
        /// </summary>
        [Test]
        public void Insert()
        {
            Salon miass = new Salon(
                "�����",
                4,
                false,
                "",
                null
                );
            _db.Insert( miass );

            Salon amelia = new Salon(
                "������",
                5,
                true,
                "����� ������",
                _db.SelectParentId("�����")
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
        /// �������� ����������� � 124 ������� ��� ��������
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