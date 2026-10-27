using System;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;

namespace ODC.S3PresignActions
{
    public class S3PresignHandler : IS3PresignHandler
    {
        // Public parameterless constructor required by ODC runtime instantiation
        public S3PresignHandler() { }

        public void GetPresignedURLforUpload(
            string accessKey, 
            string secretKey, 
            string region, 
            string bucketName, 
            string objectKey, 
            int expirationMinutes, 
            out string presignedUrl, 
            out string errorMessage)
        {
            GenerateUrl(accessKey, secretKey, region, bucketName, objectKey, HttpVerb.PUT, expirationMinutes, out presignedUrl, out errorMessage);
        }

        public void GetPresignedURLforDownload(
            string accessKey, 
            string secretKey, 
            string region, 
            string bucketName, 
            string objectKey, 
            int expirationMinutes, 
            out string presignedUrl, 
            out string errorMessage)
        {
            GenerateUrl(accessKey, secretKey, region, bucketName, objectKey, HttpVerb.GET, expirationMinutes, out presignedUrl, out errorMessage);
        }

        private static void GenerateUrl(
            string accessKey, 
            string secretKey, 
            string region, 
            string bucketName, 
            string objectKey, 
            HttpVerb verb, 
            int expirationMinutes, 
            out string presignedUrl, 
            out string errorMessage)
        {
            presignedUrl = string.Empty;
            errorMessage = string.Empty;

            try
            {
                var regionEndpoint = RegionEndpoint.GetBySystemName(region);
                var config = new AmazonS3Config { RegionEndpoint = regionEndpoint };

                using var client = new AmazonS3Client(accessKey, secretKey, config);
                
                var request = new GetPreSignedUrlRequest
                {
                    BucketName = bucketName,
                    Key = objectKey,
                    Verb = verb,
                    Expires = DateTime.UtcNow.AddMinutes(expirationMinutes > 0 ? expirationMinutes : 10)
                };

                presignedUrl = client.GetPreSignedURL(request);
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }
    }
}