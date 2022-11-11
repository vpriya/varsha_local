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
    public class DepartmentController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public DepartmentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()

        {
            string query = @"
                            select DepartmentId, DepartmentName 
                            from public.Department
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
        public JsonResult Post(Department dep)
        {
            string query = @"
                            insert into public.Department(DepartmentName) 
                            values (@DepartmentName)
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
                    myCommand.Parameters.AddWithValue("@DepartmentName", dep.DepartmentName);
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
        public JsonResult Put(Department dep)
        {
            string query = @"
                            update public.Department set 
                            DepartmentName = '"+dep.DepartmentName+@"'
                            where DepartmentId = "+dep.DepartmentId + @"
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
                    myCommand.Parameters.AddWithValue("@DepartmentName", dep.DepartmentName);
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
                            delete from public.Department
                            where DepartmentId = " + id + @"
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
