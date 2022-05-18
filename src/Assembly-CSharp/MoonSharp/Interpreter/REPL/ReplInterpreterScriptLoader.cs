using System;
using MoonSharp.Interpreter.Loaders;

namespace MoonSharp.Interpreter.REPL
{
	// Token: 0x020010D0 RID: 4304
	public class ReplInterpreterScriptLoader : FileSystemScriptLoader
	{
		// Token: 0x060067D5 RID: 26581 RVA: 0x0028ACAC File Offset: 0x00288EAC
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

		// Token: 0x060067D6 RID: 26582 RVA: 0x0028AD44 File Offset: 0x00288F44
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
