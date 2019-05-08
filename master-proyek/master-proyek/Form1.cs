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
        OracleConnection conn = new OracleConnection();

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
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        //button login
        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            string username = bunifuMaterialTextbox1.Text;
            string password = bunifuMaterialTextbox2.Text;     
            if(username=="" && password == "")
            {
                formadminfc fc = new formadminfc();
                this.Hide();
                fc.ShowDialog();
                this.Close();
            }
            else
            {
                FormTennant ft = new FormTennant();
                FormTennant.id_tennant = password;
                FormTennant.username = username;
                this.Hide();
                ft.ShowDialog();
                this.Close();
            }


        }
        
        private void bunifuMaterialTextbox2_OnValueChanged(object sender, EventArgs e)
        {
            bunifuMaterialTextbox2.isPassword = true;
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }
    }
}
