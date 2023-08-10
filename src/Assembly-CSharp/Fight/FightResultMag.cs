using UnityEngine;

namespace Fight;

public class FightResultMag : MonoBehaviour
{
	public static FightResultMag inst;

	private void Awake()
	{
		inst = this;
	}

	public void ShowVictory()
	{
		((MonoBehaviour)this).Invoke("LaterShowVictory", 1f);
	}

	private void LaterShowVictory()
	{
		if (Tools.instance.monstarMag.CanDrowpItem())
		{
			ResManager.inst.LoadPrefab("VictoryPanel").Inst();
		}
		else
		{
			VictoryClick();
		}
	}

	public void VictoryClick()
	{
		Tools.instance.getPlayer().OtherAvatar?.die();
		PanelMamager.CanOpenOrClose = true;
		Tools.canClickFlag = true;
		Tools.instance.loadMapScenes(Tools.instance.FinalScene);
	}
}
