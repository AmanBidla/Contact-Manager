/**
 * Group Members: Mary Adenuga, Adarsh K Ashok, Renoir Brown, Meetee Dave and Anandhu K. B 
 * Subject: COMP 600 
 * Assignment: Final Project (Contact Manager)
 */

/**
 * This program is designed to act as an address book/contact manager, allowing 
 * for the editing, storing and retrieval of contact data. This class represents
 * The Graphical User Interface (GUI) for the Contact Manager.
 * Main file from which the program is run. Ties together everything...
 */

using System;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security;
using System.Web.Services.Description;
using System.Web.UI.WebControls;


namespace Project2022_ContentManager
{
    public partial class EmployeeManager : System.Web.UI.Page
    {
        public EmployeeUtilities emputil = new EmployeeUtilities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)//ensures that the data does not get refreshed on the web page before posting changes to the database
            {
                Employee emp = emputil.GetCurrent();
                DisplayEmployee(emp);
            }
        }

        public void DisplayEmployee(Employee emp)
        {
            TextBoxEmployeeNumber.Text = emp.EmployeeNumber.ToString();
            TextBoxFirstName.Text = emp.FirstName.ToString();
            TextBoxLastName.Text = emp.LastName.ToString();
            TextBoxMiddleInit.Text = emp.MiddleInit.ToString();
            TextBoxPhoneNumber.Text = emp.PhoneNumber.ToString();
            TextBoxStreet.Text = emp.Address.Street.ToString();
            TextBoxCity.Text = emp.Address.City.ToString();
            TextBoxProvince.Text = emp.Address.Province.ToString();
            TextBoxPostalCode.Text = emp.Address.PostalCode.ToString();
            TextBoxJobDescription.Text = emp.JobDescription.ToString();
            TextBoxEarnings.Text = "$" + Math.Round(emp.getEarnings(), 2).ToString();

            if (emp is HourlyEmployee)
            {
                ButtonGetHoursOrEarnings.Text = "Get Hours";
            }

            else if (emp is SalaryEmployee)
            {
                ButtonGetHoursOrEarnings.Text = "Get Salary";
            }
        }

        protected void ButtonSelectNextEmployee_Click(object sender, EventArgs e)
        {
            Employee emp = emputil.GetNext();
            
            DisplayEmployee(emp);
        }

        protected void ButtonSelectPreviousEmployee_Click(object sender, EventArgs e)
        {
            Employee emp = emputil.GetPrevious();
            DisplayEmployee(emp);
        }

        protected void ButtonSearchEmployee_Click(object sender, EventArgs e)
        {
            try
            {
                int id;
                id = Int32.Parse(TextBoxTargetEmployeeNumber.Text);//covnvert textbox info to int...
                System.Diagnostics.Debug.WriteLine("input data " + id);
                TextBoxTargetEmployeeNumber.Text = string.Empty;//clears the box for neatness...

                Employee emp = emputil.GetEmployeeById(id);//use to collected id to find the employee...
                System.Diagnostics.Debug.WriteLine("output data " + emp.EmployeeNumber);

                DisplayEmployee(emp);
            }



            catch (NullReferenceException nre)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Exception", "swal(\"No such Employee!\", \"\",\"error\")", true);//swal is a reference to a JS plugin for nicer popups...
                System.Diagnostics.Debug.WriteLine(nre.StackTrace.ToString());
            }

            catch (FormatException fex)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Exception", "swal(\"Non-integer value entered!\", \"\",\"error\")", true);
                System.Diagnostics.Debug.WriteLine(fex.StackTrace.ToString());
            }

            catch (Exception gex)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Exception", "swal(\"An unknown error occured!\", \"\",\"error\")", true);
                System.Diagnostics.Debug.WriteLine(gex.StackTrace.ToString());
            }
        }

        protected void ButtonSaveEmployee_Click(object sender, EventArgs e)
        {
            UpdateEmployee();
        }

        protected void TextBoxPostalCode_TextChanged(object sender, EventArgs e)
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

        protected void TextBoxTargetEmployeeNumber_TextChanged(object sender, EventArgs e)
        {
            
        }

        protected void ButtonGetHoursOrEarnings_Click(object sender, EventArgs e)
        {
            Employee emp = emputil.GetCurrent();

            if (emp is HourlyEmployee)
            {
                decimal hours = Math.Round(((HourlyEmployee)emp).GetHoursWorked(), 1);

                string message = emp.FirstName + "'s Hours Worked";//use this to personalise the message...finicky otherwise...
                
                ClientScript.RegisterClientScriptBlock(this.GetType(), "HoursWorked", "swal(\"" + message + "\",\"" + hours + "\",\"info\")", true);
            }


            else if (emp is SalaryEmployee) 
            {
                string message = emp.FirstName + "'s Monthly Salary";

                decimal salary = Math.Round(((SalaryEmployee)emp).GetMonthlySalary(), 2);
                
                ClientScript.RegisterClientScriptBlock(this.GetType(), "MonthlySalary", "swal(\"" + message + "\",\"$" + salary + "\",\"info\")", true);
            }
        }

        protected void TextBoxEarnings_TextChanged(object sender, EventArgs e)
        {

        }

        public void UpdateEmployee()
        {
            Address a = new Address(TextBoxStreet.Text, TextBoxCity.Text, TextBoxProvince.Text, TextBoxPostalCode.Text);
            int empID = Int32.Parse(TextBoxEmployeeNumber.Text);


            Employee current = emputil.GetCurrent();

            //In the event there is no middle initial put a white space...
            //*Known Issue* - have to remember to backspace the empty space if putting in a new initial later though...
            if (TextBoxMiddleInit.Text == "")
            {
                TextBoxMiddleInit.Text = " ";
            }

            Employee e = null;//Single point of entry...has to be null or else it will flake out...

            try
            {                
                if (current is SalaryEmployee) {
                    decimal monthlySalary = ((SalaryEmployee)current).GetMonthlySalary();

                    e = new SalaryEmployee(monthlySalary, empID, TextBoxJobDescription.Text, TextBoxLastName.Text, TextBoxFirstName.Text, TextBoxMiddleInit.Text.ElementAt(0), TextBoxPhoneNumber.Text, a);
                }
                
                else if (current is HourlyEmployee) {
                    decimal hourlyRate = ((HourlyEmployee)current).GetHourlyRate();
                    decimal hoursWorked = ((HourlyEmployee)current).GetHoursWorked();

                    e = new HourlyEmployee(hourlyRate, hoursWorked, Int32.Parse(TextBoxEmployeeNumber.Text), TextBoxJobDescription.Text, TextBoxLastName.Text, TextBoxFirstName.Text, TextBoxMiddleInit.Text.ElementAt(0), TextBoxPhoneNumber.Text, a);
                }
            }

            catch (Exception)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Exception", "swal(\"An unknown error occured!\", \"\",\"error\")", true);
            }

            emputil.UpdateEmployee(e);//Single point of exit...

            ClientScript.RegisterClientScriptBlock(this.GetType(), "SaveSuccessful", "swal(\"Save Successful!\", \"\",\"success\")", true);
        }        
    }
}