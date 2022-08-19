using System;

namespace MoonSharp.Interpreter.Interop
{
	// Token: 0x02000D08 RID: 3336
	public interface IGeneratorUserDataDescriptor : IUserDataDescriptor
	{
		// Token: 0x06005D71 RID: 23921
		IUserDataDescriptor Generate(Type type);
	}
}
