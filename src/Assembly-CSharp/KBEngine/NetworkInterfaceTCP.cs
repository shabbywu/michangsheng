using System;
using System.Net.Sockets;

namespace KBEngine;

public class NetworkInterfaceTCP : NetworkInterfaceBase
{
	public override bool valid()
	{
		if (_socket != null)
		{
			return _socket.Connected;
		}
		return false;
	}

	protected override Socket createSocket()
	{
		Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
		socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveBuffer, KBEngineApp.app.getInitArgs().getTCPRecvBufferSize() * 2);
		socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendBuffer, KBEngineApp.app.getInitArgs().getTCPSendBufferSize() * 2);
		socket.NoDelay = true;
		return socket;
	}

	protected override PacketReceiverBase createPacketReceiver()
	{
		return new PacketReceiverTCP(this);
	}

	protected override PacketSenderBase createPacketSender()
	{
		return new PacketSenderTCP(this);
	}

	protected override void onAsyncConnect(ConnectState state)
	{
		try
		{
			state.socket.Connect(state.connectIP, state.connectPort);
		}
		catch (Exception ex)
		{
			Dbg.ERROR_MSG($"NetworkInterfaceTCP::_asyncConnect(), connect to '{state.connectIP}:{state.connectPort}' fault! error = '{ex}'");
			state.error = ex.ToString();
		}
	}
}
