using System;
using System.Runtime.Remoting.Messaging;

namespace KBEngine
{
	// Token: 0x02000C56 RID: 3158
	public abstract class PacketReceiverBase
	{
		// Token: 0x060055CF RID: 21967 RVA: 0x0023A38D File Offset: 0x0023858D
		public PacketReceiverBase(NetworkInterfaceBase networkInterface)
		{
			this._networkInterface = networkInterface;
		}

		// Token: 0x060055D0 RID: 21968 RVA: 0x0023A39C File Offset: 0x0023859C
		~PacketReceiverBase()
		{
		}

		// Token: 0x060055D1 RID: 21969 RVA: 0x0023A3C4 File Offset: 0x002385C4
		public NetworkInterfaceBase networkInterface()
		{
			return this._networkInterface;
		}

		// Token: 0x060055D2 RID: 21970
		public abstract void process();

		// Token: 0x060055D3 RID: 21971 RVA: 0x0023A3CC File Offset: 0x002385CC
		public virtual void startRecv()
		{
			new PacketReceiverBase.AsyncReceiveMethod(this._asyncReceive).BeginInvoke(new AsyncCallback(this._onRecv), null);
		}

		// Token: 0x060055D4 RID: 21972
		protected abstract void _asyncReceive();

		// Token: 0x060055D5 RID: 21973 RVA: 0x0023A3EE File Offset: 0x002385EE
		private void _onRecv(IAsyncResult ar)
		{
			((PacketReceiverBase.AsyncReceiveMethod)((AsyncResult)ar).AsyncDelegate).EndInvoke(ar);
		}

		// Token: 0x040050C8 RID: 20680
		protected MessageReaderBase _messageReader;

		// Token: 0x040050C9 RID: 20681
		protected NetworkInterfaceBase _networkInterface;

		// Token: 0x020015FE RID: 5630
		// (Invoke) Token: 0x060085AE RID: 34222
		protected delegate void AsyncReceiveMethod();
	}
}
