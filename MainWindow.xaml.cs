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
using System.Drawing;

namespace Kohonen
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        List<Point> punkty = new List<Point>();
        private void Pole_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Point punkt;
            punkt = Mouse.GetPosition(sender as IInputElement);
            Ellipse myEllipse = CreateEllipse(6, 6, punkt.X, punkt.Y, Color.FromRgb(0, 0, 0));
            Pole.Children.Add(myEllipse);
            punkty.Add(punkt);
        }


        Ellipse CreateEllipse(double width, double height, double desiredCenterX, double desiredCenterY, Color kolor)
        {
            Ellipse ellipse = new Ellipse { Width = width, Height = height };
            double left = desiredCenterX - (width / 2);
            double top = desiredCenterY - (height / 2);
            SolidColorBrush mySolidColorBrush = new SolidColorBrush();
            mySolidColorBrush.Color = kolor;
            ellipse.Fill = mySolidColorBrush;
            ellipse.Margin = new Thickness(left, top, 0, 0);
            return ellipse;
        }

        Line CreateLine(Point punkt1, Point punkt2)
        {
            Line myLine = new Line();
            myLine.Stroke = System.Windows.Media.Brushes.Black;
            myLine.X1 = punkt1.X;
            myLine.X2 = punkt2.X;
            myLine.Y1 = punkt1.Y;
            myLine.Y2 = punkt2.Y;
            myLine.HorizontalAlignment = HorizontalAlignment.Left;
            myLine.VerticalAlignment = VerticalAlignment.Center;
            myLine.StrokeThickness = 2;
            return myLine;
        }

        private void Kohonen_Click(object sender, RoutedEventArgs e)
        {
            Siec siec = new Siec(punkty.Count);
            siec.Learn(100000, punkty);
            Rysuj_Siec(siec.Neurony);
        }

        void Rysuj_Siec(List<Point> punkty)
        {
            Ellipse elipse;
            Line line;
            for (int i = 0; i < punkty.Count; i++)
            {
                elipse = CreateEllipse(10, 10, punkty[i].X, punkty[i].Y, Color.FromRgb(200, 0, 0));
                Pole.Children.Add(elipse);
            }
            for (int i = 1; i < punkty.Count; i++)
            {
                line = CreateLine(punkty[i - 1], punkty[i]);
                Pole.Children.Add(line);
            }

            line = CreateLine(punkty[punkty.Count-1], punkty[0]);
            Pole.Children.Add(line);
        }
    }
}
