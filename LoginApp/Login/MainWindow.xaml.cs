using Login.View;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Login
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoginPage loginPage = new LoginPage();
            InitializeLoginPage(loginPage);

        }

        private void InitializeLoginPage(LoginPage loginPage)
        {
            loginPage.ShowDialog();
            string username = loginPage._username;

            var users = File.ReadAllLines(loginPage.FilePath).Skip(1).Select(line => line.Split(','));
            var user = users.FirstOrDefault(u => u[0] == username);

            usr.Text = username;
            fvq.Text = user[2];
            string userPhotoPath = user[3];

            string photoPath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), userPhotoPath);
            if (File.Exists(photoPath))
            {
                var bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(photoPath, UriKind.Absolute);
                bitmap.EndInit();

                ProfileImage.ImageSource = bitmap;
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            LoginPage loginPage = new LoginPage();
            loginPage.ShowDialog();
            usr.Text = loginPage._username;

        }
    }
}