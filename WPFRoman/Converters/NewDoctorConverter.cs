using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using WPFRoman.ViewModels.Doctors;

namespace WPFRoman.Converters
{
    public class NewDoctorConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var q = Models.Qualification.Intern;
            Enum.TryParse((string)values[2], out q);

           return new DoctorViewModel()
            {
                Name = (string)values[0],
                Surname = (string)values[1],
                Qualification = q,
            };
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
