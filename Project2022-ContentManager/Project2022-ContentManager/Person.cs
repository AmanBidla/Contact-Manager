/**
 * Group Members: Mary Adenuga, Adarsh K Ashok, Renoir Brown, Meetee Dave and Anandhu K. B 
 * Subject: COMP 600 
 * Assignment: Final Project (Contact Manager)
 */

/**
 * This program is designed to act as an address book/contact manager, allowing 
 * for the editing, storing and retrieval of contact data. This is the class that 
 * describes the attributes of an individual contact.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project2022_ContentManager
{
    public class Person
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public char MiddleInit { get; set; }
        public string PhoneNumber { get; set; }
        public Address Address { get; set; }

        public Person(string lastName, string firstName, char middleInit, string phoneNumber, Address address)
        {
            LastName = lastName;
            FirstName = firstName;
            MiddleInit = middleInit;
            PhoneNumber = phoneNumber;
            Address = address;
        }

        public String getFullName()
        {
            return LastName + ", " + FirstName + " " + MiddleInit + ".";
        }
    }
}