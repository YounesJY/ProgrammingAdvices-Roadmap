public class Logger
{
    public delegate void LogAction(string message);
    private readonly LogAction _logAction;


    public Logger(LogAction action)
    {
        _logAction = action;
    }


    public void Log(string message)
    {
        _logAction(message);
    }
}
