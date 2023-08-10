using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using UnityEngine;

namespace KBEngine;

public class Event
{
	public struct Pair
	{
		public object obj;

		public string funcname;

		public MethodInfo method;
	}

	public struct EventObj
	{
		public Pair info;

		public string eventname;

		public object[] args;
	}

	private static Dictionary<string, List<Pair>> events_out = new Dictionary<string, List<Pair>>();

	public static bool outEventsImmediately = true;

	private static LinkedList<EventObj> firedEvents_out = new LinkedList<EventObj>();

	private static LinkedList<EventObj> doingEvents_out = new LinkedList<EventObj>();

	private static Dictionary<string, List<Pair>> events_in = new Dictionary<string, List<Pair>>();

	private static LinkedList<EventObj> firedEvents_in = new LinkedList<EventObj>();

	private static LinkedList<EventObj> doingEvents_in = new LinkedList<EventObj>();

	private static bool _isPauseOut = false;

	public static void clear()
	{
		events_out.Clear();
		events_in.Clear();
		clearFiredEvents();
	}

	public static void clearFiredEvents()
	{
		monitor_Enter(events_out);
		firedEvents_out.Clear();
		monitor_Exit(events_out);
		doingEvents_out.Clear();
		monitor_Enter(events_in);
		firedEvents_in.Clear();
		monitor_Exit(events_in);
		doingEvents_in.Clear();
		_isPauseOut = false;
	}

	public static void pause()
	{
		_isPauseOut = true;
	}

	public static void resume()
	{
		_isPauseOut = false;
		processOutEvents();
	}

	public static bool isPause()
	{
		return _isPauseOut;
	}

	public static void monitor_Enter(object obj)
	{
		if (KBEngineApp.app != null)
		{
			Monitor.Enter(obj);
		}
	}

	public static void monitor_Exit(object obj)
	{
		if (KBEngineApp.app != null)
		{
			Monitor.Exit(obj);
		}
	}

	public static bool hasRegisterOut(string eventname)
	{
		return _hasRegister(events_out, eventname);
	}

	public static bool hasRegisterIn(string eventname)
	{
		return _hasRegister(events_in, eventname);
	}

	private static bool _hasRegister(Dictionary<string, List<Pair>> events, string eventname)
	{
		monitor_Enter(events);
		bool result = events.ContainsKey(eventname);
		monitor_Exit(events);
		return result;
	}

	public static bool registerOut(string eventname, object obj, string funcname)
	{
		return register(events_out, eventname, obj, funcname);
	}

	public static bool registerOut(string eventname, Action handler)
	{
		return registerOut(eventname, handler.Target, handler.Method.Name);
	}

	public static bool registerOut<T1>(string eventname, Action<T1> handler)
	{
		return registerOut(eventname, handler.Target, handler.Method.Name);
	}

	public static bool registerOut<T1, T2>(string eventname, Action<T1, T2> handler)
	{
		return registerOut(eventname, handler.Target, handler.Method.Name);
	}

	public static bool registerOut<T1, T2, T3>(string eventname, Action<T1, T2, T3> handler)
	{
		return registerOut(eventname, handler.Target, handler.Method.Name);
	}

	public static bool registerOut<T1, T2, T3, T4>(string eventname, Action<T1, T2, T3, T4> handler)
	{
		return registerOut(eventname, handler.Target, handler.Method.Name);
	}

	public static bool registerIn(string eventname, object obj, string funcname)
	{
		return register(events_in, eventname, obj, funcname);
	}

	public static bool registerIn(string eventname, Action handler)
	{
		return registerIn(eventname, handler.Target, handler.Method.Name);
	}

	public static bool registerIn<T1>(string eventname, Action<T1> handler)
	{
		return registerIn(eventname, handler.Target, handler.Method.Name);
	}

	public static bool registerIn<T1, T2>(string eventname, Action<T1, T2> handler)
	{
		return registerIn(eventname, handler.Target, handler.Method.Name);
	}

	public static bool registerIn<T1, T2, T3>(string eventname, Action<T1, T2, T3> handler)
	{
		return registerIn(eventname, handler.Target, handler.Method.Name);
	}

	public static bool registerIn<T1, T2, T3, T4>(string eventname, Action<T1, T2, T3, T4> handler)
	{
		return registerIn(eventname, handler.Target, handler.Method.Name);
	}

	private static bool register(Dictionary<string, List<Pair>> events, string eventname, object obj, string funcname)
	{
		deregister(events, eventname, obj, funcname);
		List<Pair> value = null;
		Pair item = default(Pair);
		item.obj = obj;
		item.funcname = funcname;
		item.method = obj.GetType().GetMethod(funcname);
		if (item.method == null)
		{
			Dbg.ERROR_MSG(string.Concat("Event::register: ", obj, "not found method[", funcname, "]"));
			return false;
		}
		monitor_Enter(events);
		if (!events.TryGetValue(eventname, out value))
		{
			value = new List<Pair>();
			value.Add(item);
			events.Add(eventname, value);
			monitor_Exit(events);
			return true;
		}
		value.Add(item);
		monitor_Exit(events);
		return true;
	}

	public static bool deregisterOut(string eventname, object obj, string funcname)
	{
		removeFiredEventOut(obj, eventname, funcname);
		return deregister(events_out, eventname, obj, funcname);
	}

	public static bool deregisterOut(string eventname, Action handler)
	{
		return deregisterOut(eventname, handler.Target, handler.Method.Name);
	}

	public static bool deregisterIn(string eventname, object obj, string funcname)
	{
		removeFiredEventIn(obj, eventname, funcname);
		return deregister(events_in, eventname, obj, funcname);
	}

	public static bool deregisterIn(string eventname, Action handler)
	{
		return deregisterIn(eventname, handler.Target, handler.Method.Name);
	}

	private static bool deregister(Dictionary<string, List<Pair>> events, string eventname, object obj, string funcname)
	{
		monitor_Enter(events);
		List<Pair> value = null;
		if (!events.TryGetValue(eventname, out value))
		{
			monitor_Exit(events);
			return false;
		}
		for (int i = 0; i < value.Count; i++)
		{
			if (obj == value[i].obj && value[i].funcname == funcname)
			{
				value.RemoveAt(i);
				monitor_Exit(events);
				return true;
			}
		}
		monitor_Exit(events);
		return false;
	}

	public static bool deregisterOut(object obj)
	{
		removeAllFiredEventOut(obj);
		return deregister(events_out, obj);
	}

	public static bool deregisterIn(object obj)
	{
		removeAllFiredEventIn(obj);
		return deregister(events_in, obj);
	}

	private static bool deregister(Dictionary<string, List<Pair>> events, object obj)
	{
		monitor_Enter(events);
		Dictionary<string, List<Pair>>.Enumerator enumerator = events.GetEnumerator();
		while (enumerator.MoveNext())
		{
			List<Pair> value = enumerator.Current.Value;
			for (int num = value.Count - 1; num >= 0; num--)
			{
				if (obj == value[num].obj)
				{
					value.RemoveAt(num);
				}
			}
		}
		monitor_Exit(events);
		return true;
	}

	public static void fireOut(string eventname, params object[] args)
	{
		if (!((Object)(object)RoundManager.instance != (Object)null) || !RoundManager.instance.IsVirtual)
		{
			fire_(events_out, firedEvents_out, eventname, args, outEventsImmediately);
		}
	}

	public static void fireIn(string eventname, params object[] args)
	{
		fire_(events_in, firedEvents_in, eventname, args, eventsImmediately: false);
	}

	public static void fireAll(string eventname, params object[] args)
	{
		fire_(events_in, firedEvents_in, eventname, args, eventsImmediately: false);
		fire_(events_out, firedEvents_out, eventname, args, eventsImmediately: false);
	}

	private static void fire_(Dictionary<string, List<Pair>> events, LinkedList<EventObj> firedEvents, string eventname, object[] args, bool eventsImmediately)
	{
		monitor_Enter(events);
		List<Pair> value = null;
		if (!events.TryGetValue(eventname, out value))
		{
			monitor_Exit(events);
			return;
		}
		if (eventsImmediately && !_isPauseOut)
		{
			for (int i = 0; i < value.Count; i++)
			{
				Pair pair = value[i];
				try
				{
					pair.method.Invoke(pair.obj, args);
				}
				catch (Exception ex)
				{
					Dbg.ERROR_MSG("Event::fire_: event=" + pair.method.DeclaringType.FullName + "::" + pair.funcname + "\n" + ex.ToString());
				}
			}
		}
		else
		{
			for (int j = 0; j < value.Count; j++)
			{
				EventObj value2 = default(EventObj);
				value2.info = value[j];
				value2.eventname = eventname;
				value2.args = args;
				firedEvents.AddLast(value2);
			}
		}
		monitor_Exit(events);
	}

	public static void processOutEvents()
	{
		monitor_Enter(events_out);
		if (firedEvents_out.Count > 0)
		{
			LinkedList<EventObj>.Enumerator enumerator = firedEvents_out.GetEnumerator();
			while (enumerator.MoveNext())
			{
				doingEvents_out.AddLast(enumerator.Current);
			}
			firedEvents_out.Clear();
		}
		monitor_Exit(events_out);
		while (doingEvents_out.Count > 0 && !_isPauseOut)
		{
			EventObj value = doingEvents_out.First.Value;
			try
			{
				value.info.method.Invoke(value.info.obj, value.args);
			}
			catch (Exception ex)
			{
				Dbg.ERROR_MSG("Event::processOutEvents: event=" + value.info.method.DeclaringType.FullName + "::" + value.info.funcname + "\n" + ex.ToString());
			}
			if (doingEvents_out.Count > 0)
			{
				doingEvents_out.RemoveFirst();
			}
		}
	}

	public static void processInEvents()
	{
		monitor_Enter(events_in);
		if (firedEvents_in.Count > 0)
		{
			LinkedList<EventObj>.Enumerator enumerator = firedEvents_in.GetEnumerator();
			while (enumerator.MoveNext())
			{
				doingEvents_in.AddLast(enumerator.Current);
			}
			firedEvents_in.Clear();
		}
		monitor_Exit(events_in);
		while (doingEvents_in.Count > 0)
		{
			EventObj value = doingEvents_in.First.Value;
			try
			{
				value.info.method.Invoke(value.info.obj, value.args);
			}
			catch (Exception ex)
			{
				Dbg.ERROR_MSG("Event::processInEvents: event=" + value.info.method.DeclaringType.FullName + "::" + value.info.funcname + "\n" + ex.ToString());
			}
			if (doingEvents_in.Count > 0)
			{
				doingEvents_in.RemoveFirst();
			}
		}
	}

	public static void removeAllFiredEventIn(object obj)
	{
		removeFiredEvent(firedEvents_in, obj);
	}

	public static void removeAllFiredEventOut(object obj)
	{
		removeFiredEvent(firedEvents_out, obj);
	}

	public static void removeFiredEventIn(object obj, string eventname, string funcname)
	{
		removeFiredEvent(firedEvents_in, obj, eventname, funcname);
	}

	public static void removeFiredEventOut(object obj, string eventname, string funcname)
	{
		removeFiredEvent(firedEvents_out, obj, eventname, funcname);
	}

	public static void removeFiredEvent(LinkedList<EventObj> firedEvents, object obj, string eventname = "", string funcname = "")
	{
		monitor_Enter(firedEvents);
		bool flag;
		do
		{
			flag = false;
			foreach (EventObj firedEvent in firedEvents)
			{
				if (((eventname == "" && funcname == "") || (eventname == firedEvent.eventname && funcname == firedEvent.info.funcname)) && firedEvent.info.obj == obj)
				{
					firedEvents.Remove(firedEvent);
					flag = true;
					break;
				}
			}
		}
		while (flag);
		monitor_Exit(firedEvents);
	}
}
