using Application.SemiAuto;

namespace Application.GeneralControl
{
    public class SemiAutoViewModel : BindableBase
    {
        [Unity.Dependency("Cur")] public CurParamsFactory? CurParamsFactory { get; set; }

        public SemiAutoViewModel()
        {
        }
    }
}
