using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace WinFormsApp71
{
    public partial class Form1 : Form
    { 
        private SqlConnection _connection;
        private SqlDataAdapter adapter;
        private DataTable table;
        public Form1()
        {
            InitializeComponent();
            string path = $@"Data Source = K-405-5\SQLEXPRESS;
                             initial Catalog = udalenka;
                             integrated Security = true;";
            _connection = new SqlConnection(path);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string insert = "insert into StudentProFile (name,bday) values (@name, @date);";
            _connection.Open();
            var command = new SqlCommand(insert, _connection);
            command.Parameters.Add(new SqlParameter("@name", textBox1.Text));
            command.Parameters.Add(new SqlParameter("@date", dateTimePicker1.Value.ToString("yyyy-MM-d")));
            command.ExecuteNonQuery();
            _connection.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlDataAdapter adapter = new SqlDataAdapter("Select * from StudentProFile", _connection);
            table = new();
            adapter.Fill(table);
            dataGridView1.DataSource = table.Tables[0];
        }

        private void button3_Click(object sender, EventArgs e)
        {
            adapter = new SqlDataAdapter($"select * from StudentProFile where id = {numericUpDown1.Value}", _connection);
            table= new DataTable();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog= new OpenFileDialog();
            openFileDialog.Filter = "PNG|*.png|JPG|*.jpg|GIF|*.gif";
            DialogResult result = openFileDialog.ShowDialog();
            if(result == DialogResult.OK)
            {
                byte[] image = File.ReadAllBytes(openFileDialog.FileName);
                SaveImageOnBD(image, (int)numericUpDown1.Value);
            }
        }
        private void SaveImageOnBD(byte[] image, int id)
        {
            string query = @"update StudentProfile
                            set cover = @img
                            where id = @id;";
            _connection.Open();
            var command = new SqlCommand(query,_connection);
            command.Parameters.Add("@img", SqlDbType.Binary, image.Length);
            command.Parameters.Add["@img"].Value = image;

            command.
        }
    }
}