using System;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using YSGame;

public class AvatarInfoFile : MonoBehaviour
{
	public GameObject UIGrid;

	public GameObject Temp;

	public List<GameObject> uilist = new List<GameObject>();

	public AudioMixer audioMixer;

	public bool initFile;

	public List<UIselect> uiselect = new List<UIselect>();

	public List<Vector2> fenbianlu = new List<Vector2>();

	private void Start()
	{
	}

	private void Awake()
	{
		if (initFile)
		{
			showSave();
			((MonoBehaviour)this).Invoke("InvokeShowSave", 0.1f);
		}
	}

	private void InvokeShowSave()
	{
		((Component)((Component)this).transform.parent.Find("inventoryBtn/save")).GetComponentInChildren<UIToggle>().value = true;
	}

	public void showUIPage()
	{
		((Component)this).gameObject.SetActive(true);
	}

	public void FirstShow()
	{
		if (PlayerPrefs.GetInt("NowPlayerFileAvatar") < 100)
		{
			if (((Component)((Component)this).transform.parent.Find("inventoryBtn/save")).GetComponentInChildren<UIToggle>().value)
			{
				((MonoBehaviour)this).Invoke("showUIPage", 0.01f);
			}
		}
		else
		{
			((MonoBehaviour)this).Invoke("showConfig", 0.1f);
		}
	}

	public void showConfig()
	{
		((Component)((Component)this).transform.parent.Find("inventoryBtn/config")).GetComponentInChildren<UIToggle>().value = true;
	}

	public void clearUiGrid()
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		foreach (Transform item in UIGrid.transform)
		{
			Object.Destroy((Object)(object)((Component)item).gameObject);
		}
	}

	public GameObject getInfoCell(int index, AvatarInfoCell.FileType type)
	{
		GameObject gameObject = ((Component)UIGrid.transform.GetChild(index)).gameObject;
		gameObject.GetComponent<AvatarInfoCell>().fileType = type;
		return gameObject;
	}

	public void setInfoCell(GameObject infoCell, int avatarid, int i)
	{
		AvatarInfoCell component = infoCell.GetComponent<AvatarInfoCell>();
		bool flag = false;
		if (YSSaveGame.HasFile(Paths.GetSavePath(), "AvatarInfo" + Tools.instance.getSaveID(avatarid, i)) && !YSSaveGame.GetJsonObject("AvatarInfo" + Tools.instance.getSaveID(avatarid, i)).IsNull)
		{
			flag = true;
		}
		if (flag)
		{
			((Component)infoCell.transform.Find("empty")).gameObject.SetActive(false);
			((Component)infoCell.transform.Find("saveinfo")).gameObject.SetActive(true);
			setInfoText(infoCell, avatarid, i);
		}
		else
		{
			((Component)infoCell.transform.Find("empty")).gameObject.SetActive(true);
			((Component)infoCell.transform.Find("saveinfo")).gameObject.SetActive(false);
			if (component.fileType == AvatarInfoCell.FileType.LOAD)
			{
				((Component)infoCell.transform.Find("empty/Label")).GetComponent<UILabel>().text = "空";
				((Component)infoCell.transform.Find("empty/Texture")).gameObject.SetActive(false);
			}
			else
			{
				((Component)infoCell.transform.Find("empty/Label")).GetComponent<UILabel>().text = "新建";
				((Component)infoCell.transform.Find("empty/Texture")).gameObject.SetActive(true);
			}
		}
		component.index = i;
	}

	public string getSaveFirstText(GameObject infoCell, int nowAvatarID, int i)
	{
		string savePath = Paths.GetSavePath();
		if (YSSaveGame.HasFile(savePath, "AvatarInfo" + Tools.instance.getSaveID(nowAvatarID, i)))
		{
			new JSONObject();
			JSONObject jsonObject = YSSaveGame.GetJsonObject("AvatarInfo" + Tools.instance.getSaveID(nowAvatarID, i));
			int i2 = jsonObject["avatarLevel"].I;
			string text = "[cab25b]境界：[-][fff0a4]" + Tools.instance.Code64ToString(jsonData.instance.LevelUpDataJsonData[string.Concat(i2)]["Name"].str);
			DateTime dateTime = DateTime.Parse(jsonObject["gameTime"].str);
			return text + "\n[cab25b]时长：[-][fff0a4]" + dateTime.Year + "年" + dateTime.Month + "月" + dateTime.Day + "日";
		}
		Avatar avatar = new Avatar();
		Tools.GetValue<Avatar>("Avatar" + Tools.instance.getSaveID(nowAvatarID, i), avatar);
		ushort level = avatar.level;
		string text2 = "[cab25b]境界：[-][fff0a4]" + Tools.instance.Code64ToString(jsonData.instance.LevelUpDataJsonData[string.Concat(level)]["Name"].str);
		DateTime dateTime2 = DateTime.Parse(avatar.worldTimeMag.nowTime);
		JSONObject jSONObject = new JSONObject();
		jSONObject.SetField("firstName", avatar.firstName);
		jSONObject.SetField("lastName", avatar.lastName);
		jSONObject.SetField("gameTime", avatar.worldTimeMag.nowTime);
		jSONObject.SetField("avatarLevel", avatar.level);
		YSSaveGame.save("AvatarInfo" + Tools.instance.getSaveID(nowAvatarID, i), jSONObject, savePath);
		return text2 + "\n[cab25b]时长：[-][fff0a4]" + dateTime2.Year + "年" + dateTime2.Month + "月" + dateTime2.Day + "日";
	}

	public void setInfoText(GameObject infoCell, int nowAvatarID, int i)
	{
		UILabel component = ((Component)infoCell.transform.Find("saveinfo/Label")).GetComponent<UILabel>();
		UILabel component2 = ((Component)infoCell.transform.Find("savePercentage")).GetComponent<UILabel>();
		component.text = getSaveFirstText(infoCell, nowAvatarID, i);
		if (YSSaveGame.HasKey("AvatarSavetime" + Tools.instance.getSaveID(nowAvatarID, i)))
		{
			component2.text = DateTime.Parse(YSSaveGame.GetString("AvatarSavetime" + Tools.instance.getSaveID(nowAvatarID, i))).ToString();
		}
		else
		{
			component2.text = "";
		}
	}

	public void setMainMenuInfoText(GameObject infoCell, int nowAvatarID, int i)
	{
		Text component = ((Component)infoCell.transform.Find("saveName")).GetComponent<Text>();
		Text component2 = ((Component)infoCell.transform.Find("savePercentage")).GetComponent<Text>();
		component.text = getSaveFirstText(infoCell, nowAvatarID, i);
		component2.text = YSSaveGame.GetString("AvatarSavetime" + Tools.instance.getSaveID(nowAvatarID, i));
	}

	public void setMainMenuInfoCell(GameObject infoCell, int avatarid, int i)
	{
		AvatarInfoCell component = infoCell.GetComponent<AvatarInfoCell>();
		((Component)infoCell.transform.Find("saveName")).GetComponent<Text>().text = "空";
		((Component)infoCell.transform.Find("savePercentage")).GetComponent<Text>().text = "—";
		component.fileType = AvatarInfoCell.FileType.LOAD;
		if (YSSaveGame.GetInt("SaveAvatar" + Tools.instance.getSaveID(avatarid, i)) == 1)
		{
			setMainMenuInfoText(infoCell, avatarid, i);
		}
		component.index = i;
	}

	public void showSave()
	{
		for (int i = 0; i < 6; i++)
		{
			GameObject infoCell = getInfoCell(i, AvatarInfoCell.FileType.SAVE);
			setInfoCell(infoCell, PlayerPrefs.GetInt("NowPlayerFileAvatar"), i);
			if (i == 0)
			{
				((Component)infoCell.transform.Find("Texture/Label")).GetComponent<UILabel>().text = "自动保存";
			}
			if (i > 0)
			{
				((Component)infoCell.transform.Find("Texture/Label")).GetComponent<UILabel>().text = "存储";
			}
		}
	}

	public void showLoad()
	{
		for (int i = 0; i < 6; i++)
		{
			GameObject infoCell = getInfoCell(i, AvatarInfoCell.FileType.LOAD);
			setInfoCell(infoCell, PlayerPrefs.GetInt("NowPlayerFileAvatar"), i);
			if (i == 0)
			{
				((Component)infoCell.transform.Find("Texture/Label")).GetComponent<UILabel>().text = "快速读取";
			}
			if (i > 0)
			{
				((Component)infoCell.transform.Find("Texture/Label")).GetComponent<UILabel>().text = "读取";
			}
		}
	}

	public void escGame()
	{
		selectBox.instence.setChoice("是否确认退出游戏", new EventDelegate((EventDelegate.Callback)Application.Quit), null);
	}

	public void goToHome()
	{
		selectBox.instence.setChoice("是否确认回到主界面", new EventDelegate(delegate
		{
			YSSaveGame.Reset();
			KBEngineApp.app.entities[10] = null;
			KBEngineApp.app.entities.Remove(10);
			Tools.instance.loadOtherScenes("MainMenu");
		}), null);
	}

	public void showSet()
	{
		int num = 0;
		foreach (UIselect item in uiselect)
		{
			item.setIndex(PlayerPrefs.GetInt("SavePlayerSet" + num, 0));
			if (num == 2)
			{
				item.setIndex(PlayerPrefs.GetInt("SavePlayerSet" + num, 10));
				item.setVoidSprite();
			}
			num++;
		}
	}

	public void yinYong()
	{
		int num = 0;
		foreach (UIselect item in uiselect)
		{
			PlayerPrefs.SetInt("SavePlayerSet" + num, item.NowIndex);
			num++;
		}
		audioMixer.SetFloat("MastarValue", (float)PlayerPrefs.GetInt("SavePlayerSet2", 0) / 10f);
		audioMixer.SetFloat("BackgroudValue", 1f);
		audioMixer.SetFloat("MastarValue", 1f);
		MusicMag.instance.initAudio();
	}
}
