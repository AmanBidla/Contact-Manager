/**
 * Group Members: Mary Adenuga, Adarsh K Ashok, Renoir Brown, Meetee Dave and Anandhu K. B 
 * Subject: COMP 600 
 * Assignment: Final Project (Employee Contact Manager)
 */

/**
 * This program is designed to act as an address book/contact manager, allowing 
 * for the editing, storing and retrieval of contact data. This class is an extension 
 * of the abstract Employee class that describes the attributes of an individual 
 * hourly Employee.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project2022_ContentManager
{
    public class HourlyEmployee : Employee
    {
        public decimal HourlyRate;
        public decimal HoursWorked;

        public HourlyEmployee(decimal hourlyRate, decimal hoursWorked, int employeeNumber, string jobDescription, string lastName, string firstName, char middleInit, string phoneNumber, Address address) 
            : base (employeeNumber, jobDescription, lastName, firstName, middleInit, phoneNumber, address)
        {
            HourlyRate = hourlyRate;
            HoursWorked = hoursWorked;
        }

        public decimal GetHoursWorked()
        {
            return HoursWorked;
        }

        public decimal GetHourlyRate()
        {
            return HourlyRate;
        }

        public void SetHourlyRate(decimal hourlyRate)
        {
            if (hourlyRate < 0)
            {
                hourlyRate = 0;
            }

            HourlyRate= hourlyRate;
        }



        public override decimal getEarnings()
        {
            decimal otWorked = 0;
            decimal otEarnings, stdEarnings;

            //Bi-Weekly Version...
            if (HoursWorked > 80)
            {
                otWorked = HoursWorked - 80;
                HoursWorked = 80;
            }


            otEarnings = otWorked * (HourlyRate * 1.5M);
            stdEarnings = HoursWorked * HourlyRate;

            return stdEarnings + otEarnings;
        }
    }
}