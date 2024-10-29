using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class CustomJsonConverter : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        Debug.Log($"TryCustomJsonConverter {objectType.ToString()}");
        return typeof(IJsonSerializeAble).IsAssignableFrom(objectType);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        IJsonSerializeAble jsonSerializeAble = reader.Value as IJsonSerializeAble;
        jsonSerializeAble.DeSerialize(reader, serializer);
        return jsonSerializeAble;
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        IJsonSerializeAble jsonSerializeAble = (IJsonSerializeAble)value;
        writer.WriteRawValue(jsonSerializeAble.OnSerialize(writer, serializer));
        serializer.Serialize(writer, jsonSerializeAble);
        
    }
}
