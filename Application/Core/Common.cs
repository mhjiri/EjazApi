using System;
namespace Application.Core
{
	public static class Common
	{
		public static string GetMonthText(int Month)
		{
			string Res = String.Empty;

			switch(Month)
			{
				case 1:
					Res = "Jan";
					break;
                case 2:
                    Res = "Feb";
                    break;
                case 3:
                    Res = "Mar";
                    break;
                case 4:
                    Res = "Apr";
                    break;
                case 5:
                    Res = "May";
                    break;
                case 6:
                    Res = "Jun";
                    break;
                case 7:
                    Res = "Jul";
                    break;
                case 8:
                    Res = "Aug";
                    break;
                case 9:
                    Res = "Sep";
                    break;
                case 10:
                    Res = "Oct";
                    break;
                case 11:
                    Res = "Nov";
                    break;
                case 12:
                    Res = "Dec";
                    break;
            }

			return Res;
		}

        public static int GetAge(DateTime Date)
        {
            DateTime today = DateTime.Today;
            int age = today.Year - Date.Year;
            if (Date.Date > today.AddYears(-age)) age--;
            return age;
        }
	}
}

