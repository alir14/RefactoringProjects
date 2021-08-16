namespace Core.DBLayer
{
    public interface INumberDataSet
    {
        string GetBaseNumberDataSet(double index);

        string GetTensNumberDataSet(double index);
    }
}
