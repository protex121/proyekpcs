using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.DataAccess.Client;

namespace master_proyek
{
    public partial class formKasir : Form
    {
        public formKasir()
        {
            InitializeComponent();
        }

        OracleConnection oc;
        private void formKasir_Load(object sender, EventArgs e)
        {
            try
            {
                oc = new OracleConnection("user id= proyekpcs; password= proyekpcs; data source=orcl");
                oc.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal Karena "+ ex.Message);
            }
            String query = "SELECT * FROM MENU";
            OracleDataAdapter oda = new OracleDataAdapter(query, oc);
            DataTable dt = new DataTable();
            oda.Fill(dt);
            comboBox1.DataSource = dt;
            comboBox1.DisplayMember = "NAMA_MENU";
            comboBox1.ValueMember = "ID_MENU";
        }

        private void bunifuCustomDataGrid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == " " || textBox1.Text == " " || numericUpDown1.Value == 0)
            {
                MessageBox.Show("Fill All the Fields!");
            }
          
        }
    }
}
