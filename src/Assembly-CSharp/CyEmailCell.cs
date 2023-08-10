using System;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CyEmailCell : MonoBehaviour
{
	public Text content;

	public CyUIIconShow item;

	public CySubmitBtn submitBtn;

	public Text sendTime;

	public Text sendName;

	public EmailData emailData;

	public Image bg;

	public bool isDeath;

	public ContentSizeFitter sizeFitter;

	public RectTransform rectTransform;

	public bool IsTiJiaoEmail;

	public void Init(EmailData emailData, bool isDeath)
	{
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0507: Unknown result type (might be due to invalid IL or missing references)
		//IL_0853: Unknown result type (might be due to invalid IL or missing references)
		//IL_0873: Unknown result type (might be due to invalid IL or missing references)
		//IL_0716: Unknown result type (might be due to invalid IL or missing references)
		//IL_0736: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f1: Expected O, but got Unknown
		//IL_09a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_09b2: Expected O, but got Unknown
		//IL_07da: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e4: Expected O, but got Unknown
		IsTiJiaoEmail = false;
		Tools.instance.getPlayer();
		this.emailData = emailData;
		sendTime.text = emailData.TimeToString();
		((Component)((Component)item).gameObject.transform.parent).gameObject.SetActive(false);
		((Component)submitBtn).gameObject.SetActive(false);
		if (emailData.isPlayer)
		{
			sendName.text = Tools.instance.getPlayer().name;
			((Graphic)sendTime).color = CyUIMag.inst.cyEmail.titleColors[1];
			((Graphic)sendName).color = CyUIMag.inst.cyEmail.titleColors[1];
			bg.sprite = CyUIMag.inst.cyEmail.titleSprites[1];
			string text = CyPlayeQuestionData.DataDict[emailData.questionId].WenTi;
			if (text.Contains("{DongFuName}"))
			{
				text = text.Replace("{DongFuName}", Tools.instance.getPlayer().DongFuData[$"DongFu{emailData.DongFuId}"]["DongFuName"].Str);
			}
			content.text = text;
			return;
		}
		if (emailData.isOld)
		{
			JSONObject ChuanYingData = Tools.instance.getPlayer().NewChuanYingList[emailData.oldId.ToString()];
			if (emailData.oldId >= 2082912)
			{
				content.SetText("你在天机阁交易会上寄换的宝物已经成功交易，此物应当已随传音符附带的储物袋送于道友手中。");
				sendName.text = jsonData.instance.AvatarJsonData[912.ToString()]["Name"].Str;
			}
			else
			{
				content.SetText(CyUIMag.inst.cyEmail.GetContent(ChuanYingData["info"].Str, emailData));
				sendName.text = ChuanYingData["AvatarName"].Str;
				if (ChuanYingData["CanCaoZuo"].b)
				{
					ChuanYingData.SetField("CanCaoZuo", val: false);
					if (ChuanYingData.HasField("TaskID"))
					{
						int i = ChuanYingData["TaskID"].I;
						if (!Tools.instance.getPlayer().taskMag.isHasTask(i))
						{
							Tools.instance.getPlayer().taskMag.addTask(i);
							string name = TaskJsonData.DataDict[i].Name;
							string msg = ((TaskJsonData.DataDict[i].Type == 0) ? "获得一条新的传闻" : ("<color=#FF0000>" + name + "</color>任务已开启"));
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
			((Graphic)sendTime).color = CyUIMag.inst.cyEmail.titleColors[0];
			((Graphic)sendName).color = CyUIMag.inst.cyEmail.titleColors[0];
			bg.sprite = CyUIMag.inst.cyEmail.titleSprites[0];
			if (!ChuanYingData.HasField("ItemID") || ChuanYingData["ItemID"].I <= 0)
			{
				return;
			}
			item.Init();
			item.SetItem(ChuanYingData["ItemID"].I);
			if (ChuanYingData["ItemHasGet"].b)
			{
				((Component)submitBtn).gameObject.SetActive(false);
				item.ShowHasGet();
				return;
			}
			submitBtn.Init(CyUIMag.inst.cyEmail.btnSprites[0], "领取", (UnityAction)delegate
			{
				((Component)submitBtn).gameObject.SetActive(false);
				ChuanYingData.SetField("ItemHasGet", val: true);
				Tools.instance.getPlayer().addItem(ChuanYingData["ItemID"].I, 1, Tools.CreateItemSeid(ChuanYingData["ItemID"].I));
				item.ShowHasGet();
				UpdateSize();
			});
			return;
		}
		if (isDeath)
		{
			sendName.text = NpcJieSuanManager.inst.npcDeath.npcDeathJson[emailData.npcId.ToString()]["deathName"].Str;
		}
		else
		{
			sendName.text = jsonData.instance.AvatarRandomJsonData[emailData.npcId.ToString()]["Name"].Str;
		}
		if (emailData.isAnswer)
		{
			if (emailData.isPangBai)
			{
				((Graphic)sendTime).color = CyUIMag.inst.cyEmail.titleColors[2];
				((Graphic)sendName).color = CyUIMag.inst.cyEmail.titleColors[2];
				sendName.text = "旁白";
				bg.sprite = CyUIMag.inst.cyEmail.titleSprites[2];
			}
			else
			{
				((Graphic)sendTime).color = CyUIMag.inst.cyEmail.titleColors[0];
				((Graphic)sendName).color = CyUIMag.inst.cyEmail.titleColors[0];
				bg.sprite = CyUIMag.inst.cyEmail.titleSprites[0];
				if (emailData.PaiMaiInfo != null)
				{
					item.Init();
					item.SetItem(10047);
					if (emailData.PaiMaiInfo.EndTime >= Tools.instance.getPlayer().worldTimeMag.getNowTime())
					{
						submitBtn.Init(CyUIMag.inst.cyEmail.btnSprites[0], "查看", (UnityAction)delegate
						{
							CyUIMag.inst.PaiMaiPanel.Show(emailData.PaiMaiInfo);
						});
					}
				}
			}
			content.text = "\u3000" + CyUIMag.inst.cyEmail.GetContent(jsonData.instance.CyNpcAnswerData[emailData.answerId.ToString()]["DuiHua"].Str, emailData);
			return;
		}
		((Graphic)sendTime).color = CyUIMag.inst.cyEmail.titleColors[0];
		((Graphic)sendName).color = CyUIMag.inst.cyEmail.titleColors[0];
		bg.sprite = CyUIMag.inst.cyEmail.titleSprites[0];
		content.text = "\u3000" + CyUIMag.inst.cyEmail.GetContent(jsonData.instance.CyNpcDuiBaiData[emailData.content[0].ToString()][$"dir{emailData.content[1]}"].Str, emailData);
		try
		{
			if (emailData.actionId == 1)
			{
				if (emailData.item[1] > 0)
				{
					item.Init();
					item.SetItem(emailData.item[0]);
					item.Count = emailData.item[1];
					submitBtn.Init(CyUIMag.inst.cyEmail.btnSprites[0], "领取", (UnityAction)delegate
					{
						((Component)submitBtn).gameObject.SetActive(false);
						Tools.instance.getPlayer().addItem(emailData.item[0], emailData.item[1], Tools.CreateItemSeid(emailData.item[0]));
						item.ShowHasGet();
						UpdateSize();
						emailData.item[1] = -1;
					});
				}
				else
				{
					item.Init();
					item.SetItem(emailData.item[0]);
					item.ShowHasGet();
					((Component)submitBtn).gameObject.SetActive(false);
				}
			}
			else if (emailData.actionId == 2)
			{
				IsTiJiaoEmail = true;
				UpdateTiJiao();
			}
		}
		catch (Exception ex)
		{
			Debug.LogError((object)ex);
			Debug.LogError((object)$"物品ID:{emailData.item[0]}不存在");
		}
	}

	public void UpdateSize()
	{
		LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
		sizeFitter.SetLayoutVertical();
		LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
		CyUIMag.inst.cyEmail.UpDateSize();
	}

	public void UpdateTiJiao()
	{
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Expected O, but got Unknown
		if (!IsTiJiaoEmail)
		{
			return;
		}
		Avatar player = Tools.instance.getPlayer();
		if (emailData.isComplete)
		{
			item.Init();
			item.SetItem(emailData.item[0]);
			item.ShowHasTiJiao();
			return;
		}
		item.Init();
		item.SetItem(emailData.item[0]);
		((Component)submitBtn).gameObject.SetActive(true);
		if (!isDeath)
		{
			int hasNum = CyUIMag.inst.cyEmail.GetPlayerItemNum(emailData.item[0]);
			if (hasNum >= emailData.item[1])
			{
				item.SetCustomCountText($"{hasNum}/{emailData.item[1]}", CyUIMag.inst.cyEmail.numColors[0]);
				submitBtn.Init(CyUIMag.inst.cyEmail.btnSprites[0], "提交", (UnityAction)delegate
				{
					hasNum = CyUIMag.inst.cyEmail.GetPlayerItemNum(emailData.item[0]);
					if (hasNum < emailData.item[1])
					{
						UIPopTip.Inst.Pop("物品不足");
						UpdateTiJiao();
					}
					else
					{
						((Component)submitBtn).gameObject.SetActive(false);
						Tools.instance.getPlayer().removeItem(emailData.item[0], emailData.item[1]);
						item.ShowHasTiJiao();
						emailData.isComplete = true;
						int i = jsonData.instance.ItemJsonData[emailData.item[0].ToString()]["price"].I;
						if (emailData.CheckIsOut())
						{
							NPCEx.AddFavor(emailData.npcId, 1, addQingFen: false);
							NPCEx.AddQingFen(emailData.npcId, i);
							player.emailDateMag.AuToSendToPlayer(emailData.npcId, 998, 998, player.worldTimeMag.nowTime);
						}
						else
						{
							NPCEx.AddFavor(emailData.npcId, emailData.addHaoGanDu, addQingFen: false);
							NPCEx.AddQingFen(emailData.npcId, i);
							player.emailDateMag.AuToSendToPlayer(emailData.npcId, 997, 997, player.worldTimeMag.nowTime);
						}
						NpcJieSuanManager.inst.AddItemToNpcBackpack(emailData.npcId, emailData.item[0], emailData.item[1]);
						CyUIMag.inst.cyEmail.TiJiaoCallBack();
					}
				});
			}
			else
			{
				item.SetCustomCountText($"{hasNum}/{emailData.item[1]}", CyUIMag.inst.cyEmail.numColors[1]);
				submitBtn.Init(CyUIMag.inst.cyEmail.btnSprites[1], "<color=#e4e4e4>提交</color>", null);
			}
		}
		else
		{
			((Component)submitBtn).gameObject.SetActive(false);
		}
	}
}
