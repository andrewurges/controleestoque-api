using System;
using System.Globalization;

namespace ControleEstoque.Api.Helpers
{
    public class DateHelper
    {
        public static DateTime GetDateUTC(string data)
        {
            var dataConvertida = Convert.ToDateTime(DateTime.ParseExact(
                DateTime.Parse(data).ToString("dd/MM/yyyy"), 
                "dd/MM/yyyy", 
                CultureInfo.CreateSpecificCulture("pt-BR")));
            return new DateTime(dataConvertida.Year, dataConvertida.Month, dataConvertida.Day, 0, 0, 0, DateTimeKind.Utc);
        }

        public static DateTime GetCurrentDateTime()
        {
            return TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Utc);
        }
    }
}
