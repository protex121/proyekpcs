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
        public static string username;
        public static string id_tennant;

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
            button2.BackColor = Color.FromArgb(4, 41, 68);
            button4.BackColor = Color.FromArgb(4, 41, 68);
            button3.BackColor = Color.FromArgb(4, 41, 68);
            button5.BackColor = Color.FromArgb(4, 41, 68);
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

        private void panelcontent_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            changeform<formManageMenu>();
            button1.BackColor = Color.FromArgb(4, 41, 68);
            button2.BackColor = Color.FromArgb(12, 61, 92);
            button4.BackColor = Color.FromArgb(4, 41, 68);
            button3.BackColor = Color.FromArgb(4, 41, 68);
            button5.BackColor = Color.FromArgb(4, 41, 68);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            changeform<formAddPromo>();
            button1.BackColor = Color.FromArgb(4, 41, 68);
            button2.BackColor = Color.FromArgb(4, 41, 68);
            button4.BackColor = Color.FromArgb(12, 61, 92);

            button5.BackColor = Color.FromArgb(4, 41, 68);
            button3.BackColor = Color.FromArgb(4, 41, 68);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            changeform<formManagePromo>();
            button1.BackColor = Color.FromArgb(4, 41, 68);
            button2.BackColor = Color.FromArgb(4, 41, 68);
            button4.BackColor = Color.FromArgb(4, 41, 68);
            button5.BackColor = Color.FromArgb(4, 41, 68);
            button3.BackColor = Color.FromArgb(12, 61, 92);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Size = new Size(1080, 500);
            this.CenterToScreen();

            formLihatMenu.temp_id = id_tennant;
            changeform<formLihatMenu>();
            button1.BackColor = Color.FromArgb(4, 41, 68);
            button2.BackColor = Color.FromArgb(4, 41, 68);
            button4.BackColor = Color.FromArgb(4, 41, 68);
            button3.BackColor = Color.FromArgb(4, 41, 68);
            button5.BackColor = Color.FromArgb(12, 61, 92);
        }
    }
}
