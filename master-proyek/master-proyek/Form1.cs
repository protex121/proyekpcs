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
    public partial class Form1 : Form
    {
        int detik = 0;
        string idkasir;
        OracleConnection conn = new OracleConnection();
        List<string> id_tennant = new List<string>();
        List<string> pass_tennant = new List<string>();
        List<string> id_kasir = new List<string>();
        List<string> pass_kasir = new List<string>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            this.FormBorderStyle = FormBorderStyle.None;
            timer1.Enabled = true;

            try
            {
                conn = new OracleConnection("user id=proyekpcs;password=proyekpcs;data source=orcl");
                conn.Open();

                load_tennant();
                load_kasir();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        //load id dan password tennant
        private void load_tennant()
        {
            OracleDataAdapter cmd = new OracleDataAdapter("SELECT ID_TENNANT, NAMA_TENNANT FROM TENNANT", conn);
            DataTable dt = new DataTable();
            cmd.Fill(dt);

            for(int i = 0; i < dt.Rows.Count; i++)
            {
                id_tennant.Add(dt.Rows[i].ItemArray[1].ToString());
                pass_tennant.Add(dt.Rows[i].ItemArray[0].ToString());
            }
        }

        //load id dan password kasir
        private void load_kasir()
        {
            OracleDataAdapter cmd = new OracleDataAdapter("SELECT ID_PEGAWAI, PASS_PEGAWAI FROM PEGAWAI WHERE JABATAN_PEGAWAI='K2'", conn);
            DataTable dt = new DataTable();
            cmd.Fill(dt);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                id_kasir.Add(dt.Rows[i].ItemArray[0].ToString());
                pass_kasir.Add(dt.Rows[i].ItemArray[1].ToString());
            }
        }

        //button login
        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            string username = bunifuMaterialTextbox1.Text;
            string password = bunifuMaterialTextbox2.Text;

            bool tennant = false;
            bool kasir = false;

            if (username == "admin" && password == "admin")
            {
                formadminfc fc = new formadminfc();
                this.Hide();
                fc.ShowDialog();
                this.Close();
            }

            for(int i = 0; i < id_tennant.Count; i++)
            {
                if(id_tennant[i] == username  && pass_tennant[i] == password)
                {
                    tennant = true;
                    i = id_tennant.Count + 10;
                }
            }

            for (int i = 0; i < id_kasir.Count; i++)
            {
                if (id_kasir[i] == username && pass_kasir[i] == password)
                {
                    kasir = true;
                    i = id_kasir.Count + 10;
                    idkasir = username;
                }
            }

            if (tennant == true)
            {
                FormTennant ft = new FormTennant();
                FormTennant.id_tennant = password;
                FormTennant.username = username;
                this.Hide();
                ft.ShowDialog();
                this.Close();
            }
            else if(kasir == true)
            {
                formKasir ft = new formKasir();
                ft.idkasir = idkasir;
                String cekcab = "SELECT CABANG_PEGAWAI FROM PEGAWAI WHERE ID_PEGAWAI='" + idkasir + "'";
                OracleCommand cmd = new OracleCommand(cekcab,conn);
                //MessageBox.Show(cmd.ExecuteScalar().ToString());
                ft.idcabang = cmd.ExecuteScalar().ToString();
                this.Hide();
                ft.ShowDialog();
                this.Close();
            }


        }

        private void bunifuMaterialTextbox2_OnValueChanged(object sender, EventArgs e)
        {
            bunifuMaterialTextbox2.isPassword = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }


    }
}
