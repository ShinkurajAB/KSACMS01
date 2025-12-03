using Microsoft.AspNetCore.Components;
using SwitchCMS.Component.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.Component.CommonComponents
{
    public partial class AlertMessage
    {

        #region Parameters    
        [Parameter]
        public string ParmMessage { get; set; } = string.Empty;
        [Parameter]
        public SuccessorFailAlert ParmSucessOrFaild { get; set; }
        #endregion

        #region Object/Datatype Declarations
        string Message = string.Empty;
        SuccessorFailAlert SucessOrFaild;
        #endregion

        protected override Task OnParametersSetAsync()
        {
            Message = ParmMessage;
            SucessOrFaild = ParmSucessOrFaild;
            this.StateHasChanged();
            return base.OnParametersSetAsync();
        }
    }
}
