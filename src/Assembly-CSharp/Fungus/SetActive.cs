using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus;

[CommandInfo("Scripting", "Set Active", "Sets a game object in the scene to be active / inactive.", 0)]
[AddComponentMenu("")]
[ExecuteInEditMode]
public class SetActive : Command
{
	[Tooltip("Reference to game object to enable / disable")]
	[SerializeField]
	protected GameObjectData _targetGameObject;

	[Tooltip("Set to true to enable the game object")]
	[SerializeField]
	protected BooleanData activeState;

	[HideInInspector]
	[FormerlySerializedAs("targetGameObject")]
	public GameObject targetGameObjectOLD;

	public override void OnEnter()
	{
		if ((Object)(object)_targetGameObject.Value != (Object)null)
		{
			_targetGameObject.Value.SetActive(activeState.Value);
		}
		Continue();
	}

	public override string GetSummary()
	{
		if ((Object)(object)_targetGameObject.Value == (Object)null)
		{
			return "Error: No game object selected";
		}
		return ((Object)_targetGameObject.Value).name + " = " + activeState.GetDescription();
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)235, (byte)191, (byte)217, byte.MaxValue));
	}

	public override bool HasReference(Variable variable)
	{
		if (!((Object)(object)_targetGameObject.gameObjectRef == (Object)(object)variable) && !((Object)(object)activeState.booleanRef == (Object)(object)variable))
		{
			return base.HasReference(variable);
		}
		return true;
	}

	protected virtual void OnEnable()
	{
		if ((Object)(object)targetGameObjectOLD != (Object)null)
		{
			_targetGameObject.Value = targetGameObjectOLD;
			targetGameObjectOLD = null;
		}
	}
}
