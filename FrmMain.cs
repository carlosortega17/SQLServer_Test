using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace TestDatabase
{
    public partial class FrmMain : Form
    {
        private SqlConnection connection;

        private SqlConnection GetConnection()
        {
            var connection = new SqlConnection("Data Source=ATROX\\SQLEXPRESS;Initial Catalog=Test;Integrated Security=True");
            connection.Open();
            return connection;
        }

        private void NonQuery(string query)
        {
            var command = connection.CreateCommand();
            command.CommandText = query;
            command.ExecuteNonQuery();
        }

        private SqlDataReader Query(string query)
        {
            var command = connection.CreateCommand();
            command.CommandText = query;
            return command.ExecuteReader();
        }

        private SqlDataAdapter QueryAdapter(string query)
        {
            var command = connection.CreateCommand();
            command.CommandText = query;
            return new SqlDataAdapter(command);
        }

        public FrmMain()
        {
            InitializeComponent();
            connection = GetConnection();
            var adapter = QueryAdapter("SELECT * FROM dbo.Personas");

            DataSet data = new DataSet();
            adapter.Fill(data);
            dataGridView1.DataSource = data.Tables[0];
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            MessageBox.Show(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
        }
    }
}
