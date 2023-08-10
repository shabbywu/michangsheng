using System.Collections.Generic;
using KBEngine;
using TuPo;
using UnityEngine;
using UnityEngine.UI;

public class ZhuJiManager : MonoBehaviour
{
	private Avatar player;

	public static ZhuJiManager inst;

	public Text ZhuJiJinDu;

	public Text ShengYuHuiHe;

	public List<int> ZhuJiSkillList;

	public int AddHp;

	private void Awake()
	{
		inst = this;
		player = Tools.instance.getPlayer();
		player.ZhuJiJinDu = 0;
		ShengYuHuiHe.text = "剩余回合 九";
	}

	public void updateJinDu()
	{
		ZhuJiJinDu.text = player.ZhuJiJinDu + "/100";
	}

	private void successZhuJi()
	{
		int num = (player.ZhuJiJinDu - 100) * 2;
		GlobalValue.Set(0, 2, "ZhuJiManager.successZhuJi 筑基成功");
		GlobalValue.SetTalk(1, 2, "ZhuJiManager.successZhuJi 筑基成功");
		AddHp = num;
		if (!Tools.instance.CheckHasTianFu(315))
		{
			player._HP_Max += AddHp;
		}
		else
		{
			num += player.HP_Max;
			player._HP_Max += num;
		}
		ResManager.inst.LoadPrefab("BigTuPoResult").Inst().GetComponent<BigTuPoResultIMag>()
			.ShowSuccess(1);
	}

	private void failZhuJi()
	{
		player.exp -= 1000uL;
		GlobalValue.SetTalk(1, 0, "ZhuJiManager.failZhuJi 筑基失败");
		ResManager.inst.LoadPrefab("BigTuPoResult").Inst().GetComponent<BigTuPoResultIMag>()
			.ShowFail(1);
	}

	public void checkState()
	{
		if (player.ZhuJiJinDu >= 100)
		{
			successZhuJi();
		}
		else
		{
			failZhuJi();
		}
	}

	public void quitJieDan()
	{
		Tools.instance.loadMapScenes(Tools.instance.FinalScene);
	}

	private void OnDestroy()
	{
		inst = null;
	}
}
