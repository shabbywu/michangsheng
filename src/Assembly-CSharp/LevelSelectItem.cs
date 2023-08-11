using System.Collections.Generic;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using YSGame;

public class LevelSelectItem : MonoBehaviour
{
	[Header("UI References")]
	public Image LevelImage;

	public Text levelCount;

	public Text levelName;

	public Text levelSubName;

	public Text LevelLv;

	public GameObject lockIcon;

	public bool isLocked;

	public string levelToLoad;

	public bool hasAvatar;

	public int index;

	public int SelectType;

	public PlayerSetRandomFace playerSetRandomFace;

	public bool isDF;

	public Image Level_Image;

	public GameObject HasAvatarItem;

	public GameObject NoAvatarItem;

	public Text Level_Text;

	private LevelSelectManager levelSelectManager;

	private void Start()
	{
		levelSelectManager = GameObject.Find("Main Menu/MainMenuCanvas").GetComponent<LevelSelectManager>();
	}

	public void setLevelItem(Sprite levelImg, int levelC, string levelNm, bool hasSubName, string levelSubName, string levelToLoad, bool locked, bool hasavatar, int _index)
	{
		((Component)this).gameObject.SetActive(true);
		LevelImage.sprite = levelImg;
		levelCount.text = string.Concat(levelC);
		levelName.text = levelNm;
		hasAvatar = hasavatar;
		index = _index;
		playerSetRandomFace.faceID = _index;
		if (hasSubName)
		{
			this.levelSubName.text = levelSubName;
		}
		else
		{
			((Component)this.levelSubName).gameObject.SetActive(false);
		}
		isLocked = locked;
		this.levelToLoad = levelToLoad;
		if (isLocked)
		{
			lockIcon.SetActive(true);
		}
		else
		{
			lockIcon.SetActive(false);
		}
		if (hasAvatar)
		{
			HasAvatarItem.SetActive(true);
			NoAvatarItem.SetActive(false);
			if (YSSaveGame.HasFile(Paths.GetSavePath(), "AvatarInfo" + Tools.instance.getSaveID(index, 0)))
			{
				int i = YSSaveGame.GetJsonObject("AvatarInfo" + Tools.instance.getSaveID(index, 0))["avatarLevel"].I;
				((Component)((Component)this).transform.Find("Text")).GetComponent<Text>().text = Tools.Code64(jsonData.instance.LevelUpDataJsonData[i.ToString()]["Name"].str);
				if ((Object)(object)Level_Image != (Object)null)
				{
					((Component)Level_Image).gameObject.SetActive(true);
					Level_Image.sprite = ResManager.inst.LoadSprite($"NewUI/Fight/LevelIcon/icon_{i}");
				}
				if ((Object)(object)Level_Text != (Object)null)
				{
					((Component)Level_Text).gameObject.SetActive(true);
				}
				if (CreateAvatarMag.inst.maxLevel < i)
				{
					CreateAvatarMag.inst.maxLevel = i;
				}
				return;
			}
			Avatar avatar = new Avatar();
			Tools.GetValue<Avatar>("Avatar" + Tools.instance.getSaveID(index, 0), avatar);
			((Component)((Component)this).transform.Find("Text")).GetComponent<Text>().text = Tools.Code64(jsonData.instance.LevelUpDataJsonData[avatar.level.ToString()]["Name"].str);
			if ((Object)(object)Level_Image != (Object)null)
			{
				((Component)Level_Image).gameObject.SetActive(true);
				Level_Image.sprite = ResManager.inst.LoadSprite($"NewUI/Fight/LevelIcon/icon_{avatar.level}");
			}
			if ((Object)(object)Level_Text != (Object)null)
			{
				((Component)Level_Text).gameObject.SetActive(true);
			}
			if (isDF)
			{
				((Component)((Component)this).transform.Find("Add")).gameObject.SetActive(false);
				levelName.text = Tools.Code64(jsonData.instance.AvatarJsonData[(10001 + (_index - 100)).ToString()]["Name"].str);
			}
			JSONObject jSONObject = new JSONObject();
			jSONObject.SetField("firstName", avatar.firstName);
			jSONObject.SetField("lastName", avatar.lastName);
			jSONObject.SetField("gameTime", avatar.worldTimeMag.nowTime);
			jSONObject.SetField("avatarLevel", avatar.level);
			if (CreateAvatarMag.inst.maxLevel < avatar.level)
			{
				CreateAvatarMag.inst.maxLevel = avatar.level;
			}
			YSSaveGame.save("AvatarInfo" + Tools.instance.getSaveID(index, 0), jSONObject, Paths.GetSavePath());
		}
		else
		{
			HasAvatarItem.SetActive(false);
			NoAvatarItem.SetActive(true);
			if (isDF)
			{
				((Component)((Component)this).transform.Find("dengji")).gameObject.SetActive(true);
				((Component)((Component)this).transform.Find("Text")).gameObject.SetActive(true);
				((Component)((Component)this).transform.Find("Image")).gameObject.SetActive(true);
				((Component)((Component)this).transform.Find("LevelNameContainer")).gameObject.SetActive(true);
				((Component)((Component)this).transform.Find("Text")).GetComponent<Text>().text = Tools.Code64(jsonData.instance.LevelUpDataJsonData[((_index - 99) * 3).ToString()]["Name"].str);
			}
			if ((Object)(object)Level_Image != (Object)null)
			{
				((Component)Level_Image).gameObject.SetActive(false);
			}
			if ((Object)(object)Level_Text != (Object)null)
			{
				((Component)Level_Text).gameObject.SetActive(false);
			}
		}
	}

	public void NewSetName()
	{
		if (!hasAvatar)
		{
			showSetPlayerName();
		}
	}

	public void openSetName()
	{
		if (!hasAvatar)
		{
			showSetPlayerName();
		}
		else
		{
			selectBox.instence.setChoice("是否将该角色打入轮回", new EventDelegate(clickRemoveOk), null);
		}
	}

	public void openLoad()
	{
		if (hasAvatar)
		{
			PlayerPrefs.SetInt("NowPlayerFileAvatar", index);
			MainMenuController mainMenuController = Object.FindObjectOfType<MainMenuController>();
			AvatarInfoFile componentInChildren = mainMenuController.LoadAvatarCellPanel.GetComponentInChildren<AvatarInfoFile>();
			mainMenuController.LoadAvatarCellPanel.gameObject.SetActive(true);
			componentInChildren.showLoad();
		}
	}

	public void LoadDF()
	{
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		if (!hasAvatar)
		{
			return;
		}
		PlayerPrefs.SetInt("NowPlayerFileAvatar", index);
		int @int = PlayerPrefs.GetInt("NowPlayerFileAvatar");
		KBEngineApp.app.entities.Remove(10);
		GameObject val = new GameObject();
		val.AddComponent<StartGame>();
		if (YSSaveGame.GetInt("SaveDFAvatar" + index) == 2)
		{
			YSSaveGame.save("SaveDFAvatar" + index, 1);
			MusicMag.instance.stopMusic();
			firesetSetDFInfo();
			PlayerPrefs.SetString("sceneToLoad", "S" + (10000 + index - 100));
		}
		val.GetComponent<StartGame>().startGame(@int, 0, 10000 + index - 100);
		Avatar player = Tools.instance.getPlayer();
		for (int i = 10101; i <= 10105; i++)
		{
			jsonData.instance.setMonstarDeath(i);
		}
		foreach (JSONObject item in jsonData.instance.WuDaoAllTypeJson.list)
		{
			player.wuDaoMag.SetWuDaoEx(item["id"].I, 999999);
		}
		foreach (KeyValuePair<string, JSONObject> skill2 in jsonData.instance.skillJsonData)
		{
			if ((int)skill2.Value["DF"].n == 1 && player.hasSkillList.Find((SkillItem aa) => aa.itemId == skill2.Value["Skill_ID"].I) == null)
			{
				player.addHasSkillList(skill2.Value["Skill_ID"].I);
			}
		}
		foreach (JSONObject skill in jsonData.instance.StaticSkillJsonData.list)
		{
			if ((int)skill["DF"].n == 1 && player.hasStaticSkillList.Find((SkillItem aa) => aa.itemId == skill["Skill_ID"].I) == null)
			{
				int level = (int)jsonData.instance.StaticLVToLevelJsonData[player.getLevelType().ToString()]["Max" + (int)skill["Skill_LV"].n].n;
				player.addHasStaticSkillList(skill["Skill_ID"].I, level);
			}
		}
	}

	public void clickRemoveOk()
	{
		levelSelectManager.closeRemoveAvatar();
		HasAvatarItem.SetActive(false);
		NoAvatarItem.SetActive(true);
		hasAvatar = false;
		for (int i = 0; i < 6; i++)
		{
			if (YSSaveGame.GetInt("SaveAvatar" + Tools.instance.getSaveID(index, i)) == 1)
			{
				YSSaveGame.save("SaveAvatar" + Tools.instance.getSaveID(index, i), 0);
			}
		}
		YSSaveGame.save("FirstSetAvatarRandomJsonData" + Tools.instance.getSaveID(index, 0), 0);
		YSSaveGame.save("SaveAvatar" + index, 0);
		YSSaveGame.save("PlayerAvatarName" + index, "空");
		if ((Object)(object)Level_Image != (Object)null)
		{
			((Component)Level_Image).gameObject.SetActive(false);
		}
		if ((Object)(object)Level_Text != (Object)null)
		{
			((Component)Level_Text).gameObject.SetActive(false);
		}
		showSetPlayerName();
	}

	public void showSetPlayerName()
	{
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Expected O, but got Unknown
		GameObject setPlayerName = levelSelectManager.setPlayerName;
		setPlayerName.gameObject.SetActive(true);
		levelSelectManager.randomPlayerName();
		Button component = ((Component)setPlayerName.transform.Find("ok")).GetComponent<Button>();
		((UnityEventBase)component.onClick).RemoveAllListeners();
		((UnityEvent)component.onClick).AddListener(new UnityAction(clickOk));
	}

	public void clickOk()
	{
		CreateAvatarMag.inst.startGameClick(loadThisLevel);
	}

	public void loadThisLevel()
	{
		if (!isLocked)
		{
			PlayerPrefs.SetString("sceneToLoad", levelToLoad);
			loadScreen();
		}
	}

	public void loadScreen()
	{
		Tools.instance.isNewAvatar = true;
		MusicMag.instance.stopMusic();
		FirstsetPlayerInfo();
	}

	public void firesetSetDFInfo()
	{
		YSSaveGame.save("FirstSetAvatarRandomJsonData" + Tools.instance.getSaveID(index, 0), 0);
		GameObject.Find("Main Menu").GetComponent<StartGame>().AddDouFaPlayerInfo(index - 100);
	}

	public void FirstsetPlayerInfo()
	{
		YSSaveGame.save("FirstSetAvatarRandomJsonData" + Tools.instance.getSaveID(index, 0), 0);
		GameObject.Find("Main Menu").GetComponent<StartGame>().firstAddAvatar(index, 0, levelSelectManager.getFirstName(), levelSelectManager.getLastName());
	}
}
