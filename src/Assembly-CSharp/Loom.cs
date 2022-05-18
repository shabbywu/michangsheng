using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

// Token: 0x02000601 RID: 1537
public class Loom : MonoBehaviour
{
	// Token: 0x170002D4 RID: 724
	// (get) Token: 0x06002670 RID: 9840 RVA: 0x0001EA42 File Offset: 0x0001CC42
	public static Loom Current
	{
		get
		{
			Loom.Initialize();
			return Loom._current;
		}
	}

	// Token: 0x06002671 RID: 9841 RVA: 0x0001EA4E File Offset: 0x0001CC4E
	private void Awake()
	{
		Loom._current = this;
		Loom.initialized = true;
	}

	// Token: 0x06002672 RID: 9842 RVA: 0x0001EA5C File Offset: 0x0001CC5C
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

	// Token: 0x06002673 RID: 9843 RVA: 0x0001EA97 File Offset: 0x0001CC97
	public static void QueueOnMainThread(Action<object> taction, object tparam)
	{
		Loom.QueueOnMainThread(taction, tparam, 0f);
	}

	// Token: 0x06002674 RID: 9844 RVA: 0x0012E5B0 File Offset: 0x0012C7B0
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

	// Token: 0x06002675 RID: 9845 RVA: 0x0001EAA5 File Offset: 0x0001CCA5
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

	// Token: 0x06002676 RID: 9846 RVA: 0x0012E684 File Offset: 0x0012C884
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

	// Token: 0x06002677 RID: 9847 RVA: 0x0001EAE0 File Offset: 0x0001CCE0
	private void OnDisable()
	{
		if (Loom._current == this)
		{
			Loom._current = null;
		}
	}

	// Token: 0x06002678 RID: 9848 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x06002679 RID: 9849 RVA: 0x0012E6D0 File Offset: 0x0012C8D0
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

	// Token: 0x040020CB RID: 8395
	public static int maxThreads = Environment.ProcessorCount;

	// Token: 0x040020CC RID: 8396
	private static int numThreads;

	// Token: 0x040020CD RID: 8397
	private static Loom _current;

	// Token: 0x040020CE RID: 8398
	private static bool initialized;

	// Token: 0x040020CF RID: 8399
	private List<Loom.NoDelayedQueueItem> _actions = new List<Loom.NoDelayedQueueItem>();

	// Token: 0x040020D0 RID: 8400
	private List<Loom.DelayedQueueItem> _delayed = new List<Loom.DelayedQueueItem>();

	// Token: 0x040020D1 RID: 8401
	private List<Loom.DelayedQueueItem> _currentDelayed = new List<Loom.DelayedQueueItem>();

	// Token: 0x040020D2 RID: 8402
	private List<Loom.NoDelayedQueueItem> _currentActions = new List<Loom.NoDelayedQueueItem>();

	// Token: 0x02000602 RID: 1538
	public struct NoDelayedQueueItem
	{
		// Token: 0x040020D3 RID: 8403
		public Action<object> action;

		// Token: 0x040020D4 RID: 8404
		public object param;
	}

	// Token: 0x02000603 RID: 1539
	public struct DelayedQueueItem
	{
		// Token: 0x040020D5 RID: 8405
		public float time;

		// Token: 0x040020D6 RID: 8406
		public Action<object> action;

		// Token: 0x040020D7 RID: 8407
		public object param;
	}
}
