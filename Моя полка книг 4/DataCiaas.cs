using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 using MySql.Data.MySqlClient;

namespace Моя_полка_книг_4
{

    class DataCiaas
    {
        MySqlConnectionStringBuilder connectionStr;
        MySqlConnection connection;
        public void CreateStrConnection()
        {
            connectionStr = new MySqlConnectionStringBuilder();
            connectionStr.Server = "localhost";
            connectionStr.UserID = "root";
            connectionStr.Password = "12345";

            connectionStr.Database = "database_bk";

            connection = new MySqlConnection(connectionStr.ToString());
        }

        public int AddBook(string title, string autor, string genre, string datecreate)
        {
            string CommandText = $"INSERT INTO books (title,genre,autor,datecreate) VALUES ('{title}','{genre}','{autor}',{datecreate});";
            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(CommandText, connection);
                command.ExecuteNonQuery();

            }
            catch (Exception error)
            {
                System.Windows.MessageBox.Show(error.Message);
            }
            connection.Close();




        }
        public List<Book> ReadBook()
        {
            List<Book> books = new List<Book>();

            string CommandText = $"SELECT * FROM books;";
            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(CommandText, connection);

                MySqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        books.Add(new Book()
                        {
                            idbooks = reader.GetInt32(0),
                            title = reader.GetString(1),
                            genre = reader.GetString(2),
                            autor = reader.GetString(3),
                            datecreate = reader.GetInt32(4)
                        });
                    }
                }

            }
            catch (Exception error)
            {
                System.Windows.MessageBox.Show(error.Message);
            }
            connection.Close();
            return books;
        }
        public void UpdBook(int Id, string newTitle, string newAuthor, string newGenre, int newDate)
        {
            string CommandText = $"UPDATE books SET Title = '{newTitle}', " +
                $"Genre = '{newGenre}', " +
                $"Author = '{newAuthor}', " +
                $"DateCreate = {newDate} WHERE idbooks = {Id};";
            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(CommandText, connection);

                command.ExecuteNonQuery();

            }
            catch (Exception error)
            {
                System.Windows.MessageBox.Show(error.Message);
            }


            connection.Close();
        }
        public void DelBook(int id)
        {
            string cmdtxt = $"DELETE FROM books WHERE idbooks={id}";
            try
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand(cmdtxt, connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            connection.Close();
        }
    }
}
