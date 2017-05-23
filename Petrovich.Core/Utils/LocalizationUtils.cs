namespace Petrovich.Core.Utils
{
    public static class LocalizationUtils
    {
        public static string GetString(string key)
        {
            return Properties.Resources.ResourceManager.GetString(key);
        }
    }
}
