using System;
using UnityEngine;
using UnityEngine.Events;

namespace Fungus;

[CommandInfo("Scripting", "Invoke Event", "Calls a list of component methods via the Unity Event System (as used in the Unity UI). This command is more efficient than the Invoke Method command but can only pass a single parameter and doesn't support return values.", 0)]
[AddComponentMenu("")]
public class InvokeEvent : Command
{
	[Serializable]
	public class BooleanEvent : UnityEvent<bool>
	{
	}

	[Serializable]
	public class IntegerEvent : UnityEvent<int>
	{
	}

	[Serializable]
	public class FloatEvent : UnityEvent<float>
	{
	}

	[Serializable]
	public class StringEvent : UnityEvent<string>
	{
	}

	[Tooltip("A description of what this command does. Appears in the command summary.")]
	[SerializeField]
	protected string description = "";

	[Tooltip("Delay (in seconds) before the methods will be called")]
	[SerializeField]
	protected float delay;

	[Tooltip("Selects type of method parameter to pass")]
	[SerializeField]
	protected InvokeType invokeType;

	[Tooltip("List of methods to call. Supports methods with no parameters or exactly one string, int, float or object parameter.")]
	[SerializeField]
	protected UnityEvent staticEvent = new UnityEvent();

	[Tooltip("Boolean parameter to pass to the invoked methods.")]
	[SerializeField]
	protected BooleanData booleanParameter;

	[Tooltip("List of methods to call. Supports methods with one boolean parameter.")]
	[SerializeField]
	protected BooleanEvent booleanEvent = new BooleanEvent();

	[Tooltip("Integer parameter to pass to the invoked methods.")]
	[SerializeField]
	protected IntegerData integerParameter;

	[Tooltip("List of methods to call. Supports methods with one integer parameter.")]
	[SerializeField]
	protected IntegerEvent integerEvent = new IntegerEvent();

	[Tooltip("Float parameter to pass to the invoked methods.")]
	[SerializeField]
	protected FloatData floatParameter;

	[Tooltip("List of methods to call. Supports methods with one float parameter.")]
	[SerializeField]
	protected FloatEvent floatEvent = new FloatEvent();

	[Tooltip("String parameter to pass to the invoked methods.")]
	[SerializeField]
	protected StringDataMulti stringParameter;

	[Tooltip("List of methods to call. Supports methods with one string parameter.")]
	[SerializeField]
	protected StringEvent stringEvent = new StringEvent();

	protected virtual void DoInvoke()
	{
		switch (invokeType)
		{
		default:
			staticEvent.Invoke();
			break;
		case InvokeType.DynamicBoolean:
			((UnityEvent<bool>)booleanEvent).Invoke(booleanParameter.Value);
			break;
		case InvokeType.DynamicInteger:
			((UnityEvent<int>)integerEvent).Invoke(integerParameter.Value);
			break;
		case InvokeType.DynamicFloat:
			((UnityEvent<float>)floatEvent).Invoke(floatParameter.Value);
			break;
		case InvokeType.DynamicString:
			((UnityEvent<string>)stringEvent).Invoke(stringParameter.Value);
			break;
		}
	}

	public override void OnEnter()
	{
		if (Mathf.Approximately(delay, 0f))
		{
			DoInvoke();
		}
		else
		{
			((MonoBehaviour)this).Invoke("DoInvoke", delay);
		}
		Continue();
	}

	public override string GetSummary()
	{
		if (!string.IsNullOrEmpty(description))
		{
			return description;
		}
		string text = invokeType.ToString() + " ";
		return invokeType switch
		{
			InvokeType.DynamicBoolean => text + ((UnityEventBase)booleanEvent).GetPersistentEventCount(), 
			InvokeType.DynamicInteger => text + ((UnityEventBase)integerEvent).GetPersistentEventCount(), 
			InvokeType.DynamicFloat => text + ((UnityEventBase)floatEvent).GetPersistentEventCount(), 
			InvokeType.DynamicString => text + ((UnityEventBase)stringEvent).GetPersistentEventCount(), 
			_ => text + ((UnityEventBase)staticEvent).GetPersistentEventCount(), 
		} + " methods";
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)235, (byte)191, (byte)217, byte.MaxValue));
	}

	public override bool HasReference(Variable variable)
	{
		if (!((Object)(object)booleanParameter.booleanRef == (Object)(object)variable) && !((Object)(object)integerParameter.integerRef == (Object)(object)variable) && !((Object)(object)floatParameter.floatRef == (Object)(object)variable) && !((Object)(object)stringParameter.stringRef == (Object)(object)variable))
		{
			return base.HasReference(variable);
		}
		return true;
	}
}
