namespace Application.Hailu
{
    public class ParameterOption : BaseOption
    {
        public string? ReadyFlagUri { get; set; }
        public string? RequestFlagUri { get; set; }
        public string? LengthUri { get; set; }
        public string? WidthUri { get; set; }
        public string? ThicknessUri { get; set; }
        public string? RemarkUri { get; set; }  
        public string? CountUri { get; set; }
        public string? OffLineFlagUri { get; set; }
        public string? MeasureOKFlagUri { get; set; }
        public string? MeasureErrorFlagUri { get; set; }   
        public string? IdentityToPLCUri { get;set; }
        public string? IdentityFromPLCUri {  get; set; }
    }
}
