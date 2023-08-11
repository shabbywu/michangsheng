using System;
using Fungus;
using JSONClass;
using UnityEngine;
using UnityEngine.UI;

public class UINPCSVItem : MonoBehaviour, IComparable
{
	public static int RefreshNPCFavorID;

	public static int RefreshNPCTaskID;

	public static UINPCSVItem NowSelectedUINPCSVItem;

	private UINPCData npc;

	public Text NPCTitle;

	public Text NPCName;

	public UINPCHeadFavor NPCFavor;

	public PlayerSetRandomFace NPCHead;

	public Image NPCQuastIcon;

	public GameObject Selected;

	public Sprite SelectedBG;

	public Sprite NormalBG;

	public Image BG;

	public static bool IsExpSort;

	public UINPCData NPCData
	{
		get
		{
			return npc;
		}
		set
		{
			npc = value;
			RefreshUI();
		}
	}

	private void Start()
	{
	}

	private void Update()
	{
		if (NPCData != null && NPCData.ID == RefreshNPCFavorID)
		{
			NPCData.RefreshData();
			NPCFavor.SetFavor(NPCData.Favor);
			RefreshNPCFavorID = -1;
		}
		if (NPCData != null && NPCData.ID == RefreshNPCTaskID)
		{
			NPCData.RefreshData();
			CheckTask();
		}
	}

	public void CheckTask()
	{
		if (UINPCJiaoHu.Inst.TNPCIDList.Contains(npc.ID))
		{
			string s = SceneEx.NowSceneName.Replace("S", "");
			int result = 0;
			if (int.TryParse(s, out result) && result > 0)
			{
				foreach (ThreeSenceJsonData data in ThreeSenceJsonData.DataList)
				{
					if (data.SceneID != result || NPCEx.NPCIDToNew(data.AvatarID) != NPCData.ID)
					{
						continue;
					}
					int count = data.TaskIconValue.Count;
					bool enabled = false;
					for (int i = 0; i < count; i++)
					{
						int id = data.TaskIconValue[i];
						int num = data.TaskIconValueX[i];
						if (GlobalValue.Get(id, "UINPCSVItem.CheckTask 检查是否显示NPC的任务") == num)
						{
							enabled = true;
						}
					}
					((Behaviour)NPCQuastIcon).enabled = enabled;
					break;
				}
			}
		}
		else
		{
			((Behaviour)NPCQuastIcon).enabled = npc.IsTask;
		}
		RefreshNPCTaskID = -1;
	}

	public void RefreshUI()
	{
		npc.RefreshData();
		NPCName.text = npc.Name;
		NPCTitle.text = npc.Title;
		NPCHead.SetNPCFace(npc.ID);
		NPCFavor.SetFavor(npc.Favor);
		CheckTask();
	}

	public void OnClick()
	{
		MenuDialog menuDialog = Object.FindObjectOfType<MenuDialog>();
		if ((Object)(object)menuDialog != (Object)null && ((Component)menuDialog).gameObject.activeInHierarchy)
		{
			return;
		}
		PaiMaiHang paiMaiHang = Object.FindObjectOfType<PaiMaiHang>();
		if (((Object)(object)paiMaiHang != (Object)null && ((Component)paiMaiHang).gameObject.activeInHierarchy) || (Object)(object)FpUIMag.inst != (Object)null)
		{
			return;
		}
		UINPCJiaoHu.Inst.HideJiaoHuPop();
		UINPCJiaoHu.Inst.NowJiaoHuNPC = npc;
		NowSelectedUINPCSVItem = this;
		if (npc.IsGuDingNPC)
		{
			UINPCJiaoHu.Inst.ShowJiaoHuPop();
		}
		else if (npc.IsThreeSceneNPC)
		{
			if (UINPCData.ThreeSceneNPCTalkCache.ContainsKey(npc.ID))
			{
				UINPCData.ThreeSceneNPCTalkCache[npc.ID].Invoke();
			}
			else
			{
				Debug.LogError((object)"点击了3级场景NPC，但是没有对应的Action");
			}
		}
		else
		{
			UINPCJiaoHu.Inst.ShowJiaoHuPop();
		}
	}

	public int CompareTo(object obj)
	{
		if (IsExpSort)
		{
			if (NPCData.Exp < ((UINPCSVItem)obj).NPCData.Exp)
			{
				return 1;
			}
			if (NPCData.Exp == ((UINPCSVItem)obj).NPCData.Exp)
			{
				return 0;
			}
			return -1;
		}
		if (NPCData.Favor < ((UINPCSVItem)obj).NPCData.Favor)
		{
			return 1;
		}
		if (NPCData.Favor == ((UINPCSVItem)obj).NPCData.Favor)
		{
			return 0;
		}
		return -1;
	}
}
