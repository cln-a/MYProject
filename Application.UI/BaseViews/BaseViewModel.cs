using AutoMapper;
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

        protected virtual void Initialize() { }
    }
}
