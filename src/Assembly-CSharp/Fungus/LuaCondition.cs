using System;
using MoonSharp.Interpreter;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001231 RID: 4657
	public class LuaCondition : Condition
	{
		// Token: 0x0600718D RID: 29069 RVA: 0x002A6168 File Offset: 0x002A4368
		protected override bool EvaluateCondition()
		{
			bool condition = false;
			this.luaEnvironment.RunLuaFunction(this.luaFunction, false, delegate(DynValue returnValue)
			{
				if (returnValue != null)
				{
					condition = returnValue.Boolean;
					return;
				}
				Debug.LogWarning("No return value from " + this.friendlyName);
			});
			return condition;
		}

		// Token: 0x0600718E RID: 29070 RVA: 0x0004D38C File Offset: 0x0004B58C
		protected override bool HasNeededProperties()
		{
			return !string.IsNullOrEmpty(this.luaCompareString);
		}

		// Token: 0x0600718F RID: 29071 RVA: 0x0004D39C File Offset: 0x0004B59C
		protected virtual void Start()
		{
			this.InitExecuteLua();
		}

		// Token: 0x06007190 RID: 29072 RVA: 0x0004D3A4 File Offset: 0x0004B5A4
		protected virtual string GetLuaString()
		{
			return "return not not (" + this.luaCompareString + ")";
		}

		// Token: 0x06007191 RID: 29073 RVA: 0x002A61B0 File Offset: 0x002A43B0
		protected virtual void InitExecuteLua()
		{
			if (this.initialised)
			{
				return;
			}
			this.friendlyName = string.Concat(new string[]
			{
				base.gameObject.name,
				".",
				this.ParentBlock.BlockName,
				".",
				base.GetType().ToString(),
				" #",
				this.CommandIndex.ToString()
			});
			Flowchart flowchart = this.GetFlowchart();
			if (this.luaEnvironment == null)
			{
				this.luaEnvironment = flowchart.LuaEnv;
			}
			if (this.luaEnvironment == null)
			{
				this.luaEnvironment = LuaEnvironment.GetLua();
			}
			string luaString = this.GetLuaString();
			this.luaFunction = this.luaEnvironment.LoadLuaFunction(luaString, this.friendlyName);
			if (flowchart.LuaBindingName != "")
			{
				Table globals = this.luaEnvironment.Interpreter.Globals;
				if (globals != null)
				{
					globals[flowchart.LuaBindingName] = flowchart;
				}
			}
			if (!Application.isPlaying || !Application.isEditor)
			{
				this.initialised = true;
			}
		}

		// Token: 0x06007192 RID: 29074 RVA: 0x0004D3BB File Offset: 0x0004B5BB
		public override string GetSummary()
		{
			if (string.IsNullOrEmpty(this.luaCompareString))
			{
				return "Error: no lua compare string provided";
			}
			return this.luaCompareString;
		}

		// Token: 0x06007193 RID: 29075 RVA: 0x0000A093 File Offset: 0x00008293
		public override bool OpenBlock()
		{
			return true;
		}

		// Token: 0x06007194 RID: 29076 RVA: 0x0004C5A3 File Offset: 0x0004A7A3
		public override Color GetButtonColor()
		{
			return new Color32(253, 253, 150, byte.MaxValue);
		}

		// Token: 0x040063F6 RID: 25590
		[Tooltip("Lua Environment to use to execute this Lua script (null for global)")]
		[SerializeField]
		protected LuaEnvironment luaEnvironment;

		// Token: 0x040063F7 RID: 25591
		[Tooltip("The lua comparison string to run; implicitly prepends 'return' onto this")]
		[TextArea]
		public string luaCompareString;

		// Token: 0x040063F8 RID: 25592
		protected bool initialised;

		// Token: 0x040063F9 RID: 25593
		protected string friendlyName = "";

		// Token: 0x040063FA RID: 25594
		protected Closure luaFunction;
	}
}
