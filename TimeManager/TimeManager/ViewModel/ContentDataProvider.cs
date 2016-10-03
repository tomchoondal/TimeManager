using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimeManager.Helpers;
using TimeManager.Model;

namespace TimeManager.ViewModel
{
    public class ContentDataProvider : IContentDataProvider
    {
        #region Properties

        public static IContentDataProvider Instance { get; set; } 

        #endregion

        #region Fields

        private List<Session> allSessions;
        private readonly FileManager fileManager;

        #endregion

        #region Constructors
        static ContentDataProvider()
        {
            Instance = new ContentDataProvider();
        }
        public ContentDataProvider()
        {
            fileManager = new FileManager("content.json");
        }

        #endregion

        #region Methods

        public async Task<List<Session>> GetAllSessionsAsync()
        {
            if (allSessions == null)
            {
                allSessions = await GetSessionsAsync();
            }
            return allSessions;
        }

        private async Task<List<Session>> GetSessionsAsync()
        {
            List<Session> session = null;
            string content = await fileManager.GetTextAsync();
            if (content != null)
            {
                try
                {
                    session = JsonConvert.DeserializeObject<List<Session>>(content);
                }
                catch (Exception)
                {
                }
            }
            if (session == null)
            {
                session = new List<Session>();
            }
            return session;
        }

        public async Task SaveAllSessionsAsync(List<Session> sessions)
        {
            allSessions = sessions;
            if (allSessions != null)
            {
                string content = JsonConvert.SerializeObject(allSessions);
                await fileManager.SetTextAsync(content);
            }
        }

        #endregion
    }

    public interface IContentDataProvider
    {
        Task<List<Session>> GetAllSessionsAsync();

        Task SaveAllSessionsAsync(List<Session> sessions);
    }
}
