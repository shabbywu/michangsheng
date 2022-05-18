using System;
using System.Collections.Generic;
using Fungus;
using JSONClass;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020003A2 RID: 930
public class UINPCSVItem : MonoBehaviour, IComparable
{
	// Token: 0x17000285 RID: 645
	// (get) Token: 0x060019EB RID: 6635 RVA: 0x0001647A File Offset: 0x0001467A
	// (set) Token: 0x060019EC RID: 6636 RVA: 0x00016482 File Offset: 0x00014682
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

	// Token: 0x060019ED RID: 6637 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x060019EE RID: 6638 RVA: 0x000E5588 File Offset: 0x000E3788
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

	// Token: 0x060019EF RID: 6639 RVA: 0x000E5604 File Offset: 0x000E3804
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

	// Token: 0x060019F0 RID: 6640 RVA: 0x000E5738 File Offset: 0x000E3938
	public void RefreshUI()
	{
		this.npc.RefreshData();
		this.NPCName.text = this.npc.Name;
		this.NPCTitle.text = this.npc.Title;
		this.NPCHead.SetNPCFace(this.npc.ID);
		this.NPCFavor.SetFavor(this.npc.Favor);
		this.CheckTask();
	}

	// Token: 0x060019F1 RID: 6641 RVA: 0x000E57B0 File Offset: 0x000E39B0
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

	// Token: 0x060019F2 RID: 6642 RVA: 0x000E5894 File Offset: 0x000E3A94
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

	// Token: 0x04001536 RID: 5430
	public static int RefreshNPCFavorID;

	// Token: 0x04001537 RID: 5431
	public static int RefreshNPCTaskID;

	// Token: 0x04001538 RID: 5432
	public static UINPCSVItem NowSelectedUINPCSVItem;

	// Token: 0x04001539 RID: 5433
	private UINPCData npc;

	// Token: 0x0400153A RID: 5434
	public Text NPCTitle;

	// Token: 0x0400153B RID: 5435
	public Text NPCName;

	// Token: 0x0400153C RID: 5436
	public UINPCHeadFavor NPCFavor;

	// Token: 0x0400153D RID: 5437
	public PlayerSetRandomFace NPCHead;

	// Token: 0x0400153E RID: 5438
	public Image NPCQuastIcon;

	// Token: 0x0400153F RID: 5439
	public GameObject Selected;

	// Token: 0x04001540 RID: 5440
	public Sprite SelectedBG;

	// Token: 0x04001541 RID: 5441
	public Sprite NormalBG;

	// Token: 0x04001542 RID: 5442
	public Image BG;

	// Token: 0x04001543 RID: 5443
	public static bool IsExpSort;
}
