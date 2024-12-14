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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace QLSV_project2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void FillFalcultyCombobox(List<FACULTY> listFalcultys)
        {
            this.cmbKhoa.DataSource = listFalcultys;
            this.cmbKhoa.DisplayMember = "FacultyName";
            this.cmbKhoa.ValueMember = "FacultyID";
        }
        //Hàm binding gridView từ list sinh viên
        private void BindGrid(List<STUDENT> listStudent)
        {
            dataGridView1.Rows.Clear();
            foreach (var item in listStudent)
            {
                int index = dataGridView1.Rows.Add();
                dataGridView1.Rows[index].Cells[0].Value = item.StudentID;
                dataGridView1.Rows[index].Cells[1].Value = item.FullName;
                dataGridView1.Rows[index].Cells[2].Value = item.FACULTY.FacultyName; 
                dataGridView1.Rows[index].Cells[3].Value = item.AverageScore;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                Model1 context = new Model1();
                List<FACULTY> listFalcultys = context.FACULTies.ToList(); //lấy các khoa
                List<STUDENT> listStudent = context.STUDENTs.ToList(); //lấy sinh viên
                FillFalcultyCombobox(listFalcultys);
                BindGrid(listStudent);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                Model1 db = new Model1();
                List<STUDENT> studentlist = db.STUDENTs.ToList();
                if (studentlist.Any(s => s.StudentID == txtMSSV.Text))
                {
                    MessageBox.Show("Mã sinh viên đã tồn tại. Vui lòng nhập mã khác. ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                var newStudent = new STUDENT
                {
                    StudentID = txtMSSV.Text,
                    FullName = txtHoten.Text,
                    FacultyID = int.Parse(cmbKhoa.SelectedValue.ToString()),
                    AverageScore = float.Parse(txtDTB.Text),
                };
                db.STUDENTs.Add(newStudent);
                db.SaveChanges();
                BindGrid(db.STUDENTs.ToList());
                MessageBox.Show("Thêm sinh viên thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm dữ liệu: {ex.Message}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                Model1 db = new Model1();
                List<STUDENT>students = db.STUDENTs.ToList();
                var student = students.FirstOrDefault(s => s.StudentID == txtMSSV.Text);
                if (student != null)
                {
                    if(students.Any(s => s.StudentID == txtMSSV.Text && s.StudentID != student.StudentID))
                    {
                        MessageBox.Show("Mã SV đã tồn tại. Vui lòng nhập một mã khác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    student.FullName = txtHoten.Text;
                    student.FacultyID = int.Parse(cmbKhoa.SelectedValue.ToString());
                    student.AverageScore = double.Parse(txtDTB.Text);
                    db.SaveChanges();
                    BindGrid(db.STUDENTs.ToList());
                    MessageBox.Show("Chỉnh sửa thông tin sinh viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Sinh viên không tìm thấy!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật dữ liệu: {ex.Message}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                Model1 db = new Model1();
                List<STUDENT> studentList = db.STUDENTs.ToList();
                var student = studentList.FirstOrDefault(s => s.StudentID == txtMSSV.Text);
                if (student != null)
                {
                    db.STUDENTs.Remove(student);
                    db.SaveChanges();
                    BindGrid(db.STUDENTs.ToList());
                    MessageBox.Show("Sinh viên đã được xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show("Sinh viên không tìm thấy!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật dữ liệu: {ex.Message}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
                txtMSSV.Text = selectedRow.Cells[0].Value.ToString();
                txtHoten.Text = selectedRow.Cells[1].Value.ToString();
                cmbKhoa.Text = selectedRow.Cells[2].Value.ToString();
                txtDTB.Text = selectedRow.Cells[3].Value.ToString(); 
            }
        }

        private void qLKHOAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Khoa khoa = new Khoa();
            khoa.Show();
            Hide();
        }
    }
}
