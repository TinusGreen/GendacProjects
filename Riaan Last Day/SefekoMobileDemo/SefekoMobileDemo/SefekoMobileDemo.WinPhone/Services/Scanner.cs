using System.Threading.Tasks;
using SefekoMobileDemo.Services;

namespace SefekoMobileDemo.Uwp.Services
{
	public class Scanner : IScan
	{
		public async Task Scan()
		{
			await Task.Delay(1000);
		}
	}
}