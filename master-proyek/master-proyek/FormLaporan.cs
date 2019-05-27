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
    public partial class FormLaporan : Form
    {
        string tipe, cabang;

        public FormLaporan()
        {
            InitializeComponent();
        }

        public FormLaporan(string tipe)
        {
            InitializeComponent();

            this.tipe = tipe;
        }

        public FormLaporan(string tipe, string cabang)
        {
            InitializeComponent();

            this.tipe = tipe;
            this.cabang = cabang;
        }

        private void FormLaporan_Load(object sender, EventArgs e)
        {
            if (tipe != null)
            {
                if (tipe == "income")
                {
                    FoodCourtReport fcr = new FoodCourtReport();
                    fcr.SetDatabaseLogon("proyekpcs", "proyekpcs");

                    fcr.SetParameterValue("cabang", cabang);

                    crystalReportViewer1.ReportSource = fcr;
                    crystalReportViewer1.Refresh();
                }
                else if (tipe == "gaji")
                {
                    LaporanGajiPegawai lgp = new LaporanGajiPegawai();
                    lgp.SetDatabaseLogon("proyekpcs", "proyekpcs");

                    crystalReportViewer1.ReportSource = lgp;
                    crystalReportViewer1.Refresh();
                }
            }
            else
            {
                MessageBox.Show("Error: No Report Selected!");
            }
        }
    }
}
