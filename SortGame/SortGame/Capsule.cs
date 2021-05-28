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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SortGame
{
    class Capsule
    {
        Rectangle caps;
        internal Grid balls_spc;
        public Stack<Ellipse> balls = new Stack<Ellipse>();
        int max_balls_count, balls_count = 0;
        public bool up = false;
        public void Spawn(Grid workspace, int row, int column, int _balls_count)
        {
            caps = new Rectangle();
            //max 200 min 150
            double h = (200 / _balls_count > 50) ? 50 : 200 / _balls_count;
            caps.Height = h * _balls_count + 5;
            // max 50
            caps.Width = h + 5;
            caps.Stroke = Brushes.Black;
            Grid.SetColumn(caps, column);
            Grid.SetRow(caps, row);
            workspace.Children.Add(caps);
            max_balls_count = _balls_count;
            grid_Spwn(workspace, row, column);
        }
        public void ball_add(UIElement el)
        {
            Ellipse _el = (Ellipse)el;
            balls.Push(_el);
            Grid.SetRow(_el, max_balls_count - (balls_count + 1));
            balls_count += 1;
            balls_spc.Children.Add(balls.Peek());
            up = false;
        }
        public bool Check()
        {
            int n = 1;
            Ellipse el = (Ellipse)balls_spc.Children[0];
            Brush b = el.Fill;
            for (int i = 1; i < balls_spc.Children.Count; i++)
            {
                Ellipse _el = (Ellipse)balls_spc.Children[i];
                n += (b == _el.Fill) ? 1 : 0;
                
            }
            //throw new NotImplementedException();
            if (n >= max_balls_count)
            {
                return true;
            }
            return false;
        }
        public Ellipse ball_remove()
        {
            balls_count -= 1;
            balls_spc.Children.Remove(balls.Peek());
            return balls.Pop();
        }
        void grid_Spwn(Grid workspace, int row, int column)
        {
            balls_spc = new Grid();
            balls_spc.Background = Brushes.AliceBlue;
            balls_spc.Height = caps.Height - 5;
            balls_spc.Width = caps.Width - 5;
            
            for(int i = 0; i < max_balls_count; i++)
            {
                balls_spc.RowDefinitions.Add(new RowDefinition());
            }

            balls_spc.ShowGridLines = false;
            Grid.SetColumn(balls_spc, column);
            Grid.SetRow(balls_spc, row);
            workspace.Children.Add(balls_spc);
        }
        public void ball_Spwn(Brush br)
        {
            if(balls_count < max_balls_count)
            {
                Ellipse el = new Ellipse();
                el.Fill = br;
                Grid.SetRow(el, max_balls_count - (balls_count + 1));
                balls.Push(el);
                balls_count += 1;
                balls_spc.Children.Add(balls.Peek());
            }
        }
    }
}
