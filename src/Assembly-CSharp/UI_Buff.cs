using System.Collections.Generic;
using GUIPackage;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Buff : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
	public int buffID;

	public int buffRound;

	public List<int> avatarBuff = new List<int>();

	private void Start()
	{
		((Component)this).gameObject.GetComponent<Button>();
	}

	private void OnDestroy()
	{
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (buffID == 0)
		{
			return;
		}
		string text = _BuffJsonData.DataDict[buffID].name;
		string desstr = _BuffJsonData.DataDict[buffID].descr;
		Avatar buffAvatar = getBuffAvatar();
		if (buffAvatar.isPlayer())
		{
			if (buffAvatar.fightTemp.LianQiEquipDictionary.ContainsKey(buffID))
			{
				JSONObject jSONObject = buffAvatar.fightTemp.LianQiEquipDictionary[buffID];
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
		else if ((Object)(object)RoundManager.instance != (Object)null && RoundManager.instance.newNpcFightManager != null && RoundManager.instance.newNpcFightManager.LianQiEquipDictionary.ContainsKey(buffID))
		{
			JSONObject jSONObject2 = RoundManager.instance.newNpcFightManager.LianQiEquipDictionary[buffID];
			if (jSONObject2 != null && jSONObject2.HasField("Name"))
			{
				text = jSONObject2["Name"].Str;
			}
			if (jSONObject2 != null && jSONObject2.HasField("SeidDesc"))
			{
				desstr = jSONObject2["SeidDesc"].Str;
			}
		}
		Singleton.inventory.Tooltip.GetComponentInChildren<UILabel>().text = "[ff8300]" + text + ":[-] " + Tools.getDesc(desstr, buffRound).Replace("[FF00FF]", "[ff8300]");
		if (jsonData.instance.BuffJsonData[buffID.ToString()]["seid"].HasItem(143))
		{
			Singleton.inventory.Tooltip.GetComponentInChildren<UILabel>().text = getWuDaoText();
		}
		Singleton.inventory.showTooltip = true;
	}

	public string getWuDaoText()
	{
		string text = "[ff8300]悟道天赋[-]\n";
		List<SkillItem> allWuDaoSkills = getBuffAvatar().wuDaoMag.GetAllWuDaoSkills();
		int num = 1;
		foreach (SkillItem item in allWuDaoSkills)
		{
			WuDaoJson wuDaoJson = WuDaoJson.DataDict[item.itemId];
			string text2 = "[ff8300]" + wuDaoJson.name + "：[-]" + wuDaoJson.xiaoguo + "\n";
			text += text2;
			num++;
		}
		return text;
	}

	public Avatar getBuffAvatar()
	{
		return ((Component)((Component)this).transform).GetComponentInParent<UI_Target>().avatar;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		Singleton.inventory.showTooltip = false;
	}

	public void CloseTips()
	{
		Singleton.inventory.showTooltip = false;
	}

	public void showBuffTool()
	{
		Debug.Log((object)("buffID = " + buffID));
	}

	private void Update()
	{
	}

	public bool IsCursorOnUI(int inputID = -1)
	{
		return EventSystem.current.IsPointerOverGameObject(inputID);
	}

	public void OnPointerDown(PointerEventData eventData)
	{
	}

	public void OnPointerUp(PointerEventData eventData)
	{
	}
}
