using NUnit.Framework;
using System.Data.SQLite;
using LorenaTest;

namespace NUnit_test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CreateDB()
        {
            DB db = new DB("test.db");
            db.Create();
        }
    }
}