using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace App1.Service
{
    class ValidateMessage
    {
        public void ErrorMessage(Dictionary<string, string> errors, string filed, TextBlock tb)
        {
            if (errors.ContainsKey(filed))
            {
                tb.Visibility = Visibility.Visible;
                tb.Text = errors[filed];
            }
            else
            {
                tb.Visibility = Visibility.Collapsed;
            }
        }
    }
}
