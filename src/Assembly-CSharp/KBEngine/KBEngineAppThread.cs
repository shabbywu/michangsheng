using System;
using System.Threading;

namespace KBEngine;

public class KBEngineAppThread : KBEngineApp
{
	public class KBEThread
	{
		private KBEngineApp app_;

		public bool over;

		public KBEThread(KBEngineApp app)
		{
			app_ = app;
		}

		public void run()
		{
			Dbg.INFO_MSG("KBEThread::run()");
			over = false;
			try
			{
				app_.process();
			}
			catch (Exception ex)
			{
				Dbg.ERROR_MSG(ex.ToString());
			}
			over = true;
			Dbg.INFO_MSG("KBEThread::end()");
		}
	}

	private Thread _t;

	public KBEThread kbethread;

	public static int threadUpdateHZ = 10;

	private static float threadUpdatePeriod = 1000f / (float)threadUpdateHZ;

	private bool _isbreak;

	private DateTime _lasttime = DateTime.Now;

	public KBEngineAppThread(KBEngineArgs args)
		: base(args)
	{
	}

	public override bool initialize(KBEngineArgs args)
	{
		base.initialize(args);
		threadUpdateHZ = args.threadUpdateHZ;
		threadUpdatePeriod = 1000f / (float)threadUpdateHZ;
		kbethread = new KBEThread(this);
		_t = new Thread(kbethread.run);
		_t.Start();
		return true;
	}

	public override void reset()
	{
		_isbreak = false;
		_lasttime = DateTime.Now;
		base.reset();
	}

	public void breakProcess()
	{
		_isbreak = true;
	}

	public bool isbreak()
	{
		return _isbreak;
	}

	public override void process()
	{
		while (!isbreak())
		{
			base.process();
			_thread_wait();
		}
		Dbg.WARNING_MSG("KBEngineAppThread::process(): break!");
	}

	private void _thread_wait()
	{
		TimeSpan timeSpan = DateTime.Now - _lasttime;
		int num = (int)((double)threadUpdatePeriod - timeSpan.TotalMilliseconds);
		if (num < 0)
		{
			num = 0;
		}
		Thread.Sleep(num);
		_lasttime = DateTime.Now;
	}

	public override void destroy()
	{
		Dbg.WARNING_MSG("KBEngineAppThread::destroy()");
		breakProcess();
		int num = 0;
		while (!kbethread.over && num < 50)
		{
			Thread.Sleep(100);
			num++;
		}
		if (_t != null)
		{
			_t.Abort();
		}
		_t = null;
		base.destroy();
	}
}
