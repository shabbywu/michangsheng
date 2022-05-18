using System;
using System.Collections.Generic;
using System.Linq;
using MoonSharp.Interpreter.Compatibility;
using MoonSharp.Interpreter.Interop.BasicDescriptors;
using MoonSharp.Interpreter.Interop.Converters;

namespace MoonSharp.Interpreter.Interop
{
	// Token: 0x0200111C RID: 4380
	public class OverloadedMethodMemberDescriptor : IOptimizableDescriptor, IMemberDescriptor, IWireableDescriptor
	{
		// Token: 0x17000994 RID: 2452
		// (get) Token: 0x060069BD RID: 27069 RVA: 0x00048323 File Offset: 0x00046523
		// (set) Token: 0x060069BE RID: 27070 RVA: 0x0004832B File Offset: 0x0004652B
		public bool IgnoreExtensionMethods { get; set; }

		// Token: 0x060069BF RID: 27071 RVA: 0x00048334 File Offset: 0x00046534
		public OverloadedMethodMemberDescriptor(string name, Type declaringType)
		{
			this.Name = name;
			this.DeclaringType = declaringType;
		}

		// Token: 0x060069C0 RID: 27072 RVA: 0x00048373 File Offset: 0x00046573
		public OverloadedMethodMemberDescriptor(string name, Type declaringType, IOverloadableMemberDescriptor descriptor) : this(name, declaringType)
		{
			this.m_Overloads.Add(descriptor);
		}

		// Token: 0x060069C1 RID: 27073 RVA: 0x00048389 File Offset: 0x00046589
		public OverloadedMethodMemberDescriptor(string name, Type declaringType, IEnumerable<IOverloadableMemberDescriptor> descriptors) : this(name, declaringType)
		{
			this.m_Overloads.AddRange(descriptors);
		}

		// Token: 0x060069C2 RID: 27074 RVA: 0x0004839F File Offset: 0x0004659F
		internal void SetExtensionMethodsSnapshot(int version, List<IOverloadableMemberDescriptor> extMethods)
		{
			this.m_ExtOverloads = extMethods;
			this.m_ExtensionMethodVersion = version;
		}

		// Token: 0x17000995 RID: 2453
		// (get) Token: 0x060069C3 RID: 27075 RVA: 0x000483AF File Offset: 0x000465AF
		// (set) Token: 0x060069C4 RID: 27076 RVA: 0x000483B7 File Offset: 0x000465B7
		public string Name { get; private set; }

		// Token: 0x17000996 RID: 2454
		// (get) Token: 0x060069C5 RID: 27077 RVA: 0x000483C0 File Offset: 0x000465C0
		// (set) Token: 0x060069C6 RID: 27078 RVA: 0x000483C8 File Offset: 0x000465C8
		public Type DeclaringType { get; private set; }

		// Token: 0x060069C7 RID: 27079 RVA: 0x000483D1 File Offset: 0x000465D1
		public void AddOverload(IOverloadableMemberDescriptor overload)
		{
			this.m_Overloads.Add(overload);
			this.m_Unsorted = true;
		}

		// Token: 0x17000997 RID: 2455
		// (get) Token: 0x060069C8 RID: 27080 RVA: 0x000483E6 File Offset: 0x000465E6
		public int OverloadCount
		{
			get
			{
				return this.m_Overloads.Count;
			}
		}

		// Token: 0x060069C9 RID: 27081 RVA: 0x0028E4D0 File Offset: 0x0028C6D0
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

		// Token: 0x060069CA RID: 27082 RVA: 0x0028E6A4 File Offset: 0x0028C8A4
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

		// Token: 0x060069CB RID: 27083 RVA: 0x0028E7FC File Offset: 0x0028C9FC
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

		// Token: 0x060069CC RID: 27084 RVA: 0x0028E8AC File Offset: 0x0028CAAC
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

		// Token: 0x060069CD RID: 27085 RVA: 0x0028EAC0 File Offset: 0x0028CCC0
		private static int CalcScoreForSingleArgument(ParameterDescriptor desc, Type parameterType, DynValue arg, bool isOptional)
		{
			int num = ScriptToClrConversions.DynValueToObjectOfTypeWeight(arg, parameterType, isOptional);
			if (parameterType.IsByRef || desc.IsOut || desc.IsRef)
			{
				num = Math.Max(0, num + -10);
			}
			return num;
		}

		// Token: 0x060069CE RID: 27086 RVA: 0x000483F3 File Offset: 0x000465F3
		public Func<ScriptExecutionContext, CallbackArguments, DynValue> GetCallback(Script script, object obj)
		{
			return (ScriptExecutionContext context, CallbackArguments args) => this.PerformOverloadedCall(script, obj, context, args);
		}

		// Token: 0x060069CF RID: 27087 RVA: 0x0028EAFC File Offset: 0x0028CCFC
		void IOptimizableDescriptor.Optimize()
		{
			foreach (IOptimizableDescriptor optimizableDescriptor in this.m_Overloads.OfType<IOptimizableDescriptor>())
			{
				optimizableDescriptor.Optimize();
			}
		}

		// Token: 0x060069D0 RID: 27088 RVA: 0x0004841A File Offset: 0x0004661A
		public CallbackFunction GetCallbackFunction(Script script, object obj = null)
		{
			return new CallbackFunction(this.GetCallback(script, obj), this.Name);
		}

		// Token: 0x17000998 RID: 2456
		// (get) Token: 0x060069D1 RID: 27089 RVA: 0x0004842F File Offset: 0x0004662F
		public bool IsStatic
		{
			get
			{
				return this.m_Overloads.Any((IOverloadableMemberDescriptor o) => o.IsStatic);
			}
		}

		// Token: 0x17000999 RID: 2457
		// (get) Token: 0x060069D2 RID: 27090 RVA: 0x0002D0EC File Offset: 0x0002B2EC
		public MemberDescriptorAccess MemberAccess
		{
			get
			{
				return MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanExecute;
			}
		}

		// Token: 0x060069D3 RID: 27091 RVA: 0x0004845B File Offset: 0x0004665B
		public DynValue GetValue(Script script, object obj)
		{
			return DynValue.NewCallback(this.GetCallbackFunction(script, obj));
		}

		// Token: 0x060069D4 RID: 27092 RVA: 0x00048074 File Offset: 0x00046274
		public void SetValue(Script script, object obj, DynValue value)
		{
			this.CheckAccess(MemberDescriptorAccess.CanWrite, obj);
		}

		// Token: 0x060069D5 RID: 27093 RVA: 0x0028EB4C File Offset: 0x0028CD4C
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

		// Token: 0x04006056 RID: 24662
		private const int CACHE_SIZE = 5;

		// Token: 0x04006057 RID: 24663
		private List<IOverloadableMemberDescriptor> m_Overloads = new List<IOverloadableMemberDescriptor>();

		// Token: 0x04006058 RID: 24664
		private List<IOverloadableMemberDescriptor> m_ExtOverloads = new List<IOverloadableMemberDescriptor>();

		// Token: 0x04006059 RID: 24665
		private bool m_Unsorted = true;

		// Token: 0x0400605A RID: 24666
		private OverloadedMethodMemberDescriptor.OverloadCacheItem[] m_Cache = new OverloadedMethodMemberDescriptor.OverloadCacheItem[5];

		// Token: 0x0400605B RID: 24667
		private int m_CacheHits;

		// Token: 0x0400605C RID: 24668
		private int m_ExtensionMethodVersion;

		// Token: 0x0200111D RID: 4381
		private class OverloadableMemberDescriptorComparer : IComparer<IOverloadableMemberDescriptor>
		{
			// Token: 0x060069D6 RID: 27094 RVA: 0x0004846A File Offset: 0x0004666A
			public int Compare(IOverloadableMemberDescriptor x, IOverloadableMemberDescriptor y)
			{
				return x.SortDiscriminant.CompareTo(y.SortDiscriminant);
			}
		}

		// Token: 0x0200111E RID: 4382
		private class OverloadCacheItem
		{
			// Token: 0x04006060 RID: 24672
			public bool HasObject;

			// Token: 0x04006061 RID: 24673
			public IOverloadableMemberDescriptor Method;

			// Token: 0x04006062 RID: 24674
			public List<DataType> ArgsDataType;

			// Token: 0x04006063 RID: 24675
			public List<Type> ArgsUserDataType;

			// Token: 0x04006064 RID: 24676
			public int HitIndexAtLastHit;
		}
	}
}
