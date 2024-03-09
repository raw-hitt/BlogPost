namespace WebApplication1.Data
{
    public class ExceptionLogs
    {
        private readonly string LogPath;

        public ExceptionLogs(string exceptionMessage)
        {
            string tempFolderPath = Path.Combine(Path.GetTempPath(), "BlogExceptionLogs");
            Directory.CreateDirectory(tempFolderPath); 
            LogPath = Path.Combine(tempFolderPath, $"ErrorLog.txt");
            LogException(exceptionMessage);
        }

        private void LogException(string exceptionMessage)
        {
            try
            {
                using (StreamWriter writer = File.AppendText(LogPath))
                {
                    writer.WriteLine($"Timestamp: {DateTime.Now}");
                    writer.WriteLine($"Message: {exceptionMessage}");
                    writer.WriteLine(new string('-', 50));
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
