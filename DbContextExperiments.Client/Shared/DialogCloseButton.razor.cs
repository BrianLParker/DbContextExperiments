// ----------------------------------------------------
// Copyright ©️ 2022, Brian Parker. All rights reserved.
// ----------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace DbContextExperiments.Client.Shared;

public partial class DialogCloseButton : ComponentBase
{
    [CascadingParameter]
    public Dialog Dialog { get; set; }

    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object> CapturedAttributes { get; set; } = new Dictionary<string, object>
    {
        { "class", "btn btn-close" }
    };

    private async Task CloseDialog() => await Dialog.CloseDialogAsync();
}
