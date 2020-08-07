using System.Threading.Tasks;
using Amazon.SecurityToken;
using Amazon.SecurityToken.Model;

namespace CovidStatCruncher.Infrastructure.SecurityProvider
{
    public class AwsSecurityProvider : IAwsSecurityProvider
    {
        public async Task<Amazon.Runtime.SessionAWSCredentials> GetTemporaryCredentialsAsync()
        {
            using (var stsClient = new AmazonSecurityTokenServiceClient())
            {
                var getSessionTokenRequest = new GetSessionTokenRequest
                {
                    DurationSeconds = 7200 // seconds
                };

                GetSessionTokenResponse sessionTokenResponse =
                    await stsClient.GetSessionTokenAsync(getSessionTokenRequest);

                Credentials credentials = sessionTokenResponse.Credentials;

                var sessionCredentials =
                    new Amazon.Runtime.SessionAWSCredentials(credentials.AccessKeyId,
                        credentials.SecretAccessKey,
                        credentials.SessionToken);
                return sessionCredentials;
            }

        }
    }
}
