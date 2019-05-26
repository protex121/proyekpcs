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
        OracleDataAdapter odan;
        DataTable dtn= new DataTable();
        OracleCommandBuilder cmdn;       
        int subtotal = 0;
        int total = 0;
        int delcell;
        public string idcabang;
        public string idkasir;
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
                MessageBox.Show("Gagal Karena " + ex.Message);
            }        

            String query = "SELECT * FROM TENNANT";
            OracleDataAdapter oda = new OracleDataAdapter(query, oc);
            DataTable dt = new DataTable();            
            oda.Fill(dt);
            comboBox2.DataSource = dt;
            comboBox2.DisplayMember = "NAMA_TENNANT";
            comboBox2.ValueMember = "ID_TENNANT";

            String querypromo = "SELECT * FROM PROMO ORDER BY ID_PROMO";
            oda = new OracleDataAdapter(querypromo, oc);
            dt = new DataTable();
            oda.Fill(dt);            
            comboBox3.DataSource = dt;
            comboBox3.DisplayMember = "TITLE_PROMO";
            comboBox3.ValueMember = "ID_PROMO";
            
            //MessageBox.Show(idcabang);
            String namakasir = "SELECT NAMA_PEGAWAI,CABANG_PEGAWAI FROM PEGAWAI WHERE ID_PEGAWAI='" + idkasir + "'";
            OracleCommand loadnama = new OracleCommand(namakasir, oc);
            label12.Text = "Hi, My Name is " + loadnama.ExecuteScalar().ToString();

        }

        private void bunifuCustomDataGrid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            bool adamember = false;
            string idtrans = "";
            int pembayaran = 0;
            OracleTransaction ot = oc.BeginTransaction();
            String fetch = "SELECT * FROM DTRANS";
            odan = new OracleDataAdapter(fetch, oc);
            dtn = new DataTable();
            cmdn = new OracleCommandBuilder(odan);
            odan.Fill(dtn);
            if (textBox1.Text == "")
            {
                MessageBox.Show("Insert Jumlah yang Dibayar tidak Valid!");
            }
            else
            {
                string idt = "SELECT AUTOGEN_ID_TRANS('" + idcabang + "') FROM DUAL";
                OracleCommand cmd = new OracleCommand(idt, oc);
                idtrans = cmd.ExecuteScalar().ToString();
                if (textBox2.Text == "")
                {
                    adamember = true;
                    member = "GUEST";
                }
                else
                {
                    string cekmember = "SELECT COUNT(*) FROM MEMBER WHERE ID_MEMBER='" + textBox2.Text + "'";
                    OracleCommand cmdcek = new OracleCommand(cekmember, oc);
                    int tempcekmember = Convert.ToInt32(cmdcek.ExecuteScalar());

                    if (tempcekmember <= 0)
                    {
                        adamember = false;
                        MessageBox.Show("ID Member Tidak Valid!");
                    }
                    else
                    {
                        adamember = true;
                        member = textBox2.Text;
                    }
                }
                if (adamember == true)
                {
                    string queryhtr = "INSERT INTO HTRANS VALUES('" + idtrans + "',TO_DATE(TO_CHAR(SYSDATE, 'DD/MM/YYYY HH24:MI:SS'),'DD/MM/YYYY HH24:Mi:SS'),'" + total + "','" + comboBox3.SelectedValue.ToString() + "','" + member + "','" + idkasir + "')";
                    try
                    {                       
                        OracleCommand cmdinsh = new OracleCommand(queryhtr, oc);
                        cmdinsh.ExecuteNonQuery();                        

                        for (int i = 0; i < bunifuCustomDataGrid1.Rows.Count; i++)
                        {
                            string namamenu = bunifuCustomDataGrid1[1, i].Value.ToString();
                            string conidm = "SELECT ID_MENU FROM MENU WHERE NAMA_MENU='" + namamenu + "'";
                            try
                            {
                                OracleCommand cmdcekid = new OracleCommand(conidm, oc);
                                string idm = cmdcekid.ExecuteScalar().ToString();
                                string querydtr = "INSERT INTO DTRANS VALUES('" + idtrans + "','" + idm + "','" + bunifuCustomDataGrid1[0, i].Value.ToString() + "','"
                                    + bunifuCustomDataGrid1[2, i].Value.ToString() + "','" + bunifuCustomDataGrid1[3, i].Value.ToString() + "')";
                                OracleCommand cmdinsd = new OracleCommand(querydtr, oc);
                                cmdinsd.ExecuteNonQuery();
                                odan.Update(dtn);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Gagal Karena " + ex.Message);
                            }
                        }

                        pembayaran = Convert.ToInt32(textBox1.Text);
                        label6.Text = (pembayaran - total).ToString("#,##");

                        subtotal = 0;
                        label5.Text = subtotal.ToString("#,##");

                        total = 0;
                        label8.Text = total.ToString("#,##");

                        ot.Commit();

                        bunifuCustomDataGrid1.Rows.Clear();
                        bunifuCustomDataGrid1.Refresh();
                        MessageBox.Show("Berhasil Dibayar");

                        //Menunjukkan Nota Habis Mbayar
                        FormNota fn = new FormNota(idtrans, pembayaran);
                        fn.Show();
                    }
                    catch (Exception ex)
                    {
                        ot.Rollback();
                        MessageBox.Show("Gagal Karena " + ex.Message);
                    }
                }
            }
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)            
        {
            if (comboBox2.Text == "" || comboBox1.Text == "" || numericUpDown1.Value == 0)
            {
                MessageBox.Show("Isi Semua Inputbox");
            }
            else
            {
                label6.Text = "0";
                string harga = "SELECT HARGA_MENU FROM MENU_TENNANT WHERE ID_MENU='" + comboBox1.SelectedValue + "'";
                OracleCommand cmd = new OracleCommand(harga, oc);
                int hargaper = Convert.ToInt32(cmd.ExecuteScalar());
                int hargafix = hargaper * Convert.ToInt32(numericUpDown1.Value);
                bool cekmenusama = false;                                
                for (int i = 0; i < bunifuCustomDataGrid1.Rows.Count; i++)
                {
                    if (comboBox2.SelectedValue.ToString() == bunifuCustomDataGrid1[0, i].Value.ToString() &&
                        comboBox1.Text == bunifuCustomDataGrid1[1, i].Value.ToString())
                    {
                        cekmenusama = true;
                        bunifuCustomDataGrid1[2, i].Value = Convert.ToInt32(bunifuCustomDataGrid1[2, i].Value) + Convert.ToInt32(numericUpDown1.Value);
                        bunifuCustomDataGrid1[3, i].Value = Convert.ToInt32(bunifuCustomDataGrid1[3, i].Value) + hargafix;                      
                    }
                }
                if (cekmenusama == false)
                {
                    bunifuCustomDataGrid1.Rows.Add(comboBox2.SelectedValue.ToString(), comboBox1.Text, Convert.ToInt32(numericUpDown1.Value), hargafix);                   
                }

                subtotal += hargafix;
                label5.Text = subtotal.ToString("#,##");

                total = subtotal;
                label8.Text = total.ToString("#,##");
                cekpromo();
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
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            if (delcell < 0)
            {

            }
            else
            {
                subtotal -= Convert.ToInt32(bunifuCustomDataGrid1[3, delcell].Value);
                label5.Text = subtotal + "";
                total = subtotal;

                label8.Text = total + "";
                bunifuCustomDataGrid1.Rows.RemoveAt(delcell);
                cekpromo();
            }
        }

        private void bunifuCustomDataGrid1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            delcell = e.RowIndex;
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            cekpromo();
        }

        public void cekpromo()
        {
            if (comboBox3.Text == "NO PROMO")
            {
                label5.Text = subtotal + "";
                total = subtotal;
                label8.Text = total + "";
            }         
            else
            {                
                string potonganpromo = "SELECT JML_PROMO FROM PROMO WHERE ID_PROMO= '" + comboBox3.SelectedValue.ToString() + "'";
                OracleCommand isipromo = new OracleCommand(potonganpromo, oc);
                total = 0;
                label5.Text = subtotal + " + " + comboBox3.Text;
                for (int i = 0; i < bunifuCustomDataGrid1.Rows.Count; i++)
                {
                    total = total + Convert.ToInt32(bunifuCustomDataGrid1[2, i].Value) * Convert.ToInt32(bunifuCustomDataGrid1[3, i].Value) / Convert.ToInt32(bunifuCustomDataGrid1[2, i].Value);
                }
                total = total - (total * Convert.ToInt32(isipromo.ExecuteScalar()) / 100);
                label8.Text = total + "";
            }
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
