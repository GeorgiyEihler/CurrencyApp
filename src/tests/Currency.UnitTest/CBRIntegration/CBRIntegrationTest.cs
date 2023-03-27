using Currency.Contract.LogManager;
using Currency.Service.CBRCurrencyService;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Engine;
using Moq;
using System.Text;

namespace Currency.UnitTest.CBRIntegration;

public class CBRIntegrationTest
{
    [Fact]
    public async void CurrecnyService_GetCurrencyInformaiot_Should_Return_Valid_Data()
    {
        var httpClientFactoryMock = new Mock<IHttpClientFactory>();

        var logger = new Mock<ILoggingManager>();

        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

        using var client = new HttpClient();

        client.BaseAddress = new Uri("http://www.cbr.ru");

        client.DefaultRequestHeaders.Add("Accept", "application/xml");

        httpClientFactoryMock.Setup(factory => factory.CreateClient("CBRClient")).Returns(client);

        var service = new CBRService(logger.Object, httpClientFactoryMock.Object);

        var response = await service.GetCurrencyInformation(true);

        Assert.NotNull(response);

        Assert.Equal("Foreign Currency Market Lib", response.Name);
    }

    [Fact]
    public async void CurrecnyService_GetCurrencyRate_Should_Return_Valid_Data()
    {
        var httpClientFactoryMock = new Mock<IHttpClientFactory>();

        var logger = new Mock<ILoggingManager>();

        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

        using var client = new HttpClient();

        client.BaseAddress = new Uri("http://www.cbr.ru");

        client.DefaultRequestHeaders.Add("Accept", "application/xml");

        httpClientFactoryMock.Setup(factory => factory.CreateClient("CBRClient")).Returns(client);

        var service = new CBRService(logger.Object, httpClientFactoryMock.Object);

        var response = await service.GetCurrencyReate(new DateOnly(2023, 01, 20));

        Assert.NotNull(response);

        Assert.Equal(new DateTime(2023, 01, 20), response.Date);
    }

    [Fact]
    public async void CurrecnyService_GetLastedRate_Should_Return_Valid_Data()
    {
        var httpClientFactoryMock = new Mock<IHttpClientFactory>();

        var logger = new Mock<ILoggingManager>();

        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

        using var client = new HttpClient();

        client.BaseAddress = new Uri("https://www.cbr-xml-daily.ru/");

        client.DefaultRequestHeaders.Add("Accept", "application/json");

        httpClientFactoryMock.Setup(factory => factory.CreateClient("CBRDailyClient")).Returns(client);

        var service = new CBRService(logger.Object, httpClientFactoryMock.Object);

        var response = await service.GetLastedRate();

        Assert.NotNull(response);

        Assert.Equal(43, response.Rates.Count);
    }
}
