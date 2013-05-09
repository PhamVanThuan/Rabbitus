namespace Rabbitus.Configuration
{
    public static class ConfigurationExtensions
    {
         public static IRabbitus WithQueueNamed(this IRabbitus rabbitus, string queue)
         {
            return rabbitus;
         }
    }
}