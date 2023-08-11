using System;
using MoonSharp.Interpreter;
using UnityEngine;

namespace Fungus;

[CommandInfo("Scripting", "Execute Lua", "Executes a Lua code chunk using a Lua Environment.", 0)]
public class ExecuteLua : Command
{
	[Tooltip("Lua Environment to use to execute this Lua script")]
	[SerializeField]
	protected LuaEnvironment luaEnvironment;

	[Tooltip("A text file containing Lua script to execute.")]
	[SerializeField]
	protected TextAsset luaFile;

	[TextArea(10, 100)]
	[Tooltip("Lua script to execute. This text is appended to the contents of Lua file (if one is specified).")]
	[SerializeField]
	protected string luaScript;

	[Tooltip("Execute this Lua script as a Lua coroutine")]
	[SerializeField]
	protected bool runAsCoroutine = true;

	[Tooltip("Pause command execution until the Lua script has finished execution")]
	[SerializeField]
	protected bool waitUntilFinished = true;

	[Tooltip("A Flowchart variable to store the returned value in.")]
	[VariableProperty(new Type[] { })]
	[SerializeField]
	protected Variable returnVariable;

	protected string friendlyName = "";

	protected bool initialised;

	protected Closure luaFunction;

	protected virtual void Start()
	{
		InitExecuteLua();
	}

	protected virtual void InitExecuteLua()
	{
		if (initialised)
		{
			return;
		}
		friendlyName = ((Object)((Component)this).gameObject).name + "." + ParentBlock.BlockName + ".ExecuteLua #" + CommandIndex;
		Flowchart flowchart = GetFlowchart();
		if ((Object)(object)luaEnvironment == (Object)null)
		{
			luaEnvironment = flowchart.LuaEnv;
		}
		if ((Object)(object)luaEnvironment == (Object)null)
		{
			luaEnvironment = LuaEnvironment.GetLua();
		}
		string luaString = GetLuaString();
		luaFunction = luaEnvironment.LoadLuaFunction(luaString, friendlyName);
		if (flowchart.LuaBindingName != "")
		{
			Table globals = luaEnvironment.Interpreter.Globals;
			if (globals != null)
			{
				globals[flowchart.LuaBindingName] = flowchart;
			}
		}
		if (!Application.isPlaying || !Application.isEditor)
		{
			initialised = true;
		}
	}

	protected virtual string GetLuaString()
	{
		if ((Object)(object)luaFile == (Object)null)
		{
			return luaScript;
		}
		return luaFile.text + "\n" + luaScript;
	}

	protected virtual void StoreReturnVariable(DynValue returnValue)
	{
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_026c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a5: Unknown result type (might be due to invalid IL or missing references)
		if (!((Object)(object)returnVariable == (Object)null) && returnValue != null)
		{
			Type type = ((object)returnVariable).GetType();
			if (type == typeof(BooleanVariable) && returnValue.Type == DataType.Boolean)
			{
				(returnVariable as BooleanVariable).Value = returnValue.Boolean;
			}
			else if (type == typeof(IntegerVariable) && returnValue.Type == DataType.Number)
			{
				(returnVariable as IntegerVariable).Value = (int)returnValue.Number;
			}
			else if (type == typeof(FloatVariable) && returnValue.Type == DataType.Number)
			{
				(returnVariable as FloatVariable).Value = (float)returnValue.Number;
			}
			else if (type == typeof(StringVariable) && returnValue.Type == DataType.String)
			{
				(returnVariable as StringVariable).Value = returnValue.String;
			}
			else if (type == typeof(ColorVariable) && returnValue.Type == DataType.UserData)
			{
				(returnVariable as ColorVariable).Value = returnValue.CheckUserDataType<Color>("ExecuteLua.StoreReturnVariable");
			}
			else if (type == typeof(GameObjectVariable) && returnValue.Type == DataType.UserData)
			{
				(returnVariable as GameObjectVariable).Value = returnValue.CheckUserDataType<GameObject>("ExecuteLua.StoreReturnVariable");
			}
			else if (type == typeof(MaterialVariable) && returnValue.Type == DataType.UserData)
			{
				(returnVariable as MaterialVariable).Value = returnValue.CheckUserDataType<Material>("ExecuteLua.StoreReturnVariable");
			}
			else if (type == typeof(ObjectVariable) && returnValue.Type == DataType.UserData)
			{
				(returnVariable as ObjectVariable).Value = returnValue.CheckUserDataType<Object>("ExecuteLua.StoreReturnVariable");
			}
			else if (type == typeof(SpriteVariable) && returnValue.Type == DataType.UserData)
			{
				(returnVariable as SpriteVariable).Value = returnValue.CheckUserDataType<Sprite>("ExecuteLua.StoreReturnVariable");
			}
			else if (type == typeof(TextureVariable) && returnValue.Type == DataType.UserData)
			{
				(returnVariable as TextureVariable).Value = returnValue.CheckUserDataType<Texture>("ExecuteLua.StoreReturnVariable");
			}
			else if (type == typeof(Vector2Variable) && returnValue.Type == DataType.UserData)
			{
				(returnVariable as Vector2Variable).Value = returnValue.CheckUserDataType<Vector2>("ExecuteLua.StoreReturnVariable");
			}
			else if (type == typeof(Vector3Variable) && returnValue.Type == DataType.UserData)
			{
				(returnVariable as Vector3Variable).Value = returnValue.CheckUserDataType<Vector3>("ExecuteLua.StoreReturnVariable");
			}
			else
			{
				Debug.LogError((object)("Failed to convert " + returnValue.Type.ToLuaTypeString() + " return type to " + type.ToString()));
			}
		}
	}

	public override void OnEnter()
	{
		InitExecuteLua();
		if (luaFunction == null)
		{
			Continue();
		}
		luaEnvironment.RunLuaFunction(luaFunction, runAsCoroutine, delegate(DynValue returnValue)
		{
			StoreReturnVariable(returnValue);
			if (waitUntilFinished)
			{
				Continue();
			}
		});
		if (!waitUntilFinished)
		{
			Continue();
		}
	}

	public override string GetSummary()
	{
		return luaScript;
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)235, (byte)191, (byte)217, byte.MaxValue));
	}

	public override bool HasReference(Variable variable)
	{
		if (!((Object)(object)returnVariable == (Object)(object)variable))
		{
			return base.HasReference(variable);
		}
		return true;
	}
}
