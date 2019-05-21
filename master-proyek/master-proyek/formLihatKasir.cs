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
    public partial class formLihatKasir : Form
    {
        OracleConnection conn = new OracleConnection();
        string path = "D:\\Materi Kuliah\\Semester 4\\PCS\\proyek\\proyekpcs\\master-proyek\\master-proyek\\bin\\Debug\\pp";
        int row;

        public formLihatKasir()
        {
            InitializeComponent();
        }

        private void formLihatKasir_Load(object sender, EventArgs e)
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
            string query = "SELECT id_pegawai,nama_pegawai,to_char(tgllahir_pegawai,'DD/MM/YYYY'),nohp_pegawai,cabang_pegawai FROM PEGAWAI WHERE JABATAN_PEGAWAI='K2'";
            OracleDataAdapter oda = new OracleDataAdapter(query, conn);
            DataTable dt = new DataTable();
            oda.Fill(dt);
            bunifuCustomDataGrid1.DataSource = dt;

            bunifuCustomDataGrid1.Columns[0].HeaderText = "ID PEGAWAI";
            bunifuCustomDataGrid1.Columns[1].HeaderText = "NAMA PEGAWAI";
            bunifuCustomDataGrid1.Columns[2].HeaderText = "TGL LAHIR";
            bunifuCustomDataGrid1.Columns[3].HeaderText = "NOMOR TELP";
            bunifuCustomDataGrid1.Columns[4].HeaderText = "CABANG";
           
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

        //btn delete
        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            string id = bunifuCustomDataGrid1[0, row].Value.ToString();
            //File.Delete(@"D:\Materi Kuliah\Semester 4\PCS\proyek\proyekpcs\master-proyek\master-proyek\bin\Debug\pp\" + id + ".jpg");
            string query = "DELETE FROM pegawai WHERE id_pegawai='" + id + "'";
            OracleCommand cmd = new OracleCommand(query, conn);
            cmd.ExecuteNonQuery();        
            refresh();
            MessageBox.Show("Delete Data Berhasil!");
        }

    }
}
