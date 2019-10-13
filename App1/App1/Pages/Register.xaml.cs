using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Capture;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using App1.Constant;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using App1.Entity;
using App1.Service;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace App1.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Register : Page
    {
        private StorageFile photo;
        private string imgUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTO6b8UmHNot4Ra90A75-m6yRyuI03Q9SgvHgyiwsxHJIXXxJcL";
        MemberService memberService;
        public Register()
        {
            this.InitializeComponent();
            this.memberService = new MemberServiceImp();

            //Combobox gender:
            Dictionary<String, int> genders = new Dictionary<string, int>();
            genders.Add("", -1);
            genders.Add("Female", 0);
            genders.Add("Male", 1);
            this.Gender.ItemsSource = genders;
            this.Gender.SelectedValuePath = "Value";
            this.Gender.DisplayMemberPath = "Key";
            this.Gender.SelectedValue = -1;
        }


        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var member = new Member
            {
                firstName = "Dao",
                lastName = "Hung",
                password = "123456",
                address = "Hai Ba Trung",
                avatar = "https://i.ytimg.com/vi/MBtJdkiEhBk/maxresdefault.jpg",
                birthday = "2000-12-26",
                email = "hungdx1234567@gmail.com",
                gender = 1,
                introduction = "Hello T1807E",
                phone = "091234567"
            };
            // validate phía client.
            Debug.WriteLine(JsonConvert.SerializeObject(member));

            member = memberService.Register(member);
            if (member == null)
            {
                // show error
            }
            else
            {
                // show success
            }
        }
        private void Button_Cancel(object sender, RoutedEventArgs e)
        {

        }

        //private void Reset()
        //{
        //    this.FirstName.Text = "";
        //    this.Address.Text = "";
        //    this.Email.Text = "";
        //    this.LastName.Text = "";
        //    this.Introduction.Text = "";
        //    this.Password.Password = "";
        //    this.Phone.Text = "";
        //}
        //private void ButtonRegisterReset_Onclick(object sender, RoutedEventArgs e)
        //{
        //    Reset();
        //}

        private void ButtonRegister_OnClick(object sender, RoutedEventArgs e)
        {
            var errors = new Dictionary<string, string>();
            var member = new Member
            {
                firstName = this.FirstName.Text,
                lastName = this.LastName.Text,
                password = this.Password.Password,
                address = this.Address.Text,
                avatar = imgUrl,
                birthday = this.Birthday.Date.ToString("yyyy-MM-dd"),
                email = this.Email.Text,
                gender = (int) this.Gender.SelectedValue,
                introduction = this.Introduction.Text,
                phone = this.Phone.Text
            };
            Debug.WriteLine("Birthday: " + member.birthday);

            errors = member.Validate();
            if (errors.Count == 0)
            {
                var memberRes = memberService.Register(member);
                if (memberRes == null)
                {
                    //Show error
                }
                else
                {
                    //Show success
                    var token = memberService.Login(this.Email.Text, this.Password.Password);
                    Frame.Navigate(typeof(ListSong));
                }
            }
            else
            {
                ShowError(errors);
            }
        }
        private void ShowError(Dictionary<string, string> errors)
        {
            ValidateMessage mes = new ValidateMessage();
            mes.ErrorMessage(errors, "firstName", errorName);
            mes.ErrorMessage(errors, "lastName", errorLast);
            mes.ErrorMessage(errors, "phone", errorPhone);
            mes.ErrorMessage(errors, "address", errorAddress);
            mes.ErrorMessage(errors, "introduction", errorIntrodution);
            mes.ErrorMessage(errors, "gender", errorGender);
            mes.ErrorMessage(errors, "birthday", errorBirthday);
            mes.ErrorMessage(errors, "email", errorEmail);
            mes.ErrorMessage(errors, "password", errorPassword);
        }

        private async void Button_Photo(object sender, RoutedEventArgs e)
        {
            //Get upload url:
            FileServiceImp fileService = new FileServiceImp();
            var uploadUrl = fileService.GetUploadUrl(ApiUrl.GET_UPLOAD_TOKEN);

            CameraCaptureUI captureUI = new CameraCaptureUI();
            captureUI.PhotoSettings.Format = CameraCaptureUIPhotoFormat.Jpeg;
            captureUI.PhotoSettings.CroppedSizeInPixels = new Size(200, 200);

            this.photo = await captureUI.CaptureFileAsync(CameraCaptureUIMode.Photo);

            if (photo == null)
            {
                // User cancelled photo capture
                return;
            }
            HttpUploadFile(uploadUrl, "myFile", "image/png");
        }
        public async void HttpUploadFile(string url, string paramName, string contentType)
        {
            string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
            byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

            HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(url);
            wr.ContentType = "multipart/form-data; boundary=" + boundary;
            wr.Method = "POST";

            Stream rs = await wr.GetRequestStreamAsync();
            rs.Write(boundarybytes, 0, boundarybytes.Length);

            string header = string.Format("Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n", paramName, "path_file", contentType);
            byte[] headerbytes = System.Text.Encoding.UTF8.GetBytes(header);
            rs.Write(headerbytes, 0, headerbytes.Length);

            // write file.
            Stream fileStream = await this.photo.OpenStreamForReadAsync();
            byte[] buffer = new byte[4096];
            int bytesRead = 0;
            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
            {
                rs.Write(buffer, 0, bytesRead);
            }

            byte[] trailer = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
            rs.Write(trailer, 0, trailer.Length);

            WebResponse wresp = null;
            try
            {
                wresp = await wr.GetResponseAsync();
                Stream stream2 = wresp.GetResponseStream();
                StreamReader reader2 = new StreamReader(stream2);
                //Debug.WriteLine(string.Format("File uploaded, server response is: @{0}@", reader2.ReadToEnd()));
                //string imgUrl = reader2.ReadToEnd();
                //Uri u = new Uri(reader2.ReadToEnd(), UriKind.Absolute);
                //Debug.WriteLine(u.AbsoluteUri);
                //ImageUrl.Text = u.AbsoluteUri;
                //MyAvatar.Source = new BitmapImage(u);
                //Debug.WriteLine(reader2.ReadToEnd());
                string imageUrl = reader2.ReadToEnd();
                Debug.WriteLine(imageUrl);
                Avatar.Source = new BitmapImage(new Uri(imageUrl, UriKind.Absolute));
                imgUrl = imageUrl;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error uploading file", ex.StackTrace);
                Debug.WriteLine("Error uploading file", ex.InnerException);
                if (wresp != null)
                {
                    wresp = null;
                }
            }
            finally
            {
                wr = null;
            }
        }
    }
}
