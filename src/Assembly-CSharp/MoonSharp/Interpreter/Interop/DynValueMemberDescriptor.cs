using System;
using MoonSharp.Interpreter.Interop.BasicDescriptors;

namespace MoonSharp.Interpreter.Interop
{
	// Token: 0x020010FE RID: 4350
	public class DynValueMemberDescriptor : IMemberDescriptor, IWireableDescriptor
	{
		// Token: 0x060068F2 RID: 26866 RVA: 0x0028CA04 File Offset: 0x0028AC04
		protected DynValueMemberDescriptor(string name, string serializedTableValue)
		{
			DynValue dynValue = new Script().CreateDynamicExpression(serializedTableValue).Evaluate(null);
			this.m_Value = dynValue.Table.Get(1);
			this.Name = name;
			this.MemberAccess = MemberDescriptorAccess.CanRead;
		}

		// Token: 0x060068F3 RID: 26867 RVA: 0x00047EF7 File Offset: 0x000460F7
		protected DynValueMemberDescriptor(string name)
		{
			this.MemberAccess = MemberDescriptorAccess.CanRead;
			this.m_Value = null;
			this.Name = name;
		}

		// Token: 0x060068F4 RID: 26868 RVA: 0x00047F14 File Offset: 0x00046114
		public DynValueMemberDescriptor(string name, DynValue value)
		{
			this.m_Value = value;
			this.Name = name;
			if (value.Type == DataType.ClrFunction)
			{
				this.MemberAccess = (MemberDescriptorAccess.CanRead | MemberDescriptorAccess.CanExecute);
				return;
			}
			this.MemberAccess = MemberDescriptorAccess.CanRead;
		}

		// Token: 0x17000977 RID: 2423
		// (get) Token: 0x060068F5 RID: 26869 RVA: 0x0000A093 File Offset: 0x00008293
		public bool IsStatic
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000978 RID: 2424
		// (get) Token: 0x060068F6 RID: 26870 RVA: 0x00047F43 File Offset: 0x00046143
		// (set) Token: 0x060068F7 RID: 26871 RVA: 0x00047F4B File Offset: 0x0004614B
		public string Name { get; private set; }

		// Token: 0x17000979 RID: 2425
		// (get) Token: 0x060068F8 RID: 26872 RVA: 0x00047F54 File Offset: 0x00046154
		// (set) Token: 0x060068F9 RID: 26873 RVA: 0x00047F5C File Offset: 0x0004615C
		public MemberDescriptorAccess MemberAccess { get; private set; }

		// Token: 0x1700097A RID: 2426
		// (get) Token: 0x060068FA RID: 26874 RVA: 0x00047F65 File Offset: 0x00046165
		public virtual DynValue Value
		{
			get
			{
				return this.m_Value;
			}
		}

		// Token: 0x060068FB RID: 26875 RVA: 0x00047F6D File Offset: 0x0004616D
		public DynValue GetValue(Script script, object obj)
		{
			return this.Value;
		}

		// Token: 0x060068FC RID: 26876 RVA: 0x00047F75 File Offset: 0x00046175
		public void SetValue(Script script, object obj, DynValue value)
		{
			throw new ScriptRuntimeException("userdata '{0}' cannot be written to.", new object[]
			{
				this.Name
			});
		}

		// Token: 0x060068FD RID: 26877 RVA: 0x0028CA4C File Offset: 0x0028AC4C
		public void PrepareForWiring(Table t)
		{
			t.Set("class", DynValue.NewString(base.GetType().FullName));
			t.Set("name", DynValue.NewString(this.Name));
			switch (this.Value.Type)
			{
			case DataType.Nil:
			case DataType.Void:
			case DataType.Boolean:
			case DataType.Number:
			case DataType.String:
			case DataType.Tuple:
				t.Set("value", this.Value);
				return;
			case DataType.Table:
				if (this.Value.Table.OwnerScript == null)
				{
					t.Set("value", this.Value);
					return;
				}
				t.Set("error", DynValue.NewString("Wiring of non-prime table value members not supported."));
				return;
			case DataType.UserData:
				if (this.Value.UserData.Object == null)
				{
					t.Set("type", DynValue.NewString("userdata"));
					t.Set("staticType", DynValue.NewString(this.Value.UserData.Descriptor.Type.FullName));
					t.Set("visibility", DynValue.NewString(this.Value.UserData.Descriptor.Type.GetClrVisibility()));
					return;
				}
				t.Set("error", DynValue.NewString("Wiring of non-static userdata value members not supported."));
				return;
			}
			t.Set("error", DynValue.NewString(string.Format("Wiring of '{0}' value members not supported.", this.Value.Type.ToErrorTypeString())));
		}

		// Token: 0x04006022 RID: 24610
		private DynValue m_Value;
	}
}
