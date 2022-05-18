using System;
using System.Linq;

namespace MoonSharp.Interpreter.Loaders
{
	// Token: 0x020010DD RID: 4317
	public abstract class ScriptLoaderBase : IScriptLoader
	{
		// Token: 0x06006837 RID: 26679
		public abstract bool ScriptFileExists(string name);

		// Token: 0x06006838 RID: 26680
		public abstract object LoadFile(string file, Table globalContext);

		// Token: 0x06006839 RID: 26681 RVA: 0x0028B078 File Offset: 0x00289278
		protected virtual string ResolveModuleName(string modname, string[] paths)
		{
			if (paths == null)
			{
				return null;
			}
			modname = modname.Replace('.', '/');
			for (int i = 0; i < paths.Length; i++)
			{
				string text = paths[i].Replace("?", modname);
				if (this.ScriptFileExists(text))
				{
					return text;
				}
			}
			return null;
		}

		// Token: 0x0600683A RID: 26682 RVA: 0x0028B0C4 File Offset: 0x002892C4
		public virtual string ResolveModuleName(string modname, Table globalContext)
		{
			if (!this.IgnoreLuaPathGlobal)
			{
				DynValue dynValue = globalContext.RawGet("LUA_PATH");
				if (dynValue != null && dynValue.Type == DataType.String)
				{
					return this.ResolveModuleName(modname, ScriptLoaderBase.UnpackStringPaths(dynValue.String));
				}
			}
			return this.ResolveModuleName(modname, this.ModulePaths);
		}

		// Token: 0x17000957 RID: 2391
		// (get) Token: 0x0600683B RID: 26683 RVA: 0x00047897 File Offset: 0x00045A97
		// (set) Token: 0x0600683C RID: 26684 RVA: 0x0004789F File Offset: 0x00045A9F
		public string[] ModulePaths { get; set; }

		// Token: 0x0600683D RID: 26685 RVA: 0x0028B114 File Offset: 0x00289314
		public static string[] UnpackStringPaths(string str)
		{
			return (from s in str.Split(new char[]
			{
				';'
			}, StringSplitOptions.RemoveEmptyEntries)
			select s.Trim() into s
			where !string.IsNullOrEmpty(s)
			select s).ToArray<string>();
		}

		// Token: 0x0600683E RID: 26686 RVA: 0x0028B180 File Offset: 0x00289380
		public static string[] GetDefaultEnvironmentPaths()
		{
			string[] array = null;
			if (array == null)
			{
				string environmentVariable = Script.GlobalOptions.Platform.GetEnvironmentVariable("MOONSHARP_PATH");
				if (!string.IsNullOrEmpty(environmentVariable))
				{
					array = ScriptLoaderBase.UnpackStringPaths(environmentVariable);
				}
				if (array == null)
				{
					environmentVariable = Script.GlobalOptions.Platform.GetEnvironmentVariable("LUA_PATH");
					if (!string.IsNullOrEmpty(environmentVariable))
					{
						array = ScriptLoaderBase.UnpackStringPaths(environmentVariable);
					}
				}
				if (array == null)
				{
					array = ScriptLoaderBase.UnpackStringPaths("?;?.lua");
				}
			}
			return array;
		}

		// Token: 0x0600683F RID: 26687 RVA: 0x00010DC9 File Offset: 0x0000EFC9
		public virtual string ResolveFileName(string filename, Table globalContext)
		{
			return filename;
		}

		// Token: 0x17000958 RID: 2392
		// (get) Token: 0x06006840 RID: 26688 RVA: 0x000478A8 File Offset: 0x00045AA8
		// (set) Token: 0x06006841 RID: 26689 RVA: 0x000478B0 File Offset: 0x00045AB0
		public bool IgnoreLuaPathGlobal { get; set; }
	}
}
