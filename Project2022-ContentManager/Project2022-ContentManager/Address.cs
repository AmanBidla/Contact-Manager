/**
 * Group Members: Mary Adenuga, Adarsh K Ashok, Renoir Brown, Meetee Dave and Anandhu K. B 
 * Subject: COMP 600 
 * Assignment: Final Project (Employee Contact Manager)
 */

/**
 * This program is designed to act as an address book/contact manager, allowing 
 * for the editing, storing and retrieval of contact data. This is the class that 
 * describes the attributes of an address.
 */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project2022_ContentManager
{
    public class Address
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string PostalCode { get; set; }

        public Address() { }  

        public Address(string street, string city, string province, string postalCode)
        {
            Street = street;
            City = city;
            Province = province;
            PostalCode = postalCode;
        }

        public String GetFullAddress()
        {
            return Street + ", " + City + ", " + Province + ", " + PostalCode;
        }
    }
}