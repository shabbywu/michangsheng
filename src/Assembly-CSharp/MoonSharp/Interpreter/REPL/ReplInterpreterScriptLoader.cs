using System;
using MoonSharp.Interpreter.Loaders;

namespace MoonSharp.Interpreter.REPL
{
	// Token: 0x02000CF3 RID: 3315
	public class ReplInterpreterScriptLoader : FileSystemScriptLoader
	{
		// Token: 0x06005CBF RID: 23743 RVA: 0x00261AD8 File Offset: 0x0025FCD8
		public ReplInterpreterScriptLoader()
		{
			string environmentVariable = Environment.GetEnvironmentVariable("MOONSHARP_PATH");
			if (!string.IsNullOrEmpty(environmentVariable))
			{
				base.ModulePaths = ScriptLoaderBase.UnpackStringPaths(environmentVariable);
			}
			if (base.ModulePaths == null)
			{
				environmentVariable = Environment.GetEnvironmentVariable("LUA_PATH_5_2");
				if (!string.IsNullOrEmpty(environmentVariable))
				{
					base.ModulePaths = ScriptLoaderBase.UnpackStringPaths(environmentVariable);
				}
			}
			if (base.ModulePaths == null)
			{
				environmentVariable = Environment.GetEnvironmentVariable("LUA_PATH");
				if (!string.IsNullOrEmpty(environmentVariable))
				{
					base.ModulePaths = ScriptLoaderBase.UnpackStringPaths(environmentVariable);
				}
			}
			if (base.ModulePaths == null)
			{
				base.ModulePaths = ScriptLoaderBase.UnpackStringPaths("?;?.lua");
			}
		}

		// Token: 0x06005CC0 RID: 23744 RVA: 0x00261B70 File Offset: 0x0025FD70
		public override string ResolveModuleName(string modname, Table globalContext)
		{
			DynValue dynValue = globalContext.RawGet("LUA_PATH");
			if (dynValue != null && dynValue.Type == DataType.String)
			{
				return this.ResolveModuleName(modname, ScriptLoaderBase.UnpackStringPaths(dynValue.String));
			}
			return base.ResolveModuleName(modname, globalContext);
		}
	}
}
