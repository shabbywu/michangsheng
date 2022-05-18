using System;
using MoonSharp.Interpreter.Interop.BasicDescriptors;
using MoonSharp.Interpreter.Interop.Converters;

namespace MoonSharp.Interpreter.Interop.StandardDescriptors.HardwiredDescriptors
{
	// Token: 0x02001135 RID: 4405
	public abstract class HardwiredMemberDescriptor : IMemberDescriptor
	{
		// Token: 0x170009B5 RID: 2485
		// (get) Token: 0x06006A8D RID: 27277 RVA: 0x00048A89 File Offset: 0x00046C89
		// (set) Token: 0x06006A8E RID: 27278 RVA: 0x00048A91 File Offset: 0x00046C91
		public Type MemberType { get; private set; }

		// Token: 0x06006A8F RID: 27279 RVA: 0x00048A9A File Offset: 0x00046C9A
		protected HardwiredMemberDescriptor(Type memberType, string name, bool isStatic, MemberDescriptorAccess access)
		{
			this.IsStatic = isStatic;
			this.Name = name;
			this.MemberAccess = access;
			this.MemberType = memberType;
		}

		// Token: 0x170009B6 RID: 2486
		// (get) Token: 0x06006A90 RID: 27280 RVA: 0x00048ABF File Offset: 0x00046CBF
		// (set) Token: 0x06006A91 RID: 27281 RVA: 0x00048AC7 File Offset: 0x00046CC7
		public bool IsStatic { get; private set; }

		// Token: 0x170009B7 RID: 2487
		// (get) Token: 0x06006A92 RID: 27282 RVA: 0x00048AD0 File Offset: 0x00046CD0
		// (set) Token: 0x06006A93 RID: 27283 RVA: 0x00048AD8 File Offset: 0x00046CD8
		public string Name { get; private set; }

		// Token: 0x170009B8 RID: 2488
		// (get) Token: 0x06006A94 RID: 27284 RVA: 0x00048AE1 File Offset: 0x00046CE1
		// (set) Token: 0x06006A95 RID: 27285 RVA: 0x00048AE9 File Offset: 0x00046CE9
		public MemberDescriptorAccess MemberAccess { get; private set; }

		// Token: 0x06006A96 RID: 27286 RVA: 0x00290F7C File Offset: 0x0028F17C
		public DynValue GetValue(Script script, object obj)
		{
			this.CheckAccess(MemberDescriptorAccess.CanRead, obj);
			object valueImpl = this.GetValueImpl(script, obj);
			return ClrToScriptConversions.ObjectToDynValue(script, valueImpl);
		}

		// Token: 0x06006A97 RID: 27287 RVA: 0x00290FA4 File Offset: 0x0028F1A4
		public void SetValue(Script script, object obj, DynValue value)
		{
			this.CheckAccess(MemberDescriptorAccess.CanWrite, obj);
			object value2 = ScriptToClrConversions.DynValueToObjectOfType(value, this.MemberType, null, false);
			this.SetValueImpl(script, obj, value2);
		}

		// Token: 0x06006A98 RID: 27288 RVA: 0x00048AF2 File Offset: 0x00046CF2
		protected virtual object GetValueImpl(Script script, object obj)
		{
			throw new InvalidOperationException("GetValue on write-only hardwired descriptor " + this.Name);
		}

		// Token: 0x06006A99 RID: 27289 RVA: 0x00048B09 File Offset: 0x00046D09
		protected virtual void SetValueImpl(Script script, object obj, object value)
		{
			throw new InvalidOperationException("SetValue on read-only hardwired descriptor " + this.Name);
		}
	}
}
