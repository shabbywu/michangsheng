using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus;

[CommandInfo("Animation", "Reset Anim Trigger", "Resets a trigger parameter on an Animator component.", 0)]
[AddComponentMenu("")]
[ExecuteInEditMode]
public class ResetAnimTrigger : Command
{
	[Tooltip("Reference to an Animator component in a game object")]
	[SerializeField]
	protected AnimatorData _animator;

	[Tooltip("Name of the trigger Animator parameter that will be reset")]
	[SerializeField]
	protected StringData _parameterName;

	[HideInInspector]
	[FormerlySerializedAs("animator")]
	public Animator animatorOLD;

	[HideInInspector]
	[FormerlySerializedAs("parameterName")]
	public string parameterNameOLD = "";

	public override void OnEnter()
	{
		if ((Object)(object)_animator.Value != (Object)null)
		{
			_animator.Value.ResetTrigger(_parameterName.Value);
		}
		Continue();
	}

	public override string GetSummary()
	{
		if ((Object)(object)_animator.Value == (Object)null)
		{
			return "Error: No animator selected";
		}
		return ((Object)_animator.Value).name + " (" + _parameterName.Value + ")";
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)170, (byte)204, (byte)169, byte.MaxValue));
	}

	public override bool HasReference(Variable variable)
	{
		if (!((Object)(object)_animator.animatorRef == (Object)(object)variable) && !((Object)(object)_parameterName.stringRef == (Object)(object)variable))
		{
			return base.HasReference(variable);
		}
		return true;
	}

	protected virtual void OnEnable()
	{
		if ((Object)(object)animatorOLD != (Object)null)
		{
			_animator.Value = animatorOLD;
			animatorOLD = null;
		}
		if (parameterNameOLD != "")
		{
			_parameterName.Value = parameterNameOLD;
			parameterNameOLD = "";
		}
	}
}
