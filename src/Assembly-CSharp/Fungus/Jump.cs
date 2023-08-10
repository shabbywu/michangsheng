using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus;

[CommandInfo("Flow", "Jump", "Move execution to a specific Label command in the same block", 0)]
[AddComponentMenu("")]
[ExecuteInEditMode]
public class Jump : Command
{
	[Tooltip("Name of a label in this block to jump to")]
	[SerializeField]
	protected StringData _targetLabel = new StringData("");

	[HideInInspector]
	[FormerlySerializedAs("targetLabel")]
	public Label targetLabelOLD;

	public override void OnEnter()
	{
		if (_targetLabel.Value == "")
		{
			Continue();
			return;
		}
		List<Command> commandList = ParentBlock.CommandList;
		for (int i = 0; i < commandList.Count; i++)
		{
			Label label = commandList[i] as Label;
			if ((Object)(object)label != (Object)null && label.Key == _targetLabel.Value)
			{
				Continue(label.CommandIndex + 1);
				return;
			}
		}
		Debug.LogWarning((object)("Label not found: " + _targetLabel.Value));
		Continue();
	}

	public override string GetSummary()
	{
		if (_targetLabel.Value == "")
		{
			return "Error: No label selected";
		}
		return _targetLabel.Value;
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)253, (byte)253, (byte)150, byte.MaxValue));
	}

	public override bool HasReference(Variable variable)
	{
		if (!((Object)(object)_targetLabel.stringRef == (Object)(object)variable))
		{
			return base.HasReference(variable);
		}
		return true;
	}

	protected virtual void OnEnable()
	{
		if ((Object)(object)targetLabelOLD != (Object)null)
		{
			_targetLabel.Value = targetLabelOLD.Key;
			targetLabelOLD = null;
		}
	}
}
