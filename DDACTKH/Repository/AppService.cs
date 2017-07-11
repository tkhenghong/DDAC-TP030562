using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.Azure.ActiveDirectory.GraphClient;
using Microsoft.Azure.ActiveDirectory.GraphClient.Extensions;
using DDACTKH.Interfaces;
using DDACTKH.Models;

namespace DDACTKH.Repository
{
    public class AppService : IEventsAppService
    {
        private string _currentUserName = string.Empty;

        //private IEventsRepository _repository;

        public AppService()
        {
        }

        public AppService(string userName)
        {
            this._currentUserName = userName;
            //this._repository = new EventsRepository(userName);
        }

        //public EventsAppService(IEventsRepository repository, string userName)
        //{
        //}

        async public Task<IEnumerable<ActiveDirectoryUser>> ActiveDirectoryUsersAsync()
        {
            List<ActiveDirectoryUser> activeDirectoryUsers = new List<ActiveDirectoryUser>();
            ActiveDirectoryClient client = Helpers.AuthenticationHelper.GetActiveDirectoryClient();
            IPagedCollection<IUser> pagedCollection = await client.Users.ExecuteAsync();
            if (pagedCollection != null)
            {
                do
                {
                    List<IUser> usersList = pagedCollection.CurrentPage.ToList();
                    foreach (IUser user in usersList)
                    {
                        ActiveDirectoryUser adUser = new Models.ActiveDirectoryUser();
                        adUser.ActiveDirectoryId = user.ObjectId;
                        adUser.FullName = user.DisplayName;
                        adUser.Position = user.JobTitle;
                        adUser.Location = user.City + ", " + user.State;
                        adUser.ImageUrl = "/Users/ShowThumbnail/" + user.ObjectId;
                        adUser.ObjectId = user.ObjectId;
                        activeDirectoryUsers.Add(adUser);
                    }
                    pagedCollection = await pagedCollection.GetNextPageAsync();
                } while (pagedCollection != null);
            }

            return activeDirectoryUsers;
        }

        //public bool RegisterUser(string activeDirectoryId, int eventId)
        //{
        //    return this._repository.RegisterUser(activeDirectoryId, eventId);
        //}
    }
}