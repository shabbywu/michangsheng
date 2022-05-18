using System;
using System.IO;
using GUIPackage;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YSGame;
using YSGame.TuJian;

// Token: 0x020004A6 RID: 1190
public class MainUIMag : MonoBehaviour
{
	// Token: 0x06001F93 RID: 8083 RVA: 0x0010F524 File Offset: 0x0010D724
	private void Awake()
	{
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
		this.InitMaxLevel();
		MusicMag.instance.playMusic(0);
		this.selectAvatarPanel.Init();
		this.dfPanel.Init();
		this.CheckModFile();
	}

	// Token: 0x06001F94 RID: 8084 RVA: 0x0010F600 File Offset: 0x0010D800
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

	// Token: 0x06001F95 RID: 8085 RVA: 0x0010F694 File Offset: 0x0010D894
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

	// Token: 0x06001F96 RID: 8086 RVA: 0x0010F6D4 File Offset: 0x0010D8D4
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

	// Token: 0x06001F97 RID: 8087 RVA: 0x0000EF72 File Offset: 0x0000D172
	private void ModBtnEvent_Check()
	{
		Application.Quit();
	}

	// Token: 0x06001F98 RID: 8088 RVA: 0x0010F7BC File Offset: 0x0010D9BC
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

	// Token: 0x06001F99 RID: 8089 RVA: 0x0001A102 File Offset: 0x00018302
	public void StartGame()
	{
		this.mainPanel.SetActive(false);
		this.selectAvatarPanel.Init();
	}

	// Token: 0x06001F9A RID: 8090 RVA: 0x0001A11B File Offset: 0x0001831B
	public void DoFaClick()
	{
		this.dfPanel.Init();
	}

	// Token: 0x06001F9B RID: 8091 RVA: 0x0001A128 File Offset: 0x00018328
	public void OpenTuJian()
	{
		TuJianManager.Inst.OpenTuJian();
	}

	// Token: 0x06001F9C RID: 8092 RVA: 0x0001A134 File Offset: 0x00018334
	public void GameSet()
	{
		this.SetPanel.Show();
	}

	// Token: 0x06001F9D RID: 8093 RVA: 0x0000EF72 File Offset: 0x0000D172
	public void QuitGame()
	{
		Application.Quit();
	}

	// Token: 0x06001F9E RID: 8094 RVA: 0x0001A141 File Offset: 0x00018341
	private void OnDestroy()
	{
		MainUIMag.inst = null;
	}

	// Token: 0x06001F9F RID: 8095 RVA: 0x0010F80C File Offset: 0x0010DA0C
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
			FactoryManager.inst.loadPlayerDateFactory.LoadPlayerDate(id, index);
			Tools.instance.ResetEquipSeid();
		}
		SceneManager.LoadScene("LoadingScreen");
	}

	// Token: 0x06001FA0 RID: 8096 RVA: 0x0010F990 File Offset: 0x0010DB90
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

	// Token: 0x06001FA1 RID: 8097 RVA: 0x0010FA44 File Offset: 0x0010DC44
	private void initSkill()
	{
		Avatar avatar = (Avatar)KBEngineApp.app.player();
		avatar.equipSkillList = avatar.configEquipSkill[avatar.nowConfigEquipSkill];
		avatar.equipStaticSkillList = avatar.configEquipStaticSkill[avatar.nowConfigEquipStaticSkill];
	}

	// Token: 0x06001FA2 RID: 8098 RVA: 0x0010FA88 File Offset: 0x0010DC88
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

	// Token: 0x06001FA3 RID: 8099 RVA: 0x0010FE08 File Offset: 0x0010E008
	public void creatAvatar(int avaterID, int roleType, int HP_Max, Vector3 position, Vector3 direction, int AvatarID = 1)
	{
		KBEngineApp.app.Client_onCreatedProxies((ulong)((long)avaterID), avaterID, "Avatar");
		Avatar avatar = (Avatar)KBEngineApp.app.entities[avaterID];
		this.setAvatar(avaterID, roleType, HP_Max, position, direction, avatar, AvatarID);
	}

	// Token: 0x06001FA4 RID: 8100 RVA: 0x0010FE50 File Offset: 0x0010E050
	private void InitMaxLevel()
	{
		this.maxLevel = 0;
		if (YSSaveGame.HasFile(Paths.GetSavePath(), "MaxLevelJson"))
		{
			this.maxLevel = YSSaveGame.GetInt("MaxLevelJson", 0);
			return;
		}
		for (int i = 0; i < this.maxNum; i++)
		{
			for (int j = 0; j < this.smallDataNum; j++)
			{
				try
				{
					if (YSSaveGame.HasFile(Paths.GetSavePath(), "AvatarInfo" + Tools.instance.getSaveID(i, j)))
					{
						JSONObject jsonObject = YSSaveGame.GetJsonObject("AvatarInfo" + Tools.instance.getSaveID(i, j), null);
						if (jsonObject["avatarLevel"].I > MainUIMag.inst.maxLevel)
						{
							this.maxLevel = jsonObject["avatarLevel"].I;
						}
					}
				}
				catch (Exception)
				{
				}
			}
		}
		YSSaveGame.save("MaxLevelJson", this.maxLevel, "-1");
	}

	// Token: 0x06001FA5 RID: 8101 RVA: 0x0001A149 File Offset: 0x00018349
	private void Update()
	{
		if (Input.GetKeyDown(292))
		{
			SystemConfig.Inst.Reset();
		}
	}

	// Token: 0x04001B01 RID: 6913
	public static MainUIMag inst;

	// Token: 0x04001B02 RID: 6914
	[SerializeField]
	private Text gameVersionText;

	// Token: 0x04001B03 RID: 6915
	public FpBtn ModBtn;

	// Token: 0x04001B04 RID: 6916
	public GameObject mainPanel;

	// Token: 0x04001B05 RID: 6917
	public MainUISelectAvatar selectAvatarPanel;

	// Token: 0x04001B06 RID: 6918
	public MainUICreateAvatar createAvatarPanel;

	// Token: 0x04001B07 RID: 6919
	public MainUIDF dfPanel;

	// Token: 0x04001B08 RID: 6920
	public MainConfig SetPanel;

	// Token: 0x04001B09 RID: 6921
	public MainUITooltip tooltip;

	// Token: 0x04001B0A RID: 6922
	public int maxNum;

	// Token: 0x04001B0B RID: 6923
	public int smallDataNum;

	// Token: 0x04001B0C RID: 6924
	public int maxLevel;

	// Token: 0x04001B0D RID: 6925
	private static bool isSendGameStartEvent;
}
