using AutoMapper;
using HandyControl.Controls;
using Microsoft.Extensions.Logging;

namespace Application.UI
{
    public class BaseViewModel  :BindableBase
    {
        protected const string TitleKey = "title";
        protected const string ModelKey = "model";
        protected const string CommandTypeKey = "CommandType";

        [Unity.Dependency] public ILogger Logger { get; set; }
        [Unity.Dependency] public IMapper Mapper { get; set; }

        public void InfoGlobal(string message)
        {
            Logger.LogDebug(message);
            Growl.Info(message);
        }

        public void ErrorGlobal(string msg)
        {
            Logger.LogDebug(msg);
            Growl.Error(msg);
        }

        public void WarnGlobal(string msg)
        {
            Logger.LogDebug(msg);
            Growl.Warning(msg);
        }

        protected virtual void Initialize() { }
    }
}
