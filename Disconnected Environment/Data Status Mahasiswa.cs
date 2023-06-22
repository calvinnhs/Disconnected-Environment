using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Disconnected_Environment
{
    public partial class Data_Status_Mahasiswa : Form
    {
        private string stringconnection = "data source =DESKTOP-BI70IVU;" +
            "database=prodi_mahasiswa;user ID=sa; password=sayangmei";
        private SqlConnection koneksi;
        public Data_Status_Mahasiswa()
        {
            InitializeComponent();
            koneksi = new SqlConnection(stringconnection);
            refreshform();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView();
            btnOpen.Enabled = false;
        }

        private void Data_Status_Mahasiswa_Load(object sender, EventArgs e)
        {

        }

        private void refreshform()
        {
            txtNama.Enabled = false;
            txtStatus.Enabled = false;  
            txtTahun.Enabled = false;
            txtNama.SelectedIndex = -1;
            txtTahun.SelectedIndex = -1;
            txtNim.Visible = false;
            btnSave.Enabled = false;    
            btnClear.Enabled = false;
            btnAdd.Enabled = false;
           
        }

        private void dataGridView()
        {
            koneksi.Open();
            string str = "select * dbo.status_mahasiswa";
            SqlDataAdapter da = new SqlDataAdapter(str, koneksi);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            koneksi.Close();
        }

        private void cbnama()
        {
            koneksi.Open();
            string str = "select nama_mahasiswa from dbo.mahasiswa where " +
                "not EXISTS(select id_status from dbo.status_mahasiswa where " +
                "status_mahasiswa.nim = mahasiswa.nim)";
            SqlCommand cmd = new SqlCommand(str, koneksi);
            SqlDataAdapter da = new SqlDataAdapter(str, koneksi);
            DataSet ds = new DataSet();
            da.Fill(ds);
            cmd.ExecuteReader();
            koneksi.Close();

            txtNama.DisplayMember = "nama_mahasiswa";
            txtNama.ValueMember = "Nim";
            txtNama.DataSource = ds.Tables[0];
        }

        private void cbtahunmasuk()
        {
            int y = DateTime.Now.Year - 2010;
            string[] type = new string[y];
            int i = 0;
            for (i = 0; i < type.Length; i++)
            {
                if (i == 0)
                {
                    txtTahun.Items.Add("2010");
                }
                else
                {
                    int l = 2010 + i;
                    txtTahun.Items.Add(l.ToString());
                }
            }
        }

        private void txtNama_SelectedIndexChanged(object sender, EventArgs e)
        {
            koneksi.Open();
            string nim = "";
            string strs = "select NIM from dbo.Mahasiswa where nama_mahasiswa = @nm";
            SqlCommand cm = new SqlCommand(strs, koneksi);
            cm.CommandType = CommandType.Text;
            cm.Parameters.Add(new SqlParameter("@nm", txtNama.Text));
            SqlDataReader dr = cm.ExecuteReader();
            while (dr.Read())
            {
                nim = dr["NIM"].ToString();
            }
            dr.Close();
            koneksi.Close();

            txtNim.Text = nim;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            txtTahun.Enabled = true;
            txtNama.Enabled = true;
            txtStatus.Enabled = true;
            txtNim.Visible = true;
            cbtahunmasuk();
            cbnama();
            btnClear.Enabled = true;
            btnSave.Enabled = true;
            btnAdd.Enabled = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string nim = txtNim.Text;
            string statusmahasiswa = txtStatus.Text;
            string tahunmasuk = txtTahun.Text;
            int count = 0;
            string tempkodestatus = "";
            string kodestatus = "";
            koneksi.Open();

            string str = "select count (*) from dbo.status_mahasiswa";
            SqlCommand cm = new SqlCommand(str, koneksi);
            count = (int)cm.ExecuteScalar();    

            if(count == 0)
            {
                kodestatus = "1";
            }
            else
            {
                string querryString = "select max(id_status) from dbo.status_mahasiswa";
                SqlCommand cmStatusMahasiswaSum = new SqlCommand(str, koneksi);
                int totalStatusMahasiswa = (int)cmStatusMahasiswaSum.ExecuteScalar();
                int finalkodestatusint = totalStatusMahasiswa + 1;
                kodestatus = Convert.ToString(finalkodestatusint);
            }
            string queryString = "insert into dbo.status_mahasiswa (id_status, nim, " +
                "status_mahasiswa, tahun_masuk)" + "values(@ids, @Nim, @sm, @tm)";
            SqlCommand cmd = new SqlCommand(queryString, koneksi);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add(new SqlParameter("ids", kodestatus));
            cmd.Parameters.Add(new SqlParameter("Nim", nim));
            cmd.Parameters.Add(new SqlParameter("sm", statusmahasiswa));
            cmd.Parameters.Add(new SqlParameter("tm", tahunmasuk));
            cmd.ExecuteNonQuery();
            koneksi.Close();

            MessageBox.Show("data berhasil disimpan", "sukses", MessageBoxButtons.OK,
                MessageBoxIcon.Information );
            refreshform();
            dataGridView();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            refreshform();
        }


    }
}
