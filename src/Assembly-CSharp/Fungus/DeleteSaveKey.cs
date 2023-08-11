using UnityEngine;

namespace Fungus;

[CommandInfo("Variable", "Delete Save Key", "Deletes a saved value from permanent storage.", 0)]
[AddComponentMenu("")]
public class DeleteSaveKey : Command
{
	[Tooltip("Name of the saved value. Supports variable substition e.g. \"player_{$PlayerNumber}")]
	[SerializeField]
	protected string key = "";

	public override void OnEnter()
	{
		if (key == "")
		{
			Continue();
			return;
		}
		Flowchart flowchart = GetFlowchart();
		PlayerPrefs.DeleteKey(SetSaveProfile.SaveProfile + "_" + flowchart.SubstituteVariables(key));
		Continue();
	}

	public override string GetSummary()
	{
		if (key.Length == 0)
		{
			return "Error: No stored value key selected";
		}
		return key;
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)235, (byte)191, (byte)217, byte.MaxValue));
	}
}
