using System;
using System.Data;
using Npgsql;

namespace _460557_NurWulanFebriani_ResponsiJuniorProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private NpgsqlConnection conn;
        string connstring = "Host=localhost;Port=2022;Username=febri;Password=febri123;Database=responsifebri";
        public DataTable dt;
        public static NpgsqlCommand cmd;
        private string sql = null;
        private DataGridViewRow r;

        private void Form1_Load(object sender, EventArgs e)
        {
            conn = new NpgsqlConnection(connstring);
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            int idDep=0;
            try
            {
                conn.Open();
                sql = @"select * from st_insertkaryawan(:id_karyawan, :nama, :id_dep)";
                cmd = new NpgsqlCommand(sql, conn);
                if(comboDep.Text == "HR")
                {
                    idDep = 1;
                }
                else if(comboDep.Text == "Engineer")
                {
                    idDep = 2;
                }
                else if (comboDep.Text == "Developer")
                {
                    idDep = 3;
                }
                else if (comboDep.Text == "Product Manager")
                {
                    idDep = 4;
                }
                else if (comboDep.Text == "Product Manager")
                {
                    idDep = 5;
                }
                else
                {
                    idDep = 0;
                }
                cmd.Parameters.AddWithValue("id_karyawan", int.Parse(tbID.Text));
                cmd.Parameters.AddWithValue("nama", tbName.Text);
                cmd.Parameters.AddWithValue("id_dep", idDep);
                if((int)cmd.ExecuteScalar() == 1)
                {
                    MessageBox.Show("Data user berhasil dimasukkan", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    conn.Close();
                    btnLoad.PerformClick();
                    tbName.Text = tbID.Text = comboDep.Text = null;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Failed inserting data.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (r == null)
            {
                MessageBox.Show("Mohon pilih baris data yang akan diupdate", "Good!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                int idDep = 0;
                if (comboDep.Text == "HR")
                {
                    idDep = 1;
                }
                else if (comboDep.Text == "Engineer")
                {
                    idDep = 2;
                }
                else if (comboDep.Text == "Developer")
                {
                    idDep = 3;
                }
                else if (comboDep.Text == "Product Manager")
                {
                    idDep = 4;
                }
                else if (comboDep.Text == "Product Manager")
                {
                    idDep = 5;
                }
                else
                {
                    idDep = 0;
                }
                conn.Open();
                sql = @"select * from st_updatekaryawan(:id_karyawan,:nama,:id_dep)";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("id_karyawan", int.Parse(r.Cells["id_karyawan"].Value.ToString()));
                cmd.Parameters.AddWithValue("nama", tbName.Text);
                cmd.Parameters.AddWithValue("id_dep", idDep);
                if ((int)cmd.ExecuteScalar() == 1)
                {
                    MessageBox.Show("Data user berhasil diupdate", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    conn.Close();
                    btnLoad.PerformClick();
                    tbName.Text = tbID.Text = comboDep.Text = null;
                    r = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message, "Update Failed.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (r == null)
            {
                MessageBox.Show("Mohon pilih baris data yang akan dihapus", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Apakah benar anda ingin menghapus data " + r.Cells["nama"].Value.ToString() + "?", "Hapus data terkonfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                try
                {
                    int idDep = 0;
                    if (comboDep.Text == "HR")
                    {
                        idDep = 1;
                    }
                    else if (comboDep.Text == "Engineer")
                    {
                        idDep = 2;
                    }
                    else if (comboDep.Text == "Developer")
                    {
                        idDep = 3;
                    }
                    else if (comboDep.Text == "Product Manager")
                    {
                        idDep = 4;
                    }
                    else if (comboDep.Text == "Product Manager")
                    {
                        idDep = 5;
                    }
                    else
                    {
                        idDep = 0;
                    }
                    conn.Open();
                    sql = @"select * from st_deletekaryawan(:id_karyawan, :nama, :id_dep)";
                    cmd = new NpgsqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("id_karyawan", int.Parse(r.Cells["id_karyawan"].Value.ToString()));
                    if ((int)cmd.ExecuteScalar() == 1)
                    {
                        MessageBox.Show("Data user berhasil dihapus", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        conn.Close();
                        btnLoad.PerformClick();
                        tbName.Text = tbID.Text = comboDep.Text = null;
                        r = null;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error:" + ex.Message, "Delete Failed.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                dgvData.DataSource = null;
                sql = "select * from departemen right join karyawan on karyawan.id_dep = departemen.id_dep";
                cmd = new NpgsqlCommand(sql, conn);
                dt = new DataTable();
                NpgsqlDataReader rd = cmd.ExecuteReader();
                dt.Load(rd);
                dgvData.DataSource = dt;
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Failed.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int idDep;
            if (e.RowIndex >=0)
            {
                r = dgvData.Rows[e.RowIndex];
                tbName.Text = r.Cells["nama"].Value.ToString();
                tbID.Text = r.Cells["id_karyawan"].Value.ToString();
                idDep = int.Parse(r.Cells["id_dep"].Value.ToString());

                if (idDep == 1)
                {
                    comboDep.Text = "HR";
                }
                else if (idDep == 2)
                {
                    comboDep.Text = "Engineer";
                }
                else if (idDep == 3)
                {
                    comboDep.Text = "Developer";
                }
                else if (idDep == 4)
                {
                    comboDep.Text = "Product Manager";
                }
                else if (idDep == 5)
                {
                    comboDep.Text = "Finance";
                }
                else
                {
                    comboDep.Text = null;
                }
            }
        }
    }
}