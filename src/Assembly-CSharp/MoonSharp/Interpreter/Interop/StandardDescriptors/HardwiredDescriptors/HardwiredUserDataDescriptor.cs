using System;
using MoonSharp.Interpreter.Interop.BasicDescriptors;

namespace MoonSharp.Interpreter.Interop.StandardDescriptors.HardwiredDescriptors
{
	// Token: 0x02000D2F RID: 3375
	public abstract class HardwiredUserDataDescriptor : DispatchingUserDataDescriptor
	{
		// Token: 0x06005ECC RID: 24268 RVA: 0x00268B11 File Offset: 0x00266D11
		protected HardwiredUserDataDescriptor(Type T) : base(T, "::hardwired::" + T.Name)
		{
		}
	}
}
