using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YSGame;

// Token: 0x02000187 RID: 391
public class LevelSelectManager : MonoBehaviour
{
	// Token: 0x06000D0F RID: 3343 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x06000D10 RID: 3344 RVA: 0x0000EC8E File Offset: 0x0000CE8E
	public void closeSetName()
	{
		this.setPlayerName.SetActive(false);
	}

	// Token: 0x06000D11 RID: 3345 RVA: 0x0000EC9C File Offset: 0x0000CE9C
	public void closeRemoveAvatar()
	{
		this.removeAvatar.SetActive(false);
	}

	// Token: 0x06000D12 RID: 3346 RVA: 0x0009A7D8 File Offset: 0x000989D8
	public string getPlayerName()
	{
		InputField component = this.setPlayerName.transform.Find("GameObject/InputFieldXin").GetComponent<InputField>();
		InputField component2 = this.setPlayerName.transform.Find("GameObject/InputFieldMing").GetComponent<InputField>();
		return component.text + component2.text;
	}

	// Token: 0x06000D13 RID: 3347 RVA: 0x0000ECAA File Offset: 0x0000CEAA
	public string getFirstName()
	{
		return this.setPlayerName.transform.Find("GameObject/InputFieldXin").GetComponent<InputField>().text;
	}

	// Token: 0x06000D14 RID: 3348 RVA: 0x0000ECCB File Offset: 0x0000CECB
	public string getLastName()
	{
		return this.setPlayerName.transform.Find("GameObject/InputFieldMing").GetComponent<InputField>().text;
	}

	// Token: 0x06000D15 RID: 3349 RVA: 0x0009A82C File Offset: 0x00098A2C
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

	// Token: 0x06000D16 RID: 3350 RVA: 0x0000ECEC File Offset: 0x0000CEEC
	public virtual void openLevelSelect()
	{
		if (!this.isInit)
		{
			this.InitializeLevelSelectMenu();
		}
		this.levelSelectMenu.gameObject.SetActive(true);
		this.levelSelectMenu.GetComponentInChildren<Animation>().Play("tankuanglachu");
	}

	// Token: 0x06000D17 RID: 3351 RVA: 0x0000ED23 File Offset: 0x0000CF23
	public virtual void closeLevelSelect()
	{
		this.levelSelectMenu.gameObject.SetActive(false);
	}

	// Token: 0x06000D18 RID: 3352 RVA: 0x0009A8A4 File Offset: 0x00098AA4
	public void reloadUI()
	{
		foreach (object obj in this.levelItemsContainer.transform)
		{
			Object.Destroy(((Transform)obj).gameObject);
		}
		this.InitializeLevelSelectMenu();
	}

	// Token: 0x06000D19 RID: 3353 RVA: 0x00004050 File Offset: 0x00002250
	public virtual int StartIndex()
	{
		return 0;
	}

	// Token: 0x06000D1A RID: 3354 RVA: 0x0009A90C File Offset: 0x00098B0C
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

	// Token: 0x06000D1B RID: 3355 RVA: 0x0000ED36 File Offset: 0x0000CF36
	protected LevelSelectItem createLevelItem()
	{
		LevelSelectItem levelSelectItem = Object.Instantiate<LevelSelectItem>(this.LevelItemPrefab);
		levelSelectItem.transform.SetParent(this.levelItemsContainer);
		levelSelectItem.transform.localScale = Vector3.one;
		levelSelectItem.transform.localPosition = Vector3.zero;
		return levelSelectItem;
	}

	// Token: 0x04000A42 RID: 2626
	public bool isInit;

	// Token: 0x04000A43 RID: 2627
	[Tooltip("Do you want Level Select Menu?")]
	public bool hasLevelSelection = true;

	// Token: 0x04000A44 RID: 2628
	[Header("Level Select Properties")]
	[Tooltip("Root Level Select Menu")]
	public GameObject levelSelectMenu;

	// Token: 0x04000A45 RID: 2629
	[Tooltip("Level Select Items will be here!")]
	public Transform levelItemsContainer;

	// Token: 0x04000A46 RID: 2630
	[Tooltip("Level Item Prefab")]
	public LevelSelectItem LevelItemPrefab;

	// Token: 0x04000A47 RID: 2631
	[Space(10f)]
	[Tooltip("设置玩家名字")]
	public GameObject setPlayerName;

	// Token: 0x04000A48 RID: 2632
	[Tooltip("是否确定删除存档界面")]
	public GameObject removeAvatar;

	// Token: 0x04000A49 RID: 2633
	[Header("Level Item Configuration")]
	[Tooltip("Define everything here!")]
	public List<LevelItemsConfiguration> levelItemsConfiguration;

	// Token: 0x04000A4A RID: 2634
	protected GameObject firstItem;

	// Token: 0x02000188 RID: 392
	public enum MaxLevelSelect
	{
		// Token: 0x04000A4C RID: 2636
		Max = 6
	}
}
