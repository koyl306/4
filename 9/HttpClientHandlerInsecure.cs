using System.Net.Http;

namespace pract9;

public class HttpClientHandlerInsecure : HttpClientHandler
{
    public HttpClientHandlerInsecure()
    {
        ServerCertificateCustomValidationCallback =
            (_, _, _, _) => true;
    }
}