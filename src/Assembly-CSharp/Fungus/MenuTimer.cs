using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus;

[CommandInfo("Narrative", "Menu Timer", "Displays a timer bar and executes a target block if the player fails to select a menu option in time.", 0)]
[AddComponentMenu("")]
[ExecuteInEditMode]
public class MenuTimer : Command
{
	[Tooltip("Length of time to display the timer for")]
	[SerializeField]
	protected FloatData _duration = new FloatData(1f);

	[FormerlySerializedAs("targetSequence")]
	[Tooltip("Block to execute when the timer expires")]
	[SerializeField]
	protected Block targetBlock;

	[HideInInspector]
	[FormerlySerializedAs("duration")]
	public float durationOLD;

	public override void OnEnter()
	{
		MenuDialog menuDialog = MenuDialog.GetMenuDialog();
		if ((Object)(object)menuDialog != (Object)null && (Object)(object)targetBlock != (Object)null)
		{
			menuDialog.ShowTimer(_duration.Value, targetBlock);
		}
		Continue();
	}

	public override void GetConnectedBlocks(ref List<Block> connectedBlocks)
	{
		if ((Object)(object)targetBlock != (Object)null)
		{
			connectedBlocks.Add(targetBlock);
		}
	}

	public override string GetSummary()
	{
		if ((Object)(object)targetBlock == (Object)null)
		{
			return "Error: No target block selected";
		}
		return targetBlock.BlockName;
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)184, (byte)210, (byte)235, byte.MaxValue));
	}

	public override bool HasReference(Variable variable)
	{
		if (!((Object)(object)_duration.floatRef == (Object)(object)variable))
		{
			return base.HasReference(variable);
		}
		return true;
	}

	protected virtual void OnEnable()
	{
		if (durationOLD != 0f)
		{
			_duration.Value = durationOLD;
			durationOLD = 0f;
		}
	}
}
