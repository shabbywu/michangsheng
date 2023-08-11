using MoonSharp.Interpreter;
using UnityEngine;

namespace Fungus;

public class LuaCondition : Condition
{
	[Tooltip("Lua Environment to use to execute this Lua script (null for global)")]
	[SerializeField]
	protected LuaEnvironment luaEnvironment;

	[Tooltip("The lua comparison string to run; implicitly prepends 'return' onto this")]
	[TextArea]
	public string luaCompareString;

	protected bool initialised;

	protected string friendlyName = "";

	protected Closure luaFunction;

	protected override bool EvaluateCondition()
	{
		bool condition = false;
		luaEnvironment.RunLuaFunction(luaFunction, runAsCoroutine: false, delegate(DynValue returnValue)
		{
			if (returnValue != null)
			{
				condition = returnValue.Boolean;
			}
			else
			{
				Debug.LogWarning((object)("No return value from " + friendlyName));
			}
		});
		return condition;
	}

	protected override bool HasNeededProperties()
	{
		return !string.IsNullOrEmpty(luaCompareString);
	}

	protected virtual void Start()
	{
		InitExecuteLua();
	}

	protected virtual string GetLuaString()
	{
		return "return not not (" + luaCompareString + ")";
	}

	protected virtual void InitExecuteLua()
	{
		if (initialised)
		{
			return;
		}
		friendlyName = ((Object)((Component)this).gameObject).name + "." + ParentBlock.BlockName + "." + ((object)this).GetType().ToString() + " #" + CommandIndex;
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

	public override string GetSummary()
	{
		if (string.IsNullOrEmpty(luaCompareString))
		{
			return "Error: no lua compare string provided";
		}
		return luaCompareString;
	}

	public override bool OpenBlock()
	{
		return true;
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)253, (byte)253, (byte)150, byte.MaxValue));
	}
}
