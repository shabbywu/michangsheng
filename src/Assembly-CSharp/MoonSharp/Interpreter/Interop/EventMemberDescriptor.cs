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
	// Token: 0x02001105 RID: 4357
	public class EventMemberDescriptor : IMemberDescriptor
	{
		// Token: 0x0600692F RID: 26927 RVA: 0x0028CF8C File Offset: 0x0028B18C
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

		// Token: 0x06006930 RID: 26928 RVA: 0x0028D008 File Offset: 0x0028B208
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

		// Token: 0x06006931 RID: 26929 RVA: 0x0028D150 File Offset: 0x0028B350
		public EventMemberDescriptor(EventInfo ei, InteropAccessMode accessMode = InteropAccessMode.Default)
		{
			EventMemberDescriptor.CheckEventIsCompatible(ei, true);
			this.EventInfo = ei;
			this.m_Add = Framework.Do.GetAddMethod(ei);
			this.m_Remove = Framework.Do.GetRemoveMethod(ei);
			this.IsStatic = this.m_Add.IsStatic;
		}

		// Token: 0x17000986 RID: 2438
		// (get) Token: 0x06006932 RID: 26930 RVA: 0x000481E7 File Offset: 0x000463E7
		// (set) Token: 0x06006933 RID: 26931 RVA: 0x000481EF File Offset: 0x000463EF
		public EventInfo EventInfo { get; private set; }

		// Token: 0x17000987 RID: 2439
		// (get) Token: 0x06006934 RID: 26932 RVA: 0x000481F8 File Offset: 0x000463F8
		// (set) Token: 0x06006935 RID: 26933 RVA: 0x00048200 File Offset: 0x00046400
		public bool IsStatic { get; private set; }

		// Token: 0x06006936 RID: 26934 RVA: 0x00048209 File Offset: 0x00046409
		public DynValue GetValue(Script script, object obj)
		{
			this.CheckAccess(MemberDescriptorAccess.CanRead, obj);
			if (this.IsStatic)
			{
				obj = this;
			}
			return UserData.Create(new EventFacade(this, obj));
		}

		// Token: 0x06006937 RID: 26935 RVA: 0x0028D1D0 File Offset: 0x0028B3D0
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

		// Token: 0x06006938 RID: 26936 RVA: 0x0028D258 File Offset: 0x0028B458
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

		// Token: 0x06006939 RID: 26937 RVA: 0x0028D2E0 File Offset: 0x0028B4E0
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

		// Token: 0x0600693A RID: 26938 RVA: 0x0028D320 File Offset: 0x0028B520
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

		// Token: 0x0600693B RID: 26939 RVA: 0x0028D36C File Offset: 0x0028B56C
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

		// Token: 0x0600693C RID: 26940 RVA: 0x0028D4E8 File Offset: 0x0028B6E8
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

		// Token: 0x17000988 RID: 2440
		// (get) Token: 0x0600693D RID: 26941 RVA: 0x0004822A File Offset: 0x0004642A
		public string Name
		{
			get
			{
				return this.EventInfo.Name;
			}
		}

		// Token: 0x17000989 RID: 2441
		// (get) Token: 0x0600693E RID: 26942 RVA: 0x0000A093 File Offset: 0x00008293
		public MemberDescriptorAccess MemberAccess
		{
			get
			{
				return MemberDescriptorAccess.CanRead;
			}
		}

		// Token: 0x0600693F RID: 26943 RVA: 0x00048074 File Offset: 0x00046274
		public void SetValue(Script script, object obj, DynValue v)
		{
			this.CheckAccess(MemberDescriptorAccess.CanWrite, obj);
		}

		// Token: 0x04006037 RID: 24631
		public const int MAX_ARGS_IN_DELEGATE = 16;

		// Token: 0x04006038 RID: 24632
		private object m_Lock = new object();

		// Token: 0x04006039 RID: 24633
		private MultiDictionary<object, Closure> m_Callbacks = new MultiDictionary<object, Closure>(new ReferenceEqualityComparer());

		// Token: 0x0400603A RID: 24634
		private Dictionary<object, Delegate> m_Delegates = new Dictionary<object, Delegate>(new ReferenceEqualityComparer());

		// Token: 0x0400603D RID: 24637
		private MethodInfo m_Add;

		// Token: 0x0400603E RID: 24638
		private MethodInfo m_Remove;

		// Token: 0x02001106 RID: 4358
		// (Invoke) Token: 0x06006941 RID: 26945
		private delegate void EventWrapper00();

		// Token: 0x02001107 RID: 4359
		// (Invoke) Token: 0x06006945 RID: 26949
		private delegate void EventWrapper01(object o1);

		// Token: 0x02001108 RID: 4360
		// (Invoke) Token: 0x06006949 RID: 26953
		private delegate void EventWrapper02(object o1, object o2);

		// Token: 0x02001109 RID: 4361
		// (Invoke) Token: 0x0600694D RID: 26957
		private delegate void EventWrapper03(object o1, object o2, object o3);

		// Token: 0x0200110A RID: 4362
		// (Invoke) Token: 0x06006951 RID: 26961
		private delegate void EventWrapper04(object o1, object o2, object o3, object o4);

		// Token: 0x0200110B RID: 4363
		// (Invoke) Token: 0x06006955 RID: 26965
		private delegate void EventWrapper05(object o1, object o2, object o3, object o4, object o5);

		// Token: 0x0200110C RID: 4364
		// (Invoke) Token: 0x06006959 RID: 26969
		private delegate void EventWrapper06(object o1, object o2, object o3, object o4, object o5, object o6);

		// Token: 0x0200110D RID: 4365
		// (Invoke) Token: 0x0600695D RID: 26973
		private delegate void EventWrapper07(object o1, object o2, object o3, object o4, object o5, object o6, object o7);

		// Token: 0x0200110E RID: 4366
		// (Invoke) Token: 0x06006961 RID: 26977
		private delegate void EventWrapper08(object o1, object o2, object o3, object o4, object o5, object o6, object o7, object o8);

		// Token: 0x0200110F RID: 4367
		// (Invoke) Token: 0x06006965 RID: 26981
		private delegate void EventWrapper09(object o1, object o2, object o3, object o4, object o5, object o6, object o7, object o8, object o9);

		// Token: 0x02001110 RID: 4368
		// (Invoke) Token: 0x06006969 RID: 26985
		private delegate void EventWrapper10(object o1, object o2, object o3, object o4, object o5, object o6, object o7, object o8, object o9, object o10);

		// Token: 0x02001111 RID: 4369
		// (Invoke) Token: 0x0600696D RID: 26989
		private delegate void EventWrapper11(object o1, object o2, object o3, object o4, object o5, object o6, object o7, object o8, object o9, object o10, object o11);

		// Token: 0x02001112 RID: 4370
		// (Invoke) Token: 0x06006971 RID: 26993
		private delegate void EventWrapper12(object o1, object o2, object o3, object o4, object o5, object o6, object o7, object o8, object o9, object o10, object o11, object o12);

		// Token: 0x02001113 RID: 4371
		// (Invoke) Token: 0x06006975 RID: 26997
		private delegate void EventWrapper13(object o1, object o2, object o3, object o4, object o5, object o6, object o7, object o8, object o9, object o10, object o11, object o12, object o13);

		// Token: 0x02001114 RID: 4372
		// (Invoke) Token: 0x06006979 RID: 27001
		private delegate void EventWrapper14(object o1, object o2, object o3, object o4, object o5, object o6, object o7, object o8, object o9, object o10, object o11, object o12, object o13, object o14);

		// Token: 0x02001115 RID: 4373
		// (Invoke) Token: 0x0600697D RID: 27005
		private delegate void EventWrapper15(object o1, object o2, object o3, object o4, object o5, object o6, object o7, object o8, object o9, object o10, object o11, object o12, object o13, object o14, object o15);

		// Token: 0x02001116 RID: 4374
		// (Invoke) Token: 0x06006981 RID: 27009
		private delegate void EventWrapper16(object o1, object o2, object o3, object o4, object o5, object o6, object o7, object o8, object o9, object o10, object o11, object o12, object o13, object o14, object o15, object o16);
	}
}
