using System.Net;
using System.Net.Sockets;

namespace KBEngine;

public class PacketSenderKCP : PacketSenderBase
{
	private Socket socket_;

	private EndPoint remoteEndPint_;

	public PacketSenderKCP(NetworkInterfaceBase networkInterface)
		: base(networkInterface)
	{
		socket_ = _networkInterface.sock();
		remoteEndPint_ = ((NetworkInterfaceKCP)_networkInterface).remoteEndPint;
	}

	~PacketSenderKCP()
	{
		Dbg.DEBUG_MSG("PacketSenderKCP::~PacketSenderKCP(), destroyed!");
	}

	public override bool send(MemoryStream stream)
	{
		return true;
	}

	public bool sendto(byte[] packet, int size)
	{
		try
		{
			socket_.SendTo(packet, size, SocketFlags.None, remoteEndPint_);
		}
		catch (SocketException arg)
		{
			Dbg.ERROR_MSG($"PacketSenderKCP::sendto(): send data error, disconnect from '{socket_.RemoteEndPoint}'! error = '{arg}'");
			Event.fireIn("_closeNetwork", _networkInterface);
			return false;
		}
		return true;
	}

	protected override void _asyncSend()
	{
	}
}
