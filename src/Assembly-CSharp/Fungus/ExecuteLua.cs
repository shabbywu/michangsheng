using System;
using MoonSharp.Interpreter;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001206 RID: 4614
	[CommandInfo("Scripting", "Execute Lua", "Executes a Lua code chunk using a Lua Environment.", 0)]
	public class ExecuteLua : Command
	{
		// Token: 0x060070E7 RID: 28903 RVA: 0x0004CA25 File Offset: 0x0004AC25
		protected virtual void Start()
		{
			this.InitExecuteLua();
		}

		// Token: 0x060070E8 RID: 28904 RVA: 0x002A3634 File Offset: 0x002A1834
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

		// Token: 0x060070E9 RID: 28905 RVA: 0x0004CA2D File Offset: 0x0004AC2D
		protected virtual string GetLuaString()
		{
			if (this.luaFile == null)
			{
				return this.luaScript;
			}
			return this.luaFile.text + "\n" + this.luaScript;
		}

		// Token: 0x060070EA RID: 28906 RVA: 0x002A373C File Offset: 0x002A193C
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

		// Token: 0x060070EB RID: 28907 RVA: 0x002A3A20 File Offset: 0x002A1C20
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

		// Token: 0x060070EC RID: 28908 RVA: 0x0004CA5F File Offset: 0x0004AC5F
		public override string GetSummary()
		{
			return this.luaScript;
		}

		// Token: 0x060070ED RID: 28909 RVA: 0x0004C5E0 File Offset: 0x0004A7E0
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x060070EE RID: 28910 RVA: 0x0004CA67 File Offset: 0x0004AC67
		public override bool HasReference(Variable variable)
		{
			return this.returnVariable == variable || base.HasReference(variable);
		}

		// Token: 0x04006344 RID: 25412
		[Tooltip("Lua Environment to use to execute this Lua script")]
		[SerializeField]
		protected LuaEnvironment luaEnvironment;

		// Token: 0x04006345 RID: 25413
		[Tooltip("A text file containing Lua script to execute.")]
		[SerializeField]
		protected TextAsset luaFile;

		// Token: 0x04006346 RID: 25414
		[TextArea(10, 100)]
		[Tooltip("Lua script to execute. This text is appended to the contents of Lua file (if one is specified).")]
		[SerializeField]
		protected string luaScript;

		// Token: 0x04006347 RID: 25415
		[Tooltip("Execute this Lua script as a Lua coroutine")]
		[SerializeField]
		protected bool runAsCoroutine = true;

		// Token: 0x04006348 RID: 25416
		[Tooltip("Pause command execution until the Lua script has finished execution")]
		[SerializeField]
		protected bool waitUntilFinished = true;

		// Token: 0x04006349 RID: 25417
		[Tooltip("A Flowchart variable to store the returned value in.")]
		[VariableProperty(new Type[]
		{

		})]
		[SerializeField]
		protected Variable returnVariable;

		// Token: 0x0400634A RID: 25418
		protected string friendlyName = "";

		// Token: 0x0400634B RID: 25419
		protected bool initialised;

		// Token: 0x0400634C RID: 25420
		protected Closure luaFunction;
	}
}
