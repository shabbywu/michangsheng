using System;
using GUIPackage;
using KBEngine;
using UnityEngine;

// Token: 0x020001A3 RID: 419
public class selectSkillConfig : MonoBehaviour
{
	// Token: 0x060011DB RID: 4571 RVA: 0x0006BCF8 File Offset: 0x00069EF8
	private void Awake()
	{
		this.mList = base.GetComponent<UIPopupList>();
	}

	// Token: 0x060011DC RID: 4572 RVA: 0x0006BD08 File Offset: 0x00069F08
	private void Start()
	{
		Avatar player = Tools.instance.getPlayer();
		switch (this.type)
		{
		case selectSkillConfig.selectType.SelectSkill:
			this.mList.value = this.mList.items[player.nowConfigEquipSkill];
			this.OnChange();
			break;
		case selectSkillConfig.selectType.SelectStaticSkill:
			this.mList.value = this.mList.items[player.nowConfigEquipStaticSkill];
			break;
		case selectSkillConfig.selectType.SelectItem:
			this.mList.value = this.mList.items[player.nowConfigEquipItem];
			break;
		}
		EventDelegate.Add(this.mList.onChange, new EventDelegate.Callback(this.OnChange));
	}

	// Token: 0x060011DD RID: 4573 RVA: 0x0006BDC8 File Offset: 0x00069FC8
	public int getInputID(string name)
	{
		int num = 0;
		foreach (string b in this.mList.items)
		{
			if (name == b)
			{
				break;
			}
			num++;
		}
		return num;
	}

	// Token: 0x060011DE RID: 4574 RVA: 0x0006BE2C File Offset: 0x0006A02C
	private void OnChange()
	{
		Avatar player = Tools.instance.getPlayer();
		int inputID = this.getInputID(this.mList.value);
		switch (this.type)
		{
		case selectSkillConfig.selectType.SelectSkill:
			player.setSkillConfigIndex(inputID);
			Singleton.key.LoadMapKey();
			return;
		case selectSkillConfig.selectType.SelectStaticSkill:
			player.setStatikConfigIndex(inputID);
			Singleton.key2.LoadMapPassKey();
			return;
		case selectSkillConfig.selectType.SelectItem:
			player.setItemConfigIndex(inputID);
			return;
		default:
			return;
		}
	}

	// Token: 0x04000CBB RID: 3259
	protected UIPopupList mList;

	// Token: 0x04000CBC RID: 3260
	public selectSkillConfig.selectType type;

	// Token: 0x020012BC RID: 4796
	public enum selectType
	{
		// Token: 0x04006681 RID: 26241
		SelectSkill,
		// Token: 0x04006682 RID: 26242
		SelectStaticSkill,
		// Token: 0x04006683 RID: 26243
		SelectItem
	}
}
