using System;
using System.Runtime.Remoting.Messaging;

namespace KBEngine
{
	// Token: 0x02000C59 RID: 3161
	public abstract class PacketSenderBase
	{
		// Token: 0x060055E0 RID: 21984 RVA: 0x0023A8E0 File Offset: 0x00238AE0
		public PacketSenderBase(NetworkInterfaceBase networkInterface)
		{
			this._networkInterface = networkInterface;
			this._asyncSendMethod = new PacketSenderBase.AsyncSendMethod(this._asyncSend);
			this._asyncCallback = new AsyncCallback(PacketSenderBase._onSent);
		}

		// Token: 0x060055E1 RID: 21985 RVA: 0x0023A914 File Offset: 0x00238B14
		~PacketSenderBase()
		{
		}

		// Token: 0x060055E2 RID: 21986 RVA: 0x0023A93C File Offset: 0x00238B3C
		public NetworkInterfaceBase networkInterface()
		{
			return this._networkInterface;
		}

		// Token: 0x060055E3 RID: 21987
		public abstract bool send(MemoryStream stream);

		// Token: 0x060055E4 RID: 21988 RVA: 0x0023A944 File Offset: 0x00238B44
		protected void _startSend()
		{
			this._asyncSendMethod.BeginInvoke(this._asyncCallback, null);
		}

		// Token: 0x060055E5 RID: 21989
		protected abstract void _asyncSend();

		// Token: 0x060055E6 RID: 21990 RVA: 0x0023A959 File Offset: 0x00238B59
		protected static void _onSent(IAsyncResult ar)
		{
			((PacketSenderBase.AsyncSendMethod)((AsyncResult)ar).AsyncDelegate).EndInvoke(ar);
		}

		// Token: 0x040050CF RID: 20687
		protected NetworkInterfaceBase _networkInterface;

		// Token: 0x040050D0 RID: 20688
		private AsyncCallback _asyncCallback;

		// Token: 0x040050D1 RID: 20689
		private PacketSenderBase.AsyncSendMethod _asyncSendMethod;

		// Token: 0x020015FF RID: 5631
		// (Invoke) Token: 0x060085B2 RID: 34226
		public delegate void AsyncSendMethod();
	}
}
