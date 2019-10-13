using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using App1.Entity;
using Newtonsoft.Json;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace App1.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Upload : Page
    {
        private string postUrl = "https://2-dot-backup-server-003.appspot.com/api/v2/songs";

        public Upload()
        {
            this.InitializeComponent();
        }
        //validate submit
        private void Submit_OnClick(object sender, RoutedEventArgs e)
        {
            Boolean subimit = true;

            if (this.Name.Text.Equals(""))
            {
                this.erroName.Text = "Name is required!";
                subimit = false;
            }
            else
            {
                this.erroName.Text = "";
                subimit = true;
            }
            if (this.Thumbnail.Text.Equals(""))
            {
                this.erroThumbnail.Text = "Thumbnail is required!";
                subimit = false;
            }
            else
            {
                this.erroThumbnail.Text = "";
                subimit = true;
            }
            if (this.Link.Text.Equals(""))
            {
                this.erroLink.Text = "Link is required!";
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

        private void Reset()
        {
            this.Name.Text = "";
            this.Description.Text = "";
            this.Singer.Text = "";
            this.Author.Text = "";
            this.Thumbnail.Text = "";
            this.Link.Text = "";
        }
        private void ButtonUploadReset_OnClick(object sender, RoutedEventArgs e)
        {
            Reset();
        }
    }
}
