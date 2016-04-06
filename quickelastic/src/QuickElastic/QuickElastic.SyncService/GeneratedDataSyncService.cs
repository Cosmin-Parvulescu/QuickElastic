using System;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Timers;
using QuickElastic.Core.DataProviders;
using QuickElastic.Core.Domain;
using QuickElastic.Data;

namespace QuickElastic.SyncService
{
    public class GeneratedDataSyncService
    {
        private const int UserTimerInterval = 10000;

        private readonly DbContext _dbContext;

        private readonly IElasticIndexer<User> _userElasticIndexer;
        private readonly IDataProvider<User> _userDataProvider;

        private Timer _userTimer;

        public GeneratedDataSyncService(DbContext dbContext, IElasticIndexer<User> userElasticIndexer, IDataProvider<User> userDataProvider)
        {
            _dbContext = dbContext;

            _userElasticIndexer = userElasticIndexer;
            _userDataProvider = userDataProvider;
        }

        public void SyncUsers()
        {
            if (_userTimer != null && _userTimer.Enabled)
                StopSyncUsers();

            _userTimer = new Timer(UserTimerInterval);

            _userTimer.Elapsed += UserTimerOnElapsed;
            _userTimer.Start();
        }

        public void StopSyncUsers()
        {
            _userTimer.Stop();
            _userTimer.Dispose();
        }

        private async void UserTimerOnElapsed(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            var users = (await _userDataProvider.GetData()).ToList();

            try
            {
                // Write to Database
                _dbContext.Set(typeof(User)).AddRange(users);
                _dbContext.SaveChanges();

                // Write to ElasticSearch
                _userElasticIndexer.Index(users);

                Debug.WriteLine("[{0}] Persisted and indexed {1} entities.", DateTime.Now, users.Count);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
