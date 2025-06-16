using System.Timers;
using Application.Common;
using Application.Common.Utilities;

namespace Application.Modbus;

public class HeartBeatSlave
{
    private readonly IVariable _variable;
    private volatile bool _connected = true;
    private readonly System.Timers.Timer _timer;
    private DateTime _dateTime = DateTime.Now;
    
    public IVariable Variable => _variable;
    public HeartBeatSlaveOption Option { get; private set; }
    public int DeviceId => Option.DeviceId;
    public string DeviceName => Option?.DeviceName;
    public int TimeOut => Option.TimeOut;
    public bool Connected => _connected;

    public HeartBeatSlave(HeartBeatSlaveOption option)
    {
        Option = option;
        _timer = new System.Timers.Timer(Option.TimeOut) { AutoReset = true, Enabled = false };
        _timer.Elapsed += TimerOnElapsed;
        IO.TryGet(Option.VariablePath.Trim(), out _variable);
    }

    private void TimerOnElapsed(object? sender, ElapsedEventArgs e)
    {
        bool connected = DateTime.Now.DateToTicks() - _dateTime.DateToTicks() <= TimeOut;
        if (connected != _connected)
        {
            _connected = connected;
        }
    }
    
    public void Start() => _timer.Start();
    
    public void Stop() => _timer.Stop();
}