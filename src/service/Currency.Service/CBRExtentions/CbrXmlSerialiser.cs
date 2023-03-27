using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Xml.Serialization;

namespace Currency.Service.CBRExtentions;

public static class CbrXmlSerialiser
{
    public static async Task<T> GetResponseDto<T>(this HttpResponseMessage responseMessage) where T : class
    {
        var encoding = GetContentTypeEncoding(responseMessage.Content.Headers.ContentType);

        var byteArray = await responseMessage.Content.ReadAsByteArrayAsync();

        var responseString = encoding.GetString(byteArray);

        var serialize = new XmlSerializer(typeof(T));

        T responseDto;

        using (var reader = new StringReader(responseString))
        {
            responseDto = (T)serialize.Deserialize(reader);
        }

        return responseDto;
    }

    public static Encoding GetContentTypeEncoding(MediaTypeHeaderValue contentType)
    {
        if (contentType.CharSet is null)
        {
            return Encoding.UTF8;
        }

        return Encoding.GetEncoding(contentType.CharSet);
    }

    public static async Task<T> GetJsonResponseDto<T>(this HttpResponseMessage responseMessage) where T : class 
    {
        var responseString = await responseMessage.Content.ReadAsStringAsync();

        var responseDto = JsonSerializer.Deserialize<T>(responseString);

        return responseDto;
    }
}
