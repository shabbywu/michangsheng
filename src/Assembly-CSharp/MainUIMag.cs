using System;
using System.IO;
using System.Runtime.CompilerServices;
using GUIPackage;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YSGame;
using YSGame.TuJian;
using script.Steam;

public class MainUIMag : MonoBehaviour
{
	[Serializable]
	[CompilerGenerated]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9 = new _003C_003Ec();

		public static UnityAction _003C_003E9__15_0;

		internal void _003COpenWorkShop_003Eb__15_0()
		{
			PlayerPrefs.SetInt("HasReadWorkShop", 1);
			WorkShopMag.Open();
		}
	}

	public static MainUIMag inst;

	[SerializeField]
	private Text gameVersionText;

	public FpBtn ModBtn;

	public GameObject mainPanel;

	public MainUISelectAvatar selectAvatarPanel;

	public MainUICreateAvatar createAvatarPanel;

	public MainUIDF dfPanel;

	public MainConfig SetPanel;

	public MainUITooltip tooltip;

	public int maxNum;

	public int smallDataNum;

	public int maxLevel;

	private static bool isSendGameStartEvent;

	private void Awake()
	{
		if ((Object)(object)SetFaceUI.Inst != (Object)null)
		{
			Object.Destroy((Object)(object)((Component)SetFaceUI.Inst).gameObject);
		}
		if ((Object)(object)PanelMamager.inst.UISceneGameObject != (Object)null)
		{
			Object.Destroy((Object)(object)PanelMamager.inst.UISceneGameObject);
		}
		if ((Object)(object)PanelMamager.inst.UIBlackMaskGameObject != (Object)null)
		{
			Object.Destroy((Object)(object)PanelMamager.inst.UIBlackMaskGameObject);
		}
		inst = this;
		SetPanel = new MainConfig(((Component)mainPanel.transform.Find("ConfigPanel")).gameObject);
		gameVersionText.text = "当前版本：" + clientApp.VersionStr;
		Debug.Log((object)$"MainUIMag.Awake:{DateTime.Now}");
		MusicMag.instance.playMusic(0);
		RefreshSave();
	}

	public void RefreshSave()
	{
		InitMaxLevel();
		selectAvatarPanel.RefreshSaveSlot();
		dfPanel.RefreshSaveSlot();
	}

	public void OpenWorkShop()
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Expected O, but got Unknown
		if (PlayerPrefs.GetInt("HasReadWorkShop", 0) == 0)
		{
			object obj = _003C_003Ec._003C_003E9__15_0;
			if (obj == null)
			{
				UnityAction val = delegate
				{
					PlayerPrefs.SetInt("HasReadWorkShop", 1);
					WorkShopMag.Open();
				};
				_003C_003Ec._003C_003E9__15_0 = val;
				obj = (object)val;
			}
			USelectBox.Show("在创意工坊中启用某些模组可能会影响游戏稳定性，包括但不限于闪退、卡顿、坏档等问题。\n是否继续打开？", (UnityAction)obj);
		}
		else
		{
			WorkShopMag.Open();
		}
	}

	public void CheckModFile()
	{
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Expected O, but got Unknown
		try
		{
			if (!new FileInfo(Application.dataPath + "/../winhttp.dll").Exists)
			{
				((Component)ModBtn).gameObject.SetActive(true);
				((UnityEventBase)ModBtn.mouseUpEvent).RemoveAllListeners();
				ModBtn.mouseUpEvent.AddListener(new UnityAction(ModBtnEvent));
			}
			else
			{
				((Component)ModBtn).gameObject.SetActive(false);
			}
		}
		catch (Exception ex)
		{
			Debug.LogException(ex);
		}
	}

	private void ModBtnEvent()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Expected O, but got Unknown
		try
		{
			USelectBox.Show("是否启用Mod框架？", new UnityAction(ModBtnEvent_Select));
		}
		catch (Exception ex)
		{
			Debug.LogException(ex);
		}
	}

	private void ModBtnEvent_Select()
	{
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Expected O, but got Unknown
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Expected O, but got Unknown
		try
		{
			FileInfo fileInfo = new FileInfo(Application.dataPath + "/../winhttp.dll");
			FileInfo fileInfo2 = new FileInfo(Application.dataPath + "/../OtherFile/winhttp.dll");
			FileInfo fileInfo3 = new FileInfo(Application.dataPath + "/../BepInEx/config/BepInEx.cfg");
			FileInfo fileInfo4 = new FileInfo(Application.dataPath + "/../OtherFile/BepInEx.cfg");
			if (fileInfo2.Exists)
			{
				File.Copy(fileInfo2.FullName, fileInfo.FullName, overwrite: true);
				File.Copy(fileInfo4.FullName, fileInfo3.FullName, overwrite: true);
			}
			UCheckBox.Show("Mod框架启用完毕，请重新启动游戏", new UnityAction(ModBtnEvent_Check));
		}
		catch (Exception ex)
		{
			UCheckBox.Show("启用Mod框架时出现异常:" + ex.Message + "\n请检查游戏完整性重试", new UnityAction(ModBtnEvent_Check));
			Debug.LogException(ex);
		}
	}

	private void ModBtnEvent_Check()
	{
		Application.Quit();
	}

	private void Start()
	{
		if (!isSendGameStartEvent)
		{
			isSendGameStartEvent = true;
			MessageMag.Instance.Send(MessageName.MSG_GameInitFinish);
		}
		if (Screen.width <= 400)
		{
			UIPopTip.Inst.Pop("检测到当前分辨率异常，自动重置");
			SystemConfig.Inst.Reset();
		}
	}

	public void StartGame()
	{
		mainPanel.SetActive(false);
		selectAvatarPanel.Init();
	}

	public void DoFaClick()
	{
		dfPanel.Init();
	}

	public void OpenTuJian()
	{
		TuJianManager.Inst.OpenTuJian();
	}

	public void GameSet()
	{
		SetPanel.Show();
	}

	public void QuitGame()
	{
		Application.Quit();
	}

	private void OnDestroy()
	{
		inst = null;
	}

	public void startGame(int id, int index, int DFIndex = -1)
	{
		Tools.instance.IsCanLoadSetTalk = false;
		MusicMag.instance.stopMusic();
		addAvatar(id, index);
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

	private void addAvatar(int id, int index)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		creatAvatar(10, 51, 40, new Vector3(-5f, -1.7f, -1f), new Vector3(0f, 0f, 80f));
		KBEngineApp.app.entity_id = 10;
		Avatar avatar = (Avatar)KBEngineApp.app.player();
		Tools.GetValue<Avatar>("Avatar" + Tools.instance.getSaveID(id, index), avatar);
		initSkill();
		jsonData.instance.loadAvatarFace(id, index);
		StaticSkill.resetSeid(avatar);
		WuDaoStaticSkill.resetWuDaoSeid(avatar);
		JieDanSkill.resetJieDanSeid(avatar);
		PlayerPrefs.SetInt("NowPlayerFileAvatar", id);
		avatar.seaNodeMag.INITSEA();
	}

	private void initSkill()
	{
		Avatar avatar = (Avatar)KBEngineApp.app.player();
		avatar.equipSkillList = avatar.configEquipSkill[avatar.nowConfigEquipSkill];
		avatar.equipStaticSkillList = avatar.configEquipStaticSkill[avatar.nowConfigEquipStaticSkill];
	}

	private void setAvatar(int avaterID, int roleType, int HP_Max, Vector3 position, Vector3 direction, Avatar avatar, int AvatarID = 1)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		avatar.position = position;
		avatar.direction = direction;
		JSONObject jSONObject = jsonData.instance.AvatarJsonData[string.Concat(AvatarID)];
		int num = 0;
		foreach (JSONObject item2 in jSONObject["skills"].list)
		{
			avatar.addHasSkillList((int)item2.n);
			avatar.equipSkill((int)item2.n, num);
			num++;
		}
		int num2 = 0;
		foreach (JSONObject item3 in jSONObject["staticSkills"].list)
		{
			avatar.addHasStaticSkillList((int)item3.n);
			avatar.equipStaticSkill((int)item3.n, num2);
			num2++;
		}
		for (int j = 0; j < jSONObject["LingGen"].Count; j++)
		{
			int item = (int)jSONObject["LingGen"][j].n;
			avatar.LingGeng.Add(item);
		}
		avatar.ZiZhi = (int)jSONObject["ziZhi"].n;
		avatar.dunSu = (int)jSONObject["dunSu"].n;
		avatar.wuXin = (uint)jSONObject["wuXin"].n;
		avatar.shengShi = (int)jSONObject["shengShi"].n;
		avatar.shaQi = (uint)jSONObject["shaQi"].n;
		avatar.shouYuan = (uint)jSONObject["shouYuan"].n;
		avatar.age = (uint)jSONObject["age"].n;
		avatar.HP_Max = (int)jSONObject["HP"].n;
		avatar.HP = (int)jSONObject["HP"].n;
		avatar.money = (uint)jSONObject["MoneyType"].n;
		avatar.level = (ushort)jSONObject["Level"].n;
		avatar.AvatarType = (ushort)jSONObject["AvatarType"].n;
		avatar.roleTypeCell = (uint)jSONObject["fightFace"].n;
		avatar.roleType = (uint)jSONObject["face"].n;
		avatar.Sex = (int)jSONObject["SexType"].n;
		avatar.configEquipSkill[0] = avatar.equipSkillList;
		avatar.configEquipStaticSkill[0] = avatar.equipStaticSkillList;
		avatar.equipItemList.values.ForEach(delegate(ITEM_INFO i)
		{
			avatar.configEquipItem[0].values.Add(i);
		});
	}

	public void creatAvatar(int avaterID, int roleType, int HP_Max, Vector3 position, Vector3 direction, int AvatarID = 1)
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		KBEngineApp.app.Client_onCreatedProxies((ulong)avaterID, avaterID, "Avatar");
		Avatar avatar = (Avatar)KBEngineApp.app.entities[avaterID];
		setAvatar(avaterID, roleType, HP_Max, position, direction, avatar, AvatarID);
	}

	private void InitMaxLevel()
	{
		maxLevel = 0;
		if (YSSaveGame.HasFile(Paths.GetSavePath(), "MaxLevelJson"))
		{
			maxLevel = YSSaveGame.GetInt("MaxLevelJson");
		}
		if (YSNewSaveSystem.HasFile(Paths.GetNewSavePath() + "/MaxLevelJson.txt"))
		{
			maxLevel = YSNewSaveSystem.LoadInt("MaxLevelJson.txt", autoPath: false);
		}
		if (maxLevel != 0)
		{
			return;
		}
		for (int i = 0; i < maxNum; i++)
		{
			for (int j = 0; j < smallDataNum; j++)
			{
				try
				{
					JSONObject jSONObject = null;
					if (YSNewSaveSystem.HasFile(Paths.GetNewSavePath() + "/" + YSNewSaveSystem.GetAvatarSavePathPre(i, j) + "/AvatarInfo.json"))
					{
						jSONObject = YSNewSaveSystem.LoadJSONObject(YSNewSaveSystem.GetAvatarSavePathPre(i, j) + "/AvatarInfo.json");
					}
					else if (YSSaveGame.HasFile(Paths.GetSavePath(), "AvatarInfo" + Tools.instance.getSaveID(i, j)))
					{
						jSONObject = YSSaveGame.GetJsonObject("AvatarInfo" + Tools.instance.getSaveID(i, j));
					}
					if (jSONObject != null && jSONObject["avatarLevel"].I > inst.maxLevel)
					{
						maxLevel = jSONObject["avatarLevel"].I;
					}
				}
				catch (Exception)
				{
				}
			}
		}
		YSNewSaveSystem.Save("MaxLevelJson.txt", maxLevel, autoPath: false);
	}

	private void Update()
	{
		if (Input.GetKeyDown((KeyCode)292))
		{
			SystemConfig.Inst.Reset();
		}
	}
}
