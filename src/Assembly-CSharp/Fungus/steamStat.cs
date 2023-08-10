using Steamworks;
using UnityEngine;

namespace Fungus;

[CommandInfo("YSTools", "steamStat", "设置steam变量的值", 0)]
[AddComponentMenu("")]
public class steamStat : Command
{
	[Tooltip("steam状态名称")]
	[SerializeField]
	protected string StatName = "";

	public override void OnEnter()
	{
		SteamChengJiu.ints.SetAchievement(StatName);
		SteamUserStats.StoreStats();
		Continue();
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)184, (byte)210, (byte)235, byte.MaxValue));
	}

	public override void OnReset()
	{
	}
}
