using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.Component.CommonComponents
{
    public partial class PaginationComponent
    {
        #region Parameter and Injections
        [Parameter]
        public int selected { get; set; }

        [Parameter]
        public EventCallback<int> SelectedPageChanged { get; set; }

        [Parameter]
        public int TotalCount { get; set; }

        #endregion


        #region Event Call Back when clicking
        private async Task OnPageChanged(int newPage)
        {
            selected = newPage;
            await SelectedPageChanged.InvokeAsync(newPage);
        }
        #endregion
    }
}
