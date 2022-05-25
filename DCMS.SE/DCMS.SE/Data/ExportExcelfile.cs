using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace DCMS.SE.Data
{
    public static class ExportExcelfile
    {
        public static async Task SaveAsFileAsync(this IJSRuntime jS, string fileName, byte[] data, string type = "application/octet-stream")
        {
            await jS.InvokeAsync<object>("JSInteropExt.saveAsFile", fileName, type, Convert.ToBase64String(data));
        }
    }
}