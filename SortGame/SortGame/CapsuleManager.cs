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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SortGame
{
    class CapsuleManager
    {
        Grid workspace;
        List<Capsule> capsules = new List<Capsule>();
        ColorManager cm = new ColorManager();
        AddCNT ci = new AddCNT();
        int max_row = 2, max_column = 7;
        int cap_up, caps_count;
        int row = 1; //2
        int column = 0; //7
        public CapsuleManager(Grid _workspace)
        {
            workspace = _workspace;
        }
        public void Spawn(int balls_count, int _caps_count)
        {
            ci.new_count(balls_count, _caps_count);
            capsules.Clear();
            column = 0;
            row = 1;
            caps_count = _caps_count;
            cm.Create(balls_count, caps_count);
            
            for(int i = 0; i < caps_count; i++)
            {
                caps_Spawn();
                if (i < caps_count - 2)
                {
                    for (int j = 0; j < balls_count; j++)
                    {
                        capsules[capsules.Count - 1].ball_Spwn(cm.color_Random(balls_count, caps_count - 2, i));
                    }
                }
            }
        }
        public void caps_Spawn()
        {
            if (row < max_row + 2)
            {
                capsules.Add(new Capsule());
                capsules[capsules.Count - 1].Spawn(workspace, row, column, ci.balls);
                capsules[capsules.Count - 1].balls_spc.MouseDown += Balls_spc_MouseDown;
                column += 1;
                if (column >= max_column)
                {
                    row += 2;
                    column = 0;
                }
            }
        }
        void cap_CHCK()
        {

            int num = 0;
            for(int i = 0; i < caps_count; i++)
            {
                if(capsules[i].balls.Count != 0)
                {
                    if(capsules[i].Check())
                    {
                        num += 1;
                    }
                }
            }
            
            if (num >= caps_count - 2)
            {
                workspace.Children.Clear();
                ci.g_continue();
                Spawn(ci.balls, ci.caps);
            }
        }
        private void Balls_spc_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Grid g = (Grid)sender;
            int row = Grid.GetRow(g);
            int column = Grid.GetColumn(g);

            int num = (row == 3) ? 7 : 0;
            switch(capsules[cap_up].up)
            {
                case true:
                    {
                        Ellipse eel = (Ellipse)workspace.Children[capsules.Count * 2];
                        if (num + column == cap_up || capsules[num + column].balls.Count == 0 || eel.Fill == capsules[num + column].balls.Peek().Fill)
                        {
                            UIElement _el = workspace.Children[capsules.Count * 2];
                            workspace.Children.Remove(_el);
                            capsules[num + column].ball_add(_el);
                            capsules[cap_up].up = false;
                            cap_CHCK();
                            //throw new NotImplementedException();
                        }
                    }
                    break;
                case false:
                    {
                        if (capsules[num + column].balls.Count != 0)
                        {
                            double height = capsules[num + column].balls.Peek().ActualHeight;
                            double width = capsules[num + column].balls.Peek().ActualWidth;

                            Ellipse el = capsules[num + column].ball_remove();

                            el.Height = height;
                            el.Width = width;
                            Grid.SetColumn(el, column);
                            Grid.SetRow(el, row - 1);

                            workspace.Children.Add(el);
                            capsules[num + column].up = true;
                            cap_up = num + column;
                        }
                    }
                    break;
            }
        }
    }
}
