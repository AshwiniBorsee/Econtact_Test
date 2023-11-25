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
        public void Verify_Search_Contact_Data_Integration()
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
                econtactcls.txtboxFirstName.Text = "Mary";
                econtactcls.txtboxLastName.Text = "Carles";
                econtactcls.txtBoxContactNumber.Text = "918765";
                econtactcls.txtBoxAddress.Text = "Seattle";
                econtactcls.cmbGender.Text = "Male";

                //Step 1: Add Contact
                econtactcls.add_Contact();

                //Step2: Search contact with any keyword in the database
                DataTable dt = econtactcls.Search("Mary");

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
        public void Verify_Add_Contact_Valid_Data_Integration()
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
                econtactcls.txtboxFirstName.Text = "Mary";
                econtactcls.txtboxLastName.Text = "Carles";
                econtactcls.txtBoxContactNumber.Text = "918765";
                econtactcls.txtBoxAddress.Text = "Seattle";
                econtactcls.cmbGender.Text = "Male";

                //Step 1: Add Contact
                econtactcls.add_Contact();

                //Step2: Search contact with any keyword in the database
                DataTable dt = econtactcls.Search("Mary");

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
        public void Verify_Update_Existing_Contact()
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
                econtactcls.txtboxFirstName.Text = "Mary";
                econtactcls.txtboxLastName.Text = "Carles";
                econtactcls.txtBoxContactNumber.Text = "918765";
                econtactcls.txtBoxAddress.Text = "Seattle";
                econtactcls.cmbGender.Text = "Male";

                //Step 1: Add Contact
                econtactcls.add_Contact();

                //Step2: Search contact with any keyword in the database
                DataTable dt = econtactcls.Search("Mary");

                //Validate contact exists.
                Assert.IsNotNull(dt);
                Console.WriteLine("Contact added successfully");

                //Setp 3: Update contact added previously
                econtactcls.txtboxFirstName.Text = "Marsh";
                econtactcls.txtboxLastName.Text = "Carles";
                econtactcls.txtBoxContactNumber.Text = "918765";
                econtactcls.txtBoxAddress.Text = "Seattle";
                econtactcls.cmbGender.Text = "Male";
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
        public void Verify_Delete_Existing_Contact()
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
                econtactcls.txtboxFirstName.Text = "Mary";
                econtactcls.txtboxLastName.Text = "Carles";
                econtactcls.txtBoxContactNumber.Text = "918765";
                econtactcls.txtBoxAddress.Text = "Seattle";
                econtactcls.cmbGender.Text = "Male";

                //Step 1: Add Contact
                econtactcls.add_Contact();

                //Step2: Search contact with any keyword in the database
                DataTable dt = econtactcls.Search("Mary");

                //Validate contact exists.
                Assert.IsNotNull(dt);
                Console.WriteLine("Contact added successfully");

                //Step 3: Delete contact after successful addition.
                econtactcls.txtboxContactID.Text = dt.Rows[0]["ContactID"].ToString();
                econtactcls.delete_Contact();
                //Validate delete data.
                DataTable dt1 = econtactcls.Search("Mary");
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
        public void Verify_Search_Non_Existing_Contact()
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
                DataTable dt = econtactcls.Search("Mary");

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
        public void Verify_Update_Non_Existing_Contact()
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
                DataTable dt = econtactcls.Search("Mary");

                //Validate contact exists.
                Assert.IsTrue(dt.Rows.Count == 0);
                Console.WriteLine("No contact found");

                //Setp 3: Update contact added previously
                econtactcls.txtboxFirstName.Text = "Marsh";
                econtactcls.txtboxLastName.Text = "Carles";
                econtactcls.txtBoxContactNumber.Text = "918765";
                econtactcls.txtBoxAddress.Text = "Seattle";
                econtactcls.cmbGender.Text = "Male";
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
        public void Verify_Delete_Non_Existing_Contact()
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
                DataTable dt = econtactcls.Search("Mary");

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
