using System;
using System.Runtime.Remoting.Messaging;

namespace KBEngine
{
	// Token: 0x02000FDE RID: 4062
	public abstract class PacketReceiverBase
	{
		// Token: 0x06006016 RID: 24598 RVA: 0x00042D36 File Offset: 0x00040F36
		public PacketReceiverBase(NetworkInterfaceBase networkInterface)
		{
			this._networkInterface = networkInterface;
		}

		// Token: 0x06006017 RID: 24599 RVA: 0x0024EF70 File Offset: 0x0024D170
		~PacketReceiverBase()
		{
		}

		// Token: 0x06006018 RID: 24600 RVA: 0x00042D45 File Offset: 0x00040F45
		public NetworkInterfaceBase networkInterface()
		{
			return this._networkInterface;
		}

		// Token: 0x06006019 RID: 24601
		public abstract void process();

		// Token: 0x0600601A RID: 24602 RVA: 0x00042D4D File Offset: 0x00040F4D
		public virtual void startRecv()
		{
			new PacketReceiverBase.AsyncReceiveMethod(this._asyncReceive).BeginInvoke(new AsyncCallback(this._onRecv), null);
		}

		// Token: 0x0600601B RID: 24603
		protected abstract void _asyncReceive();

		// Token: 0x0600601C RID: 24604 RVA: 0x00042D6F File Offset: 0x00040F6F
		private void _onRecv(IAsyncResult ar)
		{
			((PacketReceiverBase.AsyncReceiveMethod)((AsyncResult)ar).AsyncDelegate).EndInvoke(ar);
		}

		// Token: 0x04005B78 RID: 23416
		protected MessageReaderBase _messageReader;

		// Token: 0x04005B79 RID: 23417
		protected NetworkInterfaceBase _networkInterface;

		// Token: 0x02000FDF RID: 4063
		// (Invoke) Token: 0x0600601E RID: 24606
		protected delegate void AsyncReceiveMethod();
	}
}
