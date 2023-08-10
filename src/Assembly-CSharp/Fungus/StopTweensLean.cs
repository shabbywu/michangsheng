using UnityEngine;

namespace Fungus;

[CommandInfo("LeanTween", "StopTweens", "Stops the LeanTweens on a target GameObject", 0)]
[AddComponentMenu("")]
[ExecuteInEditMode]
public class StopTweensLean : Command
{
	[Tooltip("Target game object stop LeanTweens on")]
	[SerializeField]
	protected GameObjectData _targetObject;

	public override void OnEnter()
	{
		if ((Object)(object)_targetObject.Value != (Object)null)
		{
			LeanTween.cancel(_targetObject.Value);
		}
		Continue();
	}

	public override string GetSummary()
	{
		if ((Object)(object)_targetObject.Value == (Object)null)
		{
			return "Error: No target object selected";
		}
		return "Stop all LeanTweens on " + ((Object)_targetObject.Value).name;
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)233, (byte)163, (byte)180, byte.MaxValue));
	}

	public override bool HasReference(Variable variable)
	{
		return (Object)(object)_targetObject.gameObjectRef == (Object)(object)variable;
	}
}
