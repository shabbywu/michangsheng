using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MoonSharp.Interpreter.Compatibility;
using MoonSharp.Interpreter.DataStructs;
using MoonSharp.Interpreter.Interop.BasicDescriptors;
using MoonSharp.Interpreter.Interop.StandardDescriptors;

namespace MoonSharp.Interpreter.Interop
{
	// Token: 0x02000D1E RID: 3358
	public class EventMemberDescriptor : IMemberDescriptor
	{
		// Token: 0x06005DF8 RID: 24056 RVA: 0x00264718 File Offset: 0x00262918
		public static EventMemberDescriptor TryCreateIfVisible(EventInfo ei, InteropAccessMode accessMode)
		{
			if (!EventMemberDescriptor.CheckEventIsCompatible(ei, false))
			{
				return null;
			}
			MethodInfo addMethod = Framework.Do.GetAddMethod(ei);
			MethodInfo removeMethod = Framework.Do.GetRemoveMethod(ei);
			if (ei.GetVisibilityFromAttributes() ?? (removeMethod != null && removeMethod.IsPublic && addMethod != null && addMethod.IsPublic))
			{
				return new EventMemberDescriptor(ei, accessMode);
			}
			return null;
		}

		// Token: 0x06005DF9 RID: 24057 RVA: 0x00264794 File Offset: 0x00262994
		public static bool CheckEventIsCompatible(EventInfo ei, bool throwException)
		{
			if (Framework.Do.IsValueType(ei.DeclaringType))
			{
				if (throwException)
				{
					throw new ArgumentException("Events are not supported on value types");
				}
				return false;
			}
			else if (Framework.Do.GetAddMethod(ei) == null || Framework.Do.GetRemoveMethod(ei) == null)
			{
				if (throwException)
				{
					throw new ArgumentException("Event must have add and remove methods");
				}
				return false;
			}
			else
			{
				MethodInfo method = Framework.Do.GetMethod(ei.EventHandlerType, "Invoke");
				if (method == null)
				{
					if (throwException)
					{
						throw new ArgumentException("Event handler type doesn't seem to be a delegate");
					}
					return false;
				}
				else
				{
					if (!MethodMemberDescriptor.CheckMethodIsCompatible(method, throwException))
					{
						return false;
					}
					if (method.ReturnType != typeof(void))
					{
						if (throwException)
						{
							throw new ArgumentException("Event handler cannot have a return type");
						}
						return false;
					}
					else
					{
						ParameterInfo[] parameters = method.GetParameters();
						if (parameters.Length <= 16)
						{
							ParameterInfo[] array = parameters;
							int i = 0;
							while (i < array.Length)
							{
								ParameterInfo parameterInfo = array[i];
								if (Framework.Do.IsValueType(parameterInfo.ParameterType))
								{
									if (throwException)
									{
										throw new ArgumentException("Event handler cannot have value type parameters");
									}
									return false;
								}
								else if (parameterInfo.ParameterType.IsByRef)
								{
									if (throwException)
									{
										throw new ArgumentException("Event handler cannot have by-ref type parameters");
									}
									return false;
								}
								else
								{
									i++;
								}
							}
							return true;
						}
						if (throwException)
						{
							throw new ArgumentException(string.Format("Event handler cannot have more than {0} parameters", 16));
						}
						return false;
					}
				}
			}
		}

		// Token: 0x06005DFA RID: 24058 RVA: 0x002648DC File Offset: 0x00262ADC
		public EventMemberDescriptor(EventInfo ei, InteropAccessMode accessMode = InteropAccessMode.Default)
		{
			EventMemberDescriptor.CheckEventIsCompatible(ei, true);
			this.EventInfo = ei;
			this.m_Add = Framework.Do.GetAddMethod(ei);
			this.m_Remove = Framework.Do.GetRemoveMethod(ei);
			this.IsStatic = this.m_Add.IsStatic;
		}

		// Token: 0x17000729 RID: 1833
		// (get) Token: 0x06005DFB RID: 24059 RVA: 0x0026495C File Offset: 0x00262B5C
		// (set) Token: 0x06005DFC RID: 24060 RVA: 0x00264964 File Offset: 0x00262B64
		public EventInfo EventInfo { get; private set; }

		// Token: 0x1700072A RID: 1834
		// (get) Token: 0x06005DFD RID: 24061 RVA: 0x0026496D File Offset: 0x00262B6D
		// (set) Token: 0x06005DFE RID: 24062 RVA: 0x00264975 File Offset: 0x00262B75
		public bool IsStatic { get; private set; }

		// Token: 0x06005DFF RID: 24063 RVA: 0x0026497E File Offset: 0x00262B7E
		public DynValue GetValue(Script script, object obj)
		{
			this.CheckAccess(MemberDescriptorAccess.CanRead, obj);
			if (this.IsStatic)
			{
				obj = this;
			}
			return UserData.Create(new EventFacade(this, obj));
		}

		// Token: 0x06005E00 RID: 24064 RVA: 0x002649A0 File Offset: 0x00262BA0
		internal DynValue AddCallback(object o, ScriptExecutionContext context, CallbackArguments args)
		{
			object @lock = this.m_Lock;
			DynValue @void;
			lock (@lock)
			{
				Closure function = args.AsType(0, string.Format("userdata<{0}>.{1}.add", this.EventInfo.DeclaringType, this.EventInfo.Name), DataType.Function, false).Function;
				if (this.m_Callbacks.Add(o, function))
				{
					this.RegisterCallback(o);
				}
				@void = DynValue.Void;
			}
			return @void;
		}

		// Token: 0x06005E01 RID: 24065 RVA: 0x00264A28 File Offset: 0x00262C28
		internal DynValue RemoveCallback(object o, ScriptExecutionContext context, CallbackArguments args)
		{
			object @lock = this.m_Lock;
			DynValue @void;
			lock (@lock)
			{
				Closure function = args.AsType(0, string.Format("userdata<{0}>.{1}.remove", this.EventInfo.DeclaringType, this.EventInfo.Name), DataType.Function, false).Function;
				if (this.m_Callbacks.RemoveValue(o, function))
				{
					this.UnregisterCallback(o);
				}
				@void = DynValue.Void;
			}
			return @void;
		}

		// Token: 0x06005E02 RID: 24066 RVA: 0x00264AB0 File Offset: 0x00262CB0
		private void RegisterCallback(object o)
		{
			this.m_Delegates.GetOrCreate(o, delegate
			{
				Delegate @delegate = this.CreateDelegate(o);
				Delegate delegate2 = Delegate.CreateDelegate(this.EventInfo.EventHandlerType, @delegate.Target, @delegate.Method);
				this.m_Add.Invoke(o, new object[]
				{
					delegate2
				});
				return delegate2;
			});
		}

		// Token: 0x06005E03 RID: 24067 RVA: 0x00264AF0 File Offset: 0x00262CF0
		private void UnregisterCallback(object o)
		{
			Delegate orDefault = this.m_Delegates.GetOrDefault(o);
			if (orDefault == null)
			{
				throw new InternalErrorException("can't unregister null delegate");
			}
			this.m_Delegates.Remove(o);
			this.m_Remove.Invoke(o, new object[]
			{
				orDefault
			});
		}

		// Token: 0x06005E04 RID: 24068 RVA: 0x00264B3C File Offset: 0x00262D3C
		private Delegate CreateDelegate(object sender)
		{
			switch (Framework.Do.GetMethod(this.EventInfo.EventHandlerType, "Invoke").GetParameters().Length)
			{
			case 0:
				return new EventMemberDescriptor.EventWrapper00(delegate()
				{
					this.DispatchEvent(sender, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);
				});
			case 1:
				return new EventMemberDescriptor.EventWrapper01(delegate(object o1)
				{
					this.DispatchEvent(sender, o1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);
				});
			case 2:
				return new EventMemberDescriptor.EventWrapper02(delegate(object o1, object o2)
				{
					this.DispatchEvent(sender, o1, o2, null, null, null, null, null, null, null, null, null, null, null, null, null, null);
				});
			case 3:
				return new EventMemberDescriptor.EventWrapper03(delegate(object o1, object o2, object o3)
				{
					this.DispatchEvent(sender, o1, o2, o3, null, null, null, null, null, null, null, null, null, null, null, null, null);
				});
			case 4:
				return new EventMemberDescriptor.EventWrapper04(delegate(object o1, object o2, object o3, object o4)
				{
					this.DispatchEvent(sender, o1, o2, o3, o4, null, null, null, null, null, null, null, null, null, null, null, null);
				});
			case 5:
				return new EventMemberDescriptor.EventWrapper05(delegate(object o1, object o2, object o3, object o4, object o5)
				{
					this.DispatchEvent(sender, o1, o2, o3, o4, o5, null, null, null, null, null, null, null, null, null, null, null);
				});
			case 6:
				return new EventMemberDescriptor.EventWrapper06(delegate(object o1, object o2, object o3, object o4, object o5, object o6)
				{
					this.DispatchEvent(sender, o1, o2, o3, o4, o5, o6, null, null, null, null, null, null, null, null, null, null);
				});
			case 7:
				return new EventMemberDescriptor.EventWrapper07(delegate(object o1, object o2, object o3, object o4, object o5, object o6, object o7)
				{
					this.DispatchEvent(sender, o1, o2, o3, o4, o5, o6, o7, null, null, null, null, null, null, null, null, null);
				});
			case 8:
				return new EventMemberDescriptor.EventWrapper08(delegate(object o1, object o2, object o3, object o4, object o5, object o6, object o7, object o8)
				{
					this.DispatchEvent(sender, o1, o2, o3, o4, o5, o6, o7, o8, null, null, null, null, null, null, null, null);
				});
			case 9:
				return new EventMemberDescriptor.EventWrapper09(delegate(object o1, object o2, object o3, object o4, object o5, object o6, object o7, object o8, object o9)
				{
					this.DispatchEvent(sender, o1, o2, o3, o4, o5, o6, o7, o8, o9, null, null, null, null, null, null, null);
				});
			case 10:
				return new EventMemberDescriptor.EventWrapper10(delegate(object o1, object o2, object o3, object o4, object o5, object o6, object o7, object o8, object o9, object o10)
				{
					this.DispatchEvent(sender, o1, o2, o3, o4, o5, o6, o7, o8, o9, o10, null, null, null, null, null, null);
				});
			case 11:
				return new EventMemberDescriptor.EventWrapper11(delegate(object o1, object o2, object o3, object o4, object o5, object o6, object o7, object o8, object o9, object o10, object o11)
				{
					this.DispatchEvent(sender, o1, o2, o3, o4, o5, o6, o7, o8, o9, o10, o11, null, null, null, null, null);
				});
			case 12:
				return new EventMemberDescriptor.EventWrapper12(delegate(object o1, object o2, object o3, object o4, object o5, object o6, object o7, object o8, object o9, object o10, object o11, object o12)
				{
					this.DispatchEvent(sender, o1, o2, o3, o4, o5, o6, o7, o8, o9, o10, o11, o12, null, null, null, null);
				});
			case 13:
				return new EventMemberDescriptor.EventWrapper13(delegate(object o1, object o2, object o3, object o4, object o5, object o6, object o7, object o8, object o9, object o10, object o11, object o12, object o13)
				{
					this.DispatchEvent(sender, o1, o2, o3, o4, o5, o6, o7, o8, o9, o10, o11, o12, o13, null, null, null);
				});
			case 14:
				return new EventMemberDescriptor.EventWrapper14(delegate(object o1, object o2, object o3, object o4, object o5, object o6, object o7, object o8, object o9, object o10, object o11, object o12, object o13, object o14)
				{
					this.DispatchEvent(sender, o1, o2, o3, o4, o5, o6, o7, o8, o9, o10, o11, o12, o13, o14, null, null);
				});
			case 15:
				return new EventMemberDescriptor.EventWrapper15(delegate(object o1, object o2, object o3, object o4, object o5, object o6, object o7, object o8, object o9, object o10, object o11, object o12, object o13, object o14, object o15)
				{
					this.DispatchEvent(sender, o1, o2, o3, o4, o5, o6, o7, o8, o9, o10, o11, o12, o13, o14, o15, null);
				});
			case 16:
				return new EventMemberDescriptor.EventWrapper16(delegate(object o1, object o2, object o3, object o4, object o5, object o6, object o7, object o8, object o9, object o10, object o11, object o12, object o13, object o14, object o15, object o16)
				{
					this.DispatchEvent(sender, o1, o2, o3, o4, o5, o6, o7, o8, o9, o10, o11, o12, o13, o14, o15, o16);
				});
			default:
				throw new InternalErrorException("too many args in delegate type");
			}
		}

		// Token: 0x06005E05 RID: 24069 RVA: 0x00264CB8 File Offset: 0x00262EB8
		private void DispatchEvent(object sender, object o01 = null, object o02 = null, object o03 = null, object o04 = null, object o05 = null, object o06 = null, object o07 = null, object o08 = null, object o09 = null, object o10 = null, object o11 = null, object o12 = null, object o13 = null, object o14 = null, object o15 = null, object o16 = null)
		{
			Closure[] array = null;
			object @lock = this.m_Lock;
			lock (@lock)
			{
				array = this.m_Callbacks.Find(sender).ToArray<Closure>();
			}
			Closure[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				array2[i].Call(new object[]
				{
					o01,
					o02,
					o03,
					o04,
					o05,
					o06,
					o07,
					o08,
					o09,
					o10,
					o11,
					o12,
					o13,
					o14,
					o15,
					o16
				});
			}
		}

		// Token: 0x1700072B RID: 1835
		// (get) Token: 0x06005E06 RID: 24070 RVA: 0x00264D80 File Offset: 0x00262F80
		public string Name
		{
			get
			{
				return this.EventInfo.Name;
			}
		}

		// Token: 0x1700072C RID: 1836
		// (get) Token: 0x06005E07 RID: 24071 RVA: 0x00024C5F File Offset: 0x00022E5F
		public MemberDescriptorAccess MemberAccess
		{
			get
			{
				return MemberDescriptorAccess.CanRead;
			}
		}

		// Token: 0x06005E08 RID: 24072 RVA: 0x002645B4 File Offset: 0x002627B4
		public void SetValue(Script script, object obj, DynValue v)
		{
			this.CheckAccess(MemberDescriptorAccess.CanWrite, obj);
		}

		// Token: 0x04005419 RID: 21529
		public const int MAX_ARGS_IN_DELEGATE = 16;

		// Token: 0x0400541A RID: 21530
		private object m_Lock = new object();

		// Token: 0x0400541B RID: 21531
		private MultiDictionary<object, Closure> m_Callbacks = new MultiDictionary<object, Closure>(new ReferenceEqualityComparer());

		// Token: 0x0400541C RID: 21532
		private Dictionary<object, Delegate> m_Delegates = new Dictionary<object, Delegate>(new ReferenceEqualityComparer());

		// Token: 0x0400541F RID: 21535
		private MethodInfo m_Add;

		// Token: 0x04005420 RID: 21536
		private MethodInfo m_Remove;

		// Token: 0x0200165C RID: 5724
		// (Invoke) Token: 0x060086A3 RID: 34467
		private delegate void EventWrapper00();

		// Token: 0x0200165D RID: 5725
		// (Invoke) Token: 0x060086A7 RID: 34471
		private delegate void EventWrapper01(object o1);

		// Token: 0x0200165E RID: 5726
		// (Invoke) Token: 0x060086AB RID: 34475
		private delegate void EventWrapper02(object o1, object o2);

		// Token: 0x0200165F RID: 5727
		// (Invoke) Token: 0x060086AF RID: 34479
		private delegate void EventWrapper03(object o1, object o2, object o3);

		// Token: 0x02001660 RID: 5728
		// (Invoke) Token: 0x060086B3 RID: 34483
		private delegate void EventWrapper04(object o1, object o2, object o3, object o4);

		// Token: 0x02001661 RID: 5729
		// (Invoke) Token: 0x060086B7 RID: 34487
		private delegate void EventWrapper05(object o1, object o2, object o3, object o4, object o5);

		// Token: 0x02001662 RID: 5730
		// (Invoke) Token: 0x060086BB RID: 34491
		private delegate void EventWrapper06(object o1, object o2, object o3, object o4, object o5, object o6);

		// Token: 0x02001663 RID: 5731
		// (Invoke) Token: 0x060086BF RID: 34495
		private delegate void EventWrapper07(object o1, object o2, object o3, object o4, object o5, object o6, object o7);

		// Token: 0x02001664 RID: 5732
		// (Invoke) Token: 0x060086C3 RID: 34499
		private delegate void EventWrapper08(object o1, object o2, object o3, object o4, object o5, object o6, object o7, object o8);

		// Token: 0x02001665 RID: 5733
		// (Invoke) Token: 0x060086C7 RID: 34503
		private delegate void EventWrapper09(object o1, object o2, object o3, object o4, object o5, object o6, object o7, object o8, object o9);

		// Token: 0x02001666 RID: 5734
		// (Invoke) Token: 0x060086CB RID: 34507
		private delegate void EventWrapper10(object o1, object o2, object o3, object o4, object o5, object o6, object o7, object o8, object o9, object o10);

		// Token: 0x02001667 RID: 5735
		// (Invoke) Token: 0x060086CF RID: 34511
		private delegate void EventWrapper11(object o1, object o2, object o3, object o4, object o5, object o6, object o7, object o8, object o9, object o10, object o11);

		// Token: 0x02001668 RID: 5736
		// (Invoke) Token: 0x060086D3 RID: 34515
		private delegate void EventWrapper12(object o1, object o2, object o3, object o4, object o5, object o6, object o7, object o8, object o9, object o10, object o11, object o12);

		// Token: 0x02001669 RID: 5737
		// (Invoke) Token: 0x060086D7 RID: 34519
		private delegate void EventWrapper13(object o1, object o2, object o3, object o4, object o5, object o6, object o7, object o8, object o9, object o10, object o11, object o12, object o13);

		// Token: 0x0200166A RID: 5738
		// (Invoke) Token: 0x060086DB RID: 34523
		private delegate void EventWrapper14(object o1, object o2, object o3, object o4, object o5, object o6, object o7, object o8, object o9, object o10, object o11, object o12, object o13, object o14);

		// Token: 0x0200166B RID: 5739
		// (Invoke) Token: 0x060086DF RID: 34527
		private delegate void EventWrapper15(object o1, object o2, object o3, object o4, object o5, object o6, object o7, object o8, object o9, object o10, object o11, object o12, object o13, object o14, object o15);

		// Token: 0x0200166C RID: 5740
		// (Invoke) Token: 0x060086E3 RID: 34531
		private delegate void EventWrapper16(object o1, object o2, object o3, object o4, object o5, object o6, object o7, object o8, object o9, object o10, object o11, object o12, object o13, object o14, object o15, object o16);
	}
}
