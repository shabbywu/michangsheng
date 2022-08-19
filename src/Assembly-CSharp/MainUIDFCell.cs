using System;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;
using YSGame;

// Token: 0x02000332 RID: 818
public class MainUIDFCell : MonoBehaviour
{
	// Token: 0x06001C2F RID: 7215 RVA: 0x000C9A74 File Offset: 0x000C7C74
	public void Init(int index, int level, bool isLock, string desc)
	{
		this.index = index;
		this.level_Text.text = jsonData.instance.LevelUpDataJsonData[level.ToString()]["Name"].Str;
		this.leverl_Image.sprite = ResManager.inst.LoadSprite(string.Format("NewUI/Fight/LevelIcon/icon_{0}", level));
		this.isLock = isLock;
		if (isLock)
		{
			this.lockObj.SetActive(true);
			this.desc_Text.text = desc;
		}
		else
		{
			this.lockObj.SetActive(false);
		}
		this.face.SetDoFaFace();
		base.gameObject.SetActive(true);
	}

	// Token: 0x06001C30 RID: 7216 RVA: 0x000C9B28 File Offset: 0x000C7D28
	public void StartDouFa()
	{
		if (this.isLock)
		{
			UIPopTip.Inst.Pop(this.desc_Text.text, PopTipIconType.叹号);
			return;
		}
		TySelect.inst.Show("是否进入" + this.level_Text.text + "神仙斗法", delegate
		{
			Tools.instance.IsInDF = true;
			PlayerPrefs.SetInt("NowPlayerFileAvatar", this.index);
			int @int = PlayerPrefs.GetInt("NowPlayerFileAvatar");
			KBEngineApp.app.entities.Remove(10);
			this.data = YSNewSaveSystem.GetAvatarSaveData(this.index, 0);
			YSNewSaveSystem.NowUsingAvatarIndex = this.index;
			YSNewSaveSystem.NowUsingSlot = 0;
			YSNewSaveSystem.NowAvatarPathPre = YSNewSaveSystem.GetAvatarSavePathPre(this.index, 0);
			if (!this.data.HasSave)
			{
				this.NewFirstSetDFInfo();
				PlayerPrefs.SetString("sceneToLoad", "S" + (10000 + this.index - 100));
			}
			if (this.data.IsNewSaveSystem || !this.data.HasSave)
			{
				YSNewSaveSystem.LoadSave(@int, 0, 10000 + this.index - 100);
			}
			else
			{
				MainUIMag.inst.startGame(@int, 0, 10000 + this.index - 100);
			}
			Avatar player = Tools.instance.getPlayer();
			player.hasSkillList = new List<SkillItem>();
			player.StreamData.FungusSaveMgr.IsNeedStop = false;
			player.hasStaticSkillList = new List<SkillItem>();
			foreach (KeyValuePair<string, JSONObject> keyValuePair in jsonData.instance.skillJsonData)
			{
				if ((int)keyValuePair.Value["DF"].n == 1)
				{
					player.addHasSkillList(keyValuePair.Value["Skill_ID"].I);
				}
			}
			foreach (JSONObject jsonobject in jsonData.instance.StaticSkillJsonData.list)
			{
				if ((int)jsonobject["DF"].n == 1)
				{
					int level = (int)jsonData.instance.StaticLVToLevelJsonData[player.getLevelType().ToString()]["Max" + (int)jsonobject["Skill_LV"].n].n;
					player.addHasStaticSkillList(jsonobject["Skill_ID"].I, level);
				}
			}
			for (int i = 10101; i <= 10105; i++)
			{
				jsonData.instance.setMonstarDeath(i, true);
			}
			foreach (JSONObject jsonobject2 in jsonData.instance.WuDaoAllTypeJson.list)
			{
				player.wuDaoMag.SetWuDaoEx(jsonobject2["id"].I, 999999);
			}
			using (Dictionary<string, JSONObject>.Enumerator enumerator = jsonData.instance.skillJsonData.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<string, JSONObject> skill = enumerator.Current;
					if (skill.Value["DF"].I == 1 && player.hasSkillList.Find((SkillItem aa) => aa.itemId == skill.Value["Skill_ID"].I) == null)
					{
						player.addHasSkillList(skill.Value["Skill_ID"].I);
					}
				}
			}
			using (List<JSONObject>.Enumerator enumerator2 = jsonData.instance.StaticSkillJsonData.list.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					JSONObject skill = enumerator2.Current;
					if (skill["DF"].I == 1 && player.hasStaticSkillList.Find((SkillItem aa) => aa.itemId == skill["Skill_ID"].I) == null)
					{
						int i2 = jsonData.instance.StaticLVToLevelJsonData[player.getLevelType().ToString()]["Max" + skill["Skill_LV"].I].I;
						player.addHasStaticSkillList(skill["Skill_ID"].I, i2);
					}
				}
			}
		}, null, false);
	}

	// Token: 0x06001C31 RID: 7217 RVA: 0x000C9B86 File Offset: 0x000C7D86
	private void FirstSetDFInfo()
	{
		YSSaveGame.save("FirstSetAvatarRandomJsonData" + Tools.instance.getSaveID(this.index, 0), 0, "-1");
		this.AddDouFaPlayerInfo(this.index - 100);
	}

	// Token: 0x06001C32 RID: 7218 RVA: 0x000C9BBD File Offset: 0x000C7DBD
	private void NewFirstSetDFInfo()
	{
		YSNewSaveSystem.Save("FirstSetAvatarRandomJsonData.txt", 0, true);
		this.NewAddDouFaPlayerInfo(this.index - 100);
	}

	// Token: 0x06001C33 RID: 7219 RVA: 0x000C9BDC File Offset: 0x000C7DDC
	private void AddDouFaPlayerInfo(int index)
	{
		int num = index + 100;
		int num2 = 10001 + index;
		MainUIMag.inst.creatAvatar(10, 51, 40, new Vector3(-5f, -1.7f, -1f), new Vector3(0f, 0f, 80f), num2);
		KBEngineApp.app.entity_id = 10;
		Avatar avatar = (Avatar)KBEngineApp.app.player();
		jsonData.instance.initDouFaFace(num, 0);
		avatar.name = jsonData.instance.AvatarJsonData[string.Concat(num2)]["Name"].Str;
		avatar.firstName = "";
		avatar.lastName = "";
		jsonData.instance.AvatarRandomJsonData[string.Concat(1)].SetField("Name", avatar.name);
		YSSaveGame.save("SaveAvatar" + num, 1, "-1");
		YSSaveGame.save("PlayerAvatarName" + num, avatar.name, "-1");
		avatar.lastScence = "S" + (index + 10000);
		GlobalValue.SetTalk(0, 0, "MainUIDFCell.AddDouFaPlayerInfo");
		Tools.instance.Save(num, 0, avatar);
	}

	// Token: 0x06001C34 RID: 7220 RVA: 0x000C9D38 File Offset: 0x000C7F38
	private void NewAddDouFaPlayerInfo(int index)
	{
		int num = index + 100;
		int num2 = 10001 + index;
		MainUIMag.inst.creatAvatar(10, 51, 40, new Vector3(-5f, -1.7f, -1f), new Vector3(0f, 0f, 80f), num2);
		KBEngineApp.app.entity_id = 10;
		Avatar avatar = (Avatar)KBEngineApp.app.player();
		this.NewInitDouFaFace(num, 0);
		avatar.name = jsonData.instance.AvatarJsonData[string.Concat(num2)]["Name"].Str;
		avatar.firstName = "";
		avatar.lastName = "";
		jsonData.instance.AvatarRandomJsonData[string.Concat(1)].SetField("Name", avatar.name);
		YSNewSaveSystem.Save("SaveAvatar.txt", 1, true);
		YSNewSaveSystem.Save("PlayerAvatarName.txt", avatar.name, true);
		avatar.lastScence = "S" + (index + 10000);
		GlobalValue.SetTalk(0, 0, "MainUIDFCell.AddDouFaPlayerInfo");
		YSNewSaveSystem.SaveGame(num, 0, avatar, false);
	}

	// Token: 0x06001C35 RID: 7221 RVA: 0x000C9E6C File Offset: 0x000C806C
	public void NewInitDouFaFace(int id, int index)
	{
		foreach (JSONObject jsonobject in jsonData.instance.AvatarJsonData.list)
		{
			int i = jsonobject["id"].I;
			if (i != 1)
			{
				if (i >= 20000)
				{
					break;
				}
				JSONObject jsonobject2 = jsonData.instance.randomAvatarFace(jsonobject, jsonData.instance.AvatarRandomJsonData.HasField(string.Concat(jsonobject["id"].I)) ? jsonData.instance.AvatarRandomJsonData[jsonobject["id"].I.ToString()] : null);
				jsonData.instance.AvatarRandomJsonData.SetField(string.Concat(jsonobject["id"].I), jsonobject2.Copy());
			}
		}
		if (jsonData.instance.AvatarRandomJsonData.HasField("1"))
		{
			jsonData.instance.AvatarRandomJsonData.SetField("10000", jsonData.instance.AvatarRandomJsonData["1"]);
		}
		YSNewSaveSystem.Save("AvatarRandomJsonData.json", jsonData.instance.AvatarRandomJsonData, true);
		YSNewSaveSystem.Save("FirstSetAvatarRandomJsonData.json", 1, true);
		YSNewSaveSystem.NewRandomAvatarBackpack(id, index);
	}

	// Token: 0x040016BB RID: 5819
	[SerializeField]
	private Image leverl_Image;

	// Token: 0x040016BC RID: 5820
	[SerializeField]
	private Text level_Text;

	// Token: 0x040016BD RID: 5821
	[SerializeField]
	private Text desc_Text;

	// Token: 0x040016BE RID: 5822
	[SerializeField]
	private GameObject lockObj;

	// Token: 0x040016BF RID: 5823
	[SerializeField]
	private PlayerSetRandomFace face;

	// Token: 0x040016C0 RID: 5824
	public bool isLock;

	// Token: 0x040016C1 RID: 5825
	public int index;

	// Token: 0x040016C2 RID: 5826
	public SaveSlotData data;
}
