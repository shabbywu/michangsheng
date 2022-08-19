using System;
using System.Collections.Generic;
using JSONClass;
using KBEngine;
using UnityEngine;

// Token: 0x02000209 RID: 521
public class DongFuScene : MonoBehaviour
{
	// Token: 0x060014D5 RID: 5333 RVA: 0x00085AF7 File Offset: 0x00083CF7
	private void Awake()
	{
		DongFuScene.Inst = this;
	}

	// Token: 0x060014D6 RID: 5334 RVA: 0x00085AFF File Offset: 0x00083CFF
	private void Start()
	{
		this.RefreshShow();
	}

	// Token: 0x060014D7 RID: 5335 RVA: 0x00085B07 File Offset: 0x00083D07
	private void Update()
	{
		if (UILingTianPanel.Inst != null && UILingTianPanel.Inst.IsShouGe && Input.GetMouseButtonDown(1))
		{
			UILingTianPanel.Inst.IsShouGe = false;
		}
	}

	// Token: 0x060014D8 RID: 5336 RVA: 0x00085B38 File Offset: 0x00083D38
	public void RefreshShow()
	{
		this.DongFu = new DongFuData(DongFuManager.NowDongFuID);
		this.DongFu.Load();
		this.LingYanShow.SpritePath = this.LingYanShowSprites[this.DongFu.LingYanLevel - 1];
		this.LingYanShow.Refresh();
		for (int i = 1; i <= 3; i++)
		{
			this.LingYanShow.transform.GetChild(i - 1).gameObject.SetActive(i == this.DongFu.LingYanLevel);
		}
		this.JuLingZhenShow.SpritePath = this.JuLingZhenShowSprites[this.DongFu.JuLingZhenLevel - 1];
		this.JuLingZhenShow.Refresh();
		if (this.DongFu.AreaUnlock[0] == 0)
		{
			this.LianQiBreak.SetActive(true);
			this.LianQiBuild.SetActive(false);
		}
		else
		{
			this.LianQiBreak.SetActive(false);
			this.LianQiBuild.SetActive(true);
		}
		if (this.DongFu.AreaUnlock[1] == 0)
		{
			this.LianDanBreak.SetActive(true);
			this.LianDanBuild.SetActive(false);
		}
		else
		{
			this.LianDanBreak.SetActive(false);
			this.LianDanBuild.SetActive(true);
		}
		if (this.DongFu.AreaUnlock[2] == 0)
		{
			this.LingTianBreak.SetActive(true);
			this.LingTianBuild.SetActive(false);
			return;
		}
		this.LingTianBreak.SetActive(false);
		this.LingTianBuild.SetActive(true);
		LingTianType lingTianType = LingTianType.未种植;
		for (int j = 0; j < DongFuManager.LingTianCount; j++)
		{
			int id = this.DongFu.LingTian[j].ID;
			int lingLi = this.DongFu.LingTian[j].LingLi;
			if (id > 0)
			{
				_ItemJsonData itemJsonData = _ItemJsonData.DataDict[id];
				int num = lingLi / itemJsonData.price;
				if (lingTianType == LingTianType.未种植)
				{
					if (num > 0)
					{
						lingTianType = LingTianType.有成熟;
						break;
					}
					lingTianType = LingTianType.生长中;
				}
				else if (lingTianType == LingTianType.生长中 && num > 0)
				{
					lingTianType = LingTianType.有成熟;
					break;
				}
			}
		}
		this.SetLingTianShow(lingTianType);
	}

	// Token: 0x060014D9 RID: 5337 RVA: 0x00085D43 File Offset: 0x00083F43
	public void SetLingTianShow(LingTianType type)
	{
		this.LingTianShow.SpritePath = this.LingTianShowSprites[(int)type];
		this.LingTianShow.Refresh();
	}

	// Token: 0x060014DA RID: 5338 RVA: 0x00085D68 File Offset: 0x00083F68
	private void BuyArea(int id, string areaName)
	{
		USelectBox.Show("是否花费10000灵石布置" + areaName + "?", delegate
		{
			Avatar player = PlayerEx.Player;
			if (player.money >= 10000UL)
			{
				player.AddMoney(-10000);
				this.DongFu.Load();
				this.DongFu.AreaUnlock[id] = 1;
				this.DongFu.Save();
				this.RefreshShow();
				return;
			}
			UIPopTip.Inst.Pop("灵石不足", PopTipIconType.叹号);
		}, null);
	}

	// Token: 0x060014DB RID: 5339 RVA: 0x00085DAB File Offset: 0x00083FAB
	public void OnLingYanFuncClick()
	{
		UIDongFu.Inst.ShowJuLingZhenPanel();
	}

	// Token: 0x060014DC RID: 5340 RVA: 0x00085DB7 File Offset: 0x00083FB7
	public void OnBiGuanFuncClick()
	{
		UIBiGuanPanel.Inst.OpenBiGuan(1);
	}

	// Token: 0x060014DD RID: 5341 RVA: 0x00085DC4 File Offset: 0x00083FC4
	public void OnLianQiBreakFuncClick()
	{
		this.BuyArea(0, "炼器室");
	}

	// Token: 0x060014DE RID: 5342 RVA: 0x00085DD2 File Offset: 0x00083FD2
	public void OnLianQiBuildFuncClick()
	{
		PanelMamager.inst.OpenPanel(PanelMamager.PanelType.炼器, 0);
	}

	// Token: 0x060014DF RID: 5343 RVA: 0x00085DE0 File Offset: 0x00083FE0
	public void OnLianDanBreakFuncClick()
	{
		this.BuyArea(1, "炼丹室");
	}

	// Token: 0x060014E0 RID: 5344 RVA: 0x00085DEE File Offset: 0x00083FEE
	public void OnLianDanBuildFuncClick()
	{
		PanelMamager.inst.OpenPanel(PanelMamager.PanelType.炼丹, 0);
	}

	// Token: 0x060014E1 RID: 5345 RVA: 0x00085DFC File Offset: 0x00083FFC
	public void OnLingTianBreakFuncClick()
	{
		this.BuyArea(2, "灵田");
	}

	// Token: 0x060014E2 RID: 5346 RVA: 0x0005E658 File Offset: 0x0005C858
	public void OnLingTianBuildFuncClick()
	{
		UIDongFu.Inst.ShowLingTianPanel();
	}

	// Token: 0x060014E3 RID: 5347 RVA: 0x00085E0A File Offset: 0x0008400A
	public void OnLingYanDecorateClick()
	{
		UIPopTip.Inst.Pop("Todo", PopTipIconType.叹号);
	}

	// Token: 0x060014E4 RID: 5348 RVA: 0x00085E0A File Offset: 0x0008400A
	public void OnBiGuanDecorateClick()
	{
		UIPopTip.Inst.Pop("Todo", PopTipIconType.叹号);
	}

	// Token: 0x060014E5 RID: 5349 RVA: 0x00085DC4 File Offset: 0x00083FC4
	public void OnLianQiBreakDecorateClick()
	{
		this.BuyArea(0, "炼器室");
	}

	// Token: 0x060014E6 RID: 5350 RVA: 0x00085E0A File Offset: 0x0008400A
	public void OnLianQiBuildDecorateClick()
	{
		UIPopTip.Inst.Pop("Todo", PopTipIconType.叹号);
	}

	// Token: 0x060014E7 RID: 5351 RVA: 0x00085DE0 File Offset: 0x00083FE0
	public void OnLianDanBreakDecorateClick()
	{
		this.BuyArea(1, "炼丹室");
	}

	// Token: 0x060014E8 RID: 5352 RVA: 0x00085E0A File Offset: 0x0008400A
	public void OnLianDanBuildDecorateClick()
	{
		UIPopTip.Inst.Pop("Todo", PopTipIconType.叹号);
	}

	// Token: 0x060014E9 RID: 5353 RVA: 0x00085DFC File Offset: 0x00083FFC
	public void OnLingTianBreakDecorateClick()
	{
		this.BuyArea(2, "灵田");
	}

	// Token: 0x060014EA RID: 5354 RVA: 0x00085E0A File Offset: 0x0008400A
	public void OnLingTianBuildDecorateClick()
	{
		UIPopTip.Inst.Pop("Todo", PopTipIconType.叹号);
	}

	// Token: 0x04000F93 RID: 3987
	public static DongFuScene Inst;

	// Token: 0x04000F94 RID: 3988
	public ModSpriteRenderer LingYanShow;

	// Token: 0x04000F95 RID: 3989
	public List<string> LingYanShowSprites;

	// Token: 0x04000F96 RID: 3990
	public ModSpriteRenderer JuLingZhenShow;

	// Token: 0x04000F97 RID: 3991
	public List<string> JuLingZhenShowSprites;

	// Token: 0x04000F98 RID: 3992
	public GameObject LianQiBuild;

	// Token: 0x04000F99 RID: 3993
	public GameObject LianQiBreak;

	// Token: 0x04000F9A RID: 3994
	public GameObject LianDanBuild;

	// Token: 0x04000F9B RID: 3995
	public GameObject LianDanBreak;

	// Token: 0x04000F9C RID: 3996
	public GameObject LingTianBuild;

	// Token: 0x04000F9D RID: 3997
	public GameObject LingTianBreak;

	// Token: 0x04000F9E RID: 3998
	public ModSpriteRenderer LingTianShow;

	// Token: 0x04000F9F RID: 3999
	public List<string> LingTianShowSprites;

	// Token: 0x04000FA0 RID: 4000
	public DFInteractiveMode InteractiveMode;

	// Token: 0x04000FA1 RID: 4001
	private DongFuData DongFu;
}
