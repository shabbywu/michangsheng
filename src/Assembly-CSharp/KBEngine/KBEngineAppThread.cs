using System;
using System.Threading;

namespace KBEngine
{
	// Token: 0x02000B98 RID: 2968
	public class KBEngineAppThread : KBEngineApp
	{
		// Token: 0x06005306 RID: 21254 RVA: 0x002336D7 File Offset: 0x002318D7
		public KBEngineAppThread(KBEngineArgs args) : base(args)
		{
		}

		// Token: 0x06005307 RID: 21255 RVA: 0x002336EC File Offset: 0x002318EC
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

		// Token: 0x06005308 RID: 21256 RVA: 0x00233751 File Offset: 0x00231951
		public override void reset()
		{
			this._isbreak = false;
			this._lasttime = DateTime.Now;
			base.reset();
		}

		// Token: 0x06005309 RID: 21257 RVA: 0x0023376B File Offset: 0x0023196B
		public void breakProcess()
		{
			this._isbreak = true;
		}

		// Token: 0x0600530A RID: 21258 RVA: 0x00233774 File Offset: 0x00231974
		public bool isbreak()
		{
			return this._isbreak;
		}

		// Token: 0x0600530B RID: 21259 RVA: 0x0023377C File Offset: 0x0023197C
		public override void process()
		{
			while (!this.isbreak())
			{
				base.process();
				this._thread_wait();
			}
			Dbg.WARNING_MSG("KBEngineAppThread::process(): break!");
		}

		// Token: 0x0600530C RID: 21260 RVA: 0x002337A0 File Offset: 0x002319A0
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

		// Token: 0x0600530D RID: 21261 RVA: 0x002337E8 File Offset: 0x002319E8
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

		// Token: 0x04005011 RID: 20497
		private Thread _t;

		// Token: 0x04005012 RID: 20498
		public KBEngineAppThread.KBEThread kbethread;

		// Token: 0x04005013 RID: 20499
		public static int threadUpdateHZ = 10;

		// Token: 0x04005014 RID: 20500
		private static float threadUpdatePeriod = 1000f / (float)KBEngineAppThread.threadUpdateHZ;

		// Token: 0x04005015 RID: 20501
		private bool _isbreak;

		// Token: 0x04005016 RID: 20502
		private DateTime _lasttime = DateTime.Now;

		// Token: 0x020015F8 RID: 5624
		public class KBEThread
		{
			// Token: 0x060085A2 RID: 34210 RVA: 0x002E4B20 File Offset: 0x002E2D20
			public KBEThread(KBEngineApp app)
			{
				this.app_ = app;
			}

			// Token: 0x060085A3 RID: 34211 RVA: 0x002E4B30 File Offset: 0x002E2D30
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

			// Token: 0x040070F6 RID: 28918
			private KBEngineApp app_;

			// Token: 0x040070F7 RID: 28919
			public bool over;
		}
	}
}
