using System;
using System.Data;
using System.Windows.Forms;

namespace EContact
{
    public partial class Econtact : Form
    {
        private readonly Database db = new Database();
        private readonly Person person = new Person();
        private DataTable dataTable;

        public Econtact()
        {
            InitializeComponent();
        }

        private void Econtact_Load(object sender, EventArgs e)
        {
            // Load Data on DataGridView
            dataTable = db.Select();
            DgvContacts.DataSource = dataTable;
        }

        private void LblExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            person.FirstName = TxtboxFirstName.Text;
            person.LastName = TxtboxLastName.Text;
            person.ContactNo = TxtboxContactNo.Text;
            person.Address = TxtboxAddress.Text;
            person.Gender = CmbGender.Text;

            bool isSuccess = db.Insert(person);
            if (isSuccess == true)
            {
                MessageBox.Show("New Contact Successfully Added");
                // Load Data on DataGridView
                dataTable = db.Select();
                DgvContacts.DataSource = dataTable;
                // Clear Fields
                Clear();
            }
            else
                MessageBox.Show("Failed to add New Contact. Try Again");

        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            // Get Data from the Fields
            person.ContactId = Convert.ToInt32(TxtboxContactID.Text);
            person.FirstName = TxtboxFirstName.Text;
            person.LastName = TxtboxLastName.Text;
            person.ContactNo = TxtboxContactNo.Text;
            person.Address = TxtboxAddress.Text;
            person.Gender = CmbGender.Text;

            bool isSuccess = db.Update(person);
            if (isSuccess == true)
            {
                MessageBox.Show("Contact Updated Successfully");
                // Load Data on DataGridView
                dataTable = db.Select();
                DgvContacts.DataSource = dataTable;
                // Clear Fields
                Clear();
            }
            else
                MessageBox.Show("Failed to Update Contact. Try Again");

        }

        private void BtnDeleteBtnDelete_Click(object sender, EventArgs e)
        {
            person.ContactId = Convert.ToInt32(TxtboxContactID.Text);
            bool isSuccess = db.Delete(person);
            if (isSuccess == true)
            {
                MessageBox.Show("Contact Deleted Successfully");
                // Load Data on DataGridView
                dataTable = db.Select();
                DgvContacts.DataSource = dataTable;
                // Clear Fields
                Clear();
            }
            else
                MessageBox.Show("Failed to Delete Contact. Try Again");
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void Clear()
        {
            TxtboxContactID.Text = "";
            TxtboxFirstName.Text = "";
            TxtboxLastName.Text = "";
            TxtboxContactNo.Text = "";
            TxtboxAddress.Text = "";
            CmbGender.Text = "";
        }

        private void DgvContacts_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // Get Data from DataGridView and Load it to the Fields
            // Identify rows which mouse is clicked
            int rowIndex = e.RowIndex;
            TxtboxContactID.Text = DgvContacts.Rows[rowIndex].Cells[0].Value.ToString();
            TxtboxFirstName.Text = DgvContacts.Rows[rowIndex].Cells[1].Value.ToString();
            TxtboxLastName.Text = DgvContacts.Rows[rowIndex].Cells[2].Value.ToString();
            TxtboxContactNo.Text = DgvContacts.Rows[rowIndex].Cells[3].Value.ToString();
            TxtboxAddress.Text = DgvContacts.Rows[rowIndex].Cells[4].Value.ToString();
            CmbGender.Text = DgvContacts.Rows[rowIndex].Cells[5].Value.ToString();
        }

        private void TxtboxSearch_TextChanged(object sender, EventArgs e)
        {
            string keyword = TxtboxSearch.Text;
            // Load Data on DataGridView
            dataTable = db.Search(keyword);
            DgvContacts.DataSource = dataTable;
        }

    }
}
