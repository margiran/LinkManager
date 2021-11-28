using System;
using System.Text.Json.Serialization;

namespace LinkManagerClientCLI.Api.ApiResponse;

public record LinkSearchResponse
{
    [JsonPropertyName("id")]
    public Guid Id { get; init; }
    [JsonPropertyName("title")]
    public string? Title { get; init; }
    [JsonPropertyName("argument")]

    public string? Argument { get; init; }
    [JsonPropertyName("fileName")]

    public string? FileName { get; init; }
    [JsonPropertyName("internetNeeded")]

    public bool InternetNeeded { get; init; }
    [JsonPropertyName("shortDescription")]

    public string? ShortDescription { get; init; }
    [JsonPropertyName("description")]

    public string? Description { get; init; }
    [JsonPropertyName("userName")]

    public string? UserName { get; init; }
    [JsonPropertyName("defaultPassword")]

    public string? DefaultPassword { get; init; }
    [JsonPropertyName("isDelete")]

    public bool IsDelete { get; init; }
    [JsonPropertyName("updateAt")]

    public DateTimeOffset UpdateAt { get; init; }
    [JsonPropertyName("order")]

    public int Order { get; init; }

}
