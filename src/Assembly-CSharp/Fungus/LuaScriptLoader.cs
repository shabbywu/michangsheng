using System;
using System.Collections.Generic;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Loaders;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F05 RID: 3845
	public class LuaScriptLoader : ScriptLoaderBase
	{
		// Token: 0x06006C3C RID: 27708 RVA: 0x001086F1 File Offset: 0x001068F1
		protected override string ResolveModuleName(string modname, string[] paths)
		{
			return modname;
		}

		// Token: 0x06006C3D RID: 27709 RVA: 0x002985D9 File Offset: 0x002967D9
		public LuaScriptLoader(IEnumerable<TextAsset> luaScripts)
		{
			this.luaScripts = luaScripts;
		}

		// Token: 0x06006C3E RID: 27710 RVA: 0x002985E8 File Offset: 0x002967E8
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

		// Token: 0x06006C3F RID: 27711 RVA: 0x00298650 File Offset: 0x00296850
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

		// Token: 0x04005AF3 RID: 23283
		protected IEnumerable<TextAsset> luaScripts;
	}
}
