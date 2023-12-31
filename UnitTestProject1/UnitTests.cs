﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Econtact;
using System.Configuration;
using Moq;
using Econtact.econtactClasses;
using System.Data.SqlClient;

namespace TestProject1
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        [DataRow("Chris", "Tate", "83782758", "Seattle", "Male")]
        [DataRow("Sam", "Fog", "287498859", "Kent", "FeMale")]
        public void Verify_Add_Contact_Valid_Data(String FirstName, String LastName, String ContactNo, String Address, String Gender)
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
            var mockMessageBoxService = new Mock<IMessageBoxService>();
            mockDatabase.Setup
                (d => d.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<SqlParameter[]>())).Returns(1);

            var contact = new contactClass(mockDatabase.Object);

            // act
            Econtactcls econtactcls = new Econtactcls(contact, mockMessageBoxService.Object);
            econtactcls.txtboxFirstName.Text = FirstName;
            econtactcls.txtboxLastName.Text = LastName;
            econtactcls.txtBoxContactNumber.Text = ContactNo;
            econtactcls.txtBoxAddress.Text = Address;
            econtactcls.cmbGender.Text = Gender;

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
        [DataRow("Chris", "Tate", "83782758", "Seattle", "Male")]
        public void Verify_Add_Contact_Valid_Data_Negative(String FirstName, String LastName, String ContactNo, String Address, String Gender)
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
            var mockMessageBoxService = new Mock<IMessageBoxService>();

            mockDatabase.Setup
                (d => d.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<SqlParameter[]>())).Returns(0);

            var contact = new contactClass(mockDatabase.Object);

            // act
            Econtactcls econtactcls = new Econtactcls(contact, mockMessageBoxService.Object);
            econtactcls.txtboxFirstName.Text = FirstName;
            econtactcls.txtboxLastName.Text = LastName;
            econtactcls.txtBoxContactNumber.Text = ContactNo;
            econtactcls.txtBoxAddress.Text = Address;
            econtactcls.cmbGender.Text = Gender;

            try
            {
                econtactcls.add_Contact();
            }
            // assert
            catch (ConfigurationErrorsException ex)
            {

                Assert.Equals("Failed to add New Contact. Try Again.", ex.Message);
            }
            Console.WriteLine("Test passed. Add_contact negative()");

        }

        [TestMethod]
        [DataRow("234Bob", "Tate", "83782758", "Seattle", "Male", DisplayName = "Verify_Add_Contact_Invalid_FirstName")]
        [DataRow("", "Tate", "83782758", "Seattle", "Male", DisplayName = "Verify_Add_Contact_Invalid_Empty_FirstName")]
        [DataRow("Sam", "876458Duglus", "287498859", "Kent", "FeMale", DisplayName = "Verify_Add_Contact_Invalid_LastName")]
        [DataRow("Sam", "", "287498859", "Kent", "FeMale", DisplayName = "Verify_Add_Contact_Invalid_Empty_LastName")]
        [DataRow("Alice", "Bob", "fshdjh", "New York", "Male", DisplayName = "Verify_Add_Contact_Invalid_ContactNumber")]
        [DataRow("Alice", "Bob", "", "New York", "Male", DisplayName = "Verify_Add_Contact_Invalid_Empty_ContactNumber")]
        [DataRow("Dave", "Pat", "58785006", "", "FeMale", DisplayName = "Verify_Add_Contact_Invalid_Empty_Address")]
        [DataRow("George", "Joe", "387592386", "Dalls", "dbnbcjh", DisplayName = "Verify_Add_Contact_Invalid_Gender")]
        [DataRow("George", "Joe", "387592386", "Dalls", "", DisplayName = "Verify_Add_Contact_Invalid_Empty_Gender")]
        public void Verify_Add_Contact_Invalid_Data(String FirstName, String LastName, String ContactNo, String Address, String Gender)
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
            var mockMessageBoxService = new Mock<IMessageBoxService>();
            mockDatabase.Setup
                (d => d.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<SqlParameter[]>())).Returns(1);

            var contact = new contactClass(mockDatabase.Object);

            Econtactcls econtactcls = new Econtactcls(contact, mockMessageBoxService.Object);

            econtactcls.txtboxFirstName.Text = FirstName;
            econtactcls.txtboxLastName.Text = LastName;
            econtactcls.txtBoxContactNumber.Text = ContactNo;
            econtactcls.txtBoxAddress.Text = Address;
            econtactcls.cmbGender.Text = Gender;
            
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
        [DataRow("Matthew", "Josh", "83782758", "Seattle", "Male", "FirstName", "Mat", DisplayName ="Verify_Update_FirstName_Contact")]
        [DataRow("Sam", "Ryal", "287498859", "Kent", "Female", "LastName", "Rayl", DisplayName = "Verify_Update_LastName_Contact")]
        [DataRow("Alice", "Bob", "2483598", "New York", "Male", "ContactNo", "3824893", DisplayName = "Verify_Update_ContactNo_Contact")]
        [DataRow("Dave", "Pat", "58785006", "Portlan", "Female", "Address", "Orlando", DisplayName = "Verify_Update_Address_Contact")]
        [DataRow("George", "Joe", "387592386", "Dalls", "Male", "Gender", "Female", DisplayName = "Verify_Update_Gender_Contact")]
        public void Verify_Update_Contact_Valid_Data
                (String FirstName, String LastName, String ContactNo, String Address, String Gender, String Field_to_update, String New_Value)
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
            var mockMessageBoxService = new Mock<IMessageBoxService>();
            mockDatabase.Setup
                (d => d.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<SqlParameter[]>())).Returns(1);
            mockDatabase.Setup
                (d => d.ExecuteQuery(It.IsAny<string>(), It.IsAny<SqlParameter[]>())).Returns(new System.Data.DataTable());
            var contact = new contactClass(mockDatabase.Object);

            Econtactcls econtactcls = new Econtactcls(contact, mockMessageBoxService.Object);
            econtactcls.txtboxFirstName.Text = FirstName;
            econtactcls.txtboxLastName.Text = LastName;
            econtactcls.txtBoxContactNumber.Text = ContactNo;
            econtactcls.txtBoxAddress.Text = Address;
            econtactcls.cmbGender.Text = Gender;

            //act
            try
            {
                econtactcls.add_Contact();
                econtactcls.txtboxFirstName.Text = (Field_to_update == "FirstName") ? New_Value : FirstName;
                econtactcls.txtboxLastName.Text = (Field_to_update == "LastName") ? New_Value : LastName;
                econtactcls.txtBoxContactNumber.Text = (Field_to_update == "ContactNo") ? New_Value : ContactNo;
                econtactcls.txtBoxAddress.Text= (Field_to_update == "Address") ? New_Value : Address;
                econtactcls.cmbGender.Text = (Field_to_update == "Gender") ? New_Value : Gender;
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

        [TestMethod]
        [DataRow("George", "Joe", "387592386", "Dalls", "Male", "Gender", "Female", DisplayName = "Verify_Update_Gender_Contact")]
        public void Verify_Update_Contact_Valid_Data_Negative
                (String FirstName, String LastName, String ContactNo, String Address, String Gender, String Field_to_update, String New_Value)
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
            var mockMessageBoxService = new Mock<IMessageBoxService>();
            mockDatabase.Setup
                (d => d.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<SqlParameter[]>())).Returns(0);
            mockDatabase.Setup
                (d => d.ExecuteQuery(It.IsAny<string>(), It.IsAny<SqlParameter[]>())).Returns(new System.Data.DataTable());
            var contact = new contactClass(mockDatabase.Object);

            Econtactcls econtactcls = new Econtactcls(contact, mockMessageBoxService.Object);
            econtactcls.txtboxFirstName.Text = FirstName;
            econtactcls.txtboxLastName.Text = LastName;
            econtactcls.txtBoxContactNumber.Text = ContactNo;
            econtactcls.txtBoxAddress.Text = Address;
            econtactcls.cmbGender.Text = Gender;

            //act
            try
            {
                econtactcls.add_Contact();
                econtactcls.txtboxFirstName.Text = (Field_to_update == "FirstName") ? New_Value : FirstName;
                econtactcls.txtboxLastName.Text = (Field_to_update == "LastName") ? New_Value : LastName;
                econtactcls.txtBoxContactNumber.Text = (Field_to_update == "ContactNo") ? New_Value : ContactNo;
                econtactcls.txtBoxAddress.Text = (Field_to_update == "Address") ? New_Value : Address;
                econtactcls.cmbGender.Text = (Field_to_update == "Gender") ? New_Value : Gender;
                econtactcls.txtboxContactID.Text = "1";
                econtactcls.update_Contact();
            }
            //assert
            catch (ConfigurationErrorsException ex)
            {
                Assert.Equals("Error: Failed to Update Contact.Try Again.", ex.Message);
            }
            Console.WriteLine("Test Passed: Update contact test is passed.");

        }

        [TestMethod]
        [DataRow("Chris", "Tate", "83782758", "Seattle", "Male")]
        public void Verify_Update_Non_Existing_contact(String FirstName, String LastName, String ContactNo, String Address, String Gender)
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
            var mockMessageBoxService = new Mock<IMessageBoxService>();
            mockDatabase.Setup
                (d => d.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<SqlParameter[]>())).Returns(1);
            mockDatabase.Setup
                (d => d.ExecuteQuery(It.IsAny<string>(), It.IsAny<SqlParameter[]>())).Returns(new System.Data.DataTable());

            var contact = new contactClass(mockDatabase.Object);

            Econtactcls econtactcls = new Econtactcls(contact, mockMessageBoxService.Object);

            econtactcls.txtboxFirstName.Text = FirstName;
            econtactcls.txtboxLastName.Text = LastName;
            econtactcls.txtBoxContactNumber.Text = ContactNo;
            econtactcls.txtBoxAddress.Text = Address;
            econtactcls.cmbGender.Text = Gender;
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
        [DataRow("Matthew", "Josh", "83782758", "Seattle", "Male", "FirstName", "23455", DisplayName = "Verify_Update_Invalid_FirstName_Contact")]
        [DataRow("Sam", "Ryal", "287498859", "Kent", "Female", "LastName", "", DisplayName = "Verify_Update__Invalid_LastName_Contact")]
        [DataRow("Alice", "Bob", "2483598", "New York", "Male", "ContactNo", "", DisplayName = "Verify_Update_Invalid_ContactNo_Contact")]
        [DataRow("Dave", "Pat", "58785006", "Portlan", "Female", "Address", "", DisplayName = "Verify_Update_Invalid_Address_Contact")]
        [DataRow("George", "Joe", "387592386", "Dalls", "Male", "Gender", "vskfkjl", DisplayName = "Verify_Update_Invalid_Gender_Contact")]
        public void Verify_Update_contact_Invalid_Data
                    (String FirstName, String LastName, String ContactNo, String Address, String Gender, String Field_to_update, String New_Value)
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
            var mockMessageBoxService = new Mock<IMessageBoxService>();
            mockDatabase.Setup
                (d => d.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<SqlParameter[]>())).Returns(1);
            mockDatabase.Setup
                (d => d.ExecuteQuery(It.IsAny<string>(), It.IsAny<SqlParameter[]>())).Returns(new System.Data.DataTable());

            var contact = new contactClass(mockDatabase.Object);

            Econtactcls econtactcls = new Econtactcls(contact, mockMessageBoxService.Object);

            econtactcls.txtboxFirstName.Text = FirstName;
            econtactcls.txtboxLastName.Text = LastName;
            econtactcls.txtBoxContactNumber.Text = ContactNo;
            econtactcls.txtBoxAddress.Text = Address;
            econtactcls.cmbGender.Text = Gender;
            // act
            try
            {
                econtactcls.add_Contact();
                econtactcls.txtboxFirstName.Text = (Field_to_update == "FirstName") ? New_Value : FirstName;
                econtactcls.txtboxLastName.Text = (Field_to_update == "LastName") ? New_Value : LastName;
                econtactcls.txtBoxContactNumber.Text = (Field_to_update == "ContactNo") ? New_Value : ContactNo;
                econtactcls.txtBoxAddress.Text = (Field_to_update == "Address") ? New_Value : Address;
                econtactcls.cmbGender.Text = (Field_to_update == "Gender") ? New_Value : Gender;
                econtactcls.txtboxContactID.Text = "1";
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
        [DataRow("Chris", "Tate", "83782758", "Seattle", "Male")]
        [DataRow("Sam", "Fog", "287498859", "Kent", "FeMale")]
        public void Verify_Delete_contact_Valid_Data(String FirstName, String LastName, String ContactNo, String Address, String Gender)
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
            var mockMessageBoxService = new Mock<IMessageBoxService>();
            mockDatabase.Setup
                (d => d.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<SqlParameter[]>())).Returns(1);
            mockDatabase.Setup
                (d => d.ExecuteQuery(It.IsAny<string>(), It.IsAny<SqlParameter[]>())).Returns(new System.Data.DataTable());
            var contact = new contactClass(mockDatabase.Object);

            Econtactcls econtactcls = new Econtactcls(contact, mockMessageBoxService.Object);
            econtactcls.txtboxFirstName.Text = FirstName;
            econtactcls.txtboxLastName.Text = LastName;
            econtactcls.txtBoxContactNumber.Text = ContactNo;
            econtactcls.txtBoxAddress.Text = Address;
            econtactcls.cmbGender.Text = Gender;

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
        [DataRow("Sam", "Fog", "287498859", "Kent", "FeMale")]
        public void Verify_Delete_contact_Valid_Data_Negative(String FirstName, String LastName, String ContactNo, String Address, String Gender)
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
            var mockMessageBoxService = new Mock<IMessageBoxService>();
            mockDatabase.Setup
                (d => d.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<SqlParameter[]>())).Returns(0);
            mockDatabase.Setup
                (d => d.ExecuteQuery(It.IsAny<string>(), It.IsAny<SqlParameter[]>())).Returns(new System.Data.DataTable());
            var contact = new contactClass(mockDatabase.Object);

            Econtactcls econtactcls = new Econtactcls(contact, mockMessageBoxService.Object);
            econtactcls.txtboxFirstName.Text = FirstName;
            econtactcls.txtboxLastName.Text = LastName;
            econtactcls.txtBoxContactNumber.Text = ContactNo;
            econtactcls.txtBoxAddress.Text = Address;
            econtactcls.cmbGender.Text = Gender;

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
                Assert.Equals("Failed to Delete Dontact. Try Again.",ex.Message);
            }
            Console.WriteLine("Test Passed: Delete contact test is passed.");

        }

        [TestMethod]
        [DataRow("Chris", "Tate", "83782758", "Seattle", "Male")]

        public void Verify_Delete_Contact_Inalid_Data(String FirstName, String LastName, String ContactNo, String Address, String Gender)
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
            var mockMessageBoxService = new Mock<IMessageBoxService>();
            mockDatabase.Setup
                (d => d.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<SqlParameter[]>())).Returns(1);
            mockDatabase.Setup
                (d => d.ExecuteQuery(It.IsAny<string>(), It.IsAny<SqlParameter[]>())).Returns(new System.Data.DataTable());

            var contact = new contactClass(mockDatabase.Object);

            Econtactcls econtactcls = new Econtactcls(contact, mockMessageBoxService.Object);

            econtactcls.txtboxFirstName.Text = FirstName;
            econtactcls.txtboxLastName.Text = LastName;
            econtactcls.txtBoxContactNumber.Text = ContactNo;
            econtactcls.txtBoxAddress.Text = Address;
            econtactcls.cmbGender.Text = Gender;
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
        [DataRow("Chris", "Tate", "83782758", "Seattle", "Male")]
        public void Verify_Clear_Contact(String FirstName, String LastName, String ContactNo, String Address, String Gender)
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
            var mockMessageBoxService = new Mock<IMessageBoxService>();
            mockDatabase.Setup
                (d => d.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<SqlParameter[]>())).Returns(1);
            mockDatabase.Setup
                (d => d.ExecuteQuery(It.IsAny<string>(), It.IsAny<SqlParameter[]>())).Returns(new System.Data.DataTable());

            var contact = new contactClass(mockDatabase.Object);

            Econtactcls econtactcls = new Econtactcls(contact, mockMessageBoxService.Object);
            econtactcls.txtboxFirstName.Text = FirstName;
            econtactcls.txtboxLastName.Text = LastName;
            econtactcls.txtBoxContactNumber.Text = ContactNo;
            econtactcls.txtBoxAddress.Text = Address;
            econtactcls.cmbGender.Text = Gender;
         

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


        [TestMethod]
        [DataRow("Chris", "Tate", "83782758", "Seattle", "Male")]
        public void Verify_Search_Contact_With_Add_Contact(String FirstName, String LastName, String ContactNo, String Address, String Gender)
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
            var mockMessageBoxService = new Mock<IMessageBoxService>();
            mockDatabase.Setup
                (d => d.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<SqlParameter[]>())).Returns(1);
            mockDatabase.Setup
                (d => d.ExecuteQuery(It.IsAny<string>(), It.IsAny<SqlParameter[]>())).Returns(new System.Data.DataTable());

            var contact = new contactClass(mockDatabase.Object);

            Econtactcls econtactcls = new Econtactcls(contact, mockMessageBoxService.Object);
            econtactcls.txtboxFirstName.Text = FirstName;
            econtactcls.txtboxLastName.Text = LastName;
            econtactcls.txtBoxContactNumber.Text = ContactNo;
            econtactcls.txtBoxAddress.Text = Address;
            econtactcls.cmbGender.Text = Gender;
            econtactcls.txtboxContactID.Text = "1";

            // act
            try
            {
                econtactcls.add_Contact();
                econtactcls.Search(FirstName);
                
            }
            // assert
            catch (Exception ex)
            {
                Console.WriteLine($"Test Failed: Search contact test failed with message {ex.Message}");
            }
            Console.WriteLine("Test passed: Search contact is passed.");
        }

        [TestMethod]
        [DataRow("Chris", "Tate", "83782758", "Seattle", "Male")]
        public void Verify_Insert_Database(String FirstName, String LastName, String ContactNo, String Address, String Gender)
        {
            /// <summary>
            /// 1. Verify Insert Database Operation
            /// 2. Mock function is used to replace database connection module
            /// <summary>

            // arrange
            var mockDatabase = new Mock<IDatabase>();
  
            mockDatabase.Setup
                (d => d.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<SqlParameter[]>())).Returns(1);
            var contact = new contactClass(mockDatabase.Object );
            contact.FirstName = FirstName;
            contact.LastName = LastName;
            contact.Address = Address;
            contact.ContactNo = (int)long.Parse(ContactNo);
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
        [DataRow("Chris", "Tate", "83782758", "Seattle", "Male")]
        public void Verify_Insert_Database_With_Exception(String FirstName, String LastName, String ContactNo, String Address, String Gender)
        {
            /// <summary>
            /// 1. Verify Insert Database Operation, send contact_no in text format
            /// 2. Mock function is used to replace database connection module
            /// <summary>

            // arrange
            var mockDatabase = new Mock<IDatabase>();
            
            var contact = new contactClass(mockDatabase.Object);
            contact.FirstName = FirstName;
            contact.LastName = LastName;
            contact.Address = Address;
            contact.ContactNo = (int)long.Parse(ContactNo); ;
            contact.Gender = Gender;
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
        [DataRow("Chris", "Tate", "83782758", "Seattle", "Male")]
        public void Verify_Update_Database(String FirstName, String LastName, String ContactNo, String Address, String Gender)
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
            contact.FirstName = FirstName;
            contact.LastName = LastName;
            contact.Address = Address;
            contact.ContactNo = (int)long.Parse(ContactNo);
            contact.Gender = Gender;
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
        [DataRow("Chris", "Tate", "83782758", "Seattle", "Male")]
        public void Verify_Update_Database_With_Exception(String FirstName, String LastName, String ContactNo, String Address, String Gender)
        {
            /// <summary>
            /// 1. Verify update Database Operation, send contact_no in text format, invalid format.
            /// 2. Mock function is used to replace database connection module
            /// <summary>

            // arrange
            var mockDatabase = new Mock<IDatabase>();
            
            var contact = new contactClass(mockDatabase.Object);
            contact.FirstName = FirstName;
            contact.LastName = LastName;
            contact.Address = Address;
            contact.ContactNo = (int)long.Parse(ContactNo);
            contact.Gender = Gender;
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

        [TestMethod]
        [DataRow("Chris", "Tate", "83782758", "Seattle", "Male")]
        public void Verify_Delete_Database(String FirstName, String LastName, String ContactNo, String Address, String Gender)
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
            contact.FirstName = FirstName;
            contact.LastName = LastName;
            contact.Address = Address;
            contact.ContactNo = (int)long.Parse(ContactNo); ;
            contact.Gender = Gender;
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
        [DataRow("Chris", "Tate", "83782758", "Seattle", "Male")]
        public void Verify_Delete_Database_With_Exception(String FirstName, String LastName, String ContactNo, String Address, String Gender)
        {
            /// <summary>
            /// 1. Verify Delete Database Operation, send contact_no in text format
            /// 2. Mock function is used to replace database connection module
            /// <summary>

            // arrange
            var mockDatabase = new Mock<IDatabase>();
            
            var contact = new contactClass(mockDatabase.Object);
            contact.FirstName = FirstName;
            contact.LastName = LastName;
            contact.Address = Address;
            contact.ContactNo = (int)long.Parse(ContactNo);
            contact.Gender = Gender;
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
