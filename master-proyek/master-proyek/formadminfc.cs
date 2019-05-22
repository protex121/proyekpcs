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
    public partial class formadminfc : Form
    {

        public formadminfc()
        {
            InitializeComponent();
        }

        private void formadminfc_Load(object sender, EventArgs e)
        {            
            ToolTip toolTip1 = new ToolTip();
            toolTip1.AutoPopDelay = 1000;
            toolTip1.InitialDelay = 1000;
            toolTip1.ReshowDelay = 500;
            toolTip1.ShowAlways = true;         
            toolTip1.SetToolTip(this.btnCancel, "Exit");
            toolTip1.SetToolTip(this.btnMinimize, "Minimize");
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        
        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        //add cabang
        private void button1_Click(object sender, EventArgs e)
        {
            changeform<formaddCabang>();
            button1.BackColor = Color.FromArgb(12, 61, 92);

            this.Size = new Size(849,460);
            this.CenterToScreen();

            button2.BackColor = Color.FromArgb(4, 41, 68);
            button3.BackColor = Color.FromArgb(4, 41, 68);
            button4.BackColor = Color.FromArgb(4, 41, 68);
        }

        //lihat cabang
        private void button2_Click(object sender, EventArgs e)
        {
            this.Size = new Size(1050, 520);
            this.CenterToScreen();
            changeform<formlihatcabang>();
            button2.BackColor = Color.FromArgb(12, 61, 92);

            button1.BackColor = Color.FromArgb(4, 41, 68);
            button3.BackColor = Color.FromArgb(4, 41, 68);
            button4.BackColor = Color.FromArgb(4, 41, 68);
        }

        //add kasir
        private void button3_Click(object sender, EventArgs e)
        {
            changeform<formAddKasir>();
            button3.BackColor = Color.FromArgb(12, 61, 92);

            this.Size = new Size(849, 600);
            this.CenterToScreen();

            button1.BackColor = Color.FromArgb(4, 41, 68);
            button2.BackColor = Color.FromArgb(4, 41, 68);
            button4.BackColor = Color.FromArgb(4, 41, 68);
        }

        //lihat kasir
        private void button4_Click(object sender, EventArgs e)
        {
            button4.BackColor = Color.FromArgb(12, 61, 92);

            changeform<formLihatKasir>();
            this.Size = new Size(1080, 500);
            this.CenterToScreen();

            button1.BackColor = Color.FromArgb(4, 41, 68);
            button2.BackColor = Color.FromArgb(4, 41, 68);
            button3.BackColor = Color.FromArgb(4, 41, 68);
        }

        private void changeform<formku>() where formku : Form, new()
        {
            
            Form content = panelcontent.Controls.OfType<formku>().FirstOrDefault();
            
            if (content == null)
            {
                content = new formku();
                content.TopLevel = false;
                content.FormBorderStyle = FormBorderStyle.None;
                content.Dock = DockStyle.Fill;
                panelcontent.Controls.Add(content);
                panelcontent.Tag = content;
                content.Show();
                content.BringToFront();
                content.FormClosed += new FormClosedEventHandler(CloseForms);
            }            
            else
            {
                content.BringToFront();
            }
        }


        private void CloseForms(object sender, FormClosedEventArgs e)
        {
            //if (Application.OpenForms["formaddcabang"] == null) {
                //button1.BackColor = Color.FromArgb(4, 41, 68);
            //}
                //button1.BackColor = Color.FromArgb(4, 41, 68);
            //if (Application.OpenForms["Form2"] == null)
              //  button2.BackColor = Color.FromArgb(4, 41, 68);
            //if (Application.OpenForms["Form3"] == null)
              //  button3.BackColor = Color.FromArgb(4, 41, 68);
        }

        private void panelcontent_Paint(object sender, PaintEventArgs e)
        {

        }

        


    }
}
