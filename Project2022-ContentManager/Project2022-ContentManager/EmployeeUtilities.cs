/**
 * Group Members: Mary Adenuga, Adarsh K Ashok, Renoir Brown, Meetee Dave and Anandhu K. B 
 * Subject: COMP 600 
 * Assignment: Final Project (Employee Contact Manager)
 */

/**
 * This program is designed to act as an address book/contact manager, allowing 
 * for the editing, storing and retrieval of contact data. This class creates and
 * populates an array of Employee objects and contains methods for navigating that
 * array (mainly for use by the GUI).
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Microsoft.Extensions.Logging;

namespace Project2022_ContentManager
{
    public class EmployeeUtilities
    {
        private readonly ILogger<EmployeeUtilities> _logger;
        public EmployeeUtilities(ILogger<EmployeeUtilities> logger)
        {
            _logger = logger;
        }
        public static int Current = 0;//works from here for some reason...

        public ArrayList EmployeeList = new ArrayList();
        EmployeeDAO empDB = new EmployeeDAO();


        public EmployeeUtilities()
        {
            GetRealData();//connects to the db...
        }

        public void GetRealData()
        {
            EmployeeList = empDB.GetEmployee();
        }

        public Employee GetCurrent()
        {
            return (Employee)EmployeeList[Current];
        }

        public Employee GetNext()
        {
             if (Current + 1 >= EmployeeList.Count)
             {
                 Current = 0;
             }

             else
             {
                 Current++;
             }

            return (Employee)EmployeeList[Current];
        }

       public Employee GetPrevious()
        {
            if (Current - 1 < 0)
            {
                Current = EmployeeList.Count - 1;
            }

            else
            {
                Current--;
            }

            return (Employee)EmployeeList[Current];
        }

       public Employee GetEmployeeById(int id)
        {
            EmployeeList = empDB.GetEmployee();
            Employee e = null;
            System.Diagnostics.Debug.WriteLine("Get Employee By ID " + id);
            for (int i = 0; i < EmployeeList.Count; i++)
            {
                System.Diagnostics.Debug.WriteLine("full data set " + ((Employee)EmployeeList[i]).EmployeeNumber);
                if (((Employee)EmployeeList[i]).EmployeeNumber == id)
                {
                    e = (Employee)EmployeeList[i]; 
                    System.Diagnostics.Debug.WriteLine("data set " + e.EmployeeNumber);
                   // _logger.LogInformation(e.EmployeeNumber,"data set", e.EmployeeNumber);

                    /**
                     * Update current here so that the next and previous buttons will 
                     * work correctly if using them to move from the ID value searched for. 
                     * Without this the next and previous buttons will "remember" where 
                     * they were before the search and act accordingly.
                     */
                    Current = i;
                    break;//found it, call off the search...

                }
            }
            return e;
        }

        public void UpdateEmployee(Employee e)
        {
            EmployeeDAO empDB = new EmployeeDAO();
            empDB.UpdateEmployee(e);
        }
    }
}