using System;
using System.Net;
using System.Net.Sockets;

namespace KBEngine
{
	// Token: 0x02000C5A RID: 3162
	public class PacketSenderKCP : PacketSenderBase
	{
		// Token: 0x060055E7 RID: 21991 RVA: 0x0023A971 File Offset: 0x00238B71
		public PacketSenderKCP(NetworkInterfaceBase networkInterface) : base(networkInterface)
		{
			this.socket_ = this._networkInterface.sock();
			this.remoteEndPint_ = ((NetworkInterfaceKCP)this._networkInterface).remoteEndPint;
		}

		// Token: 0x060055E8 RID: 21992 RVA: 0x0023A9A4 File Offset: 0x00238BA4
		~PacketSenderKCP()
		{
			Dbg.DEBUG_MSG("PacketSenderKCP::~PacketSenderKCP(), destroyed!");
		}

		// Token: 0x060055E9 RID: 21993 RVA: 0x00024C5F File Offset: 0x00022E5F
		public override bool send(MemoryStream stream)
		{
			return true;
		}

		// Token: 0x060055EA RID: 21994 RVA: 0x0023A9D4 File Offset: 0x00238BD4
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

		// Token: 0x060055EB RID: 21995 RVA: 0x00004095 File Offset: 0x00002295
		protected override void _asyncSend()
		{
		}

		// Token: 0x040050D2 RID: 20690
		private Socket socket_;

		// Token: 0x040050D3 RID: 20691
		private EndPoint remoteEndPint_;
	}
}
