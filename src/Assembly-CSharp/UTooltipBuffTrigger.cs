using System;
using System.Collections.Generic;
using GUIPackage;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.EventSystems;
using YSGame.Fight;

// Token: 0x020004E9 RID: 1257
public class UTooltipBuffTrigger : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	// Token: 0x060020C5 RID: 8389 RVA: 0x0001AFC3 File Offset: 0x000191C3
	private void Awake()
	{
		this.uiItem = base.GetComponent<UIFightBuffItem>();
		MessageMag.Instance.Register(MessageName.MSG_APP_OnFocusChanged, new Action<MessageData>(this.OnFocusChanged));
	}

	// Token: 0x060020C6 RID: 8390 RVA: 0x0001AFEC File Offset: 0x000191EC
	private void OnDestroy()
	{
		MessageMag.Instance.Remove(MessageName.MSG_APP_OnFocusChanged, new Action<MessageData>(this.OnFocusChanged));
	}

	// Token: 0x060020C7 RID: 8391 RVA: 0x0001B009 File Offset: 0x00019209
	public void OnFocusChanged(MessageData data)
	{
		if (this.isShow)
		{
			this.isShow = false;
			UToolTip.Close();
		}
	}

	// Token: 0x060020C8 RID: 8392 RVA: 0x0001B01F File Offset: 0x0001921F
	public void OnPointerEnter(PointerEventData eventData)
	{
		UToolTip.Show(this.GetDesc(), 600f, 200f);
		this.isShow = true;
	}

	// Token: 0x060020C9 RID: 8393 RVA: 0x0001B03D File Offset: 0x0001923D
	public void OnPointerExit(PointerEventData eventData)
	{
		this.isShow = false;
		UToolTip.Close();
	}

	// Token: 0x060020CA RID: 8394 RVA: 0x00114220 File Offset: 0x00112420
	private string GetDesc()
	{
		if (this.uiItem == null)
		{
			return _BuffJsonData.DataDict[this.BuffID].descr;
		}
		_BuffJsonData buffJsonData = _BuffJsonData.DataDict[this.uiItem.BuffID];
		if (buffJsonData.seid.Contains(143))
		{
			return this.GetWuDaoText();
		}
		string str = buffJsonData.name;
		string desstr = buffJsonData.descr;
		Avatar avatar = this.uiItem.Avatar;
		if (avatar.isPlayer())
		{
			if (avatar.fightTemp.LianQiEquipDictionary.ContainsKey(this.uiItem.BuffID))
			{
				JSONObject jsonobject = avatar.fightTemp.LianQiEquipDictionary[this.uiItem.BuffID];
				if (jsonobject != null && jsonobject.HasField("Name"))
				{
					str = jsonobject["Name"].Str;
				}
				if (jsonobject != null && jsonobject.HasField("SeidDesc"))
				{
					desstr = jsonobject["SeidDesc"].Str;
				}
			}
		}
		else if (RoundManager.instance != null && RoundManager.instance.newNpcFightManager != null && RoundManager.instance.newNpcFightManager.LianQiEquipDictionary.ContainsKey(this.uiItem.BuffID))
		{
			JSONObject jsonobject2 = RoundManager.instance.newNpcFightManager.LianQiEquipDictionary[this.uiItem.BuffID];
			if (jsonobject2 != null && jsonobject2.HasField("Name"))
			{
				str = jsonobject2["Name"].Str;
			}
			if (jsonobject2 != null && jsonobject2.HasField("SeidDesc"))
			{
				desstr = jsonobject2["SeidDesc"].Str;
			}
		}
		string text = Tools.getDesc(desstr, this.uiItem.BuffRound);
		text = text.Replace("[FF00FF]", "<color=#cda14c>");
		text = text.Replace("[-]", "</color>");
		return "<color=#cda14c>" + str + ":</color>" + text;
	}

	// Token: 0x060020CB RID: 8395 RVA: 0x0011442C File Offset: 0x0011262C
	private string GetWuDaoText()
	{
		string text = "";
		int i = this.uiItem.Avatar.HuaShenLingYuSkill.I;
		if (i > 0)
		{
			GUIPackage.Skill skill = SkillDatebase.instence.Dict[i][1];
			text = text + "<color=#E8CF62><size=22>化神-" + skill.skill_Name + "</size></color>\n";
			text = text + skill.skill_Desc + "\n";
		}
		text += "<color=#E8CF62><size=22>悟道天赋</size></color>\n";
		List<SkillItem> allWuDaoSkills = this.uiItem.Avatar.wuDaoMag.GetAllWuDaoSkills();
		if (allWuDaoSkills.Count > 0)
		{
			int num = 1;
			for (int j = 0; j < allWuDaoSkills.Count; j++)
			{
				WuDaoJson wuDaoJson = WuDaoJson.DataDict[allWuDaoSkills[j].itemId];
				string text2 = "<color=#cda14c>" + wuDaoJson.name + "：</color>" + wuDaoJson.xiaoguo;
				if (j != allWuDaoSkills.Count - 1)
				{
					text2 += "\n";
				}
				text += text2;
				num++;
			}
		}
		else
		{
			text += "暂无";
		}
		return text;
	}

	// Token: 0x04001C4E RID: 7246
	public int BuffID;

	// Token: 0x04001C4F RID: 7247
	private UIFightBuffItem uiItem;

	// Token: 0x04001C50 RID: 7248
	private bool isShow;
}
