using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

public interface IJsonSerializeAble
{
    void DeSerialize(JsonReader reader, JsonSerializer serializer);
    string OnSerialize(JsonWriter writer, JsonSerializer serializer);
}
