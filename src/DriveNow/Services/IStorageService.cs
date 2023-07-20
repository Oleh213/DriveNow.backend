using System;
using DriveNow.Model;

namespace DriveNow.Services
{
    public interface IStorageService

    {
        Task<S3ResponseDto> UploadFileAsync(S3Object obj, AwsCredentials awsCredentialsValues);
    }
}