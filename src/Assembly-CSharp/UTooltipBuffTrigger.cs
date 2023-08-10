using System.Collections.Generic;
using GUIPackage;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.EventSystems;
using YSGame.Fight;

public class UTooltipBuffTrigger : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	public int BuffID;

	private UIFightBuffItem uiItem;

	private bool isShow;

	private void Awake()
	{
		uiItem = ((Component)this).GetComponent<UIFightBuffItem>();
		MessageMag.Instance.Register(MessageName.MSG_APP_OnFocusChanged, OnFocusChanged);
	}

	private void OnDestroy()
	{
		MessageMag.Instance.Remove(MessageName.MSG_APP_OnFocusChanged, OnFocusChanged);
	}

	public void OnFocusChanged(MessageData data)
	{
		if (isShow)
		{
			isShow = false;
			UToolTip.Close();
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		UToolTip.Show(GetDesc());
		isShow = true;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		isShow = false;
		UToolTip.Close();
	}

	private string GetDesc()
	{
		if ((Object)(object)uiItem == (Object)null)
		{
			return _BuffJsonData.DataDict[BuffID].descr;
		}
		_BuffJsonData buffJsonData = _BuffJsonData.DataDict[uiItem.BuffID];
		if (buffJsonData.seid.Contains(143))
		{
			return GetWuDaoText();
		}
		string text = buffJsonData.name;
		string desstr = buffJsonData.descr;
		Avatar avatar = uiItem.Avatar;
		if (avatar.isPlayer())
		{
			if (avatar.fightTemp.LianQiEquipDictionary.ContainsKey(uiItem.BuffID))
			{
				JSONObject jSONObject = avatar.fightTemp.LianQiEquipDictionary[uiItem.BuffID];
				if (jSONObject != null && jSONObject.HasField("Name"))
				{
					text = jSONObject["Name"].Str;
				}
				if (jSONObject != null && jSONObject.HasField("SeidDesc"))
				{
					desstr = jSONObject["SeidDesc"].Str;
				}
			}
		}
		else if ((Object)(object)RoundManager.instance != (Object)null && RoundManager.instance.newNpcFightManager != null && RoundManager.instance.newNpcFightManager.LianQiEquipDictionary.ContainsKey(uiItem.BuffID))
		{
			JSONObject jSONObject2 = RoundManager.instance.newNpcFightManager.LianQiEquipDictionary[uiItem.BuffID];
			if (jSONObject2 != null && jSONObject2.HasField("Name"))
			{
				text = jSONObject2["Name"].Str;
			}
			if (jSONObject2 != null && jSONObject2.HasField("SeidDesc"))
			{
				desstr = jSONObject2["SeidDesc"].Str;
			}
		}
		string desc = Tools.getDesc(desstr, uiItem.BuffRound);
		desc = desc.Replace("[FF00FF]", "<color=#cda14c>");
		desc = desc.Replace("[-]", "</color>");
		return "<color=#cda14c>" + text + ":</color>" + desc;
	}

	private string GetWuDaoText()
	{
		string text = "";
		int i = uiItem.Avatar.HuaShenLingYuSkill.I;
		if (i > 0)
		{
			GUIPackage.Skill skill = SkillDatebase.instence.Dict[i][1];
			text = text + "<color=#E8CF62><size=22>化神-" + skill.skill_Name + "</size></color>\n";
			text = text + skill.skill_Desc + "\n";
		}
		text += "<color=#E8CF62><size=22>悟道天赋</size></color>\n";
		List<SkillItem> allWuDaoSkills = uiItem.Avatar.wuDaoMag.GetAllWuDaoSkills();
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
}
