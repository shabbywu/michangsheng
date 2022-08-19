using System;
using MoonSharp.Interpreter.Interop.BasicDescriptors;
using MoonSharp.Interpreter.Interop.Converters;

namespace MoonSharp.Interpreter.Interop.StandardDescriptors.HardwiredDescriptors
{
	// Token: 0x02000D2D RID: 3373
	public abstract class HardwiredMemberDescriptor : IMemberDescriptor
	{
		// Token: 0x17000758 RID: 1880
		// (get) Token: 0x06005EBB RID: 24251 RVA: 0x0026899C File Offset: 0x00266B9C
		// (set) Token: 0x06005EBC RID: 24252 RVA: 0x002689A4 File Offset: 0x00266BA4
		public Type MemberType { get; private set; }

		// Token: 0x06005EBD RID: 24253 RVA: 0x002689AD File Offset: 0x00266BAD
		protected HardwiredMemberDescriptor(Type memberType, string name, bool isStatic, MemberDescriptorAccess access)
		{
			this.IsStatic = isStatic;
			this.Name = name;
			this.MemberAccess = access;
			this.MemberType = memberType;
		}

		// Token: 0x17000759 RID: 1881
		// (get) Token: 0x06005EBE RID: 24254 RVA: 0x002689D2 File Offset: 0x00266BD2
		// (set) Token: 0x06005EBF RID: 24255 RVA: 0x002689DA File Offset: 0x00266BDA
		public bool IsStatic { get; private set; }

		// Token: 0x1700075A RID: 1882
		// (get) Token: 0x06005EC0 RID: 24256 RVA: 0x002689E3 File Offset: 0x00266BE3
		// (set) Token: 0x06005EC1 RID: 24257 RVA: 0x002689EB File Offset: 0x00266BEB
		public string Name { get; private set; }

		// Token: 0x1700075B RID: 1883
		// (get) Token: 0x06005EC2 RID: 24258 RVA: 0x002689F4 File Offset: 0x00266BF4
		// (set) Token: 0x06005EC3 RID: 24259 RVA: 0x002689FC File Offset: 0x00266BFC
		public MemberDescriptorAccess MemberAccess { get; private set; }

		// Token: 0x06005EC4 RID: 24260 RVA: 0x00268A08 File Offset: 0x00266C08
		public DynValue GetValue(Script script, object obj)
		{
			this.CheckAccess(MemberDescriptorAccess.CanRead, obj);
			object valueImpl = this.GetValueImpl(script, obj);
			return ClrToScriptConversions.ObjectToDynValue(script, valueImpl);
		}

		// Token: 0x06005EC5 RID: 24261 RVA: 0x00268A30 File Offset: 0x00266C30
		public void SetValue(Script script, object obj, DynValue value)
		{
			this.CheckAccess(MemberDescriptorAccess.CanWrite, obj);
			object value2 = ScriptToClrConversions.DynValueToObjectOfType(value, this.MemberType, null, false);
			this.SetValueImpl(script, obj, value2);
		}

		// Token: 0x06005EC6 RID: 24262 RVA: 0x00268A5D File Offset: 0x00266C5D
		protected virtual object GetValueImpl(Script script, object obj)
		{
			throw new InvalidOperationException("GetValue on write-only hardwired descriptor " + this.Name);
		}

		// Token: 0x06005EC7 RID: 24263 RVA: 0x00268A74 File Offset: 0x00266C74
		protected virtual void SetValueImpl(Script script, object obj, object value)
		{
			throw new InvalidOperationException("SetValue on read-only hardwired descriptor " + this.Name);
		}
	}
}
