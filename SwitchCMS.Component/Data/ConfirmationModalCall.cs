using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.Component.Data
{
    public static class ConfirmationModalCall
    {
        public static async void ShowModal(IJSRuntime JS)
        {
            await JS.InvokeVoidAsync("ShowDeleteConfirmationModal");
        }


        public static async void HideModal(IJSRuntime JS)
        {
            await JS.InvokeVoidAsync("HideDeleteConfirmationModal");
        }
    }
}
