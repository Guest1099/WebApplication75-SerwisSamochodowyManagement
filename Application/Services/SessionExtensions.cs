using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Application.Services
{
    public static class SessionExtensions
    {
        public static void SetStringObject<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetStringObject<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
