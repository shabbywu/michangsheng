using System.Collections.Generic;
using UnityEngine.Events;

namespace YSGame;

public class YSFuncList
{
	public Queue<Queue<UnityAction>> funcslist = new Queue<Queue<UnityAction>>();

	private static YSFuncList _Ints;

	public bool FlagSwitch = true;

	public static YSFuncList Ints
	{
		get
		{
			if (_Ints == null)
			{
				_Ints = new YSFuncList();
			}
			return _Ints;
		}
	}

	public void Clear()
	{
		_Ints = null;
	}

	public void AddFunc(Queue<UnityAction> Next)
	{
		funcslist.Enqueue(Next);
		Start();
	}

	public void ClearQueue()
	{
		FlagSwitch = true;
		funcslist = new Queue<Queue<UnityAction>>();
	}

	public void AddFuncItem(UnityAction Next)
	{
		funcslist.Peek().Enqueue(Next);
	}

	public void Start()
	{
		if (FlagSwitch)
		{
			FlagSwitch = false;
			Continue();
		}
	}

	public void Continue()
	{
		if (funcslist.Count == 0)
		{
			FlagSwitch = true;
			return;
		}
		Queue<UnityAction> queue = funcslist.Peek();
		if (queue.Count > 0)
		{
			queue.Dequeue().Invoke();
			queue.TrimExcess();
		}
		else
		{
			funcslist.Dequeue();
			funcslist.TrimExcess();
			Continue();
		}
	}
}
