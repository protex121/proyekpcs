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
    public partial class FormNota : Form
    {
        string idtrans;

        public FormNota()
        {
            InitializeComponent();
        }

        public FormNota(string idtrans)
        {
            InitializeComponent();
            this.idtrans = idtrans;
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }

        private void FormNota_Load(object sender, EventArgs e)
        {
            NotaKasir nk = new NotaKasir();
            nk.SetDatabaseLogon("proyekpcs", "proyekpcs");

            nk.SetParameterValue("IDTRANS", idtrans);

            crystalReportViewer1.ReportSource = nk;
            crystalReportViewer1.Refresh();
            MessageBox.Show(idtrans);
        }
    }
}
