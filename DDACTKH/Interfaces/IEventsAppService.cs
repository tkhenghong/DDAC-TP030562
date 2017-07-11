using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using DDACTKH.Models;

namespace DDACTKH.Interfaces
{
    public interface IEventsAppService
    {
        Task<IEnumerable<ActiveDirectoryUser>> ActiveDirectoryUsersAsync();
    }
}