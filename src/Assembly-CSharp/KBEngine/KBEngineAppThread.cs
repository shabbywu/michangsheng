using System;
using System.Threading;

namespace KBEngine
{
	// Token: 0x02000F1A RID: 3866
	public class KBEngineAppThread : KBEngineApp
	{
		// Token: 0x06005D42 RID: 23874 RVA: 0x000419BD File Offset: 0x0003FBBD
		public KBEngineAppThread(KBEngineArgs args) : base(args)
		{
		}

		// Token: 0x06005D43 RID: 23875 RVA: 0x00261840 File Offset: 0x0025FA40
		public override bool initialize(KBEngineArgs args)
		{
			base.initialize(args);
			KBEngineAppThread.threadUpdateHZ = args.threadUpdateHZ;
			KBEngineAppThread.threadUpdatePeriod = 1000f / (float)KBEngineAppThread.threadUpdateHZ;
			this.kbethread = new KBEngineAppThread.KBEThread(this);
			this._t = new Thread(new ThreadStart(this.kbethread.run));
			this._t.Start();
			return true;
		}

		// Token: 0x06005D44 RID: 23876 RVA: 0x000419D1 File Offset: 0x0003FBD1
		public override void reset()
		{
			this._isbreak = false;
			this._lasttime = DateTime.Now;
			base.reset();
		}

		// Token: 0x06005D45 RID: 23877 RVA: 0x000419EB File Offset: 0x0003FBEB
		public void breakProcess()
		{
			this._isbreak = true;
		}

		// Token: 0x06005D46 RID: 23878 RVA: 0x000419F4 File Offset: 0x0003FBF4
		public bool isbreak()
		{
			return this._isbreak;
		}

		// Token: 0x06005D47 RID: 23879 RVA: 0x000419FC File Offset: 0x0003FBFC
		public override void process()
		{
			while (!this.isbreak())
			{
				base.process();
				this._thread_wait();
			}
			Dbg.WARNING_MSG("KBEngineAppThread::process(): break!");
		}

		// Token: 0x06005D48 RID: 23880 RVA: 0x002618A8 File Offset: 0x0025FAA8
		private void _thread_wait()
		{
			TimeSpan timeSpan = DateTime.Now - this._lasttime;
			int num = (int)((double)KBEngineAppThread.threadUpdatePeriod - timeSpan.TotalMilliseconds);
			if (num < 0)
			{
				num = 0;
			}
			Thread.Sleep(num);
			this._lasttime = DateTime.Now;
		}

		// Token: 0x06005D49 RID: 23881 RVA: 0x002618F0 File Offset: 0x0025FAF0
		public override void destroy()
		{
			Dbg.WARNING_MSG("KBEngineAppThread::destroy()");
			this.breakProcess();
			int num = 0;
			while (!this.kbethread.over && num < 50)
			{
				Thread.Sleep(100);
				num++;
			}
			if (this._t != null)
			{
				this._t.Abort();
			}
			this._t = null;
			base.destroy();
		}

		// Token: 0x04005AB0 RID: 23216
		private Thread _t;

		// Token: 0x04005AB1 RID: 23217
		public KBEngineAppThread.KBEThread kbethread;

		// Token: 0x04005AB2 RID: 23218
		public static int threadUpdateHZ = 10;

		// Token: 0x04005AB3 RID: 23219
		private static float threadUpdatePeriod = 1000f / (float)KBEngineAppThread.threadUpdateHZ;

		// Token: 0x04005AB4 RID: 23220
		private bool _isbreak;

		// Token: 0x04005AB5 RID: 23221
		private DateTime _lasttime = DateTime.Now;

		// Token: 0x02000F1B RID: 3867
		public class KBEThread
		{
			// Token: 0x06005D4B RID: 23883 RVA: 0x00041A38 File Offset: 0x0003FC38
			public KBEThread(KBEngineApp app)
			{
				this.app_ = app;
			}

			// Token: 0x06005D4C RID: 23884 RVA: 0x00261950 File Offset: 0x0025FB50
			public void run()
			{
				Dbg.INFO_MSG("KBEThread::run()");
				this.over = false;
				try
				{
					this.app_.process();
				}
				catch (Exception ex)
				{
					Dbg.ERROR_MSG(ex.ToString());
				}
				this.over = true;
				Dbg.INFO_MSG("KBEThread::end()");
			}

			// Token: 0x04005AB6 RID: 23222
			private KBEngineApp app_;

			// Token: 0x04005AB7 RID: 23223
			public bool over;
		}
	}
}
