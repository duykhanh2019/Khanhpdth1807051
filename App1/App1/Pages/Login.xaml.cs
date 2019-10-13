using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
using App1.Service;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace App1.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Login : Page
    {
        private string token;
        MemberServiceImp memberService;
        public Login()
        {
            this.InitializeComponent();
            memberService = new MemberServiceImp();
            //Lay token da luu vao file trong lan dang nhap truoc:
            var token = memberService.ReadTokenFromLocalStorage();
        }

        private void ButtonLogin_OnClick(object sender, RoutedEventArgs e)
        {
            var errors = new Dictionary<string, string>();
            MemberLogin mem = new MemberLogin
            {
                email = this.Email.Text,
                password = this.Password.Password
            };
            errors = mem.Validate();
            if (errors.Count == 0)
            {
                string token = memberService.Login(this.Email.Text, this.Password.Password);
                if (token == null)
                {
                    //show errors
                }
                else
                {
                    //show success
                    //Lay info tu API bang token
                    Member memberLogin = memberService.GetInformation(token);
                    Frame.Navigate(typeof(MySong));
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
            mes.ErrorMessage(errors, "Email", email_er);
            mes.ErrorMessage(errors, "Password", password_er);
        }

        private void RegisterButtonTextBlock_OnPointerPressed(object sender, PointerRoutedEventArgs e)
        {
            Frame.Navigate(typeof(Register));
        }
    }
}
