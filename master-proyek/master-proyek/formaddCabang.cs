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
    public partial class formaddCabang : Form
    {
        OracleConnection conn = new OracleConnection();

        public formaddCabang()
        {
            InitializeComponent();
        }

        private void formaddCabang_Load(object sender, EventArgs e)
        {
            bunifuMaterialTextbox1.Enabled = false;
            
            try
            {
                conn = new OracleConnection("user id=proyekpcs;password=proyekpcs;data source=orcl");
                conn.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        //btn add
        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            string nama_cabang = bunifuMaterialTextbox2.Text;
            string alamat = bunifuMaterialTextbox3.Text;
            string no = bunifuMaterialTextbox4.Text;

            string query = "SELECT AUTO_GEN_ID_CABANG('" + nama_cabang + "') FROM DUAL";
            OracleCommand cmd = new OracleCommand(query, conn);
            string id = cmd.ExecuteScalar().ToString();

            query = "INSERT INTO CABANG VALUES('" + id + "','" + nama_cabang.ToUpper() + "','" + alamat.ToUpper() + "','" + no + "')";
            cmd = new OracleCommand(query, conn);
            cmd.ExecuteScalar();
            MessageBox.Show("INSERT BERHASIL!");

            bunifuFlatButton2_Click(sender, e);
        }

        //btn reset
        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            bunifuMaterialTextbox1.Text = "X";
            bunifuMaterialTextbox2.Text = "";
            bunifuMaterialTextbox3.Text = "";
            bunifuMaterialTextbox4.Text = "";
        }

        

    }
}
