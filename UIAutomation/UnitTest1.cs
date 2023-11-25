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

namespace UIAutomation
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Application app = StartApplication();
            UIA3Automation automation = new UIA3Automation();
            var window = app.GetMainWindow(automation);
            app.WaitWhileBusy();
            var first_name = window.FindFirstDescendant(cf => cf.ByAutomationId("txtboxFirstName")).AsTextBox();
            first_name.Enter("Pat");
            var last_name = window.FindFirstDescendant(cf => cf.ByAutomationId("txtboxLastName")).AsTextBox();
            last_name.Enter("Cummins");
            var contact_no = window.FindFirstDescendant(cf => cf.ByAutomationId("txtBoxContactNumber")).AsTextBox();
            contact_no.Enter("123456");
            var address = window.FindFirstDescendant(cf => cf.ByAutomationId("txtBoxAddress")).AsTextBox();
            address.Enter("Seattle");
            var comboBox = window.FindFirstDescendant(cf => cf.ByControlType(FlaUI.Core.Definitions.ControlType.ComboBox));
            var edit = comboBox.FindFirstDescendant(cf => cf.ByControlType(FlaUI.Core.Definitions.ControlType.Edit));
            edit.Focus();
            Keyboard.Type("Male");
            Thread.Sleep(3000);
            var add_button = window.FindFirstDescendant(cf => cf.ByAutomationId("btnAdd")).AsButton();
            add_button.Click();
            Wait.UntilInputIsProcessed();
            //app.Close();
            //Assert.AreEqual(0, app.ExitCode);

        }
        protected Application StartApplication()
        {
            return Application.Launch("C:\\Users\\atire\\OneDrive\\Documents\\ashwini\\Econtact\\Econtact\\bin\\Debug\\Econtact.exe");
        }



    }
}
