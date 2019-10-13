using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.Constant
{
    class ApiUrl
    {
        public const string API_BASE_URL = "https://2-dot-backup-server-003.appspot.com/_api/v2";
        public const string GET_UPLOAD_TOKEN = "https://2-dot-backup-server-003.appspot.com/get-upload-token";
        public const string LOGIN_URL = API_BASE_URL + "/members/authentication";
        public const string GET_INFORMATION_URL = API_BASE_URL + "/members/information";
        public const string GET_FREE_SONG_URL = API_BASE_URL + "/songs/get-free-songs";
        public const string SONG_URL = API_BASE_URL + "/songs";
        public const string GET_MINE_SONG_URL = API_BASE_URL + "/songs/get-mine";
    }
}