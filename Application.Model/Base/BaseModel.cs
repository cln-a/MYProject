namespace Application.Model
{
    public class BaseModel
    {
        protected void SetProperty<T1>(ref T1 property,T1 value) => property = value;
    }
}
