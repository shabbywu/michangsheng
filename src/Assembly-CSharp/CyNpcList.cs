using System;
using System.Collections.Generic;
using JSONClass;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020003DC RID: 988
public class CyNpcList : MonoBehaviour
{
	// Token: 0x06001B01 RID: 6913 RVA: 0x000EF2EC File Offset: 0x000ED4EC
	public void Init()
	{
		this.friendList = Tools.instance.getPlayer().emailDateMag.cyNpcList;
		this.npcNum.text = this.friendList.Count.ToString();
		this.curSelect.text = "全部";
		this.tagBtn.mouseUp.AddListener(new UnityAction(this.ShowMoreSelect));
		this.InitNpcList(-2);
	}

	// Token: 0x06001B02 RID: 6914 RVA: 0x000EF368 File Offset: 0x000ED568
	private void InitNpcList(int type)
	{
		Tools.ClearObj(this.cyNpcCell.transform);
		this.friendCells = new List<CyFriendCell>();
		this.curSelectFriend = null;
		Dictionary<string, List<EmailData>>.KeyCollection keys = Tools.instance.getPlayer().emailDateMag.newEmailDictionary.Keys;
		List<int> list = new List<int>();
		foreach (string text in keys)
		{
			if ((type != -1 || jsonData.instance.AvatarJsonData[text].TryGetField("IsTag").b) && (type != -3 || (CyTeShuNpc.DataDict.ContainsKey(int.Parse(text)) && CyTeShuNpc.DataDict[int.Parse(text)].Type == 1)) && (type < 0 || jsonData.instance.AvatarJsonData[text]["MenPai"].I == type))
			{
				CyFriendCell component = Tools.InstantiateGameObject(this.cyNpcCell, this.npcCellParent.transform).GetComponent<CyFriendCell>();
				component.Init(int.Parse(text));
				component.redDian.SetActive(true);
				list.Add(int.Parse(text));
				this.friendCells.Add(component);
			}
		}
		for (int i = 0; i < this.friendList.Count; i++)
		{
			int num = this.friendList[i];
			if (!list.Contains(this.friendList[i]) && this.friendList[i] != 0 && (type != -3 || (CyTeShuNpc.DataDict.ContainsKey(num) && CyTeShuNpc.DataDict[num].Type == 1)))
			{
				if (type == -1)
				{
					if (NpcJieSuanManager.inst.IsDeath(num))
					{
						goto IL_2B2;
					}
					jsonData.instance.AvatarJsonData[this.friendList[i].ToString()].TryGetField("IsTag");
					if (!jsonData.instance.AvatarJsonData[this.friendList[i].ToString()].TryGetField("IsTag").b)
					{
						goto IL_2B2;
					}
				}
				else if (type >= 0 && (NpcJieSuanManager.inst.IsDeath(num) || jsonData.instance.AvatarJsonData[this.friendList[i].ToString()].TryGetField("MenPai").I != type))
				{
					goto IL_2B2;
				}
				CyFriendCell component2 = Tools.InstantiateGameObject(this.cyNpcCell, this.npcCellParent.transform).GetComponent<CyFriendCell>();
				component2.Init(this.friendList[i]);
				this.friendCells.Add(component2);
			}
			IL_2B2:;
		}
		this.isShowSelectTag = false;
		this.sanJiao.transform.localRotation.Set(0f, 0f, 180f, 0f);
		CyUIMag.inst.cyEmail.cySendBtn.Hide();
		CyUIMag.inst.cyEmail.Restart();
		this.curSelectFriend = null;
	}

	// Token: 0x06001B03 RID: 6915 RVA: 0x000EF6B0 File Offset: 0x000ED8B0
	public void ShowMoreSelect()
	{
		this.isShowSelectTag = !this.isShowSelectTag;
		if (this.isShowSelectTag)
		{
			if (this.selectPanel.transform.childCount < 2)
			{
				Tools.InstantiateGameObject(this.cySelectCell, this.selectPanel.transform).GetComponent<CySelectCell>().Init("全部", delegate
				{
					this.selectPanel.SetActive(false);
					this.curSelect.text = "全部";
					this.InitNpcList(-2);
				});
				Tools.InstantiateGameObject(this.cySelectCell, this.selectPanel.transform).GetComponent<CySelectCell>().Init("拍卖会", delegate
				{
					this.selectPanel.SetActive(false);
					this.curSelect.text = "拍卖会";
					this.InitNpcList(-3);
				});
				Tools.InstantiateGameObject(this.cySelectCell, this.selectPanel.transform).GetComponent<CySelectCell>().Init("标记", delegate
				{
					this.selectPanel.SetActive(false);
					this.curSelect.text = "标记";
					this.InitNpcList(-1);
				});
				using (List<JSONObject>.Enumerator enumerator = jsonData.instance.CyShiLiNameData.list.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						JSONObject data = enumerator.Current;
						Tools.InstantiateGameObject(this.cySelectCell, this.selectPanel.transform).GetComponent<CySelectCell>().Init(data["name"].Str, delegate
						{
							this.selectPanel.SetActive(false);
							this.curSelect.text = data["name"].Str;
							this.InitNpcList(data["id"].I);
						});
					}
				}
			}
			this.sanJiao.transform.localRotation.Set(0f, 0f, 0f, 0f);
			this.selectPanel.SetActive(true);
			return;
		}
		this.isShowSelectTag = false;
		this.selectPanel.SetActive(false);
		this.sanJiao.transform.localRotation.Set(0f, 0f, 180f, 0f);
	}

	// Token: 0x06001B04 RID: 6916 RVA: 0x00016D91 File Offset: 0x00014F91
	public void ClickCallBack()
	{
		if (this.curSelectFriend != null)
		{
			this.curSelectFriend.isSelect = false;
			this.curSelectFriend.updateState();
		}
	}

	// Token: 0x040016B2 RID: 5810
	public List<Sprite> npcCellSpriteList;

	// Token: 0x040016B3 RID: 5811
	public List<Sprite> tagSpriteList;

	// Token: 0x040016B4 RID: 5812
	public List<int> friendList;

	// Token: 0x040016B5 RID: 5813
	public BtnCell tagBtn;

	// Token: 0x040016B6 RID: 5814
	public GameObject cyNpcCell;

	// Token: 0x040016B7 RID: 5815
	public GameObject cySelectCell;

	// Token: 0x040016B8 RID: 5816
	public GameObject selectPanel;

	// Token: 0x040016B9 RID: 5817
	public GameObject npcCellParent;

	// Token: 0x040016BA RID: 5818
	public Text npcNum;

	// Token: 0x040016BB RID: 5819
	public Text curSelect;

	// Token: 0x040016BC RID: 5820
	public Image sanJiao;

	// Token: 0x040016BD RID: 5821
	public List<CyFriendCell> friendCells;

	// Token: 0x040016BE RID: 5822
	public CyFriendCell curSelectFriend;

	// Token: 0x040016BF RID: 5823
	public bool isShowSelectTag;
}
