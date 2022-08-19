using System;
using System.Collections.Generic;
using KBEngine;
using TuPo;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020003A7 RID: 935
public class ZhuJiManager : MonoBehaviour
{
	// Token: 0x06001E70 RID: 7792 RVA: 0x000D6398 File Offset: 0x000D4598
	private void Awake()
	{
		ZhuJiManager.inst = this;
		this.player = Tools.instance.getPlayer();
		this.player.ZhuJiJinDu = 0;
		this.ShengYuHuiHe.text = "剩余回合 九";
	}

	// Token: 0x06001E71 RID: 7793 RVA: 0x000D63CC File Offset: 0x000D45CC
	public void updateJinDu()
	{
		this.ZhuJiJinDu.text = this.player.ZhuJiJinDu.ToString() + "/100";
	}

	// Token: 0x06001E72 RID: 7794 RVA: 0x000D6404 File Offset: 0x000D4604
	private void successZhuJi()
	{
		int num = (this.player.ZhuJiJinDu - 100) * 2;
		GlobalValue.Set(0, 2, "ZhuJiManager.successZhuJi 筑基成功");
		GlobalValue.SetTalk(1, 2, "ZhuJiManager.successZhuJi 筑基成功");
		this.AddHp = num;
		if (!Tools.instance.CheckHasTianFu(315))
		{
			this.player._HP_Max += this.AddHp;
		}
		else
		{
			num += this.player.HP_Max;
			this.player._HP_Max += num;
		}
		ResManager.inst.LoadPrefab("BigTuPoResult").Inst(null).GetComponent<BigTuPoResultIMag>().ShowSuccess(1);
	}

	// Token: 0x06001E73 RID: 7795 RVA: 0x000D64B0 File Offset: 0x000D46B0
	private void failZhuJi()
	{
		this.player.exp -= 1000UL;
		GlobalValue.SetTalk(1, 0, "ZhuJiManager.failZhuJi 筑基失败");
		ResManager.inst.LoadPrefab("BigTuPoResult").Inst(null).GetComponent<BigTuPoResultIMag>().ShowFail(1, 0);
	}

	// Token: 0x06001E74 RID: 7796 RVA: 0x000D6502 File Offset: 0x000D4702
	public void checkState()
	{
		if (this.player.ZhuJiJinDu >= 100)
		{
			this.successZhuJi();
			return;
		}
		this.failZhuJi();
	}

	// Token: 0x06001E75 RID: 7797 RVA: 0x000D6520 File Offset: 0x000D4720
	public void quitJieDan()
	{
		Tools.instance.loadMapScenes(Tools.instance.FinalScene, true);
	}

	// Token: 0x06001E76 RID: 7798 RVA: 0x000D6537 File Offset: 0x000D4737
	private void OnDestroy()
	{
		ZhuJiManager.inst = null;
	}

	// Token: 0x040018F2 RID: 6386
	private Avatar player;

	// Token: 0x040018F3 RID: 6387
	public static ZhuJiManager inst;

	// Token: 0x040018F4 RID: 6388
	public Text ZhuJiJinDu;

	// Token: 0x040018F5 RID: 6389
	public Text ShengYuHuiHe;

	// Token: 0x040018F6 RID: 6390
	public List<int> ZhuJiSkillList;

	// Token: 0x040018F7 RID: 6391
	public int AddHp;
}
