using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using App1.Constant;
using App1.Entity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace App1.Service
{
    class MemberServiceImp : MemberService
    {

        public Member GetInformation(string token)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Authorization", "Basic " + token);
                var responseContent = client.GetAsync(ApiUrl.GET_INFORMATION_URL).Result.Content.ReadAsStringAsync().Result;
                Member mem = Newtonsoft.Json.JsonConvert.DeserializeObject<Member>(responseContent);
                return mem;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }
        }

        public string Login(string username, string password)
        {
            try
            {
                //tạo đối tượng member login từ giá trị của form.
                var memberLogin = new MemberLogin()
                {
                    email = username,
                    password = password
                };
                // validate
                if (!ValidaTeMemberLogin(memberLogin))
                {
                    throw new Exception("Login fails!");
                }
                // lấy token từ api.
                var token = GetTokenFromApi(memberLogin);
                //lưu token ra file để dùng lại
                ReadTokenFromLocalStorage();
                return token;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }
        }

        public Member Register(Member member)
        {
            try
            {
                var httpClient = new HttpClient();
                //httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + token);
                HttpContent content = new StringContent(JsonConvert.SerializeObject(member), Encoding.UTF8,
                    "application/json");

                var httpRequestMessage = httpClient.PostAsync(ApiUrl.API_BASE_URL, content);
                var responseContent = httpRequestMessage.Result.Content.ReadAsStringAsync().Result;
                // parse member object
                var resMember = JsonConvert.DeserializeObject<Member>(responseContent);
                return resMember;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }
        }

        private String GetTokenFromApi(MemberLogin memberLogin)
        {
            // thực hiện request lên api lấy token về.
            var dataContent = new StringContent(JsonConvert.SerializeObject(memberLogin),
                Encoding.UTF8, "application/json");
            var client = new HttpClient();
            var responseContent = client.PostAsync(ApiUrl.LOGIN_URL, dataContent).Result.Content.ReadAsStringAsync().Result;
            var jsonJObject = JObject.Parse(responseContent);
            if (jsonJObject["token"] == null)
            {
                throw new Exception("Login fails");
            }
            return jsonJObject["token"].ToString();
        }

        public string ReadTokenFromLocalStorage()
        {
            try
            {
                Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
                Windows.Storage.StorageFile tokenFile = storageFolder.GetFileAsync("token.txt").GetAwaiter().GetResult();
                //Debug.WriteLine(tokenFile.Path);
                var token = Windows.Storage.FileIO.ReadTextAsync(tokenFile).GetAwaiter().GetResult();
                return token;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }
        }

        private bool ValidaTeMemberLogin(MemberLogin memberLogin)
        {
            return true;
        }

    }
}
