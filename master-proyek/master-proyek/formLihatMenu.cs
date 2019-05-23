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
using System.IO;


namespace master_proyek
{
    public partial class formLihatMenu : Form
    {
        OracleConnection conn = new OracleConnection();
        string path = Application.StartupPath + "\\menu";
        public static string temp_id = "";
        int row;

        public formLihatMenu()
        {
            InitializeComponent();
        }

        private void formLihatMenu_Load(object sender, EventArgs e)
        {
            bunifuCustomDataGrid1.AllowUserToAddRows = false;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            try
            {
                conn = new OracleConnection("user id=proyekpcs;password=proyekpcs;data source=orcl");
                conn.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

            refresh();
            pictureBox1.Image = Image.FromFile(path + "\\null.jpg");
        }

        public void refresh()
        {
            string query = " SELECT M.ID_MENU,M.NAMA_MENU, M.DESKRIPSI_MENU, MT.HARGA_MENU FROM MENU M, MENU_TENNANT MT WHERE M.ID_MENU = MT.ID_MENU AND MT.ID_TENNANT = '"+temp_id+"'";
            OracleDataAdapter oda = new OracleDataAdapter(query, conn);
            DataTable dt = new DataTable();
            oda.Fill(dt);
            bunifuCustomDataGrid1.DataSource = dt;

            bunifuCustomDataGrid1.Columns[0].HeaderText = "ID MENU";
            bunifuCustomDataGrid1.Columns[1].HeaderText = "NAMA MENU";
            bunifuCustomDataGrid1.Columns[2].HeaderText = "DESKRIPSI MENU";
            bunifuCustomDataGrid1.Columns[3].HeaderText = "HARGA MENU";

        }

        private void bunifuCustomDataGrid1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            row = e.RowIndex;

            if (row > -1)
            {
                String id = bunifuCustomDataGrid1[0, row].Value.ToString();
                if (File.Exists(path + "\\" + id + ".jpg"))
                {
                    pictureBox1.Image = Image.FromFile(path + "\\" + id + ".jpg");
                }
                else
                {
                    pictureBox1.Image = Image.FromFile(path + "\\null.jpg");
                }
            }
        }
    }
}
