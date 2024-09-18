using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
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
using System.Xml.Serialization;

namespace ListView
{
    class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
    }
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const string FILE_NAME = "people.xml";
        ObservableCollection<Person> people;
        public MainWindow()
        {
            InitializeComponent();
            people = new ObservableCollection<Person>();
            LoadPeopleFromFile();
            peopleListView.ItemsSource = people;
            buttonAdd.Click += ButtonAdd_Click;
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            string name = textBoxName.Text;
            int age = Convert.ToInt32(textBoxAge.Text);
            string email = textBoxEmail.Text;

            people.Add(new Person()
            {
                Name = name,
                Age = age,
                Email = email
            });

            SavePeopleToFile();

            textBoxName.Text = string.Empty;
            textBoxAge.Text = string.Empty;
            textBoxEmail.Text = string.Empty;
        }
        catch(Exception ex)
        {
            MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

        private void SavePeopleToFile()
        {
            try
            {
                using (FileStream fs = new FileStream(FILE_NAME, FileMode.Create))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Person>));
                    serializer.Serialize(fs, people);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void LoadPeopleFromFile()
        {
            if (File.Exists(FILE_NAME))
            {
                try
                {
                    using (FileStream fs = new FileStream(FILE_NAME, FileMode.Open))
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Person>));
                        people = (ObservableCollection<Person>)serializer.Deserialize(fs);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
    }
}