using FirebirdSql.Data.FirebirdClient;
using System.Data;
using Dapper;
using FireBird.Models;
using System.Diagnostics;
using Microsoft.Extensions.Caching.Memory;

namespace FireBird
{
    public class DataAccess
    {
        private readonly string _connectionString;
        public IDbConnection _connection;
        //public IEnumerable<UUmodel> uuData;
        private IMemoryCache _memoryCache;

        public DataAccess(IConfiguration config, IMemoryCache memoryCache)
        {
            _connectionString = config.GetConnectionString("default");
            _memoryCache = memoryCache;

            FbConnectionStringBuilder cnString = new FbConnectionStringBuilder();
            
            cnString.ConnectionString = _connectionString;
            cnString.Pooling = true;
            cnString.ConnectionLifeTime = 10;   // Pooling true ise Query kapatsa bile 10 san daha acik kaliyor
            
            var aaa = cnString.ToString();
            Console.WriteLine(aaa);
            _connection = new FbConnection(cnString.ToString());
            //_connection.Open();

            //uuData = Deneme3();
            
        }

        //public void Deneme()
        //{
        //    Stopwatch sw = new();
        //    sw.Start(); 
        //    IEnumerable<UUmodel> uus = _connection.GetAll<UUmodel>();
        //    sw.Stop();
        //    Console.WriteLine("");
        //}


        public async Task<IEnumerable<UUmodel>> Deneme2()
        {
            //Stopwatch sw = new();
            //sw.Start();
            return await _connection.QueryAsync<UUmodel>("select FrtID, Ad, AdN from FRT");
            //var uus = _connection.Query<UUmodel>("select FrtID, Ad, AdN from FRT");
            //await Task.Delay(0);
            //sw.Stop();
        }

        public IEnumerable<UUmodel> Deneme3()
        {
            Stopwatch sw = new();
            sw.Start();
            var uus = _connection.Query<UUmodel>("select FrtID, Ad, AdN from FRT");
            sw.Stop();
            //foreach(var u in uus)
            //{
            //    Console.WriteLine(u.ToString());
            //}

            //var aaa = new UUmodel();
            //UUmodel uus2 = new(frtID: 5, adN: "sener", ad: "SENER");
            return uus;
        }

        public IEnumerable<UUmodel> Deneme3Cache()
        {
            IEnumerable<UUmodel> output;

            output = _memoryCache.Get<IEnumerable<UUmodel>>("uu");
            if(output == null)
            {
                output = _connection.Query<UUmodel>("select FrtID, Ad, AdN from FRT");
                _memoryCache.Set("uu", output, TimeSpan.FromMinutes(10));
            }

            return output;
        }
    }
}
