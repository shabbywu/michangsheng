namespace KBEngine;

public class KBEngineArgs
{
	public string ip = "127.0.0.1";

	public int port = 20013;

	public KBEngineApp.CLIENT_TYPE clientType = KBEngineApp.CLIENT_TYPE.CLIENT_TYPE_MINI;

	public KBEngineApp.NETWORK_ENCRYPT_TYPE networkEncryptType;

	public int syncPlayerMS = 100;

	public bool useAliasEntityID = true;

	public bool isOnInitCallPropertysSetMethods = true;

	public uint TCP_SEND_BUFFER_MAX = 1460u;

	public uint UDP_SEND_BUFFER_MAX = 128u;

	public uint TCP_RECV_BUFFER_MAX = 1460u;

	public uint UDP_RECV_BUFFER_MAX = 128u;

	public bool isMultiThreads;

	public int threadUpdateHZ = 10;

	public bool forceDisableUDP;

	public int serverHeartbeatTick = 15;

	public int getTCPRecvBufferSize()
	{
		return (int)TCP_RECV_BUFFER_MAX;
	}

	public int getTCPSendBufferSize()
	{
		return (int)TCP_SEND_BUFFER_MAX;
	}

	public int getUDPRecvBufferSize()
	{
		return (int)UDP_RECV_BUFFER_MAX;
	}

	public int getUDPSendBufferSize()
	{
		return (int)UDP_SEND_BUFFER_MAX;
	}
}
