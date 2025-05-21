using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.SQLite;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace LorenaTest
{
    public class DB
    {
        private readonly string _connectionString;

        public DB(string connectionString)
        {
            _connectionString = connectionString;
        }


        /// <summary>
        /// Создает таблицы в базе данных.
        /// </summary>
        public void Create()
        {
            using(var connection = new SQLiteConnection(_connectionString))
            {
                string discountTable = @"CREATE TABLE IF NOT EXISTS " +
                    "Salon(Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, " +
                    "Name TEXT NOT NULL, " +
                    "Discount REAL NOT NULL, " +
                    "HasDependency INTEGER NOT NULL, " +
                    "Description TEXT, " +
                    "ParentID INTEGER); ";

                string finalTable = @"CREATE TABLE IF NOT EXISTS " +
                     "CalculationTable(Id INTEGER  NOT NULL PRIMARY KEY AUTOINCREMENT, " +
                     "SalonId INTEGER NOT NULL," +
                     "Price REAL NOT NULL, " +
                     "ParentDiscount REAL NOT NULL, " +
                     "FinalPrice REAL NOT NULL, " +
                     "FOREIGN KEY (SalonId) REFERENCES Salon(Id));";

                connection.Open();
                using (var cmd = new SQLiteCommand(discountTable, connection))
                {
                    cmd.ExecuteNonQuery();
                }
                using (var cmd = new SQLiteCommand(finalTable, connection))
                {
                    cmd.ExecuteNonQuery();
                }              
            }
        }


        /// <summary>
        /// добавление салона в таблицу БД
        /// </summary>
        /// <param name="salon">Салон</param>
        public void Insert(Salon salon)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                string sqlCommandString = @"
            INSERT INTO Salon (Name, Discount, HasDependency, Description, ParentId)
            VALUES (@Name, @Discount, @HasDependency, @Description, @ParentId);";

                ExecuteNonQuery(connection, sqlCommandString, new SQLiteParameter[]
                {
                    new SQLiteParameter("@Name", salon.Name),
                    new SQLiteParameter("@Discount", salon.Discount),
                    new SQLiteParameter("@HasDependency", salon.HasDependency ? 1 : 0),
                    new SQLiteParameter("@Description", salon.Description ?? (object)DBNull.Value),
                    new SQLiteParameter("@ParentId", salon.ParentId ?? (object)DBNull.Value)
                });
            }
        }


        /// <summary>
        /// Выбор родительсмкого салона
        /// </summary>
        /// <param name="salonId">ID салона</param>
        /// <returns>ID родительского салона</returns>
        public int SelectParent(int salonId)
        {
            int parentId = -1;
            using(var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                string query = @"SELECT parentId From Salon Where SalonId = @SalonId";
                using(var command = new SQLiteCommand(query, connection))
                {
                    command.CommandText = query;
                    command.Parameters.Add(new SQLiteParameter("@SalonId", salonId));
                    var result = command.ExecuteScalar();                    
                    if(result != null && result != DBNull.Value)
                    {
                        parentId = Convert.ToInt32(result);
                    }
                }
            }
            return parentId;
        }
        

        public int? SelectParentId(string name)
        {
            int? parentId = null;
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                string query = @"SELECT Id From Salon Where Name = @name";
                using (var command = new SQLiteCommand(query, connection))
                {
                    command.CommandText = query;
                    command.Parameters.Add(new SQLiteParameter("@name", name));
                    var result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        parentId = Convert.ToInt32(result);
                    }
                }
            }
            return parentId;
        }


        public void UpdateParentId(string name, string parentName)
        {
            if(parentName != null)
            {
                string query = "UPDATE Salon " +
                  "SET ParentID = (Select Id From Salon WHERE Name = @parentName) " +
                  "WHERE Name = @name";
                using(var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@parentName", parentName);
                        command.Parameters.AddWithValue("@name", name);
                        command.ExecuteNonQuery();
                    }
                    
                }

            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="query"></param>
        /// <param name="parameters"></param>

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
