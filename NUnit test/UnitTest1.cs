using NUnit.Framework;
using System.Data.SQLite;
using LorenaTest;
using System.Collections.Generic;
using System;

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


        }




    }
}