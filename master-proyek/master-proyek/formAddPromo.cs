using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace master_proyek
{
    public partial class formAddPromo : Form
    {
        OracleConnection conn = new OracleConnection();

        public formAddPromo()
        {
            InitializeComponent();
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            string judul_promo = bunifuMaterialTextbox2.Text;
            string[] temp = judul_promo.Split(' ');
            string temp_id = "";

            if (temp.Length > 1)
            {
                temp_id += temp[0].Substring(0, 1).ToUpper() + temp[1].Substring(0, 1).ToUpper();
            }
            else
            {
                temp_id += temp[0].Substring(0, 2).ToUpper();
            }

            OracleDataAdapter t_cmd = new OracleDataAdapter("SELECT COUNT(ID_PROMO) FROM PROMO WHERE ID_PROMO LIKE '%"+temp_id+"%'", conn);
            DataTable dt = new DataTable();
            t_cmd.Fill(dt);

            int temp_jml = Convert.ToInt32(dt.Rows[0].ItemArray[0]);

            if (temp_jml < 10)
            {
                temp_id += "00" + temp_jml;
            }
            else if (temp_jml < 100)
            {
                temp_id += "0" + temp_jml;
            }
            else
            {
                temp_id += temp_jml;
            }

            OracleCommand cmd = new OracleCommand("INSERT INTO PROMO VALUES('"+temp_id+"','"+judul_promo+"',"+Convert.ToInt32(bunifuMaterialTextbox3.Text)+")", conn);
            cmd.ExecuteNonQuery();
        }

        private void formAddPromo_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;

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
    }
}
