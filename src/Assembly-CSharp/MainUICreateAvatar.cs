using System;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;
using YSGame;

// Token: 0x0200048C RID: 1164
public class MainUICreateAvatar : MonoBehaviour
{
	// Token: 0x06001F10 RID: 7952 RVA: 0x00019BD4 File Offset: 0x00017DD4
	public void Init()
	{
		if (!this.isInit)
		{
			this.isInit = true;
		}
		this.setNamePanel.Init();
		base.gameObject.SetActive(true);
	}

	// Token: 0x06001F11 RID: 7953 RVA: 0x0010B034 File Offset: 0x00109234
	public void CreateFinsh()
	{
		PlayerPrefs.SetString("sceneToLoad", "AllMaps");
		Tools.instance.isNewAvatar = true;
		MusicMag.instance.stopMusic();
		YSSaveGame.save("FirstSetAvatarRandomJsonData" + Tools.instance.getSaveID(this.curIndex, 0), 0, "-1");
		this.creatAvatar(10, 51, 100, new Vector3(-5f, 0f, 0f), new Vector3(0f, 0f, 80f), 1);
		KBEngineApp.app.entity_id = 10;
		Avatar avatar = (Avatar)KBEngineApp.app.player();
		this.setTianfuInfo(avatar);
		FactoryManager.inst.createNewPlayerFactory.createPlayer(this.curIndex, 0, MainUIPlayerInfo.inst.firstName, MainUIPlayerInfo.inst.lastName, avatar);
	}

	// Token: 0x06001F12 RID: 7954 RVA: 0x0010B110 File Offset: 0x00109310
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
		avatar.money += (ulong)((long)MainUIPlayerInfo.inst.GetLinShi());
		avatar.Sex = MainUIPlayerInfo.inst.sex;
		avatar.addItem(1, 1, Tools.CreateItemSeid(1), false);
		if (this.setTianFu.hasSelectSeidList.ContainsKey(9))
		{
			avatar.hasStaticSkillList.Clear();
			avatar.equipStaticSkillList.Clear();
			int i = jsonData.instance.CrateAvatarSeidJsonData[9][this.setTianFu.hasSelectSeidList[9][0].ToString()]["value1"][0].I;
			avatar.addHasStaticSkillList(i, 1);
			avatar.equipStaticSkill(i, 0);
		}
		if (this.setTianFu.hasSelectSeidList.ContainsKey(10))
		{
			List<int> list = this.setTianFu.hasSelectSeidList[10];
			JSONObject jsonobject = jsonData.instance.CrateAvatarSeidJsonData[10];
			foreach (int num in list)
			{
				for (int j = 0; j < jsonobject[num.ToString()]["value1"].Count; j++)
				{
					avatar.addItem(jsonobject[num.ToString()]["value1"][j].I, 1, Tools.CreateItemSeid(jsonobject[num.ToString()]["value1"][j].I), false);
				}
			}
		}
		foreach (int num2 in this.setTianFu.hasSelectList.Keys)
		{
			avatar.SelectTianFuID.Add(num2);
			if (this.setTianFu.hasSelectList[num2].seidList.Contains(11))
			{
				List<int> seidValue = this.setTianFu.hasSelectList[num2].getSeidValue11();
				List<int> lingGeng = avatar.LingGeng;
				int index = seidValue[0];
				lingGeng[index] += seidValue[1];
			}
			if (this.setTianFu.hasSelectList[num2].seidList.Contains(12))
			{
				this.setTianFu.hasSelectList[num2].realizedSeid12();
			}
			if (this.setTianFu.hasSelectList[num2].seidList.Contains(13))
			{
				this.setTianFu.hasSelectList[num2].realizedSeid13();
			}
			if (this.setTianFu.hasSelectList[num2].seidList.Contains(15))
			{
				this.setTianFu.hasSelectList[num2].realizedSeid15();
			}
			if (this.setTianFu.hasSelectList[num2].seidList.Contains(16))
			{
				this.setTianFu.hasSelectList[num2].realizedSeid16();
			}
			if (this.setTianFu.hasSelectList[num2].seidList.Contains(17))
			{
				this.setTianFu.hasSelectList[num2].realizedSeid17();
			}
			if (this.setTianFu.hasSelectList[num2].seidList.Contains(18))
			{
				this.setTianFu.hasSelectList[num2].realizedSeid18();
			}
			if (this.setTianFu.hasSelectList[num2].seidList.Contains(21))
			{
				this.setTianFu.hasSelectList[num2].PlayerSetSeid(21);
			}
			if (this.setTianFu.hasSelectList[num2].seidList.Contains(22))
			{
				this.setTianFu.hasSelectList[num2].realizedSeid22();
			}
			if (this.setTianFu.hasSelectList[num2].seidList.Contains(23))
			{
				this.setTianFu.hasSelectList[num2].realizedSeid23();
			}
		}
	}

	// Token: 0x06001F13 RID: 7955 RVA: 0x0010B620 File Offset: 0x00109820
	private void creatAvatar(int avaterID, int roleType, int HP_Max, Vector3 position, Vector3 direction, int AvatarID = 1)
	{
		KBEngineApp.app.Client_onCreatedProxies((ulong)((long)avaterID), avaterID, "Avatar");
		Avatar avatar = (Avatar)KBEngineApp.app.entities[avaterID];
		this.setAvatar(avaterID, roleType, HP_Max, position, direction, avatar, AvatarID);
	}

	// Token: 0x06001F14 RID: 7956 RVA: 0x0010B668 File Offset: 0x00109868
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

	// Token: 0x06001F15 RID: 7957 RVA: 0x00017C2D File Offset: 0x00015E2D
	public void Close()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x04001A8A RID: 6794
	private bool isInit;

	// Token: 0x04001A8B RID: 6795
	public MainUISetName setNamePanel;

	// Token: 0x04001A8C RID: 6796
	public MainUISetFace setFacePanel;

	// Token: 0x04001A8D RID: 6797
	public MainUISelectTianFu setTianFu;

	// Token: 0x04001A8E RID: 6798
	public GameObject facePanel;

	// Token: 0x04001A8F RID: 6799
	public int curIndex = -1;
}
