using System;
using System.Net.Sockets;

namespace KBEngine
{
	// Token: 0x02000C52 RID: 3154
	public class NetworkInterfaceTCP : NetworkInterfaceBase
	{
		// Token: 0x060055A3 RID: 21923 RVA: 0x00238EB2 File Offset: 0x002370B2
		public override bool valid()
		{
			return this._socket != null && this._socket.Connected;
		}

		// Token: 0x060055A4 RID: 21924 RVA: 0x00239710 File Offset: 0x00237910
		protected override Socket createSocket()
		{
			Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveBuffer, KBEngineApp.app.getInitArgs().getTCPRecvBufferSize() * 2);
			socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendBuffer, KBEngineApp.app.getInitArgs().getTCPSendBufferSize() * 2);
			socket.NoDelay = true;
			return socket;
		}

		// Token: 0x060055A5 RID: 21925 RVA: 0x0023976E File Offset: 0x0023796E
		protected override PacketReceiverBase createPacketReceiver()
		{
			return new PacketReceiverTCP(this);
		}

		// Token: 0x060055A6 RID: 21926 RVA: 0x00239776 File Offset: 0x00237976
		protected override PacketSenderBase createPacketSender()
		{
			return new PacketSenderTCP(this);
		}

		// Token: 0x060055A7 RID: 21927 RVA: 0x00239780 File Offset: 0x00237980
		protected override void onAsyncConnect(NetworkInterfaceBase.ConnectState state)
		{
			try
			{
				state.socket.Connect(state.connectIP, state.connectPort);
			}
			catch (Exception ex)
			{
				Dbg.ERROR_MSG(string.Format("NetworkInterfaceTCP::_asyncConnect(), connect to '{0}:{1}' fault! error = '{2}'", state.connectIP, state.connectPort, ex));
				state.error = ex.ToString();
			}
		}
	}
}
