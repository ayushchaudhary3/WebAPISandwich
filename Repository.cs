using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;// needed for defining json properties
using System.Threading.Tasks;

namespace HttpClientApp
{
    // json Serialization and DeSerialization
    public record class Repository(
        [property: JsonPropertyName("id")] int Id,
        [property: JsonPropertyName("name")] string Name,
        [property: JsonPropertyName("price")] double Price
        );
}