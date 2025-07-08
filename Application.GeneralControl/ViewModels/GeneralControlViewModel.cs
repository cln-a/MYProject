using Application.SemiAuto;

namespace Application.GeneralControl
{
    public class GeneralControlViewModel : BindableBase
    {
        [Unity.Dependency("Set")] 
        public SetParamsFactory SetParamsFactory { get; set; }
        
        [Unity.Dependency("Cur")]
        public CurParamsFactory CurParamsFactory { get; set; }

        [Unity.Dependency("Trigger")]
        public TriggerParamsFactory TriggerParamsFactory { get; set; }      

        public GeneralControlViewModel()
        {

        }
    }
}
