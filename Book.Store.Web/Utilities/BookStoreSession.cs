using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Book.Store.Web.Utilities
{
    public class BookStoreSession
    {
        private readonly IDistributedCache _distributedCache;
        private const string SessionPrefix = "BStoreSession";

        public BookStoreSession(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        private string GetSessionKey(string key)
        {
            return $"{SessionPrefix}{key}";
        }

        public void SetObjectAsJson<T>(string key, T value)
        {
            var serializedValue = JsonConvert.SerializeObject(value);
            var sessionKey = GetSessionKey(key);
            _distributedCache.SetString(sessionKey, serializedValue);
        }

        public T GetObjectFromJson<T>(string key)
        {
            var sessionKey = GetSessionKey(key);
            var serializedValue = _distributedCache.GetString(sessionKey);

            if (string.IsNullOrWhiteSpace(serializedValue))
            {
                return default(T);
            }

            return JsonConvert.DeserializeObject<T>(serializedValue);
        }

        public int UserType
        {
            get { return GetObjectFromJson<int>("RoleId"); }
            set { SetObjectAsJson("RoleId", value); }
        }

        public int UserId
        {
            get { return GetObjectFromJson<int>("UserId"); }
            set { SetObjectAsJson("UserId", value); }
        }

        public string UserName
        {
            get { return GetObjectFromJson<string>("UserName"); }
            set { SetObjectAsJson("UserName", value); }
        }
    }
}
