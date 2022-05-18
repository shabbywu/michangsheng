using System;

namespace MoonSharp.Interpreter.Interop
{
	// Token: 0x020010EC RID: 4332
	public interface IGeneratorUserDataDescriptor : IUserDataDescriptor
	{
		// Token: 0x060068A0 RID: 26784
		IUserDataDescriptor Generate(Type type);
	}
}
