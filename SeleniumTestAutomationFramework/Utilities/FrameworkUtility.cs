using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumTestAutomationFramework.Utilities
{
    class FrameworkUtility
    {
		public static void ZipFolderLocation(string pathToZip, string zipFileDestPath)
		{
			try
			{
				ZipFile.CreateFromDirectory(pathToZip, zipFileDestPath);
			}
			catch (Exception ex)
			{

				throw ex;
			}
		}

		public static bool CheckIfFolderContainsFiles(string pathToFolder, string format)
		{
			try
			{
				DirectoryInfo di = new DirectoryInfo(pathToFolder);
				return di.GetFiles("*." + format).Any();
			}
			catch (Exception ex)
			{

				throw ex;
			}
		}

		public static void DeleteAllFilesInFolder(string pathToFolder)
		{
			try
			{
				System.IO.DirectoryInfo di = new DirectoryInfo(pathToFolder);

				foreach (FileInfo file in di.GetFiles())
				{
					file.Delete();
				}
			}
			catch (Exception ex)
			{

				throw ex;
			}
		}

		//get project directory
		public static String GetProjectBaseDirectory()
		{
			String basePath = AppDomain.CurrentDomain.BaseDirectory;
			String reportPath = basePath.Substring(0, basePath.LastIndexOf('k') + 1);
			return reportPath;
		}

		//get test report directory
		public static String GetTestReportDirectory()
		{
			String basePath = AppDomain.CurrentDomain.BaseDirectory;
			String reportPath = basePath.Substring(0, basePath.LastIndexOf('k') + 1);
			return reportPath + @"\TestReport\";
		}

		//get test report back up directory
		public static String GetTestReportBackUpDirectory()
		{
			String basePath = AppDomain.CurrentDomain.BaseDirectory;
			String reportPath = basePath.Substring(0, basePath.LastIndexOf('k') + 1);
			return reportPath + @"\TestReport_Backup\";
		}

		public static string GetCurrentDate()
		{
			try
			{
				return DateTime.Today.Day.ToString();
			}
			catch (Exception ex)
			{

				throw ex;
			}
		}

		public static string GetCheckOutDate(string checkInDate, double numberOfDays)
		{
			try
			{
				DateTime date = Convert.ToDateTime(checkInDate);
				return (date.AddDays(numberOfDays)).ToString();
			}
			catch (Exception ex)
			{

				throw ex;
			}
		}

		public static string GetDayFromDate(string date)
		{
			try
			{
				DateTime datetime = Convert.ToDateTime(date);
				return datetime.Day.ToString();
			}
			catch (Exception ex)
			{

				throw ex;
			}
		}

		public static string GetMonthFromDate(string date)
		{
			try
			{
				DateTime datetime = Convert.ToDateTime(date);
				return datetime.Month.ToString("MMMM");
			}
			catch (Exception ex)
			{

				throw ex;
			}
		}

		public static bool DateTimeParser(string date)
		{
			try
			{
				DateTime result;
				if (DateTime.TryParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out result))
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			catch (Exception ex)
			{

				throw ex;
			}
		}
		public static Dictionary<string, string> GetSplitDate(string date)
		{
			try
			{
				DateTime dateToBeChecked = Convert.ToDateTime(date);
				Dictionary<string, string> splitDate = new Dictionary<string, string>();
				splitDate.Add("year", dateToBeChecked.Year.ToString());
				splitDate.Add("month", dateToBeChecked.ToString("MMMM"));
				splitDate.Add("day", dateToBeChecked.Day.ToString());

				return splitDate;
			}
			catch (Exception ex)
			{

				throw ex;
			}
		}
	}
}
