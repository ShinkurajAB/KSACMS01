using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.Component.CommonComponents
{
    public partial class DeleteConfirmModal
    {
        [Parameter]
        public string Message { get; set; } = string.Empty;

        [Parameter]
        public EventCallback<bool> OnSubmit { get; set; }


        private async void OnSubmitted()
        {
            await OnSubmit.InvokeAsync(true);
        }
    }
}
