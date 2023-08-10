using System;
using System.Net.Sockets;
using Deps;

namespace KBEngine;

public class PacketReceiverKCP : PacketReceiverBase
{
	private byte[] _buffer;

	private KCP kcp_;

	public PacketReceiverKCP(NetworkInterfaceBase networkInterface)
		: base(networkInterface)
	{
		_buffer = new byte[65583];
		_messageReader = new MessageReaderKCP();
		kcp_ = ((NetworkInterfaceKCP)networkInterface).kcp();
	}

	~PacketReceiverKCP()
	{
		kcp_ = null;
		Dbg.DEBUG_MSG("PacketReceiverKCP::~PacketReceiverKCP(), destroyed!");
	}

	public override void process()
	{
		Socket socket = _networkInterface.sock();
		while (socket.Available > 0)
		{
			int num = 0;
			try
			{
				num = socket.Receive(_buffer);
			}
			catch (Exception ex)
			{
				Dbg.ERROR_MSG("PacketReceiverKCP::process: " + ex.ToString());
				Event.fireIn("_closeNetwork", _networkInterface);
				break;
			}
			if (num <= 0)
			{
				Dbg.WARNING_MSG("PacketReceiverKCP::_asyncReceive(): KCP Receive <= 0!");
				break;
			}
			((NetworkInterfaceKCP)_networkInterface).nextTickKcpUpdate = 0u;
			if (kcp_.Input(_buffer, 0, num) < 0)
			{
				Dbg.WARNING_MSG($"PacketReceiverKCP::_asyncReceive(): KCP Input get {num}!");
				break;
			}
			while (true)
			{
				num = kcp_.Recv(_buffer, 0, _buffer.Length);
				if (num < 0)
				{
					break;
				}
				if (_networkInterface.fileter() != null)
				{
					_networkInterface.fileter().recv(_messageReader, _buffer, 0u, (uint)num);
				}
				else
				{
					_messageReader.process(_buffer, 0u, (uint)num);
				}
			}
		}
	}

	public override void startRecv()
	{
	}

	protected override void _asyncReceive()
	{
	}
}
