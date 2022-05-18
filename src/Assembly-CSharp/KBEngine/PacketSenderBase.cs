using System;
using System.Runtime.Remoting.Messaging;

namespace KBEngine
{
	// Token: 0x02000FE2 RID: 4066
	public abstract class PacketSenderBase
	{
		// Token: 0x0600602B RID: 24619 RVA: 0x00042DEA File Offset: 0x00040FEA
		public PacketSenderBase(NetworkInterfaceBase networkInterface)
		{
			this._networkInterface = networkInterface;
			this._asyncSendMethod = new PacketSenderBase.AsyncSendMethod(this._asyncSend);
			this._asyncCallback = new AsyncCallback(PacketSenderBase._onSent);
		}

		// Token: 0x0600602C RID: 24620 RVA: 0x0024EF70 File Offset: 0x0024D170
		~PacketSenderBase()
		{
		}

		// Token: 0x0600602D RID: 24621 RVA: 0x00042E1E File Offset: 0x0004101E
		public NetworkInterfaceBase networkInterface()
		{
			return this._networkInterface;
		}

		// Token: 0x0600602E RID: 24622
		public abstract bool send(MemoryStream stream);

		// Token: 0x0600602F RID: 24623 RVA: 0x00042E26 File Offset: 0x00041026
		protected void _startSend()
		{
			this._asyncSendMethod.BeginInvoke(this._asyncCallback, null);
		}

		// Token: 0x06006030 RID: 24624
		protected abstract void _asyncSend();

		// Token: 0x06006031 RID: 24625 RVA: 0x00042E3B File Offset: 0x0004103B
		protected static void _onSent(IAsyncResult ar)
		{
			((PacketSenderBase.AsyncSendMethod)((AsyncResult)ar).AsyncDelegate).EndInvoke(ar);
		}

		// Token: 0x04005B7F RID: 23423
		protected NetworkInterfaceBase _networkInterface;

		// Token: 0x04005B80 RID: 23424
		private AsyncCallback _asyncCallback;

		// Token: 0x04005B81 RID: 23425
		private PacketSenderBase.AsyncSendMethod _asyncSendMethod;

		// Token: 0x02000FE3 RID: 4067
		// (Invoke) Token: 0x06006033 RID: 24627
		public delegate void AsyncSendMethod();
	}
}
