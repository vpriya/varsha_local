using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Data;
using Npgsql;
using vpWebApplication.Models;

namespace vpWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public EmployeeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()

        {
            string query = @"
                            select EmployeeId as ""EmployeeId"", 
                            EmployeeName as ""EmployeeName"", 
                            Department as ""Department"", 
                            to_char(DateOfJoining,'YYYY-MM-DD') as ""DateOfJoining"",
                            PhotoFileName as ""PhotoFileName""
                            from public.Employee
                            ";
            DataTable table = new DataTable();// creating a datatable object

            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");

            NpgsqlDataReader myReader; // make use of SQLdatareader to populate the data into datatable object

            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                //with given sql sonnection and sql command, we will execute our query
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);// fill the data in the datatable using sql data-reader

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);// finally return the data into json format
        }

        [HttpPost]
        // we will be sending the department object to post method in the form body
        public JsonResult Post(Employee emp)
        {
            string query = @"
                            insert into public.Employee(EmployeeName,Department,DateOfJoining,PhotoFileName) 
                            values (@EmployeeName,@Department,@DateOfJoining,@PhotoFileName)
                            ";
            DataTable table = new DataTable();// creating a datatable object

            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");

            NpgsqlDataReader myReader; // make use of SQLdatareader to populate the data into datatable object

            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                //with given sql sonnection and sql command, we will execute our query
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@EmployeeName", emp.EmployeeName);
                    myCommand.Parameters.AddWithValue("@Department", emp.Department);
                    myCommand.Parameters.AddWithValue("@DateOfJoining", Convert.ToDateTime(emp.DateOfJoining));
                    myCommand.Parameters.AddWithValue("@PhotoFileName", emp.PhotoFileName);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);// fill the data in the datatable using sql data-reader

                    myReader.Close();
                    myCon.Close();
                }
            }

            // return something
            return new JsonResult("Added Successfully");

        }
        [HttpPut]
        // we will be sending the department object to post method in the form body
        public JsonResult Put(Employee emp)
        {
            string query = @"
                            update public.Employee set 
                            EmployeeName = '" + emp.EmployeeName + @"',
                            Department = '"" + emp.Department + @""',
                            DateOfJoining = '"" + emp.DateOfJoining + @""',
                            PhotoFileName = '"" + emp.PhotoFileName + @""',
                            where EmployeeId = " + emp.EmployeeId + @"
                            ";
            DataTable table = new DataTable();// creating a datatable object

            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");

            NpgsqlDataReader myReader; // make use of SQLdatareader to populate the data into datatable object

            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                //with given sql sonnection and sql command, we will execute our query
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@EmployeeName", emp.EmployeeName);
                    myCommand.Parameters.AddWithValue("@Department", emp.Department);
                    myCommand.Parameters.AddWithValue("@DateOfJoining", Convert.ToDateTime(emp.DateOfJoining));
                    myCommand.Parameters.AddWithValue("@PhotoFileName", emp.PhotoFileName);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);// fill the data in the datatable using sql data-reader

                    myReader.Close();
                    myCon.Close();
                }
            }

            // return something
            return new JsonResult("Updated Successfully");

        }

        [HttpDelete("{id}")]
        // we will be sending the department object to post method in the form body
        public JsonResult Delete(int id)
        {
            string query = @"
                            delete from public.Employee
                            where EmployeeId = " + id + @"
                            ";
            DataTable table = new DataTable();// creating a datatable object

            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");

            NpgsqlDataReader myReader; // make use of SQLdatareader to populate the data into datatable object

            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                //with given sql sonnection and sql command, we will execute our query
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);// fill the data in the datatable using sql data-reader

                    myReader.Close();
                    myCon.Close();
                }
            }

            // return something
            return new JsonResult("Deleted Successfully");

        }
    }
}
