using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using FlaUI.Core;
using FlaUI.UIA3;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Input;
using FlaUI.Core.WindowsAPI;
using FlaUI.Core.Definitions;
using FlaUI.Core.Tools;
using System.Threading;
using Econtact;
using System.Data;
using System.IO;

namespace UIAutomation
{
    [TestClass]
    public class UIAutomationTests
    {
        [TestMethod]
        [DataRow("Chris", "Tate", "83782758", "Seattle", "Male", DisplayName = "Verify_UI_Add_Contact_Valid_Data")]
        
        public void Verify_UI_Add_Contact(String FirstName, String LastName, String ContactNo, String Address, String Gender)
        {
            //Step 1: Launch application
            Application app = StartApplication();
            UIA3Automation automation = new UIA3Automation();
            var window = app.GetMainWindow(automation);
           
            //Step 2: Add contact, fill up the data
            var first_name = window.FindFirstDescendant(cf => cf.ByAutomationId("txtboxFirstName")).AsTextBox();
            first_name.Enter(FirstName);
            var last_name = window.FindFirstDescendant(cf => cf.ByAutomationId("txtboxLastName")).AsTextBox();
            last_name.Enter(LastName);
            var contact_no = window.FindFirstDescendant(cf => cf.ByAutomationId("txtBoxContactNumber")).AsTextBox();
            contact_no.Enter(ContactNo);
            var address = window.FindFirstDescendant(cf => cf.ByAutomationId("txtBoxAddress")).AsTextBox();
            address.Enter(Address);
            var comboBox = window.FindFirstDescendant(cf => cf.ByControlType(FlaUI.Core.Definitions.ControlType.ComboBox));
            var edit = comboBox.FindFirstDescendant(cf => cf.ByControlType(FlaUI.Core.Definitions.ControlType.Edit));
            edit.Focus();
            Keyboard.Type(Gender);
            Thread.Sleep(2000);

            //Step 3: Click the add_button
            var add_button = window.FindFirstDescendant(cf => cf.ByAutomationId("btnAdd")).AsButton();
            add_button.Click();
            Wait.UntilInputIsProcessed();
            Thread.Sleep(2000);
            var ok_button = window.FindFirstDescendant(cf => cf.ByAutomationId("2")).AsButton();
            ok_button.Click();
            Wait.UntilInputIsProcessed();
            Thread.Sleep(3000);

            //Step 4: Close the application
            app.Close();

        }

        [TestMethod]
        [DataRow("Matthew", "Josh", "83782758", "Seattle", "Male", "FirstName", "Mat", DisplayName = "UI_Update_FirstName_Contact")]
        [DataRow("Sam", "Ryal", "287498859", "Kent", "Female", "LastName", "Rayl", DisplayName = "UI_Update_LastName_Contact")]
        [DataRow("Alice", "Bob", "2483598", "New York", "Male", "ContactNo", "3824893", DisplayName = "UI_Update_ContactNo_Contact")]
        [DataRow("Dave", "Pat", "58785006", "Portland", "Female", "Address", "Orlando", DisplayName = "UI_Update_Address_Contact")]
        [DataRow("George", "Joe", "387592386", "Dalls", "Male", "Gender", "Female", DisplayName = "UI_Update_Gender_Contact")]
        public void Verify_UI_Update_Contact
            (String FirstName, String LastName, String ContactNo, String Address, String Gender, String Field_to_update, String New_Value)
        {
            //Step 1: Launch Application
            Econtactcls econtactcls = new Econtactcls();
            Application app = StartApplication();
            UIA3Automation automation = new UIA3Automation();
            var window = app.GetMainWindow(automation);
            
            //Step 2: Add contact
            var first_name = window.FindFirstDescendant(cf => cf.ByAutomationId("txtboxFirstName")).AsTextBox();
            first_name.Enter(FirstName);
            var last_name = window.FindFirstDescendant(cf => cf.ByAutomationId("txtboxLastName")).AsTextBox();
            last_name.Enter(LastName);
            var contact_no = window.FindFirstDescendant(cf => cf.ByAutomationId("txtBoxContactNumber")).AsTextBox();
            contact_no.Enter(ContactNo);
            var address = window.FindFirstDescendant(cf => cf.ByAutomationId("txtBoxAddress")).AsTextBox();
            address.Enter(Address);
            var comboBox = window.FindFirstDescendant(cf => cf.ByControlType(FlaUI.Core.Definitions.ControlType.ComboBox));
            var edit = comboBox.FindFirstDescendant(cf => cf.ByControlType(FlaUI.Core.Definitions.ControlType.Edit));
            edit.Focus();
            Keyboard.Type(Gender);
            Thread.Sleep(2000);
            var add_button = window.FindFirstDescendant(cf => cf.ByAutomationId("btnAdd")).AsButton();
            add_button.Click();
            Wait.UntilInputIsProcessed();
            Thread.Sleep(2000);
            var ok_button = window.FindFirstDescendant(cf => cf.ByAutomationId("2")).AsButton();
            ok_button.Click();
            Wait.UntilInputIsProcessed();
            Thread.Sleep(3000);

            //Step 3: Search contact ID
            var searchBox = window.FindFirstDescendant(cf => cf.ByAutomationId("txtboxSearch")).AsTextBox();
            searchBox.Enter(FirstName);

            DataTable dt = econtactcls.Search(FirstName);
            var contact_id = window.FindFirstDescendant(cf => cf.ByAutomationId("txtboxContactID")).AsTextBox();
            contact_id.Enter(dt.Rows[0]["ContactID"].ToString());
            FirstName = (Field_to_update == "FirstName") ? New_Value : FirstName;
            LastName = (Field_to_update == "LastName") ? New_Value : LastName;
            ContactNo = (Field_to_update == "ContactNo") ? New_Value : ContactNo;
            Address = (Field_to_update == "Address") ? New_Value : Address;
            Gender = (Field_to_update == "Gender") ? New_Value : Gender;
    
            first_name.Enter(FirstName);
            last_name.Enter(LastName);
            contact_no.Enter(ContactNo);
            address.Enter(Address);
            edit.Focus();
            Keyboard.Type(Gender);
            Thread.Sleep(2000);

            //Step 4: Update contact
            var update_button = window.FindFirstDescendant(cf => cf.ByAutomationId("btnUpdate")).AsButton();
            update_button.Click();
            Wait.UntilInputIsProcessed();
            Thread.Sleep(2000);
            ok_button = window.FindFirstDescendant(cf => cf.ByAutomationId("2")).AsButton();
            ok_button.Click();
            Thread.Sleep(3000);

            //Step 5: Close the application
            app.Close();

        }

        [TestMethod]
        [DataRow("Josh", "Matt", "9875645", "Rutger", "Female")]
        public void Verify_UI_Delete_Contact(String FirstName, String LastName, String ContactNo, String Address, String Gender)
        {
            //Step 1: Launch the application
            Econtactcls econtactcls = new Econtactcls();
            Application app = StartApplication();
            UIA3Automation automation = new UIA3Automation();
            var window = app.GetMainWindow(automation);
            
            //Step 2: Add contact
            var first_name = window.FindFirstDescendant(cf => cf.ByAutomationId("txtboxFirstName")).AsTextBox();
            first_name.Enter(FirstName);
            var last_name = window.FindFirstDescendant(cf => cf.ByAutomationId("txtboxLastName")).AsTextBox();
            last_name.Enter(LastName);
            var contact_no = window.FindFirstDescendant(cf => cf.ByAutomationId("txtBoxContactNumber")).AsTextBox();
            contact_no.Enter(ContactNo);
            var address = window.FindFirstDescendant(cf => cf.ByAutomationId("txtBoxAddress")).AsTextBox();
            address.Enter(Address);
            var comboBox = window.FindFirstDescendant(cf => cf.ByControlType(FlaUI.Core.Definitions.ControlType.ComboBox));
            var edit = comboBox.FindFirstDescendant(cf => cf.ByControlType(FlaUI.Core.Definitions.ControlType.Edit));
            edit.Focus();
            Keyboard.Type(Gender);
            Thread.Sleep(2000);
            var add_button = window.FindFirstDescendant(cf => cf.ByAutomationId("btnAdd")).AsButton();
            add_button.Click();
            Wait.UntilInputIsProcessed();
            Thread.Sleep(2000);
            var ok_button = window.FindFirstDescendant(cf => cf.ByAutomationId("2")).AsButton();
            ok_button.Click();
            Wait.UntilInputIsProcessed();
            Thread.Sleep(3000);

            //Step 3: Delete contact
            var searchBox = window.FindFirstDescendant(cf => cf.ByAutomationId("txtboxSearch")).AsTextBox();
            searchBox.Enter(FirstName);

            DataTable dt = econtactcls.Search(FirstName);
            var contact_id = window.FindFirstDescendant(cf => cf.ByAutomationId("txtboxContactID")).AsTextBox();
            contact_id.Enter(dt.Rows[0]["ContactID"].ToString());
            Thread.Sleep(2000);
            var delete_button = window.FindFirstDescendant(cf => cf.ByAutomationId("btnDelete")).AsButton();
            delete_button.Click();
            Wait.UntilInputIsProcessed();
            Thread.Sleep(2000);
            ok_button = window.FindFirstDescendant(cf => cf.ByAutomationId("2")).AsButton();
            ok_button.Click();
            Wait.UntilInputIsProcessed();
            Thread.Sleep(3000);

            //Step 4: Close the application
            app.Close();

        }

        [TestMethod]
        [DataRow("F", "Michell", "9875645", "Rutger", "Female")]
        public void Verify_UI_Clear_Click(String FirstName, String LastName, String ContactNo, String Address, String Gender)
        {
            //Step 1: Launch the application
            Econtactcls econtactcls = new Econtactcls();
            Application app = StartApplication();
            UIA3Automation automation = new UIA3Automation();
            var window = app.GetMainWindow(automation);
            
            //Step 2: Enter data in textbox
            var first_name = window.FindFirstDescendant(cf => cf.ByAutomationId("txtboxFirstName")).AsTextBox();
            first_name.Enter(FirstName);
            var last_name = window.FindFirstDescendant(cf => cf.ByAutomationId("txtboxLastName")).AsTextBox();
            last_name.Enter(LastName);
            var contact_no = window.FindFirstDescendant(cf => cf.ByAutomationId("txtBoxContactNumber")).AsTextBox();
            contact_no.Enter(ContactNo);
            var address = window.FindFirstDescendant(cf => cf.ByAutomationId("txtBoxAddress")).AsTextBox();
            address.Enter(Address);
            var comboBox = window.FindFirstDescendant(cf => cf.ByControlType(FlaUI.Core.Definitions.ControlType.ComboBox));
            var edit = comboBox.FindFirstDescendant(cf => cf.ByControlType(FlaUI.Core.Definitions.ControlType.Edit));
            edit.Focus();
            Keyboard.Type(Gender);
            Thread.Sleep(2000);
            
            //Step 3: Clear button click operation
            var clear_button = window.FindFirstDescendant(cf => cf.ByAutomationId("btnClear")).AsButton();
            clear_button.Click();
            Wait.UntilInputIsProcessed();
            Thread.Sleep(2000);

            //Step 4: Close the application
            app.Close();
            //Assert.AreEqual(0, app.ExitCode);

        }

        [TestMethod]
        [DataRow("Kiara", "Rao", "78648626", "Maldives", "Male")]
        public void Verify_UI_Contact(String FirstName, String LastName, String ContactNo, String Address, String Gender)
        {
            //Step 1: Launch the application
            Econtactcls econtactcls = new Econtactcls();
            Application app = StartApplication();
            UIA3Automation automation = new UIA3Automation();
            var window = app.GetMainWindow(automation);

            // Step 2: Add contact
            var first_name = window.FindFirstDescendant(cf => cf.ByAutomationId("txtboxFirstName")).AsTextBox();
            first_name.Enter(FirstName);
            var last_name = window.FindFirstDescendant(cf => cf.ByAutomationId("txtboxLastName")).AsTextBox();
            last_name.Enter(LastName);
            var contact_no = window.FindFirstDescendant(cf => cf.ByAutomationId("txtBoxContactNumber")).AsTextBox();
            contact_no.Enter(ContactNo);
            var address = window.FindFirstDescendant(cf => cf.ByAutomationId("txtBoxAddress")).AsTextBox();
            address.Enter(Address);
            var comboBox = window.FindFirstDescendant(cf => cf.ByControlType(FlaUI.Core.Definitions.ControlType.ComboBox));
            var edit = comboBox.FindFirstDescendant(cf => cf.ByControlType(FlaUI.Core.Definitions.ControlType.Edit));
            edit.Focus();
            Keyboard.Type(Gender);
            Thread.Sleep(2000);
            var add_button = window.FindFirstDescendant(cf => cf.ByAutomationId("btnAdd")).AsButton();
            add_button.Click();
            Wait.UntilInputIsProcessed();
            Thread.Sleep(2000);
            var ok_button = window.FindFirstDescendant(cf => cf.ByAutomationId("2")).AsButton();
            ok_button.Click();
            Wait.UntilInputIsProcessed();
            Thread.Sleep(3000);

            //Step 2: Fetch contact ID
            var searchBox = window.FindFirstDescendant(cf => cf.ByAutomationId("txtboxSearch")).AsTextBox();
            searchBox.Enter(FirstName);

            DataTable dt = econtactcls.Search(FirstName);
            var contact_id = window.FindFirstDescendant(cf => cf.ByAutomationId("txtboxContactID")).AsTextBox();
            contact_id.Enter(dt.Rows[0]["ContactID"].ToString());
            Thread.Sleep(2000);

            //Step 3: Update contact
            first_name.Enter(FirstName);
            last_name.Enter(LastName);
            contact_no.Enter(ContactNo);
            address.Enter(Address);
            edit.Focus();
            Gender = "Female";
            Keyboard.Type(Gender);
            Thread.Sleep(2000);
            var update_button = window.FindFirstDescendant(cf => cf.ByAutomationId("btnUpdate")).AsButton();
            update_button.Click();
            Wait.UntilInputIsProcessed();
            Thread.Sleep(2000);
            ok_button = window.FindFirstDescendant(cf => cf.ByAutomationId("2")).AsButton();
            ok_button.Click();
            Thread.Sleep(3000);

            searchBox.Enter("");

            searchBox.Enter(LastName);
            contact_id.Enter(dt.Rows[0]["ContactID"].ToString());

            //Step 4: Delete contact
            var delete_button = window.FindFirstDescendant(cf => cf.ByAutomationId("btnDelete")).AsButton();
            delete_button.Click();
            Thread.Sleep(2000);
            ok_button = window.FindFirstDescendant(cf => cf.ByAutomationId("2")).AsButton();
            ok_button.Click();
            Wait.UntilInputIsProcessed();
            Thread.Sleep(3000);

            searchBox.Enter("");

            //Step 5: Clear contact
            first_name.Enter(FirstName);
            last_name.Enter(LastName);
            contact_no.Enter(ContactNo);
            address.Enter(Address);
            edit.Focus();
            Gender = "Female";
            Keyboard.Type(Gender);
            Thread.Sleep(2000);
            var clear_button = window.FindFirstDescendant(cf => cf.ByAutomationId("btnClear")).AsButton();
            clear_button.Click();
            Wait.UntilInputIsProcessed();
            Thread.Sleep(2000);

            //Step 6: close the application
            app.Close();

        }

        [TestMethod]
        public void Verify_pictureBox_click()
        {
            //Step 1: Launch the application
            Econtactcls econtactcls = new Econtactcls();
            Application app = StartApplication();
            UIA3Automation automation = new UIA3Automation();
            var window = app.GetMainWindow(automation);
            
            Thread.Sleep(2000);

            var close_button = window.FindFirstDescendant(cf => cf.ByAutomationId("pictureBox1")).AsButton();
            close_button.Click();


        }

        protected Application StartApplication()
        {
            string executablePath = @"..\..\..\Econtact\bin\Debug\Econtact.exe";
            string fullPath = Path.GetFullPath(executablePath);
            return Application.Launch(fullPath);
        }



    }
}
