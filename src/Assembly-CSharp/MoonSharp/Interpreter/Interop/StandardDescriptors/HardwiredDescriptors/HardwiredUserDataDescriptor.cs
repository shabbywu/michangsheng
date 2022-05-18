using System;
using MoonSharp.Interpreter.Interop.BasicDescriptors;

namespace MoonSharp.Interpreter.Interop.StandardDescriptors.HardwiredDescriptors
{
	// Token: 0x02001137 RID: 4407
	public abstract class HardwiredUserDataDescriptor : DispatchingUserDataDescriptor
	{
		// Token: 0x06006A9E RID: 27294 RVA: 0x00048B28 File Offset: 0x00046D28
		protected HardwiredUserDataDescriptor(Type T) : base(T, "::hardwired::" + T.Name)
		{
		}
	}
}
