 using System;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;
using System.Windows.Forms;

namespace EContact
{
    class Database
    {

        //Step 1. Create Database Connection
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;
        private readonly MySqlConnection conn = new MySqlConnection(connectionString);
        private MySqlCommand cmd;
        private MySqlDataAdapter adapter;
        private DataTable dataTable;

        /// <summary>
        /// Selecting Data from Database
        /// </summary>
        /// <returns> dataTable </returns>
        public DataTable Select()
        {
            dataTable = new DataTable();
            try
            {
                //Step 2. Create Query
                string query = "SELECT * FROM contact";
                cmd = new MySqlCommand(query, conn);
                adapter = new MySqlDataAdapter(cmd);
                conn.Open();
                adapter.Fill(dataTable);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Database Connection Error");
            }
            finally
            {
                conn.Close();
            }
            return dataTable;
        }

        /// <summary>
        /// Search Data on Database
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns> dataTable </returns>
        public DataTable Search(string keyword)
        {
            dataTable = new DataTable();
            try
            {
                //Step 2. Create Query
                string query = "SELECT * FROM contact WHERE FirstName LIKE '%" + keyword + "%' OR LastName LIKE '%" + keyword + "%' OR ContactNo LIKE '%" + keyword + "%' OR Address LIKE '%" + keyword + "%'";
                cmd = new MySqlCommand(query, conn);
                adapter = new MySqlDataAdapter(cmd);
                conn.Open();
                adapter.Fill(dataTable);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Search Error");
            }
            finally
            {
                conn.Close();
            }
            return dataTable;
        }

        /// <summary>
        /// Inserting Data into Database
        /// </summary>
        /// <param name="person"></param>
        /// <returns> isSuccess </returns>
        public bool Insert(Person person)
        {
            bool isSuccess = false;

            try
            {
                // Query to insert Data
                string query = "INSERT INTO contact(FirstName, LastName, ContactNo, Address, Gender) VALUES (@FirstName, @LastName, @ContactNo, @Address, @Gender)";
                cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@FirstName", person.FirstName);
                cmd.Parameters.AddWithValue("@LastName", person.LastName);
                cmd.Parameters.AddWithValue("@ContactNo", person.ContactNo);
                cmd.Parameters.AddWithValue("@Address", person.Address);
                cmd.Parameters.AddWithValue("@Gender", person.Gender);

                // Connection Open
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                // If Query is successful
                if (rows > 0)
                    isSuccess = true;
                else
                    isSuccess = false;

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Inserting Data Error");
            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }

        /// <summary>
        /// Update Data into Database
        /// </summary>
        /// <param name="person"></param>
        /// <returns> isSuccess </returns>
        public bool Update(Person person)
        {
            bool isSuccess = false;

            try
            {
                // Query to insert Data
                string query = "UPDATE contact SET FirstName=@FirstName, LastName=@LastName, ContactNo=@ContactNo, Address=@Address, Gender=@Gender WHERE id=@ContactId";
                cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@FirstName", person.FirstName);
                cmd.Parameters.AddWithValue("@LastName", person.LastName);
                cmd.Parameters.AddWithValue("@ContactNo", person.ContactNo);
                cmd.Parameters.AddWithValue("@Address", person.Address);
                cmd.Parameters.AddWithValue("@Gender", person.Gender);
                cmd.Parameters.AddWithValue("@ContactId", person.ContactId);

                // Connection Open
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                // If Query is successful
                if (rows > 0)
                    isSuccess = true;
                else
                    isSuccess = false;

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Updating Data Error");
            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }

        /// <summary>
        /// Delete Data from Database
        /// </summary>
        /// <param name="person"></param>
        /// <returns> isSuccess </returns>
        public bool Delete(Person person)
        {
            bool isSuccess = false;

            try
            {
                // Query to insert Data
                string query = "DELETE FROM contact WHERE id=@ContactId";
                cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ContactId", person.ContactId);

                // Connection Open
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                // If Query is successful
                if (rows > 0)
                    isSuccess = true;
                else
                    isSuccess = false;

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Deleting Data Error");
            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }

    }
}
