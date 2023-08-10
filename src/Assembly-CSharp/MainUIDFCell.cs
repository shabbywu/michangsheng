using System.Collections.Generic;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using YSGame;

public class MainUIDFCell : MonoBehaviour
{
	[SerializeField]
	private Image leverl_Image;

	[SerializeField]
	private Text level_Text;

	[SerializeField]
	private Text desc_Text;

	[SerializeField]
	private GameObject lockObj;

	[SerializeField]
	private PlayerSetRandomFace face;

	public bool isLock;

	public int index;

	public SaveSlotData data;

	public void Init(int index, int level, bool isLock, string desc)
	{
		this.index = index;
		level_Text.text = jsonData.instance.LevelUpDataJsonData[level.ToString()]["Name"].Str;
		leverl_Image.sprite = ResManager.inst.LoadSprite($"NewUI/Fight/LevelIcon/icon_{level}");
		this.isLock = isLock;
		if (isLock)
		{
			lockObj.SetActive(true);
			desc_Text.text = desc;
		}
		else
		{
			lockObj.SetActive(false);
		}
		face.SetDoFaFace();
		((Component)this).gameObject.SetActive(true);
	}

	public void StartDouFa()
	{
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Expected O, but got Unknown
		if (isLock)
		{
			UIPopTip.Inst.Pop(desc_Text.text);
			return;
		}
		TySelect.inst.Show("是否进入" + level_Text.text + "神仙斗法", (UnityAction)delegate
		{
			Tools.instance.IsInDF = true;
			PlayerPrefs.SetInt("NowPlayerFileAvatar", index);
			int @int = PlayerPrefs.GetInt("NowPlayerFileAvatar");
			KBEngineApp.app.entities.Remove(10);
			data = YSNewSaveSystem.GetAvatarSaveData(index, 0);
			YSNewSaveSystem.NowUsingAvatarIndex = index;
			YSNewSaveSystem.NowUsingSlot = 0;
			YSNewSaveSystem.NowAvatarPathPre = YSNewSaveSystem.GetAvatarSavePathPre(index, 0);
			if (!data.HasSave)
			{
				NewFirstSetDFInfo();
				PlayerPrefs.SetString("sceneToLoad", "S" + (10000 + index - 100));
			}
			if (data.IsNewSaveSystem || !data.HasSave)
			{
				YSNewSaveSystem.LoadSave(@int, 0, 10000 + index - 100);
			}
			else
			{
				MainUIMag.inst.startGame(@int, 0, 10000 + index - 100);
			}
			Avatar player = Tools.instance.getPlayer();
			player.hasSkillList = new List<SkillItem>();
			player.StreamData.FungusSaveMgr.IsNeedStop = false;
			player.hasStaticSkillList = new List<SkillItem>();
			foreach (KeyValuePair<string, JSONObject> skillJsonDatum in jsonData.instance.skillJsonData)
			{
				if ((int)skillJsonDatum.Value["DF"].n == 1)
				{
					player.addHasSkillList(skillJsonDatum.Value["Skill_ID"].I);
				}
			}
			foreach (JSONObject item in jsonData.instance.StaticSkillJsonData.list)
			{
				if ((int)item["DF"].n == 1)
				{
					int level = (int)jsonData.instance.StaticLVToLevelJsonData[player.getLevelType().ToString()]["Max" + (int)item["Skill_LV"].n].n;
					player.addHasStaticSkillList(item["Skill_ID"].I, level);
				}
			}
			for (int i = 10101; i <= 10105; i++)
			{
				jsonData.instance.setMonstarDeath(i);
			}
			foreach (JSONObject item2 in jsonData.instance.WuDaoAllTypeJson.list)
			{
				player.wuDaoMag.SetWuDaoEx(item2["id"].I, 999999);
			}
			foreach (KeyValuePair<string, JSONObject> skill2 in jsonData.instance.skillJsonData)
			{
				if (skill2.Value["DF"].I == 1 && player.hasSkillList.Find((SkillItem aa) => aa.itemId == skill2.Value["Skill_ID"].I) == null)
				{
					player.addHasSkillList(skill2.Value["Skill_ID"].I);
				}
			}
			foreach (JSONObject skill in jsonData.instance.StaticSkillJsonData.list)
			{
				if (skill["DF"].I == 1 && player.hasStaticSkillList.Find((SkillItem aa) => aa.itemId == skill["Skill_ID"].I) == null)
				{
					int i2 = jsonData.instance.StaticLVToLevelJsonData[player.getLevelType().ToString()]["Max" + skill["Skill_LV"].I].I;
					player.addHasStaticSkillList(skill["Skill_ID"].I, i2);
				}
			}
		}, null, isDestorySelf: false);
	}

	private void FirstSetDFInfo()
	{
		YSSaveGame.save("FirstSetAvatarRandomJsonData" + Tools.instance.getSaveID(index, 0), 0);
		AddDouFaPlayerInfo(index - 100);
	}

	private void NewFirstSetDFInfo()
	{
		YSNewSaveSystem.Save("FirstSetAvatarRandomJsonData.txt", 0);
		NewAddDouFaPlayerInfo(index - 100);
	}

	private void AddDouFaPlayerInfo(int index)
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
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
		YSSaveGame.save("SaveAvatar" + num, 1);
		YSSaveGame.save("PlayerAvatarName" + num, avatar.name);
		avatar.lastScence = "S" + (index + 10000);
		GlobalValue.SetTalk(0, 0, "MainUIDFCell.AddDouFaPlayerInfo");
		Tools.instance.Save(num, 0, avatar);
	}

	private void NewAddDouFaPlayerInfo(int index)
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		int num = index + 100;
		int num2 = 10001 + index;
		MainUIMag.inst.creatAvatar(10, 51, 40, new Vector3(-5f, -1.7f, -1f), new Vector3(0f, 0f, 80f), num2);
		KBEngineApp.app.entity_id = 10;
		Avatar avatar = (Avatar)KBEngineApp.app.player();
		NewInitDouFaFace(num, 0);
		avatar.name = jsonData.instance.AvatarJsonData[string.Concat(num2)]["Name"].Str;
		avatar.firstName = "";
		avatar.lastName = "";
		jsonData.instance.AvatarRandomJsonData[string.Concat(1)].SetField("Name", avatar.name);
		YSNewSaveSystem.Save("SaveAvatar.txt", 1);
		YSNewSaveSystem.Save("PlayerAvatarName.txt", avatar.name);
		avatar.lastScence = "S" + (index + 10000);
		GlobalValue.SetTalk(0, 0, "MainUIDFCell.AddDouFaPlayerInfo");
		YSNewSaveSystem.SaveGame(num, 0, avatar);
	}

	public void NewInitDouFaFace(int id, int index)
	{
		foreach (JSONObject item in jsonData.instance.AvatarJsonData.list)
		{
			int i = item["id"].I;
			if (i != 1)
			{
				if (i >= 20000)
				{
					break;
				}
				JSONObject jSONObject = jsonData.instance.randomAvatarFace(item, jsonData.instance.AvatarRandomJsonData.HasField(string.Concat(item["id"].I)) ? jsonData.instance.AvatarRandomJsonData[item["id"].I.ToString()] : null);
				jsonData.instance.AvatarRandomJsonData.SetField(string.Concat(item["id"].I), jSONObject.Copy());
			}
		}
		if (jsonData.instance.AvatarRandomJsonData.HasField("1"))
		{
			jsonData.instance.AvatarRandomJsonData.SetField("10000", jsonData.instance.AvatarRandomJsonData["1"]);
		}
		YSNewSaveSystem.Save("AvatarRandomJsonData.json", jsonData.instance.AvatarRandomJsonData);
		YSNewSaveSystem.Save("FirstSetAvatarRandomJsonData.json", 1);
		YSNewSaveSystem.NewRandomAvatarBackpack(id, index);
	}
}
