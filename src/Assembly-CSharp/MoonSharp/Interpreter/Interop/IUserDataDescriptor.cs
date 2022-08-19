using System;

namespace MoonSharp.Interpreter.Interop
{
	// Token: 0x02000D0A RID: 3338
	public interface IUserDataDescriptor
	{
		// Token: 0x1700070A RID: 1802
		// (get) Token: 0x06005D75 RID: 23925
		string Name { get; }

		// Token: 0x1700070B RID: 1803
		// (get) Token: 0x06005D76 RID: 23926
		Type Type { get; }

		// Token: 0x06005D77 RID: 23927
		DynValue Index(Script script, object obj, DynValue index, bool isDirectIndexing);

		// Token: 0x06005D78 RID: 23928
		bool SetIndex(Script script, object obj, DynValue index, DynValue value, bool isDirectIndexing);

		// Token: 0x06005D79 RID: 23929
		string AsString(object obj);

		// Token: 0x06005D7A RID: 23930
		DynValue MetaIndex(Script script, object obj, string metaname);

		// Token: 0x06005D7B RID: 23931
		bool IsTypeCompatible(Type type, object obj);
	}
}
