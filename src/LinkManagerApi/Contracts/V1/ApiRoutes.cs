namespace LinkManagerApi.Contracts.V1
{
    public static class ApiRoutes
    {
        private const string ApiVersion="v1";
        private const string rootUrl ="api";
        private const string BaseUrl = $"{rootUrl}/{ApiVersion}";

        public static class Links
        {
            public const string GetAll = $"{BaseUrl}/Links";
            public const string Get = $"{BaseUrl}/Links/{{id}}";
            public const string Create = $"{BaseUrl}/Links";
            public const string Update = $"{BaseUrl}/Links/{{id}}";
            public const string Delete = $"{BaseUrl}/Links/{{id}}";
            
        }
    }
}