using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MusicHelpers.Type
{
    interface IMusicRAsync
    {
        Task<string> GetSongByIdRAsync(string id);
        Task<string> SearchRAsync(string name, int page);
        Task<string> GetLrcRAsync(string id);
    }
}
