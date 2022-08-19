using System;
using System.Collections.Generic;
using System.Linq;
using MoonSharp.Interpreter.Compatibility;
using MoonSharp.Interpreter.Interop.BasicDescriptors;
using MoonSharp.Interpreter.Interop.Converters;

namespace MoonSharp.Interpreter.Interop
{
	// Token: 0x02000D21 RID: 3361
	public class OverloadedMethodMemberDescriptor : IOptimizableDescriptor, IMemberDescriptor, IWireableDescriptor
	{
		// Token: 0x17000737 RID: 1847
		// (get) Token: 0x06005E29 RID: 24105 RVA: 0x00265982 File Offset: 0x00263B82
		// (set) Token: 0x06005E2A RID: 24106 RVA: 0x0026598A File Offset: 0x00263B8A
		public bool IgnoreExtensionMethods { get; set; }

		// Token: 0x06005E2B RID: 24107 RVA: 0x00265993 File Offset: 0x00263B93
		public OverloadedMethodMemberDescriptor(string name, Type declaringType)
		{
			this.Name = name;
			this.DeclaringType = declaringType;
		}

		// Token: 0x06005E2C RID: 24108 RVA: 0x002659D2 File Offset: 0x00263BD2
		public OverloadedMethodMemberDescriptor(string name, Type declaringType, IOverloadableMemberDescriptor descriptor) : this(name, declaringType)
		{
			this.m_Overloads.Add(descriptor);
		}

		// Token: 0x06005E2D RID: 24109 RVA: 0x002659E8 File Offset: 0x00263BE8
		public OverloadedMethodMemberDescriptor(string name, Type declaringType, IEnumerable<IOverloadableMemberDescriptor> descriptors) : this(name, declaringType)
		{
			this.m_Overloads.AddRange(descriptors);
		}

		// Token: 0x06005E2E RID: 24110 RVA: 0x002659FE File Offset: 0x00263BFE
		internal void SetExtensionMethodsSnapshot(int version, List<IOverloadableMemberDescriptor> extMethods)
		{
			this.m_ExtOverloads = extMethods;
			this.m_ExtensionMethodVersion = version;
		}

		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x06005E2F RID: 24111 RVA: 0x00265A0E File Offset: 0x00263C0E
		// (set) Token: 0x06005E30 RID: 24112 RVA: 0x00265A16 File Offset: 0x00263C16
		public string Name { get; private set; }

		// Token: 0x17000739 RID: 1849
		// (get) Token: 0x06005E31 RID: 24113 RVA: 0x00265A1F File Offset: 0x00263C1F
		// (set) Token: 0x06005E32 RID: 24114 RVA: 0x00265A27 File Offset: 0x00263C27
		public Type DeclaringType { get; private set; }

		// Token: 0x06005E33 RID: 24115 RVA: 0x00265A30 File Offset: 0x00263C30
		public void AddOverload(IOverloadableMemberDescriptor overload)
		{
			this.m_Overloads.Add(overload);
			this.m_Unsorted = true;
		}

		// Token: 0x1700073A RID: 1850
		// (get) Token: 0x06005E34 RID: 24116 RVA: 0x00265A45 File Offset: 0x00263C45
		public int OverloadCount
		{
			get
			{
				return this.m_Overloads.Count;
			}
		}

		// Token: 0x06005E35 RID: 24117 RVA: 0x00265A54 File Offset: 0x00263C54
		private DynValue PerformOverloadedCall(Script script, object obj, ScriptExecutionContext context, CallbackArguments args)
		{
			bool flag = this.IgnoreExtensionMethods || obj == null || this.m_ExtensionMethodVersion == UserData.GetExtensionMethodsChangeVersion();
			if (this.m_Overloads.Count == 1 && this.m_ExtOverloads.Count == 0 && flag)
			{
				return this.m_Overloads[0].Execute(script, obj, context, args);
			}
			if (this.m_Unsorted)
			{
				this.m_Overloads.Sort(new OverloadedMethodMemberDescriptor.OverloadableMemberDescriptorComparer());
				this.m_Unsorted = false;
			}
			if (flag)
			{
				for (int i = 0; i < this.m_Cache.Length; i++)
				{
					if (this.m_Cache[i] != null && this.CheckMatch(obj != null, args, this.m_Cache[i]))
					{
						return this.m_Cache[i].Method.Execute(script, obj, context, args);
					}
				}
			}
			int num = 0;
			IOverloadableMemberDescriptor overloadableMemberDescriptor = null;
			for (int j = 0; j < this.m_Overloads.Count; j++)
			{
				if (obj != null || this.m_Overloads[j].IsStatic)
				{
					int num2 = this.CalcScoreForOverload(context, args, this.m_Overloads[j], false);
					if (num2 > num)
					{
						num = num2;
						overloadableMemberDescriptor = this.m_Overloads[j];
					}
				}
			}
			if (!this.IgnoreExtensionMethods && obj != null)
			{
				if (!flag)
				{
					this.m_ExtensionMethodVersion = UserData.GetExtensionMethodsChangeVersion();
					this.m_ExtOverloads = UserData.GetExtensionMethodsByNameAndType(this.Name, this.DeclaringType);
				}
				for (int k = 0; k < this.m_ExtOverloads.Count; k++)
				{
					int num3 = this.CalcScoreForOverload(context, args, this.m_ExtOverloads[k], true);
					if (num3 > num)
					{
						num = num3;
						overloadableMemberDescriptor = this.m_ExtOverloads[k];
					}
				}
			}
			if (overloadableMemberDescriptor != null)
			{
				this.Cache(obj != null, args, overloadableMemberDescriptor);
				return overloadableMemberDescriptor.Execute(script, obj, context, args);
			}
			throw new ScriptRuntimeException("function call doesn't match any overload");
		}

		// Token: 0x06005E36 RID: 24118 RVA: 0x00265C28 File Offset: 0x00263E28
		private void Cache(bool hasObject, CallbackArguments args, IOverloadableMemberDescriptor bestOverload)
		{
			int num = int.MaxValue;
			OverloadedMethodMemberDescriptor.OverloadCacheItem overloadCacheItem = null;
			for (int i = 0; i < this.m_Cache.Length; i++)
			{
				if (this.m_Cache[i] == null)
				{
					overloadCacheItem = new OverloadedMethodMemberDescriptor.OverloadCacheItem
					{
						ArgsDataType = new List<DataType>(),
						ArgsUserDataType = new List<Type>()
					};
					this.m_Cache[i] = overloadCacheItem;
					break;
				}
				if (this.m_Cache[i].HitIndexAtLastHit < num)
				{
					num = this.m_Cache[i].HitIndexAtLastHit;
					overloadCacheItem = this.m_Cache[i];
				}
			}
			if (overloadCacheItem == null)
			{
				this.m_Cache = new OverloadedMethodMemberDescriptor.OverloadCacheItem[5];
				overloadCacheItem = new OverloadedMethodMemberDescriptor.OverloadCacheItem
				{
					ArgsDataType = new List<DataType>(),
					ArgsUserDataType = new List<Type>()
				};
				this.m_Cache[0] = overloadCacheItem;
				this.m_CacheHits = 0;
			}
			overloadCacheItem.Method = bestOverload;
			OverloadedMethodMemberDescriptor.OverloadCacheItem overloadCacheItem2 = overloadCacheItem;
			int num2 = this.m_CacheHits + 1;
			this.m_CacheHits = num2;
			overloadCacheItem2.HitIndexAtLastHit = num2;
			overloadCacheItem.ArgsDataType.Clear();
			overloadCacheItem.HasObject = hasObject;
			for (int j = 0; j < args.Count; j++)
			{
				overloadCacheItem.ArgsDataType.Add(args[j].Type);
				if (args[j].Type == DataType.UserData)
				{
					overloadCacheItem.ArgsUserDataType.Add(args[j].UserData.Descriptor.Type);
				}
				else
				{
					overloadCacheItem.ArgsUserDataType.Add(null);
				}
			}
		}

		// Token: 0x06005E37 RID: 24119 RVA: 0x00265D80 File Offset: 0x00263F80
		private bool CheckMatch(bool hasObject, CallbackArguments args, OverloadedMethodMemberDescriptor.OverloadCacheItem overloadCacheItem)
		{
			if (overloadCacheItem.HasObject && !hasObject)
			{
				return false;
			}
			if (args.Count != overloadCacheItem.ArgsDataType.Count)
			{
				return false;
			}
			for (int i = 0; i < args.Count; i++)
			{
				if (args[i].Type != overloadCacheItem.ArgsDataType[i])
				{
					return false;
				}
				if (args[i].Type == DataType.UserData && args[i].UserData.Descriptor.Type != overloadCacheItem.ArgsUserDataType[i])
				{
					return false;
				}
			}
			int num = this.m_CacheHits + 1;
			this.m_CacheHits = num;
			overloadCacheItem.HitIndexAtLastHit = num;
			return true;
		}

		// Token: 0x06005E38 RID: 24120 RVA: 0x00265E30 File Offset: 0x00264030
		private int CalcScoreForOverload(ScriptExecutionContext context, CallbackArguments args, IOverloadableMemberDescriptor method, bool isExtMethod)
		{
			int num = 100;
			int num2 = args.IsMethodCall ? 1 : 0;
			int num3 = num2;
			bool flag = false;
			for (int i = 0; i < method.Parameters.Length; i++)
			{
				if ((!isExtMethod || i != 0) && !method.Parameters[i].IsOut)
				{
					Type type = method.Parameters[i].Type;
					if (!(type == typeof(Script)) && !(type == typeof(ScriptExecutionContext)) && !(type == typeof(CallbackArguments)))
					{
						if (i == method.Parameters.Length - 1 && method.VarArgsArrayType != null)
						{
							int num4 = 0;
							DynValue dynValue = null;
							int num5 = num;
							for (;;)
							{
								DynValue dynValue2 = args.RawGet(num3, false);
								if (dynValue2 == null)
								{
									break;
								}
								if (dynValue == null)
								{
									dynValue = dynValue2;
								}
								num3++;
								num4++;
								int val = OverloadedMethodMemberDescriptor.CalcScoreForSingleArgument(method.Parameters[i], method.VarArgsElementType, dynValue2, false);
								num = Math.Min(num, val);
							}
							if (num4 == 1 && dynValue.Type == DataType.UserData && dynValue.UserData.Object != null && Framework.Do.IsAssignableFrom(method.VarArgsArrayType, dynValue.UserData.Object.GetType()))
							{
								num = num5;
							}
							else
							{
								if (num4 == 0)
								{
									num = Math.Min(num, 40);
								}
								flag = true;
							}
						}
						else
						{
							DynValue arg = args.RawGet(num3, false) ?? DynValue.Void;
							int val2 = OverloadedMethodMemberDescriptor.CalcScoreForSingleArgument(method.Parameters[i], type, arg, method.Parameters[i].HasDefaultValue);
							num = Math.Min(num, val2);
							num3++;
						}
					}
				}
			}
			if (num > 0)
			{
				if (args.Count - num2 <= method.Parameters.Length)
				{
					num += 100;
					num *= 1000;
				}
				else if (flag)
				{
					num--;
					num *= 1000;
				}
				else
				{
					num *= 1000;
					num -= 2 * (args.Count - num2 - method.Parameters.Length);
					num = Math.Max(1, num);
				}
			}
			return num;
		}

		// Token: 0x06005E39 RID: 24121 RVA: 0x00266044 File Offset: 0x00264244
		private static int CalcScoreForSingleArgument(ParameterDescriptor desc, Type parameterType, DynValue arg, bool isOptional)
		{
			int num = ScriptToClrConversions.DynValueToObjectOfTypeWeight(arg, parameterType, isOptional);
			if (parameterType.IsByRef || desc.IsOut || desc.IsRef)
			{
				num = Math.Max(0, num + -10);
			}
			return num;
		}

		// Token: 0x06005E3A RID: 24122 RVA: 0x0026607E File Offset: 0x0026427E
		public Func<ScriptExecutionContext, CallbackArguments, DynValue> GetCallback(Script script, object obj)
		{
			return (ScriptExecutionContext context, CallbackArguments args) => this.PerformOverloadedCall(script, obj, context, args);
		}

		// Token: 0x06005E3B RID: 24123 RVA: 0x002660A8 File Offset: 0x002642A8
		void IOptimizableDescriptor.Optimize()
		{
			foreach (IOptimizableDescriptor optimizableDescriptor in this.m_Overloads.OfType<IOptimizableDescriptor>())
			{
				optimizableDescriptor.Optimize();
			}
		}

		// Token: 0x06005E3C RID: 24124 RVA: 0x002660F8 File Offset: 0x002642F8
		public CallbackFunction GetCallbackFunction(Script script, object obj = null)
		{
			return new CallbackFunction(this.GetCallback(script, obj), this.Name);
		}

		// Token: 0x1700073B RID: 1851
		// (get) Token: 0x06005E3D RID: 24125 RVA: 0x0026610D File Offset: 0x0026430D
		public bool IsStatic
		{
			get
			{
				return this.m_Overloads.Any((IOverloadableMemberDescriptor o) => o.IsStatic);
			}
		}

		// Token: 0x1700073C RID: 1852
		// (get) Token: 0x06005E3E RID: 24126 RVA: 0x0016F21F File Offset: 0x0016D41F
		public MemberDescriptorAccess MemberAccess
		{
			get
			{
				return MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanExecute;
			}
		}

		// Token: 0x06005E3F RID: 24127 RVA: 0x00266139 File Offset: 0x00264339
		public DynValue GetValue(Script script, object obj)
		{
			return DynValue.NewCallback(this.GetCallbackFunction(script, obj));
		}

		// Token: 0x06005E40 RID: 24128 RVA: 0x002645B4 File Offset: 0x002627B4
		public void SetValue(Script script, object obj, DynValue value)
		{
			this.CheckAccess(MemberDescriptorAccess.CanWrite, obj);
		}

		// Token: 0x06005E41 RID: 24129 RVA: 0x00266148 File Offset: 0x00264348
		public void PrepareForWiring(Table t)
		{
			t.Set("class", DynValue.NewString(base.GetType().FullName));
			t.Set("name", DynValue.NewString(this.Name));
			t.Set("decltype", DynValue.NewString(this.DeclaringType.FullName));
			DynValue dynValue = DynValue.NewPrimeTable();
			t.Set("overloads", dynValue);
			int num = 0;
			foreach (IOverloadableMemberDescriptor overloadableMemberDescriptor in this.m_Overloads)
			{
				IWireableDescriptor wireableDescriptor = overloadableMemberDescriptor as IWireableDescriptor;
				if (wireableDescriptor != null)
				{
					DynValue dynValue2 = DynValue.NewPrimeTable();
					dynValue.Table.Set(++num, dynValue2);
					wireableDescriptor.PrepareForWiring(dynValue2.Table);
				}
				else
				{
					dynValue.Table.Set(++num, DynValue.NewString(string.Format("unsupported - {0} is not serializable", overloadableMemberDescriptor.GetType().FullName)));
				}
			}
		}

		// Token: 0x04005430 RID: 21552
		private const int CACHE_SIZE = 5;

		// Token: 0x04005431 RID: 21553
		private List<IOverloadableMemberDescriptor> m_Overloads = new List<IOverloadableMemberDescriptor>();

		// Token: 0x04005432 RID: 21554
		private List<IOverloadableMemberDescriptor> m_ExtOverloads = new List<IOverloadableMemberDescriptor>();

		// Token: 0x04005433 RID: 21555
		private bool m_Unsorted = true;

		// Token: 0x04005434 RID: 21556
		private OverloadedMethodMemberDescriptor.OverloadCacheItem[] m_Cache = new OverloadedMethodMemberDescriptor.OverloadCacheItem[5];

		// Token: 0x04005435 RID: 21557
		private int m_CacheHits;

		// Token: 0x04005436 RID: 21558
		private int m_ExtensionMethodVersion;

		// Token: 0x02001670 RID: 5744
		private class OverloadableMemberDescriptorComparer : IComparer<IOverloadableMemberDescriptor>
		{
			// Token: 0x060086FF RID: 34559 RVA: 0x002E6B3D File Offset: 0x002E4D3D
			public int Compare(IOverloadableMemberDescriptor x, IOverloadableMemberDescriptor y)
			{
				return x.SortDiscriminant.CompareTo(y.SortDiscriminant);
			}
		}

		// Token: 0x02001671 RID: 5745
		private class OverloadCacheItem
		{
			// Token: 0x04007283 RID: 29315
			public bool HasObject;

			// Token: 0x04007284 RID: 29316
			public IOverloadableMemberDescriptor Method;

			// Token: 0x04007285 RID: 29317
			public List<DataType> ArgsDataType;

			// Token: 0x04007286 RID: 29318
			public List<Type> ArgsUserDataType;

			// Token: 0x04007287 RID: 29319
			public int HitIndexAtLastHit;
		}
	}
}
