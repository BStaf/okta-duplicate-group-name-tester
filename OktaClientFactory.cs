using Microsoft.Extensions.Logging.Abstractions;
using Okta.Sdk;
using Okta.Sdk.Configuration;
using Okta.Sdk.Internal;

namespace DuplicateGroupTester;

public class OktaClientFactory
{
	private const int RequestTimeout = 30;

    public static IOktaClient Create(string apiUrl, string apiToken)
	{
		return new OktaClient(
				new OktaClientConfiguration
				{
					OktaDomain = $"https://{apiUrl}",
					Token = apiToken
				},
				DefaultHttpClient.Create(RequestTimeout, null, NullLogger.Instance),
				retryStrategy: new DefaultRetryStrategy(OktaClientConfiguration.DefaultMaxRetries, 0)
			);
    }
}
