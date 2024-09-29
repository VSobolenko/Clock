using System.Threading.Tasks;
using UnityEngine.Networking;

namespace ClockService
{
public class WebRequest : IWebRequest
{
    private const int TimeOut = 20000;
    private readonly string _url;

    public WebRequest(string url)
    {
        _url = url;
    }

    public Task<string> RequestJsonTime()
    {
        var taskSource = new TaskCompletionSource<string>();

        var webRequest = UnityWebRequest.Get(_url);
        var request = webRequest.SendWebRequest();

        request.completed += operation =>
        {
            var text = webRequest.downloadHandler.text;
            taskSource.SetResult(text);
        };

        return taskSource.Task;
    }
}
}