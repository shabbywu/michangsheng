using System;
using System.IO;
using GUIPackage;
using KBEngine;
using script.Steam;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YSGame;
using YSGame.TuJian;

// Token: 0x02000336 RID: 822
public class MainUIMag : MonoBehaviour
{
	// Token: 0x06001C45 RID: 7237 RVA: 0x000CA68C File Offset: 0x000C888C
	private void Awake()
	{
		if (SetFaceUI.Inst != null)
		{
			Object.Destroy(SetFaceUI.Inst.gameObject);
		}
		if (PanelMamager.inst.UISceneGameObject != null)
		{
			Object.Destroy(PanelMamager.inst.UISceneGameObject);
		}
		if (PanelMamager.inst.UIBlackMaskGameObject != null)
		{
			Object.Destroy(PanelMamager.inst.UIBlackMaskGameObject);
		}
		MainUIMag.inst = this;
		this.SetPanel = new MainConfig(this.mainPanel.transform.Find("ConfigPanel").gameObject);
		this.gameVersionText.text = "当前版本：" + clientApp.VersionStr;
		Debug.Log(string.Format("MainUIMag.Awake:{0}", DateTime.Now));
		MusicMag.instance.playMusic(0);
		this.RefreshSave();
	}

	// Token: 0x06001C46 RID: 7238 RVA: 0x000CA766 File Offset: 0x000C8966
	public void RefreshSave()
	{
		this.InitMaxLevel();
		this.selectAvatarPanel.RefreshSaveSlot();
		this.dfPanel.RefreshSaveSlot();
	}

	// Token: 0x06001C47 RID: 7239 RVA: 0x000CA784 File Offset: 0x000C8984
	public void OpenWorkShop()
	{
		if (PlayerPrefs.GetInt("HasReadWorkShop", 0) == 0)
		{
			USelectBox.Show("在创意工坊中启用某些模组可能会影响游戏稳定性，包括但不限于闪退、卡顿、坏档等问题。\n是否继续打开？", delegate
			{
				PlayerPrefs.SetInt("HasReadWorkShop", 1);
				WorkShopMag.Open();
			}, null);
			return;
		}
		WorkShopMag.Open();
	}

	// Token: 0x06001C48 RID: 7240 RVA: 0x000CA7C4 File Offset: 0x000C89C4
	public void CheckModFile()
	{
		try
		{
			if (!new FileInfo(Application.dataPath + "/../winhttp.dll").Exists)
			{
				this.ModBtn.gameObject.SetActive(true);
				this.ModBtn.mouseUpEvent.RemoveAllListeners();
				this.ModBtn.mouseUpEvent.AddListener(new UnityAction(this.ModBtnEvent));
			}
			else
			{
				this.ModBtn.gameObject.SetActive(false);
			}
		}
		catch (Exception ex)
		{
			Debug.LogException(ex);
		}
	}

	// Token: 0x06001C49 RID: 7241 RVA: 0x000CA858 File Offset: 0x000C8A58
	private void ModBtnEvent()
	{
		try
		{
			USelectBox.Show("是否启用Mod框架？", new UnityAction(this.ModBtnEvent_Select), null);
		}
		catch (Exception ex)
		{
			Debug.LogException(ex);
		}
	}

	// Token: 0x06001C4A RID: 7242 RVA: 0x000CA898 File Offset: 0x000C8A98
	private void ModBtnEvent_Select()
	{
		try
		{
			FileInfo fileInfo = new FileInfo(Application.dataPath + "/../winhttp.dll");
			FileInfo fileInfo2 = new FileInfo(Application.dataPath + "/../OtherFile/winhttp.dll");
			FileInfo fileInfo3 = new FileInfo(Application.dataPath + "/../BepInEx/config/BepInEx.cfg");
			FileInfo fileInfo4 = new FileInfo(Application.dataPath + "/../OtherFile/BepInEx.cfg");
			if (fileInfo2.Exists)
			{
				File.Copy(fileInfo2.FullName, fileInfo.FullName, true);
				File.Copy(fileInfo4.FullName, fileInfo3.FullName, true);
			}
			UCheckBox.Show("Mod框架启用完毕，请重新启动游戏", new UnityAction(this.ModBtnEvent_Check));
		}
		catch (Exception ex)
		{
			UCheckBox.Show("启用Mod框架时出现异常:" + ex.Message + "\n请检查游戏完整性重试", new UnityAction(this.ModBtnEvent_Check));
			Debug.LogException(ex);
		}
	}

	// Token: 0x06001C4B RID: 7243 RVA: 0x00049258 File Offset: 0x00047458
	private void ModBtnEvent_Check()
	{
		Application.Quit();
	}

	// Token: 0x06001C4C RID: 7244 RVA: 0x000CA980 File Offset: 0x000C8B80
	private void Start()
	{
		if (!MainUIMag.isSendGameStartEvent)
		{
			MainUIMag.isSendGameStartEvent = true;
			MessageMag.Instance.Send(MessageName.MSG_GameInitFinish, null);
		}
		if (Screen.width <= 400)
		{
			UIPopTip.Inst.Pop("检测到当前分辨率异常，自动重置", PopTipIconType.叹号);
			SystemConfig.Inst.Reset();
		}
	}

	// Token: 0x06001C4D RID: 7245 RVA: 0x000CA9D0 File Offset: 0x000C8BD0
	public void StartGame()
	{
		this.mainPanel.SetActive(false);
		this.selectAvatarPanel.Init();
	}

	// Token: 0x06001C4E RID: 7246 RVA: 0x000CA9E9 File Offset: 0x000C8BE9
	public void DoFaClick()
	{
		this.dfPanel.Init();
	}

	// Token: 0x06001C4F RID: 7247 RVA: 0x000CA9F6 File Offset: 0x000C8BF6
	public void OpenTuJian()
	{
		TuJianManager.Inst.OpenTuJian();
	}

	// Token: 0x06001C50 RID: 7248 RVA: 0x000CAA02 File Offset: 0x000C8C02
	public void GameSet()
	{
		this.SetPanel.Show();
	}

	// Token: 0x06001C51 RID: 7249 RVA: 0x00049258 File Offset: 0x00047458
	public void QuitGame()
	{
		Application.Quit();
	}

	// Token: 0x06001C52 RID: 7250 RVA: 0x000CAA0F File Offset: 0x000C8C0F
	private void OnDestroy()
	{
		MainUIMag.inst = null;
	}

	// Token: 0x06001C53 RID: 7251 RVA: 0x000CAA18 File Offset: 0x000C8C18
	public void startGame(int id, int index, int DFIndex = -1)
	{
		Tools.instance.IsCanLoadSetTalk = false;
		MusicMag.instance.stopMusic();
		this.addAvatar(id, index);
		Tools.instance.getPlayer().Load(id, index);
		Tools.instance.getPlayer().nomelTaskMag.restAllTaskType();
		Avatar player = Tools.instance.getPlayer();
		if (player.age > player.shouYuan)
		{
			UIDeath.Inst.Show(DeathType.寿元已尽);
			return;
		}
		if (Tools.instance.getPlayer().lastScence.Equals("LoadingScreen") || Tools.instance.getPlayer().lastScence.Equals("") || Tools.instance.getPlayer().lastScence.Equals("MainMenu"))
		{
			Tools.instance.getPlayer().lastScence = "AllMaps";
		}
		if (DFIndex > 0)
		{
			Tools.instance.IsInDF = true;
			Tools.instance.getPlayer().lastScence = "S" + DFIndex;
		}
		else
		{
			Tools.instance.IsInDF = false;
		}
		PlayerPrefs.SetString("sceneToLoad", Tools.instance.getPlayer().lastScence);
		Tools.instance.IsLoadData = true;
		Object.FindObjectOfType<Fader>();
		if (DFIndex > 0)
		{
			FactoryManager.inst.loadPlayerDateFactory.isLoadComplete = true;
		}
		else
		{
			Tools.instance.IsLoadData = true;
			FactoryManager.inst.loadPlayerDateFactory.LoadPlayerData(id, index);
			Tools.instance.ResetEquipSeid();
		}
		SceneManager.LoadScene("LoadingScreen");
	}

	// Token: 0x06001C54 RID: 7252 RVA: 0x000CAB9C File Offset: 0x000C8D9C
	private void addAvatar(int id, int index)
	{
		this.creatAvatar(10, 51, 40, new Vector3(-5f, -1.7f, -1f), new Vector3(0f, 0f, 80f), 1);
		KBEngineApp.app.entity_id = 10;
		Avatar avatar = (Avatar)KBEngineApp.app.player();
		Tools.GetValue<Avatar>("Avatar" + Tools.instance.getSaveID(id, index), avatar);
		this.initSkill();
		jsonData.instance.loadAvatarFace(id, index);
		StaticSkill.resetSeid(avatar);
		WuDaoStaticSkill.resetWuDaoSeid(avatar);
		JieDanSkill.resetJieDanSeid(avatar);
		PlayerPrefs.SetInt("NowPlayerFileAvatar", id);
		avatar.seaNodeMag.INITSEA();
	}

	// Token: 0x06001C55 RID: 7253 RVA: 0x000CAC50 File Offset: 0x000C8E50
	private void initSkill()
	{
		Avatar avatar = (Avatar)KBEngineApp.app.player();
		avatar.equipSkillList = avatar.configEquipSkill[avatar.nowConfigEquipSkill];
		avatar.equipStaticSkillList = avatar.configEquipStaticSkill[avatar.nowConfigEquipStaticSkill];
	}

	// Token: 0x06001C56 RID: 7254 RVA: 0x000CAC94 File Offset: 0x000C8E94
	private void setAvatar(int avaterID, int roleType, int HP_Max, Vector3 position, Vector3 direction, Avatar avatar, int AvatarID = 1)
	{
		avatar.position = position;
		avatar.direction = direction;
		JSONObject jsonobject = jsonData.instance.AvatarJsonData[string.Concat(AvatarID)];
		int num = 0;
		foreach (JSONObject jsonobject2 in jsonobject["skills"].list)
		{
			avatar.addHasSkillList((int)jsonobject2.n);
			avatar.equipSkill((int)jsonobject2.n, num);
			num++;
		}
		int num2 = 0;
		foreach (JSONObject jsonobject3 in jsonobject["staticSkills"].list)
		{
			avatar.addHasStaticSkillList((int)jsonobject3.n, 1);
			avatar.equipStaticSkill((int)jsonobject3.n, num2);
			num2++;
		}
		for (int j = 0; j < jsonobject["LingGen"].Count; j++)
		{
			int item = (int)jsonobject["LingGen"][j].n;
			avatar.LingGeng.Add(item);
		}
		avatar.ZiZhi = (int)jsonobject["ziZhi"].n;
		avatar.dunSu = (int)jsonobject["dunSu"].n;
		avatar.wuXin = (uint)jsonobject["wuXin"].n;
		avatar.shengShi = (int)jsonobject["shengShi"].n;
		avatar.shaQi = (uint)jsonobject["shaQi"].n;
		avatar.shouYuan = (uint)jsonobject["shouYuan"].n;
		avatar.age = (uint)jsonobject["age"].n;
		avatar.HP_Max = (int)jsonobject["HP"].n;
		avatar.HP = (int)jsonobject["HP"].n;
		avatar.money = (ulong)((uint)jsonobject["MoneyType"].n);
		avatar.level = (ushort)jsonobject["Level"].n;
		avatar.AvatarType = (uint)((ushort)jsonobject["AvatarType"].n);
		avatar.roleTypeCell = (uint)jsonobject["fightFace"].n;
		avatar.roleType = (uint)jsonobject["face"].n;
		avatar.Sex = (int)jsonobject["SexType"].n;
		avatar.configEquipSkill[0] = avatar.equipSkillList;
		avatar.configEquipStaticSkill[0] = avatar.equipStaticSkillList;
		avatar.equipItemList.values.ForEach(delegate(ITEM_INFO i)
		{
			avatar.configEquipItem[0].values.Add(i);
		});
	}

	// Token: 0x06001C57 RID: 7255 RVA: 0x000CB014 File Offset: 0x000C9214
	public void creatAvatar(int avaterID, int roleType, int HP_Max, Vector3 position, Vector3 direction, int AvatarID = 1)
	{
		KBEngineApp.app.Client_onCreatedProxies((ulong)((long)avaterID), avaterID, "Avatar");
		Avatar avatar = (Avatar)KBEngineApp.app.entities[avaterID];
		this.setAvatar(avaterID, roleType, HP_Max, position, direction, avatar, AvatarID);
	}

	// Token: 0x06001C58 RID: 7256 RVA: 0x000CB05C File Offset: 0x000C925C
	private void InitMaxLevel()
	{
		this.maxLevel = 0;
		if (YSSaveGame.HasFile(Paths.GetSavePath(), "MaxLevelJson"))
		{
			this.maxLevel = YSSaveGame.GetInt("MaxLevelJson", 0);
		}
		if (YSNewSaveSystem.HasFile(Paths.GetNewSavePath() + "/MaxLevelJson.txt"))
		{
			this.maxLevel = YSNewSaveSystem.LoadInt("MaxLevelJson.txt", false);
		}
		if (this.maxLevel == 0)
		{
			for (int i = 0; i < this.maxNum; i++)
			{
				for (int j = 0; j < this.smallDataNum; j++)
				{
					try
					{
						JSONObject jsonobject = null;
						if (YSNewSaveSystem.HasFile(Paths.GetNewSavePath() + "/" + YSNewSaveSystem.GetAvatarSavePathPre(i, j) + "/AvatarInfo.json"))
						{
							jsonobject = YSNewSaveSystem.LoadJSONObject(YSNewSaveSystem.GetAvatarSavePathPre(i, j) + "/AvatarInfo.json", true);
						}
						else if (YSSaveGame.HasFile(Paths.GetSavePath(), "AvatarInfo" + Tools.instance.getSaveID(i, j)))
						{
							jsonobject = YSSaveGame.GetJsonObject("AvatarInfo" + Tools.instance.getSaveID(i, j), null);
						}
						if (jsonobject != null && jsonobject["avatarLevel"].I > MainUIMag.inst.maxLevel)
						{
							this.maxLevel = jsonobject["avatarLevel"].I;
						}
					}
					catch (Exception)
					{
					}
				}
			}
			YSNewSaveSystem.Save("MaxLevelJson.txt", this.maxLevel, false);
		}
	}

	// Token: 0x06001C59 RID: 7257 RVA: 0x000CB1CC File Offset: 0x000C93CC
	private void Update()
	{
		if (Input.GetKeyDown(292))
		{
			SystemConfig.Inst.Reset();
		}
	}

	// Token: 0x040016C6 RID: 5830
	public static MainUIMag inst;

	// Token: 0x040016C7 RID: 5831
	[SerializeField]
	private Text gameVersionText;

	// Token: 0x040016C8 RID: 5832
	public FpBtn ModBtn;

	// Token: 0x040016C9 RID: 5833
	public GameObject mainPanel;

	// Token: 0x040016CA RID: 5834
	public MainUISelectAvatar selectAvatarPanel;

	// Token: 0x040016CB RID: 5835
	public MainUICreateAvatar createAvatarPanel;

	// Token: 0x040016CC RID: 5836
	public MainUIDF dfPanel;

	// Token: 0x040016CD RID: 5837
	public MainConfig SetPanel;

	// Token: 0x040016CE RID: 5838
	public MainUITooltip tooltip;

	// Token: 0x040016CF RID: 5839
	public int maxNum;

	// Token: 0x040016D0 RID: 5840
	public int smallDataNum;

	// Token: 0x040016D1 RID: 5841
	public int maxLevel;

	// Token: 0x040016D2 RID: 5842
	private static bool isSendGameStartEvent;
}
