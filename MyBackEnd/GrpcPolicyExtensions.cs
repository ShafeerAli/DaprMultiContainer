using Grpc.Core;
using Polly;
using System.Net;

namespace MyBackEnd
{
    public static class GrpcPolicyExtensions
    {
        private static readonly Func<HttpResponseMessage, bool> TransientHttpStatusCodePredicate = (HttpResponseMessage response) => GetStatusCode(response) == StatusCode.Unavailable || GetStatusCode(response) == StatusCode.ResourceExhausted;

        public static StatusCode? GetStatusCode(HttpResponseMessage response)
        {
            var headers = response.Headers;

            if (!headers.Contains("grpc-status") && response.StatusCode == HttpStatusCode.OK)
                return StatusCode.OK;

            if (headers.Contains("grpc-status"))
                return (StatusCode)int.Parse(headers.GetValues("grpc-status").First());

            return null;
        }
        //
       
        public static PolicyBuilder<HttpResponseMessage> HandleTransientHttpError()
        {
            return Policy<HttpResponseMessage>.Handle<Grpc.Core.RpcException>().OrTransientHttpStatusCode();
        }

        //
        // Summary:
        //     Configures the Polly.PolicyBuilder`1 to handle System.Net.Http.HttpClient requests
        //     that fail with System.Net.HttpStatusCodes indicating a transient failure.
        //     The System.Net.HttpStatusCodes configured to be handled are:
        //     • HTTP 5XX status codes (server errors)
        //     • HTTP 408 status code (request timeout)
        //
        // Returns:
        //     The Polly.PolicyBuilder`1 pre-configured to handle System.Net.Http.HttpClient
        //     requests that fail with System.Net.HttpStatusCodes indicating a transient failure.
        public static PolicyBuilder<HttpResponseMessage> OrTransientHttpStatusCode(this PolicyBuilder policyBuilder)
        {
            if (policyBuilder == null)
            {
                throw new ArgumentNullException("policyBuilder");
            }

            return policyBuilder.OrResult(TransientHttpStatusCodePredicate);
        }

        //
        // Summary:
        //     Configures the Polly.PolicyBuilder`1 to handle System.Net.Http.HttpClient requests
        //     that fail with conditions indicating a transient failure.
        //     The conditions configured to be handled are:
        //     • Network failures (as System.Net.Http.HttpRequestException)
        //     • HTTP 5XX status codes (server errors)
        //     • HTTP 408 status code (request timeout)
        //
        // Returns:
        //     The Polly.PolicyBuilder`1 pre-configured to handle System.Net.Http.HttpClient
        //     requests that fail with conditions indicating a transient failure.
        public static PolicyBuilder<HttpResponseMessage> OrTransientHttpError(this PolicyBuilder policyBuilder)
        {
            if (policyBuilder == null)
            {
                throw new ArgumentNullException("policyBuilder");
            }

            return policyBuilder.Or<Grpc.Core.RpcException>().OrTransientHttpStatusCode();
        }

        //
        // Summary:
        //     Configures the Polly.PolicyBuilder`1 to handle System.Net.Http.HttpClient requests
        //     that fail with System.Net.HttpStatusCodes indicating a transient failure.
        //     The System.Net.HttpStatusCodes configured to be handled are:
        //     • HTTP 5XX status codes (server errors)
        //     • HTTP 408 status code (request timeout)
        //
        // Returns:
        //     The Polly.PolicyBuilder`1 pre-configured to handle System.Net.Http.HttpClient
        //     requests that fail with System.Net.HttpStatusCodes indicating a transient failure.
        public static PolicyBuilder<HttpResponseMessage> OrTransientHttpStatusCode(this PolicyBuilder<HttpResponseMessage> policyBuilder)
        {
            if (policyBuilder == null)
            {
                throw new ArgumentNullException("policyBuilder");
            }

            return policyBuilder.OrResult(TransientHttpStatusCodePredicate);
        }

        //
        // Summary:
        //     Configures the Polly.PolicyBuilder`1 to handle System.Net.Http.HttpClient requests
        //     that fail with conditions indicating a transient failure.
        //     The conditions configured to be handled are:
        //     • Network failures (as System.Net.Http.HttpRequestException)
        //     • HTTP 5XX status codes (server errors)
        //     • HTTP 408 status code (request timeout)
        //
        // Returns:
        //     The Polly.PolicyBuilder`1 pre-configured to handle System.Net.Http.HttpClient
        //     requests that fail with conditions indicating a transient failure.
        public static PolicyBuilder<HttpResponseMessage> OrTransientHttpError(this PolicyBuilder<HttpResponseMessage> policyBuilder)
        {
            if (policyBuilder == null)
            {
                throw new ArgumentNullException("policyBuilder");
            }

            return policyBuilder.Or<Grpc.Core.RpcException>().OrTransientHttpStatusCode();
        }
    }
}
