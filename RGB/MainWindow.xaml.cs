using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            rect_Canvas.Fill = new SolidColorBrush(Color.FromRgb(byte.Parse(txt_Red.Text), byte.Parse(txt_Green.Text), byte.Parse(txt_Blue.Text)));
         
            sli_Red.Value = byte.Parse(txt_Red.Text);
            sli_Green.Value = byte.Parse(txt_Green.Text);
            sli_Blue.Value = byte.Parse(txt_Blue.Text);

            lbl_Hex.Content = $"#{(byte)sli_Red.Value:X2}{(byte)sli_Green.Value:X2}{(byte)sli_Blue.Value:X2}";
        }

        private void chceckOverlap (TextBox txt)
        {
            if (int.TryParse(txt.Text, out int number))
            {
                if (number > 255)
                {
                    MessageBox.Show("Můžete zadávat pouze hodnoty v rozmezí 0-255", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);

                    //string firstTwoDigitsString = txt.Text.Substring(0, 2);

                    //if (int.TryParse(firstTwoDigitsString, out int firstTwoDigits))
                    //{
                    //    if (firstTwoDigits > 25)
                    //    {
                    //        txt.Text = txt.Text.Substring(0, 2);
                    //    }
                    //    else
                    //    {
                    //        txt.Text = txt.Text.Substring(0, 3);
                    //    }
                    //}

                    //txt.SelectionStart = txt.Text.Length;
                    //return;

                } 
                
            }
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, @"^\d$");
        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true; 
            }
        }

        private void txt_ChangedRed(object sender, TextChangedEventArgs e)
        { 
            byte red = byte.TryParse(txt_Red.Text, out red) ? red : (byte)0;

            if(txt_Red != null)
            {
                chceckOverlap(txt_Red);
            }
 

            SolidColorBrush solidColorBrush = rect_Canvas.Fill as SolidColorBrush;

            if (solidColorBrush != null)
            {
                Color color = solidColorBrush.Color;

                Color newColor = Color.FromRgb(red, color.G, color.B);

                rect_Canvas.Fill = new SolidColorBrush(newColor);

                if (sli_Red != null)
                {
                    sli_Red.Value = red;
                    lbl_Hex.Content = $"#{red:X2}{(byte)color.G:X2}{(byte)color.B:X2}";             
                }
            }
        }

        private void txt_ChangedGreen(object sender, TextChangedEventArgs e)
        {
       
            byte green = byte.TryParse(txt_Green.Text, out green) ? green : (byte)0;

            if (txt_Green != null)
            {
                chceckOverlap(txt_Green);
            }

            SolidColorBrush solidColorBrush = rect_Canvas.Fill as SolidColorBrush;

            if (solidColorBrush != null)
            {
                Color color = solidColorBrush.Color;

                Color newColor = Color.FromRgb(color.R, green, color.B);

                rect_Canvas.Fill = new SolidColorBrush(newColor);

                if (sli_Green != null)
                {
                    sli_Green.Value = green;
                    lbl_Hex.Content = $"#{(byte)color.R:X2}{green:X2}{(byte)color.B:X2}";
                }
            }
        }

        private void txt_ChangedBlue(object sender, TextChangedEventArgs e)
        {
   
            byte blue = byte.TryParse(txt_Blue.Text, out blue) ? blue : (byte)0;

            if (txt_Blue != null)
            {
                chceckOverlap(txt_Blue);
            }

            SolidColorBrush solidColorBrush = rect_Canvas.Fill as SolidColorBrush;

            if (solidColorBrush != null)
            {
                Color color = solidColorBrush.Color;

                Color newColor = Color.FromRgb(color.R, color.G, blue);

                rect_Canvas.Fill = new SolidColorBrush(newColor);
             
                if (sli_Blue != null)
                {
                    sli_Blue.Value = blue;
                    lbl_Hex.Content = $"#{(byte)color.R:X2}{(byte)color.G:X2}{blue:X2}";
                }
            }
        }

        private void sli_ValueChangedRed(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            
            byte red = (byte)sli_Red.Value;
         
            txt_Red.Text = $"{red}";
         
        }

        private void sli_ValueChangedGreen(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

            byte green = (byte)sli_Green.Value;

            txt_Green.Text = $"{green}";

        }

        private void sli_ValueChangedBlue(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

            byte blue = (byte)sli_Blue.Value;

            txt_Blue.Text = $"{blue}";

        }

    
    }
}