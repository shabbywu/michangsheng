using System;
using MoonSharp.Interpreter.Compatibility;
using MoonSharp.Interpreter.Interop.BasicDescriptors;
using MoonSharp.Interpreter.Interop.Converters;

namespace MoonSharp.Interpreter.Interop
{
	// Token: 0x02000D23 RID: 3363
	public class ValueTypeDefaultCtorMemberDescriptor : IOverloadableMemberDescriptor, IMemberDescriptor, IWireableDescriptor
	{
		// Token: 0x17000744 RID: 1860
		// (get) Token: 0x06005E57 RID: 24151 RVA: 0x00024C5F File Offset: 0x00022E5F
		public bool IsStatic
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000745 RID: 1861
		// (get) Token: 0x06005E58 RID: 24152 RVA: 0x002669FC File Offset: 0x00264BFC
		// (set) Token: 0x06005E59 RID: 24153 RVA: 0x00266A04 File Offset: 0x00264C04
		public string Name { get; private set; }

		// Token: 0x17000746 RID: 1862
		// (get) Token: 0x06005E5A RID: 24154 RVA: 0x00266A0D File Offset: 0x00264C0D
		// (set) Token: 0x06005E5B RID: 24155 RVA: 0x00266A15 File Offset: 0x00264C15
		public Type ValueTypeDefaultCtor { get; private set; }

		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x06005E5C RID: 24156 RVA: 0x00266A1E File Offset: 0x00264C1E
		// (set) Token: 0x06005E5D RID: 24157 RVA: 0x00266A26 File Offset: 0x00264C26
		public ParameterDescriptor[] Parameters { get; private set; }

		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x06005E5E RID: 24158 RVA: 0x000306E7 File Offset: 0x0002E8E7
		public Type ExtensionMethodType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000749 RID: 1865
		// (get) Token: 0x06005E5F RID: 24159 RVA: 0x000306E7 File Offset: 0x0002E8E7
		public Type VarArgsArrayType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700074A RID: 1866
		// (get) Token: 0x06005E60 RID: 24160 RVA: 0x000306E7 File Offset: 0x0002E8E7
		public Type VarArgsElementType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06005E61 RID: 24161 RVA: 0x00266A2F File Offset: 0x00264C2F
		public ValueTypeDefaultCtorMemberDescriptor(Type valueType)
		{
			if (!Framework.Do.IsValueType(valueType))
			{
				throw new ArgumentException("valueType is not a value type");
			}
			this.Name = "__new";
			this.Parameters = new ParameterDescriptor[0];
			this.ValueTypeDefaultCtor = valueType;
		}

		// Token: 0x06005E62 RID: 24162 RVA: 0x00266A70 File Offset: 0x00264C70
		public DynValue Execute(Script script, object obj, ScriptExecutionContext context, CallbackArguments args)
		{
			this.CheckAccess(MemberDescriptorAccess.CanRead, obj);
			object obj2 = Activator.CreateInstance(this.ValueTypeDefaultCtor);
			return ClrToScriptConversions.ObjectToDynValue(script, obj2);
		}

		// Token: 0x1700074B RID: 1867
		// (get) Token: 0x06005E63 RID: 24163 RVA: 0x00266A98 File Offset: 0x00264C98
		public string SortDiscriminant
		{
			get
			{
				return "@.ctor";
			}
		}

		// Token: 0x1700074C RID: 1868
		// (get) Token: 0x06005E64 RID: 24164 RVA: 0x0016F21F File Offset: 0x0016D41F
		public MemberDescriptorAccess MemberAccess
		{
			get
			{
				return MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanExecute;
			}
		}

		// Token: 0x06005E65 RID: 24165 RVA: 0x00266AA0 File Offset: 0x00264CA0
		public DynValue GetValue(Script script, object obj)
		{
			this.CheckAccess(MemberDescriptorAccess.CanRead, obj);
			object obj2 = Activator.CreateInstance(this.ValueTypeDefaultCtor);
			return ClrToScriptConversions.ObjectToDynValue(script, obj2);
		}

		// Token: 0x06005E66 RID: 24166 RVA: 0x002645B4 File Offset: 0x002627B4
		public void SetValue(Script script, object obj, DynValue value)
		{
			this.CheckAccess(MemberDescriptorAccess.CanWrite, obj);
		}

		// Token: 0x06005E67 RID: 24167 RVA: 0x00266AC8 File Offset: 0x00264CC8
		public void PrepareForWiring(Table t)
		{
			t.Set("class", DynValue.NewString(base.GetType().FullName));
			t.Set("type", DynValue.NewString(this.ValueTypeDefaultCtor.FullName));
			t.Set("name", DynValue.NewString(this.Name));
		}
	}
}
