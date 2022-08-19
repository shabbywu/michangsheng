using System;
using JSONClass;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200029B RID: 667
public class CyEmailCell : MonoBehaviour
{
	// Token: 0x060017F2 RID: 6130 RVA: 0x000A66F4 File Offset: 0x000A48F4
	public void Init(EmailData emailData, bool isDeath)
	{
		this.IsTiJiaoEmail = false;
		Tools.instance.getPlayer();
		this.emailData = emailData;
		this.sendTime.text = emailData.TimeToString();
		this.item.gameObject.transform.parent.gameObject.SetActive(false);
		this.submitBtn.gameObject.SetActive(false);
		if (emailData.isPlayer)
		{
			this.sendName.text = Tools.instance.getPlayer().name;
			this.sendTime.color = CyUIMag.inst.cyEmail.titleColors[1];
			this.sendName.color = CyUIMag.inst.cyEmail.titleColors[1];
			this.bg.sprite = CyUIMag.inst.cyEmail.titleSprites[1];
			string text = CyPlayeQuestionData.DataDict[emailData.questionId].WenTi;
			if (text.Contains("{DongFuName}"))
			{
				text = text.Replace("{DongFuName}", Tools.instance.getPlayer().DongFuData[string.Format("DongFu{0}", emailData.DongFuId)]["DongFuName"].Str);
			}
			this.content.text = text;
			return;
		}
		if (emailData.isOld)
		{
			JSONObject ChuanYingData = Tools.instance.getPlayer().NewChuanYingList[emailData.oldId.ToString()];
			if (emailData.oldId >= 2082912)
			{
				this.content.SetText("你在天机阁交易会上寄换的宝物已经成功交易，此物应当已随传音符附带的储物袋送于道友手中。");
				this.sendName.text = jsonData.instance.AvatarJsonData[912.ToString()]["Name"].Str;
			}
			else
			{
				this.content.SetText(CyUIMag.inst.cyEmail.GetContent(ChuanYingData["info"].Str, emailData));
				this.sendName.text = ChuanYingData["AvatarName"].Str;
				if (ChuanYingData["CanCaoZuo"].b)
				{
					ChuanYingData.SetField("CanCaoZuo", false);
					if (ChuanYingData.HasField("TaskID"))
					{
						int i = ChuanYingData["TaskID"].I;
						if (!Tools.instance.getPlayer().taskMag.isHasTask(i))
						{
							Tools.instance.getPlayer().taskMag.addTask(i);
							string name = TaskJsonData.DataDict[i].Name;
							string msg = (TaskJsonData.DataDict[i].Type == 0) ? "获得一条新的传闻" : ("<color=#FF0000>" + name + "</color>任务已开启");
							UIPopTip.Inst.Pop(msg, PopTipIconType.任务进度);
						}
					}
					if (ChuanYingData.HasField("WeiTuo"))
					{
						int i2 = ChuanYingData["WeiTuo"].I;
						if (!Tools.instance.getPlayer().nomelTaskMag.IsNTaskStart(i2))
						{
							Tools.instance.getPlayer().nomelTaskMag.StartNTask(i2, 0);
							UIPopTip.Inst.Pop("获得一条新的委托任务", PopTipIconType.任务进度);
						}
					}
					if (ChuanYingData.HasField("TaskIndex"))
					{
						int i3 = ChuanYingData["TaskIndex"][0].I;
						int i4 = ChuanYingData["TaskIndex"][1].I;
						Tools.instance.getPlayer().taskMag.setTaskIndex(i3, i4);
						string name2 = TaskJsonData.DataDict[i3].Name;
						UIPopTip.Inst.Pop("<color=#FF0000> " + name2 + " </color> 进度已更新", PopTipIconType.任务进度);
					}
					if (ChuanYingData.HasField("valueID"))
					{
						for (int j = 0; j < ChuanYingData["valueID"].Count; j++)
						{
							GlobalValue.Set(ChuanYingData["valueID"][j].I, ChuanYingData["value"][j].I, "CyEmailCell.Init");
						}
					}
				}
			}
			this.sendTime.color = CyUIMag.inst.cyEmail.titleColors[0];
			this.sendName.color = CyUIMag.inst.cyEmail.titleColors[0];
			this.bg.sprite = CyUIMag.inst.cyEmail.titleSprites[0];
			if (ChuanYingData.HasField("ItemID") && ChuanYingData["ItemID"].I > 0)
			{
				this.item.Init();
				this.item.SetItem(ChuanYingData["ItemID"].I);
				if (ChuanYingData["ItemHasGet"].b)
				{
					this.submitBtn.gameObject.SetActive(false);
					this.item.ShowHasGet();
					return;
				}
				this.submitBtn.Init(CyUIMag.inst.cyEmail.btnSprites[0], "领取", delegate
				{
					this.submitBtn.gameObject.SetActive(false);
					ChuanYingData.SetField("ItemHasGet", true);
					Tools.instance.getPlayer().addItem(ChuanYingData["ItemID"].I, 1, Tools.CreateItemSeid(ChuanYingData["ItemID"].I), false);
					this.item.ShowHasGet();
					this.UpdateSize();
				});
				return;
			}
		}
		else
		{
			if (isDeath)
			{
				this.sendName.text = NpcJieSuanManager.inst.npcDeath.npcDeathJson[emailData.npcId.ToString()]["deathName"].Str;
			}
			else
			{
				this.sendName.text = jsonData.instance.AvatarRandomJsonData[emailData.npcId.ToString()]["Name"].Str;
			}
			if (emailData.isAnswer)
			{
				if (emailData.isPangBai)
				{
					this.sendTime.color = CyUIMag.inst.cyEmail.titleColors[2];
					this.sendName.color = CyUIMag.inst.cyEmail.titleColors[2];
					this.sendName.text = "旁白";
					this.bg.sprite = CyUIMag.inst.cyEmail.titleSprites[2];
				}
				else
				{
					this.sendTime.color = CyUIMag.inst.cyEmail.titleColors[0];
					this.sendName.color = CyUIMag.inst.cyEmail.titleColors[0];
					this.bg.sprite = CyUIMag.inst.cyEmail.titleSprites[0];
					if (emailData.PaiMaiInfo != null)
					{
						this.item.Init();
						this.item.SetItem(10047);
						if (emailData.PaiMaiInfo.EndTime >= Tools.instance.getPlayer().worldTimeMag.getNowTime())
						{
							this.submitBtn.Init(CyUIMag.inst.cyEmail.btnSprites[0], "查看", delegate
							{
								CyUIMag.inst.PaiMaiPanel.Show(emailData.PaiMaiInfo);
							});
						}
					}
				}
				this.content.text = "\u3000" + CyUIMag.inst.cyEmail.GetContent(jsonData.instance.CyNpcAnswerData[emailData.answerId.ToString()]["DuiHua"].Str, emailData);
				return;
			}
			this.sendTime.color = CyUIMag.inst.cyEmail.titleColors[0];
			this.sendName.color = CyUIMag.inst.cyEmail.titleColors[0];
			this.bg.sprite = CyUIMag.inst.cyEmail.titleSprites[0];
			this.content.text = "\u3000" + CyUIMag.inst.cyEmail.GetContent(jsonData.instance.CyNpcDuiBaiData[emailData.content[0].ToString()][string.Format("dir{0}", emailData.content[1])].Str, emailData);
			try
			{
				if (emailData.actionId == 1)
				{
					if (emailData.item[1] > 0)
					{
						this.item.Init();
						this.item.SetItem(emailData.item[0]);
						this.item.Count = emailData.item[1];
						this.submitBtn.Init(CyUIMag.inst.cyEmail.btnSprites[0], "领取", delegate
						{
							this.submitBtn.gameObject.SetActive(false);
							Tools.instance.getPlayer().addItem(emailData.item[0], emailData.item[1], Tools.CreateItemSeid(emailData.item[0]), false);
							this.item.ShowHasGet();
							this.UpdateSize();
							emailData.item[1] = -1;
						});
					}
					else
					{
						this.item.Init();
						this.item.SetItem(emailData.item[0]);
						this.item.ShowHasGet();
						this.submitBtn.gameObject.SetActive(false);
					}
				}
				else if (emailData.actionId == 2)
				{
					this.IsTiJiaoEmail = true;
					this.UpdateTiJiao();
				}
			}
			catch (Exception ex)
			{
				Debug.LogError(ex);
				Debug.LogError(string.Format("物品ID:{0}不存在", emailData.item[0]));
			}
		}
	}

	// Token: 0x060017F3 RID: 6131 RVA: 0x000A7154 File Offset: 0x000A5354
	public void UpdateSize()
	{
		LayoutRebuilder.ForceRebuildLayoutImmediate(this.rectTransform);
		this.sizeFitter.SetLayoutVertical();
		LayoutRebuilder.ForceRebuildLayoutImmediate(this.rectTransform);
		CyUIMag.inst.cyEmail.UpDateSize();
	}

	// Token: 0x060017F4 RID: 6132 RVA: 0x000A7188 File Offset: 0x000A5388
	public void UpdateTiJiao()
	{
		CyEmailCell.<>c__DisplayClass13_0 CS$<>8__locals1 = new CyEmailCell.<>c__DisplayClass13_0();
		CS$<>8__locals1.<>4__this = this;
		if (!this.IsTiJiaoEmail)
		{
			return;
		}
		CS$<>8__locals1.player = Tools.instance.getPlayer();
		if (this.emailData.isComplete)
		{
			this.item.Init();
			this.item.SetItem(this.emailData.item[0]);
			this.item.ShowHasTiJiao();
			return;
		}
		this.item.Init();
		this.item.SetItem(this.emailData.item[0]);
		this.submitBtn.gameObject.SetActive(true);
		if (this.isDeath)
		{
			this.submitBtn.gameObject.SetActive(false);
			return;
		}
		int hasNum = CyUIMag.inst.cyEmail.GetPlayerItemNum(this.emailData.item[0]);
		if (hasNum >= this.emailData.item[1])
		{
			this.item.SetCustomCountText(string.Format("{0}/{1}", hasNum, this.emailData.item[1]), CyUIMag.inst.cyEmail.numColors[0]);
			this.submitBtn.Init(CyUIMag.inst.cyEmail.btnSprites[0], "提交", delegate
			{
				hasNum = CyUIMag.inst.cyEmail.GetPlayerItemNum(CS$<>8__locals1.<>4__this.emailData.item[0]);
				if (hasNum < CS$<>8__locals1.<>4__this.emailData.item[1])
				{
					UIPopTip.Inst.Pop("物品不足", PopTipIconType.叹号);
					CS$<>8__locals1.<>4__this.UpdateTiJiao();
					return;
				}
				CS$<>8__locals1.<>4__this.submitBtn.gameObject.SetActive(false);
				Tools.instance.getPlayer().removeItem(CS$<>8__locals1.<>4__this.emailData.item[0], CS$<>8__locals1.<>4__this.emailData.item[1]);
				CS$<>8__locals1.<>4__this.item.ShowHasTiJiao();
				CS$<>8__locals1.<>4__this.emailData.isComplete = true;
				int i = jsonData.instance.ItemJsonData[CS$<>8__locals1.<>4__this.emailData.item[0].ToString()]["price"].I;
				if (CS$<>8__locals1.<>4__this.emailData.CheckIsOut())
				{
					NPCEx.AddFavor(CS$<>8__locals1.<>4__this.emailData.npcId, 1, false, true);
					NPCEx.AddQingFen(CS$<>8__locals1.<>4__this.emailData.npcId, i, false);
					CS$<>8__locals1.player.emailDateMag.AuToSendToPlayer(CS$<>8__locals1.<>4__this.emailData.npcId, 998, 998, CS$<>8__locals1.player.worldTimeMag.nowTime, null);
				}
				else
				{
					NPCEx.AddFavor(CS$<>8__locals1.<>4__this.emailData.npcId, CS$<>8__locals1.<>4__this.emailData.addHaoGanDu, false, true);
					NPCEx.AddQingFen(CS$<>8__locals1.<>4__this.emailData.npcId, i, false);
					CS$<>8__locals1.player.emailDateMag.AuToSendToPlayer(CS$<>8__locals1.<>4__this.emailData.npcId, 997, 997, CS$<>8__locals1.player.worldTimeMag.nowTime, null);
				}
				NpcJieSuanManager.inst.AddItemToNpcBackpack(CS$<>8__locals1.<>4__this.emailData.npcId, CS$<>8__locals1.<>4__this.emailData.item[0], CS$<>8__locals1.<>4__this.emailData.item[1], null, false);
				CyUIMag.inst.cyEmail.TiJiaoCallBack();
			});
			return;
		}
		this.item.SetCustomCountText(string.Format("{0}/{1}", hasNum, this.emailData.item[1]), CyUIMag.inst.cyEmail.numColors[1]);
		this.submitBtn.Init(CyUIMag.inst.cyEmail.btnSprites[1], "<color=#e4e4e4>提交</color>", null);
	}

	// Token: 0x040012CA RID: 4810
	public Text content;

	// Token: 0x040012CB RID: 4811
	public CyUIIconShow item;

	// Token: 0x040012CC RID: 4812
	public CySubmitBtn submitBtn;

	// Token: 0x040012CD RID: 4813
	public Text sendTime;

	// Token: 0x040012CE RID: 4814
	public Text sendName;

	// Token: 0x040012CF RID: 4815
	public EmailData emailData;

	// Token: 0x040012D0 RID: 4816
	public Image bg;

	// Token: 0x040012D1 RID: 4817
	public bool isDeath;

	// Token: 0x040012D2 RID: 4818
	public ContentSizeFitter sizeFitter;

	// Token: 0x040012D3 RID: 4819
	public RectTransform rectTransform;

	// Token: 0x040012D4 RID: 4820
	public bool IsTiJiaoEmail;
}
