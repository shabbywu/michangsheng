using System;
using MoonSharp.Interpreter;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000EFD RID: 3837
	public class LuaScript : MonoBehaviour
	{
		// Token: 0x170008DB RID: 2267
		// (set) Token: 0x06006BFF RID: 27647 RVA: 0x002979A6 File Offset: 0x00295BA6
		public bool Initialised
		{
			set
			{
				this.initialised = value;
			}
		}

		// Token: 0x06006C00 RID: 27648 RVA: 0x002979AF File Offset: 0x00295BAF
		protected static string GetPath(Transform current)
		{
			if (current.parent == null)
			{
				return current.name;
			}
			return LuaScript.GetPath(current.parent) + "." + current.name;
		}

		// Token: 0x06006C01 RID: 27649 RVA: 0x002979E1 File Offset: 0x00295BE1
		protected virtual void Start()
		{
			this.InitLuaScript();
		}

		// Token: 0x06006C02 RID: 27650 RVA: 0x002979EC File Offset: 0x00295BEC
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

		// Token: 0x06006C03 RID: 27651 RVA: 0x00297A80 File Offset: 0x00295C80
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

		// Token: 0x06006C04 RID: 27652 RVA: 0x00297AC9 File Offset: 0x00295CC9
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

		// Token: 0x04005ADC RID: 23260
		[Tooltip("The Lua Environment to use when executing Lua script.")]
		[SerializeField]
		protected LuaEnvironment luaEnvironment;

		// Token: 0x04005ADD RID: 23261
		[Tooltip("Text file containing Lua script to be executed.")]
		[SerializeField]
		protected TextAsset luaFile;

		// Token: 0x04005ADE RID: 23262
		[Tooltip("A Lua string to execute, appended to the contents of Lua File (if one is specified).")]
		[TextArea(5, 50)]
		[SerializeField]
		protected string luaScript = "";

		// Token: 0x04005ADF RID: 23263
		[Tooltip("Run the script as a Lua coroutine so execution can be yielded for asynchronous operations.")]
		[SerializeField]
		protected bool runAsCoroutine = true;

		// Token: 0x04005AE0 RID: 23264
		protected string friendlyName = "";

		// Token: 0x04005AE1 RID: 23265
		protected bool initialised;

		// Token: 0x04005AE2 RID: 23266
		protected Closure luaFunction;
	}
}
