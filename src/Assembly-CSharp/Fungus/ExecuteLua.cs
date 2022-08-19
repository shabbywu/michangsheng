using System;
using MoonSharp.Interpreter;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000DCC RID: 3532
	[CommandInfo("Scripting", "Execute Lua", "Executes a Lua code chunk using a Lua Environment.", 0)]
	public class ExecuteLua : Command
	{
		// Token: 0x06006468 RID: 25704 RVA: 0x0027E669 File Offset: 0x0027C869
		protected virtual void Start()
		{
			this.InitExecuteLua();
		}

		// Token: 0x06006469 RID: 25705 RVA: 0x0027E674 File Offset: 0x0027C874
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
				".ExecuteLua #",
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

		// Token: 0x0600646A RID: 25706 RVA: 0x0027E779 File Offset: 0x0027C979
		protected virtual string GetLuaString()
		{
			if (this.luaFile == null)
			{
				return this.luaScript;
			}
			return this.luaFile.text + "\n" + this.luaScript;
		}

		// Token: 0x0600646B RID: 25707 RVA: 0x0027E7AC File Offset: 0x0027C9AC
		protected virtual void StoreReturnVariable(DynValue returnValue)
		{
			if (this.returnVariable == null || returnValue == null)
			{
				return;
			}
			Type type = this.returnVariable.GetType();
			if (type == typeof(BooleanVariable) && returnValue.Type == DataType.Boolean)
			{
				(this.returnVariable as BooleanVariable).Value = returnValue.Boolean;
				return;
			}
			if (type == typeof(IntegerVariable) && returnValue.Type == DataType.Number)
			{
				(this.returnVariable as IntegerVariable).Value = (int)returnValue.Number;
				return;
			}
			if (type == typeof(FloatVariable) && returnValue.Type == DataType.Number)
			{
				(this.returnVariable as FloatVariable).Value = (float)returnValue.Number;
				return;
			}
			if (type == typeof(StringVariable) && returnValue.Type == DataType.String)
			{
				(this.returnVariable as StringVariable).Value = returnValue.String;
				return;
			}
			if (type == typeof(ColorVariable) && returnValue.Type == DataType.UserData)
			{
				(this.returnVariable as ColorVariable).Value = returnValue.CheckUserDataType<Color>("ExecuteLua.StoreReturnVariable", -1, TypeValidationFlags.AutoConvert);
				return;
			}
			if (type == typeof(GameObjectVariable) && returnValue.Type == DataType.UserData)
			{
				(this.returnVariable as GameObjectVariable).Value = returnValue.CheckUserDataType<GameObject>("ExecuteLua.StoreReturnVariable", -1, TypeValidationFlags.AutoConvert);
				return;
			}
			if (type == typeof(MaterialVariable) && returnValue.Type == DataType.UserData)
			{
				(this.returnVariable as MaterialVariable).Value = returnValue.CheckUserDataType<Material>("ExecuteLua.StoreReturnVariable", -1, TypeValidationFlags.AutoConvert);
				return;
			}
			if (type == typeof(ObjectVariable) && returnValue.Type == DataType.UserData)
			{
				(this.returnVariable as ObjectVariable).Value = returnValue.CheckUserDataType<Object>("ExecuteLua.StoreReturnVariable", -1, TypeValidationFlags.AutoConvert);
				return;
			}
			if (type == typeof(SpriteVariable) && returnValue.Type == DataType.UserData)
			{
				(this.returnVariable as SpriteVariable).Value = returnValue.CheckUserDataType<Sprite>("ExecuteLua.StoreReturnVariable", -1, TypeValidationFlags.AutoConvert);
				return;
			}
			if (type == typeof(TextureVariable) && returnValue.Type == DataType.UserData)
			{
				(this.returnVariable as TextureVariable).Value = returnValue.CheckUserDataType<Texture>("ExecuteLua.StoreReturnVariable", -1, TypeValidationFlags.AutoConvert);
				return;
			}
			if (type == typeof(Vector2Variable) && returnValue.Type == DataType.UserData)
			{
				(this.returnVariable as Vector2Variable).Value = returnValue.CheckUserDataType<Vector2>("ExecuteLua.StoreReturnVariable", -1, TypeValidationFlags.AutoConvert);
				return;
			}
			if (type == typeof(Vector3Variable) && returnValue.Type == DataType.UserData)
			{
				(this.returnVariable as Vector3Variable).Value = returnValue.CheckUserDataType<Vector3>("ExecuteLua.StoreReturnVariable", -1, TypeValidationFlags.AutoConvert);
				return;
			}
			Debug.LogError("Failed to convert " + returnValue.Type.ToLuaTypeString() + " return type to " + type.ToString());
		}

		// Token: 0x0600646C RID: 25708 RVA: 0x0027EA90 File Offset: 0x0027CC90
		public override void OnEnter()
		{
			this.InitExecuteLua();
			if (this.luaFunction == null)
			{
				this.Continue();
			}
			this.luaEnvironment.RunLuaFunction(this.luaFunction, this.runAsCoroutine, delegate(DynValue returnValue)
			{
				this.StoreReturnVariable(returnValue);
				if (this.waitUntilFinished)
				{
					this.Continue();
				}
			});
			if (!this.waitUntilFinished)
			{
				this.Continue();
			}
		}

		// Token: 0x0600646D RID: 25709 RVA: 0x0027EAE2 File Offset: 0x0027CCE2
		public override string GetSummary()
		{
			return this.luaScript;
		}

		// Token: 0x0600646E RID: 25710 RVA: 0x0027D3DB File Offset: 0x0027B5DB
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x0600646F RID: 25711 RVA: 0x0027EAEA File Offset: 0x0027CCEA
		public override bool HasReference(Variable variable)
		{
			return this.returnVariable == variable || base.HasReference(variable);
		}

		// Token: 0x04005645 RID: 22085
		[Tooltip("Lua Environment to use to execute this Lua script")]
		[SerializeField]
		protected LuaEnvironment luaEnvironment;

		// Token: 0x04005646 RID: 22086
		[Tooltip("A text file containing Lua script to execute.")]
		[SerializeField]
		protected TextAsset luaFile;

		// Token: 0x04005647 RID: 22087
		[TextArea(10, 100)]
		[Tooltip("Lua script to execute. This text is appended to the contents of Lua file (if one is specified).")]
		[SerializeField]
		protected string luaScript;

		// Token: 0x04005648 RID: 22088
		[Tooltip("Execute this Lua script as a Lua coroutine")]
		[SerializeField]
		protected bool runAsCoroutine = true;

		// Token: 0x04005649 RID: 22089
		[Tooltip("Pause command execution until the Lua script has finished execution")]
		[SerializeField]
		protected bool waitUntilFinished = true;

		// Token: 0x0400564A RID: 22090
		[Tooltip("A Flowchart variable to store the returned value in.")]
		[VariableProperty(new Type[]
		{

		})]
		[SerializeField]
		protected Variable returnVariable;

		// Token: 0x0400564B RID: 22091
		protected string friendlyName = "";

		// Token: 0x0400564C RID: 22092
		protected bool initialised;

		// Token: 0x0400564D RID: 22093
		protected Closure luaFunction;
	}
}
