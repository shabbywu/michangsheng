using System.Collections.Generic;
using GUIPackage;
using KBEngine;
using UnityEngine;
using YSGame;

public class StartGame : MonoBehaviour
{
	private void Start()
	{
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
			if (DFIndex > 0)
			{
				Tools.instance.getPlayer().lastScence = "S" + DFIndex;
			}
		}
		PlayerPrefs.SetString("sceneToLoad", Tools.instance.getPlayer().lastScence);
		Fader fader = Object.FindObjectOfType<Fader>();
		Tools.instance.IsLoadData = true;
		if (DFIndex > 0)
		{
			FactoryManager.inst.loadPlayerDateFactory.isLoadComplete = true;
		}
		else
		{
			FactoryManager.inst.loadPlayerDateFactory.LoadPlayerData(id, index);
			Tools.instance.ResetEquipSeid();
		}
		if ((Object)(object)fader == (Object)null)
		{
			Tools.instance.loadOtherScenes("LoadingScreen");
		}
		else
		{
			fader.FadeIntoLevel("LoadingScreen");
		}
	}

	public void firstAddAvatar(int id, int index, string firstName, string lastName)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		creatAvatar(10, 51, 100, new Vector3(-5f, 0f, 0f), new Vector3(0f, 0f, 80f));
		KBEngineApp.app.entity_id = 10;
		Avatar avatar = (Avatar)KBEngineApp.app.player();
		setTianfuInfo(avatar);
		FactoryManager.inst.createNewPlayerFactory.createPlayer(id, index, firstName, lastName, avatar);
	}

	public void setTianfuInfo(Avatar avatar)
	{
		avatar.ZiZhi = CreateAvatarMag.inst.tianfuUI.ZiZhi;
		avatar.shengShi = CreateAvatarMag.inst.tianfuUI.ShenShi;
		avatar.dunSu = CreateAvatarMag.inst.tianfuUI.DunSu;
		avatar.xinjin = CreateAvatarMag.inst.tianfuUI.XinJin;
		avatar.wuXin = (uint)CreateAvatarMag.inst.tianfuUI.WuXin;
		avatar.shouYuan = (uint)CreateAvatarMag.inst.tianfuUI.ShowYuan;
		avatar.LingGeng = CreateAvatarMag.inst.lingenUI.createLingen;
		avatar.HP_Max = CreateAvatarMag.inst.tianfuUI.HP_Max;
		avatar.HP = CreateAvatarMag.inst.tianfuUI.HP_Max;
		avatar.Sex = CreateAvatarMag.inst.faceUI.faceDatabase.ListType;
		avatar.addItem(1, 1, Tools.CreateItemSeid(1));
		foreach (int item in CreateAvatarMag.inst.tianfuUI.Items)
		{
			avatar.addItem(item, 1, Tools.CreateItemSeid(item));
		}
		int num = 0;
		foreach (int item2 in CreateAvatarMag.inst.tianfuUI.StaticSkill)
		{
			if (num == 0)
			{
				avatar.hasStaticSkillList.Clear();
				avatar.equipStaticSkillList.Clear();
			}
			avatar.addHasStaticSkillList(item2);
			avatar.equipStaticSkill(item2, num);
			num++;
		}
		CreateAvatarMag.inst.tianfuUI.getSelectChoice.ForEach(delegate(createAvatarChoice aa)
		{
			avatar.SelectTianFuID.Add(aa.id);
			if (aa.seid.Contains(11))
			{
				List<int> seidValue = aa.getSeidValue11();
				avatar.LingGeng[seidValue[0]] += seidValue[1];
			}
			if (aa.seid.Contains(12))
			{
				aa.realizedSeid12();
			}
			if (aa.seid.Contains(13))
			{
				aa.realizedSeid13();
			}
			if (aa.seid.Contains(14))
			{
				aa.getValue(14);
				avatar.money += (ulong)aa.getValue(14);
			}
			if (aa.seid.Contains(15))
			{
				aa.realizedSeid15();
			}
			if (aa.seid.Contains(16))
			{
				aa.realizedSeid16();
			}
			if (aa.seid.Contains(17))
			{
				aa.realizedSeid17();
			}
			if (aa.seid.Contains(18))
			{
				aa.realizedSeid18();
			}
			if (aa.seid.Contains(21))
			{
				aa.PlayerSetSeid(21);
			}
			if (aa.seid.Contains(22))
			{
				aa.realizedSeid22();
			}
			if (aa.seid.Contains(23))
			{
				aa.realizedSeid23();
			}
		});
	}

	public void addAvatar(int id, int index)
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

	public void AddDouFaPlayerInfo(int index)
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		int num = index + 100;
		int num2 = 10001 + index;
		creatAvatar(10, 51, 40, new Vector3(-5f, -1.7f, -1f), new Vector3(0f, 0f, 80f), num2);
		KBEngineApp.app.entity_id = 10;
		Avatar avatar = (Avatar)KBEngineApp.app.player();
		jsonData.instance.initAvatarFace(num, 0);
		jsonData.instance.AvatarRandomJsonData.SetField("1", jsonData.instance.AvatarRandomJsonData[num2.ToString()]);
		avatar.name = Tools.Code64(jsonData.instance.AvatarJsonData[string.Concat(num2)]["Name"].str);
		avatar.firstName = "";
		avatar.lastName = "";
		jsonData.instance.AvatarRandomJsonData[string.Concat(1)].SetField("Name", avatar.name);
		YSSaveGame.save("SaveAvatar" + num, 1);
		YSSaveGame.save("PlayerAvatarName" + num, avatar.name);
		foreach (KeyValuePair<string, JSONObject> skillJsonDatum in jsonData.instance.skillJsonData)
		{
			if (skillJsonDatum.Value["DF"].I == 1)
			{
				avatar.addHasSkillList(skillJsonDatum.Value["Skill_ID"].I);
			}
		}
		foreach (JSONObject item in jsonData.instance.StaticSkillJsonData.list)
		{
			if (item["DF"].I == 1)
			{
				int i = jsonData.instance.StaticLVToLevelJsonData[avatar.getLevelType().ToString()]["Max" + item["Skill_LV"].I].I;
				avatar.addHasStaticSkillList(item["Skill_ID"].I, i);
			}
		}
		avatar.lastScence = "S" + (index + 10000);
		GlobalValue.SetTalk(0, 0, "StartGame.AddDouFaPlayerInfo");
		Tools.instance.Save(num, 0, avatar);
	}

	public void initSkill()
	{
		Avatar avatar = (Avatar)KBEngineApp.app.player();
		avatar.equipSkillList = avatar.configEquipSkill[avatar.nowConfigEquipSkill];
		avatar.equipStaticSkillList = avatar.configEquipStaticSkill[avatar.nowConfigEquipStaticSkill];
	}

	public void setAvatar(int avaterID, int roleType, int HP_Max, Vector3 position, Vector3 direction, Avatar avatar, int AvatarID = 1)
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
}
