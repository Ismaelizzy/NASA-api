using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NASAApi
{



   
    public static class ErrorLogger
    {
        private static string docPath = @"C:\\Users\\izzym\\OneDrive\\Desktop\\Spring of 2024\\Server Side\\Doc2.docx";

        public static void LogError(Exception ex)
        {
            try
            {
                // Append the error message to the Word document
                using (StreamWriter streamWriter = new StreamWriter(docPath, true))
                {
                    streamWriter.WriteLine($"Timestamp: {DateTime.Now}");
                    streamWriter.WriteLine($"Error Message: {ex.Message}");
                    streamWriter.WriteLine($"Stack Trace: {ex.StackTrace}");
                    streamWriter.WriteLine("-------------------------------------------");
                }

                Console.WriteLine("Error logged to document successfully.");
            }
            catch (Exception e)
            {
                ErrorLogger.LogError(e);
                Console.WriteLine($"An error occurred while logging the error: {e.Message}");
            }
        }
    }
}