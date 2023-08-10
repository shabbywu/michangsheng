using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YSGame;

public class LevelSelectManager : MonoBehaviour
{
	public enum MaxLevelSelect
	{
		Max = 6
	}

	public bool isInit;

	[Tooltip("Do you want Level Select Menu?")]
	public bool hasLevelSelection = true;

	[Header("Level Select Properties")]
	[Tooltip("Root Level Select Menu")]
	public GameObject levelSelectMenu;

	[Tooltip("Level Select Items will be here!")]
	public Transform levelItemsContainer;

	[Tooltip("Level Item Prefab")]
	public LevelSelectItem LevelItemPrefab;

	[Space(10f)]
	[Tooltip("设置玩家名字")]
	public GameObject setPlayerName;

	[Tooltip("是否确定删除存档界面")]
	public GameObject removeAvatar;

	[Header("Level Item Configuration")]
	[Tooltip("Define everything here!")]
	public List<LevelItemsConfiguration> levelItemsConfiguration;

	protected GameObject firstItem;

	private void Start()
	{
	}

	public void closeSetName()
	{
		setPlayerName.SetActive(false);
	}

	public void closeRemoveAvatar()
	{
		removeAvatar.SetActive(false);
	}

	public string getPlayerName()
	{
		InputField component = ((Component)setPlayerName.transform.Find("GameObject/InputFieldXin")).GetComponent<InputField>();
		return string.Concat(str1: ((Component)setPlayerName.transform.Find("GameObject/InputFieldMing")).GetComponent<InputField>().text, str0: component.text);
	}

	public string getFirstName()
	{
		return ((Component)setPlayerName.transform.Find("GameObject/InputFieldXin")).GetComponent<InputField>().text;
	}

	public string getLastName()
	{
		return ((Component)setPlayerName.transform.Find("GameObject/InputFieldMing")).GetComponent<InputField>().text;
	}

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
		((Component)setPlayerName.transform.Find("GameObject/InputFieldXin")).GetComponent<InputField>().text = text;
		((Component)setPlayerName.transform.Find("GameObject/InputFieldMing")).GetComponent<InputField>().text = text2;
	}

	public virtual void openLevelSelect()
	{
		if (!isInit)
		{
			InitializeLevelSelectMenu();
		}
		levelSelectMenu.gameObject.SetActive(true);
		levelSelectMenu.GetComponentInChildren<Animation>().Play("tankuanglachu");
	}

	public virtual void closeLevelSelect()
	{
		levelSelectMenu.gameObject.SetActive(false);
	}

	public void reloadUI()
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		foreach (Transform item in ((Component)levelItemsContainer).transform)
		{
			Object.Destroy((Object)(object)((Component)item).gameObject);
		}
		InitializeLevelSelectMenu();
	}

	public virtual int StartIndex()
	{
		return 0;
	}

	private void InitializeLevelSelectMenu()
	{
		isInit = true;
		for (int i = StartIndex(); i < StartIndex() + levelItemsConfiguration.Count; i++)
		{
			LevelSelectItem levelSelectItem = createLevelItem();
			try
			{
				string levelNm = levelItemsConfiguration[i - StartIndex()].levelName;
				if (YSSaveGame.GetString("PlayerAvatarName" + i) != "")
				{
					levelNm = YSSaveGame.GetString("PlayerAvatarName" + i);
				}
				levelSelectItem.setLevelItem(levelItemsConfiguration[i - StartIndex()].levelImage, i - StartIndex() + 1, levelNm, levelItemsConfiguration[i - StartIndex()].hasSubLevel, levelItemsConfiguration[i - StartIndex()].subLevelName, levelItemsConfiguration[i - StartIndex()].levelToLoad, levelItemsConfiguration[i - StartIndex()].isLocked, YSSaveGame.GetInt("SaveAvatar" + i) == 1, i);
				if (i == 0)
				{
					firstItem = ((Component)levelSelectItem).gameObject;
				}
			}
			catch (Exception ex)
			{
				Debug.LogError((object)$"读档时出现错误，已跳过当前存档，索引{i}，错误信息:\n{ex.Message}\n{ex.StackTrace}");
				UIPopTip.Inst.Pop($"存档{i + 1}损坏，已略过");
				Object.Destroy((Object)(object)((Component)levelSelectItem).gameObject);
			}
		}
	}

	protected LevelSelectItem createLevelItem()
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		LevelSelectItem levelSelectItem = Object.Instantiate<LevelSelectItem>(LevelItemPrefab);
		((Component)levelSelectItem).transform.SetParent(levelItemsContainer);
		((Component)levelSelectItem).transform.localScale = Vector3.one;
		((Component)levelSelectItem).transform.localPosition = Vector3.zero;
		return levelSelectItem;
	}
}
