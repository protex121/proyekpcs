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
    public partial class formAddKasir : Form
    {
        OracleConnection conn = new OracleConnection();
        string path;
        public formAddKasir()
        {
            InitializeComponent();
        }

        private void formAddKasir_Load(object sender, EventArgs e)
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

            String query = "SELECT * FROM CABANG";
            OracleDataAdapter oda = new OracleDataAdapter(query, conn);
            DataTable dt = new DataTable();
            oda.Fill(dt);            
            comboBox1.ValueMember = "id_cabang";
            comboBox1.DisplayMember = "nama_cabang";
            comboBox1.DataSource = dt;

        }

        //btn add
        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            string nama = bunifuMaterialTextbox2.Text;
            string pass = bunifuMaterialTextbox3.Text;
            string tgl = bunifuDatepicker1.Value.ToString("dd/MM/yyyy HH:mm:ss");
            string no = bunifuMaterialTextbox5.Text;
            string cabang = comboBox1.SelectedValue + "";

            string id = "SELECT AUTO_GEN_ID_KASIR('" + nama.ToUpper() + "') FROM DUAL";
            OracleCommand cmd = new OracleCommand(id, conn);
            id = cmd.ExecuteScalar().ToString();

            MessageBox.Show(tgl);
            string query = "INSERT INTO PEGAWAI VALUES('" + id + "','" + nama.ToUpper() + "','" + pass + "',to_date('" + tgl + "','DD/MM/YYYY HH24:Mi:SS'),'" + no + "','K2','" + cabang + "')";
            cmd = new OracleCommand(query, conn);
            cmd.ExecuteNonQuery();

            MessageBox.Show("Insert Berhasil!");

            bunifuFlatButton2_Click(sender,e);
            string newPath = Application.StartupPath+"\\pp";
            string destFile = Path.Combine(newPath, id + ".jpg");
            File.Copy(path, destFile, true);

            
        }

        //btn reset
        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            bunifuMaterialTextbox2.Text = "";
            bunifuMaterialTextbox3.Text = "";
            bunifuDatepicker1.Value = DateTime.Now;
            bunifuMaterialTextbox5.Text = "";
            comboBox1.SelectedIndex = 0;
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
