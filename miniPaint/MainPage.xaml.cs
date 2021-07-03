using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

namespace miniPaint
{
    public sealed partial class MainPage : Page
    {
        
        Point punktPoczatkowy = new Point();
        bool czyRysuje = false;
        private Stack<Shape> listaUndo = new Stack<Shape>();
        public Shape shape;
        int tryb = 0;
        Line[] linia2 = new Line[1037600];
        int i = 0;
        int doUsuniecia = 0;
        int poczatek;
        int kolor = 0;
        double gruboscPedzla = 5;
        int[] flagaP = new int[100000];
        int[] flagaK = new int[100000];
        int nrLini = 0;
        Stack<UIElement> listaUndo2 = new Stack<UIElement>();

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void poleRysowania_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            nrLini++;
            poczatek = doUsuniecia;
            punktPoczatkowy = e.GetCurrentPoint(poleRysowania).Position;
            czyRysuje = true;
        }

        public void poleRysowania_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
           
            Line linia = new Line();
            linia.StrokeThickness = gruboscPedzla;
            linia.StrokeStartLineCap = PenLineCap.Round;
            linia.StrokeEndLineCap= PenLineCap.Round;
            if (kolor == 1)
            {
                linia.Stroke = new SolidColorBrush(Windows.UI.Colors.Blue);
            }
            if (kolor == 2)
            {
                linia.Stroke = new SolidColorBrush(Windows.UI.Colors.Green);
            }
            if (kolor == 3)
            {
                linia.Stroke = new SolidColorBrush(Windows.UI.Colors.Red);
            }
            if (kolor == 4)
            {
                linia.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
            }
            if (tryb == 1)
            {

                linia.X1 = punktPoczatkowy.X;
                linia.Y1 = punktPoczatkowy.Y;

                linia.X2 = e.GetCurrentPoint(poleRysowania).Position.X;
                linia.Y2 = e.GetCurrentPoint(poleRysowania).Position.Y;
            }

            if (tryb == 2)
            {

                linia.X1 = punktPoczatkowy.X;
                linia.Y1 = punktPoczatkowy.Y;

                linia.X2 = e.GetCurrentPoint(poleRysowania).Position.X;
                linia.Y2 = e.GetCurrentPoint(poleRysowania).Position.Y;

                punktPoczatkowy = e.GetCurrentPoint(poleRysowania).Position;
            }

            if (czyRysuje == true)
            {
                poleRysowania.Children.Add(linia);
                linia2[i] = linia;
                i++;
                doUsuniecia = i;
            }
        }

        private void poleRysowania_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            czyRysuje = false;
            flagaP[nrLini] = poczatek;
            flagaK[nrLini] = doUsuniecia;

        }

        private void rdbProsta_Checked(object sender, RoutedEventArgs e)
        {
            tryb = 1;
        }


        private void rdbDowolna_Checked(object sender, RoutedEventArgs e)
        {
            tryb = 2;
        }

        public void usunPoprzednie_Click(object sender, RoutedEventArgs e)
        {
            if (nrLini > -1)
            {
                for (int k = flagaP[nrLini]; k < flagaK[nrLini]; k++)
                    poleRysowania.Children.Remove(linia2[k]);
                nrLini--;
            }
        }

        private void czyszczenie_Click(object sender, RoutedEventArgs e)
        {
            if (nrLini > -1)
            {
                for (int k = 0; k < flagaK[nrLini]; k++)
                    poleRysowania.Children.Remove(linia2[k]);
            }
        }

        public void niebieski_Click(object sender, RoutedEventArgs e)
        {
            kolor = 1;
        }

        private void zielony_Click(object sender, RoutedEventArgs e)
        {
            kolor = 2;
        }

        private void czerwony_Click(object sender, RoutedEventArgs e)
        {
            kolor = 3;
        }

        private void czarny_Click(object sender, RoutedEventArgs e)
        {
            kolor = 4;
        }

        private void sldGrubosc_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            gruboscPedzla = sldGrubosc.Value;
        }


    }
}
