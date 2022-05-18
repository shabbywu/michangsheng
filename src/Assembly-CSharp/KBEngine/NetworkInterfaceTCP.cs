using System;
using System.Net.Sockets;

namespace KBEngine
{
	// Token: 0x02000FDA RID: 4058
	public class NetworkInterfaceTCP : NetworkInterfaceBase
	{
		// Token: 0x06005FEA RID: 24554 RVA: 0x00042ABB File Offset: 0x00040CBB
		public override bool valid()
		{
			return this._socket != null && this._socket.Connected;
		}

		// Token: 0x06005FEB RID: 24555 RVA: 0x00266654 File Offset: 0x00264854
		protected override Socket createSocket()
		{
			Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveBuffer, KBEngineApp.app.getInitArgs().getTCPRecvBufferSize() * 2);
			socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendBuffer, KBEngineApp.app.getInitArgs().getTCPSendBufferSize() * 2);
			socket.NoDelay = true;
			return socket;
		}

		// Token: 0x06005FEC RID: 24556 RVA: 0x00042C61 File Offset: 0x00040E61
		protected override PacketReceiverBase createPacketReceiver()
		{
			return new PacketReceiverTCP(this);
		}

		// Token: 0x06005FED RID: 24557 RVA: 0x00042C69 File Offset: 0x00040E69
		protected override PacketSenderBase createPacketSender()
		{
			return new PacketSenderTCP(this);
		}

		// Token: 0x06005FEE RID: 24558 RVA: 0x002666B4 File Offset: 0x002648B4
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
