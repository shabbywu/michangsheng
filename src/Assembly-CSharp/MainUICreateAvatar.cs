using System.Collections.Generic;
using KBEngine;
using UnityEngine;
using YSGame;

public class MainUICreateAvatar : MonoBehaviour
{
	private bool isInit;

	public MainUISetName setNamePanel;

	public MainUISetFace setFacePanel;

	public MainUISelectTianFu setTianFu;

	public GameObject facePanel;

	public int curIndex = -1;

	public void Init()
	{
		if (!isInit)
		{
			isInit = true;
		}
		setNamePanel.Init();
		((Component)this).gameObject.SetActive(true);
	}

	public void CreateFinsh()
	{
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		PlayerPrefs.SetString("sceneToLoad", "AllMaps");
		Tools.instance.isNewAvatar = true;
		MusicMag.instance.stopMusic();
		YSNewSaveSystem.Save(YSNewSaveSystem.GetAvatarSavePathPre(curIndex, 0) + "/FirstSetAvatarRandomJsonData.txt", 0, autoPath: false);
		creatAvatar(10, 51, 100, new Vector3(-5f, 0f, 0f), new Vector3(0f, 0f, 80f));
		YSNewSaveSystem.CreatAvatar(10, 51, 100, new Vector3(-5f, 0f, 0f), new Vector3(0f, 0f, 80f));
		KBEngineApp.app.entity_id = 10;
		Avatar avatar = (Avatar)KBEngineApp.app.player();
		setTianfuInfo(avatar);
		FactoryManager.inst.createNewPlayerFactory.createPlayer(curIndex, 0, MainUIPlayerInfo.inst.firstName, MainUIPlayerInfo.inst.lastName, avatar);
	}

	private void setTianfuInfo(Avatar avatar)
	{
		avatar.ZiZhi = MainUIPlayerInfo.inst.GetZiZhi();
		avatar.shengShi = MainUIPlayerInfo.inst.GetShenShi();
		avatar.dunSu = MainUIPlayerInfo.inst.GetDunSu();
		avatar.xinjin = MainUIPlayerInfo.inst.GetXinJing();
		avatar.wuXin = (uint)MainUIPlayerInfo.inst.GetWuXin();
		avatar.shouYuan = (uint)MainUIPlayerInfo.inst.GetShouYuan();
		avatar.LingGeng = MainUIPlayerInfo.inst.GetLingGen();
		avatar.HP_Max = MainUIPlayerInfo.inst.GetHp();
		avatar.HP = avatar.HP_Max;
		avatar.money += (ulong)MainUIPlayerInfo.inst.GetLinShi();
		avatar.Sex = MainUIPlayerInfo.inst.sex;
		avatar.addItem(1, 1, Tools.CreateItemSeid(1));
		if (setTianFu.hasSelectSeidList.ContainsKey(9))
		{
			avatar.hasStaticSkillList.Clear();
			avatar.equipStaticSkillList.Clear();
			int i = jsonData.instance.CrateAvatarSeidJsonData[9][setTianFu.hasSelectSeidList[9][0].ToString()]["value1"][0].I;
			avatar.addHasStaticSkillList(i);
			avatar.equipStaticSkill(i);
		}
		if (setTianFu.hasSelectSeidList.ContainsKey(10))
		{
			List<int> list = setTianFu.hasSelectSeidList[10];
			JSONObject jSONObject = jsonData.instance.CrateAvatarSeidJsonData[10];
			foreach (int item in list)
			{
				for (int j = 0; j < jSONObject[item.ToString()]["value1"].Count; j++)
				{
					avatar.addItem(jSONObject[item.ToString()]["value1"][j].I, 1, Tools.CreateItemSeid(jSONObject[item.ToString()]["value1"][j].I));
				}
			}
		}
		foreach (int key in setTianFu.hasSelectList.Keys)
		{
			avatar.SelectTianFuID.Add(key);
			if (setTianFu.hasSelectList[key].seidList.Contains(11))
			{
				List<int> seidValue = setTianFu.hasSelectList[key].getSeidValue11();
				avatar.LingGeng[seidValue[0]] += seidValue[1];
			}
			if (setTianFu.hasSelectList[key].seidList.Contains(12))
			{
				setTianFu.hasSelectList[key].realizedSeid12();
			}
			if (setTianFu.hasSelectList[key].seidList.Contains(13))
			{
				setTianFu.hasSelectList[key].realizedSeid13();
			}
			if (setTianFu.hasSelectList[key].seidList.Contains(15))
			{
				setTianFu.hasSelectList[key].realizedSeid15();
			}
			if (setTianFu.hasSelectList[key].seidList.Contains(16))
			{
				setTianFu.hasSelectList[key].realizedSeid16();
			}
			if (setTianFu.hasSelectList[key].seidList.Contains(17))
			{
				setTianFu.hasSelectList[key].realizedSeid17();
			}
			if (setTianFu.hasSelectList[key].seidList.Contains(18))
			{
				setTianFu.hasSelectList[key].realizedSeid18();
			}
			if (setTianFu.hasSelectList[key].seidList.Contains(21))
			{
				setTianFu.hasSelectList[key].PlayerSetSeid(21);
			}
			if (setTianFu.hasSelectList[key].seidList.Contains(22))
			{
				setTianFu.hasSelectList[key].realizedSeid22();
			}
			if (setTianFu.hasSelectList[key].seidList.Contains(23))
			{
				setTianFu.hasSelectList[key].realizedSeid23();
			}
		}
	}

	private void creatAvatar(int avaterID, int roleType, int HP_Max, Vector3 position, Vector3 direction, int AvatarID = 1)
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		KBEngineApp.app.Client_onCreatedProxies((ulong)avaterID, avaterID, "Avatar");
		Avatar avatar = (Avatar)KBEngineApp.app.entities[avaterID];
		setAvatar(avaterID, roleType, HP_Max, position, direction, avatar, AvatarID);
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

	public void Close()
	{
		((Component)this).gameObject.SetActive(false);
	}
}
