/**
 * Group Members: Mary Adenuga, Adarsh K Ashok, Renoir Brown, Meetee Dave and Anandhu K. B 
 * Subject: COMP 600 
 * Assignment: Final Project (Employee Contact Manager)
 */

/**
 * This program is designed to act as an address book/contact manager, allowing 
 * for the editing, storing and retrieval of contact data. This class is an abstract 
 * extension of the Person class that describes the attributes of an individual Employee.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project2022_ContentManager
{
    public abstract class Employee:Person
    {
        public int EmployeeNumber { get; set; }
        public string JobDescription { get; set; }

        public Employee(int employeeNumber, string jobDescription, string lastName, string firstName, char middleInit, string phoneNumber, Address address) 
            : base (lastName, firstName, middleInit, phoneNumber, address)
        {
            EmployeeNumber = employeeNumber;
            JobDescription = jobDescription;
        }
        
        //Figure out if we need other constructors later...
        public String toString()
        {
            return "Employee: " + EmployeeNumber + " - " + getFullName();
        }

        public abstract decimal getEarnings();

    }
}