using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000389 RID: 905
public class UINPCInfoPanel : MonoBehaviour, IESCClose
{
	// Token: 0x06001952 RID: 6482 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x06001953 RID: 6483 RVA: 0x00015AF7 File Offset: 0x00013CF7
	private void Update()
	{
		this.AutoHide();
	}

	// Token: 0x06001954 RID: 6484 RVA: 0x000E2284 File Offset: 0x000E0484
	public bool CanShow()
	{
		return !UINPCJiaoHu.AllShouldHide && (!(PanelMamager.inst != null) || !(PanelMamager.inst.UISceneGameObject == null)) && (!(PanelMamager.inst != null) || PanelMamager.inst.nowPanel == PanelMamager.PanelType.空);
	}

	// Token: 0x06001955 RID: 6485 RVA: 0x00015AFF File Offset: 0x00013CFF
	private void AutoHide()
	{
		if (!this.CanShow())
		{
			UINPCJiaoHu.Inst.HideNPCInfoPanel();
		}
	}

	// Token: 0x06001956 RID: 6486 RVA: 0x000E22D8 File Offset: 0x000E04D8
	public void RefreshUI(UINPCData data = null)
	{
		if (data == null)
		{
			this.npc = UINPCJiaoHu.Inst.NowJiaoHuNPC;
		}
		else
		{
			this.npc = data;
		}
		this.Face.SetNPCFace(this.npc.ID);
		this.NPCName.text = this.npc.Name;
		if (this.npc.IsFight)
		{
			this.SetFightInfo();
		}
		else
		{
			this.SetNPCInfo();
		}
		this.TabGroup.SetFirstTab();
	}

	// Token: 0x06001957 RID: 6487 RVA: 0x000E2354 File Offset: 0x000E0554
	public void SetNPCInfo()
	{
		this.ShuXing.SetActive(true);
		this.FightShuXing.SetActive(false);
		this.Age.text = this.npc.Age.ToString();
		this.HP.text = this.npc.HP.ToString();
		if (this.npc.Favor >= 200)
		{
			this.QingFen.text = string.Format("{0} (满)", this.npc.Favor);
		}
		else
		{
			this.QingFen.text = string.Format("{0} ({1}%)", this.npc.Favor, (int)(this.npc.FavorPer * 100f));
		}
		this.XiuWei.text = this.npc.LevelStr;
		this.ZhuangTai.text = this.npc.ZhuangTaiStr;
		this.ShouYuan.text = this.npc.ShouYuan.ToString();
		this.ZiZhi.text = this.npc.ZiZhi.ToString();
		this.WuXing.text = this.npc.WuXing.ToString();
		this.DunSu.text = this.npc.DunSu.ToString();
		this.ShenShi.text = this.npc.ShenShi.ToString();
	}

	// Token: 0x06001958 RID: 6488 RVA: 0x000E24D8 File Offset: 0x000E06D8
	public void SetFightInfo()
	{
		this.ShuXing.SetActive(false);
		this.FightShuXing.SetActive(true);
		this.FightHP.text = this.npc.HP.ToString();
		this.FightXiuWei.text = this.npc.LevelStr;
		this.FightDunSu.text = this.npc.DunSu.ToString();
		this.FightShenShi.text = this.npc.ShenShi.ToString();
	}

	// Token: 0x06001959 RID: 6489 RVA: 0x00015B13 File Offset: 0x00013D13
	public bool TryEscClose()
	{
		UINPCJiaoHu.Inst.HideNPCInfoPanel();
		return true;
	}

	// Token: 0x04001467 RID: 5223
	[HideInInspector]
	public UINPCData npc;

	// Token: 0x04001468 RID: 5224
	public PlayerSetRandomFace Face;

	// Token: 0x04001469 RID: 5225
	public TabGroup TabGroup;

	// Token: 0x0400146A RID: 5226
	public GameObject ShuXing;

	// Token: 0x0400146B RID: 5227
	public Text NPCName;

	// Token: 0x0400146C RID: 5228
	public Text Age;

	// Token: 0x0400146D RID: 5229
	public Text HP;

	// Token: 0x0400146E RID: 5230
	public Text QingFen;

	// Token: 0x0400146F RID: 5231
	public Text XiuWei;

	// Token: 0x04001470 RID: 5232
	public Text ZhuangTai;

	// Token: 0x04001471 RID: 5233
	public Text ShouYuan;

	// Token: 0x04001472 RID: 5234
	public Text ZiZhi;

	// Token: 0x04001473 RID: 5235
	public Text WuXing;

	// Token: 0x04001474 RID: 5236
	public Text DunSu;

	// Token: 0x04001475 RID: 5237
	public Text ShenShi;

	// Token: 0x04001476 RID: 5238
	public GameObject FightShuXing;

	// Token: 0x04001477 RID: 5239
	public Text FightHP;

	// Token: 0x04001478 RID: 5240
	public Text FightXiuWei;

	// Token: 0x04001479 RID: 5241
	public Text FightDunSu;

	// Token: 0x0400147A RID: 5242
	public Text FightShenShi;
}
