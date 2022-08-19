using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YSGame;

// Token: 0x02000105 RID: 261
public class LevelSelectManager : MonoBehaviour
{
	// Token: 0x06000BF4 RID: 3060 RVA: 0x00004095 File Offset: 0x00002295
	private void Start()
	{
	}

	// Token: 0x06000BF5 RID: 3061 RVA: 0x00048ADC File Offset: 0x00046CDC
	public void closeSetName()
	{
		this.setPlayerName.SetActive(false);
	}

	// Token: 0x06000BF6 RID: 3062 RVA: 0x00048AEA File Offset: 0x00046CEA
	public void closeRemoveAvatar()
	{
		this.removeAvatar.SetActive(false);
	}

	// Token: 0x06000BF7 RID: 3063 RVA: 0x00048AF8 File Offset: 0x00046CF8
	public string getPlayerName()
	{
		InputField component = this.setPlayerName.transform.Find("GameObject/InputFieldXin").GetComponent<InputField>();
		InputField component2 = this.setPlayerName.transform.Find("GameObject/InputFieldMing").GetComponent<InputField>();
		return component.text + component2.text;
	}

	// Token: 0x06000BF8 RID: 3064 RVA: 0x00048B4A File Offset: 0x00046D4A
	public string getFirstName()
	{
		return this.setPlayerName.transform.Find("GameObject/InputFieldXin").GetComponent<InputField>().text;
	}

	// Token: 0x06000BF9 RID: 3065 RVA: 0x00048B6B File Offset: 0x00046D6B
	public string getLastName()
	{
		return this.setPlayerName.transform.Find("GameObject/InputFieldMing").GetComponent<InputField>().text;
	}

	// Token: 0x06000BFA RID: 3066 RVA: 0x00048B8C File Offset: 0x00046D8C
	public void randomPlayerName()
	{
		string text;
		string text2;
		string input;
		do
		{
			text = jsonData.instance.RandomFirstName();
			text2 = jsonData.instance.RandomManLastName();
			input = text + text2;
		}
		while (!Tools.instance.CheckBadWord(input));
		this.setPlayerName.transform.Find("GameObject/InputFieldXin").GetComponent<InputField>().text = text;
		this.setPlayerName.transform.Find("GameObject/InputFieldMing").GetComponent<InputField>().text = text2;
	}

	// Token: 0x06000BFB RID: 3067 RVA: 0x00048C04 File Offset: 0x00046E04
	public virtual void openLevelSelect()
	{
		if (!this.isInit)
		{
			this.InitializeLevelSelectMenu();
		}
		this.levelSelectMenu.gameObject.SetActive(true);
		this.levelSelectMenu.GetComponentInChildren<Animation>().Play("tankuanglachu");
	}

	// Token: 0x06000BFC RID: 3068 RVA: 0x00048C3B File Offset: 0x00046E3B
	public virtual void closeLevelSelect()
	{
		this.levelSelectMenu.gameObject.SetActive(false);
	}

	// Token: 0x06000BFD RID: 3069 RVA: 0x00048C50 File Offset: 0x00046E50
	public void reloadUI()
	{
		foreach (object obj in this.levelItemsContainer.transform)
		{
			Object.Destroy(((Transform)obj).gameObject);
		}
		this.InitializeLevelSelectMenu();
	}

	// Token: 0x06000BFE RID: 3070 RVA: 0x0000280F File Offset: 0x00000A0F
	public virtual int StartIndex()
	{
		return 0;
	}

	// Token: 0x06000BFF RID: 3071 RVA: 0x00048CB8 File Offset: 0x00046EB8
	private void InitializeLevelSelectMenu()
	{
		this.isInit = true;
		for (int i = this.StartIndex(); i < this.StartIndex() + this.levelItemsConfiguration.Count; i++)
		{
			LevelSelectItem levelSelectItem = this.createLevelItem();
			try
			{
				string levelNm = this.levelItemsConfiguration[i - this.StartIndex()].levelName;
				if (YSSaveGame.GetString("PlayerAvatarName" + i, "") != "")
				{
					levelNm = YSSaveGame.GetString("PlayerAvatarName" + i, "");
				}
				levelSelectItem.setLevelItem(this.levelItemsConfiguration[i - this.StartIndex()].levelImage, i - this.StartIndex() + 1, levelNm, this.levelItemsConfiguration[i - this.StartIndex()].hasSubLevel, this.levelItemsConfiguration[i - this.StartIndex()].subLevelName, this.levelItemsConfiguration[i - this.StartIndex()].levelToLoad, this.levelItemsConfiguration[i - this.StartIndex()].isLocked, YSSaveGame.GetInt("SaveAvatar" + i, 0) == 1, i);
				if (i == 0)
				{
					this.firstItem = levelSelectItem.gameObject;
				}
			}
			catch (Exception ex)
			{
				Debug.LogError(string.Format("读档时出现错误，已跳过当前存档，索引{0}，错误信息:\n{1}\n{2}", i, ex.Message, ex.StackTrace));
				UIPopTip.Inst.Pop(string.Format("存档{0}损坏，已略过", i + 1), PopTipIconType.叹号);
				Object.Destroy(levelSelectItem.gameObject);
			}
		}
	}

	// Token: 0x06000C00 RID: 3072 RVA: 0x00048E74 File Offset: 0x00047074
	protected LevelSelectItem createLevelItem()
	{
		LevelSelectItem levelSelectItem = Object.Instantiate<LevelSelectItem>(this.LevelItemPrefab);
		levelSelectItem.transform.SetParent(this.levelItemsContainer);
		levelSelectItem.transform.localScale = Vector3.one;
		levelSelectItem.transform.localPosition = Vector3.zero;
		return levelSelectItem;
	}

	// Token: 0x0400084B RID: 2123
	public bool isInit;

	// Token: 0x0400084C RID: 2124
	[Tooltip("Do you want Level Select Menu?")]
	public bool hasLevelSelection = true;

	// Token: 0x0400084D RID: 2125
	[Header("Level Select Properties")]
	[Tooltip("Root Level Select Menu")]
	public GameObject levelSelectMenu;

	// Token: 0x0400084E RID: 2126
	[Tooltip("Level Select Items will be here!")]
	public Transform levelItemsContainer;

	// Token: 0x0400084F RID: 2127
	[Tooltip("Level Item Prefab")]
	public LevelSelectItem LevelItemPrefab;

	// Token: 0x04000850 RID: 2128
	[Space(10f)]
	[Tooltip("设置玩家名字")]
	public GameObject setPlayerName;

	// Token: 0x04000851 RID: 2129
	[Tooltip("是否确定删除存档界面")]
	public GameObject removeAvatar;

	// Token: 0x04000852 RID: 2130
	[Header("Level Item Configuration")]
	[Tooltip("Define everything here!")]
	public List<LevelItemsConfiguration> levelItemsConfiguration;

	// Token: 0x04000853 RID: 2131
	protected GameObject firstItem;

	// Token: 0x02001246 RID: 4678
	public enum MaxLevelSelect
	{
		// Token: 0x04006547 RID: 25927
		Max = 6
	}
}
