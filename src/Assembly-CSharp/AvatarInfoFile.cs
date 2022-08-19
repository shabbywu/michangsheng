using System;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using YSGame;

// Token: 0x02000181 RID: 385
public class AvatarInfoFile : MonoBehaviour
{
	// Token: 0x06001053 RID: 4179 RVA: 0x00004095 File Offset: 0x00002295
	private void Start()
	{
	}

	// Token: 0x06001054 RID: 4180 RVA: 0x0005FDA0 File Offset: 0x0005DFA0
	private void Awake()
	{
		if (this.initFile)
		{
			this.showSave();
			base.Invoke("InvokeShowSave", 0.1f);
		}
	}

	// Token: 0x06001055 RID: 4181 RVA: 0x0005FDC0 File Offset: 0x0005DFC0
	private void InvokeShowSave()
	{
		base.transform.parent.Find("inventoryBtn/save").GetComponentInChildren<UIToggle>().value = true;
	}

	// Token: 0x06001056 RID: 4182 RVA: 0x0005FDE2 File Offset: 0x0005DFE2
	public void showUIPage()
	{
		base.gameObject.SetActive(true);
	}

	// Token: 0x06001057 RID: 4183 RVA: 0x0005FDF0 File Offset: 0x0005DFF0
	public void FirstShow()
	{
		if (PlayerPrefs.GetInt("NowPlayerFileAvatar") < 100)
		{
			if (base.transform.parent.Find("inventoryBtn/save").GetComponentInChildren<UIToggle>().value)
			{
				base.Invoke("showUIPage", 0.01f);
				return;
			}
		}
		else
		{
			base.Invoke("showConfig", 0.1f);
		}
	}

	// Token: 0x06001058 RID: 4184 RVA: 0x0005FE4D File Offset: 0x0005E04D
	public void showConfig()
	{
		base.transform.parent.Find("inventoryBtn/config").GetComponentInChildren<UIToggle>().value = true;
	}

	// Token: 0x06001059 RID: 4185 RVA: 0x0005FE70 File Offset: 0x0005E070
	public void clearUiGrid()
	{
		foreach (object obj in this.UIGrid.transform)
		{
			Object.Destroy(((Transform)obj).gameObject);
		}
	}

	// Token: 0x0600105A RID: 4186 RVA: 0x0005FED0 File Offset: 0x0005E0D0
	public GameObject getInfoCell(int index, AvatarInfoCell.FileType type)
	{
		GameObject gameObject = this.UIGrid.transform.GetChild(index).gameObject;
		gameObject.GetComponent<AvatarInfoCell>().fileType = type;
		return gameObject;
	}

	// Token: 0x0600105B RID: 4187 RVA: 0x0005FEF4 File Offset: 0x0005E0F4
	public void setInfoCell(GameObject infoCell, int avatarid, int i)
	{
		AvatarInfoCell component = infoCell.GetComponent<AvatarInfoCell>();
		bool flag = false;
		if (YSSaveGame.HasFile(Paths.GetSavePath(), "AvatarInfo" + Tools.instance.getSaveID(avatarid, i)) && !YSSaveGame.GetJsonObject("AvatarInfo" + Tools.instance.getSaveID(avatarid, i), null).IsNull)
		{
			flag = true;
		}
		if (flag)
		{
			infoCell.transform.Find("empty").gameObject.SetActive(false);
			infoCell.transform.Find("saveinfo").gameObject.SetActive(true);
			this.setInfoText(infoCell, avatarid, i);
		}
		else
		{
			infoCell.transform.Find("empty").gameObject.SetActive(true);
			infoCell.transform.Find("saveinfo").gameObject.SetActive(false);
			if (component.fileType == AvatarInfoCell.FileType.LOAD)
			{
				infoCell.transform.Find("empty/Label").GetComponent<UILabel>().text = "空";
				infoCell.transform.Find("empty/Texture").gameObject.SetActive(false);
			}
			else
			{
				infoCell.transform.Find("empty/Label").GetComponent<UILabel>().text = "新建";
				infoCell.transform.Find("empty/Texture").gameObject.SetActive(true);
			}
		}
		component.index = i;
	}

	// Token: 0x0600105C RID: 4188 RVA: 0x00060058 File Offset: 0x0005E258
	public string getSaveFirstText(GameObject infoCell, int nowAvatarID, int i)
	{
		string savePath = Paths.GetSavePath();
		if (YSSaveGame.HasFile(savePath, "AvatarInfo" + Tools.instance.getSaveID(nowAvatarID, i)))
		{
			new JSONObject();
			JSONObject jsonObject = YSSaveGame.GetJsonObject("AvatarInfo" + Tools.instance.getSaveID(nowAvatarID, i), null);
			int i2 = jsonObject["avatarLevel"].I;
			string text = "[cab25b]境界：[-][fff0a4]" + Tools.instance.Code64ToString(jsonData.instance.LevelUpDataJsonData[string.Concat(i2)]["Name"].str);
			DateTime dateTime = DateTime.Parse(jsonObject["gameTime"].str);
			return string.Concat(new object[]
			{
				text,
				"\n[cab25b]时长：[-][fff0a4]",
				dateTime.Year,
				"年",
				dateTime.Month,
				"月",
				dateTime.Day,
				"日"
			});
		}
		Avatar avatar = new Avatar();
		Tools.GetValue<Avatar>("Avatar" + Tools.instance.getSaveID(nowAvatarID, i), avatar);
		ushort level = avatar.level;
		string text2 = "[cab25b]境界：[-][fff0a4]" + Tools.instance.Code64ToString(jsonData.instance.LevelUpDataJsonData[string.Concat(level)]["Name"].str);
		DateTime dateTime2 = DateTime.Parse(avatar.worldTimeMag.nowTime);
		JSONObject jsonobject = new JSONObject();
		jsonobject.SetField("firstName", avatar.firstName);
		jsonobject.SetField("lastName", avatar.lastName);
		jsonobject.SetField("gameTime", avatar.worldTimeMag.nowTime);
		jsonobject.SetField("avatarLevel", (int)avatar.level);
		YSSaveGame.save("AvatarInfo" + Tools.instance.getSaveID(nowAvatarID, i), jsonobject, savePath);
		return string.Concat(new object[]
		{
			text2,
			"\n[cab25b]时长：[-][fff0a4]",
			dateTime2.Year,
			"年",
			dateTime2.Month,
			"月",
			dateTime2.Day,
			"日"
		});
	}

	// Token: 0x0600105D RID: 4189 RVA: 0x000602C4 File Offset: 0x0005E4C4
	public void setInfoText(GameObject infoCell, int nowAvatarID, int i)
	{
		UILabel component = infoCell.transform.Find("saveinfo/Label").GetComponent<UILabel>();
		UILabel component2 = infoCell.transform.Find("savePercentage").GetComponent<UILabel>();
		component.text = this.getSaveFirstText(infoCell, nowAvatarID, i);
		if (YSSaveGame.HasKey("AvatarSavetime" + Tools.instance.getSaveID(nowAvatarID, i)))
		{
			component2.text = DateTime.Parse(YSSaveGame.GetString("AvatarSavetime" + Tools.instance.getSaveID(nowAvatarID, i), "")).ToString();
			return;
		}
		component2.text = "";
	}

	// Token: 0x0600105E RID: 4190 RVA: 0x00060368 File Offset: 0x0005E568
	public void setMainMenuInfoText(GameObject infoCell, int nowAvatarID, int i)
	{
		Text component = infoCell.transform.Find("saveName").GetComponent<Text>();
		Text component2 = infoCell.transform.Find("savePercentage").GetComponent<Text>();
		component.text = this.getSaveFirstText(infoCell, nowAvatarID, i);
		component2.text = YSSaveGame.GetString("AvatarSavetime" + Tools.instance.getSaveID(nowAvatarID, i), "");
	}

	// Token: 0x0600105F RID: 4191 RVA: 0x000603D4 File Offset: 0x0005E5D4
	public void setMainMenuInfoCell(GameObject infoCell, int avatarid, int i)
	{
		AvatarInfoCell component = infoCell.GetComponent<AvatarInfoCell>();
		infoCell.transform.Find("saveName").GetComponent<Text>().text = "空";
		infoCell.transform.Find("savePercentage").GetComponent<Text>().text = "—";
		component.fileType = AvatarInfoCell.FileType.LOAD;
		if (YSSaveGame.GetInt("SaveAvatar" + Tools.instance.getSaveID(avatarid, i), 0) == 1)
		{
			this.setMainMenuInfoText(infoCell, avatarid, i);
		}
		component.index = i;
	}

	// Token: 0x06001060 RID: 4192 RVA: 0x0006045C File Offset: 0x0005E65C
	public void showSave()
	{
		for (int i = 0; i < 6; i++)
		{
			GameObject infoCell = this.getInfoCell(i, AvatarInfoCell.FileType.SAVE);
			this.setInfoCell(infoCell, PlayerPrefs.GetInt("NowPlayerFileAvatar"), i);
			if (i == 0)
			{
				infoCell.transform.Find("Texture/Label").GetComponent<UILabel>().text = "自动保存";
			}
			if (i > 0)
			{
				infoCell.transform.Find("Texture/Label").GetComponent<UILabel>().text = "存储";
			}
		}
	}

	// Token: 0x06001061 RID: 4193 RVA: 0x000604D8 File Offset: 0x0005E6D8
	public void showLoad()
	{
		for (int i = 0; i < 6; i++)
		{
			GameObject infoCell = this.getInfoCell(i, AvatarInfoCell.FileType.LOAD);
			this.setInfoCell(infoCell, PlayerPrefs.GetInt("NowPlayerFileAvatar"), i);
			if (i == 0)
			{
				infoCell.transform.Find("Texture/Label").GetComponent<UILabel>().text = "快速读取";
			}
			if (i > 0)
			{
				infoCell.transform.Find("Texture/Label").GetComponent<UILabel>().text = "读取";
			}
		}
	}

	// Token: 0x06001062 RID: 4194 RVA: 0x00060551 File Offset: 0x0005E751
	public void escGame()
	{
		selectBox.instence.setChoice("是否确认退出游戏", new EventDelegate(new EventDelegate.Callback(Application.Quit)), null);
	}

	// Token: 0x06001063 RID: 4195 RVA: 0x00060574 File Offset: 0x0005E774
	public void goToHome()
	{
		selectBox.instence.setChoice("是否确认回到主界面", new EventDelegate(delegate()
		{
			YSSaveGame.Reset();
			KBEngineApp.app.entities[10] = null;
			KBEngineApp.app.entities.Remove(10);
			Tools.instance.loadOtherScenes("MainMenu");
		}), null);
	}

	// Token: 0x06001064 RID: 4196 RVA: 0x000605AC File Offset: 0x0005E7AC
	public void showSet()
	{
		int num = 0;
		foreach (UIselect uiselect in this.uiselect)
		{
			uiselect.setIndex(PlayerPrefs.GetInt("SavePlayerSet" + num, 0));
			if (num == 2)
			{
				uiselect.setIndex(PlayerPrefs.GetInt("SavePlayerSet" + num, 10));
				uiselect.setVoidSprite();
			}
			num++;
		}
	}

	// Token: 0x06001065 RID: 4197 RVA: 0x00060644 File Offset: 0x0005E844
	public void yinYong()
	{
		int num = 0;
		foreach (UIselect uiselect in this.uiselect)
		{
			PlayerPrefs.SetInt("SavePlayerSet" + num, uiselect.NowIndex);
			num++;
		}
		this.audioMixer.SetFloat("MastarValue", (float)PlayerPrefs.GetInt("SavePlayerSet2", 0) / 10f);
		this.audioMixer.SetFloat("BackgroudValue", 1f);
		this.audioMixer.SetFloat("MastarValue", 1f);
		MusicMag.instance.initAudio();
	}

	// Token: 0x04000BD9 RID: 3033
	public GameObject UIGrid;

	// Token: 0x04000BDA RID: 3034
	public GameObject Temp;

	// Token: 0x04000BDB RID: 3035
	public List<GameObject> uilist = new List<GameObject>();

	// Token: 0x04000BDC RID: 3036
	public AudioMixer audioMixer;

	// Token: 0x04000BDD RID: 3037
	public bool initFile;

	// Token: 0x04000BDE RID: 3038
	public List<UIselect> uiselect = new List<UIselect>();

	// Token: 0x04000BDF RID: 3039
	public List<Vector2> fenbianlu = new List<Vector2>();
}
