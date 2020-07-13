using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Amazon.Runtime;

namespace CovidStatCruncher.Infrastructure.SecurityProvider
{
    public interface IAwsSecurityProvider
    {
        Task<SessionAWSCredentials> GetTemporaryCredentialsAsync();
    }
}
