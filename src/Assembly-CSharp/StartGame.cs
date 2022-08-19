using System;
using System.Collections.Generic;
using GUIPackage;
using KBEngine;
using UnityEngine;
using YSGame;

// Token: 0x020003C3 RID: 963
public class StartGame : MonoBehaviour
{
	// Token: 0x06001F57 RID: 8023 RVA: 0x00004095 File Offset: 0x00002295
	private void Start()
	{
	}

	// Token: 0x06001F58 RID: 8024 RVA: 0x000DC710 File Offset: 0x000DA910
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
		if (fader == null)
		{
			Tools.instance.loadOtherScenes("LoadingScreen");
			return;
		}
		fader.FadeIntoLevel("LoadingScreen");
	}

	// Token: 0x06001F59 RID: 8025 RVA: 0x000DC88C File Offset: 0x000DAA8C
	public void firstAddAvatar(int id, int index, string firstName, string lastName)
	{
		this.creatAvatar(10, 51, 100, new Vector3(-5f, 0f, 0f), new Vector3(0f, 0f, 80f), 1);
		KBEngineApp.app.entity_id = 10;
		Avatar avatar = (Avatar)KBEngineApp.app.player();
		this.setTianfuInfo(avatar);
		FactoryManager.inst.createNewPlayerFactory.createPlayer(id, index, firstName, lastName, avatar);
	}

	// Token: 0x06001F5A RID: 8026 RVA: 0x000DC908 File Offset: 0x000DAB08
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
		avatar.addItem(1, 1, Tools.CreateItemSeid(1), false);
		foreach (int itemID in CreateAvatarMag.inst.tianfuUI.Items)
		{
			avatar.addItem(itemID, 1, Tools.CreateItemSeid(itemID), false);
		}
		int num = 0;
		foreach (int num2 in CreateAvatarMag.inst.tianfuUI.StaticSkill)
		{
			if (num == 0)
			{
				avatar.hasStaticSkillList.Clear();
				avatar.equipStaticSkillList.Clear();
			}
			avatar.addHasStaticSkillList(num2, 1);
			avatar.equipStaticSkill(num2, num);
			num++;
		}
		CreateAvatarMag.inst.tianfuUI.getSelectChoice.ForEach(delegate(createAvatarChoice aa)
		{
			avatar.SelectTianFuID.Add(aa.id);
			if (aa.seid.Contains(11))
			{
				List<int> seidValue = aa.getSeidValue11();
				List<int> lingGeng = avatar.LingGeng;
				int index = seidValue[0];
				lingGeng[index] += seidValue[1];
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
				avatar.money += (ulong)((long)aa.getValue(14));
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

	// Token: 0x06001F5B RID: 8027 RVA: 0x000DCB48 File Offset: 0x000DAD48
	public void addAvatar(int id, int index)
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

	// Token: 0x06001F5C RID: 8028 RVA: 0x000DCBFC File Offset: 0x000DADFC
	public void AddDouFaPlayerInfo(int index)
	{
		int num = index + 100;
		int num2 = 10001 + index;
		this.creatAvatar(10, 51, 40, new Vector3(-5f, -1.7f, -1f), new Vector3(0f, 0f, 80f), num2);
		KBEngineApp.app.entity_id = 10;
		Avatar avatar = (Avatar)KBEngineApp.app.player();
		jsonData.instance.initAvatarFace(num, 0, 1);
		jsonData.instance.AvatarRandomJsonData.SetField("1", jsonData.instance.AvatarRandomJsonData[num2.ToString()]);
		avatar.name = Tools.Code64(jsonData.instance.AvatarJsonData[string.Concat(num2)]["Name"].str);
		avatar.firstName = "";
		avatar.lastName = "";
		jsonData.instance.AvatarRandomJsonData[string.Concat(1)].SetField("Name", avatar.name);
		YSSaveGame.save("SaveAvatar" + num, 1, "-1");
		YSSaveGame.save("PlayerAvatarName" + num, avatar.name, "-1");
		foreach (KeyValuePair<string, JSONObject> keyValuePair in jsonData.instance.skillJsonData)
		{
			if (keyValuePair.Value["DF"].I == 1)
			{
				avatar.addHasSkillList(keyValuePair.Value["Skill_ID"].I);
			}
		}
		foreach (JSONObject jsonobject in jsonData.instance.StaticSkillJsonData.list)
		{
			if (jsonobject["DF"].I == 1)
			{
				int i = jsonData.instance.StaticLVToLevelJsonData[avatar.getLevelType().ToString()]["Max" + jsonobject["Skill_LV"].I].I;
				avatar.addHasStaticSkillList(jsonobject["Skill_ID"].I, i);
			}
		}
		avatar.lastScence = "S" + (index + 10000);
		GlobalValue.SetTalk(0, 0, "StartGame.AddDouFaPlayerInfo");
		Tools.instance.Save(num, 0, avatar);
	}

	// Token: 0x06001F5D RID: 8029 RVA: 0x000DCEC0 File Offset: 0x000DB0C0
	public void initSkill()
	{
		Avatar avatar = (Avatar)KBEngineApp.app.player();
		avatar.equipSkillList = avatar.configEquipSkill[avatar.nowConfigEquipSkill];
		avatar.equipStaticSkillList = avatar.configEquipStaticSkill[avatar.nowConfigEquipStaticSkill];
	}

	// Token: 0x06001F5E RID: 8030 RVA: 0x000DCF04 File Offset: 0x000DB104
	public void setAvatar(int avaterID, int roleType, int HP_Max, Vector3 position, Vector3 direction, Avatar avatar, int AvatarID = 1)
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

	// Token: 0x06001F5F RID: 8031 RVA: 0x000DD284 File Offset: 0x000DB484
	public void creatAvatar(int avaterID, int roleType, int HP_Max, Vector3 position, Vector3 direction, int AvatarID = 1)
	{
		KBEngineApp.app.Client_onCreatedProxies((ulong)((long)avaterID), avaterID, "Avatar");
		Avatar avatar = (Avatar)KBEngineApp.app.entities[avaterID];
		this.setAvatar(avaterID, roleType, HP_Max, position, direction, avatar, AvatarID);
	}
}
