using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;

namespace KBEngine
{
	// Token: 0x02000F13 RID: 3859
	public class Event
	{
		// Token: 0x06005C79 RID: 23673 RVA: 0x00041427 File Offset: 0x0003F627
		public static void clear()
		{
			Event.events_out.Clear();
			Event.events_in.Clear();
			Event.clearFiredEvents();
		}

		// Token: 0x06005C7A RID: 23674 RVA: 0x0025D610 File Offset: 0x0025B810
		public static void clearFiredEvents()
		{
			Event.monitor_Enter(Event.events_out);
			Event.firedEvents_out.Clear();
			Event.monitor_Exit(Event.events_out);
			Event.doingEvents_out.Clear();
			Event.monitor_Enter(Event.events_in);
			Event.firedEvents_in.Clear();
			Event.monitor_Exit(Event.events_in);
			Event.doingEvents_in.Clear();
			Event._isPauseOut = false;
		}

		// Token: 0x06005C7B RID: 23675 RVA: 0x00041442 File Offset: 0x0003F642
		public static void pause()
		{
			Event._isPauseOut = true;
		}

		// Token: 0x06005C7C RID: 23676 RVA: 0x0004144A File Offset: 0x0003F64A
		public static void resume()
		{
			Event._isPauseOut = false;
			Event.processOutEvents();
		}

		// Token: 0x06005C7D RID: 23677 RVA: 0x00041457 File Offset: 0x0003F657
		public static bool isPause()
		{
			return Event._isPauseOut;
		}

		// Token: 0x06005C7E RID: 23678 RVA: 0x0004145E File Offset: 0x0003F65E
		public static void monitor_Enter(object obj)
		{
			if (KBEngineApp.app == null)
			{
				return;
			}
			Monitor.Enter(obj);
		}

		// Token: 0x06005C7F RID: 23679 RVA: 0x0004146E File Offset: 0x0003F66E
		public static void monitor_Exit(object obj)
		{
			if (KBEngineApp.app == null)
			{
				return;
			}
			Monitor.Exit(obj);
		}

		// Token: 0x06005C80 RID: 23680 RVA: 0x0004147E File Offset: 0x0003F67E
		public static bool hasRegisterOut(string eventname)
		{
			return Event._hasRegister(Event.events_out, eventname);
		}

		// Token: 0x06005C81 RID: 23681 RVA: 0x0004148B File Offset: 0x0003F68B
		public static bool hasRegisterIn(string eventname)
		{
			return Event._hasRegister(Event.events_in, eventname);
		}

		// Token: 0x06005C82 RID: 23682 RVA: 0x00041498 File Offset: 0x0003F698
		private static bool _hasRegister(Dictionary<string, List<Event.Pair>> events, string eventname)
		{
			Event.monitor_Enter(events);
			bool result = events.ContainsKey(eventname);
			Event.monitor_Exit(events);
			return result;
		}

		// Token: 0x06005C83 RID: 23683 RVA: 0x000414AD File Offset: 0x0003F6AD
		public static bool registerOut(string eventname, object obj, string funcname)
		{
			return Event.register(Event.events_out, eventname, obj, funcname);
		}

		// Token: 0x06005C84 RID: 23684 RVA: 0x000414BC File Offset: 0x0003F6BC
		public static bool registerOut(string eventname, Action handler)
		{
			return Event.registerOut(eventname, handler.Target, handler.Method.Name);
		}

		// Token: 0x06005C85 RID: 23685 RVA: 0x000414BC File Offset: 0x0003F6BC
		public static bool registerOut<T1>(string eventname, Action<T1> handler)
		{
			return Event.registerOut(eventname, handler.Target, handler.Method.Name);
		}

		// Token: 0x06005C86 RID: 23686 RVA: 0x000414BC File Offset: 0x0003F6BC
		public static bool registerOut<T1, T2>(string eventname, Action<T1, T2> handler)
		{
			return Event.registerOut(eventname, handler.Target, handler.Method.Name);
		}

		// Token: 0x06005C87 RID: 23687 RVA: 0x000414BC File Offset: 0x0003F6BC
		public static bool registerOut<T1, T2, T3>(string eventname, Action<T1, T2, T3> handler)
		{
			return Event.registerOut(eventname, handler.Target, handler.Method.Name);
		}

		// Token: 0x06005C88 RID: 23688 RVA: 0x000414BC File Offset: 0x0003F6BC
		public static bool registerOut<T1, T2, T3, T4>(string eventname, Action<T1, T2, T3, T4> handler)
		{
			return Event.registerOut(eventname, handler.Target, handler.Method.Name);
		}

		// Token: 0x06005C89 RID: 23689 RVA: 0x000414D5 File Offset: 0x0003F6D5
		public static bool registerIn(string eventname, object obj, string funcname)
		{
			return Event.register(Event.events_in, eventname, obj, funcname);
		}

		// Token: 0x06005C8A RID: 23690 RVA: 0x000414E4 File Offset: 0x0003F6E4
		public static bool registerIn(string eventname, Action handler)
		{
			return Event.registerIn(eventname, handler.Target, handler.Method.Name);
		}

		// Token: 0x06005C8B RID: 23691 RVA: 0x000414E4 File Offset: 0x0003F6E4
		public static bool registerIn<T1>(string eventname, Action<T1> handler)
		{
			return Event.registerIn(eventname, handler.Target, handler.Method.Name);
		}

		// Token: 0x06005C8C RID: 23692 RVA: 0x000414E4 File Offset: 0x0003F6E4
		public static bool registerIn<T1, T2>(string eventname, Action<T1, T2> handler)
		{
			return Event.registerIn(eventname, handler.Target, handler.Method.Name);
		}

		// Token: 0x06005C8D RID: 23693 RVA: 0x000414E4 File Offset: 0x0003F6E4
		public static bool registerIn<T1, T2, T3>(string eventname, Action<T1, T2, T3> handler)
		{
			return Event.registerIn(eventname, handler.Target, handler.Method.Name);
		}

		// Token: 0x06005C8E RID: 23694 RVA: 0x000414E4 File Offset: 0x0003F6E4
		public static bool registerIn<T1, T2, T3, T4>(string eventname, Action<T1, T2, T3, T4> handler)
		{
			return Event.registerIn(eventname, handler.Target, handler.Method.Name);
		}

		// Token: 0x06005C8F RID: 23695 RVA: 0x0025D674 File Offset: 0x0025B874
		private static bool register(Dictionary<string, List<Event.Pair>> events, string eventname, object obj, string funcname)
		{
			Event.deregister(events, eventname, obj, funcname);
			List<Event.Pair> list = null;
			Event.Pair pair = new Event.Pair
			{
				obj = obj,
				funcname = funcname,
				method = obj.GetType().GetMethod(funcname)
			};
			if (pair.method == null)
			{
				Dbg.ERROR_MSG(string.Concat(new object[]
				{
					"Event::register: ",
					obj,
					"not found method[",
					funcname,
					"]"
				}));
				return false;
			}
			Event.monitor_Enter(events);
			if (!events.TryGetValue(eventname, out list))
			{
				list = new List<Event.Pair>();
				list.Add(pair);
				events.Add(eventname, list);
				Event.monitor_Exit(events);
				return true;
			}
			list.Add(pair);
			Event.monitor_Exit(events);
			return true;
		}

		// Token: 0x06005C90 RID: 23696 RVA: 0x000414FD File Offset: 0x0003F6FD
		public static bool deregisterOut(string eventname, object obj, string funcname)
		{
			Event.removeFiredEventOut(obj, eventname, funcname);
			return Event.deregister(Event.events_out, eventname, obj, funcname);
		}

		// Token: 0x06005C91 RID: 23697 RVA: 0x00041514 File Offset: 0x0003F714
		public static bool deregisterOut(string eventname, Action handler)
		{
			return Event.deregisterOut(eventname, handler.Target, handler.Method.Name);
		}

		// Token: 0x06005C92 RID: 23698 RVA: 0x0004152D File Offset: 0x0003F72D
		public static bool deregisterIn(string eventname, object obj, string funcname)
		{
			Event.removeFiredEventIn(obj, eventname, funcname);
			return Event.deregister(Event.events_in, eventname, obj, funcname);
		}

		// Token: 0x06005C93 RID: 23699 RVA: 0x00041544 File Offset: 0x0003F744
		public static bool deregisterIn(string eventname, Action handler)
		{
			return Event.deregisterIn(eventname, handler.Target, handler.Method.Name);
		}

		// Token: 0x06005C94 RID: 23700 RVA: 0x0025D734 File Offset: 0x0025B934
		private static bool deregister(Dictionary<string, List<Event.Pair>> events, string eventname, object obj, string funcname)
		{
			Event.monitor_Enter(events);
			List<Event.Pair> list = null;
			if (!events.TryGetValue(eventname, out list))
			{
				Event.monitor_Exit(events);
				return false;
			}
			for (int i = 0; i < list.Count; i++)
			{
				if (obj == list[i].obj && list[i].funcname == funcname)
				{
					list.RemoveAt(i);
					Event.monitor_Exit(events);
					return true;
				}
			}
			Event.monitor_Exit(events);
			return false;
		}

		// Token: 0x06005C95 RID: 23701 RVA: 0x0004155D File Offset: 0x0003F75D
		public static bool deregisterOut(object obj)
		{
			Event.removeAllFiredEventOut(obj);
			return Event.deregister(Event.events_out, obj);
		}

		// Token: 0x06005C96 RID: 23702 RVA: 0x00041570 File Offset: 0x0003F770
		public static bool deregisterIn(object obj)
		{
			Event.removeAllFiredEventIn(obj);
			return Event.deregister(Event.events_in, obj);
		}

		// Token: 0x06005C97 RID: 23703 RVA: 0x0025D7A8 File Offset: 0x0025B9A8
		private static bool deregister(Dictionary<string, List<Event.Pair>> events, object obj)
		{
			Event.monitor_Enter(events);
			foreach (KeyValuePair<string, List<Event.Pair>> keyValuePair in events)
			{
				List<Event.Pair> value = keyValuePair.Value;
				for (int i = value.Count - 1; i >= 0; i--)
				{
					if (obj == value[i].obj)
					{
						value.RemoveAt(i);
					}
				}
			}
			Event.monitor_Exit(events);
			return true;
		}

		// Token: 0x06005C98 RID: 23704 RVA: 0x00041583 File Offset: 0x0003F783
		public static void fireOut(string eventname, params object[] args)
		{
			if (RoundManager.instance != null && RoundManager.instance.IsVirtual)
			{
				return;
			}
			Event.fire_(Event.events_out, Event.firedEvents_out, eventname, args, Event.outEventsImmediately);
		}

		// Token: 0x06005C99 RID: 23705 RVA: 0x000415B5 File Offset: 0x0003F7B5
		public static void fireIn(string eventname, params object[] args)
		{
			Event.fire_(Event.events_in, Event.firedEvents_in, eventname, args, false);
		}

		// Token: 0x06005C9A RID: 23706 RVA: 0x000415C9 File Offset: 0x0003F7C9
		public static void fireAll(string eventname, params object[] args)
		{
			Event.fire_(Event.events_in, Event.firedEvents_in, eventname, args, false);
			Event.fire_(Event.events_out, Event.firedEvents_out, eventname, args, false);
		}

		// Token: 0x06005C9B RID: 23707 RVA: 0x0025D810 File Offset: 0x0025BA10
		private static void fire_(Dictionary<string, List<Event.Pair>> events, LinkedList<Event.EventObj> firedEvents, string eventname, object[] args, bool eventsImmediately)
		{
			Event.monitor_Enter(events);
			List<Event.Pair> list = null;
			if (!events.TryGetValue(eventname, out list))
			{
				Event.monitor_Exit(events);
				return;
			}
			if (eventsImmediately && !Event._isPauseOut)
			{
				for (int i = 0; i < list.Count; i++)
				{
					Event.Pair pair = list[i];
					try
					{
						pair.method.Invoke(pair.obj, args);
					}
					catch (Exception ex)
					{
						Dbg.ERROR_MSG(string.Concat(new string[]
						{
							"Event::fire_: event=",
							pair.method.DeclaringType.FullName,
							"::",
							pair.funcname,
							"\n",
							ex.ToString()
						}));
					}
				}
			}
			else
			{
				for (int j = 0; j < list.Count; j++)
				{
					firedEvents.AddLast(new Event.EventObj
					{
						info = list[j],
						eventname = eventname,
						args = args
					});
				}
			}
			Event.monitor_Exit(events);
		}

		// Token: 0x06005C9C RID: 23708 RVA: 0x0025D924 File Offset: 0x0025BB24
		public static void processOutEvents()
		{
			Event.monitor_Enter(Event.events_out);
			if (Event.firedEvents_out.Count > 0)
			{
				foreach (Event.EventObj value in Event.firedEvents_out)
				{
					Event.doingEvents_out.AddLast(value);
				}
				Event.firedEvents_out.Clear();
			}
			Event.monitor_Exit(Event.events_out);
			while (Event.doingEvents_out.Count > 0 && !Event._isPauseOut)
			{
				Event.EventObj value2 = Event.doingEvents_out.First.Value;
				try
				{
					value2.info.method.Invoke(value2.info.obj, value2.args);
				}
				catch (Exception ex)
				{
					Dbg.ERROR_MSG(string.Concat(new string[]
					{
						"Event::processOutEvents: event=",
						value2.info.method.DeclaringType.FullName,
						"::",
						value2.info.funcname,
						"\n",
						ex.ToString()
					}));
				}
				if (Event.doingEvents_out.Count > 0)
				{
					Event.doingEvents_out.RemoveFirst();
				}
			}
		}

		// Token: 0x06005C9D RID: 23709 RVA: 0x0025DA58 File Offset: 0x0025BC58
		public static void processInEvents()
		{
			Event.monitor_Enter(Event.events_in);
			if (Event.firedEvents_in.Count > 0)
			{
				foreach (Event.EventObj value in Event.firedEvents_in)
				{
					Event.doingEvents_in.AddLast(value);
				}
				Event.firedEvents_in.Clear();
			}
			Event.monitor_Exit(Event.events_in);
			while (Event.doingEvents_in.Count > 0)
			{
				Event.EventObj value2 = Event.doingEvents_in.First.Value;
				try
				{
					value2.info.method.Invoke(value2.info.obj, value2.args);
				}
				catch (Exception ex)
				{
					Dbg.ERROR_MSG(string.Concat(new string[]
					{
						"Event::processInEvents: event=",
						value2.info.method.DeclaringType.FullName,
						"::",
						value2.info.funcname,
						"\n",
						ex.ToString()
					}));
				}
				if (Event.doingEvents_in.Count > 0)
				{
					Event.doingEvents_in.RemoveFirst();
				}
			}
		}

		// Token: 0x06005C9E RID: 23710 RVA: 0x000415EF File Offset: 0x0003F7EF
		public static void removeAllFiredEventIn(object obj)
		{
			Event.removeFiredEvent(Event.firedEvents_in, obj, "", "");
		}

		// Token: 0x06005C9F RID: 23711 RVA: 0x00041606 File Offset: 0x0003F806
		public static void removeAllFiredEventOut(object obj)
		{
			Event.removeFiredEvent(Event.firedEvents_out, obj, "", "");
		}

		// Token: 0x06005CA0 RID: 23712 RVA: 0x0004161D File Offset: 0x0003F81D
		public static void removeFiredEventIn(object obj, string eventname, string funcname)
		{
			Event.removeFiredEvent(Event.firedEvents_in, obj, eventname, funcname);
		}

		// Token: 0x06005CA1 RID: 23713 RVA: 0x0004162C File Offset: 0x0003F82C
		public static void removeFiredEventOut(object obj, string eventname, string funcname)
		{
			Event.removeFiredEvent(Event.firedEvents_out, obj, eventname, funcname);
		}

		// Token: 0x06005CA2 RID: 23714 RVA: 0x0025DB84 File Offset: 0x0025BD84
		public static void removeFiredEvent(LinkedList<Event.EventObj> firedEvents, object obj, string eventname = "", string funcname = "")
		{
			Event.monitor_Enter(firedEvents);
			bool flag;
			do
			{
				flag = false;
				foreach (Event.EventObj eventObj in firedEvents)
				{
					if (((eventname == "" && funcname == "") || (eventname == eventObj.eventname && funcname == eventObj.info.funcname)) && eventObj.info.obj == obj)
					{
						firedEvents.Remove(eventObj);
						flag = true;
						break;
					}
				}
			}
			while (flag);
			Event.monitor_Exit(firedEvents);
		}

		// Token: 0x04005A67 RID: 23143
		private static Dictionary<string, List<Event.Pair>> events_out = new Dictionary<string, List<Event.Pair>>();

		// Token: 0x04005A68 RID: 23144
		public static bool outEventsImmediately = true;

		// Token: 0x04005A69 RID: 23145
		private static LinkedList<Event.EventObj> firedEvents_out = new LinkedList<Event.EventObj>();

		// Token: 0x04005A6A RID: 23146
		private static LinkedList<Event.EventObj> doingEvents_out = new LinkedList<Event.EventObj>();

		// Token: 0x04005A6B RID: 23147
		private static Dictionary<string, List<Event.Pair>> events_in = new Dictionary<string, List<Event.Pair>>();

		// Token: 0x04005A6C RID: 23148
		private static LinkedList<Event.EventObj> firedEvents_in = new LinkedList<Event.EventObj>();

		// Token: 0x04005A6D RID: 23149
		private static LinkedList<Event.EventObj> doingEvents_in = new LinkedList<Event.EventObj>();

		// Token: 0x04005A6E RID: 23150
		private static bool _isPauseOut = false;

		// Token: 0x02000F14 RID: 3860
		public struct Pair
		{
			// Token: 0x04005A6F RID: 23151
			public object obj;

			// Token: 0x04005A70 RID: 23152
			public string funcname;

			// Token: 0x04005A71 RID: 23153
			public MethodInfo method;
		}

		// Token: 0x02000F15 RID: 3861
		public struct EventObj
		{
			// Token: 0x04005A72 RID: 23154
			public Event.Pair info;

			// Token: 0x04005A73 RID: 23155
			public string eventname;

			// Token: 0x04005A74 RID: 23156
			public object[] args;
		}
	}
}
