using Econtact.econtactClasses;
using Econtact;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Configuration;
using System.Data;

namespace TestProject1
{
    [TestClass]
    public class IntegrationTests
    {

        [TestMethod]
        [DataRow("Chris", "Tate", "83782758", "Seattle", "Male")]
        public void Verify_Search_Contact_Data_Integration(String FirstName, String LastName, String ContactNo, String Address, String Gender)
        {
            /// <summary>
            /// 1. Add valid data while creating new data
            /// 2. Mock function is used to replace database module
            /// 3. Validating Add_Contact event handler with this unit test
            /// 4. With valid data inpus, add_contact is successful.
            /// 5. Assert if add_contact is failed.
            /// <summary>
            // arrange
            try
            {
                // act
                Econtactcls econtactcls = new Econtactcls();
                econtactcls.txtboxFirstName.Text = FirstName;
                econtactcls.txtboxLastName.Text = LastName;
                econtactcls.txtBoxContactNumber.Text = ContactNo;
                econtactcls.txtBoxAddress.Text = Address;
                econtactcls.cmbGender.Text = Gender;

                //Step 1: Add Contact
                econtactcls.add_Contact();

                //Step2: Search contact with any keyword in the database
                DataTable dt = econtactcls.Search(FirstName);

                //Validate contact exists.
                Assert.IsNotNull(dt);
                Console.WriteLine("Search is successful, contact exists.");

                //Step 3: Delete contact after successful addition.
                econtactcls.txtboxContactID.Text = dt.Rows[0]["ContactID"].ToString();
                econtactcls.delete_Contact();
                Console.WriteLine("Contact deleted successfully");
            }
            // assert
            catch (ConfigurationErrorsException ex)
            {

                Assert.Fail($"Test Failed: Error while creating contac. Message: {ex.Message}");
            }
            Console.WriteLine("Test passed. Add_contact()");

        }

        [TestMethod]
        [DataRow("Chris", "Tate", "83782758", "Seattle", "Male")]
        public void Verify_Add_Contact_Valid_Data_Integration(String FirstName, String LastName, String ContactNo, String Address, String Gender)
        {
            /// <summary>
            /// 1. Add valid data while creating new data
            /// 2. Mock function is used to replace database module
            /// 3. Validating Add_Contact event handler with this unit test
            /// 4. With valid data inpus, add_contact is successful.
            /// 5. Assert if add_contact is failed.
            /// <summary>
            // arrange
            try {
            // act
                Econtactcls econtactcls = new Econtactcls();
                econtactcls.txtboxFirstName.Text = FirstName;
                econtactcls.txtboxLastName.Text = LastName;
                econtactcls.txtBoxContactNumber.Text = ContactNo;
                econtactcls.txtBoxAddress.Text = Address;
                econtactcls.cmbGender.Text = Gender;

                //Step 1: Add Contact
                econtactcls.add_Contact();

                //Step2: Search contact with any keyword in the database
                DataTable dt = econtactcls.Search(FirstName);

                //Validate contact exists.
                Assert.IsNotNull(dt);
                Console.WriteLine("Contact added successfully");

                //Step 3: Delete contact after successful addition.
                econtactcls.txtboxContactID.Text = dt.Rows[0]["ContactID"].ToString();
                econtactcls.delete_Contact();
                Console.WriteLine("Contact deleted successfully");
            }
            // assert
            catch (ConfigurationErrorsException ex)
            {

                Assert.Fail($"Test Failed: Error while creating contac. Message: {ex.Message}");
            }
            Console.WriteLine("Test passed. Add_contact()");

        }

        [TestMethod]
        [DataRow("Matthew", "Josh", "83782758", "Seattle", "Male", "FirstName", "Mat", DisplayName = "Verify_Update_FirstName_Contact")]
        [DataRow("Sam", "Ryal", "287498859", "Kent", "Female", "LastName", "Rayl", DisplayName = "Verify_Update_LastName_Contact")]
        [DataRow("Alice", "Bob", "2483598", "New York", "Male", "ContactNo", "3824893", DisplayName = "Verify_Update_ContactNo_Contact")]
        [DataRow("Dave", "Pat", "58785006", "Portlan", "Female", "Address", "Orlando", DisplayName = "Verify_Update_Address_Contact")]
        [DataRow("George", "Joe", "387592386", "Dalls", "Male", "Gender", "Female", DisplayName = "Verify_Update_Gender_Contact")]
        public void Verify_Update_Existing_Contact
                 (String FirstName, String LastName, String ContactNo, String Address, String Gender, String Field_to_update, String New_Value)

        {
            /// <summary>
            /// 1. Add valid data while creating new data
            /// 2. Mock function is used to replace database module
            /// 3. Validating Add_Contact event handler with this unit test
            /// 4. With valid data inpus, add_contact is successful.
            /// 5. Assert if add_contact is failed.
            /// <summary>
            // arrange
            try
            {
                // act
                Econtactcls econtactcls = new Econtactcls();
                econtactcls.txtboxFirstName.Text = FirstName;
                econtactcls.txtboxLastName.Text = LastName;
                econtactcls.txtBoxContactNumber.Text = ContactNo;
                econtactcls.txtBoxAddress.Text = Address;
                econtactcls.cmbGender.Text = Gender;

                //Step 1: Add Contact
                econtactcls.add_Contact();

                //Step2: Search contact with any keyword in the database
                DataTable dt = econtactcls.Search(FirstName);

                //Validate contact exists.
                Assert.IsNotNull(dt);
                Console.WriteLine("Contact added successfully");

                //Setp 3: Update contact added previously
                econtactcls.txtboxFirstName.Text = (Field_to_update == "FirstName") ? New_Value : FirstName;
                econtactcls.txtboxLastName.Text = (Field_to_update == "LastName") ? New_Value : LastName;
                econtactcls.txtBoxContactNumber.Text = (Field_to_update == "ContactNo") ? New_Value : ContactNo;
                econtactcls.txtBoxAddress.Text = (Field_to_update == "Address") ? New_Value : Address;
                econtactcls.cmbGender.Text = (Field_to_update == "Gender") ? New_Value : Gender;
                econtactcls.txtboxContactID.Text = dt.Rows[0]["ContactID"].ToString();

                econtactcls.update_Contact();
                //Step 3: Delete contact after successful addition.
                econtactcls.txtboxContactID.Text = dt.Rows[0]["ContactID"].ToString();
                econtactcls.delete_Contact();
                Console.WriteLine("Contact deleted successfully");
            }
            // assert
            catch (ConfigurationErrorsException ex)
            {

                Assert.Fail($"Test Failed: Error while creating contac. Message: {ex.Message}");
            }
            Console.WriteLine("Test passed. Add_contact()");

        }

        [TestMethod]
        [DataRow("Mary", "Carls", "58738967", "Rutegers", "Male")]
        public void Verify_Delete_Existing_Contact(String FirstName, String LastName, String ContactNo, String Address, String Gender)
        {
            /// <summary>
            /// 1. Add valid data while creating new data
            /// 2. Mock function is used to replace database module
            /// 3. Validating Add_Contact event handler with this unit test
            /// 4. With valid data inpus, add_contact is successful.
            /// 5. Assert if add_contact is failed.
            /// <summary>
            // arrange
            try
            {
                // act
                Econtactcls econtactcls = new Econtactcls();
                econtactcls.txtboxFirstName.Text = FirstName;
                econtactcls.txtboxLastName.Text = LastName;
                econtactcls.txtBoxContactNumber.Text = ContactNo;
                econtactcls.txtBoxAddress.Text = Address;
                econtactcls.cmbGender.Text = Gender;

                //Step 1: Add Contact
                econtactcls.add_Contact();

                //Step2: Search contact with any keyword in the database
                DataTable dt = econtactcls.Search(FirstName);

                //Validate contact exists.
                Assert.IsNotNull(dt);
                Console.WriteLine("Contact added successfully");

                //Step 3: Delete contact after successful addition.
                econtactcls.txtboxContactID.Text = dt.Rows[0]["ContactID"].ToString();
                econtactcls.delete_Contact();
                //Validate delete data.
                DataTable dt1 = econtactcls.Search(FirstName);
                Assert.IsTrue(dt1.Rows.Count == 0);
                Console.WriteLine("Contact deleted successfully");
            }
            // assert
            catch (ConfigurationErrorsException ex)
            {

                Assert.Fail($"Test Failed: Error while creating contac. Message: {ex.Message}");
            }
            Console.WriteLine("Test passed. Add_contact()");

        }


        [TestMethod]
        [DataRow("Chris", "Tate", "83782758", "Seattle", "Male")]
        public void Verify_Search_Non_Existing_Contact(String FirstName, String LastName, String ContactNo, String Address, String Gender)
        {
            /// <summary>
            /// 1. Add valid data while creating new data
            /// 2. Mock function is used to replace database module
            /// 3. Validating Add_Contact event handler with this unit test
            /// 4. With valid data inpus, add_contact is successful.
            /// 5. Assert if add_contact is failed.
            /// <summary>
            // arrange
            try
            {
                // act
                Econtactcls econtactcls = new Econtactcls();

                //Step2: Search contact with any keyword in the database
                DataTable dt = econtactcls.Search(FirstName);

                //Validate contact exists.
                Assert.IsTrue(dt.Rows.Count == 0);
                Console.WriteLine("No such contact exists");

                //Step 3: Delete contact after successful addition.
                econtactcls.txtboxContactID.Text = dt.Rows[0]["ContactID"].ToString();

            }
            catch (Exception ex)
            {

                Assert.IsInstanceOfType(ex, typeof(Exception));
            }
            Console.WriteLine("Test passed. Add_contact()");
        }
        
        [TestMethod]
        [DataRow("Chris", "Tate", "83782758", "Seattle", "Male")]
        public void Verify_Update_Non_Existing_Contact(String FirstName, String LastName, String ContactNo, String Address, String Gender)
        {
            /// <summary>
            /// 1. Add valid data while creating new data
            /// 2. Mock function is used to replace database module
            /// 3. Validating Add_Contact event handler with this unit test
            /// 4. With valid data inpus, add_contact is successful.
            /// 5. Assert if add_contact is failed.
            /// <summary>
            // arrange
            try
            {
                // act
                Econtactcls econtactcls = new Econtactcls();

                //Step2: Search contact with any keyword in the database
                DataTable dt = econtactcls.Search(FirstName);

                //Validate contact exists.
                Assert.IsTrue(dt.Rows.Count == 0);
                Console.WriteLine("No contact found");

                //Setp 3: Update contact added previously
                econtactcls.txtboxFirstName.Text = FirstName;
                econtactcls.txtboxLastName.Text = LastName;
                econtactcls.txtBoxContactNumber.Text = ContactNo;
                econtactcls.txtBoxAddress.Text = Address;
                econtactcls.cmbGender.Text = Gender;
                econtactcls.txtboxContactID.Text = dt.Rows[0]["ContactID"].ToString();

                econtactcls.update_Contact();
                //Step 3: Delete contact after successful addition.
                econtactcls.txtboxContactID.Text = dt.Rows[0]["ContactID"].ToString();
            }
            // assert
            catch (Exception ex)
            {

                Assert.IsInstanceOfType(ex, typeof(Exception));
            }
            Console.WriteLine("Test passed. Add_contact()");

        }

        [TestMethod]
        [DataRow("Chris", "Tate", "83782758", "Seattle", "Male")]
        public void Verify_Delete_Non_Existing_Contact(String FirstName, String LastName, String ContactNo, String Address, String Gender)
        {
            /// <summary>
            /// 1. Add valid data while creating new data
            /// 2. Mock function is used to replace database module
            /// 3. Validating Add_Contact event handler with this unit test
            /// 4. With valid data inpus, add_contact is successful.
            /// 5. Assert if add_contact is failed.
            /// <summary>
            // arrange
            try
            {
                // act
                Econtactcls econtactcls = new Econtactcls();
              
                //Step2: Search contact with any keyword in the database
                DataTable dt = econtactcls.Search(FirstName);

                //Validate contact exists.
                Assert.IsTrue(dt.Rows.Count == 0);
                Console.WriteLine("No Such Contact exists");

                //Step 3: Delete contact after successful addition.
                econtactcls.txtboxContactID.Text = dt.Rows[0]["ContactID"].ToString();
                econtactcls.delete_Contact();
                Console.WriteLine("Contact deleted successfully");
            }
            // assert
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(Exception));
            }
            Console.WriteLine("Test passed. Add_contact()");

        }
    }
}
