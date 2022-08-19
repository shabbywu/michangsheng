using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;

namespace KBEngine
{
	// Token: 0x02000B95 RID: 2965
	public class Event
	{
		// Token: 0x0600523D RID: 21053 RVA: 0x0022EF12 File Offset: 0x0022D112
		public static void clear()
		{
			Event.events_out.Clear();
			Event.events_in.Clear();
			Event.clearFiredEvents();
		}

		// Token: 0x0600523E RID: 21054 RVA: 0x0022EF30 File Offset: 0x0022D130
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

		// Token: 0x0600523F RID: 21055 RVA: 0x0022EF93 File Offset: 0x0022D193
		public static void pause()
		{
			Event._isPauseOut = true;
		}

		// Token: 0x06005240 RID: 21056 RVA: 0x0022EF9B File Offset: 0x0022D19B
		public static void resume()
		{
			Event._isPauseOut = false;
			Event.processOutEvents();
		}

		// Token: 0x06005241 RID: 21057 RVA: 0x0022EFA8 File Offset: 0x0022D1A8
		public static bool isPause()
		{
			return Event._isPauseOut;
		}

		// Token: 0x06005242 RID: 21058 RVA: 0x0022EFAF File Offset: 0x0022D1AF
		public static void monitor_Enter(object obj)
		{
			if (KBEngineApp.app == null)
			{
				return;
			}
			Monitor.Enter(obj);
		}

		// Token: 0x06005243 RID: 21059 RVA: 0x0022EFBF File Offset: 0x0022D1BF
		public static void monitor_Exit(object obj)
		{
			if (KBEngineApp.app == null)
			{
				return;
			}
			Monitor.Exit(obj);
		}

		// Token: 0x06005244 RID: 21060 RVA: 0x0022EFCF File Offset: 0x0022D1CF
		public static bool hasRegisterOut(string eventname)
		{
			return Event._hasRegister(Event.events_out, eventname);
		}

		// Token: 0x06005245 RID: 21061 RVA: 0x0022EFDC File Offset: 0x0022D1DC
		public static bool hasRegisterIn(string eventname)
		{
			return Event._hasRegister(Event.events_in, eventname);
		}

		// Token: 0x06005246 RID: 21062 RVA: 0x0022EFE9 File Offset: 0x0022D1E9
		private static bool _hasRegister(Dictionary<string, List<Event.Pair>> events, string eventname)
		{
			Event.monitor_Enter(events);
			bool result = events.ContainsKey(eventname);
			Event.monitor_Exit(events);
			return result;
		}

		// Token: 0x06005247 RID: 21063 RVA: 0x0022EFFE File Offset: 0x0022D1FE
		public static bool registerOut(string eventname, object obj, string funcname)
		{
			return Event.register(Event.events_out, eventname, obj, funcname);
		}

		// Token: 0x06005248 RID: 21064 RVA: 0x0022F00D File Offset: 0x0022D20D
		public static bool registerOut(string eventname, Action handler)
		{
			return Event.registerOut(eventname, handler.Target, handler.Method.Name);
		}

		// Token: 0x06005249 RID: 21065 RVA: 0x0022F00D File Offset: 0x0022D20D
		public static bool registerOut<T1>(string eventname, Action<T1> handler)
		{
			return Event.registerOut(eventname, handler.Target, handler.Method.Name);
		}

		// Token: 0x0600524A RID: 21066 RVA: 0x0022F00D File Offset: 0x0022D20D
		public static bool registerOut<T1, T2>(string eventname, Action<T1, T2> handler)
		{
			return Event.registerOut(eventname, handler.Target, handler.Method.Name);
		}

		// Token: 0x0600524B RID: 21067 RVA: 0x0022F00D File Offset: 0x0022D20D
		public static bool registerOut<T1, T2, T3>(string eventname, Action<T1, T2, T3> handler)
		{
			return Event.registerOut(eventname, handler.Target, handler.Method.Name);
		}

		// Token: 0x0600524C RID: 21068 RVA: 0x0022F00D File Offset: 0x0022D20D
		public static bool registerOut<T1, T2, T3, T4>(string eventname, Action<T1, T2, T3, T4> handler)
		{
			return Event.registerOut(eventname, handler.Target, handler.Method.Name);
		}

		// Token: 0x0600524D RID: 21069 RVA: 0x0022F026 File Offset: 0x0022D226
		public static bool registerIn(string eventname, object obj, string funcname)
		{
			return Event.register(Event.events_in, eventname, obj, funcname);
		}

		// Token: 0x0600524E RID: 21070 RVA: 0x0022F035 File Offset: 0x0022D235
		public static bool registerIn(string eventname, Action handler)
		{
			return Event.registerIn(eventname, handler.Target, handler.Method.Name);
		}

		// Token: 0x0600524F RID: 21071 RVA: 0x0022F035 File Offset: 0x0022D235
		public static bool registerIn<T1>(string eventname, Action<T1> handler)
		{
			return Event.registerIn(eventname, handler.Target, handler.Method.Name);
		}

		// Token: 0x06005250 RID: 21072 RVA: 0x0022F035 File Offset: 0x0022D235
		public static bool registerIn<T1, T2>(string eventname, Action<T1, T2> handler)
		{
			return Event.registerIn(eventname, handler.Target, handler.Method.Name);
		}

		// Token: 0x06005251 RID: 21073 RVA: 0x0022F035 File Offset: 0x0022D235
		public static bool registerIn<T1, T2, T3>(string eventname, Action<T1, T2, T3> handler)
		{
			return Event.registerIn(eventname, handler.Target, handler.Method.Name);
		}

		// Token: 0x06005252 RID: 21074 RVA: 0x0022F035 File Offset: 0x0022D235
		public static bool registerIn<T1, T2, T3, T4>(string eventname, Action<T1, T2, T3, T4> handler)
		{
			return Event.registerIn(eventname, handler.Target, handler.Method.Name);
		}

		// Token: 0x06005253 RID: 21075 RVA: 0x0022F050 File Offset: 0x0022D250
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

		// Token: 0x06005254 RID: 21076 RVA: 0x0022F110 File Offset: 0x0022D310
		public static bool deregisterOut(string eventname, object obj, string funcname)
		{
			Event.removeFiredEventOut(obj, eventname, funcname);
			return Event.deregister(Event.events_out, eventname, obj, funcname);
		}

		// Token: 0x06005255 RID: 21077 RVA: 0x0022F127 File Offset: 0x0022D327
		public static bool deregisterOut(string eventname, Action handler)
		{
			return Event.deregisterOut(eventname, handler.Target, handler.Method.Name);
		}

		// Token: 0x06005256 RID: 21078 RVA: 0x0022F140 File Offset: 0x0022D340
		public static bool deregisterIn(string eventname, object obj, string funcname)
		{
			Event.removeFiredEventIn(obj, eventname, funcname);
			return Event.deregister(Event.events_in, eventname, obj, funcname);
		}

		// Token: 0x06005257 RID: 21079 RVA: 0x0022F157 File Offset: 0x0022D357
		public static bool deregisterIn(string eventname, Action handler)
		{
			return Event.deregisterIn(eventname, handler.Target, handler.Method.Name);
		}

		// Token: 0x06005258 RID: 21080 RVA: 0x0022F170 File Offset: 0x0022D370
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

		// Token: 0x06005259 RID: 21081 RVA: 0x0022F1E2 File Offset: 0x0022D3E2
		public static bool deregisterOut(object obj)
		{
			Event.removeAllFiredEventOut(obj);
			return Event.deregister(Event.events_out, obj);
		}

		// Token: 0x0600525A RID: 21082 RVA: 0x0022F1F5 File Offset: 0x0022D3F5
		public static bool deregisterIn(object obj)
		{
			Event.removeAllFiredEventIn(obj);
			return Event.deregister(Event.events_in, obj);
		}

		// Token: 0x0600525B RID: 21083 RVA: 0x0022F208 File Offset: 0x0022D408
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

		// Token: 0x0600525C RID: 21084 RVA: 0x0022F26D File Offset: 0x0022D46D
		public static void fireOut(string eventname, params object[] args)
		{
			if (RoundManager.instance != null && RoundManager.instance.IsVirtual)
			{
				return;
			}
			Event.fire_(Event.events_out, Event.firedEvents_out, eventname, args, Event.outEventsImmediately);
		}

		// Token: 0x0600525D RID: 21085 RVA: 0x0022F29F File Offset: 0x0022D49F
		public static void fireIn(string eventname, params object[] args)
		{
			Event.fire_(Event.events_in, Event.firedEvents_in, eventname, args, false);
		}

		// Token: 0x0600525E RID: 21086 RVA: 0x0022F2B3 File Offset: 0x0022D4B3
		public static void fireAll(string eventname, params object[] args)
		{
			Event.fire_(Event.events_in, Event.firedEvents_in, eventname, args, false);
			Event.fire_(Event.events_out, Event.firedEvents_out, eventname, args, false);
		}

		// Token: 0x0600525F RID: 21087 RVA: 0x0022F2DC File Offset: 0x0022D4DC
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

		// Token: 0x06005260 RID: 21088 RVA: 0x0022F3F0 File Offset: 0x0022D5F0
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

		// Token: 0x06005261 RID: 21089 RVA: 0x0022F524 File Offset: 0x0022D724
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

		// Token: 0x06005262 RID: 21090 RVA: 0x0022F650 File Offset: 0x0022D850
		public static void removeAllFiredEventIn(object obj)
		{
			Event.removeFiredEvent(Event.firedEvents_in, obj, "", "");
		}

		// Token: 0x06005263 RID: 21091 RVA: 0x0022F667 File Offset: 0x0022D867
		public static void removeAllFiredEventOut(object obj)
		{
			Event.removeFiredEvent(Event.firedEvents_out, obj, "", "");
		}

		// Token: 0x06005264 RID: 21092 RVA: 0x0022F67E File Offset: 0x0022D87E
		public static void removeFiredEventIn(object obj, string eventname, string funcname)
		{
			Event.removeFiredEvent(Event.firedEvents_in, obj, eventname, funcname);
		}

		// Token: 0x06005265 RID: 21093 RVA: 0x0022F68D File Offset: 0x0022D88D
		public static void removeFiredEventOut(object obj, string eventname, string funcname)
		{
			Event.removeFiredEvent(Event.firedEvents_out, obj, eventname, funcname);
		}

		// Token: 0x06005266 RID: 21094 RVA: 0x0022F69C File Offset: 0x0022D89C
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

		// Token: 0x04004FD9 RID: 20441
		private static Dictionary<string, List<Event.Pair>> events_out = new Dictionary<string, List<Event.Pair>>();

		// Token: 0x04004FDA RID: 20442
		public static bool outEventsImmediately = true;

		// Token: 0x04004FDB RID: 20443
		private static LinkedList<Event.EventObj> firedEvents_out = new LinkedList<Event.EventObj>();

		// Token: 0x04004FDC RID: 20444
		private static LinkedList<Event.EventObj> doingEvents_out = new LinkedList<Event.EventObj>();

		// Token: 0x04004FDD RID: 20445
		private static Dictionary<string, List<Event.Pair>> events_in = new Dictionary<string, List<Event.Pair>>();

		// Token: 0x04004FDE RID: 20446
		private static LinkedList<Event.EventObj> firedEvents_in = new LinkedList<Event.EventObj>();

		// Token: 0x04004FDF RID: 20447
		private static LinkedList<Event.EventObj> doingEvents_in = new LinkedList<Event.EventObj>();

		// Token: 0x04004FE0 RID: 20448
		private static bool _isPauseOut = false;

		// Token: 0x020015F4 RID: 5620
		public struct Pair
		{
			// Token: 0x040070E5 RID: 28901
			public object obj;

			// Token: 0x040070E6 RID: 28902
			public string funcname;

			// Token: 0x040070E7 RID: 28903
			public MethodInfo method;
		}

		// Token: 0x020015F5 RID: 5621
		public struct EventObj
		{
			// Token: 0x040070E8 RID: 28904
			public Event.Pair info;

			// Token: 0x040070E9 RID: 28905
			public string eventname;

			// Token: 0x040070EA RID: 28906
			public object[] args;
		}
	}
}
