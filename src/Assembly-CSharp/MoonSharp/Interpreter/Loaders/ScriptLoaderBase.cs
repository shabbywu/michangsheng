using System;
using System.Linq;

namespace MoonSharp.Interpreter.Loaders
{
	// Token: 0x02000CFF RID: 3327
	public abstract class ScriptLoaderBase : IScriptLoader
	{
		// Token: 0x06005D1D RID: 23837
		public abstract bool ScriptFileExists(string name);

		// Token: 0x06005D1E RID: 23838
		public abstract object LoadFile(string file, Table globalContext);

		// Token: 0x06005D1F RID: 23839 RVA: 0x002620BC File Offset: 0x002602BC
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

		// Token: 0x06005D20 RID: 23840 RVA: 0x00262108 File Offset: 0x00260308
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

		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x06005D21 RID: 23841 RVA: 0x00262155 File Offset: 0x00260355
		// (set) Token: 0x06005D22 RID: 23842 RVA: 0x0026215D File Offset: 0x0026035D
		public string[] ModulePaths { get; set; }

		// Token: 0x06005D23 RID: 23843 RVA: 0x00262168 File Offset: 0x00260368
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

		// Token: 0x06005D24 RID: 23844 RVA: 0x002621D4 File Offset: 0x002603D4
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

		// Token: 0x06005D25 RID: 23845 RVA: 0x001086F1 File Offset: 0x001068F1
		public virtual string ResolveFileName(string filename, Table globalContext)
		{
			return filename;
		}

		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x06005D26 RID: 23846 RVA: 0x00262240 File Offset: 0x00260440
		// (set) Token: 0x06005D27 RID: 23847 RVA: 0x00262248 File Offset: 0x00260448
		public bool IgnoreLuaPathGlobal { get; set; }
	}
}
