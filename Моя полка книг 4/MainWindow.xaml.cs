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


namespace Моя_полка_книг_4
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataCiaas db = new DataCiaas();
        public MainWindow()
        {
            InitializeComponent();
            db.CreateStrConnection();
            dtBooks.ItemSource = db.ReadBook();   
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (db.AddBook(tbTitle.Text, tbAutor.Text, tbGenre.Text, tbData.Text) > 0)
            {
                MessageBox.Show("Данные успешно добавлены!");
            }
            else
            {
                MessageBox.Show("Данные не добавлены!");
            }
        }

        private void dtBooks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Book book = new Book();
            book = dtBooks.SelectedItem as Book;
            if (book != null)
            {
                tbTitle.Text = book.title;
                tbAutor.Text = book.autor;
                tbGenre.Text = book.genre;
                tbData.Text = book.datecreate.ToString();
                idBook = book.idbooks;

            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            db.UpdBook(idBook, tbTitle.Text, tbAutor.Text, tbGenre.Text, Convert.ToInt32(tbData.Text));
            dtBooks.ItemsSource = db.ReadBook();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            db.DelBook(idBook);
            dgdbBook.ItemSource = db.ReadBook();
        }
    }
}
