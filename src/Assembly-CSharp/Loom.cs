using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

public class Loom : MonoBehaviour
{
	public struct NoDelayedQueueItem
	{
		public Action<object> action;

		public object param;
	}

	public struct DelayedQueueItem
	{
		public float time;

		public Action<object> action;

		public object param;
	}

	public static int maxThreads = Environment.ProcessorCount;

	private static int numThreads;

	private static Loom _current;

	private static bool initialized;

	private List<NoDelayedQueueItem> _actions = new List<NoDelayedQueueItem>();

	private List<DelayedQueueItem> _delayed = new List<DelayedQueueItem>();

	private List<DelayedQueueItem> _currentDelayed = new List<DelayedQueueItem>();

	private List<NoDelayedQueueItem> _currentActions = new List<NoDelayedQueueItem>();

	public static Loom Current
	{
		get
		{
			Initialize();
			return _current;
		}
	}

	private void Awake()
	{
		_current = this;
		initialized = true;
	}

	public static void Initialize()
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Expected O, but got Unknown
		if (!initialized && Application.isPlaying)
		{
			maxThreads = Environment.ProcessorCount;
			initialized = true;
			GameObject val = new GameObject("Loom");
			_current = val.AddComponent<Loom>();
			Object.DontDestroyOnLoad((Object)val);
		}
	}

	public static void QueueOnMainThread(Action<object> taction, object tparam)
	{
		QueueOnMainThread(taction, tparam, 0f);
	}

	public static void QueueOnMainThread(Action<object> taction, object tparam, float time)
	{
		if (time != 0f)
		{
			lock (Current._delayed)
			{
				Current._delayed.Add(new DelayedQueueItem
				{
					time = Time.time + time,
					action = taction,
					param = tparam
				});
				return;
			}
		}
		lock (Current._actions)
		{
			Current._actions.Add(new NoDelayedQueueItem
			{
				action = taction,
				param = tparam
			});
		}
	}

	public static Thread RunAsync(Action a)
	{
		Initialize();
		while (numThreads >= maxThreads)
		{
			Thread.Sleep(100);
		}
		Interlocked.Increment(ref numThreads);
		ThreadPool.QueueUserWorkItem(RunAction, a);
		return null;
	}

	private static void RunAction(object action)
	{
		try
		{
			((Action)action)();
		}
		catch (Exception ex)
		{
			Debug.LogError((object)ex);
		}
		finally
		{
			Interlocked.Decrement(ref numThreads);
		}
	}

	private void OnDisable()
	{
		if ((Object)(object)_current == (Object)(object)this)
		{
			_current = null;
		}
	}

	private void Start()
	{
	}

	private void Update()
	{
		if (_actions.Count > 0)
		{
			lock (_actions)
			{
				_currentActions.Clear();
				_currentActions.AddRange(_actions);
				_actions.Clear();
			}
			for (int i = 0; i < _currentActions.Count; i++)
			{
				_currentActions[i].action(_currentActions[i].param);
			}
		}
		if (_delayed.Count <= 0)
		{
			return;
		}
		lock (_delayed)
		{
			_currentDelayed.Clear();
			_currentDelayed.AddRange(_delayed.Where((DelayedQueueItem d) => d.time <= Time.time));
			for (int j = 0; j < _currentDelayed.Count; j++)
			{
				_delayed.Remove(_currentDelayed[j]);
			}
		}
		for (int k = 0; k < _currentDelayed.Count; k++)
		{
			_currentDelayed[k].action(_currentDelayed[k].param);
		}
	}
}
