using UnityEngine;

namespace Fungus;

[CommandInfo("Variable", "Set Save Profile", "Sets the active profile that the Save Variable and Load Variable commands will use. This is useful to crete multiple player save games. Once set, the profile applies across all Flowcharts and will also persist across scene loads.", 0)]
[AddComponentMenu("")]
public class SetSaveProfile : Command
{
	[Tooltip("Name of save profile to make active.")]
	[SerializeField]
	protected string saveProfileName = "";

	private static string saveProfile = "";

	public static string SaveProfile => saveProfile;

	public override void OnEnter()
	{
		saveProfile = saveProfileName;
		Continue();
	}

	public override string GetSummary()
	{
		return saveProfileName;
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)235, (byte)191, (byte)217, byte.MaxValue));
	}
}
