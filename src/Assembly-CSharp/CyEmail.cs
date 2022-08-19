using System;
using System.Collections.Generic;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x0200029A RID: 666
public class CyEmail : MonoBehaviour
{
	// Token: 0x060017E8 RID: 6120 RVA: 0x000A5EE8 File Offset: 0x000A40E8
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

	// Token: 0x060017E9 RID: 6121 RVA: 0x000A6148 File Offset: 0x000A4348
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

	// Token: 0x060017EA RID: 6122 RVA: 0x000A6202 File Offset: 0x000A4402
	public void Restart()
	{
		Tools.ClearObj(this.msgCell.transform);
		this.cySendBtn.gameObject.SetActive(false);
		CyUIMag.inst.No.SetActive(true);
	}

	// Token: 0x060017EB RID: 6123 RVA: 0x000A6238 File Offset: 0x000A4438
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

	// Token: 0x060017EC RID: 6124 RVA: 0x000A6493 File Offset: 0x000A4693
	public void UpDateSize()
	{
		LayoutRebuilder.ForceRebuildLayoutImmediate(this.rectTransform);
		this.sizeFitter.SetLayoutVertical();
		LayoutRebuilder.ForceRebuildLayoutImmediate(this.rectTransform);
	}

	// Token: 0x060017ED RID: 6125 RVA: 0x000A64B8 File Offset: 0x000A46B8
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

	// Token: 0x060017EE RID: 6126 RVA: 0x000A6528 File Offset: 0x000A4728
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

	// Token: 0x060017EF RID: 6127 RVA: 0x000A65E0 File Offset: 0x000A47E0
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

	// Token: 0x060017F0 RID: 6128 RVA: 0x000A6688 File Offset: 0x000A4888
	public void SendItemBack()
	{
		foreach (CyEmailCell cyEmailCell in this.ActiveList)
		{
			cyEmailCell.UpdateTiJiao();
		}
	}

	// Token: 0x040012BA RID: 4794
	public GameObject noEmail;

	// Token: 0x040012BB RID: 4795
	public GameObject msgCell;

	// Token: 0x040012BC RID: 4796
	public List<Sprite> titleSprites;

	// Token: 0x040012BD RID: 4797
	public List<Color> titleColors;

	// Token: 0x040012BE RID: 4798
	public List<Sprite> btnSprites;

	// Token: 0x040012BF RID: 4799
	public List<Color> numColors;

	// Token: 0x040012C0 RID: 4800
	public CySendBtn cySendBtn;

	// Token: 0x040012C1 RID: 4801
	public List<CyEmailCell> ActiveList = new List<CyEmailCell>();

	// Token: 0x040012C2 RID: 4802
	public ScrollRect scrollRect;

	// Token: 0x040012C3 RID: 4803
	public Transform msgParent;

	// Token: 0x040012C4 RID: 4804
	public int npcId;

	// Token: 0x040012C5 RID: 4805
	public Avatar player;

	// Token: 0x040012C6 RID: 4806
	private ContentSizeFitter sizeFitter;

	// Token: 0x040012C7 RID: 4807
	private RectTransform rectTransform;

	// Token: 0x040012C8 RID: 4808
	public bool isDeath;

	// Token: 0x040012C9 RID: 4809
	public int curIndex = -1;
}
