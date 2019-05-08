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
    public partial class formaddCabang : Form
    {
        public formaddCabang()
        {
            InitializeComponent();
        }

        //btn add
        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {

        }

        //btn reset
        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            bunifuMaterialTextbox1.Text = "";
            bunifuMaterialTextbox2.Text = "";
            bunifuMaterialTextbox3.Text = "";
            bunifuMaterialTextbox4.Text = "";
        }


    }
}
