using System;
using UnityEngine;

namespace Fungus;

[CommandInfo("Narrative", "Menu Shuffle", "Shuffle the order of the items in a Fungus Menu", 0)]
[AddComponentMenu("")]
[ExecuteInEditMode]
public class MenuShuffle : Command
{
	public enum Mode
	{
		Every,
		Once
	}

	[SerializeField]
	[Tooltip("Determines if the order is shuffled everytime this command is it (Every) or if it is consistent when returned to but random (Once)")]
	protected Mode shuffleMode = Mode.Once;

	private int seed = -1;

	public override void OnEnter()
	{
		MenuDialog menuDialog = MenuDialog.GetMenuDialog();
		if (shuffleMode == Mode.Every || seed == -1)
		{
			seed = Random.Range(0, 1000000);
		}
		if ((Object)(object)menuDialog != (Object)null)
		{
			menuDialog.Shuffle(new Random(seed));
		}
		Continue();
	}

	public override string GetSummary()
	{
		return shuffleMode.ToString();
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)184, (byte)210, (byte)235, byte.MaxValue));
	}
}
