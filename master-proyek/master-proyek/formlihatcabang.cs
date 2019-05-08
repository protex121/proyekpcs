using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace master_proyek
{
    public partial class formlihatcabang : Form
    {
        OracleConnection conn = new OracleConnection();
        DataTable dtcabang = new DataTable();

        public formlihatcabang()
        {
            InitializeComponent();
        }

        private void formlihatcabang_Load(object sender, EventArgs e)
        {
            try
            {
                conn = new OracleConnection("user id=proyekpcs;password=proyekpcs;data source=orcl");
                conn.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            refresh_data();
        }

        private void refresh_data() {
            string query = "SELECT * FROM cabang";
            OracleDataAdapter oda = new OracleDataAdapter(query, conn);
            DataTable dt = new DataTable();
            oda.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        

    }
}
