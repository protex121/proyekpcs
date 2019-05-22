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
    public partial class formAddMenu : Form
    {
        OracleConnection conn = new OracleConnection();
        string id_cabang = "";
        string path;

        public formAddMenu()
        {
            InitializeComponent();
        }

        private void formAddMenu_Load(object sender, EventArgs e)
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

            

        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            bunifuMaterialTextbox1.Text = "";
            bunifuMaterialTextbox2.Text = "";
            numericUpDown1.Value = 1000;
            bunifuMaterialTextbox4.Text = "";
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            string id = FormTennant.id_tennant;
            string nama = FormTennant.username;
            string nama_menu = bunifuMaterialTextbox1.Text;
            string[] menu = nama_menu.Split(' ');
            string temp_id = "";
            string desc_menu = bunifuMaterialTextbox4.Text;
            int harga = Convert.ToInt32(numericUpDown1.Value);

            if (menu.Length > 1)
            {
                temp_id = menu[0].Substring(0, 2).ToUpper();
            }
            else
            {
                temp_id = menu[0].Substring(0, 1).ToUpper() + menu[1].Substring(0, 1).ToUpper();
            }

            OracleDataAdapter cmd = new OracleDataAdapter("SELECT ID_CABANG FROM TENNANT WHERE ID_TENNANT WHERE ID_TENNANT = '"+id+"'", conn);
            DataTable dt = new DataTable();
            cmd.Fill(dt);

            OracleDataAdapter tmp = new OracleDataAdapter("SELECT COUNT(ID_MENU) FROM MENU WHERE ID_MENU LIKE '" + temp_id + "'", conn);
            DataTable dtt = new DataTable();
            tmp.Fill(dtt);

            int jumlah = Convert.ToInt32(dtt.Rows[0].ItemArray[0])+1;

            if (jumlah < 10)
            {
                temp_id += "00" + jumlah;
            }
            else if (jumlah < 100)
            {
                temp_id += "0" + jumlah;
            }
            else
            {
                temp_id += jumlah;
            }

            id_cabang = dt.Rows[0].ItemArray[0].ToString();

            OracleCommand tcmd = new OracleCommand("INSERT INTO MENU VALUES ('"+temp_id+"','"+nama_menu+"','"+desc_menu+"')", conn);
            tcmd.ExecuteNonQuery();

            OracleCommand tmp_cmd = new OracleCommand("INSERT INTO MENU_TENNANT VALUES ('"+temp_id+"',''"+id+"',"+harga+")", conn);
            tmp_cmd.ExecuteNonQuery();

            bunifuFlatButton2_Click(sender, e);
            string newPath = Application.StartupPath+"\\pp";
            string destFile = Path.Combine(newPath, temp_id + ".jpg");
            File.Copy(path, destFile, true);
        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Title = "Browse Picture Files";
            openFileDialog1.Filter = "Picture files (*.jpg)|*.jpg|Picture files (*.png)|*.png|All files (*.*)|*.*";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                bunifuFlatButton3.Text = "  " + Path.GetFileName(openFileDialog1.FileName);
                path = openFileDialog1.FileName;
            }
        }
    }
}
