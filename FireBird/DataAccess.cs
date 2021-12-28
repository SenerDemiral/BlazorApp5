using FirebirdSql.Data.FirebirdClient;
using System.Data;
using Dapper;
using FireBird.Models;
using System.Diagnostics;

namespace FireBird
{
    public class DataAccess
    {
        private readonly string _connectionString;
        public IDbConnection _connection;

        public DataAccess(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("default");

            FbConnectionStringBuilder cnString = new FbConnectionStringBuilder();
            
            cnString.ConnectionString = _connectionString;
            cnString.Pooling = true;
            cnString.ConnectionLifeTime = 10;   // Pooling true ise Query kapatsa bile 10 san daha acik kaliyor
            
            var aaa = cnString.ToString();
            Console.WriteLine(aaa);
            _connection = new FbConnection(cnString.ToString());
            //_connection.Open();
            
        }

        //public void Deneme()
        //{
        //    Stopwatch sw = new();
        //    sw.Start(); 
        //    IEnumerable<UUmodel> uus = _connection.GetAll<UUmodel>();
        //    sw.Stop();
        //    Console.WriteLine("");
        //}


        public async Task Deneme2()
        {
            Stopwatch sw = new();
            sw.Start();
            var uus = await _connection.QueryAsync<UUmodel>("select FrtID, Ad, AdN from FRT");
            //var uus = _connection.Query<UUmodel>("select FrtID, Ad, AdN from FRT");
            //await Task.Delay(0);
            sw.Stop();
        }

        public void Deneme3()
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
        }
    }
}
