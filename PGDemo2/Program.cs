using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Npgsql;

namespace PGDemo2
{
    class Program
    {
        static void Main(string[] args)
        {
            //TestConnection();
            InsertRecord();
            //DeleteRecord();
            Console.ReadKey();
        }

        private static void InsertRecord()
        {
            using(NpgsqlConnection con=GetConnection())
            {
                string query = @"insert into public.Students(Name,Fees) values('VP4',400.0)";
                NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                if(rowsAffected == 1)
                {
                    Console.WriteLine("Record inserted");
                }
            }
        }

        private static void DeleteRecord()
        {
            using (NpgsqlConnection con = GetConnection())
            {
                string query = @"delete from students where id=2;";
                NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected == 1)
                {
                    Console.WriteLine("Record deleted");
                }
            }

        }

        private static void TestConnection()
        {
            using (NpgsqlConnection con=GetConnection())
            {
                con.Open();
                if(con.State==ConnectionState.Open)
                {
                    Console.WriteLine("Connected");
                }
            }
        }
        private static NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection(@"Server=localhost;Port=5432;Username=postgres;Password=;Database=testDB;Pooling=true;");
        }
        
    }
}
