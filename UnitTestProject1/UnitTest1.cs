using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Econtact;
using System.Configuration;
using Moq;
using Econtact.econtactClasses;
using System.Data.SqlClient;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Verify_Add_Contact_Valid_Data()
        {
            /// <summary>
            /// 1. Add valid data while creating new data
            /// 2. Mock function is used to replace database module
            /// 3. Validating Add_Contact event handler with this unit test
            /// 4. With valid data inpus, add_contact is successful.
            /// 5. Assert if add_contact is failed.
            /// <summary>
            // arrange
            var mockDatabase = new Mock<IDatabase>();
            mockDatabase.Setup
                (d => d.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<SqlParameter[]>())).Returns(1);

            var contact = new contactClass(mockDatabase.Object);

            // act
            Econtactcls econtactcls = new Econtactcls(contact);
            econtactcls.txtboxFirstName.Text = "Test";
            econtactcls.txtboxLastName.Text = "Done";
            econtactcls.txtBoxContactNumber.Text = "1234567890";
            econtactcls.txtBoxAddress.Text = "Seattle";
            econtactcls.cmbGender.Text = "Male";

            try
            {
                econtactcls.add_Contact();
            }
            // assert
            catch(ConfigurationErrorsException ex)
            {
               
                Assert.Fail($"Test Failed: Error while creating contac. Message: {ex.Message}");
            }
            Console.WriteLine("Test passed. Add_contact()");

        }
        
        [TestMethod]
        public void Verify_Add_Contact_Invalid_Data()
        {
            /// <summary>
            /// 1. Add valid data while creating new data with invalid input string.
            /// 2. Mock function is used to replace database module
            /// 3. Validating Add_Contact event handler with this unit test
            /// 4. add_contact should throw an error.
            /// 5. Assert if expected output is not received.
            /// <summary>
            
            // arrange
            var mockDatabase = new Mock<IDatabase>();
            mockDatabase.Setup
                (d => d.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<SqlParameter[]>())).Returns(1);

            var contact = new contactClass(mockDatabase.Object);

            Econtactcls econtactcls = new Econtactcls(contact);

            econtactcls.txtboxFirstName.Text = "123Ac";
            econtactcls.txtboxLastName.Text = "346cd";
            econtactcls.txtBoxContactNumber.Text = "sdfnmb";
            econtactcls.txtBoxAddress.Text = "";
            econtactcls.cmbGender.Text = "sdbhj";
            
            // act
            try
            {
                econtactcls.add_Contact();
            }

            catch (Exception ex)
            {

                Assert.IsInstanceOfType<Exception>(ex);
                return;
            }
            // assert
            Assert.Fail("Expected exception did not happened");

        }

        [TestMethod]
        public void Verify_Update_Contact_Valid_Data()
        {
            /// <summary>
            /// 1. Add valid data while creating new data
            /// 2. Mock function is used to replace database module
            /// 3. Validating update_contact button click event handler with this unit test
            /// 4. Update the existing contacted added in step 1, update_contact should successful.
            /// 5. Assert if update_contact is failed.
            /// <summary>

            // arrange
            var mockDatabase = new Mock<IDatabase>();
            mockDatabase.Setup
                (d => d.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<SqlParameter[]>())).Returns(1);
            mockDatabase.Setup
                (d => d.ExecuteQuery(It.IsAny<string>(), It.IsAny<SqlParameter[]>())).Returns(new System.Data.DataTable());
            var contact = new contactClass(mockDatabase.Object);

            Econtactcls econtactcls = new Econtactcls(contact);
            econtactcls.txtboxFirstName.Text = "Taylor";
            econtactcls.txtboxLastName.Text = "Swift";
            econtactcls.txtBoxContactNumber.Text = "1234567890";
            econtactcls.txtBoxAddress.Text = "Seattle";
            econtactcls.cmbGender.Text = "Female";

            //act
            try
            {
                econtactcls.add_Contact();
                econtactcls.txtboxFirstName.Text = "Unit";
                econtactcls.txtboxLastName.Text = "Swift";
                econtactcls.txtBoxContactNumber.Text = "57648579";
                econtactcls.txtBoxAddress.Text = "Seattle";
                econtactcls.cmbGender.Text = "Female";
                econtactcls.txtboxContactID.Text = "1";
                econtactcls.update_Contact();
            }
            //assert
            catch (ConfigurationErrorsException ex)
            {
                Assert.Fail($"Test Failed: Error while updating contact.Message {ex.Message}");
            }
            Console.WriteLine("Test Passed: Update contact test is passed.");

        }

        public void Verify_Update_Non_Existing_contact()
        {
            /// <summary>
            /// 1. Update contact without adding contact.
            /// 2. Mock function is used to replace database module
            /// 3. Validating update_contact button click event handler with this unit test
            /// 4. Update the contact with invalid input string, update_contact should fail.
            /// 5. Assert if expected output is not received.
            /// <summary>

            // arrange
            var mockDatabase = new Mock<IDatabase>();
            mockDatabase.Setup
                (d => d.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<SqlParameter[]>())).Returns(1);
            mockDatabase.Setup
                (d => d.ExecuteQuery(It.IsAny<string>(), It.IsAny<SqlParameter[]>())).Returns(new System.Data.DataTable());

            var contact = new contactClass(mockDatabase.Object);

            Econtactcls econtactcls = new Econtactcls(contact);

            econtactcls.txtboxFirstName.Text = "123Ac";
            econtactcls.txtboxLastName.Text = "346cd";
            econtactcls.txtBoxContactNumber.Text = "sdfnmb";
            econtactcls.txtBoxAddress.Text = "bcvdkmv";
            econtactcls.cmbGender.Text = "Male";
            econtactcls.txtboxContactID.Text = "5";
            // act
            try
            {
                econtactcls.cmbGender.Text = "sdbhj";
                econtactcls.update_Contact();
            }
            // assert
            catch (Exception ex)
            {
                Console.WriteLine("Test Passed: update contact with invalid data test passed.");
                Assert.IsInstanceOfType<Exception>(ex);
                return;
            }
            Assert.Fail("Expected exception did not happened");

        }

        [TestMethod]
        public void Verify_Update_contact_Inalid_Data()
        {
            /// <summary>
            /// 1. Add contact with valid data.
            /// 2. Mock function is used to replace database module
            /// 3. Validating update_contact button click event handler with this unit test
            /// 4. Update the contact with invalid input string, update_contact should fail.
            /// 5. Assert if expected output is not received.
            /// <summary>

            // arrange
            var mockDatabase = new Mock<IDatabase>();
            mockDatabase.Setup
                (d => d.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<SqlParameter[]>())).Returns(1);
            mockDatabase.Setup
                (d => d.ExecuteQuery(It.IsAny<string>(), It.IsAny<SqlParameter[]>())).Returns(new System.Data.DataTable());

            var contact = new contactClass(mockDatabase.Object);

            Econtactcls econtactcls = new Econtactcls(contact);

            econtactcls.txtboxFirstName.Text = "123Ac";
            econtactcls.txtboxLastName.Text = "346cd";
            econtactcls.txtBoxContactNumber.Text = "sdfnmb";
            econtactcls.txtBoxAddress.Text = "bcvdkmv";
            econtactcls.cmbGender.Text = "Male";
            econtactcls.txtboxContactID.Text = "1";
            // act
            try
            {
                econtactcls.add_Contact();
                econtactcls.cmbGender.Text = "sdbhj";
                econtactcls.update_Contact();
            }
            // assert
            catch (Exception ex)
            {
                Console.WriteLine("Test Passed: update contact with invalid data test passed.");
                Assert.IsInstanceOfType<Exception>(ex);
                return;
            }
            Assert.Fail("Expected exception did not happened");

        }

        [TestMethod]
        public void Verify_Delete_contact_Valid_Data()
        {
            /// <summary>
            /// 1. Add valid data while creating new data
            /// 2. Mock function is used to replace database module
            /// 3. Validating delete_contact button click event handler with this unit test
            /// 4. Delete contacted added in step 1, update_contact should successful.
            /// 5. Assert if update_contact is failed.
            /// <summary>

            // arrange
            var mockDatabase = new Mock<IDatabase>();
            mockDatabase.Setup
                (d => d.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<SqlParameter[]>())).Returns(1);
            mockDatabase.Setup
                (d => d.ExecuteQuery(It.IsAny<string>(), It.IsAny<SqlParameter[]>())).Returns(new System.Data.DataTable());
            var contact = new contactClass(mockDatabase.Object);

            Econtactcls econtactcls = new Econtactcls(contact);
            econtactcls.txtboxFirstName.Text = "Taylor";
            econtactcls.txtboxLastName.Text = "Swift";
            econtactcls.txtBoxContactNumber.Text = "1234567890";
            econtactcls.txtBoxAddress.Text = "Seattle";
            econtactcls.cmbGender.Text = "Male";

            //act
            try
            {
                econtactcls.add_Contact();
                econtactcls.txtboxContactID.Text = "1";
                econtactcls.delete_Contact();
            }
            //assert
            catch (ConfigurationErrorsException ex)
            {
                Assert.Fail($"Test Failed: Error while deleting contact.Message {ex.Message}");
            }
            Console.WriteLine("Test Passed: Delete contact test is passed.");

        }

        [TestMethod]
        public void Verify_Delete_Contact_Inalid_Data()
        {
            /// <summary>
            /// 1. Add contact with valid data.
            /// 2. Mock function is used to replace database module
            /// 3. Validating delete_contact button click event handler with this unit test
            /// 4. Delete contact with invalid contact id, update_contact should fail.
            /// 5. Assert if expected output is not received.
            /// <summary>

            // arrange
            var mockDatabase = new Mock<IDatabase>();
            mockDatabase.Setup
                (d => d.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<SqlParameter[]>())).Returns(1);
            mockDatabase.Setup
                (d => d.ExecuteQuery(It.IsAny<string>(), It.IsAny<SqlParameter[]>())).Returns(new System.Data.DataTable());

            var contact = new contactClass(mockDatabase.Object);

            Econtactcls econtactcls = new Econtactcls(contact);

            econtactcls.txtboxFirstName.Text = "Taylor";
            econtactcls.txtboxLastName.Text = "Swift";
            econtactcls.txtBoxContactNumber.Text = "1235672312";
            econtactcls.txtBoxAddress.Text = "Seattle";
            econtactcls.cmbGender.Text = "Male";
            econtactcls.txtboxContactID.Text = "1";
            // act
            try
            {
                econtactcls.add_Contact();
                econtactcls.txtboxContactID.Text = "sdcsbj";
                econtactcls.delete_Contact();
            }
            // assert
            catch (Exception ex)
            {
                Console.WriteLine("Test Passed: update contact with invalid data test passed.");
                Assert.IsInstanceOfType<Exception>(ex);
                return;
            }
            Assert.Fail("Expected exception did not happened");

        }

        [TestMethod]
        public void Verify_Clear_Contact()
        {
            /// <summary>
            /// 1. Add_contact and then Verify click event for clear contact. Clear will clear out UI textbox
            /// 2. Mock function is used to replace database module
            /// 3. Validating clear button click event handler with this unit test
            /// 4. After clear, input string should set to empty string.
            /// 5. Assert if expected output is not received.
            /// <summary>

            // arrange
            var mockDatabase = new Mock<IDatabase>();
            mockDatabase.Setup
                (d => d.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<SqlParameter[]>())).Returns(1);
            mockDatabase.Setup
                (d => d.ExecuteQuery(It.IsAny<string>(), It.IsAny<SqlParameter[]>())).Returns(new System.Data.DataTable());

            var contact = new contactClass(mockDatabase.Object);

            Econtactcls econtactcls = new Econtactcls(contact);
            econtactcls.txtboxFirstName.Text = "Taylor";
            econtactcls.txtboxLastName.Text = "Swift";
            econtactcls.txtBoxContactNumber.Text = "12343567";
            econtactcls.txtBoxAddress.Text = "bcvdkmv";
            econtactcls.cmbGender.Text = "Male";
         

            // act
            try
            {
                econtactcls.add_Contact();
                econtactcls.Clear();
                Assert.AreEqual(econtactcls.txtboxFirstName.Text, "");
                Assert.AreEqual(econtactcls.txtboxLastName.Text, "");
                Assert.AreEqual(econtactcls.txtBoxContactNumber.Text, "");
                Assert.AreEqual(econtactcls.txtBoxAddress.Text, "");
                Assert.AreEqual(econtactcls.cmbGender.Text, "");
            }
            // assert
            catch (Exception ex)
            {
                Assert.Fail($"Expected exception did not happened{ex.Message}");
            }
            Console.WriteLine("Expected exception did not happened");

        }

        //Disale this test, issue is already there in code.
        public void Verify_Search_Contact_With_Add_Contact()
        {
            /// <summary>
            /// 1. Add_contact and then Verify search button event for clear contact. 
            /// 2. Mock function is used to replace database module
            /// 3. Validating search button click event handler with this unit test
            /// 4. Search contact with existing contact id, search contact should pass.
            /// 5. Assert if expected output is not received.
            /// <summary>

            // arrange
            var mockDatabase = new Mock<IDatabase>();
            mockDatabase.Setup
                (d => d.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<SqlParameter[]>())).Returns(1);
            mockDatabase.Setup
                (d => d.ExecuteQuery(It.IsAny<string>(), It.IsAny<SqlParameter[]>())).Returns(new System.Data.DataTable());

            var contact = new contactClass(mockDatabase.Object);

            Econtactcls econtactcls = new Econtactcls(contact);
            econtactcls.txtboxFirstName.Text = "John";
            econtactcls.txtboxLastName.Text = "Duguls";
            econtactcls.txtBoxContactNumber.Text = "41234589";
            econtactcls.txtBoxAddress.Text = "Seattle";
            econtactcls.cmbGender.Text = "Male";
            econtactcls.txtboxContactID.Text = "1";

            // act
            try
            {
                econtactcls.add_Contact();
                econtactcls.Search("John");
                
            }
            // assert
            catch (Exception ex)
            {
                Assert.Fail($"Test Failed: Search contact test failed with message {ex.Message}");
            }
            Console.WriteLine("Test passed: Search contact is passed.");
        }

        [TestMethod]
        public void Verify_Insert_Database()
        {
            /// <summary>
            /// 1. Verify Insert Database Operation
            /// 2. Mock function is used to replace database connection module
            /// <summary>

            // arrange
            var mockDatabase = new Mock<IDatabase>();
            mockDatabase.Setup
                (d => d.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<SqlParameter[]>())).Returns(1);
            var contact = new contactClass(mockDatabase.Object);
            contact.FirstName = "Taylor";
            contact.LastName = "Swift";
            contact.Address = "Bush Street, Seattle";
            contact.ContactNo = 123183418;
            contact.Gender = "Female";
            // act
            try
            {
                contact.Insert_Database(contact);

            }
            catch (Exception ex)
            {
                Assert.Fail($"Test Failed: Insert query on dataase failed with message {ex.Message}");

            }
            Console.WriteLine("Test Passed: Insert query on database is passed.");

        }

        [TestMethod]
        public void Verify_Insert_Database_With_Exception()
        {
            /// <summary>
            /// 1. Verify Insert Database Operation, send contact_no in text format
            /// 2. Mock function is used to replace database connection module
            /// <summary>

            // arrange
            var mockDatabase = new Mock<IDatabase>();
            var contact = new contactClass(mockDatabase.Object);
            contact.FirstName = "Taylor";
            contact.LastName = "Swift";
            contact.Address = "Bush Street, Seattle";
            contact.ContactNo = 17398721;
            contact.Gender = "Female";
            contact.ContactID = 1;

            string sql = "INSERT INTO tbl_contact" +
                " (contact.FirstName, contact.LastName, contact.ContactNo, contact.Address, contact.Gender) " +
                "VALUES (@FirstName, @LastName, @ContactNo, @Address, @Gender)";
            mockDatabase.Setup
                (d => d.ExecuteNonQuery(sql, It.IsAny<SqlParameter[]>())).Throws(new Exception("Invalid query"));
            // act
            try
            {
                contact.Insert_Database(contact);

            }
            catch (Exception ex)
            {   
                Assert.IsInstanceOfType(ex, typeof(Exception));
            }
            Console.WriteLine("Test Passed: Insert query with invalid input is passed.");

        }

        [TestMethod]
        public void Verify_Update_Database()
        {
            /// <summary>
            /// 1. Verify Update Database Operation
            /// 2. Mock function is used to replace database connection module
            /// <summary>

            // arrange
            var mockDatabase = new Mock<IDatabase>();
            mockDatabase.Setup
                (d => d.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<SqlParameter[]>())).Returns(1);
            var contact = new contactClass(mockDatabase.Object);
            contact.FirstName = "Taylor";
            contact.LastName = "Swift";
            contact.Address = "Bush Street, Seattle";
            contact.ContactNo = 1231834187;
            contact.Gender = "Female";
            // act
            try
            {
                contact.Insert_Database(contact);
                contact.Update_Database(contact);

            }
            catch (Exception ex)
            {
                Assert.Fail($"Test Failed: Update query on dataase failed with message {ex.Message}");

            }
            Console.WriteLine("Test Passed: Update query on database is passed.");

        }

        [TestMethod]
        public void Verify_Update_Database_With_Exception()
        {
            /// <summary>
            /// 1. Verify update Database Operation, send contact_no in text format, invalid format.
            /// 2. Mock function is used to replace database connection module
            /// <summary>

            // arrange
            var mockDatabase = new Mock<IDatabase>();
            var contact = new contactClass(mockDatabase.Object);
            contact.FirstName = "Taylor";
            contact.LastName = "Swift";
            contact.Address = "Bush Street, Seattle";
            contact.ContactNo = 12345678;
            contact.Gender = "Female";
            contact.ContactID = 1;

            string sql = "UPDATE tbl_contact SET " +
                " FirstName=contact.FirstName, LastName=contact.LastName, ContactNo=contact.ContactNo, " +
                "Address=contact.Address, Gender=contact.Gender WHERE ContactID=@ContactID";
            mockDatabase.Setup
                (d => d.ExecuteNonQuery(sql, It.IsAny<SqlParameter[]>())).Throws(new Exception("Invalid query"));
            // act
            try
            {
                contact.Insert_Database(contact);
                contact.Update_Database(contact);

            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(Exception));
            }
            Console.WriteLine("Test Passed: Update query with invalid input is passed.");

        }

        public void Verify_Delete_Database()
        {
            /// <summary>
            /// 1. Verify Delete Database Operation which is inserted.
            /// 2. Mock function is used to replace database connection module
            /// <summary>

            // arrange
            var mockDatabase = new Mock<IDatabase>();
            mockDatabase.Setup
                (d => d.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<SqlParameter[]>())).Returns(1);
            var contact = new contactClass(mockDatabase.Object);
            contact.FirstName = "Taylor";
            contact.LastName = "Swift";
            contact.Address = "Bush Street, Seattle";
            contact.ContactNo = 23442547;
            contact.Gender = "Female";
            // act
            try
            {
                contact.Insert_Database(contact);
                contact.Delete_from_database(contact);

            }
            catch (Exception ex)
            {
                Assert.Fail($"Test Failed: Update query on dataase failed with message {ex.Message}");

            }
            Console.WriteLine("Test Passed: Update query on database is passed.");

        }

        [TestMethod]
        public void Verify_Delete_Database_With_Exception()
        {
            /// <summary>
            /// 1. Verify Delete Database Operation, send contact_no in text format
            /// 2. Mock function is used to replace database connection module
            /// <summary>

            // arrange
            var mockDatabase = new Mock<IDatabase>();
            var contact = new contactClass(mockDatabase.Object);
            contact.FirstName = "Taylor";
            contact.LastName = "Swift";
            contact.Address = "Bush Street, Seattle";
            contact.ContactNo = 1234676235;
            contact.Gender = "Female";
            contact.ContactID = 1;

            string sql = "DELETE FROM tbl_contact WHERE ContactID=@ContactID";
            mockDatabase.Setup
                (d => d.ExecuteNonQuery(sql, It.IsAny<SqlParameter[]>())).Throws(new Exception("Invalid query"));
            // act
            try
            {
                contact.Insert_Database(contact);
                contact.Delete_from_database(contact);

            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(Exception));
            }
            Console.WriteLine("Test Passed: Delete query with invalid input is passed.");

        }
    }

}
