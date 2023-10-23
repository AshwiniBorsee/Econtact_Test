using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;

namespace Econtact.econtactClasses
{
    
    public class contactClass
    {
        private readonly IDatabase _database;

        public contactClass(IDatabase database)
        {
            _database = database;
        }

        //Getter Setter Properties 
        //Acts as Data Carrier in Our Application
        public int ContactID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int ContactNo { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }

        public DataTable Select()
        {
            DataTable dt = new DataTable();
            try
            {
                //Writing SQL Query
                string sql = "SELECT * FROM tbl_contact";

                dt = _database.ExecuteQuery(sql, null);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
            return dt;
        }

        //Inserting DAta into Database
        public bool Insert_Database(contactClass c)
        {
            //Creating a default return type and setting its value to false
            bool isSuccess = false;
            try
            {
                // Create a SQL Query to insert DAta
                string sql = "INSERT INTO tbl_contact (FirstName, LastName, ContactNo, Address, Gender) VALUES (@FirstName, @LastName, @ContactNo, @Address, @Gender)";
                //Create Parameters to add data
                SqlParameter[] parameters =
                {
                    new SqlParameter("@FirstName", c.FirstName),
                    new SqlParameter("@LastName", c.LastName),
                    new SqlParameter("@ContactNo", c.ContactNo),
                    new SqlParameter("@Address", c.Address),
                    new SqlParameter("@Gender", c.Gender)
                };

                int rows = _database.ExecuteNonQuery(sql, parameters);
                ////If the query runs successfully then the value of rows will be greater than zero else its value will be 0
                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
      
            return isSuccess;
        }

        //Method to update data in database from our application
        public bool Update_Database(contactClass c)
        {
            //Create a default return type and set its default value to false
            bool isSuccess = false;
            //SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                //SQL to update data in our Database
                string sql = "UPDATE tbl_contact SET FirstName=@FirstName, LastName=@Lastname, ContactNo=@ContactNo, Address=@Address, Gender=@Gender WHERE ContactID=@ContactID";
                //Create Parameters to add data
                SqlParameter[] parameters =
                {
                    new SqlParameter("@FirstName", c.FirstName),
                    new SqlParameter("@LastName", c.LastName),
                    new SqlParameter("@ContactNo", c.ContactNo),
                    new SqlParameter("@Address", c.Address),
                    new SqlParameter("@Gender", c.Gender),
                    new SqlParameter("@Gender", c.ContactID)
                };

                int rows = _database.ExecuteNonQuery(sql, parameters);
                //if the query runs sucessfully then the value of rows will be greater than zero else its value will be zero
                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                
            }
            return isSuccess;
        }
        //Method to Delete_from_database Data from DAtabase
        public bool Delete_from_database(contactClass c)
        {
            //Create a default return value and set its value to false
            bool isSuccess = false;
            try
            {
                //SQL To Delte DAta
                string sql = "DELETE FROM tbl_contact WHERE ContactID=@ContactID";


                SqlParameter[] parameters =
               {
                    new SqlParameter("@Gender", c.ContactID)
                };

                int rows = _database.ExecuteNonQuery(sql, parameters);
                isSuccess = true;
                //If the query run sucessfully then the value of rows is greater than zero else its value is 0
                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message); 
            }
            return isSuccess;
        }
        
    }
}
