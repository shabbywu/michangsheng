using UnityEngine;

namespace Fungus;

[CommandInfo("YSTools", "YSSaveGame", "保存游戏", 0)]
[AddComponentMenu("")]
public class YSSaveGame : Command
{
	[Tooltip("说明")]
	[SerializeField]
	protected string init = "该指令调用后将保存游戏存档到第一号存档中";

	[Tooltip("是否忽略自动保存间隔时间")]
	[SerializeField]
	protected bool ignoreSlot0Time;

	public override void OnEnter()
	{
		YSNewSaveSystem.SaveGame(PlayerPrefs.GetInt("NowPlayerFileAvatar"), 0, null, ignoreSlot0Time);
		Continue();
	}

	public void saveAnimation()
	{
		GameObject.Find("savagameUI").GetComponent<Animation>().Play("SaveGameUI");
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)184, (byte)210, (byte)235, byte.MaxValue));
	}
}
