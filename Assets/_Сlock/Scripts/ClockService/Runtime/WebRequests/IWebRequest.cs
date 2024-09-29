using System.Threading.Tasks;

namespace ClockService
{
public interface IWebRequest
{
    Task<string> RequestJsonTime();
}
}