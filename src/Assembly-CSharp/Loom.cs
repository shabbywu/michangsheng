using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

// Token: 0x0200044A RID: 1098
public class Loom : MonoBehaviour
{
	// Token: 0x1700028A RID: 650
	// (get) Token: 0x060022B3 RID: 8883 RVA: 0x000ED946 File Offset: 0x000EBB46
	public static Loom Current
	{
		get
		{
			Loom.Initialize();
			return Loom._current;
		}
	}

	// Token: 0x060022B4 RID: 8884 RVA: 0x000ED952 File Offset: 0x000EBB52
	private void Awake()
	{
		Loom._current = this;
		Loom.initialized = true;
	}

	// Token: 0x060022B5 RID: 8885 RVA: 0x000ED960 File Offset: 0x000EBB60
	public static void Initialize()
	{
		if (!Loom.initialized)
		{
			if (!Application.isPlaying)
			{
				return;
			}
			Loom.maxThreads = Environment.ProcessorCount;
			Loom.initialized = true;
			GameObject gameObject = new GameObject("Loom");
			Loom._current = gameObject.AddComponent<Loom>();
			Object.DontDestroyOnLoad(gameObject);
		}
	}

	// Token: 0x060022B6 RID: 8886 RVA: 0x000ED99B File Offset: 0x000EBB9B
	public static void QueueOnMainThread(Action<object> taction, object tparam)
	{
		Loom.QueueOnMainThread(taction, tparam, 0f);
	}

	// Token: 0x060022B7 RID: 8887 RVA: 0x000ED9AC File Offset: 0x000EBBAC
	public static void QueueOnMainThread(Action<object> taction, object tparam, float time)
	{
		if (time != 0f)
		{
			List<Loom.DelayedQueueItem> delayed = Loom.Current._delayed;
			lock (delayed)
			{
				Loom.Current._delayed.Add(new Loom.DelayedQueueItem
				{
					time = Time.time + time,
					action = taction,
					param = tparam
				});
				return;
			}
		}
		List<Loom.NoDelayedQueueItem> actions = Loom.Current._actions;
		lock (actions)
		{
			Loom.Current._actions.Add(new Loom.NoDelayedQueueItem
			{
				action = taction,
				param = tparam
			});
		}
	}

	// Token: 0x060022B8 RID: 8888 RVA: 0x000EDA80 File Offset: 0x000EBC80
	public static Thread RunAsync(Action a)
	{
		Loom.Initialize();
		while (Loom.numThreads >= Loom.maxThreads)
		{
			Thread.Sleep(100);
		}
		Interlocked.Increment(ref Loom.numThreads);
		ThreadPool.QueueUserWorkItem(new WaitCallback(Loom.RunAction), a);
		return null;
	}

	// Token: 0x060022B9 RID: 8889 RVA: 0x000EDABC File Offset: 0x000EBCBC
	private static void RunAction(object action)
	{
		try
		{
			((Action)action)();
		}
		catch (Exception ex)
		{
			Debug.LogError(ex);
		}
		finally
		{
			Interlocked.Decrement(ref Loom.numThreads);
		}
	}

	// Token: 0x060022BA RID: 8890 RVA: 0x000EDB08 File Offset: 0x000EBD08
	private void OnDisable()
	{
		if (Loom._current == this)
		{
			Loom._current = null;
		}
	}

	// Token: 0x060022BB RID: 8891 RVA: 0x00004095 File Offset: 0x00002295
	private void Start()
	{
	}

	// Token: 0x060022BC RID: 8892 RVA: 0x000EDB20 File Offset: 0x000EBD20
	private void Update()
	{
		if (this._actions.Count > 0)
		{
			List<Loom.NoDelayedQueueItem> actions = this._actions;
			lock (actions)
			{
				this._currentActions.Clear();
				this._currentActions.AddRange(this._actions);
				this._actions.Clear();
			}
			for (int i = 0; i < this._currentActions.Count; i++)
			{
				this._currentActions[i].action(this._currentActions[i].param);
			}
		}
		if (this._delayed.Count > 0)
		{
			List<Loom.DelayedQueueItem> delayed = this._delayed;
			lock (delayed)
			{
				this._currentDelayed.Clear();
				this._currentDelayed.AddRange(from d in this._delayed
				where d.time <= Time.time
				select d);
				for (int j = 0; j < this._currentDelayed.Count; j++)
				{
					this._delayed.Remove(this._currentDelayed[j]);
				}
			}
			for (int k = 0; k < this._currentDelayed.Count; k++)
			{
				this._currentDelayed[k].action(this._currentDelayed[k].param);
			}
		}
	}

	// Token: 0x04001BFF RID: 7167
	public static int maxThreads = Environment.ProcessorCount;

	// Token: 0x04001C00 RID: 7168
	private static int numThreads;

	// Token: 0x04001C01 RID: 7169
	private static Loom _current;

	// Token: 0x04001C02 RID: 7170
	private static bool initialized;

	// Token: 0x04001C03 RID: 7171
	private List<Loom.NoDelayedQueueItem> _actions = new List<Loom.NoDelayedQueueItem>();

	// Token: 0x04001C04 RID: 7172
	private List<Loom.DelayedQueueItem> _delayed = new List<Loom.DelayedQueueItem>();

	// Token: 0x04001C05 RID: 7173
	private List<Loom.DelayedQueueItem> _currentDelayed = new List<Loom.DelayedQueueItem>();

	// Token: 0x04001C06 RID: 7174
	private List<Loom.NoDelayedQueueItem> _currentActions = new List<Loom.NoDelayedQueueItem>();

	// Token: 0x0200139B RID: 5019
	public struct NoDelayedQueueItem
	{
		// Token: 0x040068DD RID: 26845
		public Action<object> action;

		// Token: 0x040068DE RID: 26846
		public object param;
	}

	// Token: 0x0200139C RID: 5020
	public struct DelayedQueueItem
	{
		// Token: 0x040068DF RID: 26847
		public float time;

		// Token: 0x040068E0 RID: 26848
		public Action<object> action;

		// Token: 0x040068E1 RID: 26849
		public object param;
	}
}
