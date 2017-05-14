using MonkeyHubApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MonkeyHubApp.Services
{
    interface IMonkeyHubApiService
    {
        Task<List<Tag>> GetTagsAsync();
        Task<List<Content>> GetContentsByTagIdAsync(string tagId);
        Task<List<Content>> GetContentsByFilterAsync(string filter);
    }
}
