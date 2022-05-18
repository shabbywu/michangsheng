using System;

namespace KBEngine
{
	// Token: 0x02000F1C RID: 3868
	public class KBEngineArgs
	{
		// Token: 0x06005D4D RID: 23885 RVA: 0x00041A47 File Offset: 0x0003FC47
		public int getTCPRecvBufferSize()
		{
			return (int)this.TCP_RECV_BUFFER_MAX;
		}

		// Token: 0x06005D4E RID: 23886 RVA: 0x00041A4F File Offset: 0x0003FC4F
		public int getTCPSendBufferSize()
		{
			return (int)this.TCP_SEND_BUFFER_MAX;
		}

		// Token: 0x06005D4F RID: 23887 RVA: 0x00041A57 File Offset: 0x0003FC57
		public int getUDPRecvBufferSize()
		{
			return (int)this.UDP_RECV_BUFFER_MAX;
		}

		// Token: 0x06005D50 RID: 23888 RVA: 0x00041A5F File Offset: 0x0003FC5F
		public int getUDPSendBufferSize()
		{
			return (int)this.UDP_SEND_BUFFER_MAX;
		}

		// Token: 0x04005AB8 RID: 23224
		public string ip = "127.0.0.1";

		// Token: 0x04005AB9 RID: 23225
		public int port = 20013;

		// Token: 0x04005ABA RID: 23226
		public KBEngineApp.CLIENT_TYPE clientType = KBEngineApp.CLIENT_TYPE.CLIENT_TYPE_MINI;

		// Token: 0x04005ABB RID: 23227
		public KBEngineApp.NETWORK_ENCRYPT_TYPE networkEncryptType;

		// Token: 0x04005ABC RID: 23228
		public int syncPlayerMS = 100;

		// Token: 0x04005ABD RID: 23229
		public bool useAliasEntityID = true;

		// Token: 0x04005ABE RID: 23230
		public bool isOnInitCallPropertysSetMethods = true;

		// Token: 0x04005ABF RID: 23231
		public uint TCP_SEND_BUFFER_MAX = 1460U;

		// Token: 0x04005AC0 RID: 23232
		public uint UDP_SEND_BUFFER_MAX = 128U;

		// Token: 0x04005AC1 RID: 23233
		public uint TCP_RECV_BUFFER_MAX = 1460U;

		// Token: 0x04005AC2 RID: 23234
		public uint UDP_RECV_BUFFER_MAX = 128U;

		// Token: 0x04005AC3 RID: 23235
		public bool isMultiThreads;

		// Token: 0x04005AC4 RID: 23236
		public int threadUpdateHZ = 10;

		// Token: 0x04005AC5 RID: 23237
		public bool forceDisableUDP;

		// Token: 0x04005AC6 RID: 23238
		public int serverHeartbeatTick = 15;
	}
}
