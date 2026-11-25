using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
namespace CattleCart
{
    public static class Class1
    {
        public static string ConnectionString
        {
            get { return Environment.GetEnvironmentVariable("CATTLECART_CONNECTION_STRING") ??
                ConfigurationManager.ConnectionStrings["CattleCart"].ConnectionString; }
        }
        public static SqlConnection GetConnection() { return new SqlConnection(ConnectionString); }
        public static void InitializeDatabase()
        {
            var builder = new SqlConnectionStringBuilder(ConnectionString);
            string database = builder.InitialCatalog;
            if (string.IsNullOrWhiteSpace(database)) throw new InvalidOperationException("Set Initial Catalog in App.config.");
            builder.InitialCatalog = "master";
            using (var con = new SqlConnection(builder.ConnectionString))
            {
                con.Open();
                using (var cmd = new SqlCommand("IF DB_ID(@name) IS NULL BEGIN DECLARE @sql nvarchar(max) = N'CREATE DATABASE ' + QUOTENAME(@name); EXEC sp_executesql @sql; END", con))
                { cmd.Parameters.AddWithValue("@name", database); cmd.ExecuteNonQuery(); }
            }
            using (var con = GetConnection())
            using (var stream = typeof(Class1).Assembly.GetManifestResourceStream("CattleCart.Database.sql"))
            using (var reader = new StreamReader(stream))
            {
                con.Open();
                using (var cmd = new SqlCommand(reader.ReadToEnd(), con)) cmd.ExecuteNonQuery();
            }
        }
    }
}
