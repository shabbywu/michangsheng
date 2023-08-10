using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus;

[CommandInfo("Flow", "Call", "Execute another block in the same Flowchart as the command, or in a different Flowchart.", 0)]
[AddComponentMenu("")]
public class Call : Command, INoCommand
{
	[Tooltip("Flowchart which contains the block to execute. If none is specified then the current Flowchart is used.")]
	[SerializeField]
	protected Flowchart targetFlowchart;

	[FormerlySerializedAs("targetSequence")]
	[Tooltip("Block to start executing")]
	[SerializeField]
	protected Block targetBlock;

	[Tooltip("Label to start execution at. Takes priority over startIndex.")]
	[SerializeField]
	protected StringData startLabel;

	[Tooltip("Command index to start executing")]
	[FormerlySerializedAs("commandIndex")]
	[SerializeField]
	protected int startIndex;

	[Tooltip("Select if the calling block should stop or continue executing commands, or wait until the called block finishes.")]
	[SerializeField]
	protected CallMode callMode;

	public override void OnEnter()
	{
		Flowchart flowchart = GetFlowchart();
		if ((Object)(object)targetBlock != (Object)null)
		{
			if ((Object)(object)ParentBlock != (Object)null && ((object)ParentBlock).Equals((object?)targetBlock))
			{
				Continue(0);
				return;
			}
			if (targetBlock.IsExecuting())
			{
				Debug.LogWarning((object)(targetBlock.BlockName + " cannot be called/executed, it is already running."));
				Continue();
				return;
			}
			Action onComplete = null;
			if (callMode == CallMode.WaitUntilFinished)
			{
				onComplete = delegate
				{
					flowchart.SelectedBlock = ParentBlock;
					Continue();
				};
			}
			int commandIndex = startIndex;
			if (startLabel.Value != "")
			{
				int labelIndex = targetBlock.GetLabelIndex(startLabel.Value);
				if (labelIndex != -1)
				{
					commandIndex = labelIndex;
				}
			}
			if ((Object)(object)targetFlowchart == (Object)null || ((object)targetFlowchart).Equals((object?)GetFlowchart()))
			{
				if ((Object)(object)flowchart.SelectedBlock == (Object)(object)ParentBlock)
				{
					flowchart.SelectedBlock = targetBlock;
				}
				if (callMode == CallMode.StopThenCall)
				{
					StopParentBlock();
				}
				((MonoBehaviour)this).StartCoroutine(targetBlock.Execute(commandIndex, onComplete));
			}
			else
			{
				if (callMode == CallMode.StopThenCall)
				{
					StopParentBlock();
				}
				targetFlowchart.ExecuteBlock(targetBlock, commandIndex, onComplete);
			}
		}
		if (callMode == CallMode.Stop)
		{
			StopParentBlock();
		}
		else if (callMode == CallMode.Continue)
		{
			Continue();
		}
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
		string text = "";
		text = ((!((Object)(object)targetBlock == (Object)null)) ? targetBlock.BlockName : "<None>");
		return text + " : " + callMode;
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)235, (byte)191, (byte)217, byte.MaxValue));
	}

	public override bool HasReference(Variable variable)
	{
		if (!((Object)(object)startLabel.stringRef == (Object)(object)variable))
		{
			return base.HasReference(variable);
		}
		return true;
	}
}
