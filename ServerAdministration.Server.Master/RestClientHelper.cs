using RestSharp;

namespace ServerAdministration.Server.Master
{
    public class RestClientHelper
    {
        public static IRestResponse<TResponse> Post<TInput, TResponse>(string address, TInput requestedData) where TInput : new() where TResponse : new()
        {
            return Send<TResponse>(address, Method.POST, requestedData);
        }

        public static IRestResponse<TResponse> Post<TResponse>(string address, object requestedData, string bearerToken = null) where TResponse : new()
        {
            return Send<TResponse>(address, Method.POST, requestedData, bearerToken);
        }

        public static IRestResponse<TResponse> Get<TInput, TResponse>(string address, TInput requestedData) where TInput : new() where TResponse : new()
        {
            return Send<TResponse>(address, Method.GET, requestedData);
        }
        private static IRestResponse<TResponse> Send<TResponse>(string fullAdress, Method method, object input = null, string bearerToken = null) where TResponse : new()
        {

            RestClient client = new RestClient();
            RestRequest request =
                new RestRequest(fullAdress, method)
                {
                    RequestFormat = DataFormat.Json
                };

            if (input != null)
                request.AddJsonBody(input);

            if (bearerToken != null)
                request.AddHeader("Authorization", $"Bearer {bearerToken}");

            return client.Execute<TResponse>(request);
        }
    }
}
