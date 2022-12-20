using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace blazor_wasm.Client.Pages.DialogPages.DialogItems
{
    public partial class ProgressDialog
    {
        [Parameter]
        public ProgressDialogDTO? ProgressDto { get; set; }

        private MudDialog? MudDialog { get; set; }

        public string GetTitleContent()
        {
            if (ProgressDto is not null)
            {
                return $"processing {ProgressDto.Value} to {ProgressDto.MaxValue}";
            }
            return string.Empty;
        }
        public void Refresh()
        {
            StateHasChanged();
        }

        public void CloseDialog()
        {
            if (MudDialog is null)
            {
                throw new Exception("can't take a dialog");
            }

            if (ProgressDto is not null)
            {
                ProgressDto.Clear();
                ProgressDto.UnShow();
            }
        }
    }
}