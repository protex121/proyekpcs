using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.DataAccess.Client;

namespace master_proyek
{
    public partial class formKasir : Form
    {
        public formKasir()
        {
            InitializeComponent();
        }

        OracleConnection oc;
        int subtotal = 0;
        int total = 0;
        int delcell;
        string idcabang="T01";
        string idkasir="PD001";
        string member;
        private void formKasir_Load(object sender, EventArgs e)
        {
            bunifuCustomDataGrid1.AllowUserToAddRows = false;
            try
            {
                oc = new OracleConnection("user id= proyekpcs; password= proyekpcs; data source=orcl");
                oc.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal Karena "+ ex.Message);
            }
            String query = "SELECT * FROM TENNANT";
            OracleDataAdapter oda = new OracleDataAdapter(query, oc);
            DataTable dt = new DataTable();
            oda.Fill(dt);
            comboBox2.DataSource = dt;
            comboBox2.DisplayMember = "NAMA_TENNANT";
            comboBox2.ValueMember = "ID_TENNANT";
            String querypromo = "SELECT * FROM PROMO ORDER BY ID_PROMO";
            OracleDataAdapter oda1 = new OracleDataAdapter(querypromo, oc);
            DataTable dt1 = new DataTable();
            oda1.Fill(dt1);            
            comboBox3.DataSource = dt1;            
            comboBox3.DisplayMember = "TITLE_PROMO";
            comboBox3.ValueMember = "ID_PROMO";
            String namakasir = "SELECT NAMA_PEGAWAI FROM PEGAWAI WHERE ID_PEGAWAI='" + idkasir + "'";
        }

        private void bunifuCustomDataGrid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Jumlah yang Dibayar tidak Valid!");
            }
            else {
                string idt = "SELECT AUTOGEN_ID_TRANS('" + idcabang + "') FROM DUAL";
                OracleCommand cmd = new OracleCommand(idt, oc);
                string idtrans = cmd.ExecuteScalar().ToString();
                if (textBox2.Text == "")
                {
                    member = "GUEST";
                }
                else
                {
                    member = textBox2.Text;
                }
                //MessageBox.Show(bunifuCustomDataGrid1.Columns.Count+"");
                string queryhtr = "INSERT INTO HTRANS VALUES('" + idtrans + "',TO_DATE(SYSDATE,'DD/MM/YYYY'),'" + total + "','" + comboBox3.SelectedValue.ToString() + "','" + member + "','" + idkasir + "')";
                OracleCommand cmdinsh = new OracleCommand(queryhtr, oc);
                cmdinsh.ExecuteNonQuery();
                for (int i = 0; i < bunifuCustomDataGrid1.Rows.Count; i++)
                {
                    string namamenu = bunifuCustomDataGrid1[1, i].Value.ToString();
                    string conidm = "SELECT ID_MENU FROM MENU WHERE NAMA_MENU='"+namamenu + "'";
                    //MessageBox.Show(conidm);
                    try
                    {
                        OracleCommand cmdcekid = new OracleCommand(conidm, oc);
                        string idm = cmdcekid.ExecuteScalar().ToString();
                        string querydtr = "INSERT INTO DTRANS VALUES('" + idtrans + "','" + idm + "','" + bunifuCustomDataGrid1[0, i].Value.ToString() + "'," +
                            "'" + bunifuCustomDataGrid1[2, i].Value.ToString() + "','" + bunifuCustomDataGrid1[3, i].Value.ToString() + "')";
                        OracleCommand cmdinsd = new OracleCommand(querydtr, oc);
                        cmdinsd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Gagal Karena " + ex.Message);
                    }
                }
                label6.Text = (Convert.ToInt32(textBox1.Text) - total).ToString();
            }            
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            label6.Text = "0";
            if (comboBox1.Text == " " || comboBox2.Text == " " || comboBox3.Text == " " || numericUpDown1.Value == 0)
            {
                MessageBox.Show("Fill All the Fields!");
            }
            else {
                String harga = "SELECT HARGA_MENU FROM MENU_TENNANT WHERE ID_MENU='" + comboBox1.SelectedValue + "'";
                OracleCommand cmd = new OracleCommand(harga, oc);
                int hargaper = Convert.ToInt32(cmd.ExecuteScalar());
                int hargafix = hargaper * Convert.ToInt32(numericUpDown1.Value);
                bunifuCustomDataGrid1.Rows.Add(comboBox2.SelectedValue.ToString(), comboBox1.Text, Convert.ToInt32(numericUpDown1.Value), hargafix);
                subtotal += hargafix;
                label5.Text = subtotal + "";
                total = subtotal;
                label8.Text = total + "";
            }          
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedValueChanged(object sender, EventArgs e)
        {
            string query = "SELECT M.NAMA_MENU AS N,M.ID_MENU AS I FROM MENU M, MENU_TENNANT MT " +
                           "WHERE M.ID_MENU= MT.ID_MENU AND MT.ID_TENNANT= '" + comboBox2.SelectedValue.ToString() + "'";
            OracleDataAdapter oda = new OracleDataAdapter(query, oc);
            DataTable dt = new DataTable();
            oda.Fill(dt);
            comboBox1.DataSource = dt;
            comboBox1.DisplayMember = "N";
            comboBox1.ValueMember = "I";
            MessageBox.Show(query);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            if (delcell < 0)
            {

            }
            else {
                subtotal -= Convert.ToInt32(bunifuCustomDataGrid1[3,delcell ].Value);
                label5.Text = subtotal+"";
                total = subtotal;
                label8.Text = total+"";
                bunifuCustomDataGrid1.Rows.RemoveAt(delcell);
                
            }            
        }

        private void bunifuCustomDataGrid1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            delcell = e.RowIndex;
            MessageBox.Show(delcell+"");
        }
    }
}
