using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using QuantityAppModel;

namespace QuantityAppRepository
{
    public class QuantityMeasurementDatabaseRepository : IQuantityMeasurementRepository
    {
        private readonly string ConnectionString;

        public QuantityMeasurementDatabaseRepository(string connectionString)
        {
            ConnectionString = connectionString;   
        }

        public bool IsDatabaseAvailable()
        {
            try
            {
                using SqlConnection connection = new SqlConnection(ConnectionString);
                connection.Open();
                return true;
            }
            catch
            {
                return false;
            }
        }


        public void SaveMeasurement(QuantityMeasurementEntity entity)
        {
            string query = @"INSERT INTO QuantityMeasurements
                            (Operand1Value, Operand1Unit,
                             Operand2Value, Operand2Unit,
                             MeasurementType, OperationType,
                             ResultValue, ResultUnit,
                             ErrorMessage, CreatedAt)
                            VALUES
                            (@Operand1Value, @Operand1Unit,
                             @Operand2Value, @Operand2Unit,
                             @MeasurementCategory, @OperationType,
                             @ResultValue, @ResultUnit,
                             @ErrorMessage, @CreatedAt)";


            using SqlConnection con = new SqlConnection(ConnectionString);
            using SqlCommand command = new SqlCommand(query, con);

            command.Parameters.AddWithValue("@Operand1Value", entity.Operand1Value);
            command.Parameters.AddWithValue("@Operand1Unit", entity.Operand1Unit);
            command.Parameters.AddWithValue("@Operand2Value", (object?)entity.Operand2Value ?? DBNull.Value);
            command.Parameters.AddWithValue("@Operand2Unit", (object?)entity.Operand2Unit ?? DBNull.Value);
            command.Parameters.AddWithValue("@MeasurementCategory", entity.MeasurementCategory);
            command.Parameters.AddWithValue("@OperationType", entity.OperationType);
            command.Parameters.AddWithValue("@ResultValue", (object?)entity.ResultValue ?? DBNull.Value);
            command.Parameters.AddWithValue("@ResultUnit", (object?)entity.ResultUnit ?? DBNull.Value);
            command.Parameters.AddWithValue("@ErrorMessage", (object?)entity.ErrorMessage ?? DBNull.Value);
            command.Parameters.AddWithValue("@CreatedAt", entity.CreatedAt);

            con.Open();
            command.ExecuteNonQuery();
        }

        public List<QuantityMeasurementEntity> GetAllMeasurements()
        {
            List<QuantityMeasurementEntity> measurements = new List<QuantityMeasurementEntity>();
            
            string query = "SELECT * FROM QuantityMeasurements ORDER BY CreatedAt DESC";

            using SqlConnection con = new SqlConnection(ConnectionString);    
            using SqlCommand command = new SqlCommand(query, con);

            con.Open();

            using SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                measurements.Add(MapReaderToEntity(reader));
            }

            return measurements;    
        }


        public List<QuantityMeasurementEntity> GetMeasurementsByOperation(string operationType)
        {
            List<QuantityMeasurementEntity> results = new List<QuantityMeasurementEntity>();

            string query = $"SELECT * FROM QuantityMeasurements WHERE OperationType = @OperationType";

            using SqlConnection con = new SqlConnection(ConnectionString);
            using SqlCommand command = new SqlCommand(query, con);

            command.Parameters.AddWithValue("@OperationType", operationType);

            con.Open();

            using SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                results.Add(MapReaderToEntity(reader));
            } 

            return results;
        }


        public List<QuantityMeasurementEntity> GetMeasurementsByCategory(string category)
        {
            List<QuantityMeasurementEntity> results = new List<QuantityMeasurementEntity>();

            string query = "SELECT * FROM QuantityMeasurements WHERE MeasurementCategory = @Category";

            using SqlConnection con = new SqlConnection(ConnectionString);
            using SqlCommand commamd = new SqlCommand(query, con);

            con.Open();

            using SqlDataReader reader = commamd.ExecuteReader();

            while (reader.Read())
            {
                results.Add(MapReaderToEntity(reader));
            }

            return results;
        }

        public int GetMeasurementCount()
        {
            int count = 0;
            string query = "SELECT COUNT(*) FROM QuantityMeasurements";

            SqlConnection con = new SqlConnection(ConnectionString);
            SqlCommand command = new SqlCommand(query, con);

            con.Open();

            count = (int) command.ExecuteScalar();
            return count;
        }

        public void DeleteAllMeasurements()
        {
            string query = "DELETE FROM QuantityMeasurements";

            using SqlConnection con = new SqlConnection(ConnectionString);
            using SqlCommand command = new SqlCommand(query, con);

            con.Open();
            command.ExecuteNonQuery();
        }

        private QuantityMeasurementEntity MapReaderToEntity(SqlDataReader reader)
        {
          return new QuantityMeasurementEntity
          {
            Id = Convert.ToInt32(reader["Id"]),
            Operand1Value = Convert.ToDouble(reader["Operand1Value"]),
            Operand1Unit = reader["Operand1Unit"].ToString() ?? "", 
            Operand2Value = reader["Operand2Value"] as double?,
            Operand2Unit = reader["Operand2Unit"].ToString(),
            MeasurementCategory = reader["MeasurementCategory"].ToString() ?? "",
            OperationType = reader["OperationType"].ToString() ?? "",
            ResultValue = reader["ResultValue"] as double?,
            ResultUnit = reader["ResultUnit"]?.ToString(),
            ErrorMessage = reader["ErrorMessage"]?.ToString(),
            CreatedAt = Convert.ToDateTime(reader["CreatedAt"])
          };   
        }


        public void SaveMeasurementsFromCache(List<QuantityMeasurementEntity> list)
        {
            foreach(var entity in list)
            {
                SaveMeasurement(entity);
            }
        }

    }
}