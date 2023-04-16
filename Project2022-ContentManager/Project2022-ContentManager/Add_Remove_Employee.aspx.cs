using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security;
using System.Web.Services.Description;
using Microsoft.Extensions.Logging;
using System.Collections;

namespace Project2022_ContentManager
{
    public partial class Add_Remove_Employee : System.Web.UI.Page
    {
        /* private readonly ILogger<Add_Remove_Employee> _logger;
         public Add_Remove_Employee(ILogger<Add_Remove_Employee> logger)
         {
             _logger = logger;
         }*/

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) { 
                System.Diagnostics.Debug.WriteLine("Add_Remove_Employee.aspx page loading");
           }
        }


        protected void TextBoxEmployeeNumber_TextChanged(object sender, EventArgs e)
        {

        }

        protected void TextBoxFirstName_TextChanged(object sender, EventArgs e)
        {

        }

        protected void TextBoxStreet_TextChanged(object sender, EventArgs e)
        {

        }

        protected void TextBoxCity_TextChanged(object sender, EventArgs e)
        {

        }

        protected void TextBoxProvince_TextChanged(object sender, EventArgs e)
        {

        }

        protected void TextBoxJobDescription_TextChanged(object sender, EventArgs e)
        {

        }

        protected void TextBoxPostalCode_TextChanged(object sender, EventArgs e)
        {

        }

        protected void AddEmployee_Click(object sender, EventArgs e)
        {
            EmployeeDAO empDB = new EmployeeDAO();

            int en = Int32.Parse(TextBoxEmployeeNumber.Text);
            string jd = TextBoxJobDescription.Text;
            string ln = TextBoxLastName.Text;
            string fn = TextBoxFirstName.Text;
            char mi = (TextBoxMiddleInit.Text)[0];
            string pn = TextBoxPhoneNumber.Text;

            string st = TextBoxStreet.Text;
            string ct = TextBoxCity.Text;
            string pv = TextBoxProvince.Text;
            string pc = TextBoxPostalCode.Text;
            decimal rate = 10;
            decimal hours = 15;

            Address a = new Address(st, ct, pv, pc);
            Person p = new Person(ln, fn, mi, pn, a);
            // Employee employee = new Employee(en, jd, p.LastName, p.FirstName, p.MiddleInit, p.PhoneNumber, p.Address);
            HourlyEmployee emp = new HourlyEmployee(rate, hours, en, jd, p.LastName, p.FirstName, p.MiddleInit, p.PhoneNumber, p.Address);
          // _logger.LogInformation(emp.HourlyRate + " " + emp.HoursWorked + " " + emp.EmployeeNumber + " " + emp.JobDescription + " " + emp.LastName + " " + emp.FirstName + " " + emp.MiddleInit + " " + emp.PhoneNumber + " " + a.Street + " " + a.City + " " + a.Province + " " + a.PostalCode);
            empDB.AddEmployee(emp);
            ClientScript.RegisterClientScriptBlock(this.GetType(), "AddSuccessful", "swal(\"Add Successful!\", \"\",\"success\")", true);
            //EmployeeManager M = new EmployeeManager();
            //SwitchToThisWindow(M, empDB);

        }
    }
}