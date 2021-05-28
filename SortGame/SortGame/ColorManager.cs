using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SortGame
{
    class ColorManager
    {
        Random r = new Random();
        int col_cap = -1;
        Color_arr[] colors;
        List<Brush> brushes = new List<Brush>();
        public void Create(int balls_count, int _caps_count)
        {
            colors = new Color_arr[_caps_count];
            for (int i = 0; i < _caps_count; i++)     
                colors[i] = new Color_arr(balls_count);
            PickBrush(_caps_count);

        }
        public Brush color_Random(int balls_count, int _caps_count, int num)
        {
            int j;
            do
            {
                j = r.Next(0, _caps_count);
            } while (colors[j].Count >= balls_count || col_cap == j);
            if (_caps_count - 1 > num)
                col_cap = j;
            else
                col_cap = -1;
            colors[j].Color_Add(brushes[j]);
            if (j < brushes.Count)
                return brushes[j];
            return Brushes.Black;
        }
        public void PickBrush(int count)
        {
            brushes.Clear();
            Brush result = Brushes.Transparent;
            Random rnd = new Random();
            Type brushesType = typeof(Brushes);
            PropertyInfo[] properties = brushesType.GetProperties();
            for (int i = 0; i < count; i++)
            {
                int random = rnd.Next(properties.Length);
                brushes.Add((Brush)properties[random].GetValue(null, null));
            }
        }
    }
    class Color_arr
    {
        Brush[] col;
        int count = 0;
        public int Count
        {
            get
            {
                return count;
            }
        }
        public Color_arr(int count)
        {
            col = new Brush[count];
        }
        public void Color_Add(Brush c)
        {
            col[count] = c;
            count++;
        }
        
    }
}
