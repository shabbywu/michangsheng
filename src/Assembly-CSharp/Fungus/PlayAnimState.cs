using UnityEngine;

namespace Fungus;

[CommandInfo("Animation", "Play Anim State", "Plays a state of an animator according to the state name", 0)]
[AddComponentMenu("")]
public class PlayAnimState : Command
{
	[Tooltip("Reference to an Animator component in a game object")]
	[SerializeField]
	protected AnimatorData animator;

	[Tooltip("Name of the state you want to play")]
	[SerializeField]
	protected StringData stateName;

	[Tooltip("Layer to play animation on")]
	[SerializeField]
	protected IntegerData layer = new IntegerData(-1);

	[Tooltip("Start time of animation")]
	[SerializeField]
	protected FloatData time = new FloatData(0f);

	public override void OnEnter()
	{
		if ((Object)(object)animator.Value != (Object)null)
		{
			animator.Value.Play(stateName.Value, layer.Value, time.Value);
		}
		Continue();
	}

	public override string GetSummary()
	{
		if ((Object)(object)animator.Value == (Object)null)
		{
			return "Error: No animator selected";
		}
		return ((Object)animator.Value).name + " (" + stateName.Value + ")";
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)170, (byte)204, (byte)169, byte.MaxValue));
	}

	public override bool HasReference(Variable variable)
	{
		if (!((Object)(object)animator.animatorRef == (Object)(object)variable) && !((Object)(object)stateName.stringRef == (Object)(object)variable) && !((Object)(object)layer.integerRef == (Object)(object)variable) && !((Object)(object)time.floatRef == (Object)(object)variable))
		{
			return base.HasReference(variable);
		}
		return true;
	}
}
