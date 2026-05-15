using Microsoft.EntityFrameworkCore;
using pract6.database;
using System.ComponentModel;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace pract6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private AppDbContext _dbContext;
        private BindingList<tests> _tests;

        private void Form1_Load(object sender, EventArgs e)
        {
            listView1.GridLines = true;
            listView1.FullRowSelect = true;

            listView1.View = View.Details;
            listView1.Scrollable = true;
            listView1.MultiSelect = false;

            if (listView1.Columns.Count == 0)
            {
                listView1.Columns.Add("Id", 50);
                listView1.Columns.Add("Name", 150);
                listView1.Columns.Add("Date", 150);
                listView1.Columns.Add("Used Material", 150);
                listView1.Columns.Add("Result", 150);
            }

            _dbContext = new AppDbContext();
            //_dbContext.Database.EnsureDeleted();
            _dbContext.Database.EnsureCreated();

            LoadData();

            _tests = _dbContext.tests.Local.ToBindingList();
            _tests.RaiseListChangedEvents = true;
            _tests.ListChanged += _tests_ListChanged;
            _dbContext.tests.Load();

            //_dbContext.tests.Load();
            //_tests = _dbContext.tests.Local.ToBindingList();


            //listBox1.DataSource = _tests;
            //listBox1.ValueMember = "Id";
            //listBox1.DisplayMember = "Name";

            //comboBox1.DataSource = _tests;
            //comboBox1.ValueMember = "Id";
            //comboBox1.DisplayMember = "Name";

            button4.Visible = false;
            button5.Visible = false;

            LoadGrid();
        }

        //??? where is that supposed to be
        private void _tests_ListChanged(object? sender, ListChangedEventArgs e)
        {
            listView1.Items.Clear();
            foreach (var test in _tests)
            {
                var item = new ListViewItem(test.Id.ToString());
                item.SubItems.Add(test.Name);
                item.SubItems.Add(test.Date);
                item.SubItems.Add(test.UsedMaterial);
                item.SubItems.Add(test.Result);
                listView1.Items.Add(item);
            }
        }

        //add
        private void button1_Click(object sender, EventArgs e)
        {
            _dbContext.Add(new tests
            {
                Name = textBox1.Text,
                Date = DateTime.Now.ToString("yyyy-MM-dd"),
                UsedMaterial = textBox3.Text,
                Result = textBox4.Text
            }
                );
            _dbContext.SaveChanges();

            textBox1.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";

            LoadData();
            LoadGrid();
        }

        //delete
        private void button2_Click(object sender, EventArgs e)
        {
            int id = (int)comboBox1.SelectedValue;

            var test = _dbContext.tests.Find(id);

            if (test != null)
            {
                _dbContext.tests.Remove(test);
                _dbContext.SaveChanges();
                LoadData();
                LoadGrid();
            }
        }

        //edit
        private void button3_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Visible = true;
            button5.Visible = true;
        }

        //save
        private void button4_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            button4.Visible = false;
            button5.Visible = false;

            if (comboBox1.SelectedItem is tests selectedTest)
            {
                selectedTest.Name = textBox1.Text;
                selectedTest.UsedMaterial = textBox3.Text;
                selectedTest.Result = textBox4.Text;

                _dbContext.SaveChanges();
                LoadData();
                LoadGrid();
            }

            textBox1.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";

        }

        //cancel
        private void button5_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            button4.Visible = false;
            button5.Visible = false;

            textBox1.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
        }


        private void LoadData()
        {
            listBox1.DataSource = null;
            comboBox1.DataSource = null;

            var data = _dbContext.tests.ToList();

            listBox1.DataSource = data;
            listBox1.DisplayMember = "Name";
            listBox1.ValueMember = "Id";

            comboBox1.DataSource = data;
            comboBox1.DisplayMember = "Name";
            comboBox1.ValueMember = "Id";
        }

        private void LoadGrid()
        {
            using var db = new AppDbContext();

            var data = db.tests.ToList();

            dataGridView1.DataSource = data;
        }

    }
}
