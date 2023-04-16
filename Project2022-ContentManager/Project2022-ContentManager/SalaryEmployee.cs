/**
 * Group Members: Mary Adenuga, Adarsh K Ashok, Renoir Brown, Meetee Dave and Anandhu K. B 
 * Subject: COMP 600 
 * Assignment: Final Project (Contact Manager)
 */

/**
 * This program is designed to act as an address book/contact manager, allowing 
 * for the editing, storing and retrieval of contact data. This class is an extension 
 * of the abstract Employee class that describes the attributes of an individual 
 * salaried Employee.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project2022_ContentManager
{
    public class SalaryEmployee : Employee
    {
        private decimal MonthlySalary;

        public SalaryEmployee(decimal monthlySalary, int employeeNumber, string jobDescription, string lastName, string firstName, char middleInit, string phoneNumber, Address address) : base(employeeNumber, jobDescription, lastName, firstName, middleInit, phoneNumber, address)
        {
            SetMonthlySalary(monthlySalary);//use setter to avoid -ve value errors...
        }

        public decimal GetMonthlySalary()
        {
            return MonthlySalary;
        }
        
        public void SetMonthlySalary(decimal monthlySalary)
        {
            if (monthlySalary < 0)
            {//salary cant be less than 0
                monthlySalary = 0;
            }
            
            MonthlySalary = monthlySalary;
    
        }
        public override decimal getEarnings()
        {

            decimal earnings = (MonthlySalary * 12) / 26;//x 12 to account for all months in a year, divide by 26 to account for 52 weeks / 2 

            return earnings;
        }
    }
}