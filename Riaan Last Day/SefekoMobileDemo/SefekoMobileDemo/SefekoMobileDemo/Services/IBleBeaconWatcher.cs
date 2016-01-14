namespace SefekoMobileDemo.Services
{
	public interface IBleBeaconWatcher
	{
		void Start();
	}

	public class BleBeaconMessage
	{

		public byte[] Data { get; set; }
	}
}