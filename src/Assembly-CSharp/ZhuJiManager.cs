using System;
using System.Collections.Generic;
using KBEngine;
using TuPo;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000531 RID: 1329
public class ZhuJiManager : MonoBehaviour
{
	// Token: 0x060021F3 RID: 8691 RVA: 0x0001BDC4 File Offset: 0x00019FC4
	private void Awake()
	{
		ZhuJiManager.inst = this;
		this.player = Tools.instance.getPlayer();
		this.player.ZhuJiJinDu = 0;
		this.ShengYuHuiHe.text = "剩余回合 九";
	}

	// Token: 0x060021F4 RID: 8692 RVA: 0x00119B64 File Offset: 0x00117D64
	public void updateJinDu()
	{
		this.ZhuJiJinDu.text = this.player.ZhuJiJinDu.ToString() + "/100";
	}

	// Token: 0x060021F5 RID: 8693 RVA: 0x00119B9C File Offset: 0x00117D9C
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

	// Token: 0x060021F6 RID: 8694 RVA: 0x00119C48 File Offset: 0x00117E48
	private void failZhuJi()
	{
		this.player.exp -= 1000UL;
		GlobalValue.SetTalk(1, 0, "ZhuJiManager.failZhuJi 筑基失败");
		ResManager.inst.LoadPrefab("BigTuPoResult").Inst(null).GetComponent<BigTuPoResultIMag>().ShowFail(1, 0);
	}

	// Token: 0x060021F7 RID: 8695 RVA: 0x0001BDF8 File Offset: 0x00019FF8
	public void checkState()
	{
		if (this.player.ZhuJiJinDu >= 100)
		{
			this.successZhuJi();
			return;
		}
		this.failZhuJi();
	}

	// Token: 0x060021F8 RID: 8696 RVA: 0x0001BE16 File Offset: 0x0001A016
	public void quitJieDan()
	{
		Tools.instance.loadMapScenes(Tools.instance.FinalScene, true);
	}

	// Token: 0x060021F9 RID: 8697 RVA: 0x0001BE2D File Offset: 0x0001A02D
	private void OnDestroy()
	{
		ZhuJiManager.inst = null;
	}

	// Token: 0x04001D5F RID: 7519
	private Avatar player;

	// Token: 0x04001D60 RID: 7520
	public static ZhuJiManager inst;

	// Token: 0x04001D61 RID: 7521
	public Text ZhuJiJinDu;

	// Token: 0x04001D62 RID: 7522
	public Text ShengYuHuiHe;

	// Token: 0x04001D63 RID: 7523
	public List<int> ZhuJiSkillList;

	// Token: 0x04001D64 RID: 7524
	public int AddHp;
}
