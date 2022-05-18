using System;

namespace MoonSharp.Interpreter.Interop
{
	// Token: 0x020010F0 RID: 4336
	public interface IUserDataType
	{
		// Token: 0x060068B2 RID: 26802
		DynValue Index(Script script, DynValue index, bool isDirectIndexing);

		// Token: 0x060068B3 RID: 26803
		bool SetIndex(Script script, DynValue index, DynValue value, bool isDirectIndexing);

		// Token: 0x060068B4 RID: 26804
		DynValue MetaIndex(Script script, string metaname);
	}
}
