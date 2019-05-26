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
    public partial class FormPilihCabang : Form
    {
        OracleConnection oc;

        public FormPilihCabang()
        {
            InitializeComponent();
        }

        private void FormPilihCabang_Load(object sender, EventArgs e)
        {
            oc = new OracleConnection("data source=orcl;user id=proyekpcs;password=proyekpcs");
            oc.Open();

            OracleDataAdapter da = new OracleDataAdapter("select id_cabang as i, nama_cabang as n from cabang", oc);
            DataTable dt = new DataTable();

            da.Fill(dt);

            comboBox1.DataSource = dt;
            comboBox1.ValueMember = "i";
            comboBox1.DisplayMember = "n";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string cabang = comboBox1.SelectedValue.ToString();

            FormLaporan fl = new FormLaporan("income", cabang);
            fl.Show();

        }
    }
}
