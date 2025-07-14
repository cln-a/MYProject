namespace Application.Journal
{
    public class LogEntry : BindableBase
    {
        private DateTime _timestamp;
        private string? _level;
        private string? _logger;
        private string? _message;

        public DateTime Timestamp { get => _timestamp; set => SetProperty(ref _timestamp, value); }
        public string? Level { get => _level; set => SetProperty(ref _level, value); }
        public string? Logger { get => _logger; set => SetProperty(ref _logger, value); }
        public string? Message { get => _message; set => SetProperty(ref _message, value); }

        public static LogEntry Prase(string line)
        {
            try
            {
                var parts = line.Split('|');
                if (parts.Length >= 4)
                {
                    return new LogEntry()
                    {
                        Timestamp = DateTime.Parse(parts[0]),
                        Level = parts[1],
                        Logger = parts[2],
                        Message = parts[3]
                    };
                }
            }
            catch(Exception ex)
            {
            }

            return null!;
        }
    }
}
