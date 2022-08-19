using System;
using MoonSharp.Interpreter;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000DED RID: 3565
	public class LuaCondition : Condition
	{
		// Token: 0x06006501 RID: 25857 RVA: 0x00281A10 File Offset: 0x0027FC10
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

		// Token: 0x06006502 RID: 25858 RVA: 0x00281A55 File Offset: 0x0027FC55
		protected override bool HasNeededProperties()
		{
			return !string.IsNullOrEmpty(this.luaCompareString);
		}

		// Token: 0x06006503 RID: 25859 RVA: 0x00281A65 File Offset: 0x0027FC65
		protected virtual void Start()
		{
			this.InitExecuteLua();
		}

		// Token: 0x06006504 RID: 25860 RVA: 0x00281A6D File Offset: 0x0027FC6D
		protected virtual string GetLuaString()
		{
			return "return not not (" + this.luaCompareString + ")";
		}

		// Token: 0x06006505 RID: 25861 RVA: 0x00281A84 File Offset: 0x0027FC84
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

		// Token: 0x06006506 RID: 25862 RVA: 0x00281B9F File Offset: 0x0027FD9F
		public override string GetSummary()
		{
			if (string.IsNullOrEmpty(this.luaCompareString))
			{
				return "Error: no lua compare string provided";
			}
			return this.luaCompareString;
		}

		// Token: 0x06006507 RID: 25863 RVA: 0x00024C5F File Offset: 0x00022E5F
		public override bool OpenBlock()
		{
			return true;
		}

		// Token: 0x06006508 RID: 25864 RVA: 0x0027D1B6 File Offset: 0x0027B3B6
		public override Color GetButtonColor()
		{
			return new Color32(253, 253, 150, byte.MaxValue);
		}

		// Token: 0x040056E5 RID: 22245
		[Tooltip("Lua Environment to use to execute this Lua script (null for global)")]
		[SerializeField]
		protected LuaEnvironment luaEnvironment;

		// Token: 0x040056E6 RID: 22246
		[Tooltip("The lua comparison string to run; implicitly prepends 'return' onto this")]
		[TextArea]
		public string luaCompareString;

		// Token: 0x040056E7 RID: 22247
		protected bool initialised;

		// Token: 0x040056E8 RID: 22248
		protected string friendlyName = "";

		// Token: 0x040056E9 RID: 22249
		protected Closure luaFunction;
	}
}
