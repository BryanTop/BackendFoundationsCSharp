using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using SqliteFromScratch;

namespace rawSql.Controllers
{
     [Route("api/[Controller]")]
     public class DatabaseController: Controller {
         
        string dataSource = "Data Source=" + Path.GetFullPath("chinook.db");
        List<Track> tracks = new List<Track>();
        List<Customer> customers = new List<Customer>();
        List<Employee> employees = new List<Employee>(); 

         [HttpGet]
         public ViewResult ShowData(string dataType, int limit = 20, string year = "2003-01-01 00:00:00")
        {
            
            string sql = "";
            switch (dataType)
            {
                case "track":
                    sql = $"select * from tracks limit 200;";
                    GetData("track", sql);
                    return View("tracks", tracks);
                case "customer":
                     sql = "Select * From customers limit " + limit + ";";
                    GetData("customer", sql);
                    return View("customer", customers);
                case "employee":
                     sql = "Select * From employees Where HireDate < '" + year + "';";
                    GetData("employee", sql);
                    return View("employee", employees);
            }
            return View("Index");
            
        }

        private void GetData(string dataType, string sql)
        {
            using (SqliteConnection conn = new SqliteConnection(dataSource))
            {
                conn.Open();

                using (SqliteCommand command = new SqliteCommand(sql, conn))
                {
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string state;
                            string company;
                            string postalCode;
                            string fax;
                            switch (dataType)
                            {
                                case "track":
                                    Track newTrack = new Track()
                                        {
                                            Id = reader.GetInt32(0),
                                            Name = reader.GetString(1),
                                            AlbumId = reader.GetInt32(2),
                                            MediaTypeId = reader.GetInt32(3),
                                            GenreId = reader.GetInt32(4),
                                            Composer = reader.GetValue(5).ToString(),
                                            Milliseconds = reader.GetInt32(6),
                                            Bytes = reader.GetInt32(7),
                                            UnitPrice = reader.GetInt32(8)
                                        };

                                    tracks.Add(newTrack);
                                    break;
                                case "customer":;
                                if (reader.IsDBNull(3)) 
                                {
                                    company = "";
                                } else {
                                    company = reader.GetString(3);
                                }

                                if(reader.IsDBNull(6)) {
                                    state = "";
                                } else {
                                    state = reader.GetString(6);
                                }

                                if(reader.IsDBNull(8)) {
                                    postalCode = "";
                                } else {
                                    postalCode = reader.GetString(8);
                                }

                                if(reader.IsDBNull(10)) {
                                    fax = "";
                                } else {
                                    fax = reader.GetString(10);
                                }
                                    Customer newCutomer = new Customer() {
                                        CustomerId = reader.GetInt32(0),
                                        FirstName = reader.GetString(1),
                                        LastName = reader.GetString(2),
                                        Company = company,
                                        Address = reader.GetString(4),
                                        City = reader.GetString(5),
                                        State = state,
                                        Country = reader.GetString(7),
                                        PostalCode = postalCode,
                                        Phone = reader.GetString(9),
                                        Fax = fax,
                                        Email = reader.GetString(11),
                                        SupportRepId = reader.GetInt32(12)
                                    };
                                    customers.Add(newCutomer);
                                    break;
                                case "employee": 

                                    if(reader.IsDBNull(9)) {
                                        state = "";
                                    } else {
                                        state = reader.GetString(9);
                                    }

                                    if(reader.IsDBNull(11)) {
                                        postalCode = "";
                                    } else {
                                        postalCode = reader.GetString(11);
                                    }

                                    if(reader.IsDBNull(13)) {
                                        fax = "";
                                    } else {
                                        fax = reader.GetString(13);
                                    }
                                    int reportsTo;
                                    if(reader.IsDBNull(4)) {
                                        continue;
                                    } else {
                                        reportsTo = reader.GetInt32(4);
                                    }
                                    Employee newEmployee = new Employee() {
                                        EmployeeId = reader.GetInt32(0),
                                        LastName = reader.GetString(1),
                                        FirstName = reader.GetString(2),
                                        Title = reader.GetString(3),
                                        ReportsTo = reportsTo,
                                        BirthDate = reader.GetString(5),
                                        HireDate = reader.GetString(6),
                                        Address = reader.GetString(7),
                                        City = reader.GetString(8),
                                        State = state,
                                        Country = reader.GetString(10),
                                        PostalCode = postalCode,
                                        Phone = reader.GetString(12),
                                        Fax = fax,
                                        Email = reader.GetString(14),
                                    };
                                    employees.Add(newEmployee);
                                    break;
                                default:
                                    break;
                            }

                        }
                    }
                }

                conn.Close();
            }
        }

     }
}