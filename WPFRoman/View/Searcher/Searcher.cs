using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFRoman.View.Searcher
{
    public static class Searcher
    {
        public static IEnumerable<IEnumerable<string>> LookUpRows(string s, params IEnumerable<string>[] rows)
        {
            return rows.Where(row => row.Contains(s)).ToList();
        }

        public static IEnumerable<IEnumerable<string>> LookUpRows(string s, IEnumerable<IEnumerable<string>> rows)
        {
            return rows.Where(row => row.Contains(s)).ToList();
        }
    }
}
