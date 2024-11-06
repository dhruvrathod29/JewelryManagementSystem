namespace JewelryManagementSystem.DAL
{
    public class CCommon
    {
        public static string NullOrEmptyToString(object obj)
        {
            // Check if obj is null
            if (obj == null)
            {
                return string.Empty;
            }

            // Check if obj is a string
            if (obj is string str)
            {
                return string.IsNullOrEmpty(str) ? string.Empty : str;
            }

            return string.Empty; // Return empty string if obj is null or not a string
        }

        public static int GetInt(object obj)
        {
            if (obj is int i)
            {
                return i;
            }

            // Handle the case where the object is a numeric type (e.g., double, float, decimal)
            if (obj is double || obj is float || obj is decimal)
            {
                return Convert.ToInt32(obj);
            }

            // Handle the case where the object is a string that can be parsed to an integer
            if (obj is string s && int.TryParse(s, out int result))
            {
                return result;
            }

            return 0;
        }

        public static DateTime NullOrDefaultDateTime(object obj)
        {
            if (obj is DateTime dateTime)
            {
                return dateTime; // Return the DateTime if it's valid
            }

            // Try to parse the object as a string to DateTime
            if (obj is string str && DateTime.TryParse(str, out var parsedDateTime))
            {
                return parsedDateTime; // Return the parsed DateTime if successful
            }

            return DateTime.MinValue; // Return DateTime.MinValue if null or invalid
        }

        public static string FormatDateTime(object obj)
        {
            var dateTime = NullOrDefaultDateTime(obj); // Use the previous function to get a DateTime

            // Format the DateTime to "dd-MMM-yyyy" and return it
            return dateTime != DateTime.MinValue
                ? dateTime.ToString("dd-MMM-yyyy hh:mm tt")
                : string.Empty; // Return empty string if it's DateTime.MinValue
        }
    }
}
