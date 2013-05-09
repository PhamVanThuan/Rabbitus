namespace Rabbitus
{
    public interface IBuilder<out TOutput> 
        where TOutput : class
    {
        TOutput Build();
    }
}