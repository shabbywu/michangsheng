using System;
using MoonSharp.Interpreter.Interop.BasicDescriptors;

namespace MoonSharp.Interpreter.Interop
{
	// Token: 0x02000D1A RID: 3354
	public class DynValueMemberDescriptor : IMemberDescriptor, IWireableDescriptor
	{
		// Token: 0x06005DC3 RID: 24003 RVA: 0x00263EE8 File Offset: 0x002620E8
		protected DynValueMemberDescriptor(string name, string serializedTableValue)
		{
			DynValue dynValue = new Script().CreateDynamicExpression(serializedTableValue).Evaluate(null);
			this.m_Value = dynValue.Table.Get(1);
			this.Name = name;
			this.MemberAccess = MemberDescriptorAccess.CanRead;
		}

		// Token: 0x06005DC4 RID: 24004 RVA: 0x00263F2D File Offset: 0x0026212D
		protected DynValueMemberDescriptor(string name)
		{
			this.MemberAccess = MemberDescriptorAccess.CanRead;
			this.m_Value = null;
			this.Name = name;
		}

		// Token: 0x06005DC5 RID: 24005 RVA: 0x00263F4A File Offset: 0x0026214A
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

		// Token: 0x1700071A RID: 1818
		// (get) Token: 0x06005DC6 RID: 24006 RVA: 0x00024C5F File Offset: 0x00022E5F
		public bool IsStatic
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700071B RID: 1819
		// (get) Token: 0x06005DC7 RID: 24007 RVA: 0x00263F79 File Offset: 0x00262179
		// (set) Token: 0x06005DC8 RID: 24008 RVA: 0x00263F81 File Offset: 0x00262181
		public string Name { get; private set; }

		// Token: 0x1700071C RID: 1820
		// (get) Token: 0x06005DC9 RID: 24009 RVA: 0x00263F8A File Offset: 0x0026218A
		// (set) Token: 0x06005DCA RID: 24010 RVA: 0x00263F92 File Offset: 0x00262192
		public MemberDescriptorAccess MemberAccess { get; private set; }

		// Token: 0x1700071D RID: 1821
		// (get) Token: 0x06005DCB RID: 24011 RVA: 0x00263F9B File Offset: 0x0026219B
		public virtual DynValue Value
		{
			get
			{
				return this.m_Value;
			}
		}

		// Token: 0x06005DCC RID: 24012 RVA: 0x00263FA3 File Offset: 0x002621A3
		public DynValue GetValue(Script script, object obj)
		{
			return this.Value;
		}

		// Token: 0x06005DCD RID: 24013 RVA: 0x00263FAB File Offset: 0x002621AB
		public void SetValue(Script script, object obj, DynValue value)
		{
			throw new ScriptRuntimeException("userdata '{0}' cannot be written to.", new object[]
			{
				this.Name
			});
		}

		// Token: 0x06005DCE RID: 24014 RVA: 0x00263FC8 File Offset: 0x002621C8
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

		// Token: 0x0400540B RID: 21515
		private DynValue m_Value;
	}
}
