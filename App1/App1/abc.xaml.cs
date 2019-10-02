using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using App1.Entity;
using Newtonsoft.Json;
using System.Threading.Tasks;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace App1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class abc : Page
    {
        private string postUrl = "https://2-dot-backup-server-003.appspot.com/_api/v2/songs/post-free";
        public abc()
        {
            this.InitializeComponent();
        }

        private void Submit_OnClick(object sender, RoutedEventArgs e)
        {
            Boolean subimit = true;

            if (this.Name.Text.Equals(""))
            {
                this.erroName.Text = "Name is requaired!";
                subimit = false;
            }
            else
            {
                this.erroName.Text = "";
                subimit = true;
            }
            if (this.Thumbnail.Text.Equals(""))
            {
                this.erroThumbnail.Text = "Thumbnail is requaired!";
                subimit = false;
            }
            else
            {
                this.erroThumbnail.Text = "";
                subimit = true;
            }
            if (this.Link.Text.Equals(""))
            {
                this.erroLink.Text = "Link is requaired!";
                subimit = false;
            }
            else
            {
                this.erroLink.Text = "";
                subimit = true;
            }

            if (subimit)
            {
                Song newSong = new Song()
                {
                    name = this.Name.Text,
                    description = this.Description.Text,
                    singer = this.Singer.Text,
                    author = this.Author.Text,
                    thumbnail = this.Thumbnail.Text,
                    link = this.Link.Text
                };
                var httpClient = new HttpClient();
                //httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + token);
                HttpContent content = new StringContent(JsonConvert.SerializeObject(newSong), Encoding.UTF8,
                    "application/json");

                Task<HttpResponseMessage> httpRequestMessage = httpClient.PostAsync(postUrl, content);
                String responseContent = httpRequestMessage.Result.Content.ReadAsStringAsync().Result;
                Debug.WriteLine("Response: " + responseContent);
            }
            
        }
    }
}
