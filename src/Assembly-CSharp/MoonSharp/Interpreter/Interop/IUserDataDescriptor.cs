using System;

namespace MoonSharp.Interpreter.Interop
{
	// Token: 0x020010EE RID: 4334
	public interface IUserDataDescriptor
	{
		// Token: 0x17000967 RID: 2407
		// (get) Token: 0x060068A4 RID: 26788
		string Name { get; }

		// Token: 0x17000968 RID: 2408
		// (get) Token: 0x060068A5 RID: 26789
		Type Type { get; }

		// Token: 0x060068A6 RID: 26790
		DynValue Index(Script script, object obj, DynValue index, bool isDirectIndexing);

		// Token: 0x060068A7 RID: 26791
		bool SetIndex(Script script, object obj, DynValue index, DynValue value, bool isDirectIndexing);

		// Token: 0x060068A8 RID: 26792
		string AsString(object obj);

		// Token: 0x060068A9 RID: 26793
		DynValue MetaIndex(Script script, object obj, string metaname);

		// Token: 0x060068AA RID: 26794
		bool IsTypeCompatible(Type type, object obj);
	}
}
