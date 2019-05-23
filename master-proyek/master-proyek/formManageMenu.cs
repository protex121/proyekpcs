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
    public partial class formManageMenu : Form
    {
        OracleConnection conn = new OracleConnection();
        List<string> id_menu = new List<string>();
        List<string> nama_menu = new List<string>();
        List<string> desc_menu = new List<string>();
        List<int> harga = new List<int>();
        string path;

        public formManageMenu()
        {
            InitializeComponent();
        }

        private void formManageMenu_Load(object sender, EventArgs e)
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

            load_menu();            
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            try
            {
                OracleCommand cmd = new OracleCommand("UPDATE MENU SET NAMA_MENU = '"+bunifuMaterialTextbox1.Text+"', DESKRIPSI_MENU = '"+bunifuMaterialTextbox2.Text+"' WHERE ID_MENU='"+comboBox1.SelectedItem.ToString()+"'", conn);
                cmd.ExecuteNonQuery();

                OracleCommand tcmd = new OracleCommand("UPDATE MENU_TENNANT SET HARGA_MENU = "+Convert.ToInt32(numericUpDown1.Value)+" WHERE ID_MENU = '"+comboBox1.SelectedItem.ToString()+"'", conn);
                tcmd.ExecuteNonQuery();

                bunifuFlatButton2_Click(sender, e);
                string newPath = Application.StartupPath+"\\menu";
                string destFile = Path.Combine(newPath, comboBox1.SelectedItem.ToString() + ".jpg");
                File.Copy(path, destFile, true);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            try
            {
                OracleCommand cmd = new OracleCommand("DELETE FROM MENU WHERE ID_MENU='" + comboBox1.SelectedItem.ToString() + "'", conn);
                cmd.ExecuteNonQuery();

                OracleCommand tcmd = new OracleCommand("DELETE FROM MENU_TENNANT WHERE ID_MENU = '" + comboBox1.SelectedItem.ToString() + "'", conn);
                tcmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void load_menu()
        {
            try
            {
                OracleDataAdapter cmd = new OracleDataAdapter("SELECT M.ID_MENU, MT.HARGA_MENU, M.NAMA_MENU,M.DESKRIPSI_MENU FROM MENU_TENNANT MT, MENU M WHERE MT.ID_MENU = M.ID_MENU AND ID_TENNANT = '"+FormTennant.id_tennant+"'", conn);
                DataTable dt = new DataTable();
                cmd.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    id_menu.Add(dt.Rows[i].ItemArray[0].ToString());
                    nama_menu.Add(dt.Rows[i].ItemArray[2].ToString());
                    desc_menu.Add(dt.Rows[i].ItemArray[3].ToString());
                    harga.Add(Convert.ToInt32(dt.Rows[i].ItemArray[1]));
                }

                for (int i = 0; i < id_menu.Count; i++)
                {
                    comboBox1.Items.Add(id_menu[i]);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void bunifuMaterialTextbox2_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            bunifuMaterialTextbox1.Text = nama_menu[comboBox1.SelectedIndex];
            bunifuMaterialTextbox2.Text = desc_menu[comboBox1.SelectedIndex];
            numericUpDown1.Value = harga[comboBox1.SelectedIndex];

            bunifuFlatButton3.Text = comboBox1.SelectedItem.ToString()+".jpg";
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
