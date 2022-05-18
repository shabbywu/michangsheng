using System;
using System.Collections.Generic;
using JSONClass;
using KBEngine;
using UnityEngine;

// Token: 0x0200031E RID: 798
public class DongFuScene : MonoBehaviour
{
	// Token: 0x0600177F RID: 6015 RVA: 0x00014C81 File Offset: 0x00012E81
	private void Awake()
	{
		DongFuScene.Inst = this;
	}

	// Token: 0x06001780 RID: 6016 RVA: 0x00014C89 File Offset: 0x00012E89
	private void Start()
	{
		this.RefreshShow();
	}

	// Token: 0x06001781 RID: 6017 RVA: 0x00014C91 File Offset: 0x00012E91
	private void Update()
	{
		if (UILingTianPanel.Inst != null && UILingTianPanel.Inst.IsShouGe && Input.GetMouseButtonDown(1))
		{
			UILingTianPanel.Inst.IsShouGe = false;
		}
	}

	// Token: 0x06001782 RID: 6018 RVA: 0x000CE360 File Offset: 0x000CC560
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

	// Token: 0x06001783 RID: 6019 RVA: 0x00014CBF File Offset: 0x00012EBF
	public void SetLingTianShow(LingTianType type)
	{
		this.LingTianShow.SpritePath = this.LingTianShowSprites[(int)type];
		this.LingTianShow.Refresh();
	}

	// Token: 0x06001784 RID: 6020 RVA: 0x000CE56C File Offset: 0x000CC76C
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

	// Token: 0x06001785 RID: 6021 RVA: 0x00014CE3 File Offset: 0x00012EE3
	public void OnLingYanFuncClick()
	{
		UIDongFu.Inst.ShowJuLingZhenPanel();
	}

	// Token: 0x06001786 RID: 6022 RVA: 0x00014CEF File Offset: 0x00012EEF
	public void OnBiGuanFuncClick()
	{
		UIBiGuanPanel.Inst.OpenBiGuan(1);
	}

	// Token: 0x06001787 RID: 6023 RVA: 0x00014CFC File Offset: 0x00012EFC
	public void OnLianQiBreakFuncClick()
	{
		this.BuyArea(0, "炼器室");
	}

	// Token: 0x06001788 RID: 6024 RVA: 0x00014D0A File Offset: 0x00012F0A
	public void OnLianQiBuildFuncClick()
	{
		PanelMamager.inst.OpenPanel(PanelMamager.PanelType.炼器, 0);
	}

	// Token: 0x06001789 RID: 6025 RVA: 0x00014D18 File Offset: 0x00012F18
	public void OnLianDanBreakFuncClick()
	{
		this.BuyArea(1, "炼丹室");
	}

	// Token: 0x0600178A RID: 6026 RVA: 0x00014D26 File Offset: 0x00012F26
	public void OnLianDanBuildFuncClick()
	{
		PanelMamager.inst.OpenPanel(PanelMamager.PanelType.炼丹, 0);
	}

	// Token: 0x0600178B RID: 6027 RVA: 0x00014D34 File Offset: 0x00012F34
	public void OnLingTianBreakFuncClick()
	{
		this.BuyArea(2, "灵田");
	}

	// Token: 0x0600178C RID: 6028 RVA: 0x000114FD File Offset: 0x0000F6FD
	public void OnLingTianBuildFuncClick()
	{
		UIDongFu.Inst.ShowLingTianPanel();
	}

	// Token: 0x0600178D RID: 6029 RVA: 0x00014D42 File Offset: 0x00012F42
	public void OnLingYanDecorateClick()
	{
		UIPopTip.Inst.Pop("Todo", PopTipIconType.叹号);
	}

	// Token: 0x0600178E RID: 6030 RVA: 0x00014D42 File Offset: 0x00012F42
	public void OnBiGuanDecorateClick()
	{
		UIPopTip.Inst.Pop("Todo", PopTipIconType.叹号);
	}

	// Token: 0x0600178F RID: 6031 RVA: 0x00014CFC File Offset: 0x00012EFC
	public void OnLianQiBreakDecorateClick()
	{
		this.BuyArea(0, "炼器室");
	}

	// Token: 0x06001790 RID: 6032 RVA: 0x00014D42 File Offset: 0x00012F42
	public void OnLianQiBuildDecorateClick()
	{
		UIPopTip.Inst.Pop("Todo", PopTipIconType.叹号);
	}

	// Token: 0x06001791 RID: 6033 RVA: 0x00014D18 File Offset: 0x00012F18
	public void OnLianDanBreakDecorateClick()
	{
		this.BuyArea(1, "炼丹室");
	}

	// Token: 0x06001792 RID: 6034 RVA: 0x00014D42 File Offset: 0x00012F42
	public void OnLianDanBuildDecorateClick()
	{
		UIPopTip.Inst.Pop("Todo", PopTipIconType.叹号);
	}

	// Token: 0x06001793 RID: 6035 RVA: 0x00014D34 File Offset: 0x00012F34
	public void OnLingTianBreakDecorateClick()
	{
		this.BuyArea(2, "灵田");
	}

	// Token: 0x06001794 RID: 6036 RVA: 0x00014D42 File Offset: 0x00012F42
	public void OnLingTianBuildDecorateClick()
	{
		UIPopTip.Inst.Pop("Todo", PopTipIconType.叹号);
	}

	// Token: 0x040012D9 RID: 4825
	public static DongFuScene Inst;

	// Token: 0x040012DA RID: 4826
	public ModSpriteRenderer LingYanShow;

	// Token: 0x040012DB RID: 4827
	public List<string> LingYanShowSprites;

	// Token: 0x040012DC RID: 4828
	public ModSpriteRenderer JuLingZhenShow;

	// Token: 0x040012DD RID: 4829
	public List<string> JuLingZhenShowSprites;

	// Token: 0x040012DE RID: 4830
	public GameObject LianQiBuild;

	// Token: 0x040012DF RID: 4831
	public GameObject LianQiBreak;

	// Token: 0x040012E0 RID: 4832
	public GameObject LianDanBuild;

	// Token: 0x040012E1 RID: 4833
	public GameObject LianDanBreak;

	// Token: 0x040012E2 RID: 4834
	public GameObject LingTianBuild;

	// Token: 0x040012E3 RID: 4835
	public GameObject LingTianBreak;

	// Token: 0x040012E4 RID: 4836
	public ModSpriteRenderer LingTianShow;

	// Token: 0x040012E5 RID: 4837
	public List<string> LingTianShowSprites;

	// Token: 0x040012E6 RID: 4838
	public DFInteractiveMode InteractiveMode;

	// Token: 0x040012E7 RID: 4839
	private DongFuData DongFu;
}
