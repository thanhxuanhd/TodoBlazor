using System.Net.Http;

namespace Todo.App.Services;

public partial interface IClient
{
    public HttpClient HttpClient { get; }
}