using System;
using System.Collections.Generic;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020003CA RID: 970
public class CyEmail : MonoBehaviour
{
	// Token: 0x06001AC8 RID: 6856 RVA: 0x000ECD30 File Offset: 0x000EAF30
	public void Init(int npcId)
	{
		this.curIndex = -1;
		this.ActiveList = new List<CyEmailCell>();
		this.scrollRect.onValueChanged.AddListener(new UnityAction<Vector2>(this.AddMore));
		this.sizeFitter = this.msgParent.gameObject.GetComponent<ContentSizeFitter>();
		this.rectTransform = this.msgParent.gameObject.GetComponent<RectTransform>();
		this.player = Tools.instance.getPlayer();
		this.npcId = npcId;
		this.isDeath = NpcJieSuanManager.inst.IsDeath(npcId);
		Tools.ClearObj(this.msgCell.transform);
		string key = npcId.ToString();
		if (this.player.emailDateMag.newEmailDictionary.ContainsKey(key))
		{
			this.player.emailDateMag.NewToHasRead(key);
		}
		if (this.player.emailDateMag.hasReadEmailDictionary.ContainsKey(key) && this.player.emailDateMag.hasReadEmailDictionary[key].Count > 0)
		{
			List<EmailData> list = this.player.emailDateMag.hasReadEmailDictionary[key];
			int i = list.Count;
			if (i > 5)
			{
				i = 5;
			}
			this.curIndex = list.Count - 1;
			while (i > 0)
			{
				GameObject gameObject = Tools.InstantiateGameObject(this.msgCell, this.msgParent);
				gameObject.transform.SetAsFirstSibling();
				CyEmailCell component = gameObject.GetComponent<CyEmailCell>();
				component.Init(list[this.curIndex], this.isDeath);
				this.ActiveList.Add(component);
				this.curIndex--;
				i--;
			}
			this.UpDateSize();
			this.noEmail.SetActive(false);
			this.msgParent.gameObject.SetActive(true);
		}
		else
		{
			this.noEmail.SetActive(true);
			this.msgParent.gameObject.SetActive(false);
		}
		if (this.isDeath || this.player.emailDateMag.IsStopAll)
		{
			this.cySendBtn.gameObject.SetActive(false);
			return;
		}
		if (!CyTeShuNpc.DataDict.ContainsKey(npcId))
		{
			this.cySendBtn.gameObject.SetActive(true);
			return;
		}
		if (CyTeShuNpc.DataDict[npcId].Type == 2)
		{
			this.cySendBtn.gameObject.SetActive(false);
			return;
		}
		this.cySendBtn.gameObject.SetActive(true);
	}

	// Token: 0x06001AC9 RID: 6857 RVA: 0x000ECF90 File Offset: 0x000EB190
	private void AddMore(Vector2 arg0)
	{
		if (this.scrollRect.verticalNormalizedPosition >= 0.95f && this.curIndex >= 0)
		{
			int i = this.curIndex + 1;
			if (i > 5)
			{
				i = 5;
			}
			while (i > 0)
			{
				GameObject gameObject = Tools.InstantiateGameObject(this.msgCell, this.msgParent);
				gameObject.transform.SetAsFirstSibling();
				CyEmailCell component = gameObject.GetComponent<CyEmailCell>();
				component.Init(this.player.emailDateMag.hasReadEmailDictionary[this.npcId.ToString()][this.curIndex], this.isDeath);
				this.ActiveList.Add(component);
				this.curIndex--;
				i--;
			}
		}
	}

	// Token: 0x06001ACA RID: 6858 RVA: 0x00016BAF File Offset: 0x00014DAF
	public void Restart()
	{
		Tools.ClearObj(this.msgCell.transform);
		this.cySendBtn.gameObject.SetActive(false);
		CyUIMag.inst.No.SetActive(true);
	}

	// Token: 0x06001ACB RID: 6859 RVA: 0x000ED04C File Offset: 0x000EB24C
	public string GetContent(string msg, EmailData emailData)
	{
		if (msg.Contains("{FirstName}") && PlayerEx.IsDaoLv(emailData.npcId))
		{
			msg = msg.Replace("{FirstName}", "");
		}
		msg = msg.ReplaceTalkWord();
		List<int> contentKey = emailData.contentKey;
		if (msg.Contains("{daoyou}"))
		{
			if (PlayerEx.IsDaoLv(emailData.npcId))
			{
				if (this.player.DaoLvChengHu.HasField(emailData.npcId.ToString()))
				{
					msg = msg.Replace("{daoyou}", this.player.DaoLvChengHu[emailData.npcId.ToString()].Str);
				}
				else
				{
					msg = msg.Replace("{daoyou}", this.player.lastName);
				}
			}
			else
			{
				msg = msg.Replace("{daoyou}", emailData.daoYaoStr);
			}
		}
		if (msg.Contains("{xiaoyou}"))
		{
			if (PlayerEx.IsDaoLv(emailData.npcId))
			{
				if (this.player.DaoLvChengHu.HasField(emailData.npcId.ToString()))
				{
					msg = msg.Replace("{xiaoyou}", this.player.DaoLvChengHu[emailData.npcId.ToString()].Str);
				}
				else
				{
					msg = msg.Replace("{xiaoyou}", this.player.lastName);
				}
			}
			else
			{
				msg = msg.Replace("{xiaoyou}", emailData.xiaoYaoStr);
			}
		}
		if (msg.Contains("{DiDian}"))
		{
			msg = msg.Replace("{DiDian}", emailData.sceneName);
		}
		if (msg.Contains("{npcname}"))
		{
			msg = msg.Replace("{npcname}", emailData.npcName);
		}
		if (msg.Contains("{item}"))
		{
			msg = msg.Replace("{item}", jsonData.instance.ItemJsonData[emailData.item[0].ToString()]["name"].Str);
		}
		if (msg.Contains("{DongFuName}"))
		{
			msg = msg.Replace("{DongFuName}", Tools.instance.getPlayer().DongFuData[string.Format("DongFu{0}", emailData.DongFuId)]["DongFuName"].Str);
		}
		msg = msg.ReplaceTalkWord();
		return msg;
	}

	// Token: 0x06001ACC RID: 6860 RVA: 0x00016BE2 File Offset: 0x00014DE2
	public void UpDateSize()
	{
		LayoutRebuilder.ForceRebuildLayoutImmediate(this.rectTransform);
		this.sizeFitter.SetLayoutVertical();
		LayoutRebuilder.ForceRebuildLayoutImmediate(this.rectTransform);
	}

	// Token: 0x06001ACD RID: 6861 RVA: 0x000ED2A8 File Offset: 0x000EB4A8
	public int GetPlayerItemNum(int id)
	{
		int num = 0;
		foreach (ITEM_INFO item_INFO in Tools.instance.getPlayer().itemList.values)
		{
			if (item_INFO.itemId == id)
			{
				num += (int)item_INFO.itemCount;
			}
		}
		return num;
	}

	// Token: 0x06001ACE RID: 6862 RVA: 0x000ED318 File Offset: 0x000EB518
	public void SendMsgCallBack()
	{
		int num = this.player.emailDateMag.hasReadEmailDictionary[this.npcId.ToString()].Count - 2;
		if (num < 0)
		{
			num = 0;
		}
		for (int i = 0; i < 2; i++)
		{
			Tools.InstantiateGameObject(this.msgCell, this.msgParent).GetComponent<CyEmailCell>().Init(this.player.emailDateMag.hasReadEmailDictionary[this.npcId.ToString()][num], this.isDeath);
			num++;
		}
		this.noEmail.SetActive(false);
		this.msgParent.gameObject.SetActive(true);
		this.UpDateSize();
	}

	// Token: 0x06001ACF RID: 6863 RVA: 0x000ED3D0 File Offset: 0x000EB5D0
	public void TiJiaoCallBack()
	{
		int num = this.player.emailDateMag.hasReadEmailDictionary[this.npcId.ToString()].Count - 1;
		if (num < 0)
		{
			num = 0;
		}
		Tools.InstantiateGameObject(this.msgCell, this.msgParent).GetComponent<CyEmailCell>().Init(this.player.emailDateMag.hasReadEmailDictionary[this.npcId.ToString()][num], this.isDeath);
		this.noEmail.SetActive(false);
		this.msgParent.gameObject.SetActive(true);
		this.UpDateSize();
	}

	// Token: 0x06001AD0 RID: 6864 RVA: 0x000ED478 File Offset: 0x000EB678
	public void SendItemBack()
	{
		foreach (CyEmailCell cyEmailCell in this.ActiveList)
		{
			cyEmailCell.UpdateTiJiao();
		}
	}

	// Token: 0x04001643 RID: 5699
	public GameObject noEmail;

	// Token: 0x04001644 RID: 5700
	public GameObject msgCell;

	// Token: 0x04001645 RID: 5701
	public List<Sprite> titleSprites;

	// Token: 0x04001646 RID: 5702
	public List<Color> titleColors;

	// Token: 0x04001647 RID: 5703
	public List<Sprite> btnSprites;

	// Token: 0x04001648 RID: 5704
	public List<Color> numColors;

	// Token: 0x04001649 RID: 5705
	public CySendBtn cySendBtn;

	// Token: 0x0400164A RID: 5706
	public List<CyEmailCell> ActiveList = new List<CyEmailCell>();

	// Token: 0x0400164B RID: 5707
	public ScrollRect scrollRect;

	// Token: 0x0400164C RID: 5708
	public Transform msgParent;

	// Token: 0x0400164D RID: 5709
	public int npcId;

	// Token: 0x0400164E RID: 5710
	public Avatar player;

	// Token: 0x0400164F RID: 5711
	private ContentSizeFitter sizeFitter;

	// Token: 0x04001650 RID: 5712
	private RectTransform rectTransform;

	// Token: 0x04001651 RID: 5713
	public bool isDeath;

	// Token: 0x04001652 RID: 5714
	public int curIndex = -1;
}
