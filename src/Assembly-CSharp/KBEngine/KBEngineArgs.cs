using System;

namespace KBEngine
{
	// Token: 0x02000B99 RID: 2969
	public class KBEngineArgs
	{
		// Token: 0x0600530F RID: 21263 RVA: 0x00233860 File Offset: 0x00231A60
		public int getTCPRecvBufferSize()
		{
			return (int)this.TCP_RECV_BUFFER_MAX;
		}

		// Token: 0x06005310 RID: 21264 RVA: 0x00233868 File Offset: 0x00231A68
		public int getTCPSendBufferSize()
		{
			return (int)this.TCP_SEND_BUFFER_MAX;
		}

		// Token: 0x06005311 RID: 21265 RVA: 0x00233870 File Offset: 0x00231A70
		public int getUDPRecvBufferSize()
		{
			return (int)this.UDP_RECV_BUFFER_MAX;
		}

		// Token: 0x06005312 RID: 21266 RVA: 0x00233878 File Offset: 0x00231A78
		public int getUDPSendBufferSize()
		{
			return (int)this.UDP_SEND_BUFFER_MAX;
		}

		// Token: 0x04005017 RID: 20503
		public string ip = "127.0.0.1";

		// Token: 0x04005018 RID: 20504
		public int port = 20013;

		// Token: 0x04005019 RID: 20505
		public KBEngineApp.CLIENT_TYPE clientType = KBEngineApp.CLIENT_TYPE.CLIENT_TYPE_MINI;

		// Token: 0x0400501A RID: 20506
		public KBEngineApp.NETWORK_ENCRYPT_TYPE networkEncryptType;

		// Token: 0x0400501B RID: 20507
		public int syncPlayerMS = 100;

		// Token: 0x0400501C RID: 20508
		public bool useAliasEntityID = true;

		// Token: 0x0400501D RID: 20509
		public bool isOnInitCallPropertysSetMethods = true;

		// Token: 0x0400501E RID: 20510
		public uint TCP_SEND_BUFFER_MAX = 1460U;

		// Token: 0x0400501F RID: 20511
		public uint UDP_SEND_BUFFER_MAX = 128U;

		// Token: 0x04005020 RID: 20512
		public uint TCP_RECV_BUFFER_MAX = 1460U;

		// Token: 0x04005021 RID: 20513
		public uint UDP_RECV_BUFFER_MAX = 128U;

		// Token: 0x04005022 RID: 20514
		public bool isMultiThreads;

		// Token: 0x04005023 RID: 20515
		public int threadUpdateHZ = 10;

		// Token: 0x04005024 RID: 20516
		public bool forceDisableUDP;

		// Token: 0x04005025 RID: 20517
		public int serverHeartbeatTick = 15;
	}
}
