using System;

namespace MoonSharp.Interpreter.Interop
{
	// Token: 0x02000D0C RID: 3340
	public interface IUserDataType
	{
		// Token: 0x06005D83 RID: 23939
		DynValue Index(Script script, DynValue index, bool isDirectIndexing);

		// Token: 0x06005D84 RID: 23940
		bool SetIndex(Script script, DynValue index, DynValue value, bool isDirectIndexing);

		// Token: 0x06005D85 RID: 23941
		DynValue MetaIndex(Script script, string metaname);
	}
}
