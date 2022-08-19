using System;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using YSGame;

// Token: 0x02000104 RID: 260
public class LevelSelectItem : MonoBehaviour
{
	// Token: 0x06000BE6 RID: 3046 RVA: 0x00047F81 File Offset: 0x00046181
	private void Start()
	{
		this.levelSelectManager = GameObject.Find("Main Menu/MainMenuCanvas").GetComponent<LevelSelectManager>();
	}

	// Token: 0x06000BE7 RID: 3047 RVA: 0x00047F98 File Offset: 0x00046198
	public void setLevelItem(Sprite levelImg, int levelC, string levelNm, bool hasSubName, string levelSubName, string levelToLoad, bool locked, bool hasavatar, int _index)
	{
		base.gameObject.SetActive(true);
		this.LevelImage.sprite = levelImg;
		this.levelCount.text = string.Concat(levelC);
		this.levelName.text = levelNm;
		this.hasAvatar = hasavatar;
		this.index = _index;
		this.playerSetRandomFace.faceID = _index;
		if (hasSubName)
		{
			this.levelSubName.text = levelSubName;
		}
		else
		{
			this.levelSubName.gameObject.SetActive(false);
		}
		this.isLocked = locked;
		this.levelToLoad = levelToLoad;
		if (this.isLocked)
		{
			this.lockIcon.SetActive(true);
		}
		else
		{
			this.lockIcon.SetActive(false);
		}
		if (this.hasAvatar)
		{
			this.HasAvatarItem.SetActive(true);
			this.NoAvatarItem.SetActive(false);
			if (!YSSaveGame.HasFile(Paths.GetSavePath(), "AvatarInfo" + Tools.instance.getSaveID(this.index, 0)))
			{
				Avatar avatar = new Avatar();
				Tools.GetValue<Avatar>("Avatar" + Tools.instance.getSaveID(this.index, 0), avatar);
				base.transform.Find("Text").GetComponent<Text>().text = Tools.Code64(jsonData.instance.LevelUpDataJsonData[avatar.level.ToString()]["Name"].str);
				if (this.Level_Image != null)
				{
					this.Level_Image.gameObject.SetActive(true);
					this.Level_Image.sprite = ResManager.inst.LoadSprite(string.Format("NewUI/Fight/LevelIcon/icon_{0}", avatar.level));
				}
				if (this.Level_Text != null)
				{
					this.Level_Text.gameObject.SetActive(true);
				}
				if (this.isDF)
				{
					base.transform.Find("Add").gameObject.SetActive(false);
					this.levelName.text = Tools.Code64(jsonData.instance.AvatarJsonData[(10001 + (_index - 100)).ToString()]["Name"].str);
				}
				JSONObject jsonobject = new JSONObject();
				jsonobject.SetField("firstName", avatar.firstName);
				jsonobject.SetField("lastName", avatar.lastName);
				jsonobject.SetField("gameTime", avatar.worldTimeMag.nowTime);
				jsonobject.SetField("avatarLevel", (int)avatar.level);
				if (CreateAvatarMag.inst.maxLevel < (int)avatar.level)
				{
					CreateAvatarMag.inst.maxLevel = (int)avatar.level;
				}
				YSSaveGame.save("AvatarInfo" + Tools.instance.getSaveID(this.index, 0), jsonobject, Paths.GetSavePath());
				return;
			}
			int i = YSSaveGame.GetJsonObject("AvatarInfo" + Tools.instance.getSaveID(this.index, 0), null)["avatarLevel"].I;
			base.transform.Find("Text").GetComponent<Text>().text = Tools.Code64(jsonData.instance.LevelUpDataJsonData[i.ToString()]["Name"].str);
			if (this.Level_Image != null)
			{
				this.Level_Image.gameObject.SetActive(true);
				this.Level_Image.sprite = ResManager.inst.LoadSprite(string.Format("NewUI/Fight/LevelIcon/icon_{0}", i));
			}
			if (this.Level_Text != null)
			{
				this.Level_Text.gameObject.SetActive(true);
			}
			if (CreateAvatarMag.inst.maxLevel < i)
			{
				CreateAvatarMag.inst.maxLevel = i;
				return;
			}
		}
		else
		{
			this.HasAvatarItem.SetActive(false);
			this.NoAvatarItem.SetActive(true);
			if (this.isDF)
			{
				base.transform.Find("dengji").gameObject.SetActive(true);
				base.transform.Find("Text").gameObject.SetActive(true);
				base.transform.Find("Image").gameObject.SetActive(true);
				base.transform.Find("LevelNameContainer").gameObject.SetActive(true);
				base.transform.Find("Text").GetComponent<Text>().text = Tools.Code64(jsonData.instance.LevelUpDataJsonData[((_index - 99) * 3).ToString()]["Name"].str);
			}
			if (this.Level_Image != null)
			{
				this.Level_Image.gameObject.SetActive(false);
			}
			if (this.Level_Text != null)
			{
				this.Level_Text.gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x06000BE8 RID: 3048 RVA: 0x0004847F File Offset: 0x0004667F
	public void NewSetName()
	{
		if (!this.hasAvatar)
		{
			this.showSetPlayerName();
		}
	}

	// Token: 0x06000BE9 RID: 3049 RVA: 0x0004848F File Offset: 0x0004668F
	public void openSetName()
	{
		if (!this.hasAvatar)
		{
			this.showSetPlayerName();
			return;
		}
		selectBox.instence.setChoice("是否将该角色打入轮回", new EventDelegate(new EventDelegate.Callback(this.clickRemoveOk)), null);
	}

	// Token: 0x06000BEA RID: 3050 RVA: 0x000484C4 File Offset: 0x000466C4
	public void openLoad()
	{
		if (!this.hasAvatar)
		{
			return;
		}
		PlayerPrefs.SetInt("NowPlayerFileAvatar", this.index);
		MainMenuController mainMenuController = Object.FindObjectOfType<MainMenuController>();
		AvatarInfoFile componentInChildren = mainMenuController.LoadAvatarCellPanel.GetComponentInChildren<AvatarInfoFile>();
		mainMenuController.LoadAvatarCellPanel.gameObject.SetActive(true);
		componentInChildren.showLoad();
	}

	// Token: 0x06000BEB RID: 3051 RVA: 0x00048514 File Offset: 0x00046714
	public void LoadDF()
	{
		if (!this.hasAvatar)
		{
			return;
		}
		PlayerPrefs.SetInt("NowPlayerFileAvatar", this.index);
		int @int = PlayerPrefs.GetInt("NowPlayerFileAvatar");
		KBEngineApp.app.entities.Remove(10);
		GameObject gameObject = new GameObject();
		gameObject.AddComponent<StartGame>();
		if (YSSaveGame.GetInt("SaveDFAvatar" + this.index, 0) == 2)
		{
			YSSaveGame.save("SaveDFAvatar" + this.index, 1, "-1");
			MusicMag.instance.stopMusic();
			this.firesetSetDFInfo();
			PlayerPrefs.SetString("sceneToLoad", "S" + (10000 + this.index - 100));
		}
		gameObject.GetComponent<StartGame>().startGame(@int, 0, 10000 + this.index - 100);
		Avatar player = Tools.instance.getPlayer();
		for (int i = 10101; i <= 10105; i++)
		{
			jsonData.instance.setMonstarDeath(i, true);
		}
		foreach (JSONObject jsonobject in jsonData.instance.WuDaoAllTypeJson.list)
		{
			player.wuDaoMag.SetWuDaoEx(jsonobject["id"].I, 999999);
		}
		using (Dictionary<string, JSONObject>.Enumerator enumerator2 = jsonData.instance.skillJsonData.GetEnumerator())
		{
			while (enumerator2.MoveNext())
			{
				KeyValuePair<string, JSONObject> skill = enumerator2.Current;
				if ((int)skill.Value["DF"].n == 1 && player.hasSkillList.Find((SkillItem aa) => aa.itemId == skill.Value["Skill_ID"].I) == null)
				{
					player.addHasSkillList(skill.Value["Skill_ID"].I);
				}
			}
		}
		using (List<JSONObject>.Enumerator enumerator = jsonData.instance.StaticSkillJsonData.list.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				JSONObject skill = enumerator.Current;
				if ((int)skill["DF"].n == 1 && player.hasStaticSkillList.Find((SkillItem aa) => aa.itemId == skill["Skill_ID"].I) == null)
				{
					int level = (int)jsonData.instance.StaticLVToLevelJsonData[player.getLevelType().ToString()]["Max" + (int)skill["Skill_LV"].n].n;
					player.addHasStaticSkillList(skill["Skill_ID"].I, level);
				}
			}
		}
	}

	// Token: 0x06000BEC RID: 3052 RVA: 0x00048830 File Offset: 0x00046A30
	public void clickRemoveOk()
	{
		this.levelSelectManager.closeRemoveAvatar();
		this.HasAvatarItem.SetActive(false);
		this.NoAvatarItem.SetActive(true);
		this.hasAvatar = false;
		for (int i = 0; i < 6; i++)
		{
			if (YSSaveGame.GetInt("SaveAvatar" + Tools.instance.getSaveID(this.index, i), 0) == 1)
			{
				YSSaveGame.save("SaveAvatar" + Tools.instance.getSaveID(this.index, i), 0, "-1");
			}
		}
		YSSaveGame.save("FirstSetAvatarRandomJsonData" + Tools.instance.getSaveID(this.index, 0), 0, "-1");
		YSSaveGame.save("SaveAvatar" + this.index, 0, "-1");
		YSSaveGame.save("PlayerAvatarName" + this.index, "空", "-1");
		if (this.Level_Image != null)
		{
			this.Level_Image.gameObject.SetActive(false);
		}
		if (this.Level_Text != null)
		{
			this.Level_Text.gameObject.SetActive(false);
		}
		this.showSetPlayerName();
	}

	// Token: 0x06000BED RID: 3053 RVA: 0x0004896C File Offset: 0x00046B6C
	public void showSetPlayerName()
	{
		GameObject setPlayerName = this.levelSelectManager.setPlayerName;
		setPlayerName.gameObject.SetActive(true);
		this.levelSelectManager.randomPlayerName();
		Button component = setPlayerName.transform.Find("ok").GetComponent<Button>();
		component.onClick.RemoveAllListeners();
		component.onClick.AddListener(new UnityAction(this.clickOk));
	}

	// Token: 0x06000BEE RID: 3054 RVA: 0x000489D0 File Offset: 0x00046BD0
	public void clickOk()
	{
		CreateAvatarMag.inst.startGameClick(new CreateAvatarMag.createAvatardelegate(this.loadThisLevel));
	}

	// Token: 0x06000BEF RID: 3055 RVA: 0x000489E8 File Offset: 0x00046BE8
	public void loadThisLevel()
	{
		if (!this.isLocked)
		{
			PlayerPrefs.SetString("sceneToLoad", this.levelToLoad);
			this.loadScreen();
		}
	}

	// Token: 0x06000BF0 RID: 3056 RVA: 0x00048A08 File Offset: 0x00046C08
	public void loadScreen()
	{
		Tools.instance.isNewAvatar = true;
		MusicMag.instance.stopMusic();
		this.FirstsetPlayerInfo();
	}

	// Token: 0x06000BF1 RID: 3057 RVA: 0x00048A28 File Offset: 0x00046C28
	public void firesetSetDFInfo()
	{
		YSSaveGame.save("FirstSetAvatarRandomJsonData" + Tools.instance.getSaveID(this.index, 0), 0, "-1");
		GameObject.Find("Main Menu").GetComponent<StartGame>().AddDouFaPlayerInfo(this.index - 100);
	}

	// Token: 0x06000BF2 RID: 3058 RVA: 0x00048A78 File Offset: 0x00046C78
	public void FirstsetPlayerInfo()
	{
		YSSaveGame.save("FirstSetAvatarRandomJsonData" + Tools.instance.getSaveID(this.index, 0), 0, "-1");
		GameObject.Find("Main Menu").GetComponent<StartGame>().firstAddAvatar(this.index, 0, this.levelSelectManager.getFirstName(), this.levelSelectManager.getLastName());
	}

	// Token: 0x04000839 RID: 2105
	[Header("UI References")]
	public Image LevelImage;

	// Token: 0x0400083A RID: 2106
	public Text levelCount;

	// Token: 0x0400083B RID: 2107
	public Text levelName;

	// Token: 0x0400083C RID: 2108
	public Text levelSubName;

	// Token: 0x0400083D RID: 2109
	public Text LevelLv;

	// Token: 0x0400083E RID: 2110
	public GameObject lockIcon;

	// Token: 0x0400083F RID: 2111
	public bool isLocked;

	// Token: 0x04000840 RID: 2112
	public string levelToLoad;

	// Token: 0x04000841 RID: 2113
	public bool hasAvatar;

	// Token: 0x04000842 RID: 2114
	public int index;

	// Token: 0x04000843 RID: 2115
	public int SelectType;

	// Token: 0x04000844 RID: 2116
	public PlayerSetRandomFace playerSetRandomFace;

	// Token: 0x04000845 RID: 2117
	public bool isDF;

	// Token: 0x04000846 RID: 2118
	public Image Level_Image;

	// Token: 0x04000847 RID: 2119
	public GameObject HasAvatarItem;

	// Token: 0x04000848 RID: 2120
	public GameObject NoAvatarItem;

	// Token: 0x04000849 RID: 2121
	public Text Level_Text;

	// Token: 0x0400084A RID: 2122
	private LevelSelectManager levelSelectManager;
}
