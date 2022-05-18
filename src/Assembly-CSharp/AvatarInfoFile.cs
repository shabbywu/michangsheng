using System;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using YSGame;

// Token: 0x0200025F RID: 607
public class AvatarInfoFile : MonoBehaviour
{
	// Token: 0x060012A5 RID: 4773 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x060012A6 RID: 4774 RVA: 0x00011B40 File Offset: 0x0000FD40
	private void Awake()
	{
		if (this.initFile)
		{
			this.showSave();
			base.Invoke("InvokeShowSave", 0.1f);
		}
	}

	// Token: 0x060012A7 RID: 4775 RVA: 0x00011B60 File Offset: 0x0000FD60
	private void InvokeShowSave()
	{
		base.transform.parent.Find("inventoryBtn/save").GetComponentInChildren<UIToggle>().value = true;
	}

	// Token: 0x060012A8 RID: 4776 RVA: 0x00011B82 File Offset: 0x0000FD82
	public void showUIPage()
	{
		base.gameObject.SetActive(true);
	}

	// Token: 0x060012A9 RID: 4777 RVA: 0x000AED80 File Offset: 0x000ACF80
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

	// Token: 0x060012AA RID: 4778 RVA: 0x00011B90 File Offset: 0x0000FD90
	public void showConfig()
	{
		base.transform.parent.Find("inventoryBtn/config").GetComponentInChildren<UIToggle>().value = true;
	}

	// Token: 0x060012AB RID: 4779 RVA: 0x000AEDE0 File Offset: 0x000ACFE0
	public void clearUiGrid()
	{
		foreach (object obj in this.UIGrid.transform)
		{
			Object.Destroy(((Transform)obj).gameObject);
		}
	}

	// Token: 0x060012AC RID: 4780 RVA: 0x00011BB2 File Offset: 0x0000FDB2
	public GameObject getInfoCell(int index, AvatarInfoCell.FileType type)
	{
		GameObject gameObject = this.UIGrid.transform.GetChild(index).gameObject;
		gameObject.GetComponent<AvatarInfoCell>().fileType = type;
		return gameObject;
	}

	// Token: 0x060012AD RID: 4781 RVA: 0x000AEE40 File Offset: 0x000AD040
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

	// Token: 0x060012AE RID: 4782 RVA: 0x000AEFA4 File Offset: 0x000AD1A4
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

	// Token: 0x060012AF RID: 4783 RVA: 0x000AF210 File Offset: 0x000AD410
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

	// Token: 0x060012B0 RID: 4784 RVA: 0x000AF2B4 File Offset: 0x000AD4B4
	public void setMainMenuInfoText(GameObject infoCell, int nowAvatarID, int i)
	{
		Text component = infoCell.transform.Find("saveName").GetComponent<Text>();
		Text component2 = infoCell.transform.Find("savePercentage").GetComponent<Text>();
		component.text = this.getSaveFirstText(infoCell, nowAvatarID, i);
		component2.text = YSSaveGame.GetString("AvatarSavetime" + Tools.instance.getSaveID(nowAvatarID, i), "");
	}

	// Token: 0x060012B1 RID: 4785 RVA: 0x000AF320 File Offset: 0x000AD520
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

	// Token: 0x060012B2 RID: 4786 RVA: 0x000AF3A8 File Offset: 0x000AD5A8
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

	// Token: 0x060012B3 RID: 4787 RVA: 0x000AF424 File Offset: 0x000AD624
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

	// Token: 0x060012B4 RID: 4788 RVA: 0x00011BD6 File Offset: 0x0000FDD6
	public void escGame()
	{
		selectBox.instence.setChoice("是否确认退出游戏", new EventDelegate(new EventDelegate.Callback(Application.Quit)), null);
	}

	// Token: 0x060012B5 RID: 4789 RVA: 0x00011BF9 File Offset: 0x0000FDF9
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

	// Token: 0x060012B6 RID: 4790 RVA: 0x000AF4A0 File Offset: 0x000AD6A0
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

	// Token: 0x060012B7 RID: 4791 RVA: 0x000AF538 File Offset: 0x000AD738
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

	// Token: 0x04000EAE RID: 3758
	public GameObject UIGrid;

	// Token: 0x04000EAF RID: 3759
	public GameObject Temp;

	// Token: 0x04000EB0 RID: 3760
	public List<GameObject> uilist = new List<GameObject>();

	// Token: 0x04000EB1 RID: 3761
	public AudioMixer audioMixer;

	// Token: 0x04000EB2 RID: 3762
	public bool initFile;

	// Token: 0x04000EB3 RID: 3763
	public List<UIselect> uiselect = new List<UIselect>();

	// Token: 0x04000EB4 RID: 3764
	public List<Vector2> fenbianlu = new List<Vector2>();
}
