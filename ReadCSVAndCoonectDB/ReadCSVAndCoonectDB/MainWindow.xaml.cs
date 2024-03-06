using Microsoft.VisualBasic;
using Microsoft.Win32;
using ReadCSVAndCoonectDB;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xaml;

namespace ReadCSVAndCoonectDB
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
            Closed += MainWindow_Closed;
        }

        private void MainWindow_Closed(object? sender, EventArgs e)
        {
            list.Clear();
            thisConnection.Close();
        }
        List<string> sub = new List<string>();
        List<string> year = new List<string>();
        List<Information> list = new List<Information>();
        SqlConnection thisConnection = new SqlConnection(@"Server=(local);Database=PRN_Scores;Trusted_Connection=Yes;");
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {

                thisConnection.Open();

                string Get_Data = "SELECT * FROM SchoolYear";

                SqlCommand cmd = thisConnection.CreateCommand();
                cmd.CommandText = Get_Data;

                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    box.Items.Add(reader["exam_year"].ToString());
                }
            }
            catch
            {
                MessageBox.Show("db error");
            }
        }

        private void Btn_Browse(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Csv Files| *.csv";
            if (dialog.ShowDialog() == true)
            {
                txtPath.Text = dialog.FileName;
            }
        }

        private void Btn_Import(object sender, RoutedEventArgs e)
        {
            Dictionary<string, Number> map = new Dictionary<string, Number>();
            using (var reader = new StreamReader(txtPath.Text))
            {
                //get subject name
                var subject = reader.ReadLine();
                var st = subject.Split(',');
                sub.Add(st[1]);
                sub.Add(st[2]);
                sub.Add(st[3]);
                sub.Add(st[4]);
                sub.Add(st[5]);
                sub.Add(st[7]);
                sub.Add(st[8]);
                sub.Add(st[9]);
                sub.Add(st[10]);
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    try
                    {
                        for (int i = 0; i < values.Length; i++)
                        {
                            if (values[i] == "")
                            {
                                values[i] = "0";
                            }
                        }
                        list.Add(new Information(values[0], double.Parse(values[1]), double.Parse(values[2]), double.Parse(values[3]), double.Parse(values[4]), double.Parse(values[5]), values[6], double.Parse(values[7]), double.Parse(values[8]), double.Parse(values[9]), double.Parse(values[10]), values[11]));
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Read error");
                        break;
                    }
                }
                reader.Close();
            }

            foreach (var item in list)
            {
                //get year
                if (!year.Contains(item.Year))
                {
                    year.Add(item.Year);
                }
                //calculate how many student in each year
                if (!map.ContainsKey(item.Year))
                {
                    Number nm = new Number();
                    nm.Year = item.Year;
                    nm.Student_Count++;
                    if (item.English != 0)
                        nm.English++;
                    if (item.History != 0)
                        nm.History++;
                    if (item.CivicEducation != 0)
                        nm.CivicEducation++;
                    if (item.Biology != 0)
                        nm.Biology++;
                    if (item.Chemistry != 0)
                        nm.Chemistry++;
                    if (item.Mathematics != 0)
                        nm.Mathematics++;
                    if (item.Literature != 0)
                        nm.Literature++;
                    if (item.Physics != 0)
                        nm.Physics++;
                    if (item.Geography != 0)
                        nm.Geography++;
                    map.Add(item.Year, nm);
                }
                else
                {
                    map[item.Year].Student_Count++;
                    if (item.English != 0)
                        map[item.Year].English++;
                    if (item.History != 0)
                        map[item.Year].History++;
                    if (item.CivicEducation != 0)
                        map[item.Year].CivicEducation++;
                    if (item.Biology != 0)
                        map[item.Year].Biology++;
                    if (item.Chemistry != 0)
                        map[item.Year].Chemistry++;
                    if (item.Mathematics != 0)
                        map[item.Year].Mathematics++;
                    if (item.Literature != 0)
                        map[item.Year].Literature++;
                    if (item.Physics != 0)
                        map[item.Year].Physics++;
                    if (item.Geography != 0)
                        map[item.Year].Geography++;
                }

            }
            CsvData.ItemsSource = map.Values;
        }

        private void Btn_Save(object sender, RoutedEventArgs e)
        {
            if (list.Count == 0)
            {
                Btn_Import(sender, e);
            }
            //check year is existed or not
            foreach (var item in year)
            {

                string checkYearQuery = "SELECT COUNT(*) FROM dbo.SchoolYear WHERE exam_year = @year";
                SqlCommand checkYearCommand = new SqlCommand(checkYearQuery, thisConnection);
                checkYearCommand.Parameters.AddWithValue("@year", item);

                int count = (int)checkYearCommand.ExecuteScalar();
                if (count > 0)
                {
                    MessageBox.Show(item + " existed");
                    continue;
                }
                else
                {
                    string insertYearQuery = "INSERT INTO dbo.SchoolYear(id, exam_year, status) VALUES(@id, @year, @status)";
                    SqlCommand insertYearCommand = new SqlCommand(insertYearQuery, thisConnection);
                    insertYearCommand.Parameters.AddWithValue("@id", year.IndexOf(item) + 1);
                    insertYearCommand.Parameters.AddWithValue("@year", item);
                    insertYearCommand.Parameters.AddWithValue("@status", 1);
                    insertYearCommand.ExecuteNonQuery();
                }
            }
            //save subject
            foreach (var i in sub)
            {
                string insertSubjectQuery = "SELECT COUNT(*) FROM dbo.Subject WHERE name = @name";
                SqlCommand insertSubjectCommand = new SqlCommand(insertSubjectQuery, thisConnection);
                insertSubjectCommand.Parameters.AddWithValue("@name", i);
                int count = (int)insertSubjectCommand.ExecuteScalar();
                if (count == 0)
                {
                    insertSubject(i);
                }
            }

        }

        private void insertSubject(string name)
        {
            string insertSubjectQuery = "INSERT INTO dbo.Subject(id, code, name) VALUES(@id, @code, @name)";
            SqlCommand insertSubjectCommand = new SqlCommand(insertSubjectQuery, thisConnection);
            insertSubjectCommand.Parameters.AddWithValue("@id", Guid.NewGuid());
            switch (name)
            {
                case "Toan":
                    insertSubjectCommand.Parameters.AddWithValue("@code", "Mathematics");
                    insertSubjectCommand.Parameters.AddWithValue("@name", "Toán");
                    break;
                case "Van":
                    insertSubjectCommand.Parameters.AddWithValue("@code", "Literature");
                    insertSubjectCommand.Parameters.AddWithValue("@name", "Văn");
                    break;
                case "Ngoai ngu":
                    insertSubjectCommand.Parameters.AddWithValue("@code", "English");
                    insertSubjectCommand.Parameters.AddWithValue("@name", "Anh");
                    break;
                case "Ly":
                    insertSubjectCommand.Parameters.AddWithValue("@code", "Physics");
                    insertSubjectCommand.Parameters.AddWithValue("@name", "Lý");
                    break;
                case "Hoa":
                    insertSubjectCommand.Parameters.AddWithValue("@code", "Chemistry");
                    insertSubjectCommand.Parameters.AddWithValue("@name", "Hóa");
                    break;
                case "Sinh":
                    insertSubjectCommand.Parameters.AddWithValue("@code", "Biology");
                    insertSubjectCommand.Parameters.AddWithValue("@name", "Sinh");
                    break;
                case "Lich su":
                    insertSubjectCommand.Parameters.AddWithValue("@code", "History");
                    insertSubjectCommand.Parameters.AddWithValue("@name", "Sử");
                    break;
                case "Dia ly":
                    insertSubjectCommand.Parameters.AddWithValue("@code", "Geography");
                    insertSubjectCommand.Parameters.AddWithValue("@name", "Địa");
                    break;
                case "GDCD":
                    insertSubjectCommand.Parameters.AddWithValue("@code", "CivicEducation");
                    insertSubjectCommand.Parameters.AddWithValue("@name", "GDCD");
                    break;
                default:
                    break;
            }
            insertSubjectCommand.ExecuteNonQuery();
        }
    }
}