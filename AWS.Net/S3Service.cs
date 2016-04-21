using System;
using System.Diagnostics;
using System.IO;
using Amazon.S3;
using Amazon.S3.Model;

namespace AWS.Net
{
    public class S3Service
    {
        private readonly AwsCredentials _credentials;
        private readonly string _bucketName;

        public S3Service(AwsCredentials credentials, string bucketName = "")
        {
            _credentials = credentials;
            _bucketName = bucketName;
        }

        public GetObjectResponse Download(string filename, string bucketName = null)
        {
            using (
                IAmazonS3 client = new AmazonS3Client(_credentials.Key, _credentials.Secret, _credentials.RegionEndpoint)
                )
            {
                var request = new GetObjectRequest
                {
                    BucketName = bucketName ?? this._bucketName,
                    Key = filename
                };

                var result = client.GetObject(request);
                return result;
            }
        }

        public PutObjectResponse Upload(string filename, Stream stream, S3CannedACL acl = null, string bucketName= null)
        {
            try
            {
                using (
                    IAmazonS3 client = new AmazonS3Client(_credentials.Key, _credentials.Secret,
                        _credentials.RegionEndpoint))
                {
                    var request = new PutObjectRequest
                    {
                        BucketName = bucketName ?? this._bucketName,
                        Key = filename,
                        InputStream = stream,
                        CannedACL = acl ?? S3CannedACL.PublicRead
                    };

                    var result = client.PutObject(request);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
                throw;
            }
            finally
            {
                stream.Dispose();
            }
        }
    }
}