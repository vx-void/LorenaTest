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


        [SetUp]
        public void Setup()
        {
            string connectionString = "Data Source=Test.db;Version=3;";
            _db = new DB(connectionString);


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

        [Test]
        public void InsertParentIdInDB()
        {
           
            foreach(var kv in _salonDictionary)
            {
                _db.UpdateParentId(kv.Key, kv.Value);
            }
            
        }
    }
}