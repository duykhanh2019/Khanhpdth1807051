﻿#pragma checksum "C:\Users\khanhSky\Desktop\Assigment Part1\Khanhpdth1807051\App1\App1\Pages\Login.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "472AC42B7F3542FEBBAE5BF6F26DD859"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace App1.Pages
{
    partial class Login : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.18362.1")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 2: // Pages\Login.xaml line 15
                {
                    this.Email = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 3: // Pages\Login.xaml line 16
                {
                    this.email_er = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 4: // Pages\Login.xaml line 19
                {
                    this.Password = (global::Windows.UI.Xaml.Controls.PasswordBox)(target);
                }
                break;
            case 5: // Pages\Login.xaml line 20
                {
                    this.password_er = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 6: // Pages\Login.xaml line 31
                {
                    this.RegisterButtonTextBlock = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                    ((global::Windows.UI.Xaml.Controls.TextBlock)this.RegisterButtonTextBlock).PointerPressed += this.RegisterButtonTextBlock_OnPointerPressed;
                }
                break;
            case 7: // Pages\Login.xaml line 27
                {
                    this.loginButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.loginButton).Click += this.ButtonLogin_OnClick;
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        /// <summary>
        /// GetBindingConnector(int connectionId, object target)
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.18362.1")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

