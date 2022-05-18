using System;
using System.Collections.Generic;
using GUIPackage;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x020005B1 RID: 1457
public class UI_Buff : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
	// Token: 0x060024BF RID: 9407 RVA: 0x0001D8D0 File Offset: 0x0001BAD0
	private void Start()
	{
		base.gameObject.GetComponent<Button>();
	}

	// Token: 0x060024C0 RID: 9408 RVA: 0x000042DD File Offset: 0x000024DD
	private void OnDestroy()
	{
	}

	// Token: 0x060024C1 RID: 9409 RVA: 0x00129708 File Offset: 0x00127908
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

	// Token: 0x060024C2 RID: 9410 RVA: 0x0012990C File Offset: 0x00127B0C
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

	// Token: 0x060024C3 RID: 9411 RVA: 0x0001D8DE File Offset: 0x0001BADE
	public Avatar getBuffAvatar()
	{
		return base.transform.GetComponentInParent<UI_Target>().avatar;
	}

	// Token: 0x060024C4 RID: 9412 RVA: 0x0001D8F0 File Offset: 0x0001BAF0
	public void OnPointerExit(PointerEventData eventData)
	{
		Singleton.inventory.showTooltip = false;
	}

	// Token: 0x060024C5 RID: 9413 RVA: 0x0001D8F0 File Offset: 0x0001BAF0
	public void CloseTips()
	{
		Singleton.inventory.showTooltip = false;
	}

	// Token: 0x060024C6 RID: 9414 RVA: 0x0001D8FD File Offset: 0x0001BAFD
	public void showBuffTool()
	{
		Debug.Log("buffID = " + this.buffID);
	}

	// Token: 0x060024C7 RID: 9415 RVA: 0x000042DD File Offset: 0x000024DD
	private void Update()
	{
	}

	// Token: 0x060024C8 RID: 9416 RVA: 0x0001D919 File Offset: 0x0001BB19
	public bool IsCursorOnUI(int inputID = -1)
	{
		return EventSystem.current.IsPointerOverGameObject(inputID);
	}

	// Token: 0x060024C9 RID: 9417 RVA: 0x000042DD File Offset: 0x000024DD
	public void OnPointerDown(PointerEventData eventData)
	{
	}

	// Token: 0x060024CA RID: 9418 RVA: 0x000042DD File Offset: 0x000024DD
	public void OnPointerUp(PointerEventData eventData)
	{
	}

	// Token: 0x04001F88 RID: 8072
	public int buffID;

	// Token: 0x04001F89 RID: 8073
	public int buffRound;

	// Token: 0x04001F8A RID: 8074
	public List<int> avatarBuff = new List<int>();
}
