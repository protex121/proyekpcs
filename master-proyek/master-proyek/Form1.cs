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

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            timer1.Enabled = true;
            
        }

        //button login
        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            string username = bunifuMaterialTextbox1.Text;
            string password = bunifuMaterialTextbox2.Text;

            MessageBox.Show(password);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (detik > 8)
            {
                pictureBox2.Visible = false;               
                panel2.Visible = true;
                panel1.Visible = true;
                this.Size = new Size(781, 541);
                this.Location = new Point((Screen.PrimaryScreen.Bounds.Size.Width / 2) - (this.Size.Width / 2), (Screen.PrimaryScreen.Bounds.Size.Height / 2) - (this.Size.Height / 2));
            }
            else
            {
                detik++;
            }
        }

        private void bunifuMaterialTextbox2_OnValueChanged(object sender, EventArgs e)
        {
            bunifuMaterialTextbox2.isPassword = true;
        }

    }
}
