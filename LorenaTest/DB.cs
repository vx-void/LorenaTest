using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SQLite;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace LorenaTest
{
    public class DB
    {
        public string dbName { get; set; }
       

        public DB(string name)
        {
            dbName = name;
        }

        public void Create()
        {
            using(var connection = new SQLiteConnection($"Data Source={dbName};Version=3;"))
            {
                string commandString = @"CREATE TABLE IF NOT EXISTS " +
                    "Salon(id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, " +
                    "name TEXT NOT NULL, " +
                    "discount REAL NOT NULL, " +
                    "hasDependency INTEGER NOT NULL, " +
                    "description TEXT, " +
                    "parentID INTEGER); ";

                connection.Open();
                using (var cmd = new SQLiteCommand(commandString, connection))
                {
                    cmd.ExecuteNonQuery();
                }                
            }
        }

        public void Insert(Salon salon)
        {
            using (var connection = new SQLiteConnection($"Data Source = {dbName}; Version=3;"))
            {
                connection.Open();
                string SqlCommandString = @"" +
                    "INSERT INTO Salon" +
                    "(name, discount, hasDependency, Description, ParentID)" +
                    "VALUES" +
                    "(@name, @discount, @hasDependency, @Description, @parentID);";
                     ExecuteNonQuery(connection, SqlCommandString, new SQLiteParameter[] {
                new SQLiteParameter("@Name", salon.Name),
                new SQLiteParameter("@Discount", salon.Discount),
                new SQLiteParameter("@HasDependency", salon.IsDependency()),
                new SQLiteParameter("@Description", salon.Description ?? (object)DBNull.Value),
                new SQLiteParameter("@ParentId", salon.ParentId ?? (object)DBNull.Value)
            });

            }
        }

        private void ExecuteNonQuery(SQLiteConnection connection, string query, SQLiteParameter[] parameters = null)
        {
            using (var command = new SQLiteCommand(query, connection))
            {
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }
                command.ExecuteNonQuery();
            }
        }

    }

}
