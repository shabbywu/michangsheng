using UnityEngine;

namespace Fungus;

[CommandInfo("Sprite", "Set Draggable 2D", "Sets a Draggable2D component to be draggable / non-draggable.", 0)]
[AddComponentMenu("")]
public class SetDraggable2D : Command
{
	[Tooltip("Reference to Draggable2D component on a gameobject")]
	[SerializeField]
	protected Draggable2D targetDraggable2D;

	[Tooltip("Set to true to enable the component")]
	[SerializeField]
	protected BooleanData activeState;

	public override void OnEnter()
	{
		if ((Object)(object)targetDraggable2D != (Object)null)
		{
			targetDraggable2D.DragEnabled = activeState.Value;
		}
		Continue();
	}

	public override string GetSummary()
	{
		if ((Object)(object)targetDraggable2D == (Object)null)
		{
			return "Error: No Draggable2D component selected";
		}
		return ((Object)((Component)targetDraggable2D).gameObject).name;
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)235, (byte)191, (byte)217, byte.MaxValue));
	}

	public override bool HasReference(Variable variable)
	{
		if (!((Object)(object)activeState.booleanRef == (Object)(object)variable))
		{
			return base.HasReference(variable);
		}
		return true;
	}
}
