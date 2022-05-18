using System;
using System.Net;
using System.Net.Sockets;

namespace KBEngine
{
	// Token: 0x02000FE4 RID: 4068
	public class PacketSenderKCP : PacketSenderBase
	{
		// Token: 0x06006036 RID: 24630 RVA: 0x00042E53 File Offset: 0x00041053
		public PacketSenderKCP(NetworkInterfaceBase networkInterface) : base(networkInterface)
		{
			this.socket_ = this._networkInterface.sock();
			this.remoteEndPint_ = ((NetworkInterfaceKCP)this._networkInterface).remoteEndPint;
		}

		// Token: 0x06006037 RID: 24631 RVA: 0x00267670 File Offset: 0x00265870
		~PacketSenderKCP()
		{
			Dbg.DEBUG_MSG("PacketSenderKCP::~PacketSenderKCP(), destroyed!");
		}

		// Token: 0x06006038 RID: 24632 RVA: 0x0000A093 File Offset: 0x00008293
		public override bool send(MemoryStream stream)
		{
			return true;
		}

		// Token: 0x06006039 RID: 24633 RVA: 0x002676A0 File Offset: 0x002658A0
		public bool sendto(byte[] packet, int size)
		{
			try
			{
				this.socket_.SendTo(packet, size, SocketFlags.None, this.remoteEndPint_);
			}
			catch (SocketException arg)
			{
				Dbg.ERROR_MSG(string.Format("PacketSenderKCP::sendto(): send data error, disconnect from '{0}'! error = '{1}'", this.socket_.RemoteEndPoint, arg));
				Event.fireIn("_closeNetwork", new object[]
				{
					this._networkInterface
				});
				return false;
			}
			return true;
		}

		// Token: 0x0600603A RID: 24634 RVA: 0x000042DD File Offset: 0x000024DD
		protected override void _asyncSend()
		{
		}

		// Token: 0x04005B82 RID: 23426
		private Socket socket_;

		// Token: 0x04005B83 RID: 23427
		private EndPoint remoteEndPint_;
	}
}
