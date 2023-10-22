using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Econtact;
using System.Configuration;
using Moq;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            
            //Program program = new Program();
            Program.print();

        }
        [TestMethod]
        public void Validate_Add_Contact()
        {
           
            Econtactcls econtactcls = new Econtactcls();
            econtactcls.txtboxFirstName.Text = "Test";
            econtactcls.txtboxLastName.Text = "Done";
            econtactcls.txtBoxContactNumber.Text = "1234567890";
            econtactcls.txtBoxAddress.Text = "Seattle";
            econtactcls.cmbGender.Text = "Male";
            
            //econtactcls.txtboxContactID.Text = "";
            try
            {
                econtactcls.add_Contact();
            }
            catch (NullReferenceException ex)
            {
                Assert.Fail($"NullReferenceException was thrown: {ex.Message}");
            }

        }
    }
   
    
}
