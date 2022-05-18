using System;
using GUIPackage;
using KBEngine;
using UnityEngine;

// Token: 0x0200029F RID: 671
public class selectSkillConfig : MonoBehaviour
{
	// Token: 0x06001482 RID: 5250 RVA: 0x00012F42 File Offset: 0x00011142
	private void Awake()
	{
		this.mList = base.GetComponent<UIPopupList>();
	}

	// Token: 0x06001483 RID: 5251 RVA: 0x000B9DA0 File Offset: 0x000B7FA0
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

	// Token: 0x06001484 RID: 5252 RVA: 0x000B9E60 File Offset: 0x000B8060
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

	// Token: 0x06001485 RID: 5253 RVA: 0x000B9EC4 File Offset: 0x000B80C4
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

	// Token: 0x04000FDF RID: 4063
	protected UIPopupList mList;

	// Token: 0x04000FE0 RID: 4064
	public selectSkillConfig.selectType type;

	// Token: 0x020002A0 RID: 672
	public enum selectType
	{
		// Token: 0x04000FE2 RID: 4066
		SelectSkill,
		// Token: 0x04000FE3 RID: 4067
		SelectStaticSkill,
		// Token: 0x04000FE4 RID: 4068
		SelectItem
	}
}
