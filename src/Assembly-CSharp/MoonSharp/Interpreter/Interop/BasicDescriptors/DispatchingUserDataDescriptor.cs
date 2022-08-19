using System;
using System.Collections.Generic;
using System.Linq;
using MoonSharp.Interpreter.Compatibility;
using MoonSharp.Interpreter.Interop.Converters;

namespace MoonSharp.Interpreter.Interop.BasicDescriptors
{
	// Token: 0x02000D3F RID: 3391
	public abstract class DispatchingUserDataDescriptor : IUserDataDescriptor, IOptimizableDescriptor
	{
		// Token: 0x17000764 RID: 1892
		// (get) Token: 0x06005F7D RID: 24445 RVA: 0x0026BE64 File Offset: 0x0026A064
		// (set) Token: 0x06005F7E RID: 24446 RVA: 0x0026BE6C File Offset: 0x0026A06C
		public string Name { get; private set; }

		// Token: 0x17000765 RID: 1893
		// (get) Token: 0x06005F7F RID: 24447 RVA: 0x0026BE75 File Offset: 0x0026A075
		// (set) Token: 0x06005F80 RID: 24448 RVA: 0x0026BE7D File Offset: 0x0026A07D
		public Type Type { get; private set; }

		// Token: 0x17000766 RID: 1894
		// (get) Token: 0x06005F81 RID: 24449 RVA: 0x0026BE86 File Offset: 0x0026A086
		// (set) Token: 0x06005F82 RID: 24450 RVA: 0x0026BE8E File Offset: 0x0026A08E
		public string FriendlyName { get; private set; }

		// Token: 0x06005F83 RID: 24451 RVA: 0x0026BE98 File Offset: 0x0026A098
		protected DispatchingUserDataDescriptor(Type type, string friendlyName = null)
		{
			this.Type = type;
			this.Name = type.FullName;
			this.FriendlyName = (friendlyName ?? type.Name);
		}

		// Token: 0x06005F84 RID: 24452 RVA: 0x0026BEE5 File Offset: 0x0026A0E5
		public void AddMetaMember(string name, IMemberDescriptor desc)
		{
			if (desc != null)
			{
				this.AddMemberTo(this.m_MetaMembers, name, desc);
			}
		}

		// Token: 0x06005F85 RID: 24453 RVA: 0x0026BEF8 File Offset: 0x0026A0F8
		public void AddDynValue(string name, DynValue value)
		{
			DynValueMemberDescriptor desc = new DynValueMemberDescriptor(name, value);
			this.AddMemberTo(this.m_Members, name, desc);
		}

		// Token: 0x06005F86 RID: 24454 RVA: 0x0026BF1B File Offset: 0x0026A11B
		public void AddMember(string name, IMemberDescriptor desc)
		{
			if (desc != null)
			{
				this.AddMemberTo(this.m_Members, name, desc);
			}
		}

		// Token: 0x17000767 RID: 1895
		// (get) Token: 0x06005F87 RID: 24455 RVA: 0x0026BF2E File Offset: 0x0026A12E
		public IEnumerable<string> MemberNames
		{
			get
			{
				return this.m_Members.Keys;
			}
		}

		// Token: 0x17000768 RID: 1896
		// (get) Token: 0x06005F88 RID: 24456 RVA: 0x0026BF3B File Offset: 0x0026A13B
		public IEnumerable<KeyValuePair<string, IMemberDescriptor>> Members
		{
			get
			{
				return this.m_Members;
			}
		}

		// Token: 0x06005F89 RID: 24457 RVA: 0x0026BF43 File Offset: 0x0026A143
		public IMemberDescriptor FindMember(string memberName)
		{
			return this.m_Members.GetOrDefault(memberName);
		}

		// Token: 0x06005F8A RID: 24458 RVA: 0x0026BF51 File Offset: 0x0026A151
		public void RemoveMember(string memberName)
		{
			this.m_Members.Remove(memberName);
		}

		// Token: 0x17000769 RID: 1897
		// (get) Token: 0x06005F8B RID: 24459 RVA: 0x0026BF60 File Offset: 0x0026A160
		public IEnumerable<string> MetaMemberNames
		{
			get
			{
				return this.m_MetaMembers.Keys;
			}
		}

		// Token: 0x1700076A RID: 1898
		// (get) Token: 0x06005F8C RID: 24460 RVA: 0x0026BF6D File Offset: 0x0026A16D
		public IEnumerable<KeyValuePair<string, IMemberDescriptor>> MetaMembers
		{
			get
			{
				return this.m_MetaMembers;
			}
		}

		// Token: 0x06005F8D RID: 24461 RVA: 0x0026BF75 File Offset: 0x0026A175
		public IMemberDescriptor FindMetaMember(string memberName)
		{
			return this.m_MetaMembers.GetOrDefault(memberName);
		}

		// Token: 0x06005F8E RID: 24462 RVA: 0x0026BF83 File Offset: 0x0026A183
		public void RemoveMetaMember(string memberName)
		{
			this.m_MetaMembers.Remove(memberName);
		}

		// Token: 0x06005F8F RID: 24463 RVA: 0x0026BF94 File Offset: 0x0026A194
		private void AddMemberTo(Dictionary<string, IMemberDescriptor> members, string name, IMemberDescriptor desc)
		{
			IOverloadableMemberDescriptor overloadableMemberDescriptor = desc as IOverloadableMemberDescriptor;
			if (overloadableMemberDescriptor != null)
			{
				if (!members.ContainsKey(name))
				{
					members.Add(name, new OverloadedMethodMemberDescriptor(name, this.Type, overloadableMemberDescriptor));
					return;
				}
				OverloadedMethodMemberDescriptor overloadedMethodMemberDescriptor = members[name] as OverloadedMethodMemberDescriptor;
				if (overloadedMethodMemberDescriptor != null)
				{
					overloadedMethodMemberDescriptor.AddOverload(overloadableMemberDescriptor);
					return;
				}
				throw new ArgumentException(string.Format("Multiple members named {0} are being added to type {1} and one or more of these members do not support overloads.", name, this.Type.FullName));
			}
			else
			{
				if (members.ContainsKey(name))
				{
					throw new ArgumentException(string.Format("Multiple members named {0} are being added to type {1} and one or more of these members do not support overloads.", name, this.Type.FullName));
				}
				members.Add(name, desc);
				return;
			}
		}

		// Token: 0x06005F90 RID: 24464 RVA: 0x0026C02C File Offset: 0x0026A22C
		public virtual DynValue Index(Script script, object obj, DynValue index, bool isDirectIndexing)
		{
			if (!isDirectIndexing)
			{
				IMemberDescriptor memberDescriptor = this.m_Members.GetOrDefault("get_Item").WithAccessOrNull(MemberDescriptorAccess.CanExecute);
				if (memberDescriptor != null)
				{
					return this.ExecuteIndexer(memberDescriptor, script, obj, index, null);
				}
			}
			index = index.ToScalar();
			if (index.Type != DataType.String)
			{
				return null;
			}
			DynValue dynValue = this.TryIndex(script, obj, index.String);
			if (dynValue == null)
			{
				dynValue = this.TryIndex(script, obj, DispatchingUserDataDescriptor.UpperFirstLetter(index.String));
			}
			if (dynValue == null)
			{
				dynValue = this.TryIndex(script, obj, DispatchingUserDataDescriptor.Camelify(index.String));
			}
			if (dynValue == null)
			{
				dynValue = this.TryIndex(script, obj, DispatchingUserDataDescriptor.UpperFirstLetter(DispatchingUserDataDescriptor.Camelify(index.String)));
			}
			if (dynValue == null && this.m_ExtMethodsVersion < UserData.GetExtensionMethodsChangeVersion())
			{
				this.m_ExtMethodsVersion = UserData.GetExtensionMethodsChangeVersion();
				dynValue = this.TryIndexOnExtMethod(script, obj, index.String);
				if (dynValue == null)
				{
					dynValue = this.TryIndexOnExtMethod(script, obj, DispatchingUserDataDescriptor.UpperFirstLetter(index.String));
				}
				if (dynValue == null)
				{
					dynValue = this.TryIndexOnExtMethod(script, obj, DispatchingUserDataDescriptor.Camelify(index.String));
				}
				if (dynValue == null)
				{
					dynValue = this.TryIndexOnExtMethod(script, obj, DispatchingUserDataDescriptor.UpperFirstLetter(DispatchingUserDataDescriptor.Camelify(index.String)));
				}
			}
			return dynValue;
		}

		// Token: 0x06005F91 RID: 24465 RVA: 0x0026C144 File Offset: 0x0026A344
		private DynValue TryIndexOnExtMethod(Script script, object obj, string indexName)
		{
			List<IOverloadableMemberDescriptor> extensionMethodsByNameAndType = UserData.GetExtensionMethodsByNameAndType(indexName, this.Type);
			if (extensionMethodsByNameAndType != null && extensionMethodsByNameAndType.Count > 0)
			{
				OverloadedMethodMemberDescriptor overloadedMethodMemberDescriptor = new OverloadedMethodMemberDescriptor(indexName, this.Type);
				overloadedMethodMemberDescriptor.SetExtensionMethodsSnapshot(UserData.GetExtensionMethodsChangeVersion(), extensionMethodsByNameAndType);
				this.m_Members.Add(indexName, overloadedMethodMemberDescriptor);
				return DynValue.NewCallback(overloadedMethodMemberDescriptor.GetCallback(script, obj), null);
			}
			return null;
		}

		// Token: 0x06005F92 RID: 24466 RVA: 0x0026C1A0 File Offset: 0x0026A3A0
		public bool HasMember(string exactName)
		{
			return this.m_Members.ContainsKey(exactName);
		}

		// Token: 0x06005F93 RID: 24467 RVA: 0x0026C1AE File Offset: 0x0026A3AE
		public bool HasMetaMember(string exactName)
		{
			return this.m_MetaMembers.ContainsKey(exactName);
		}

		// Token: 0x06005F94 RID: 24468 RVA: 0x0026C1BC File Offset: 0x0026A3BC
		protected virtual DynValue TryIndex(Script script, object obj, string indexName)
		{
			IMemberDescriptor memberDescriptor;
			if (this.m_Members.TryGetValue(indexName, out memberDescriptor))
			{
				return memberDescriptor.GetValue(script, obj);
			}
			return null;
		}

		// Token: 0x06005F95 RID: 24469 RVA: 0x0026C1E4 File Offset: 0x0026A3E4
		public virtual bool SetIndex(Script script, object obj, DynValue index, DynValue value, bool isDirectIndexing)
		{
			if (!isDirectIndexing)
			{
				IMemberDescriptor memberDescriptor = this.m_Members.GetOrDefault("set_Item").WithAccessOrNull(MemberDescriptorAccess.CanExecute);
				if (memberDescriptor != null)
				{
					this.ExecuteIndexer(memberDescriptor, script, obj, index, value);
					return true;
				}
			}
			index = index.ToScalar();
			if (index.Type != DataType.String)
			{
				return false;
			}
			bool flag = this.TrySetIndex(script, obj, index.String, value);
			if (!flag)
			{
				flag = this.TrySetIndex(script, obj, DispatchingUserDataDescriptor.UpperFirstLetter(index.String), value);
			}
			if (!flag)
			{
				flag = this.TrySetIndex(script, obj, DispatchingUserDataDescriptor.Camelify(index.String), value);
			}
			if (!flag)
			{
				flag = this.TrySetIndex(script, obj, DispatchingUserDataDescriptor.UpperFirstLetter(DispatchingUserDataDescriptor.Camelify(index.String)), value);
			}
			return flag;
		}

		// Token: 0x06005F96 RID: 24470 RVA: 0x0026C294 File Offset: 0x0026A494
		protected virtual bool TrySetIndex(Script script, object obj, string indexName, DynValue value)
		{
			IMemberDescriptor orDefault = this.m_Members.GetOrDefault(indexName);
			if (orDefault != null)
			{
				orDefault.SetValue(script, obj, value);
				return true;
			}
			return false;
		}

		// Token: 0x06005F97 RID: 24471 RVA: 0x0026C2C0 File Offset: 0x0026A4C0
		void IOptimizableDescriptor.Optimize()
		{
			foreach (IOptimizableDescriptor optimizableDescriptor in this.m_MetaMembers.Values.OfType<IOptimizableDescriptor>())
			{
				optimizableDescriptor.Optimize();
			}
			foreach (IOptimizableDescriptor optimizableDescriptor2 in this.m_Members.Values.OfType<IOptimizableDescriptor>())
			{
				optimizableDescriptor2.Optimize();
			}
		}

		// Token: 0x06005F98 RID: 24472 RVA: 0x0026C358 File Offset: 0x0026A558
		protected static string Camelify(string name)
		{
			return DescriptorHelpers.Camelify(name);
		}

		// Token: 0x06005F99 RID: 24473 RVA: 0x0026C360 File Offset: 0x0026A560
		protected static string UpperFirstLetter(string name)
		{
			return DescriptorHelpers.UpperFirstLetter(name);
		}

		// Token: 0x06005F9A RID: 24474 RVA: 0x00263CC0 File Offset: 0x00261EC0
		public virtual string AsString(object obj)
		{
			if (obj == null)
			{
				return null;
			}
			return obj.ToString();
		}

		// Token: 0x06005F9B RID: 24475 RVA: 0x0026C368 File Offset: 0x0026A568
		protected virtual DynValue ExecuteIndexer(IMemberDescriptor mdesc, Script script, object obj, DynValue index, DynValue value)
		{
			IList<DynValue> list;
			if (index.Type == DataType.Tuple)
			{
				if (value == null)
				{
					list = index.Tuple;
				}
				else
				{
					list = new List<DynValue>(index.Tuple);
					list.Add(value);
				}
			}
			else if (value == null)
			{
				list = new DynValue[]
				{
					index
				};
			}
			else
			{
				list = new DynValue[]
				{
					index,
					value
				};
			}
			CallbackArguments arg = new CallbackArguments(list, false);
			ScriptExecutionContext arg2 = script.CreateDynamicExecutionContext(null);
			DynValue value2 = mdesc.GetValue(script, obj);
			if (value2.Type != DataType.ClrFunction)
			{
				throw new ScriptRuntimeException("a clr callback was expected in member {0}, while a {1} was found", new object[]
				{
					mdesc.Name,
					value2.Type
				});
			}
			return value2.Callback.ClrCallback(arg2, arg);
		}

		// Token: 0x06005F9C RID: 24476 RVA: 0x0026C424 File Offset: 0x0026A624
		public virtual DynValue MetaIndex(Script script, object obj, string metaname)
		{
			IMemberDescriptor orDefault = this.m_MetaMembers.GetOrDefault(metaname);
			if (orDefault != null)
			{
				return orDefault.GetValue(script, obj);
			}
			uint num = <PrivateImplementationDetails>.ComputeStringHash(metaname);
			if (num <= 983266344U)
			{
				if (num <= 586211335U)
				{
					if (num != 444955513U)
					{
						if (num != 470922707U)
						{
							if (num == 586211335U)
							{
								if (metaname == "__sub")
								{
									return this.DispatchMetaOnMethod(script, obj, "op_Subtraction");
								}
							}
						}
						else if (metaname == "__mul")
						{
							return this.DispatchMetaOnMethod(script, obj, "op_Multiply");
						}
					}
					else if (metaname == "__eq")
					{
						return this.MultiDispatchEqual(script, obj);
					}
				}
				else if (num != 698343734U)
				{
					if (num != 731602059U)
					{
						if (num == 983266344U)
						{
							if (metaname == "__le")
							{
								return this.MultiDispatchLessThanOrEqual(script, obj);
							}
						}
					}
					else if (metaname == "__lt")
					{
						return this.MultiDispatchLessThan(script, obj);
					}
				}
				else if (metaname == "__div")
				{
					return this.DispatchMetaOnMethod(script, obj, "op_Division");
				}
			}
			else if (num <= 2173486251U)
			{
				if (num != 1204900801U)
				{
					if (num != 1795331225U)
					{
						if (num == 2173486251U)
						{
							if (metaname == "__unm")
							{
								return this.DispatchMetaOnMethod(script, obj, "op_UnaryNegation");
							}
						}
					}
					else if (metaname == "__iterator")
					{
						return ClrToScriptConversions.EnumerationToDynValue(script, obj);
					}
				}
				else if (metaname == "__mod")
				{
					return this.DispatchMetaOnMethod(script, obj, "op_Modulus");
				}
			}
			else if (num <= 2463902914U)
			{
				if (num != 2293762610U)
				{
					if (num == 2463902914U)
					{
						if (metaname == "__tobool")
						{
							return this.TryDispatchToBool(script, obj);
						}
					}
				}
				else if (metaname == "__len")
				{
					return this.TryDispatchLength(script, obj);
				}
			}
			else if (num != 3367840379U)
			{
				if (num == 4200909926U)
				{
					if (metaname == "__add")
					{
						return this.DispatchMetaOnMethod(script, obj, "op_Addition");
					}
				}
			}
			else if (metaname == "__tonumber")
			{
				return this.TryDispatchToNumber(script, obj);
			}
			return null;
		}

		// Token: 0x06005F9D RID: 24477 RVA: 0x0026C6B8 File Offset: 0x0026A8B8
		private int PerformComparison(object obj, object p1, object p2)
		{
			IComparable comparable = (IComparable)obj;
			if (comparable != null)
			{
				if (obj == p1)
				{
					return comparable.CompareTo(p2);
				}
				if (obj == p2)
				{
					return -comparable.CompareTo(p1);
				}
			}
			throw new InternalErrorException("unexpected case");
		}

		// Token: 0x06005F9E RID: 24478 RVA: 0x0026C6F4 File Offset: 0x0026A8F4
		private DynValue MultiDispatchLessThanOrEqual(Script script, object obj)
		{
			if (obj is IComparable)
			{
				return DynValue.NewCallback((ScriptExecutionContext context, CallbackArguments args) => DynValue.NewBoolean(this.PerformComparison(obj, args[0].ToObject(), args[1].ToObject()) <= 0), null);
			}
			return null;
		}

		// Token: 0x06005F9F RID: 24479 RVA: 0x0026C738 File Offset: 0x0026A938
		private DynValue MultiDispatchLessThan(Script script, object obj)
		{
			if (obj is IComparable)
			{
				return DynValue.NewCallback((ScriptExecutionContext context, CallbackArguments args) => DynValue.NewBoolean(this.PerformComparison(obj, args[0].ToObject(), args[1].ToObject()) < 0), null);
			}
			return null;
		}

		// Token: 0x06005FA0 RID: 24480 RVA: 0x0026C77C File Offset: 0x0026A97C
		private DynValue TryDispatchLength(Script script, object obj)
		{
			if (obj == null)
			{
				return null;
			}
			IMemberDescriptor orDefault = this.m_Members.GetOrDefault("Length");
			if (orDefault != null && orDefault.CanRead() && !orDefault.CanExecute())
			{
				return orDefault.GetGetterCallbackAsDynValue(script, obj);
			}
			IMemberDescriptor orDefault2 = this.m_Members.GetOrDefault("Count");
			if (orDefault2 != null && orDefault2.CanRead() && !orDefault2.CanExecute())
			{
				return orDefault2.GetGetterCallbackAsDynValue(script, obj);
			}
			return null;
		}

		// Token: 0x06005FA1 RID: 24481 RVA: 0x0026C7E9 File Offset: 0x0026A9E9
		private DynValue MultiDispatchEqual(Script script, object obj)
		{
			return DynValue.NewCallback((ScriptExecutionContext context, CallbackArguments args) => DynValue.NewBoolean(this.CheckEquality(obj, args[0].ToObject(), args[1].ToObject())), null);
		}

		// Token: 0x06005FA2 RID: 24482 RVA: 0x0026C80F File Offset: 0x0026AA0F
		private bool CheckEquality(object obj, object p1, object p2)
		{
			if (obj != null)
			{
				if (obj == p1)
				{
					return obj.Equals(p2);
				}
				if (obj == p2)
				{
					return obj.Equals(p1);
				}
			}
			if (p1 != null)
			{
				return p1.Equals(p2);
			}
			return p2 == null || p2.Equals(p1);
		}

		// Token: 0x06005FA3 RID: 24483 RVA: 0x0026C844 File Offset: 0x0026AA44
		private DynValue DispatchMetaOnMethod(Script script, object obj, string methodName)
		{
			IMemberDescriptor orDefault = this.m_Members.GetOrDefault(methodName);
			if (orDefault != null)
			{
				return orDefault.GetValue(script, obj);
			}
			return null;
		}

		// Token: 0x06005FA4 RID: 24484 RVA: 0x0026C86C File Offset: 0x0026AA6C
		private DynValue TryDispatchToNumber(Script script, object obj)
		{
			Type[] numericTypesOrdered = NumericConversions.NumericTypesOrdered;
			for (int i = 0; i < numericTypesOrdered.Length; i++)
			{
				string conversionMethodName = numericTypesOrdered[i].GetConversionMethodName();
				DynValue dynValue = this.DispatchMetaOnMethod(script, obj, conversionMethodName);
				if (dynValue != null)
				{
					return dynValue;
				}
			}
			return null;
		}

		// Token: 0x06005FA5 RID: 24485 RVA: 0x0026C8A8 File Offset: 0x0026AAA8
		private DynValue TryDispatchToBool(Script script, object obj)
		{
			string conversionMethodName = typeof(bool).GetConversionMethodName();
			DynValue dynValue = this.DispatchMetaOnMethod(script, obj, conversionMethodName);
			if (dynValue != null)
			{
				return dynValue;
			}
			return this.DispatchMetaOnMethod(script, obj, "op_True");
		}

		// Token: 0x06005FA6 RID: 24486 RVA: 0x00259E25 File Offset: 0x00258025
		public virtual bool IsTypeCompatible(Type type, object obj)
		{
			return Framework.Do.IsInstanceOfType(type, obj);
		}

		// Token: 0x04005493 RID: 21651
		private int m_ExtMethodsVersion;

		// Token: 0x04005494 RID: 21652
		private Dictionary<string, IMemberDescriptor> m_MetaMembers = new Dictionary<string, IMemberDescriptor>();

		// Token: 0x04005495 RID: 21653
		private Dictionary<string, IMemberDescriptor> m_Members = new Dictionary<string, IMemberDescriptor>();

		// Token: 0x04005496 RID: 21654
		protected const string SPECIALNAME_INDEXER_GET = "get_Item";

		// Token: 0x04005497 RID: 21655
		protected const string SPECIALNAME_INDEXER_SET = "set_Item";

		// Token: 0x04005498 RID: 21656
		protected const string SPECIALNAME_CAST_EXPLICIT = "op_Explicit";

		// Token: 0x04005499 RID: 21657
		protected const string SPECIALNAME_CAST_IMPLICIT = "op_Implicit";
	}
}
