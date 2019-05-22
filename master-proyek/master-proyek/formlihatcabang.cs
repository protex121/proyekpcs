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
            bunifuCustomDataGrid1.AllowUserToAddRows = false;
            textBox2.Enabled = false;
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

            comboBox1.Items.Add("ID");
            comboBox1.Items.Add("NAMA CABANG");
            comboBox1.SelectedIndex = 0;

            ToolTip toolTip1 = new ToolTip();
            toolTip1.AutoPopDelay = 1000;
            toolTip1.InitialDelay = 1000;
            toolTip1.ReshowDelay = 500;
            toolTip1.ShowAlways = true;
            toolTip1.SetToolTip(this.btnCancel, "Refresh");
        }

        private void refresh_data() {
            string query = "SELECT * FROM cabang";
            OracleDataAdapter oda = new OracleDataAdapter(query, conn);
            DataTable dt = new DataTable();
            oda.Fill(dt);
            bunifuCustomDataGrid1.DataSource = dt;

            bunifuCustomDataGrid1.Columns[0].HeaderText="ID CABANG";
            bunifuCustomDataGrid1.Columns[1].HeaderText = "NAMA CABANG";
            bunifuCustomDataGrid1.Columns[2].HeaderText = "ALAMAT";
            bunifuCustomDataGrid1.Columns[3].HeaderText = "NOMOR TELP";
            
            bunifuCustomDataGrid1.ColumnHeadersHeight = 100;
            bunifuCustomDataGrid1.RowHeadersVisible = false;           
        }

        //ketik di tempat search
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //masih salah
            string key = textBox1.Text.ToUpper();
            string query = "SELECT * FROM CABANG WHERE upper(id_cabang)='"+key+"' OR upper(nama_cabang)='"+key+"'";

            OracleDataAdapter oda = new OracleDataAdapter(query, conn);
            DataTable dt = new DataTable();
            oda.Fill(dt);
            bunifuCustomDataGrid1.DataSource = dt;

            bunifuCustomDataGrid1.Columns[0].HeaderText = "ID CABANG";
            bunifuCustomDataGrid1.Columns[1].HeaderText = "NAMA CABANG";
            bunifuCustomDataGrid1.Columns[2].HeaderText = "ALAMAT";
            bunifuCustomDataGrid1.Columns[3].HeaderText = "NOMOR TELP";

            bunifuCustomDataGrid1.ColumnHeadersHeight = 100;
            bunifuCustomDataGrid1.RowHeadersVisible = false;


        }

        
        //btn update
        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            string id = textBox2.Text;
            string nama_cabang = textBox3.Text.ToUpper();
            string alamat = textBox4.Text.ToUpper();
            string no = textBox5.Text;

            string query = "UPDATE cabang SET nama_cabang='"+nama_cabang+"',alamat_cabang='"+alamat+"',telp_cabang='"+no+"' WHERE id_cabang='"+id+"'";
            OracleCommand cmd = new OracleCommand(query, conn);
            cmd.ExecuteNonQuery();
            refresh_data();
        }

        //btn delete
        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            string id = textBox2.Text;
            
            string query = "DELETE FROM cabang WHERE id_cabang='"+id+"'";
            OracleCommand cmd = new OracleCommand(query, conn);
            cmd.ExecuteNonQuery();
            refresh_data();
        }

        //btn refresh
        private void btnCancel_Click(object sender, EventArgs e)
        {
            refresh_data();
        }

        private void bunifuCustomDataGrid1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int row = e.RowIndex;
            if (row > -1) {
                String id_cabang = bunifuCustomDataGrid1[0, row].Value.ToString();
                String nama_cabang = bunifuCustomDataGrid1[1, row].Value.ToString();
                String alamat = bunifuCustomDataGrid1[2, row].Value.ToString();
                String no_telp = bunifuCustomDataGrid1[3, row].Value.ToString();

                textBox2.Text = id_cabang;
                textBox3.Text = nama_cabang;
                textBox4.Text = alamat;
                textBox5.Text = no_telp;
            }
            
        }


    }
}
