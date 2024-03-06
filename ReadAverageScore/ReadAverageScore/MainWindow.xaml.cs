using Microsoft.VisualBasic.FileIO;
using Microsoft.Win32;
using System;
using System.Data;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ReadAverageScore
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Csv Files| *.csv";
            if (dialog.ShowDialog() == true)
            {
                CalculateAverageScores(dialog.FileName);
                

            }
        }

        private void CalculateAverageScores(string filePath)
        {
            //List<Information> displayDataList = new List<Information>();
            //Dictionary<string, Dictionary<string, double>> infor = new Dictionary<string, Dictionary<string, double>>();
            //string[] lines = File.ReadAllLines(filePath);

            //// Lấy tên các môn học từ dòng tiêu đề
            //string[] subjectNames = lines[0].Split(',');

            //foreach(string sub in  subjectNames)
            //{
            //    if (!infor.ContainsKey(sub))
            //    {
            //        Dictionary<string, double> s = new Dictionary<string, double>();
            //        s.Add(sub, 0.0);
            //        infor.Add("", s);
            //    }
            //}

            //// Bỏ qua dòng tiêu đề
            //for (int i = 1; i < lines.Length; i++)
            //{
            //    string[] values = lines[i].Split(',');

            //    double totalScore = 0.0;
            //    int numSubjects = 0;

            //    for (int j = 2; j < values.Length; j++)
            //    {
            //        double score;
            //        if (double.TryParse(values[j], out score))
            //        {
            //            totalScore += score;
            //            numSubjects++;
            //        }
            //    }

            //    double averageScore = numSubjects > 0 ? totalScore / numSubjects : 0.0;

            //    // Tạo một Dictionary để lưu trữ điểm của từng môn học
            //    Dictionary<string, double> subjectScores = new Dictionary<string, double>();
            //    for (int j = 2; j < values.Length; j++)
            //    {
            //        double score;
            //        if (double.TryParse(values[j], out score))
            //        {
            //            string subjectName = subjectNames[j];
            //            subjectScores.Add(subjectName, score);
            //        }
            //    }

            //    displayDataList.Add(new Information
            //    {
            //        Province = province,
            //        Mathematics = subjectScores.ContainsKey("mathematics") ? subjectScores["mathematics"] : 0.0,
            //        Literature = subjectScores.ContainsKey("literature") ? subjectScores["literature"] : 0.0,
            //        Physics = subjectScores.ContainsKey("physics") ? subjectScores["physics"] : 0.0,
            //        Chemistry = subjectScores.ContainsKey("chemistry") ? subjectScores["chemistry"] : 0.0,
            //        Biology = subjectScores.ContainsKey("biology") ? subjectScores["biology"] : 0.0,
            //        CombinedNaturalSciences = subjectScores.ContainsKey("combined_natural_sciences") ? subjectScores["combined_natural_sciences"] : 0.0,
            //        History = subjectScores.ContainsKey("history") ? subjectScores["history"] : 0.0,
            //        Geography = subjectScores.ContainsKey("geography") ? subjectScores["geography"] : 0.0,
            //        CivicEducation = subjectScores.ContainsKey("civic_education") ? subjectScores["civic_education"] : 0.0,
            //        CombinedSocialSciences = subjectScores.ContainsKey("combined_social_sciences") ? subjectScores["combined_social_sciences"] : 0.0,
            //        English = subjectScores.ContainsKey("english") ? subjectScores["english"] : 0.0
            //    });
            //}

            //return displayDataList;

            List<Information> list = new List<Information>();
            using (var reader = new StreamReader(filePath))
            {
                //bo qua dong dau tien
                reader.ReadLine();
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
                        list.Add(new Information(int.Parse(values[0]), values[1], double.Parse(values[2]), double.Parse(values[3]), double.Parse(values[4]), double.Parse(values[5]), double.Parse(values[6]), double.Parse(values[7]), double.Parse(values[8]), double.Parse(values[9]), double.Parse(values[10]), double.Parse(values[11]), double.Parse(values[12])));
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }
                reader.Close();
            }

            //foreach (var item in list)
            //{
            //    string code = item.ProvinceId.ToString();
            //    item.ProvinceId = int.Parse(code.Substring(0));
            //}


            //Calculate average score of each subject on each province and decending sort by mathematics average score
            var result = list.GroupBy(x => x.Province)
                             .Select(x => new
                             {
                                 //ProvinceId = x.Key,
                                 ProvinceName = x.First().Province,
                                 mathematics = x.Average(y => y.Mathematics),
                                 literature = x.Average(y => y.Literature),
                                 physics = x.Average(y => y.Physics),
                                 chemistry = x.Average(y => y.Chemistry),
                                 biology = x.Average(y => y.Biology),
                                 combined_natural_sciences = x.Average(y => y.CombinedNaturalSciences),
                                 history = x.Average(y => y.History),
                                 geography = x.Average(y => y.Geography),
                                 civic_education = x.Average(y => y.CivicEducation),
                                 combined_social_sciences = x.Average(y => y.CombinedSocialSciences),
                                 english = x.Average(y => y.English)
                             })
                             .OrderByDescending(x => x.mathematics)
                             .ToList();

            CsvData.ItemsSource = result;
        }
    }
}