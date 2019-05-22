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
    public partial class formManagePromo : Form
    {
        OracleConnection conn = new OracleConnection();
        List<string> id_promo = new List<string>();
        List<string> judul_promo = new List<string>();
        List<int> nominal_promo = new List<int>();

        public formManagePromo()
        {
            InitializeComponent();
        }

        private void formManagePromo_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;

            try
            {
                conn = new OracleConnection("user id=zamorano;password=zamorano;data source=zamorano");
                conn.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

            load_promo();
        }

        private void load_promo()
        {
            try
            {
                OracleDataAdapter cmd = new OracleDataAdapter("SELECT ID_PROMO, TITLE_PROMO, JML_PROMO FROM PROMO", conn);
                DataTable dt = new DataTable();
                cmd.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    id_promo.Add(dt.Rows[i].ItemArray[0].ToString());
                    judul_promo.Add(dt.Rows[i].ItemArray[1].ToString());
                    nominal_promo.Add(Convert.ToInt32(dt.Rows[i].ItemArray[2].ToString()));
                }

                for (int i = 0; i < id_promo.Count; i++)
                {
                    comboBox1.Items.Add(id_promo[i]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            bunifuMaterialTextbox1.Text = judul_promo[comboBox1.SelectedIndex];
            bunifuMaterialTextbox2.Text = nominal_promo[comboBox1.SelectedIndex].ToString();
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            try
            {
                OracleCommand cmd = new OracleCommand("UPDATE PROMO SET TITLE_PROMO = '"+bunifuMaterialTextbox1.Text+"', JML_PROMO="+Convert.ToInt32(bunifuMaterialTextbox2.Text)+" WHERE ID_PROMO = '"+comboBox1.SelectedItem.ToString()+"'", conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            try
            {
                OracleCommand cmd = new OracleCommand("DELETE FROM PROMO WHERE ID_PROMO = '" + comboBox1.SelectedItem.ToString() + "'", conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
