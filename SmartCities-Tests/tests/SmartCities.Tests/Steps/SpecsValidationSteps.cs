using System.IO;
using FluentAssertions;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Linq;
using TechTalk.SpecFlow;
using RestSharp;

namespace SmartCities.Tests.Steps
{
    [Binding]
    public class SpecsValidationSteps
    {
        private readonly ScenarioContext _ctx;

        public SpecsValidationSteps(ScenarioContext ctx)
        {
            _ctx = ctx;
        }

        [Then(@"a resposta deve obedecer ao schema ""([^""]*)""")]
        public void ThenValidateJsonSchema(string schemaFile)
        {
            var response = (RestResponse)_ctx["response"]!;
            var content = response.Content!;
            var schemaText = File.ReadAllText($"Specs/{schemaFile}");
            var schema = JSchema.Parse(schemaText);
            var json = JToken.Parse(content);
            json.IsValid(schema)
                .Should().BeTrue($"JSON não bate com o schema {schemaFile}");
        }
    }
}
