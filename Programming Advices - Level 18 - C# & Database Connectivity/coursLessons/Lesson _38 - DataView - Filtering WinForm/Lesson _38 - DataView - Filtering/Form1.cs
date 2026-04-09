using System;
using System.Data;
using System.Windows.Forms;


namespace Lesson__38___DataView___Filtering
{
    public partial class Form1 : Form
    {
        private DataView employeesDataView = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            bindData();
        }

        private void bindData()
        {

            DataTable employeesDataTable = new DataTable();
            employeesDataTable.Columns.Add("ID", typeof(int));
            employeesDataTable.Columns.Add("Name", typeof(string));
            employeesDataTable.Columns.Add("Country", typeof(string));
            employeesDataTable.Columns.Add("Salary", typeof(Double));
            employeesDataTable.Columns.Add("Date", typeof(DateTime));

            //Add rows 
            employeesDataTable.Rows.Add(1, "Mohammed Abu-Hadhoud", "Jordan", 5000, DateTime.Now);
            employeesDataTable.Rows.Add(2, "Ali Maher", "KSA", 525.5, DateTime.Now);
            employeesDataTable.Rows.Add(3, "Lina Kamal", "Jordan", 730.5, DateTime.Now);
            employeesDataTable.Rows.Add(4, "Fadi Jameel", "Egypt", 800, DateTime.Now);
            employeesDataTable.Rows.Add(5, "Omar Mahmoud", "Lebanon", 7000, DateTime.Now);
            employeesDataTable.Rows.Add(6, "Sara Ahmed", "UAE", 4500, DateTime.Now);
            employeesDataTable.Rows.Add(7, "Khaled Hassan", "Egypt", 3200, DateTime.Now);
            employeesDataTable.Rows.Add(8, "Nadia Ali", "Jordan", 2800, DateTime.Now);
            employeesDataTable.Rows.Add(9, "Hassan Youssef", "KSA", 1500, DateTime.Now);
            employeesDataTable.Rows.Add(10, "Mona Ibrahim", "Lebanon", 6200, DateTime.Now);
            employeesDataTable.Rows.Add(11, "Youssef Mahmoud", "Egypt", 950, DateTime.Now);
            employeesDataTable.Rows.Add(12, "Amira Samir", "Jordan", 4100, DateTime.Now);
            employeesDataTable.Rows.Add(13, "Tamer Hany", "UAE", 8300, DateTime.Now);
            employeesDataTable.Rows.Add(14, "Rania Adel", "KSA", 2750, DateTime.Now);
            employeesDataTable.Rows.Add(15, "Mahmoud Said", "Lebanon", 1900, DateTime.Now);

            employeesDataView = employeesDataTable.DefaultView;
            dgvEmployees.DataSource = employeesDataView;
        }

        private void ApplyRowFilter()
        {
            if (string.IsNullOrWhiteSpace(txtRowFilter.Text))
            {
                employeesDataView.RowFilter = "";
                return;
            }

            try
            {
                employeesDataView.RowFilter = txtRowFilter.Text;
            }
            catch
            {
                MessageBox.Show($" Invalid filter expression : {txtRowFilter.Text} \n Example: Country = 'Jordan' OR Country = 'Egypt'",
                               "Error",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Warning);
            }
        }

        private void txtRowFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Apply filter when Enter is pressed
                ApplyRowFilter();
                e.SuppressKeyPress = true;  // Optional: prevents "ding" sound
            }
        }
    }
}
