using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLSV_project2.model2;

namespace QLSV_project2
{
    public partial class Khoa : Form
    {
        public Khoa()
        {
            InitializeComponent();
        }
        private void Khoa_Load(object sender, EventArgs e)
        {
            try
            {
                Model1 context = new Model1();
                List<FACULTY> listFalcultys = context.FACULTies.ToList();
                BindGrid(listFalcultys);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void BindGrid(List<FACULTY> listfaculty)
        {
            dataGridView1.Rows.Clear();
            foreach (var item in listfaculty)
            {
                int index = dataGridView1.Rows.Add();
                dataGridView1.Rows[index].Cells[0].Value = item.FacultyID;
                dataGridView1.Rows[index].Cells[1].Value = item.FacultyName;
                dataGridView1.Rows[index].Cells[2].Value = item.TotalProfessor;
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 form = new Form1();
            form.ShowDialog();
        }
    }
}
