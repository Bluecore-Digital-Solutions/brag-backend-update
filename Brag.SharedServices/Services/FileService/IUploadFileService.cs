
using Brag.Domain.Model.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Brag.SharedServices.Services.FileService
{
    public interface IUploadFileService
    {
        Task<FileUploadDto> UploadCloudinaryFile(string FileContent);
        Task<FileUploadDto> UploadCloudinaryFileAsync(string FileContent, CancellationToken cancellationToken);
       // Task<object> DeleteFile(string FilePath);
    }
}
