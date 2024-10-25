using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class CustomJsonConverter : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(IJsonSerializeAble);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        IJsonSerializeAble jsonSerializeAble = reader.Value as IJsonSerializeAble;

        return jsonSerializeAble;
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        IJsonSerializeAble jsonSerializeAble = (IJsonSerializeAble)value;
        writer.WriteRawValue(jsonSerializeAble.OnSerialize());
    }
}
