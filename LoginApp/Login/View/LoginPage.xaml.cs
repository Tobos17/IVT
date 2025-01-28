using Microsoft.Win32;
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
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace Login.View
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Window
    {
        public string FilePath = "users.csv";
        public string _username { get; set; }
        private string profilePhotoPath { get; set; }
        

        public LoginPage()
        {
            InitializeComponent();
            EnsureCSVExists();
        }

        // Zajistí, že CSV soubor existuje a má záhlaví
        private void EnsureCSVExists()
        {
            if (!File.Exists(FilePath))
            {
                File.WriteAllText(FilePath, "Username, Password, FavoriteQuote, ProfilePhotoPath\n");
            }
        }

        // Přihlášení
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameLogin.Text;
            string password = PasswordLogin.Password;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                LoginStatus.Text = "Please fill in all fields.";
                return;
            }

            var users = File.ReadAllLines(FilePath).Skip(1).Select(line => line.Split(','));
            var user = users.FirstOrDefault(u => u[0] == username);

            if (user == null)
            {
                LoginStatus.Text = "Username does not exist.";
            }
            else if (user[1] != password)
            {
                LoginStatus.Text = "Incorrect password.";
            }
            else
            {
                LoginStatus.Text = "Login successful!";
                LoginStatus.Foreground = System.Windows.Media.Brushes.Green;
                _username = user[0];
                Close();
            }
        }

        // Registrace
        private void Register_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameRegister.Text;
            string password = PasswordRegister.Password;
            string favoriteQuote = FavoriteQuote.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(favoriteQuote))
            {
                RegisterStatus.Text = "Please fill in all fields.";
                return;
            }

            var users = File.ReadAllLines(FilePath).Skip(1).Select(line => line.Split(','));
            if (users.Any(u => u[0] == username))
            {
                RegisterStatus.Text = "Username already exists.";
                return;
            }

            if (profilePhotoPath == null)
            {
                RegisterStatus.Text = "Profile photo is required.";
                return;
            }

            File.AppendAllText(FilePath, $"{username},{password},{favoriteQuote},{profilePhotoPath}\n");
            RegisterStatus.Text = "Registration successful!";
            RegisterStatus.Foreground = System.Windows.Media.Brushes.Green;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            // Configure open file dialog box
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.FileName = "Image"; // Default file name
            dialog.DefaultExt = ".png"; // Default file extension
            dialog.Filter = "Obrázky (*.png;*.jpg;*.jpeg;*.bmp;*.gif)|*.png;*.jpg;*.jpeg;*.bmp;*.gif|Všechny soubory (*.*)|*.*"; // Filter files by extension

            // Show open file dialog box
            bool? result = dialog.ShowDialog();

            if (result != true)
            {
                return;
            }

            // Process open file dialog box results
            else if (result == true)
            {
                // Open document
                string sourceFilePath = dialog.FileName;

                // Cíl: složka Resources ve vašem projektu
                string projectPath = Directory.GetCurrentDirectory(); // Aktuální adresář aplikace
                string resourcesPath = System.IO.Path.Combine(projectPath, "Resources");

                // Vytvoření složky Resources, pokud neexistuje
                if (!Directory.Exists(resourcesPath))
                {
                    Directory.CreateDirectory(resourcesPath);
                }

                // Určení cílové cesty
                string fileName = System.IO.Path.GetFileName(sourceFilePath);
                string destinationFilePath = System.IO.Path.Combine(resourcesPath, fileName);

                // Kopírování souboru
                File.Copy(sourceFilePath, destinationFilePath, overwrite: true);

                profilePhotoPath = $"Resources/{fileName}";
               
            }
        }
    }
}
