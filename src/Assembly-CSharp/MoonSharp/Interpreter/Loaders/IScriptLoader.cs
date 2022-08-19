using System;

namespace MoonSharp.Interpreter.Loaders
{
	// Token: 0x02000CFE RID: 3326
	public interface IScriptLoader
	{
		// Token: 0x06005D1A RID: 23834
		object LoadFile(string file, Table globalContext);

		// Token: 0x06005D1B RID: 23835
		[Obsolete("This serves almost no purpose. Kept here just to preserve backward compatibility.")]
		string ResolveFileName(string filename, Table globalContext);

		// Token: 0x06005D1C RID: 23836
		string ResolveModuleName(string modname, Table globalContext);
	}
}
