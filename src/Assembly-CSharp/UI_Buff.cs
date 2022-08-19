using System;
using System.Collections.Generic;
using GUIPackage;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000401 RID: 1025
public class UI_Buff : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
	// Token: 0x0600210D RID: 8461 RVA: 0x000E7947 File Offset: 0x000E5B47
	private void Start()
	{
		base.gameObject.GetComponent<Button>();
	}

	// Token: 0x0600210E RID: 8462 RVA: 0x00004095 File Offset: 0x00002295
	private void OnDestroy()
	{
	}

	// Token: 0x0600210F RID: 8463 RVA: 0x000E7958 File Offset: 0x000E5B58
	public void OnPointerEnter(PointerEventData eventData)
	{
		if (this.buffID == 0)
		{
			return;
		}
		string str = _BuffJsonData.DataDict[this.buffID].name;
		string desstr = _BuffJsonData.DataDict[this.buffID].descr;
		Avatar buffAvatar = this.getBuffAvatar();
		if (buffAvatar.isPlayer())
		{
			if (buffAvatar.fightTemp.LianQiEquipDictionary.ContainsKey(this.buffID))
			{
				JSONObject jsonobject = buffAvatar.fightTemp.LianQiEquipDictionary[this.buffID];
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
		else if (RoundManager.instance != null && RoundManager.instance.newNpcFightManager != null && RoundManager.instance.newNpcFightManager.LianQiEquipDictionary.ContainsKey(this.buffID))
		{
			JSONObject jsonobject2 = RoundManager.instance.newNpcFightManager.LianQiEquipDictionary[this.buffID];
			if (jsonobject2 != null && jsonobject2.HasField("Name"))
			{
				str = jsonobject2["Name"].Str;
			}
			if (jsonobject2 != null && jsonobject2.HasField("SeidDesc"))
			{
				desstr = jsonobject2["SeidDesc"].Str;
			}
		}
		Singleton.inventory.Tooltip.GetComponentInChildren<UILabel>().text = "[ff8300]" + str + ":[-] " + Tools.getDesc(desstr, this.buffRound).Replace("[FF00FF]", "[ff8300]");
		if (jsonData.instance.BuffJsonData[this.buffID.ToString()]["seid"].HasItem(143))
		{
			Singleton.inventory.Tooltip.GetComponentInChildren<UILabel>().text = this.getWuDaoText();
		}
		Singleton.inventory.showTooltip = true;
	}

	// Token: 0x06002110 RID: 8464 RVA: 0x000E7B5C File Offset: 0x000E5D5C
	public string getWuDaoText()
	{
		string text = "[ff8300]悟道天赋[-]\n";
		List<SkillItem> allWuDaoSkills = this.getBuffAvatar().wuDaoMag.GetAllWuDaoSkills();
		int num = 1;
		foreach (SkillItem skillItem in allWuDaoSkills)
		{
			WuDaoJson wuDaoJson = WuDaoJson.DataDict[skillItem.itemId];
			string str = string.Concat(new string[]
			{
				"[ff8300]",
				wuDaoJson.name,
				"：[-]",
				wuDaoJson.xiaoguo,
				"\n"
			});
			text += str;
			num++;
		}
		return text;
	}

	// Token: 0x06002111 RID: 8465 RVA: 0x000E7C14 File Offset: 0x000E5E14
	public Avatar getBuffAvatar()
	{
		return base.transform.GetComponentInParent<UI_Target>().avatar;
	}

	// Token: 0x06002112 RID: 8466 RVA: 0x000E7C26 File Offset: 0x000E5E26
	public void OnPointerExit(PointerEventData eventData)
	{
		Singleton.inventory.showTooltip = false;
	}

	// Token: 0x06002113 RID: 8467 RVA: 0x000E7C26 File Offset: 0x000E5E26
	public void CloseTips()
	{
		Singleton.inventory.showTooltip = false;
	}

	// Token: 0x06002114 RID: 8468 RVA: 0x000E7C33 File Offset: 0x000E5E33
	public void showBuffTool()
	{
		Debug.Log("buffID = " + this.buffID);
	}

	// Token: 0x06002115 RID: 8469 RVA: 0x00004095 File Offset: 0x00002295
	private void Update()
	{
	}

	// Token: 0x06002116 RID: 8470 RVA: 0x000E7C4F File Offset: 0x000E5E4F
	public bool IsCursorOnUI(int inputID = -1)
	{
		return EventSystem.current.IsPointerOverGameObject(inputID);
	}

	// Token: 0x06002117 RID: 8471 RVA: 0x00004095 File Offset: 0x00002295
	public void OnPointerDown(PointerEventData eventData)
	{
	}

	// Token: 0x06002118 RID: 8472 RVA: 0x00004095 File Offset: 0x00002295
	public void OnPointerUp(PointerEventData eventData)
	{
	}

	// Token: 0x04001ACC RID: 6860
	public int buffID;

	// Token: 0x04001ACD RID: 6861
	public int buffRound;

	// Token: 0x04001ACE RID: 6862
	public List<int> avatarBuff = new List<int>();
}
