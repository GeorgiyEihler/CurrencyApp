using Currency.Shared.CBRResponseDto;
using System.Xml.Serialization;
using System.Text.Json;
using System.Reflection;
using Currency.Service.CurrencyEndpoints;

namespace Currency.UnitTest.CBRIntegration;

public class TestData
{
    public static IEnumerable<object[]> GetTestData()
    {
        var data = new List<object[]>
        {
            new object[] { new DateOnly(2001, 01, 01), "scripts/XML_daily.asp?date_req=01/01/2001" },
            new object[] { new DateOnly(800, 12, 01), "scripts/XML_daily.asp?date_req=01/12/0800" },
            new object[] { new DateOnly(2001, 01, 12), "scripts/XML_daily.asp?date_req=12/01/2001" },
        };

        return data;
    }
}

public class CBRParsingTest
{
    [Fact]
    public void ParseResponse_Should_Return_Valid_Array()
    {
        var responseXml = "<Valuta name=\"Foreign Currency Market Lib\">\r\n<Item ID=\"R01010\">\r\n<Name>Австралийский доллар</Name>\r\n<EngName>Australian Dollar</EngName>\r\n<Nominal>1</Nominal>\r\n<ParentCode>R01010 </ParentCode>\r\n</Item>\r\n<Item ID=\"R01015\">\r\n<Name>Австрийский шиллинг</Name>\r\n<EngName>Austrian Shilling</EngName>\r\n<Nominal>1000</Nominal>\r\n<ParentCode>R01015 </ParentCode>\r\n</Item>\r\n</Valuta>";

        var serialize = new XmlSerializer(typeof(CBRCurrencyInfromationResponseDto));

        CBRCurrencyInfromationResponseDto responseDto;

        using (var reader = new StringReader(responseXml))
        {
            responseDto = (CBRCurrencyInfromationResponseDto)serialize.Deserialize(reader);
        }

        Assert.NotNull(responseDto);

        Assert.Equal(2, responseDto.CurrencyInformationDto.Length);
    }

    [Fact]
    public void ParseResponseCurse_Should_Return_Valid_Array() 
    {
        var responseXml = "<ValCurs Date=\"02.03.2002\" name=\"Foreign Currency Market\">\r\n<Valute ID=\"R01010\">\r\n<NumCode>036</NumCode>\r\n<CharCode>AUD</CharCode>\r\n<Nominal>1</Nominal>\r\n<Name>Австралийский доллар</Name>\r\n<Value>16,0102</Value>\r\n</Valute>\r\n<Valute ID=\"R01035\">\r\n<NumCode>826</NumCode>\r\n<CharCode>GBP</CharCode>\r\n<Nominal>1</Nominal>\r\n<Name>Фунт стерлингов Соединенного королевства</Name>\r\n<Value>43,8254</Value>\r\n</Valute>\r\n</ValCurs>";

        var serialize = new XmlSerializer(typeof(CBRCurrencyResponseDto));

        CBRCurrencyResponseDto responseDto;

        using (var reader = new StringReader(responseXml))
        {
            responseDto = (CBRCurrencyResponseDto)serialize.Deserialize(reader);
        }

        Assert.NotNull(responseDto);

        Assert.Equal(2, responseDto.CBRCurrencyRateResponseDto.Length);
    }

    [Fact]
    public void PaseResponseLasted_Should_Return_Valid_Lasted()
    {
        var responseJson = "{\r\n    \"disclaimer\": \"https://www.cbr-xml-daily.ru/#terms\",\r\n    \"date\": \"2023-03-25\",\r\n    \"timestamp\": 1679691600,\r\n    \"base\": \"RUB\",\r\n    \"rates\": {\r\n        \"AUD\": 0.019561547,\r\n        \"AZN\": 0.0222373,\r\n        \"GBP\": 0.0106209879,\r\n        \"AMD\": 5.07627097,\r\n        \"BYN\": 0.0371834,\r\n        \"BGN\": 0.023516685,\r\n        \"BRL\": 0.068843,\r\n        \"HUF\": 4.7343329,\r\n        \"VND\": 308.7067656,\r\n        \"HKD\": 0.1025077,\r\n        \"GEL\": 0.0337065775,\r\n        \"DKK\": 0.08955678,\r\n        \"AED\": 0.04804066,\r\n        \"USD\": 0.0130808,\r\n        \"EUR\": 0.012137056,\r\n        \"EGP\": 0.4041547,\r\n        \"INR\": 1.078725,\r\n        \"IDR\": 200.77741,\r\n        \"KZT\": 6.048033,\r\n        \"CAD\": 0.01788275,\r\n        \"QAR\": 0.047614,\r\n        \"KGS\": 1.14352365,\r\n        \"CNY\": 0.0899717,\r\n        \"MDL\": 0.2431327,\r\n        \"NZD\": 0.02094938,\r\n        \"NOK\": 0.138302,\r\n        \"PLN\": 0.057159,\r\n        \"RON\": 0.060045995,\r\n        \"XDR\": 0.00970608989,\r\n        \"SGD\": 0.0173713,\r\n        \"TJS\": 0.1427574,\r\n        \"THB\": 0.4457579,\r\n        \"TRY\": 0.2490517,\r\n        \"TMT\": 0.0457827,\r\n        \"UZS\": 149.006425,\r\n        \"UAH\": 0.48309878887,\r\n        \"CZK\": 0.284481,\r\n        \"SEK\": 0.13565447,\r\n        \"CHF\": 0.01201602,\r\n        \"RSD\": 1.4169765,\r\n        \"ZAR\": 0.2376087,\r\n        \"KRW\": 16.9304716,\r\n        \"JPY\": 1.708353\r\n    }\r\n}";
        var exchangeRates = JsonSerializer.Deserialize<CBRCurrencyLastedResponseDto>(responseJson);

        Assert.NotNull(exchangeRates);

        Assert.Equal(new DateTime(2023, 03, 25), exchangeRates.Date);

        Assert.Equal(43, exchangeRates.Rates.Count);
    }

    [Theory]
    [MemberData(nameof(TestData.GetTestData), MemberType = typeof(TestData))]
    public void CreateReuqest_Should_Return_Valid_Address(DateOnly date, string expected)
    {
        var requestUri = CBREndpoints.CurrencyRate(date);

        Assert.Equal(expected, requestUri);
    }
}
