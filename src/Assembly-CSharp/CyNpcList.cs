using System;
using System.Collections.Generic;
using JSONClass;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020002A3 RID: 675
public class CyNpcList : MonoBehaviour
{
	// Token: 0x0600180F RID: 6159 RVA: 0x000A80C0 File Offset: 0x000A62C0
	public void Init()
	{
		this.friendList = Tools.instance.getPlayer().emailDateMag.cyNpcList;
		this.npcNum.text = this.friendList.Count.ToString();
		this.curSelect.text = "全部";
		this.tagBtn.mouseUpEvent.AddListener(new UnityAction(this.ShowMoreSelect));
		this.InitNpcList(-2);
	}

	// Token: 0x06001810 RID: 6160 RVA: 0x000A813C File Offset: 0x000A633C
	private void InitNpcList(int type)
	{
		Tools.ClearObj(this.cyNpcCell.transform);
		this.friendCells = new List<CyFriendCell>();
		this.curSelectFriend = null;
		Dictionary<string, List<EmailData>>.KeyCollection keys = Tools.instance.getPlayer().emailDateMag.newEmailDictionary.Keys;
		List<int> list = new List<int>();
		foreach (string text in keys)
		{
			if ((type != -1 || jsonData.instance.AvatarJsonData[text].TryGetField("IsTag").b) && (type != -4 || PlayerEx.IsDaoLv(int.Parse(text))) && (type != -3 || (CyTeShuNpc.DataDict.ContainsKey(int.Parse(text)) && CyTeShuNpc.DataDict[int.Parse(text)].Type == 1)) && (type < 0 || jsonData.instance.AvatarJsonData[text]["MenPai"].I == type))
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
			if (!list.Contains(this.friendList[i]) && this.friendList[i] != 0 && (type != -4 || PlayerEx.IsDaoLv(num)) && (type != -3 || (CyTeShuNpc.DataDict.ContainsKey(num) && CyTeShuNpc.DataDict[num].Type == 1)))
			{
				if (type == -1)
				{
					if (NpcJieSuanManager.inst.IsDeath(num))
					{
						goto IL_2D7;
					}
					jsonData.instance.AvatarJsonData[this.friendList[i].ToString()].TryGetField("IsTag");
					if (!jsonData.instance.AvatarJsonData[this.friendList[i].ToString()].TryGetField("IsTag").b)
					{
						goto IL_2D7;
					}
				}
				else if (type >= 0 && (NpcJieSuanManager.inst.IsDeath(num) || jsonData.instance.AvatarJsonData[this.friendList[i].ToString()].TryGetField("MenPai").I != type))
				{
					goto IL_2D7;
				}
				CyFriendCell component2 = Tools.InstantiateGameObject(this.cyNpcCell, this.npcCellParent.transform).GetComponent<CyFriendCell>();
				component2.Init(this.friendList[i]);
				this.friendCells.Add(component2);
			}
			IL_2D7:;
		}
		this.isShowSelectTag = false;
		this.sanJiao.transform.localRotation.Set(0f, 0f, 180f, 0f);
		CyUIMag.inst.cyEmail.cySendBtn.Hide();
		CyUIMag.inst.cyEmail.Restart();
		this.curSelectFriend = null;
	}

	// Token: 0x06001811 RID: 6161 RVA: 0x000A84B4 File Offset: 0x000A66B4
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
				Tools.InstantiateGameObject(this.cySelectCell, this.selectPanel.transform).GetComponent<CySelectCell>().Init("道侣", delegate
				{
					this.selectPanel.SetActive(false);
					this.curSelect.text = "道侣";
					this.InitNpcList(-4);
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

	// Token: 0x06001812 RID: 6162 RVA: 0x000A86C8 File Offset: 0x000A68C8
	public void ClickCallBack()
	{
		if (this.curSelectFriend != null)
		{
			this.curSelectFriend.isSelect = false;
			this.curSelectFriend.updateState();
		}
	}

	// Token: 0x04001316 RID: 4886
	public List<Sprite> npcCellSpriteList;

	// Token: 0x04001317 RID: 4887
	public List<Sprite> tagSpriteList;

	// Token: 0x04001318 RID: 4888
	public List<int> friendList;

	// Token: 0x04001319 RID: 4889
	public FpBtn tagBtn;

	// Token: 0x0400131A RID: 4890
	public GameObject cyNpcCell;

	// Token: 0x0400131B RID: 4891
	public GameObject cySelectCell;

	// Token: 0x0400131C RID: 4892
	public GameObject selectPanel;

	// Token: 0x0400131D RID: 4893
	public GameObject npcCellParent;

	// Token: 0x0400131E RID: 4894
	public Text npcNum;

	// Token: 0x0400131F RID: 4895
	public Text curSelect;

	// Token: 0x04001320 RID: 4896
	public Image sanJiao;

	// Token: 0x04001321 RID: 4897
	public List<CyFriendCell> friendCells;

	// Token: 0x04001322 RID: 4898
	public CyFriendCell curSelectFriend;

	// Token: 0x04001323 RID: 4899
	public bool isShowSelectTag;
}
