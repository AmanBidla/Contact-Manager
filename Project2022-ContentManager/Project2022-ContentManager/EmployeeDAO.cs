/**
 * Group Members: Mary Adenuga, Adarsh K Ashok, Renoir Brown, Meetee Dave and Anandhu K. B 
 * Subject: COMP 600 
 * Assignment: Final Project (Employee Contact Manager)
 */

/*
 * This file handles the connection between the SQL database and the employee contact manager, as well as SQL queries.
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Microsoft.Extensions.Logging;

namespace Project2022_ContentManager
{
    public class EmployeeDAO
    {
       private readonly ILogger<EmployeeDAO> _logger;
        public EmployeeDAO(ILogger<EmployeeDAO> logger)
        {
            _logger = logger;
        }

        public EmployeeDAO() { }

        public SqlConnection getDefaultConnection()//Connection object with pre-entered information for connecting to the Employees Database
        {
            //String connString = "Data Source = localhost; Initial Catalog = Employees; Persist Security Info = True; User ID = EmpDBApps; Password = java1";
            String connString = "Data Source = localhost; Initial Catalog = Employees; Persist Security Info = True; Integrated Security = True";
            SqlConnection conn = new SqlConnection(connString);

            return conn;
        }

        public SqlConnection getDefinedConnection(String serverName, String dataBaseName, String userName, String passWord)//Connection object with parameters which could be used to connect it to a specified Database (unused currently)
        {
            String connString = "Data Source = " + serverName + "; Initial Catalog = " + dataBaseName + "; User ID = " + userName + "; Password = " + passWord;

            SqlConnection conn = new SqlConnection(connString);

            return conn;
        }

        public SqlDataReader executeQuery(String sqlCmd, SqlConnection conn)
        {
            SqlCommand cmd = new SqlCommand(sqlCmd, conn);
            SqlDataReader reader = cmd.ExecuteReader();

            return reader;
        }

        public void executeNonQuery(String sqlCmd, SqlConnection conn)
        {
            SqlCommand cmd = new SqlCommand(sqlCmd, conn);
            cmd.ExecuteNonQuery();
        }

        public void AddEmployee(HourlyEmployee e)//To be used for adding new employees to the Database
        {
            try
            {
                SqlConnection conn = getDefaultConnection();
                Address a = e.Address;
               //_logger.LogInformation(e.EmployeeNumber + " " + e.JobDescription + " " + e.LastName + " " + e.FirstName + " " + e.MiddleInit + " " + e.PhoneNumber + " " + a.Street + " " + a.City + " " + a.Province + " " + a.PostalCode);
                String sql = "INSERT INTO Employee values (" + e.EmployeeNumber + ", '"
                         + e.JobDescription + "', '"
                         + e.LastName + "', '"
                         + e.FirstName + "', '"
                         + e.MiddleInit + "', '"
                         + e.PhoneNumber + "', '"
                         + a.Street + "', '"
                         + a.City + "', '"
                         + a.Province + "', '"
                         + a.PostalCode + "');";

                /* String sql = "INSERT INTO Employee values (" + e.EmployeeNumber + ", "
                          + e.JobDescription + ", "
                          + e.LastName + ", "
                          + e.FirstName + ", "
                          + e.MiddleInit + ", "
                          + e.PhoneNumber + ", "
                          + a.Street + ", "
                          + a.City + ", "
                          + a.Province + ", "
                          + a.PostalCode + ");";*/


                conn.Open();
                executeQuery(sql, conn);
                conn.Close();

                if (e is HourlyEmployee)
                {
                    // HourlyEmployee h = (HourlyEmployee)e;

                    /* sql = "INSERT INTO HourlyEmployee Set EmployeeID = " + e.EmployeeNumber
                             + ", HourlyRate = " + e.HourlyRate
                             + "HoursWorked = " + e.HoursWorked;*/

                    sql = "INSERT INTO HourlyEmployee values (" + e.EmployeeNumber + ", "
                         + e.HourlyRate + ", "
                         + e.HoursWorked + ");";

                    conn.Open();
                    executeQuery(sql, conn);
                    conn.Close();

                }

                /* else if (e is SalaryEmployee)
                 {
                     SalaryEmployee s = (SalaryEmployee)e;

                     sql = "INSERT INTO SalaryEmployee Set MonthlySalary = " + s.GetMonthlySalary()
                             + "EmployeeID = " + s.EmployeeNumber;
                 }*/
            }

            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.StackTrace.ToString());
            }

        }

        public void UpdateEmployee(Employee e)
        {
            SqlConnection conn = getDefaultConnection();
            Address a = e.Address;

            try
            {
                String sql = "Update Employee Set LastName='" + e.LastName + "', "
                        + "FirstName = '" + e.FirstName + "', "
                        + "MiddleInit = '" + e.MiddleInit + "', "
                        + "PhoneNumber = '" + e.PhoneNumber + "', "
                        + "Street = '" + a.Street + "', "
                        + "City = '" + a.City + "', "
                        + "Province = '" + a.Province + "', "
                        + "PostalCode = '" + a.PostalCode + "', "
                        + "JobDescription = '" + e.JobDescription + "' "
                        + "WHERE EmployeeID = " + e.EmployeeNumber;

                conn.Open();//remember this or nothing will happen...
                executeQuery(sql, conn);//Runs the SQL query...

                /*
                if (e is HourlyEmployee){
                    HourlyEmployee h = (HourlyEmployee)e;

                    sql = "Update HourlyEmployee Set HoursWorked = " + h.GetHoursWorked()
                            + ", HourlyRate = " + h.GetHourlyRate()
                            + " WHERE EmployeeID = " + h.EmployeeNumber;
                }

                else if (e is SalaryEmployee)
                {
                    SalaryEmployee s = (SalaryEmployee)e;

                    sql = "Update SalaryEmployee Set MonthlySalary = " + s.GetMonthlySalary()
                            + " WHERE EmployeeID = " + s.EmployeeNumber;
                }
                

                executeQuery(sql, conn);
                */

                conn.Close();
            }

            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.StackTrace.ToString());
            }
        }

        public ArrayList GetEmployee()
        {
            ArrayList Employees = new ArrayList();

            SqlConnection conn = getDefaultConnection();

            conn.Open();

            try
            {

                /**
                 * Instead of doing two separate SELECTs for each employee type, do 
                 * one big SELECT which captures all of them at once and an ORDER BY
                 * to ensure that they are organized by Employee ID. Use an IF
                 * statement checking for the existence of Salary to decide what type
                 * of employee is being placed into the array list as you go along.
                 * This is important in order to show the correct interface for each
                 * employee type. 
                 */

                String sql = "SELECT e.EmployeeID EmployeeID, JobDescription, " +
                         "LastName, FirstName, MiddleInit, PhoneNumber, Street, " +
                         "City, Province, PostalCode, MonthlySalary, HourlyRate, " +
                         "HoursWorked FROM Employee e FULL OUTER JOIN SalaryEmployee " +
                         "ON e.EmployeeID=SalaryEmployee.EmployeeID " +
                         "FULL OUTER JOIN HourlyEmployee " +
                         "ON e.EmployeeID=HourlyEmployee.EmployeeID " +
                         "ORDER BY e.EmployeeID";

                SqlDataReader rdr = executeQuery(sql, conn);
                while (rdr.Read())
                {
                    System.Diagnostics.Debug.WriteLine("Reader data employee ID " + (int)rdr["EmployeeID"]);
                    Address address = new Address((string)rdr["Street"], (string)rdr["City"], (string)rdr["Province"], (string)rdr["PostalCode"]);
                  
                    if(rdr["MonthlySalary"] != System.DBNull.Value)
                    //Making an assumption that an Employee with a salary greater than 0 must be a SalaryEmployee 
                    //if (!DBNull.Value.Equals(rdr["HourlyRateMonthlySalary"]))
                    {
                        SalaryEmployee S = new SalaryEmployee(

                        (decimal)rdr["MonthlySalary"],
                        (int)rdr["EmployeeID"],
                        (string)rdr["JobDescription"],
                        (string)rdr["LastName"],
                        (string)rdr["FirstName"],
                        ((string)rdr["MiddleInit"])[0],
                        (string)rdr["PhoneNumber"],
                        address);

                        Employees.Add(S);
                    }
                    else if (rdr["HourlyRate"] != System.DBNull.Value)
                    {
                        //Making an assumption that an Employee with a Salary of 0 or less (it should never be less, but still checking just in case) must be an HourlyEmployee
                        HourlyEmployee H = new HourlyEmployee(

                        (decimal)rdr["HourlyRate"],
                        (decimal)rdr["HoursWorked"],
                        (int)rdr["EmployeeID"],
                        (string)rdr["JobDescription"],
                        (string)rdr["LastName"],
                        (string)rdr["FirstName"],
                        ((string)rdr["MiddleInit"])[0],
                        (string)rdr["PhoneNumber"],
                        address);

                        Employees.Add(H);
                    }

                }

                conn.Close();
            }

            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.StackTrace.ToString());
            }

            return Employees;
        }

    }
}