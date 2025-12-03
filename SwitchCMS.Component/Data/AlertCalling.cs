using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.Component.Data
{
    public static class AlertCalling
    {
        public static void CallAlert(IJSRuntime JS)
        {
            JS.InvokeVoidAsync("showAlert");
        }
    }
}
