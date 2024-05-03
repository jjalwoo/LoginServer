using StackExchange.Redis;

namespace LoginServer.Redis
{
    public class Redis
    {
        ConnectionMultiplexer _redisConn;
        IDatabase _redisDb;

        public Redis()
        {
            _redisConn = ConnectionMultiplexer.Connect("localhost");
            _redisDb = _redisConn.GetDatabase();
        }

        //public void SetKeyValue(string key, string value)
        //{
        //    _redisDb.StringSet(key, value);
        //}

        //public string GetValue(string key)
        //{            
        //    return _redisDb.StringGet(key);
        //}
    }
}
