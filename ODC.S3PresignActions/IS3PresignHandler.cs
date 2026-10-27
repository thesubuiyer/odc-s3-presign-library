using OutSystems.ExternalLibraries.SDK;

namespace ODC.S3PresignActions
{
    [OSInterface(Description = "Generates S3 Presigned URLs for direct upload and download",
    IconResourceName = "ODC.S3PresignActions.Resources.s3actions.png")]
    public interface IS3PresignHandler
    {
        [OSAction(Description = "Generates a Presigned URL for uploading a file directly to S3")]
        void GetPresignedURLforUpload(
            string accessKey, 
            string secretKey, 
            string region, 
            string bucketName, 
            string objectKey, 
            int expirationMinutes, 
            [OSParameter(Description = "Generated secure S3 upload URL")] out string presignedUrl, 
            [OSParameter(Description = "Error message if generation fails")] out string errorMessage);

        [OSAction(Description = "Generates a Presigned URL for downloading a file directly from S3")]
        void GetPresignedURLforDownload(
            string accessKey, 
            string secretKey, 
            string region, 
            string bucketName, 
            string objectKey, 
            int expirationMinutes, 
            [OSParameter(Description = "Generated secure S3 download URL")] out string presignedUrl, 
            [OSParameter(Description = "Error message if generation fails")] out string errorMessage);
    }
}