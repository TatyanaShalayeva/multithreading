using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Threading.Tasks;
using System.Net;

namespace MyEBookReader
{
    public partial class MainForm : Form
    {
        private string theEBook = "";

        public MainForm()
        {
            InitializeComponent();
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            // The Project Gutenberg EBook of A Tale of Two Cities, by Charles Dickens
            WebClient wc = new WebClient();
            wc.DownloadStringCompleted += (s, eArgs) => 
            { 
                theEBook = eArgs.Result;
                txtBook.Text = theEBook;
            };
            wc.DownloadStringAsync(new Uri("http://www.gutenberg.org/files/98/98-8.txt"));            
        }

        #region Do work in parallel!
        private void btnGetStats_Click(object sender, EventArgs e)
        {
            char[] separators = new char[] { ' ', '\n', '\r', '*', ',', '.','!', '?', ':', ';', '-', '"', '[', ']', '#', '(', ')', '/', '\\' };
            string[] words = txtBook.Text.Split(separators, StringSplitOptions.RemoveEmptyEntries); //TODO: Получить слова из книги theBook в виде массива
            string[] tenMostCommon = new string[10];
            string longestWord = string.Empty;

            //TODO: Запустить параллельно задачи Parallel.Invoke
            //FindTenMostCommon и FindLongestWord
            //...
            Parallel.Invoke(() => { longestWord = FindLongestWord(words); }, ()=> { tenMostCommon = FindTenMostCommon(words); });
            
            // Сейчас все задачи уже выполнены,
            // формируем строку для вывода в MessageBox
            StringBuilder bookStats = new StringBuilder("Ten Most Common Words are:\n");
            foreach (string s in tenMostCommon)
            {
                bookStats.AppendLine(s);
            }
            bookStats.AppendFormat("The longest word is: {0}", longestWord);
            bookStats.AppendLine();
            MessageBox.Show(bookStats.ToString(), "Book info");
        }
        #endregion

        #region Task methods.
        private string[] FindTenMostCommon(string[] words)
        {
            string[] tmp = new string[words.Length];
            for (int j = 0; j < words.Length; j++)
            {
                tmp[j] = words[j].ToLower();
            }

            Dictionary<string, int> dic = new Dictionary<string, int>();
            
            foreach (var s in tmp)
            {
                if (!dic.ContainsKey(s))
                {
                    dic.Add(s, 1);
                }
                else
                {
                    int x = 0;
                    foreach (var key in dic)
                    {
                        if (key.Key == s)
                        {
                            x = (int)key.Value + 1;
                        }
                    }
                    dic.Remove(s);
                    dic.Add(s, x);
                }
                    
            }

            //TODO:
            //var frequencyOrder = ...;
            //Используя LINQ найти 10 наиболее часто встречаемых слов

            var frequencyOrder = from word in dic
                                 orderby word.Value descending
                                 select word.Key;

            string[] commonWords = new string[10];

            int i = 0;

            foreach (var s in frequencyOrder)
            {
                commonWords[i] = s;
                i++;
                if (i == 10)
                    break;
            }
            return commonWords;
        }

        private string FindLongestWord(string[] words)
        {
            //TODO: Найти самое длинное слово
            //также можно использовать LINQ
            
            var maxlongArray = from word in words
                          orderby word.Length descending
                          select word;

            string maxlong = "";
            //for (var i = 0; i < words.Length; i++)
            //{
            //    if (words[i].Length > maxlong.Length)
            //        maxlong = words[i];
            //}

            int i = 0;
            foreach (var s in maxlongArray)
            {
                maxlong = s;
                i++;
                if (i == 1)
                    break;
            }
            return maxlong;
        }
        #endregion
    }
}
