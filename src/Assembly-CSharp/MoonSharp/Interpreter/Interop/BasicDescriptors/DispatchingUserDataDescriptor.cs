using System;
using System.Collections.Generic;
using System.Linq;
using MoonSharp.Interpreter.Compatibility;
using MoonSharp.Interpreter.Interop.Converters;

namespace MoonSharp.Interpreter.Interop.BasicDescriptors
{
	// Token: 0x02001149 RID: 4425
	public abstract class DispatchingUserDataDescriptor : IUserDataDescriptor, IOptimizableDescriptor
	{
		// Token: 0x170009C1 RID: 2497
		// (get) Token: 0x06006B59 RID: 27481 RVA: 0x00049242 File Offset: 0x00047442
		// (set) Token: 0x06006B5A RID: 27482 RVA: 0x0004924A File Offset: 0x0004744A
		public string Name { get; private set; }

		// Token: 0x170009C2 RID: 2498
		// (get) Token: 0x06006B5B RID: 27483 RVA: 0x00049253 File Offset: 0x00047453
		// (set) Token: 0x06006B5C RID: 27484 RVA: 0x0004925B File Offset: 0x0004745B
		public Type Type { get; private set; }

		// Token: 0x170009C3 RID: 2499
		// (get) Token: 0x06006B5D RID: 27485 RVA: 0x00049264 File Offset: 0x00047464
		// (set) Token: 0x06006B5E RID: 27486 RVA: 0x0004926C File Offset: 0x0004746C
		public string FriendlyName { get; private set; }

		// Token: 0x06006B5F RID: 27487 RVA: 0x00293C84 File Offset: 0x00291E84
		protected DispatchingUserDataDescriptor(Type type, string friendlyName = null)
		{
			this.Type = type;
			this.Name = type.FullName;
			this.FriendlyName = (friendlyName ?? type.Name);
		}

		// Token: 0x06006B60 RID: 27488 RVA: 0x00049275 File Offset: 0x00047475
		public void AddMetaMember(string name, IMemberDescriptor desc)
		{
			if (desc != null)
			{
				this.AddMemberTo(this.m_MetaMembers, name, desc);
			}
		}

		// Token: 0x06006B61 RID: 27489 RVA: 0x00293CD4 File Offset: 0x00291ED4
		public void AddDynValue(string name, DynValue value)
		{
			DynValueMemberDescriptor desc = new DynValueMemberDescriptor(name, value);
			this.AddMemberTo(this.m_Members, name, desc);
		}

		// Token: 0x06006B62 RID: 27490 RVA: 0x00049288 File Offset: 0x00047488
		public void AddMember(string name, IMemberDescriptor desc)
		{
			if (desc != null)
			{
				this.AddMemberTo(this.m_Members, name, desc);
			}
		}

		// Token: 0x170009C4 RID: 2500
		// (get) Token: 0x06006B63 RID: 27491 RVA: 0x0004929B File Offset: 0x0004749B
		public IEnumerable<string> MemberNames
		{
			get
			{
				return this.m_Members.Keys;
			}
		}

		// Token: 0x170009C5 RID: 2501
		// (get) Token: 0x06006B64 RID: 27492 RVA: 0x000492A8 File Offset: 0x000474A8
		public IEnumerable<KeyValuePair<string, IMemberDescriptor>> Members
		{
			get
			{
				return this.m_Members;
			}
		}

		// Token: 0x06006B65 RID: 27493 RVA: 0x000492B0 File Offset: 0x000474B0
		public IMemberDescriptor FindMember(string memberName)
		{
			return this.m_Members.GetOrDefault(memberName);
		}

		// Token: 0x06006B66 RID: 27494 RVA: 0x000492BE File Offset: 0x000474BE
		public void RemoveMember(string memberName)
		{
			this.m_Members.Remove(memberName);
		}

		// Token: 0x170009C6 RID: 2502
		// (get) Token: 0x06006B67 RID: 27495 RVA: 0x000492CD File Offset: 0x000474CD
		public IEnumerable<string> MetaMemberNames
		{
			get
			{
				return this.m_MetaMembers.Keys;
			}
		}

		// Token: 0x170009C7 RID: 2503
		// (get) Token: 0x06006B68 RID: 27496 RVA: 0x000492DA File Offset: 0x000474DA
		public IEnumerable<KeyValuePair<string, IMemberDescriptor>> MetaMembers
		{
			get
			{
				return this.m_MetaMembers;
			}
		}

		// Token: 0x06006B69 RID: 27497 RVA: 0x000492E2 File Offset: 0x000474E2
		public IMemberDescriptor FindMetaMember(string memberName)
		{
			return this.m_MetaMembers.GetOrDefault(memberName);
		}

		// Token: 0x06006B6A RID: 27498 RVA: 0x000492F0 File Offset: 0x000474F0
		public void RemoveMetaMember(string memberName)
		{
			this.m_MetaMembers.Remove(memberName);
		}

		// Token: 0x06006B6B RID: 27499 RVA: 0x00293CF8 File Offset: 0x00291EF8
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

		// Token: 0x06006B6C RID: 27500 RVA: 0x00293D90 File Offset: 0x00291F90
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

		// Token: 0x06006B6D RID: 27501 RVA: 0x00293EA8 File Offset: 0x002920A8
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

		// Token: 0x06006B6E RID: 27502 RVA: 0x000492FF File Offset: 0x000474FF
		public bool HasMember(string exactName)
		{
			return this.m_Members.ContainsKey(exactName);
		}

		// Token: 0x06006B6F RID: 27503 RVA: 0x0004930D File Offset: 0x0004750D
		public bool HasMetaMember(string exactName)
		{
			return this.m_MetaMembers.ContainsKey(exactName);
		}

		// Token: 0x06006B70 RID: 27504 RVA: 0x00293F04 File Offset: 0x00292104
		protected virtual DynValue TryIndex(Script script, object obj, string indexName)
		{
			IMemberDescriptor memberDescriptor;
			if (this.m_Members.TryGetValue(indexName, out memberDescriptor))
			{
				return memberDescriptor.GetValue(script, obj);
			}
			return null;
		}

		// Token: 0x06006B71 RID: 27505 RVA: 0x00293F2C File Offset: 0x0029212C
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

		// Token: 0x06006B72 RID: 27506 RVA: 0x00293FDC File Offset: 0x002921DC
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

		// Token: 0x06006B73 RID: 27507 RVA: 0x00294008 File Offset: 0x00292208
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

		// Token: 0x06006B74 RID: 27508 RVA: 0x0004931B File Offset: 0x0004751B
		protected static string Camelify(string name)
		{
			return DescriptorHelpers.Camelify(name);
		}

		// Token: 0x06006B75 RID: 27509 RVA: 0x00049323 File Offset: 0x00047523
		protected static string UpperFirstLetter(string name)
		{
			return DescriptorHelpers.UpperFirstLetter(name);
		}

		// Token: 0x06006B76 RID: 27510 RVA: 0x00047E8F File Offset: 0x0004608F
		public virtual string AsString(object obj)
		{
			if (obj == null)
			{
				return null;
			}
			return obj.ToString();
		}

		// Token: 0x06006B77 RID: 27511 RVA: 0x002940A0 File Offset: 0x002922A0
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

		// Token: 0x06006B78 RID: 27512 RVA: 0x0029415C File Offset: 0x0029235C
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

		// Token: 0x06006B79 RID: 27513 RVA: 0x002943F0 File Offset: 0x002925F0
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

		// Token: 0x06006B7A RID: 27514 RVA: 0x0029442C File Offset: 0x0029262C
		private DynValue MultiDispatchLessThanOrEqual(Script script, object obj)
		{
			if (obj is IComparable)
			{
				return DynValue.NewCallback((ScriptExecutionContext context, CallbackArguments args) => DynValue.NewBoolean(this.PerformComparison(obj, args[0].ToObject(), args[1].ToObject()) <= 0), null);
			}
			return null;
		}

		// Token: 0x06006B7B RID: 27515 RVA: 0x00294470 File Offset: 0x00292670
		private DynValue MultiDispatchLessThan(Script script, object obj)
		{
			if (obj is IComparable)
			{
				return DynValue.NewCallback((ScriptExecutionContext context, CallbackArguments args) => DynValue.NewBoolean(this.PerformComparison(obj, args[0].ToObject(), args[1].ToObject()) < 0), null);
			}
			return null;
		}

		// Token: 0x06006B7C RID: 27516 RVA: 0x002944B4 File Offset: 0x002926B4
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

		// Token: 0x06006B7D RID: 27517 RVA: 0x0004932B File Offset: 0x0004752B
		private DynValue MultiDispatchEqual(Script script, object obj)
		{
			return DynValue.NewCallback((ScriptExecutionContext context, CallbackArguments args) => DynValue.NewBoolean(this.CheckEquality(obj, args[0].ToObject(), args[1].ToObject())), null);
		}

		// Token: 0x06006B7E RID: 27518 RVA: 0x00049351 File Offset: 0x00047551
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

		// Token: 0x06006B7F RID: 27519 RVA: 0x00294524 File Offset: 0x00292724
		private DynValue DispatchMetaOnMethod(Script script, object obj, string methodName)
		{
			IMemberDescriptor orDefault = this.m_Members.GetOrDefault(methodName);
			if (orDefault != null)
			{
				return orDefault.GetValue(script, obj);
			}
			return null;
		}

		// Token: 0x06006B80 RID: 27520 RVA: 0x0029454C File Offset: 0x0029274C
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

		// Token: 0x06006B81 RID: 27521 RVA: 0x00294588 File Offset: 0x00292788
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

		// Token: 0x06006B82 RID: 27522 RVA: 0x00046989 File Offset: 0x00044B89
		public virtual bool IsTypeCompatible(Type type, object obj)
		{
			return Framework.Do.IsInstanceOfType(type, obj);
		}

		// Token: 0x04006103 RID: 24835
		private int m_ExtMethodsVersion;

		// Token: 0x04006104 RID: 24836
		private Dictionary<string, IMemberDescriptor> m_MetaMembers = new Dictionary<string, IMemberDescriptor>();

		// Token: 0x04006105 RID: 24837
		private Dictionary<string, IMemberDescriptor> m_Members = new Dictionary<string, IMemberDescriptor>();

		// Token: 0x04006106 RID: 24838
		protected const string SPECIALNAME_INDEXER_GET = "get_Item";

		// Token: 0x04006107 RID: 24839
		protected const string SPECIALNAME_INDEXER_SET = "set_Item";

		// Token: 0x04006108 RID: 24840
		protected const string SPECIALNAME_CAST_EXPLICIT = "op_Explicit";

		// Token: 0x04006109 RID: 24841
		protected const string SPECIALNAME_CAST_IMPLICIT = "op_Implicit";
	}
}
