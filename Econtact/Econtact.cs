using Econtact.econtactClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Econtact
{
    public partial class Econtactcls : Form
    {
        static string connectionString; //ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;
        // Create an instance of SqlDatabase, passing the connection string
        static IDatabase database; //= new SqlDatabase(connectionString);

        // Create an instance of contactClass, passing the database instance
        contactClass c;// = new contactClass(database);

        public Econtactcls()
        {
            connectionString = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;
            database = new SqlDatabase(connectionString);
            c = new contactClass(database);
            InitializeComponent();

        }
        public Econtactcls(contactClass c)
        {
            this.c = c;
            InitializeComponent();
        }
        public void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            add_Contact();
        }

        public void add_Contact()
        {
            //Get the value from the input fields
            if (!string.IsNullOrEmpty(txtboxFirstName.Text) && !txtboxFirstName.Text.Any(char.IsDigit))
            {
                c.FirstName = txtboxFirstName.Text;
            }
            else
            {
               throw new Exception("Error: Invalid FirstName value");
            }
            if(!string.IsNullOrEmpty(txtboxLastName.Text) && !txtboxLastName.Text.Any(char.IsDigit))
            {
                c.LastName = txtboxLastName.Text;
            }
            else
            {
               throw new Exception("Error: Invalid LastName value");
            }
            if (!string.IsNullOrEmpty(txtBoxContactNumber.Text))
            {
                c.ContactNo = int.Parse(txtBoxContactNumber.Text);
            }
            else
            {
               throw new Exception("Error: Invalid ContactNumer value");
            }
            if (!string.IsNullOrEmpty(txtBoxAddress.Text))
            {
                c.Address = txtBoxAddress.Text;
            }
            else
            {
               throw new Exception("Error: Invalid Address value");
            }
            if (!string.IsNullOrEmpty(cmbGender.Text) && (cmbGender.Text == "Male" ||
                 cmbGender.Text == "Female"))
            {
                c.Gender = cmbGender.Text;
            }
            else
            {
               throw new Exception("Error: Invalid Gender value");
            }

            //Inserting Data into DAtabase uing the method we created in previous episode
            bool success = c.Insert_Database(c);
            if(success==true)
            {
                //Successfully Inserted
               MessageBox.Show("New Contact Successfully Inserted");
                //Call the Clear Method Here
                Clear();
            }
            else
            {
                //FAiled to Add Contact
                MessageBox.Show("Add contact failed");
                Console.WriteLine("Error: Failed to add New Contact. Try Again.");
            }
            //Load Data on Data GRidview
            DataTable dt = c.Select();
            dgvContactList.DataSource = dt;
        }

        public void Econtact_Load(object sender, EventArgs e)
        {
            //Load Data on Data GRidview
            DataTable dt = c.Select();
            dgvContactList.DataSource = dt;
        }

        public void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //Method to Clear Fields
        public void Clear()
        {
            txtboxFirstName.Text = "";
            txtboxLastName.Text = "";
            txtBoxContactNumber.Text = "";
            txtBoxAddress.Text = "";
            cmbGender.Text = "";
            txtboxContactID.Text = "";
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            update_Contact();
        }
        public void update_Contact()
        {
            //Get the Data from textboxes
            c.ContactID = int.Parse(txtboxContactID.Text);
            //Get the value from the input fields
            if (!string.IsNullOrEmpty(txtboxFirstName.Text) && !txtboxFirstName.Text.Any(char.IsDigit))
            {
                c.FirstName = txtboxFirstName.Text;
            }
            else
            {
               throw new Exception("Error: Invalid FirstName value");
            }
            if (!string.IsNullOrEmpty(txtboxLastName.Text) && !txtboxLastName.Text.Any(char.IsDigit))
            {
                c.LastName = txtboxLastName.Text;
            }
            else
            {
               throw new Exception("Error: Invalid LastName value");
            }
            if (!string.IsNullOrEmpty(txtBoxContactNumber.Text))
            {
                c.ContactNo = int.Parse(txtBoxContactNumber.Text);
            }
            else
            {
                throw new Exception("Error: Invalid ContactNumer value");
            }
            if (!string.IsNullOrEmpty(txtBoxAddress.Text))
            {
                c.Address = txtBoxAddress.Text;
            }
            else
            {
                throw new Exception("Error: Invalid Address value");
            }
            if (!string.IsNullOrEmpty(cmbGender.Text) && (cmbGender.Text == "Male" ||
                 cmbGender.Text == "Female"))
            {
                c.Gender = cmbGender.Text;
            }
            else
            {
               throw new Exception("Error: Invalid Gender value");
            }
            //Update DAta in Database
            bool success = c.Update_Database(c);
            if(success==true)
            {
                //Updated Successfully
               MessageBox.Show("Contact has been successfully Updated.");
                //Load Data on Data GRidview
                DataTable dt = c.Select();
                dgvContactList.DataSource = dt;
                //Call Clear Method
                Clear();
            }
            else
            {
                //Failed to Update
                MessageBox.Show("Update Contact Failed");
               Console.WriteLine("Error: Failed to Update Contact.Try Again.");
            }
        }

        public void dgvContactList_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //Get the DAta From DAta Grid View and Load it to the textboxes respectively
            //identify the row on which mouse is clicked
            int rowIndex = e.RowIndex;
            txtboxContactID.Text = dgvContactList.Rows[rowIndex].Cells[0].Value.ToString();
            txtboxFirstName.Text = dgvContactList.Rows[rowIndex].Cells[1].Value.ToString();
            txtboxLastName.Text = dgvContactList.Rows[rowIndex].Cells[2].Value.ToString();
            txtBoxContactNumber.Text = dgvContactList.Rows[rowIndex].Cells[3].Value.ToString();
            txtBoxAddress.Text = dgvContactList.Rows[rowIndex].Cells[4].Value.ToString();
            cmbGender.Text = dgvContactList.Rows[rowIndex].Cells[5].Value.ToString();
        }

        public void btnClear_Click(object sender, EventArgs e)
        {
            //Call Clear Method Here
            Clear();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            delete_Contact();
        }

        public void delete_Contact()
        {
            //Get the Contact ID fromt eh Application
            c.ContactID = Convert.ToInt32(txtboxContactID.Text);
            bool success = c.Delete_from_database(c);
            if(success==true)
            {
                //Successfully Deleted
               MessageBox.Show("Contact successfully deleted.");
                //Refresh Data GridView
                //Load Data on Data GRidview
                DataTable dt = c.Select();
                dgvContactList.DataSource = dt;
                //CAll the Clear Method Here
                Clear();
            }
            else
            {
                //FAiled to dElte
                MessageBox.Show("Delete contact failed");
               Console.WriteLine("Failed to Delete Contact. Try Again.");
            }
        }
        public void txtboxSearch_TextChanged(object sender, EventArgs e)
        {
            //Get teh value from text box
            string keyword = txtboxSearch.Text;
            Search(keyword);
            
        }

        public DataTable Search(string keyword)
        {
            string sql = "SELECT * FROM tbl_contact WHERE FirstName LIKE '%" + keyword + "%' OR LastName LIKE '%" + keyword + "%' OR Address LIKE '%" + keyword + "%'";
            DataTable dt = new DataTable();
            dt = database.ExecuteQuery(sql, null);
            dgvContactList.DataSource = dt;
            return dt;
        }
    }
}
