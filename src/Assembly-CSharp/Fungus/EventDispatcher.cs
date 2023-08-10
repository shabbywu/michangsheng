using System;
using System.Collections.Generic;
using UnityEngine;

namespace Fungus;

public class EventDispatcher : MonoBehaviour
{
	public delegate void TypedDelegate<T>(T e) where T : class;

	protected readonly Dictionary<Type, List<Delegate>> delegates = new Dictionary<Type, List<Delegate>>();

	protected virtual event Action<string> onLog;

	protected virtual List<Delegate> GetDelegateListCopy<T>(T evt)
	{
		Type typeFromHandle = typeof(T);
		if (!delegates.ContainsKey(typeFromHandle))
		{
			return null;
		}
		return new List<Delegate>(delegates[typeFromHandle]);
	}

	protected virtual void Log(string message)
	{
		if (this.onLog != null)
		{
			this.onLog(message);
		}
	}

	public virtual void AddLog(Action<string> log)
	{
		onLog += log;
	}

	public virtual void RemoveLog(Action<string> log)
	{
		onLog -= log;
	}

	public virtual void AddListener<T>(TypedDelegate<T> listener) where T : class
	{
		Type typeFromHandle = typeof(T);
		if (!delegates.ContainsKey(typeFromHandle))
		{
			delegates.Add(typeFromHandle, new List<Delegate>());
		}
		List<Delegate> list = delegates[typeFromHandle];
		if (!list.Contains(listener))
		{
			list.Add(listener);
		}
	}

	public virtual void RemoveListener<T>(TypedDelegate<T> listener) where T : class
	{
		Type typeFromHandle = typeof(T);
		if (delegates.ContainsKey(typeFromHandle))
		{
			delegates[typeFromHandle].Remove(listener);
		}
	}

	public virtual void Raise<T>(T evt) where T : class
	{
		if (evt == null)
		{
			Log("Raised a null event");
			return;
		}
		List<Delegate> delegateListCopy = GetDelegateListCopy(evt);
		if (delegateListCopy == null || delegateListCopy.Count < 1)
		{
			Log("Raised an event with no listeners");
			return;
		}
		for (int i = 0; i < delegateListCopy.Count; i++)
		{
			if (delegateListCopy[i] is TypedDelegate<T> typedDelegate)
			{
				try
				{
					typedDelegate(evt);
				}
				catch (Exception ex)
				{
					Log(ex.Message);
				}
			}
		}
	}

	public virtual void Raise<T>() where T : class, new()
	{
		Raise(new T());
	}

	public virtual void UnregisterAll()
	{
		delegates.Clear();
	}
}
