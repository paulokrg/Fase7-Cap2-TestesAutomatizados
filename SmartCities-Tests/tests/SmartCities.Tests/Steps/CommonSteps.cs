using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json.Linq;
using RestSharp;
using TechTalk.SpecFlow;

namespace SmartCities.Tests.Steps
{
    [Binding]
    public class CommonSteps
    {
        private readonly ScenarioContext _ctx;

        public CommonSteps(ScenarioContext ctx)
        {
            _ctx = ctx;
        }

        [Given(@"que a API está disponível em ""([^""]*)""")]
        public void GivenApiDisponivel(string baseUrl)
        {
            var options = new RestClientOptions(baseUrl)
            {
                RemoteCertificateValidationCallback = (_, _, _, _) => true
            };
            _ctx["client"] = new RestClient(options);
        }

        [When(@"faço GET em ""([^""]*)""")]
        public async Task WhenGetRequest(string endpoint)
        {
            var client = (RestClient)_ctx["client"]!;
            var response = await client.GetAsync(new RestRequest(endpoint));
            _ctx["response"] = response;
        }

        [Then(@"o status code deve ser (.*)")]
        public void ThenStatusCode(int expectedCode)
        {
            var response = (RestResponse)_ctx["response"]!;
            ((int)response.StatusCode).Should().Be(expectedCode);
        }

        [Then(@"a resposta deve ser um array JSON não vazio")]
        public void ThenResponseIsNonEmptyArray()
        {
            var response = (RestResponse)_ctx["response"]!;
            var json = JArray.Parse(response.Content!);
            json.Should().NotBeEmpty();
        }
    }
}
