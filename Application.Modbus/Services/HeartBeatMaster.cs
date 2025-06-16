using Application.Common;

namespace Application.Modbus
{
    public class HeartBeatMaster
    {
        private IVariable _variable;
        private HeartBeatMasterOption _option;
        private ushort _curValue;
        private volatile bool _running;

        private IVariable Variable => _variable;

        private HeartBeatMasterOption Option
        {
            get
            {
                _option ??= new HeartBeatMasterOption()
                    {
                        DeviceName = "Default",
                        VariablePath = "",
                        HeartBeatInterval = 1000,
                        HeartBeatValues = [0, 1]
                    };
                return _option!;
            }
        }
        private ushort HearBeatValue
        {
            get
            {
                if (++_curValue > _option.HeartBeatValues[1])
                    _curValue = _option.HeartBeatValues[0];
                return _curValue;
            }
        }

        public HeartBeatMaster(HeartBeatMasterOption option)
        {
            _option = option;
            IO.TryGet(_option.VariablePath.Trim(),out _variable);
        }

        public void Start()
        {
            try
            {
                _running = true;
                ThreadPool.QueueUserWorkItem(WorkAction);
            }
            catch (Exception e)
            {
                //Logger
                _running = false;
                throw;
            }
        }

        public void Stop() => _running = false;

        private void WorkAction(object? state)
        {
            while (_running)
            {
                try
                {
                    try
                    {
                        Variable.WriteAnyValueEx(HearBeatValue);
                    }
                    catch(Exception e)
                    {
                        //Logger
                        throw;
                    }
                }
                catch (Exception e) 
                {
                    //Logger
                    Thread.Sleep(Option.HeartBeatInterval);
                }
            }
        }
    }
}
