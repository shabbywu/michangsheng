using System;
using MoonSharp.Interpreter.Compatibility;
using MoonSharp.Interpreter.Interop.BasicDescriptors;
using MoonSharp.Interpreter.Interop.Converters;

namespace MoonSharp.Interpreter.Interop
{
	// Token: 0x02001122 RID: 4386
	public class ValueTypeDefaultCtorMemberDescriptor : IOverloadableMemberDescriptor, IMemberDescriptor, IWireableDescriptor
	{
		// Token: 0x170009A1 RID: 2465
		// (get) Token: 0x060069F3 RID: 27123 RVA: 0x0000A093 File Offset: 0x00008293
		public bool IsStatic
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170009A2 RID: 2466
		// (get) Token: 0x060069F4 RID: 27124 RVA: 0x00048559 File Offset: 0x00046759
		// (set) Token: 0x060069F5 RID: 27125 RVA: 0x00048561 File Offset: 0x00046761
		public string Name { get; private set; }

		// Token: 0x170009A3 RID: 2467
		// (get) Token: 0x060069F6 RID: 27126 RVA: 0x0004856A File Offset: 0x0004676A
		// (set) Token: 0x060069F7 RID: 27127 RVA: 0x00048572 File Offset: 0x00046772
		public Type ValueTypeDefaultCtor { get; private set; }

		// Token: 0x170009A4 RID: 2468
		// (get) Token: 0x060069F8 RID: 27128 RVA: 0x0004857B File Offset: 0x0004677B
		// (set) Token: 0x060069F9 RID: 27129 RVA: 0x00048583 File Offset: 0x00046783
		public ParameterDescriptor[] Parameters { get; private set; }

		// Token: 0x170009A5 RID: 2469
		// (get) Token: 0x060069FA RID: 27130 RVA: 0x0000B171 File Offset: 0x00009371
		public Type ExtensionMethodType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170009A6 RID: 2470
		// (get) Token: 0x060069FB RID: 27131 RVA: 0x0000B171 File Offset: 0x00009371
		public Type VarArgsArrayType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170009A7 RID: 2471
		// (get) Token: 0x060069FC RID: 27132 RVA: 0x0000B171 File Offset: 0x00009371
		public Type VarArgsElementType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060069FD RID: 27133 RVA: 0x0004858C File Offset: 0x0004678C
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

		// Token: 0x060069FE RID: 27134 RVA: 0x0028F350 File Offset: 0x0028D550
		public DynValue Execute(Script script, object obj, ScriptExecutionContext context, CallbackArguments args)
		{
			this.CheckAccess(MemberDescriptorAccess.CanRead, obj);
			object obj2 = Activator.CreateInstance(this.ValueTypeDefaultCtor);
			return ClrToScriptConversions.ObjectToDynValue(script, obj2);
		}

		// Token: 0x170009A8 RID: 2472
		// (get) Token: 0x060069FF RID: 27135 RVA: 0x000485CA File Offset: 0x000467CA
		public string SortDiscriminant
		{
			get
			{
				return "@.ctor";
			}
		}

		// Token: 0x170009A9 RID: 2473
		// (get) Token: 0x06006A00 RID: 27136 RVA: 0x0002D0EC File Offset: 0x0002B2EC
		public MemberDescriptorAccess MemberAccess
		{
			get
			{
				return MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanExecute;
			}
		}

		// Token: 0x06006A01 RID: 27137 RVA: 0x0028F350 File Offset: 0x0028D550
		public DynValue GetValue(Script script, object obj)
		{
			this.CheckAccess(MemberDescriptorAccess.CanRead, obj);
			object obj2 = Activator.CreateInstance(this.ValueTypeDefaultCtor);
			return ClrToScriptConversions.ObjectToDynValue(script, obj2);
		}

		// Token: 0x06006A02 RID: 27138 RVA: 0x00048074 File Offset: 0x00046274
		public void SetValue(Script script, object obj, DynValue value)
		{
			this.CheckAccess(MemberDescriptorAccess.CanWrite, obj);
		}

		// Token: 0x06006A03 RID: 27139 RVA: 0x0028F378 File Offset: 0x0028D578
		public void PrepareForWiring(Table t)
		{
			t.Set("class", DynValue.NewString(base.GetType().FullName));
			t.Set("type", DynValue.NewString(this.ValueTypeDefaultCtor.FullName));
			t.Set("name", DynValue.NewString(this.Name));
		}
	}
}
