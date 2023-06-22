using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Disconnected_Environment
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {
            Form2 fe = new Form2();
            fe.Show();
            this.Hide();
        }

        private void electroToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        

        private void dataMahasiswaToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Halaman_Data_Mahasiswa fo = new Halaman_Data_Mahasiswa();
            fo.Show();
            this.Hide();
        }

        private void dataStatusMahasiswaToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Data_Status_Mahasiswa fr = new Data_Status_Mahasiswa();
            fr.Show();
            this.Hide();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
