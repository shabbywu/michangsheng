using System;
using JSONClass;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020003CB RID: 971
public class CyEmailCell : MonoBehaviour
{
	// Token: 0x06001AD2 RID: 6866 RVA: 0x000ED4C8 File Offset: 0x000EB6C8
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
			this.content.text = "\u3000" + ChuanYingData["info"].Str;
			this.sendTime.color = CyUIMag.inst.cyEmail.titleColors[0];
			this.sendName.text = ChuanYingData["AvatarName"].Str;
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
				}
				else
				{
					this.submitBtn.Init(CyUIMag.inst.cyEmail.btnSprites[0], "领取", delegate
					{
						this.submitBtn.gameObject.SetActive(false);
						ChuanYingData.SetField("ItemHasGet", true);
						Tools.instance.getPlayer().addItem(ChuanYingData["ItemID"].I, 1, Tools.CreateItemSeid(ChuanYingData["ItemID"].I), false);
						this.item.ShowHasGet();
						this.UpdateSize();
					});
				}
			}
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
					return;
				}
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

	// Token: 0x06001AD3 RID: 6867 RVA: 0x00016C1F File Offset: 0x00014E1F
	public void UpdateSize()
	{
		LayoutRebuilder.ForceRebuildLayoutImmediate(this.rectTransform);
		this.sizeFitter.SetLayoutVertical();
		LayoutRebuilder.ForceRebuildLayoutImmediate(this.rectTransform);
		CyUIMag.inst.cyEmail.UpDateSize();
	}

	// Token: 0x06001AD4 RID: 6868 RVA: 0x000EDEB8 File Offset: 0x000EC0B8
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

	// Token: 0x04001653 RID: 5715
	public Text content;

	// Token: 0x04001654 RID: 5716
	public CyUIIconShow item;

	// Token: 0x04001655 RID: 5717
	public CySubmitBtn submitBtn;

	// Token: 0x04001656 RID: 5718
	public Text sendTime;

	// Token: 0x04001657 RID: 5719
	public Text sendName;

	// Token: 0x04001658 RID: 5720
	public EmailData emailData;

	// Token: 0x04001659 RID: 5721
	public Image bg;

	// Token: 0x0400165A RID: 5722
	public bool isDeath;

	// Token: 0x0400165B RID: 5723
	public ContentSizeFitter sizeFitter;

	// Token: 0x0400165C RID: 5724
	public RectTransform rectTransform;

	// Token: 0x0400165D RID: 5725
	public bool IsTiJiaoEmail;
}
