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
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CapsuleManager cm;
        public MainWindow()
        {
            InitializeComponent();
            MinWidth = 700.0;
            cm = new CapsuleManager(Workspace);
            Retry.Click += Retry_Click;
            cm.Spawn(3, 4);
        }

        private void Retry_Click(object sender, RoutedEventArgs e)
        {
            Workspace.Children.Clear();
            cm.Spawn(3, 4);
            //throw new NotImplementedException();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            cm.caps_Spawn();
        }

        private void Main_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Workspace.Width = Width;
        }
    }
}
