using System;
using System.Collections.Generic;
using System.IO;
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
using Microsoft.Win32;
using SimplePolygonGenerator.Extensions;

namespace SimplePolygonGenerator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Point[] noktalar;
        int noktaSayisi = 3;
        double width, height;
        Point[] sortedPoints;
        Rectangle rectangle;
        Point nokta;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void sliderNokta_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            noktaSayisi = (int)sliderNokta.Value;
            txtNoktaSayisi.Text = string.Format("Poligon Nokta Sayisi: {0}", (int)noktaSayisi);
        }

        private void btnOlustur_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CanvasPoligon.Children.Clear();
                width = double.Parse(txtGenislik.Text);
                height = float.Parse(txtYukseklik.Text);
                
                
                noktalar = new Point[noktaSayisi];
                Random random = new Random();
                for (int i = 0; i < noktaSayisi; i++)
                {
                    noktalar[i] = new Point();
                    noktalar[i].X = random.NextDouble() * width;
                    noktalar[i].Y = random.NextDouble() * height;

                }
                Point merkezNokta = MerkezBul(noktalar);
                //Point merkezNokta = new Point(width / 2, height / 2);
                sortedPoints = noktalar.OrderBy(t => t.NoktaAcisi(merkezNokta)).ToArray();
                //var acilar = new double[noktaSayisi];
                //for (int i = 0; i < noktaSayisi; i++)
                //{
                //    acilar[i] = sortedPoints[i].NoktaAcisi(merkezNokta);
                //}
                Polyline simplePoligon = new Polyline();
                PointCollection noktaKolleksiyonu = new PointCollection(sortedPoints.AsEnumerable());
                simplePoligon.Points = noktaKolleksiyonu;
                //loopu kapatmak icin ilk noktayı ekliyoruz
                simplePoligon.Points.Add(noktaKolleksiyonu[0]);
                SolidColorBrush blackBrush = new SolidColorBrush();
                blackBrush.Color = Colors.Black;
                simplePoligon.Stroke = blackBrush;
                CanvasPoligon.Children.Add(simplePoligon);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Hata");
            }
            
        }

        private Point MerkezBul(Point[] noktalar)
        {
            double sumX=0, SumY=0;
            for (int i = 0; i < noktalar.Length; i++)
            {
                sumX += noktalar[i].X;
                SumY += noktalar[i].Y;
            }
            return new Point(sumX / noktalar.Length, SumY / noktalar.Length);
        }

        private void btnDosyayaYaz_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();

            if (dialog.ShowDialog().Value)
            {
                DosyayaKaydet(dialog.FileName);
            }
        }

        private void CanvasPoligon_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //MessageBox.Show( e.MouseDevice.GetPosition(CanvasPoligon).ToString());
            if (rectangle!=null)
            {
                CanvasPoligon.Children.Remove(rectangle);
            }
            nokta = e.GetPosition(CanvasPoligon);
            rectangle = new Rectangle();
            rectangle.Stroke = new SolidColorBrush(Colors.Black);
            rectangle.Fill = new SolidColorBrush(Colors.Black);
            rectangle.Width = 2;
            rectangle.Height = 2;
            Canvas.SetLeft(rectangle, nokta.X);
            Canvas.SetTop(rectangle, nokta.Y);
            CanvasPoligon.Children.Add(rectangle);
        }

        private void DosyayaKaydet(string path)
        {
            
            using (TextWriter tw = new StreamWriter(path))
            {
                tw.WriteLine(string.Format("{0} {1}",nokta.X,nokta.Y));

                for (int i = 0; i < sortedPoints.Length-1; i++)
                {
                    tw.WriteLine(string.Format("{0} {1}", sortedPoints[i].X, sortedPoints[i].Y));

                }
                tw.Write(string.Format("{0} {1}", sortedPoints[sortedPoints.Length - 1].X, sortedPoints[sortedPoints.Length - 1].Y));
            }
            
        }
    }
}
