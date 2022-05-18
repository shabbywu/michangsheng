using System;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;
using YSGame;

// Token: 0x020004A0 RID: 1184
public class MainUIDFCell : MonoBehaviour
{
	// Token: 0x06001F7E RID: 8062 RVA: 0x0010ED20 File Offset: 0x0010CF20
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

	// Token: 0x06001F7F RID: 8063 RVA: 0x0010EDD4 File Offset: 0x0010CFD4
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
			if (!YSSaveGame.HasFile(Paths.GetSavePath(), "AvatarInfo" + Tools.instance.getSaveID(this.index, 0)))
			{
				YSSaveGame.save("SaveDFAvatar" + this.index, 1, "-1");
				MusicMag.instance.stopMusic();
				this.firesetSetDFInfo();
				PlayerPrefs.SetString("sceneToLoad", "S" + (10000 + this.index - 100));
			}
			MainUIMag.inst.startGame(@int, 0, 10000 + this.index - 100);
			Avatar player = Tools.instance.getPlayer();
			player.hasSkillList = new List<SkillItem>();
			player.StreamData.FungusSaveMgr.IsNeedStop = false;
			player.hasStaticSkillList = new List<SkillItem>();
			foreach (KeyValuePair<string, JSONObject> keyValuePair in jsonData.instance.skillJsonData)
			{
				if ((int)keyValuePair.Value["DF"].n == 1)
				{
					player.addHasSkillList((int)keyValuePair.Value["Skill_ID"].n);
				}
			}
			foreach (JSONObject jsonobject in jsonData.instance.StaticSkillJsonData.list)
			{
				if ((int)jsonobject["DF"].n == 1)
				{
					int level = (int)jsonData.instance.StaticLVToLevelJsonData[player.getLevelType().ToString()]["Max" + (int)jsonobject["Skill_LV"].n].n;
					player.addHasStaticSkillList((int)jsonobject["Skill_ID"].n, level);
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
					if ((int)skill.Value["DF"].n == 1 && player.hasSkillList.Find((SkillItem aa) => aa.itemId == (int)skill.Value["Skill_ID"].n) == null)
					{
						player.addHasSkillList((int)skill.Value["Skill_ID"].n);
					}
				}
			}
			using (List<JSONObject>.Enumerator enumerator2 = jsonData.instance.StaticSkillJsonData.list.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					JSONObject skill = enumerator2.Current;
					if ((int)skill["DF"].n == 1 && player.hasStaticSkillList.Find((SkillItem aa) => aa.itemId == (int)skill["Skill_ID"].n) == null)
					{
						int level2 = (int)jsonData.instance.StaticLVToLevelJsonData[player.getLevelType().ToString()]["Max" + (int)skill["Skill_LV"].n].n;
						player.addHasStaticSkillList((int)skill["Skill_ID"].n, level2);
					}
				}
			}
		}, null, false);
	}

	// Token: 0x06001F80 RID: 8064 RVA: 0x00019FF2 File Offset: 0x000181F2
	private void firesetSetDFInfo()
	{
		YSSaveGame.save("FirstSetAvatarRandomJsonData" + Tools.instance.getSaveID(this.index, 0), 0, "-1");
		this.AddDouFaPlayerInfo(this.index - 100);
	}

	// Token: 0x06001F81 RID: 8065 RVA: 0x0010EE34 File Offset: 0x0010D034
	private void AddDouFaPlayerInfo(int index)
	{
		int num = index + 100;
		int num2 = 10001 + index;
		MainUIMag.inst.creatAvatar(10, 51, 40, new Vector3(-5f, -1.7f, -1f), new Vector3(0f, 0f, 80f), num2);
		KBEngineApp.app.entity_id = 10;
		Avatar avatar = (Avatar)KBEngineApp.app.player();
		jsonData.instance.initDouFaFace(num, 0);
		avatar.name = Tools.Code64(jsonData.instance.AvatarJsonData[string.Concat(num2)]["Name"].str);
		avatar.firstName = "";
		avatar.lastName = "";
		jsonData.instance.AvatarRandomJsonData[string.Concat(1)].SetField("Name", avatar.name);
		YSSaveGame.save("SaveAvatar" + num, 1, "-1");
		YSSaveGame.save("PlayerAvatarName" + num, avatar.name, "-1");
		avatar.lastScence = "S" + (index + 10000);
		GlobalValue.SetTalk(0, 0, "MainUIDFCell.AddDouFaPlayerInfo");
		Tools.instance.Save(num, 0, avatar);
	}

	// Token: 0x04001AF0 RID: 6896
	[SerializeField]
	private Image leverl_Image;

	// Token: 0x04001AF1 RID: 6897
	[SerializeField]
	private Text level_Text;

	// Token: 0x04001AF2 RID: 6898
	[SerializeField]
	private Text desc_Text;

	// Token: 0x04001AF3 RID: 6899
	[SerializeField]
	private GameObject lockObj;

	// Token: 0x04001AF4 RID: 6900
	[SerializeField]
	private PlayerSetRandomFace face;

	// Token: 0x04001AF5 RID: 6901
	public bool isLock;

	// Token: 0x04001AF6 RID: 6902
	public int index;
}
