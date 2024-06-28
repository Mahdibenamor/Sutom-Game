using System.Text.Json;

namespace Sutom.Core
{
    public static class Utils
    {
        public static int GenerateRandomId()
        {
            return new Random().Next(1, 1000000);
        }

        public static T? DeepCopy<T>(this T? obj)
        {
            var json = JsonSerializer.Serialize(obj);
            return JsonSerializer.Deserialize<T>(json);
        }
    }
}
