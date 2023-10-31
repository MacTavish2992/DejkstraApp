using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

namespace Dejkstra
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void UpdateCombobox()
        {
            cmb.Items.Clear();
            cmb.Items.Add("A");
            cmb.Items.Add("B");
            cmb.Items.Add("C");
            cmb.Items.Add("D");
            cmb.Items.Add("E");
            cmb.Items.Add("F");
            cmb.Items.Add("G");
            cmb.Items.Add("H");
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.UpdateCombobox();
        }
        void Path(int[,] matrix, int start)
        {
            int countvertex = matrix.GetLength(0);
            int[] dist = new int[countvertex];
            bool[] visit = new bool[countvertex];

            for (int i = 0; i < countvertex; i++)
            {
                dist[i] = 10000000;
            }

            dist[start] = 0;

            for (int j = 0; j < countvertex - 1; j++)
            {
                int minimal = GetMinimalDistance(visit, dist, countvertex);

                if (minimal == -1)
                {
                    MessageBox.Show("Вершина-источник неверно выбрана или что-то пошло не так");
                    break; 
                }

                visit[minimal] = true;

                for (int i = 0; i < countvertex; i++)
                {
                    if (!visit[i] && matrix[minimal, i] != 0 && dist[minimal] != 10000000 &&
                        dist[minimal] + matrix[minimal, i] < dist[i])
                    {
                        dist[i] = dist[minimal] + matrix[minimal, i];
                    }
                }
            }

            PrintToLabel(dist, countvertex);
        }
        int GetMinimalDistance(bool[]visit, int[]dist, int countvertex)
        {
            int min = 10000000;
            int index = 0;

            for (int i = 0; i < countvertex; i++)
            {
                if (!visit[i] && dist[i] < min)
                {
                    min = dist[i];
                    index = i;
                }
            }

            return index;
        }
        void PrintToLabel(int[] dist, int countvertex)
        {
            string str1 = $"Выбрана вершина источник - {cmb.Text}\n";
            string str = "Вершина \t Расстояние от начальной вершины\n";

            for (int i = 0; i < countvertex; i++)
            {
                str += $"{i}\t\t{dist[i]}\n";
            }
            
            lb.Content = str1+str;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<TextBox> data = new List<TextBox>();
            for(int i = 0;i<64;++i)
            {
                data.Add((TextBox)FindName($"BX{i + 1}"));
            }
            int count = 0;
            int[,] matrix = new int[8,8];
            for (int i = 0;i<8;++i)
            {
                for(int j = 0;j<8;++j)
                {
                    if (int.TryParse(data[count].Text, out int value))
                    {
                        matrix[i, j] = value;
                        count++;
                    }
                }
            }

            int start;
            switch (cmb.Text)
            {
                case "A":
                    start = 0;
                    break;
                case "B":
                    start = 1;
                    break;
                case "C":
                    start = 2;
                    break;
                case "D":
                    start = 3;
                    break;
                case "E":
                    start = 4;
                    break;
                case "F":
                    start = 5;
                    break;
                case "G":
                    start = 6;
                    break;
                case "H":
                    start = 7;
                    break;
                default: start = 0;break;
            }

            Path(matrix, start);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            for (int i = 1; i <= 64; i++)
            {
                string textBoxName = "BX" + i;
                TextBox textBox = FindName(textBoxName) as TextBox;

                if (textBox != null)
                {
                    textBox.Text = Convert.ToString(0);
                }
            }

        }
    }
}
