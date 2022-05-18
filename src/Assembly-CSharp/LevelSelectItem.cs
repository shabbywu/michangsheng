using System;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using YSGame;

// Token: 0x02000184 RID: 388
public class LevelSelectItem : MonoBehaviour
{
	// Token: 0x06000CFD RID: 3325 RVA: 0x0000EB9B File Offset: 0x0000CD9B
	private void Start()
	{
		this.levelSelectManager = GameObject.Find("Main Menu/MainMenuCanvas").GetComponent<LevelSelectManager>();
	}

	// Token: 0x06000CFE RID: 3326 RVA: 0x00099D30 File Offset: 0x00097F30
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

	// Token: 0x06000CFF RID: 3327 RVA: 0x0000EBB2 File Offset: 0x0000CDB2
	public void NewSetName()
	{
		if (!this.hasAvatar)
		{
			this.showSetPlayerName();
		}
	}

	// Token: 0x06000D00 RID: 3328 RVA: 0x0000EBC2 File Offset: 0x0000CDC2
	public void openSetName()
	{
		if (!this.hasAvatar)
		{
			this.showSetPlayerName();
			return;
		}
		selectBox.instence.setChoice("是否将该角色打入轮回", new EventDelegate(new EventDelegate.Callback(this.clickRemoveOk)), null);
	}

	// Token: 0x06000D01 RID: 3329 RVA: 0x0009A218 File Offset: 0x00098418
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

	// Token: 0x06000D02 RID: 3330 RVA: 0x0009A268 File Offset: 0x00098468
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
				if ((int)skill.Value["DF"].n == 1 && player.hasSkillList.Find((SkillItem aa) => aa.itemId == (int)skill.Value["Skill_ID"].n) == null)
				{
					player.addHasSkillList((int)skill.Value["Skill_ID"].n);
				}
			}
		}
		using (List<JSONObject>.Enumerator enumerator = jsonData.instance.StaticSkillJsonData.list.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				JSONObject skill = enumerator.Current;
				if ((int)skill["DF"].n == 1 && player.hasStaticSkillList.Find((SkillItem aa) => aa.itemId == (int)skill["Skill_ID"].n) == null)
				{
					int level = (int)jsonData.instance.StaticLVToLevelJsonData[player.getLevelType().ToString()]["Max" + (int)skill["Skill_LV"].n].n;
					player.addHasStaticSkillList((int)skill["Skill_ID"].n, level);
				}
			}
		}
	}

	// Token: 0x06000D03 RID: 3331 RVA: 0x0009A584 File Offset: 0x00098784
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

	// Token: 0x06000D04 RID: 3332 RVA: 0x0009A6C0 File Offset: 0x000988C0
	public void showSetPlayerName()
	{
		GameObject setPlayerName = this.levelSelectManager.setPlayerName;
		setPlayerName.gameObject.SetActive(true);
		this.levelSelectManager.randomPlayerName();
		Button component = setPlayerName.transform.Find("ok").GetComponent<Button>();
		component.onClick.RemoveAllListeners();
		component.onClick.AddListener(new UnityAction(this.clickOk));
	}

	// Token: 0x06000D05 RID: 3333 RVA: 0x0000EBF4 File Offset: 0x0000CDF4
	public void clickOk()
	{
		CreateAvatarMag.inst.startGameClick(new CreateAvatarMag.createAvatardelegate(this.loadThisLevel));
	}

	// Token: 0x06000D06 RID: 3334 RVA: 0x0000EC0C File Offset: 0x0000CE0C
	public void loadThisLevel()
	{
		if (!this.isLocked)
		{
			PlayerPrefs.SetString("sceneToLoad", this.levelToLoad);
			this.loadScreen();
		}
	}

	// Token: 0x06000D07 RID: 3335 RVA: 0x0000EC2C File Offset: 0x0000CE2C
	public void loadScreen()
	{
		Tools.instance.isNewAvatar = true;
		MusicMag.instance.stopMusic();
		this.FirstsetPlayerInfo();
	}

	// Token: 0x06000D08 RID: 3336 RVA: 0x0009A724 File Offset: 0x00098924
	public void firesetSetDFInfo()
	{
		YSSaveGame.save("FirstSetAvatarRandomJsonData" + Tools.instance.getSaveID(this.index, 0), 0, "-1");
		GameObject.Find("Main Menu").GetComponent<StartGame>().AddDouFaPlayerInfo(this.index - 100);
	}

	// Token: 0x06000D09 RID: 3337 RVA: 0x0009A774 File Offset: 0x00098974
	public void FirstsetPlayerInfo()
	{
		YSSaveGame.save("FirstSetAvatarRandomJsonData" + Tools.instance.getSaveID(this.index, 0), 0, "-1");
		GameObject.Find("Main Menu").GetComponent<StartGame>().firstAddAvatar(this.index, 0, this.levelSelectManager.getFirstName(), this.levelSelectManager.getLastName());
	}

	// Token: 0x04000A2E RID: 2606
	[Header("UI References")]
	public Image LevelImage;

	// Token: 0x04000A2F RID: 2607
	public Text levelCount;

	// Token: 0x04000A30 RID: 2608
	public Text levelName;

	// Token: 0x04000A31 RID: 2609
	public Text levelSubName;

	// Token: 0x04000A32 RID: 2610
	public Text LevelLv;

	// Token: 0x04000A33 RID: 2611
	public GameObject lockIcon;

	// Token: 0x04000A34 RID: 2612
	public bool isLocked;

	// Token: 0x04000A35 RID: 2613
	public string levelToLoad;

	// Token: 0x04000A36 RID: 2614
	public bool hasAvatar;

	// Token: 0x04000A37 RID: 2615
	public int index;

	// Token: 0x04000A38 RID: 2616
	public int SelectType;

	// Token: 0x04000A39 RID: 2617
	public PlayerSetRandomFace playerSetRandomFace;

	// Token: 0x04000A3A RID: 2618
	public bool isDF;

	// Token: 0x04000A3B RID: 2619
	public Image Level_Image;

	// Token: 0x04000A3C RID: 2620
	public GameObject HasAvatarItem;

	// Token: 0x04000A3D RID: 2621
	public GameObject NoAvatarItem;

	// Token: 0x04000A3E RID: 2622
	public Text Level_Text;

	// Token: 0x04000A3F RID: 2623
	private LevelSelectManager levelSelectManager;
}
