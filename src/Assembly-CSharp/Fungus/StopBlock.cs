using UnityEngine;

namespace Fungus;

[CommandInfo("Flow", "Stop Block", "Stops executing the named Block", 0)]
public class StopBlock : Command
{
	[Tooltip("Flowchart containing the Block. If none is specified, the parent Flowchart is used.")]
	[SerializeField]
	protected Flowchart flowchart;

	[Tooltip("Name of the Block to stop")]
	[SerializeField]
	protected StringData blockName = new StringData("");

	public override void OnEnter()
	{
		if (blockName.Value == "")
		{
			Continue();
		}
		if ((Object)(object)flowchart == (Object)null)
		{
			flowchart = GetFlowchart();
		}
		Block block = flowchart.FindBlock(blockName.Value);
		if ((Object)(object)block == (Object)null || !block.IsExecuting())
		{
			Continue();
		}
		block.Stop();
		Continue();
	}

	public override string GetSummary()
	{
		return blockName;
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)253, (byte)253, (byte)150, byte.MaxValue));
	}

	public override bool HasReference(Variable variable)
	{
		if (!((Object)(object)blockName.stringRef == (Object)(object)variable))
		{
			return base.HasReference(variable);
		}
		return true;
	}
}
