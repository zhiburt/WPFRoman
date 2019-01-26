using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
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

namespace WPFRoman.View.Windows
{
    /// <summary>
    /// Interaction logic for SearchWindow.xaml
    /// </summary>
    public partial class SearchWindow : Window
    {
        public SearchWindow(IEnumerable<string> columns,  params IEnumerable<string>[] rows) : this()
        {
            if (rows == null || rows.Count() == 0)
            {
                ShowNotFound();
                return;
            }

            ShowColumns(columns.ToArray());
            ShowRows(rows.Cast<string[]>(), columns.ToArray());
        }

        private SearchWindow()
        {
            InitializeComponent();
        }

        private void ShowNotFound()
        {
            NotFound.Visibility = Visibility.Visible;
        }

        private void ShowColumns(IEnumerable<string> columns)
        {

            foreach (var column in columns)
            {
                SearchedList.Columns.Add(new DataGridTextColumn() {
                    Header = column,
                    Binding = new Binding($"{column}")
                });
            }
        }

        private void ShowRows(IEnumerable<string[]> rows, string[] columns)
        {
            foreach (var row in rows)
            {
                dynamic r = new ExpandoObject();
                for (int i = 0; i < columns.Count(); i++)
                {
                    var cell = row[i];
                    var columnName = columns[i];
                    ((IDictionary<String, Object>)r)[columnName] = cell;
                }

                SearchedList.Items.Add(r);
            }
        }
    }
}
