using Microsoft.AspNetCore.Components.Forms;
using System.Threading.Tasks;

namespace DCMS.SE.Services.IService
{
    public interface IFileUpload
    {
        Task<string> UploadFile(IBrowserFile file);

        bool DeleteFile(string fileName);
    }
}
