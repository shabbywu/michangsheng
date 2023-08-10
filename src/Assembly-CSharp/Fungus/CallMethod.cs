using UnityEngine;

namespace Fungus;

[CommandInfo("Scripting", "Call Method", "Calls a named method on a GameObject using the GameObject.SendMessage() system.", 0)]
[AddComponentMenu("")]
public class CallMethod : Command
{
	[Tooltip("Target monobehavior which contains the method we want to call")]
	[SerializeField]
	protected GameObject targetObject;

	[Tooltip("Name of the method to call")]
	[SerializeField]
	protected string methodName = "";

	[Tooltip("Delay (in seconds) before the method will be called")]
	[SerializeField]
	protected float delay;

	protected virtual void CallTheMethod()
	{
		targetObject.SendMessage(methodName, (SendMessageOptions)1);
	}

	public override void OnEnter()
	{
		if ((Object)(object)targetObject == (Object)null || methodName.Length == 0)
		{
			Continue();
			return;
		}
		if (Mathf.Approximately(delay, 0f))
		{
			CallTheMethod();
		}
		else
		{
			((MonoBehaviour)this).Invoke("CallTheMethod", delay);
		}
		Continue();
	}

	public override string GetSummary()
	{
		if ((Object)(object)targetObject == (Object)null)
		{
			return "Error: No target GameObject specified";
		}
		if (methodName.Length == 0)
		{
			return "Error: No named method specified";
		}
		return ((Object)targetObject).name + " : " + methodName;
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)235, (byte)191, (byte)217, byte.MaxValue));
	}
}
