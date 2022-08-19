using System;
using System.Collections.Generic;
using Fungus;
using JSONClass;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200027B RID: 635
public class UINPCSVItem : MonoBehaviour, IComparable
{
	// Token: 0x1700023D RID: 573
	// (get) Token: 0x0600171B RID: 5915 RVA: 0x0009DC8F File Offset: 0x0009BE8F
	// (set) Token: 0x0600171C RID: 5916 RVA: 0x0009DC97 File Offset: 0x0009BE97
	public UINPCData NPCData
	{
		get
		{
			return this.npc;
		}
		set
		{
			this.npc = value;
			this.RefreshUI();
		}
	}

	// Token: 0x0600171D RID: 5917 RVA: 0x00004095 File Offset: 0x00002295
	private void Start()
	{
	}

	// Token: 0x0600171E RID: 5918 RVA: 0x0009DCA8 File Offset: 0x0009BEA8
	private void Update()
	{
		if (this.NPCData != null && this.NPCData.ID == UINPCSVItem.RefreshNPCFavorID)
		{
			this.NPCData.RefreshData();
			this.NPCFavor.SetFavor(this.NPCData.Favor);
			UINPCSVItem.RefreshNPCFavorID = -1;
		}
		if (this.NPCData != null && this.NPCData.ID == UINPCSVItem.RefreshNPCTaskID)
		{
			this.NPCData.RefreshData();
			this.CheckTask();
		}
	}

	// Token: 0x0600171F RID: 5919 RVA: 0x0009DD24 File Offset: 0x0009BF24
	public void CheckTask()
	{
		if (UINPCJiaoHu.Inst.TNPCIDList.Contains(this.npc.ID))
		{
			string s = SceneEx.NowSceneName.Replace("S", "");
			int num = 0;
			if (!int.TryParse(s, out num) || num <= 0)
			{
				goto IL_10E;
			}
			using (List<ThreeSenceJsonData>.Enumerator enumerator = ThreeSenceJsonData.DataList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					ThreeSenceJsonData threeSenceJsonData = enumerator.Current;
					if (threeSenceJsonData.SceneID == num && NPCEx.NPCIDToNew(threeSenceJsonData.AvatarID) == this.NPCData.ID)
					{
						int count = threeSenceJsonData.TaskIconValue.Count;
						bool enabled = false;
						for (int i = 0; i < count; i++)
						{
							int id = threeSenceJsonData.TaskIconValue[i];
							int num2 = threeSenceJsonData.TaskIconValueX[i];
							if (GlobalValue.Get(id, "UINPCSVItem.CheckTask 检查是否显示NPC的任务") == num2)
							{
								enabled = true;
							}
						}
						this.NPCQuastIcon.enabled = enabled;
						break;
					}
				}
				goto IL_10E;
			}
		}
		this.NPCQuastIcon.enabled = this.npc.IsTask;
		IL_10E:
		UINPCSVItem.RefreshNPCTaskID = -1;
	}

	// Token: 0x06001720 RID: 5920 RVA: 0x0009DE58 File Offset: 0x0009C058
	public void RefreshUI()
	{
		this.npc.RefreshData();
		this.NPCName.text = this.npc.Name;
		this.NPCTitle.text = this.npc.Title;
		this.NPCHead.SetNPCFace(this.npc.ID);
		this.NPCFavor.SetFavor(this.npc.Favor);
		this.CheckTask();
	}

	// Token: 0x06001721 RID: 5921 RVA: 0x0009DED0 File Offset: 0x0009C0D0
	public void OnClick()
	{
		MenuDialog menuDialog = Object.FindObjectOfType<MenuDialog>();
		if (menuDialog != null && menuDialog.gameObject.activeInHierarchy)
		{
			return;
		}
		PaiMaiHang paiMaiHang = Object.FindObjectOfType<PaiMaiHang>();
		if (paiMaiHang != null && paiMaiHang.gameObject.activeInHierarchy)
		{
			return;
		}
		if (FpUIMag.inst != null)
		{
			return;
		}
		UINPCJiaoHu.Inst.HideJiaoHuPop();
		UINPCJiaoHu.Inst.NowJiaoHuNPC = this.npc;
		UINPCSVItem.NowSelectedUINPCSVItem = this;
		if (this.npc.IsGuDingNPC)
		{
			UINPCJiaoHu.Inst.ShowJiaoHuPop();
			return;
		}
		if (!this.npc.IsThreeSceneNPC)
		{
			UINPCJiaoHu.Inst.ShowJiaoHuPop();
			return;
		}
		if (UINPCData.ThreeSceneNPCTalkCache.ContainsKey(this.npc.ID))
		{
			UINPCData.ThreeSceneNPCTalkCache[this.npc.ID].Invoke();
			return;
		}
		Debug.LogError("点击了3级场景NPC，但是没有对应的Action");
	}

	// Token: 0x06001722 RID: 5922 RVA: 0x0009DFB4 File Offset: 0x0009C1B4
	public int CompareTo(object obj)
	{
		if (UINPCSVItem.IsExpSort)
		{
			if (this.NPCData.Exp < ((UINPCSVItem)obj).NPCData.Exp)
			{
				return 1;
			}
			if (this.NPCData.Exp == ((UINPCSVItem)obj).NPCData.Exp)
			{
				return 0;
			}
			return -1;
		}
		else
		{
			if (this.NPCData.Favor < ((UINPCSVItem)obj).NPCData.Favor)
			{
				return 1;
			}
			if (this.NPCData.Favor == ((UINPCSVItem)obj).NPCData.Favor)
			{
				return 0;
			}
			return -1;
		}
	}

	// Token: 0x040011C3 RID: 4547
	public static int RefreshNPCFavorID;

	// Token: 0x040011C4 RID: 4548
	public static int RefreshNPCTaskID;

	// Token: 0x040011C5 RID: 4549
	public static UINPCSVItem NowSelectedUINPCSVItem;

	// Token: 0x040011C6 RID: 4550
	private UINPCData npc;

	// Token: 0x040011C7 RID: 4551
	public Text NPCTitle;

	// Token: 0x040011C8 RID: 4552
	public Text NPCName;

	// Token: 0x040011C9 RID: 4553
	public UINPCHeadFavor NPCFavor;

	// Token: 0x040011CA RID: 4554
	public PlayerSetRandomFace NPCHead;

	// Token: 0x040011CB RID: 4555
	public Image NPCQuastIcon;

	// Token: 0x040011CC RID: 4556
	public GameObject Selected;

	// Token: 0x040011CD RID: 4557
	public Sprite SelectedBG;

	// Token: 0x040011CE RID: 4558
	public Sprite NormalBG;

	// Token: 0x040011CF RID: 4559
	public Image BG;

	// Token: 0x040011D0 RID: 4560
	public static bool IsExpSort;
}
