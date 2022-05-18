using System;
using MoonSharp.Interpreter;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020013A2 RID: 5026
	public class LuaScript : MonoBehaviour
	{
		// Token: 0x17000B7E RID: 2942
		// (set) Token: 0x060079B3 RID: 31155 RVA: 0x000530F5 File Offset: 0x000512F5
		public bool Initialised
		{
			set
			{
				this.initialised = value;
			}
		}

		// Token: 0x060079B4 RID: 31156 RVA: 0x000530FE File Offset: 0x000512FE
		protected static string GetPath(Transform current)
		{
			if (current.parent == null)
			{
				return current.name;
			}
			return LuaScript.GetPath(current.parent) + "." + current.name;
		}

		// Token: 0x060079B5 RID: 31157 RVA: 0x00053130 File Offset: 0x00051330
		protected virtual void Start()
		{
			this.InitLuaScript();
		}

		// Token: 0x060079B6 RID: 31158 RVA: 0x002B8D14 File Offset: 0x002B6F14
		protected virtual void InitLuaScript()
		{
			if (this.initialised)
			{
				return;
			}
			if (this.luaEnvironment == null)
			{
				this.luaEnvironment = LuaEnvironment.GetLua();
			}
			if (this.luaEnvironment == null)
			{
				Debug.LogError("No Lua Environment found");
				return;
			}
			this.luaEnvironment.InitEnvironment();
			this.friendlyName = LuaScript.GetPath(base.transform) + ".LuaScript";
			string luaString = this.GetLuaString();
			this.luaFunction = this.luaEnvironment.LoadLuaFunction(luaString, this.friendlyName);
			this.initialised = true;
		}

		// Token: 0x060079B7 RID: 31159 RVA: 0x002B8DA8 File Offset: 0x002B6FA8
		protected virtual string GetLuaString()
		{
			string text = "";
			if (this.luaFile != null)
			{
				text = this.luaFile.text;
			}
			if (this.luaScript.Length > 0)
			{
				text += this.luaScript;
			}
			return text;
		}

		// Token: 0x060079B8 RID: 31160 RVA: 0x00053138 File Offset: 0x00051338
		public virtual void OnExecute()
		{
			this.InitLuaScript();
			if (this.luaEnvironment == null)
			{
				Debug.LogWarning("No Lua Environment found");
				return;
			}
			this.luaEnvironment.RunLuaFunction(this.luaFunction, this.runAsCoroutine, null);
		}

		// Token: 0x04006955 RID: 26965
		[Tooltip("The Lua Environment to use when executing Lua script.")]
		[SerializeField]
		protected LuaEnvironment luaEnvironment;

		// Token: 0x04006956 RID: 26966
		[Tooltip("Text file containing Lua script to be executed.")]
		[SerializeField]
		protected TextAsset luaFile;

		// Token: 0x04006957 RID: 26967
		[Tooltip("A Lua string to execute, appended to the contents of Lua File (if one is specified).")]
		[TextArea(5, 50)]
		[SerializeField]
		protected string luaScript = "";

		// Token: 0x04006958 RID: 26968
		[Tooltip("Run the script as a Lua coroutine so execution can be yielded for asynchronous operations.")]
		[SerializeField]
		protected bool runAsCoroutine = true;

		// Token: 0x04006959 RID: 26969
		protected string friendlyName = "";

		// Token: 0x0400695A RID: 26970
		protected bool initialised;

		// Token: 0x0400695B RID: 26971
		protected Closure luaFunction;
	}
}
