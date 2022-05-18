using System;
using System.Collections.Generic;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Loaders;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020013AA RID: 5034
	public class LuaScriptLoader : ScriptLoaderBase
	{
		// Token: 0x060079F0 RID: 31216 RVA: 0x00010DC9 File Offset: 0x0000EFC9
		protected override string ResolveModuleName(string modname, string[] paths)
		{
			return modname;
		}

		// Token: 0x060079F1 RID: 31217 RVA: 0x00053304 File Offset: 0x00051504
		public LuaScriptLoader(IEnumerable<TextAsset> luaScripts)
		{
			this.luaScripts = luaScripts;
		}

		// Token: 0x060079F2 RID: 31218 RVA: 0x002B9734 File Offset: 0x002B7934
		public override object LoadFile(string file, Table globalContext)
		{
			foreach (TextAsset textAsset in this.luaScripts)
			{
				if (string.Compare(textAsset.name, file, true) == 0)
				{
					return textAsset.text;
				}
			}
			return "";
		}

		// Token: 0x060079F3 RID: 31219 RVA: 0x002B979C File Offset: 0x002B799C
		public override bool ScriptFileExists(string name)
		{
			using (IEnumerator<TextAsset> enumerator = this.luaScripts.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (string.Compare(enumerator.Current.name, name, true) == 0)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0400696C RID: 26988
		protected IEnumerable<TextAsset> luaScripts;
	}
}
