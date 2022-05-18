using System;

namespace MoonSharp.Interpreter.Loaders
{
	// Token: 0x020010DC RID: 4316
	public interface IScriptLoader
	{
		// Token: 0x06006834 RID: 26676
		object LoadFile(string file, Table globalContext);

		// Token: 0x06006835 RID: 26677
		[Obsolete("This serves almost no purpose. Kept here just to preserve backward compatibility.")]
		string ResolveFileName(string filename, Table globalContext);

		// Token: 0x06006836 RID: 26678
		string ResolveModuleName(string modname, Table globalContext);
	}
}
