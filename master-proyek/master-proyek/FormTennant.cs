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
    public partial class FormTennant : Form
    {
        public string username;

        public FormTennant()
        {
            InitializeComponent();
        }

        private void FormTennant_Load(object sender, EventArgs e)
        {
            bunifuCustomLabel2.Text = "WELCOME "+username.ToUpper()+",";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            changeform<formAddMenu>();
            button1.BackColor = Color.FromArgb(12, 61, 92);
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
            //if (Application.OpenForms["Form1"] == null)
            //button1.BackColor = Color.FromArgb(4, 41, 68);
            //if (Application.OpenForms["Form2"] == null)
            //  button2.BackColor = Color.FromArgb(4, 41, 68);
            //if (Application.OpenForms["Form3"] == null)
            //  button3.BackColor = Color.FromArgb(4, 41, 68);
        }
    }
}
