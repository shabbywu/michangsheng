using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using Bag;
using CaiYao;
using Fungus;
using GUIPackage;
using JSONClass;
using KBEngine;
using Newtonsoft.Json.Linq;
using Tab;
using UltimateSurvival;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using YSGame;
using YSGame.TuJian;

public class Tools : MonoBehaviour
{
	public delegate bool FindTokenMethod(JToken aa);

	public static Tools instance;

	public int MonstarID = 1;

	public int LunDaoNpcId = -1;

	public List<int> LunTiList = new List<int>();

	public bool IsSuiJiLunTi;

	public int LunTiNum;

	public int TargetLunTiNum;

	public MonstarMag monstarMag;

	public string FinalScene = "AllMaps";

	public int CanShowFightUI;

	public static bool canClickFlag = true;

	public int fubenLastIndex = -1;

	public CaiYao.ItemData CaiYaoData;

	public bool SeaRemoveMonstarFlag;

	public string SeaRemoveMonstarUUID = "";

	public bool ShowPingJin = true;

	public bool isInitSceneBtn;

	public int loadSceneType = -1;

	public string ohtherSceneName = "";

	public int CanFpRun = 1;

	public bool IsNeedLaterCheck;

	public Random random;

	public bool IsCanLoadSetTalk = true;

	private bool _isNeedSetTalk;

	public bool IsInDF;

	public bool isNewAvatar;

	public bool CanOpenTab = true;

	public bool IsLoadData;

	public static string jumpToName = "";

	public static float startSaveTime;

	public DateTime NextSaveTime;

	public bool isNeedSetTalk
	{
		get
		{
			return _isNeedSetTalk;
		}
		set
		{
			_isNeedSetTalk = value;
		}
	}

	private void Awake()
	{
		instance = this;
		random = new Random();
		monstarMag = new MonstarMag();
	}

	private void Start()
	{
	}

	public void SetCaiYaoData(CaiYao.ItemData data)
	{
		CaiYaoData = new CaiYao.ItemData(data.ItemId, data.ItemNum, data.AddNum, data.AddTime, data.HasEnemy, data.FirstEnemyId, data.ScondEnemyId);
	}

	public void startNomalFight(int monstarID)
	{
		instance.getPlayer();
		Object obj = Resources.Load("talkPrefab/OptionPrefab/OptionFight");
		((StartFight)Object.Instantiate<GameObject>((GameObject)(object)((obj is GameObject) ? obj : null)).GetComponentInChildren<Flowchart>().FindBlock("Splash")
			.CommandList[0]).MonstarID = monstarID;
	}

	public void startFight(int monstarID)
	{
		try
		{
			if ((Object)(object)FpUIMag.inst == (Object)null)
			{
				MonstarID = monstarID;
				Object.Instantiate<GameObject>(ResManager.inst.LoadPrefab("FpPanel"));
				FpUIMag.inst.Init();
			}
		}
		catch (Exception ex)
		{
			if ((Object)(object)FpUIMag.inst != (Object)null)
			{
				FpUIMag.inst.Close();
			}
			Debug.LogError((object)ex);
			Debug.LogError((object)("错误NPCId：" + monstarID));
			UIPopTip.Inst.Pop("获取NPC错误，请排查是否有mod冲突等问题");
		}
	}

	public static void ClearObj(Transform obj)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Expected O, but got Unknown
		foreach (Transform item in obj.parent)
		{
			Transform val = item;
			if (((Component)val).gameObject.activeSelf)
			{
				Object.Destroy((Object)(object)((Component)val).gameObject);
			}
		}
	}

	public static void ClearChild(Transform obj)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Expected O, but got Unknown
		foreach (Transform item in obj)
		{
			Transform val = item;
			if (((Component)val).gameObject.activeSelf)
			{
				Object.Destroy((Object)(object)((Component)val).gameObject);
			}
		}
	}

	public static string setColorByID(string name, int id)
	{
		int num;
		try
		{
			num = jsonData.instance.ItemJsonData[id.ToString()]["quality"].I;
		}
		catch (Exception)
		{
			Debug.LogError((object)$"该ID不存在品阶,ID:{id}");
			num = 1;
		}
		switch (num)
		{
		case 1:
			name = "<color=#d8d8ca>" + name + "</color>";
			break;
		case 2:
			name = "<color=#cce281>" + name + "</color>";
			break;
		case 3:
			name = "<color=#acfffe>" + name + "</color>";
			break;
		case 4:
			name = "<color=#f1b7f8>" + name + "</color>";
			break;
		case 5:
			name = "<color=#FFAC5F>" + name + "</color>";
			break;
		case 6:
			name = "<color=#ffb28b>" + name + "</color>";
			break;
		}
		return name;
	}

	public static JSONObject getSatticSkillItem(int skillId)
	{
		foreach (KeyValuePair<string, JSONObject> itemJsonDatum in jsonData.instance.ItemJsonData)
		{
			if (itemJsonDatum.Value["type"].I == 4)
			{
				float result = 0f;
				if (float.TryParse(itemJsonDatum.Value["desc"].str, out result) && (int)result == skillId)
				{
					return itemJsonDatum.Value;
				}
			}
		}
		return null;
	}

	public static int GetStaticSkillBookItemIDByStaticSkillID(int staticSkillID)
	{
		foreach (_ItemJsonData data in _ItemJsonData.DataList)
		{
			if (data.type == 4)
			{
				float result = 0f;
				if (float.TryParse(data.desc, out result) && (int)result == staticSkillID)
				{
					return data.id;
				}
			}
		}
		return -1;
	}

	public void StartRemoveSeaMonstarFight(string MonstarUUID)
	{
		SeaRemoveMonstarUUID = MonstarUUID;
		SeaRemoveMonstarFlag = true;
	}

	public void AutoSetSeaMonstartDie()
	{
		if (SeaRemoveMonstarFlag)
		{
			SeaRemoveMonstarFlag = false;
			instance.getPlayer().seaNodeMag.RemoveSeaMonstar(SeaRemoveMonstarUUID);
		}
	}

	public void AutoSeatSeaRunAway(bool isFp = false)
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Expected O, but got Unknown
		if (!PlayerEx.Player.lastScence.StartsWith("Sea") || !SeaRemoveMonstarFlag)
		{
			return;
		}
		if (isFp)
		{
			Scene activeScene = SceneManager.GetActiveScene();
			FinalScene = ((Scene)(ref activeScene)).name;
		}
		Avatar player = getPlayer();
		SeaRemoveMonstarFlag = false;
		int nowIndex = player.fubenContorl[FinalScene].NowIndex;
		int result = 0;
		List<int> list = new List<int>();
		if (!int.TryParse(FinalScene.Replace("Sea", ""), out result))
		{
			return;
		}
		foreach (int aroundIndex in EndlessSeaMag.GetAroundIndexList(nowIndex, 1, shizi: true))
		{
			if (aroundIndex != nowIndex)
			{
				int inSeaID = player.seaNodeMag.GetInSeaID(aroundIndex, EndlessSeaMag.MapWide);
				if (ContensInt((JArray)jsonData.instance.EndlessSeaHaiYuData[result.ToString()][(object)"shuxing"], inSeaID))
				{
					list.Add(aroundIndex);
				}
			}
		}
		if (list.Count > 0)
		{
			player.fubenContorl[FinalScene].NowIndex = list[jsonData.GetRandom() % list.Count];
		}
	}

	public static int CalcLingWuTime(int bookItemID)
	{
		if (!_ItemJsonData.DataDict.ContainsKey(bookItemID))
		{
			Debug.LogError((object)$"计算领悟时间出错，没有ID为{bookItemID}的书籍");
			return 10000;
		}
		_ItemJsonData itemJsonData = _ItemJsonData.DataDict[bookItemID];
		return CalcLingWuOrTuPoTime(itemJsonData.StuTime, itemJsonData.wuDao);
	}

	public static int CalcTuPoTime(int staticSkillID)
	{
		StaticSkillJsonData staticSkillJsonData = StaticSkillJsonData.DataDict[staticSkillID + 1];
		int staticSkillBookItemIDByStaticSkillID = GetStaticSkillBookItemIDByStaticSkillID(staticSkillJsonData.Skill_ID);
		if (!_ItemJsonData.DataDict.ContainsKey(staticSkillBookItemIDByStaticSkillID))
		{
			Debug.LogError((object)$"计算突破功法时间出错，没有ID为{staticSkillBookItemIDByStaticSkillID}的书籍");
			return 10000;
		}
		_ItemJsonData itemJsonData = _ItemJsonData.DataDict[staticSkillBookItemIDByStaticSkillID];
		return CalcLingWuOrTuPoTime(staticSkillJsonData.Skill_castTime, itemJsonData.wuDao);
	}

	public static int CalcLingWuOrTuPoTime(int baseTime, List<int> wuDao)
	{
		float num = 0f;
		Avatar player = PlayerEx.Player;
		float num3;
		if (player.wuXin > 100)
		{
			int num2 = Mathf.Min((int)player.wuXin, 200);
			num3 = 0.5f - (float)(num2 - 100) / 400f;
		}
		else
		{
			num3 = 1f - (float)player.wuXin / 200f;
		}
		if (wuDao.Count > 0)
		{
			int num4 = wuDao.Count / 2;
			for (int i = 0; i < wuDao.Count; i += 2)
			{
				int wuDaoType = wuDao[i];
				int wuDaoLevelByType = player.wuDaoMag.getWuDaoLevelByType(wuDaoType);
				float n = jsonData.instance.WuDaoJinJieJson[wuDaoLevelByType.ToString()]["JiaCheng"].n;
				n /= (float)num4;
				num += n;
			}
		}
		num = 1f - num;
		num = Mathf.Clamp(num, 0f, 1f);
		return (int)((float)baseTime * num3 * num);
	}

	public static int DayToYear(int day)
	{
		return day / 365;
	}

	public static int DayToMonth(int day)
	{
		return (day - 365 * DayToYear(day)) / 30;
	}

	public static int DayToDay(int day)
	{
		return day - 365 * DayToYear(day) - 30 * DayToMonth(day);
	}

	public static string getStr(string str)
	{
		if (StrTextJsonData.DataDict.ContainsKey(str))
		{
			return StrTextJsonData.DataDict[str].ChinaText;
		}
		Debug.LogError((object)("JSONClass.StrTextJsonData.DataDict[" + str + "]不存在"));
		return "None";
	}

	public Avatar getPlayer()
	{
		return (Avatar)KBEngineApp.app.player();
	}

	public bool CheckHasTianFu(int id)
	{
		return getPlayer().SelectTianFuID.list.Find((JSONObject aa) => (int)aa.n == id) != null;
	}

	public bool CheckHasTianFuSeid(int seid)
	{
		if (getPlayer().TianFuID.HasField(seid.ToString()))
		{
			return true;
		}
		return false;
	}

	public void ResetEquipSeid()
	{
		Avatar player = instance.getPlayer();
		player.EquipSeidFlag = new Dictionary<int, Dictionary<int, int>>();
		Dictionary<int, BaseItem> curEquipDict = player.StreamData.FangAnData.GetCurEquipDict();
		foreach (int key in curEquipDict.Keys)
		{
			if (curEquipDict[key].Seid == null && curEquipDict[key].Seid.HasField("ItemSeids"))
			{
				foreach (JSONObject item in curEquipDict[key].Seid["ItemSeids"].list)
				{
					Equips equips = new Equips(curEquipDict[key].Id, 0, 5);
					equips.ItemAddSeid = item;
					equips.Puting(player, player, 2);
				}
			}
			else
			{
				new Equips(curEquipDict[key].Id, 0, 5).Puting(player, player, 2);
			}
		}
		player.nowConfigEquipItem = player.StreamData.FangAnData.CurEquipIndex;
		player.equipItemList.values = player.StreamData.FangAnData.CurEquipDictToOldList();
	}

	public void NewAddItem(int id, int count, JSONObject seid, string uuid = "无", bool ShowText = false)
	{
		Avatar player = getPlayer();
		int type = _ItemJsonData.DataDict[id].type;
		if (type <= 2 || type == 14)
		{
			if (uuid == "无")
			{
				uuid = getUUID();
			}
			if (seid == null)
			{
				seid = CreateItemSeid(id);
			}
			if (seid != null && seid.HasField("isPaiMai"))
			{
				seid.RemoveField("isPaiMai");
			}
			player.AddEquip(id, uuid, seid);
		}
		else
		{
			player.addItem(id, count, seid, ShowText);
		}
	}

	public void RemoveItem(int id, int count = 1)
	{
		Avatar player = getPlayer();
		for (int num = count; num > 0; num--)
		{
			if (!RemoveEquip(id))
			{
				player.removeItem(id);
			}
		}
		ResetEquipSeid();
	}

	public void RemoveTieJian(int id)
	{
		Avatar player = getPlayer();
		Dictionary<int, BaseItem> curEquipDict = player.StreamData.FangAnData.GetCurEquipDict();
		foreach (int key in curEquipDict.Keys)
		{
			if (curEquipDict[key].Id == id)
			{
				curEquipDict.Remove(key);
			}
		}
		player.removeItem(id);
	}

	public void RemoveItem(string uuid, int count = 1)
	{
		Avatar player = getPlayer();
		for (int num = count; num > 0; num--)
		{
			RemoveEquip(uuid);
			player.removeItem(uuid);
		}
		ResetEquipSeid();
	}

	private void RemoveEquip(string uuid)
	{
		Dictionary<int, Dictionary<int, BaseItem>> equipDictionary = getPlayer().StreamData.FangAnData.EquipDictionary;
		Dictionary<int, List<int>> dictionary = new Dictionary<int, List<int>>();
		foreach (int key in equipDictionary.Keys)
		{
			foreach (int key2 in equipDictionary[key].Keys)
			{
				if (equipDictionary[key][key2].Uid == uuid)
				{
					if (!dictionary.ContainsKey(key))
					{
						dictionary.Add(key, new List<int> { key2 });
					}
					else
					{
						dictionary[key].Add(key2);
					}
				}
			}
		}
		if (dictionary.Count <= 0)
		{
			return;
		}
		foreach (int key3 in dictionary.Keys)
		{
			foreach (int item in dictionary[key3])
			{
				equipDictionary[key3].Remove(item);
				if (equipDictionary[key3].Count < 1)
				{
					equipDictionary.Remove(key3);
				}
			}
		}
	}

	private bool RemoveEquip(int id)
	{
		Dictionary<int, BaseItem> curEquipDict = getPlayer().StreamData.FangAnData.GetCurEquipDict();
		foreach (int key in curEquipDict.Keys)
		{
			if (curEquipDict[key].Id == id)
			{
				curEquipDict.Remove(key);
				return true;
			}
		}
		return false;
	}

	public string Code64ToString(string aa)
	{
		return aa.ToCN();
	}

	public static string Code64(string aa)
	{
		return aa.ToCN();
	}

	public static string getDescByID(string desstr, int skillID)
	{
		return getDesc(desstr, _skillJsonData.DataDict[skillID].HP);
	}

	private static void setAttackTxt(ref string desstr, int __attack)
	{
		string text = desstr.Substring(0, desstr.IndexOf("（"));
		string text2 = desstr.Substring(desstr.IndexOf("）"), desstr.Length - desstr.IndexOf("）"));
		string text3 = text2.Substring(1, text2.Length - 1);
		int length = desstr.Length - text3.Length - text.Length - 2;
		string expression = desstr.Substring(desstr.IndexOf("（") + 1, length).Replace("attack", string.Concat(__attack));
		object obj = new DataTable().Compute(expression, "");
		desstr = string.Concat(text, "[FF00FF]", obj, "[-]", text3);
		if (desstr.IndexOf("attack") > 0)
		{
			setAttackTxt(ref desstr, __attack);
		}
	}

	public static string getDesc(string desstr, int __attack)
	{
		if (desstr.IndexOf("attack") > 0)
		{
			setAttackTxt(ref desstr, __attack);
		}
		return desstr;
	}

	public string getSkillDesc(int skillID)
	{
		return getDesc(Code64ToString(jsonData.instance.skillJsonData[string.Concat(skillID)]["descr"].str), (int)jsonData.instance.skillJsonData[string.Concat(skillID)]["HP"].n);
	}

	public string getSkillName(int skillID, bool includecolor = false)
	{
		JSONObject jSONObject = jsonData.instance.skillJsonData[string.Concat(skillID)];
		string text = instance.Code64ToString(jSONObject["name"].str);
		string text2 = "";
		return "" + text.Replace("1", "").Replace("2", "").Replace("3", "")
			.Replace("4", "")
			.Replace("5", "") + text2;
	}

	public string getStaticSkillName(int skillID, bool includecolor = false)
	{
		JSONObject jSONObject = jsonData.instance.StaticSkillJsonData[string.Concat(skillID)];
		string text = instance.Code64ToString(jSONObject["name"].str);
		string text2 = "";
		return "" + text.Replace("1", "").Replace("2", "").Replace("3", "")
			.Replace("4", "")
			.Replace("5", "") + text2;
	}

	public Sprite getLevelSprite(int level, Rect rect)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Expected O, but got Unknown
		return Sprite.Create((Texture2D)Resources.Load("NewUI/Fight/LevelIcon/icon_" + level), rect, new Vector2(0.5f, 0.5f));
	}

	public string getSkillText(int skillID)
	{
		_ = jsonData.instance.skillJsonData[string.Concat(skillID)];
		string skillDesc = instance.getSkillDesc(skillID);
		instance.getSkillName(skillID);
		return "[FF0000]说明:[-] " + skillDesc;
	}

	public void setAvaterCanAttack(Entity targAvater)
	{
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			Avatar entity = (Avatar)KBEngineApp.app.player();
			string avaterType = jsonData.instance.getAvaterType(targAvater);
			string avaterType2 = jsonData.instance.getAvaterType(entity);
			if (avaterType != avaterType2)
			{
				((GameObject)targAvater.renderObj).GetComponent<GameEntity>().canAttack = true;
			}
			else
			{
				((GameObject)targAvater.renderObj).GetComponent<GameEntity>().canAttack = false;
			}
		}
		catch (Exception ex)
		{
			Debug.LogError((object)ex.ToString());
		}
	}

	public JSONObject readMGconfig()
	{
		try
		{
			MonoBehaviour.print((object)("Unity2:" + Application.persistentDataPath));
			StreamReader streamReader = new StreamReader(Application.persistentDataPath + "/MGconfig.txt");
			string text = streamReader.ReadToEnd();
			MonoBehaviour.print((object)("Unity3:" + text));
			JSONObject result = new JSONObject(text);
			streamReader.Close();
			MonoBehaviour.print((object)"Unity5:");
			return result;
		}
		catch (Exception)
		{
			StreamWriter streamWriter = new StreamWriter(Application.persistentDataPath + "/MGconfig.txt", append: false, Encoding.UTF8);
			streamWriter.Write("1");
			MonoBehaviour.print((object)"Unity4:");
			streamWriter.Close();
			return new JSONObject(JSONObject.Type.OBJECT);
		}
	}

	public void saveMGConfig(string encodedString)
	{
		MonoBehaviour.print((object)"Unity99:");
		MonoBehaviour.print((object)("Unity:" + Application.persistentDataPath + encodedString));
		StreamWriter streamWriter = new StreamWriter(Application.persistentDataPath + "/MGconfig.txt", append: false, Encoding.UTF8);
		streamWriter.Write("123");
		streamWriter.Close();
	}

	public string Encryption(string express)
	{
		using RSACryptoServiceProvider rSACryptoServiceProvider = new RSACryptoServiceProvider(new CspParameters
		{
			KeyContainerName = "oa_erp_dowork_lgmg"
		});
		byte[] bytes = Encoding.Default.GetBytes(express);
		return Convert.ToBase64String(rSACryptoServiceProvider.Encrypt(bytes, fOAEP: false));
	}

	public string Decrypt(string ciphertext)
	{
		using RSACryptoServiceProvider rSACryptoServiceProvider = new RSACryptoServiceProvider(new CspParameters
		{
			KeyContainerName = "oa_erp_dowork_lgmg"
		});
		byte[] rgb = Convert.FromBase64String(ciphertext);
		byte[] bytes = rSACryptoServiceProvider.Decrypt(rgb, fOAEP: false);
		return Encoding.Default.GetString(bytes);
	}

	public void loadMapScenes(string name, bool LastSceneIsValue = true)
	{
		if (name != "LianDan" && LastSceneIsValue)
		{
			instance.getPlayer().lastScence = name;
		}
		jumpToName = name;
		loadSceneType = 1;
		SceneManager.LoadScene(name);
		if ((Object)(object)PanelMamager.inst.UIBlackMaskGameObject == (Object)null)
		{
			Object.Instantiate<GameObject>(ResManager.inst.LoadPrefab("BlackHide"));
		}
		PanelMamager.inst.UIBlackMaskGameObject.SetActive(false);
		PanelMamager.inst.UIBlackMaskGameObject.SetActive(true);
		if ((Object)(object)PanelMamager.inst.UISceneGameObject == (Object)null)
		{
			SceneManager.LoadScene("UIScene", (LoadSceneMode)1);
		}
		if ((Object)(object)PanelMamager.inst.UISceneGameObject != (Object)null)
		{
			Transform val = PanelMamager.inst.UISceneGameObject.transform.Find("ThreeSceneNpcCanvas");
			if ((Object)(object)val != (Object)null)
			{
				((Component)val).gameObject.SetActive(true);
			}
		}
		if ((Object)(object)UI_Manager.inst != (Object)null)
		{
			if ((Object)(object)ThreeSceernUIFab.inst != (Object)null)
			{
				Object.Destroy((Object)(object)((Component)ThreeSceernUIFab.inst).gameObject);
			}
			if ((Object)(object)ThreeSceneMagFab.inst != (Object)null)
			{
				Object.Destroy((Object)(object)((Component)ThreeSceneMagFab.inst).gameObject);
			}
		}
		isNeedSetTalk = true;
		CanOpenTab = true;
	}

	public void loadOtherScenes(string name)
	{
		loadSceneType = 0;
		ohtherSceneName = name;
		isNeedSetTalk = true;
		if ((Object)(object)SingletonMono<TabUIMag>.Instance != (Object)null)
		{
			SingletonMono<TabUIMag>.Instance.TryEscClose();
		}
		SceneManager.LoadScene("NextScene");
		CanOpenTab = false;
	}

	public static void startPaiMai()
	{
		instance.loadOtherScenes("PaiMai");
	}

	public bool isEquip(int id)
	{
		bool result = false;
		if ((int)jsonData.instance.ItemJsonData[string.Concat(id)]["vagueType"].n == 0)
		{
			result = true;
		}
		return result;
	}

	public void findSkillBy()
	{
	}

	public int getSkillIDByKey(int key)
	{
		if (key <= 0)
		{
			return -1;
		}
		return _skillJsonData.DataDict[key].Skill_ID;
	}

	public int getStaticSkillIDByKey(int key)
	{
		if (key < 0)
		{
			return -1;
		}
		return StaticSkillJsonData.DataDict[key].Skill_ID;
	}

	public int getSkillKeyByID(int ID, Avatar _avatar)
	{
		List<_skillJsonData> list = _skillJsonData.DataList.FindAll((_skillJsonData s) => s.Skill_ID == ID);
		if (list.Count == 1)
		{
			return list[0].id;
		}
		if (list.Count >= 5)
		{
			int levelType = _avatar.getLevelType();
			foreach (_skillJsonData item in list)
			{
				if (item.Skill_Lv == levelType)
				{
					return item.id;
				}
			}
		}
		return -1;
	}

	public int getStaticSkillKeyByID(int ID)
	{
		Avatar avatar = (Avatar)KBEngineApp.app.player();
		foreach (StaticSkillJsonData data in StaticSkillJsonData.DataList)
		{
			if (data.Skill_ID != ID)
			{
				continue;
			}
			foreach (SkillItem hasStaticSkill in avatar.hasStaticSkillList)
			{
				if (hasStaticSkill.level == data.Skill_Lv && hasStaticSkill.itemId == ID)
				{
					return data.id;
				}
			}
		}
		return -1;
	}

	public int getStaticSkillKeyByID(int ID, int level)
	{
		foreach (StaticSkillJsonData data in StaticSkillJsonData.DataList)
		{
			if (data.Skill_ID == ID && data.Skill_Lv == level)
			{
				return data.id;
			}
		}
		return -1;
	}

	public static void Save(string objectName, object o, string gamePath = "-1")
	{
		//IL_0c03: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c0d: Expected O, but got Unknown
		FieldInfo[] fields = o.GetType().GetFields();
		JSONObject jSONObject = new JSONObject(JSONObject.Type.OBJECT);
		for (int i = 0; i < fields.Length; i++)
		{
			string text = objectName + "." + fields[i].Name;
			switch (fields[i].FieldType.Name)
			{
			case "String":
				jSONObject.AddField(text, fields[i].GetValue(o).ToString());
				break;
			case "Int32":
			case "Int64":
			case "Int16":
			case "UInt32":
			case "UInt16":
			case "UInt64":
			case "Int":
			case "uInt":
			{
				long val = Convert.ToInt64(fields[i].GetValue(o));
				jSONObject.AddField(text, val);
				break;
			}
			case "Float":
				jSONObject.AddField(text, (float)fields[i].GetValue(o));
				break;
			case "List`1":
			{
				if (fields[i].Name == "hasSkillList" || fields[i].Name == "hasStaticSkillList" || fields[i].Name == "hasJieDanSkillList" || fields[i].Name == "equipSkillList" || fields[i].Name == "equipStaticSkillList")
				{
					int num3 = 0;
					foreach (SkillItem item in (List<SkillItem>)fields[i].GetValue(o))
					{
						jSONObject.AddField(text + ".UUID." + num3, item.uuid);
						jSONObject.AddField(text + ".id." + num3, item.itemId);
						jSONObject.AddField(text + ".level." + num3, item.level);
						jSONObject.AddField(text + ".index." + num3, item.itemIndex);
						jSONObject.AddField(text + ".Seid." + num3, item.Seid);
						num3++;
					}
					jSONObject.AddField(text + ".Num", num3);
				}
				if (fields[i].Name == "bufflist")
				{
					JSONObject jSONObject2 = new JSONObject(JSONObject.Type.ARRAY);
					foreach (List<int> item2 in (List<List<int>>)fields[i].GetValue(o))
					{
						JSONObject jSONObject3 = new JSONObject(JSONObject.Type.ARRAY);
						foreach (int item3 in item2)
						{
							jSONObject3.Add(item3);
						}
						jSONObject2.Add(jSONObject3);
					}
					jSONObject.AddField(text + ".bufflist", jSONObject2);
				}
				if (!(fields[i].Name == "LingGeng"))
				{
					break;
				}
				int num4 = 0;
				foreach (int item4 in (List<int>)fields[i].GetValue(o))
				{
					jSONObject.AddField(text + ".id." + num4, item4);
					num4++;
				}
				jSONObject.AddField(text + ".Num", num4);
				break;
			}
			case "List`1[]":
			{
				if (!(fields[i].Name == "configEquipSkill") && !(fields[i].Name == "configEquipStaticSkill"))
				{
					break;
				}
				int num = 0;
				List<SkillItem>[] array = (List<SkillItem>[])fields[i].GetValue(o);
				foreach (List<SkillItem> obj3 in array)
				{
					int num2 = 0;
					foreach (SkillItem item5 in obj3)
					{
						jSONObject.AddField(text + num + ".UUID." + num2, item5.uuid);
						jSONObject.AddField(text + num + ".id." + num2, item5.itemId);
						jSONObject.AddField(text + num + ".level." + num2, item5.level);
						jSONObject.AddField(text + num + ".index." + num2, item5.itemIndex);
						jSONObject.AddField(text + num + ".Seid." + num2, item5.Seid);
						num2++;
					}
					jSONObject.AddField(text + num + ".Num", num2);
					num++;
				}
				break;
			}
			case "ITEM_INFO_LIST":
			{
				int num7 = 0;
				foreach (ITEM_INFO value in ((ITEM_INFO_LIST)fields[i].GetValue(o)).values)
				{
					jSONObject.AddField(text + ".UUID." + num7, value.uuid);
					jSONObject.AddField(text + ".id." + num7, value.itemId);
					jSONObject.AddField(text + ".count." + num7, (int)value.itemCount);
					jSONObject.AddField(text + ".index." + num7, value.itemIndex);
					jSONObject.AddField(text + ".Seid." + num7, value.Seid);
					num7++;
				}
				jSONObject.AddField(text + ".Num", num7);
				break;
			}
			case "ITEM_INFO_LIST[]":
			{
				int num5 = 0;
				ITEM_INFO_LIST[] array2 = (ITEM_INFO_LIST[])fields[i].GetValue(o);
				foreach (ITEM_INFO_LIST obj4 in array2)
				{
					int num6 = 0;
					foreach (ITEM_INFO value2 in obj4.values)
					{
						jSONObject.AddField(text + num5 + ".UUID." + num6, value2.uuid);
						jSONObject.AddField(text + num5 + ".id." + num6, value2.itemId);
						jSONObject.AddField(text + num5 + ".count." + num6, (int)value2.itemCount);
						jSONObject.AddField(text + num5 + ".index." + num6, value2.itemIndex);
						jSONObject.AddField(text + num5 + ".Seid." + num6, value2.Seid);
						num6++;
					}
					jSONObject.AddField(text + num5 + ".Num", num6);
					num5++;
				}
				break;
			}
			case "AvatarStaticValue":
			{
				AvatarStaticValue avatarStaticValue = (AvatarStaticValue)fields[i].GetValue(o);
				for (int k = 0; k < 2500; k++)
				{
					jSONObject.AddField(text + ".Value." + k, avatarStaticValue.Value[k]);
				}
				jSONObject.AddField(text + ".talk", avatarStaticValue.talk[0]);
				break;
			}
			case "WorldTime":
			{
				WorldTime worldTime = (WorldTime)fields[i].GetValue(o);
				jSONObject.AddField(text + ".nowTime", worldTime.nowTime ?? "");
				break;
			}
			case "EmailDataMag":
			{
				EmailDataMag graph = (EmailDataMag)fields[i].GetValue(o);
				FileStream fileStream = new FileStream(Paths.GetSavePath() + "/EmailDataMag" + objectName.Replace("Avatar", "") + ".sav", FileMode.Create);
				new BinaryFormatter().Serialize(fileStream, graph);
				fileStream.Close();
				break;
			}
			case "TaskMag":
			{
				TaskMag taskMag = (TaskMag)fields[i].GetValue(o);
				jSONObject.AddField(text + "._TaskData", taskMag._TaskData);
				break;
			}
			case "JSONObject":
			{
				JSONObject obj2 = (JSONObject)fields[i].GetValue(o);
				jSONObject.AddField(text + "._JSONObject", obj2);
				break;
			}
			case "JObject":
			{
				JSONObject obj = new JSONObject(((object)(JObject)fields[i].GetValue(o)).ToString());
				jSONObject.AddField(text + "._JObject", obj);
				break;
			}
			}
		}
		YSGame.YSSaveGame.save(objectName, jSONObject, gamePath);
	}

	public static void GetValue<T>(string objectName, Avatar avatar) where T : Avatar, new()
	{
		JSONObject jsonObject = YSGame.YSSaveGame.GetJsonObject(objectName);
		FieldInfo[] fields = avatar.GetType().GetFields();
		for (int i = 0; i < fields.Length; i++)
		{
			string text = objectName + "." + fields[i].Name;
			switch (fields[i].FieldType.Name)
			{
			case "String":
				if (YSGame.YSSaveGame.GetString(jsonObject, text) != "")
				{
					fields[i].SetValue(avatar, YSGame.YSSaveGame.GetString(jsonObject, text));
				}
				break;
			case "Int32":
			case "Int64":
			case "Int":
			case "uInt":
				if (YSGame.YSSaveGame.GetInt(jsonObject, text) != 0)
				{
					fields[i].SetValue(avatar, YSGame.YSSaveGame.GetInt(jsonObject, text));
				}
				break;
			case "UInt32":
			case "UInt16":
			case "Int16":
			case "UInt64":
				if (YSGame.YSSaveGame.GetInt(jsonObject, text) != 0)
				{
					if (fields[i].FieldType.Name == "UInt32")
					{
						fields[i].SetValue(avatar, Convert.ToUInt32(YSGame.YSSaveGame.GetInt(jsonObject, text)));
					}
					if (fields[i].FieldType.Name == "UInt16")
					{
						fields[i].SetValue(avatar, Convert.ToUInt16(YSGame.YSSaveGame.GetInt(jsonObject, text)));
					}
					if (fields[i].FieldType.Name == "Int16")
					{
						fields[i].SetValue(avatar, Convert.ToInt16(YSGame.YSSaveGame.GetInt(jsonObject, text)));
					}
					if (fields[i].FieldType.Name == "UInt64")
					{
						fields[i].SetValue(avatar, Convert.ToUInt64(YSGame.YSSaveGame.GetInt(jsonObject, text)));
					}
				}
				break;
			case "Float":
				fields[i].SetValue(avatar, YSGame.YSSaveGame.GetFloat(jsonObject, text));
				break;
			case "List`1":
				if (fields[i].Name == "hasSkillList" || fields[i].Name == "hasStaticSkillList" || fields[i].Name == "hasJieDanSkillList" || fields[i].Name == "equipSkillList" || fields[i].Name == "equipStaticSkillList")
				{
					int int2 = YSGame.YSSaveGame.GetInt(jsonObject, text + ".Num", -1);
					List<SkillItem> list = new List<SkillItem>();
					for (int k = 0; k < int2; k++)
					{
						SkillItem skillItem = new SkillItem();
						skillItem.uuid = YSGame.YSSaveGame.GetString(jsonObject, text + ".UUID." + k);
						if (skillItem.uuid == "")
						{
							skillItem.uuid = getUUID();
						}
						skillItem.itemId = YSGame.YSSaveGame.GetInt(jsonObject, text + ".id." + k);
						skillItem.level = YSGame.YSSaveGame.GetInt(jsonObject, text + ".level." + k);
						skillItem.itemIndex = YSGame.YSSaveGame.GetInt(jsonObject, text + ".index." + k);
						JSONObject jsonObject8 = YSGame.YSSaveGame.GetJsonObject(jsonObject, text + ".Seid." + k);
						if (jsonObject8 != null)
						{
							skillItem.Seid = jsonObject8;
						}
						list.Add(skillItem);
					}
					fields[i].SetValue(avatar, list);
				}
				if (fields[i].Name == "bufflist")
				{
					JSONObject jsonObject9 = YSGame.YSSaveGame.GetJsonObject(jsonObject, text + ".bufflist");
					if (jsonObject9 == null || jsonObject9.Count <= 0)
					{
						break;
					}
					List<List<int>> list2 = new List<List<int>>();
					foreach (JSONObject item in jsonObject9.list)
					{
						List<int> list3 = new List<int>();
						foreach (JSONObject item2 in item.list)
						{
							list3.Add(item2.I);
						}
						list2.Add(list3);
					}
					fields[i].SetValue(avatar, list2);
				}
				if (fields[i].Name == "LingGeng")
				{
					int int3 = YSGame.YSSaveGame.GetInt(jsonObject, text + ".Num", 5);
					List<int> list4 = new List<int>();
					for (int l = 0; l < int3; l++)
					{
						list4.Add(YSGame.YSSaveGame.GetInt(jsonObject, text + ".id." + l, 1));
					}
					fields[i].SetValue(avatar, list4);
				}
				break;
			case "List`1[]":
			{
				if (!(fields[i].Name == "configEquipSkill") && !(fields[i].Name == "configEquipStaticSkill"))
				{
					break;
				}
				List<SkillItem>[] array = new List<SkillItem>[5]
				{
					new List<SkillItem>(),
					new List<SkillItem>(),
					new List<SkillItem>(),
					new List<SkillItem>(),
					new List<SkillItem>()
				};
				int num = 0;
				List<SkillItem>[] array2 = array;
				for (int n = 0; n < array2.Length; n++)
				{
					_ = array2[n];
					int int4 = YSGame.YSSaveGame.GetInt(jsonObject, text + num + ".Num", -1);
					for (int num2 = 0; num2 < int4; num2++)
					{
						SkillItem skillItem2 = new SkillItem();
						skillItem2.uuid = YSGame.YSSaveGame.GetString(jsonObject, text + num + ".UUID." + num2);
						if (skillItem2.uuid == "")
						{
							skillItem2.uuid = getUUID();
						}
						skillItem2.itemId = YSGame.YSSaveGame.GetInt(jsonObject, text + num + ".id." + num2);
						skillItem2.level = YSGame.YSSaveGame.GetInt(jsonObject, text + num + ".level." + num2);
						skillItem2.itemIndex = YSGame.YSSaveGame.GetInt(jsonObject, text + num + ".index." + num2);
						JSONObject jsonObject11 = YSGame.YSSaveGame.GetJsonObject(jsonObject, text + num + ".Seid." + num2);
						if (jsonObject11 != null)
						{
							skillItem2.Seid = jsonObject11;
						}
						array[num].Add(skillItem2);
					}
					num++;
				}
				fields[i].SetValue(avatar, array);
				break;
			}
			case "ITEM_INFO_LIST":
			{
				int @int = YSGame.YSSaveGame.GetInt(jsonObject, text + ".Num", -1);
				ITEM_INFO_LIST iTEM_INFO_LIST = new ITEM_INFO_LIST();
				for (int j = 0; j < @int; j++)
				{
					ITEM_INFO iTEM_INFO = new ITEM_INFO();
					iTEM_INFO.uuid = YSGame.YSSaveGame.GetString(jsonObject, text + ".UUID." + j);
					if (iTEM_INFO.uuid == "")
					{
						iTEM_INFO.uuid = getUUID();
					}
					iTEM_INFO.itemId = YSGame.YSSaveGame.GetInt(jsonObject, text + ".id." + j);
					iTEM_INFO.itemCount = (uint)YSGame.YSSaveGame.GetInt(jsonObject, text + ".count." + j);
					iTEM_INFO.itemIndex = YSGame.YSSaveGame.GetInt(jsonObject, text + ".index." + j);
					JSONObject jsonObject3 = YSGame.YSSaveGame.GetJsonObject(jsonObject, text + ".Seid." + j);
					if (jsonObject3 != null)
					{
						iTEM_INFO.Seid = jsonObject3;
					}
					iTEM_INFO_LIST.values.Add(iTEM_INFO);
				}
				fields[i].SetValue(avatar, iTEM_INFO_LIST);
				break;
			}
			case "ITEM_INFO_LIST[]":
			{
				ITEM_INFO_LIST[] array3 = new ITEM_INFO_LIST[5]
				{
					new ITEM_INFO_LIST(),
					new ITEM_INFO_LIST(),
					new ITEM_INFO_LIST(),
					new ITEM_INFO_LIST(),
					new ITEM_INFO_LIST()
				};
				int num3 = 0;
				ITEM_INFO_LIST[] array4 = array3;
				foreach (ITEM_INFO_LIST iTEM_INFO_LIST2 in array4)
				{
					int int5 = YSGame.YSSaveGame.GetInt(jsonObject, text + num3 + ".Num", -1);
					for (int num4 = 0; num4 < int5; num4++)
					{
						ITEM_INFO iTEM_INFO2 = new ITEM_INFO();
						iTEM_INFO2.uuid = YSGame.YSSaveGame.GetString(jsonObject, text + num3 + ".UUID." + num4);
						if (iTEM_INFO2.uuid == "")
						{
							iTEM_INFO2.uuid = getUUID();
						}
						iTEM_INFO2.itemId = YSGame.YSSaveGame.GetInt(jsonObject, text + num3 + ".id." + num4);
						iTEM_INFO2.itemCount = (uint)YSGame.YSSaveGame.GetInt(jsonObject, text + num3 + ".count." + num4);
						iTEM_INFO2.itemIndex = YSGame.YSSaveGame.GetInt(jsonObject, text + num3 + ".index." + num4);
						JSONObject jsonObject12 = YSGame.YSSaveGame.GetJsonObject(jsonObject, text + num3 + ".Seid." + num4);
						if (jsonObject12 != null)
						{
							iTEM_INFO2.Seid = jsonObject12;
						}
						iTEM_INFO_LIST2.values.Add(iTEM_INFO2);
					}
					num3++;
				}
				fields[i].SetValue(avatar, array3);
				break;
			}
			case "AvatarStaticValue":
			{
				AvatarStaticValue avatarStaticValue = new AvatarStaticValue();
				for (int m = 0; m < 2500; m++)
				{
					avatarStaticValue.Value[m] = YSGame.YSSaveGame.GetInt(jsonObject, text + ".Value." + m);
				}
				avatarStaticValue.talk[0] = YSGame.YSSaveGame.GetInt(jsonObject, text + ".talk");
				fields[i].SetValue(avatar, avatarStaticValue);
				break;
			}
			case "WorldTime":
			{
				WorldTime worldTime = new WorldTime();
				worldTime.isLoadDate = true;
				worldTime.nowTime = YSGame.YSSaveGame.GetString(jsonObject, text + ".nowTime", "0016-1-1");
				fields[i].SetValue(avatar, worldTime);
				break;
			}
			case "TaskMag":
			{
				TaskMag taskMag = new TaskMag(avatar);
				JSONObject jsonObject10 = YSGame.YSSaveGame.GetJsonObject(jsonObject, text + "._TaskData");
				taskMag._TaskData = jsonObject10;
				fields[i].SetValue(avatar, taskMag);
				break;
			}
			case "EmailDataMag":
			{
				EmailDataMag emailDataMag = null;
				try
				{
					if (File.Exists(Paths.GetSavePath() + "/EmailDataMag" + objectName.Replace("Avatar", "") + ".sav"))
					{
						FileStream fileStream = new FileStream(Paths.GetSavePath() + "/EmailDataMag" + objectName.Replace("Avatar", "") + ".sav", FileMode.Open, FileAccess.Read, FileShare.Read);
						emailDataMag = (EmailDataMag)new BinaryFormatter().Deserialize(fileStream);
						fileStream.Close();
						emailDataMag.Init();
					}
					else
					{
						emailDataMag = new EmailDataMag();
						JSONObject jsonObject5 = YSGame.YSSaveGame.GetJsonObject(jsonObject, text + ".newEmailDictionary");
						if (jsonObject5 != null)
						{
							emailDataMag.InitNewJson(jsonObject5);
						}
						JSONObject jsonObject6 = YSGame.YSSaveGame.GetJsonObject(jsonObject, text + ".hasReadEmailDictionary");
						if (jsonObject6 != null)
						{
							emailDataMag.InitHasReadJson(jsonObject6);
						}
						JSONObject jsonObject7 = YSGame.YSSaveGame.GetJsonObject(jsonObject, text + ".cyNpcList");
						if (jsonObject7 != null)
						{
							emailDataMag.cyNpcList = jsonObject7.ToList();
						}
					}
				}
				catch (Exception)
				{
					Debug.LogError((object)"传音符错误,清空修复");
					emailDataMag = new EmailDataMag();
				}
				fields[i].SetValue(avatar, emailDataMag);
				break;
			}
			case "JSONObject":
			{
				JSONObject jsonObject4 = YSGame.YSSaveGame.GetJsonObject(jsonObject, text + "._JSONObject");
				if (jsonObject4 != null)
				{
					fields[i].SetValue(avatar, jsonObject4);
				}
				break;
			}
			case "JObject":
			{
				JSONObject jsonObject2 = YSGame.YSSaveGame.GetJsonObject(jsonObject, text + "._JObject");
				if (jsonObject2 != null)
				{
					JObject value = JObject.Parse(jsonObject2.ToString());
					fields[i].SetValue(avatar, value);
				}
				break;
			}
			}
		}
	}

	public string getSaveID(int id, int index)
	{
		return id + "_" + index;
	}

	public void Save(int id, int index, Avatar _avatar = null)
	{
		saveGame(id, index, _avatar);
	}

	public void playerSaveGame(int id, int index, Avatar _avatar = null)
	{
		if (jsonData.instance.saveState == 1)
		{
			UIPopTip.Inst.Pop("存档未完成,请稍等");
		}
		else
		{
			saveGame(id, index, _avatar);
		}
	}

	public void saveGame(int id, int index, Avatar _avatar = null)
	{
		if (!NpcJieSuanManager.inst.isCanJieSuan)
		{
			UIPopTip.Inst.Pop("正在结算中不能存档");
		}
		else if ((Object)(object)FpUIMag.inst != (Object)null || (Object)(object)TpUIMag.inst != (Object)null || UINPCJiaoHu.Inst.NowIsJiaoHu2 || (Object)(object)SetFaceUI.Inst != (Object)null)
		{
			UIPopTip.Inst.Pop("当前状态不能存档");
		}
		else
		{
			if (jsonData.instance.SaveLock)
			{
				return;
			}
			if (index == 0 && SystemConfig.Inst.GetSaveTimes() != 0)
			{
				if (SystemConfig.Inst.GetSaveTimes() == -1)
				{
					return;
				}
				DateTime now = DateTime.Now;
				if (!(now >= NextSaveTime))
				{
					return;
				}
				NextSaveTime = NextSaveTime.AddMinutes(GetAddTime());
				if (NextSaveTime < now)
				{
					NextSaveTime = now.AddMinutes(GetAddTime());
				}
			}
			startSaveTime = Time.realtimeSinceStartup;
			Avatar avatar = (Avatar)KBEngineApp.app.player();
			if (_avatar != null)
			{
				avatar = _avatar;
			}
			string gamePath = Paths.GetSavePath();
			GameVersion.inst.GetGameVersion();
			_ = JSONObject.arr;
			JSONObject jSONObject = new JSONObject();
			int level = avatar.level;
			jSONObject.SetField("firstName", avatar.firstName);
			jSONObject.SetField("lastName", avatar.lastName);
			jSONObject.SetField("gameTime", avatar.worldTimeMag.nowTime);
			jSONObject.SetField("avatarLevel", avatar.level);
			YSGame.YSSaveGame.save("AvatarInfo" + instance.getSaveID(id, index), jSONObject, gamePath);
			getPlayer().StreamData.FungusSaveMgr.SaveData();
			Save("Avatar" + getSaveID(id, index), avatar, gamePath);
			string AvatarBackpackJsonDataClone = jsonData.instance.AvatarBackpackJsonData.ToString();
			string AvatarRandomJsonDataClone = jsonData.instance.AvatarRandomJsonData.ToString();
			string AvatarJsonDateClone = jsonData.instance.AvatarJsonData.ToString();
			string deathNPCJsonDate = NpcJieSuanManager.inst.npcDeath.npcDeathJson.ToString();
			string npcOnlyChenghHao = NpcJieSuanManager.inst.npcChengHao.npcOnlyChengHao.ToString();
			JSONObject JieSuanData = new JSONObject();
			JieSuanData.SetField("JieSuanTimes", NpcJieSuanManager.inst.JieSuanTimes);
			JieSuanData.SetField("JieSuanTime", NpcJieSuanManager.inst.JieSuanTime);
			Dictionary<int, List<int>> npcBigMapDictionary = new Dictionary<int, List<int>>(NpcJieSuanManager.inst.npcMap.bigMapNPCDictionary);
			Dictionary<string, List<int>> npcThreeSenceDictionary = new Dictionary<string, List<int>>(NpcJieSuanManager.inst.npcMap.threeSenceNPCDictionary);
			Dictionary<string, Dictionary<int, List<int>>> npcFuBenDictionary = new Dictionary<string, Dictionary<int, List<int>>>(NpcJieSuanManager.inst.npcMap.fuBenNPCDictionary);
			getPlayer().StreamData.NpcJieSuanData.SaveData();
			avatar.Save(id, index);
			jsonData.instance.saveState = 1;
			UIPopTip.Inst.Pop("开始存档,请等待存档完成");
			Loom.RunAsync(delegate
			{
				jsonData.instance.SaveLock = true;
				YSGame.YSSaveGame.save("GameVersion" + instance.getSaveID(id, index), GameVersion.inst.GetGameVersion(), gamePath);
				YSGame.YSSaveGame.save("AvatarBackpackJsonData" + instance.getSaveID(id, index), AvatarBackpackJsonDataClone, gamePath);
				YSGame.YSSaveGame.save("AvatarRandomJsonData" + instance.getSaveID(id, index), AvatarRandomJsonDataClone, gamePath);
				YSGame.YSSaveGame.save("NpcBackpack" + instance.getSaveID(id, index), FactoryManager.inst.loadPlayerDateFactory.SavePackData(AvatarBackpackJsonDataClone), gamePath);
				YSGame.YSSaveGame.save("NpcJsonData" + instance.getSaveID(id, index), FactoryManager.inst.loadPlayerDateFactory.SaveNpcData(AvatarJsonDateClone), gamePath);
				YSGame.YSSaveGame.save("DeathNpcJsonData" + instance.getSaveID(id, index), deathNPCJsonDate, gamePath);
				YSGame.YSSaveGame.save("OnlyChengHao" + instance.getSaveID(id, index), npcOnlyChenghHao, gamePath);
				YSGame.YSSaveGame.save("SaveAvatar" + getSaveID(id, index), 1, gamePath);
				YSGame.YSSaveGame.save("AvatarSavetime" + instance.getSaveID(id, index), DateTime.Now.ToString(), gamePath);
				YSGame.YSSaveGame.save("JieSuanData" + instance.getSaveID(id, index), FactoryManager.inst.loadPlayerDateFactory.SaveJieSuanData(npcBigMapDictionary, npcThreeSenceDictionary, npcFuBenDictionary, JieSuanData), gamePath);
				YSGame.YSSaveGame.save("TuJianSave", TuJianManager.Inst.TuJianSave, gamePath);
				YSGame.YSSaveGame.save("IsComplete" + instance.getSaveID(id, index), "true", gamePath);
				jsonData.instance.SaveLock = false;
				jsonData.instance.saveState = 0;
				Loom.QueueOnMainThread(delegate
				{
					if (YSGame.YSSaveGame.HasFile(Paths.GetSavePath(), "MaxLevelJson"))
					{
						int @int = YSGame.YSSaveGame.GetInt("MaxLevelJson");
						if (level > @int)
						{
							YSGame.YSSaveGame.save("MaxLevelJson", level, gamePath);
						}
					}
					if ((Object)(object)SaveManager.inst != (Object)null)
					{
						SaveManager.inst.updateState();
					}
					jsonData.instance.saveState = -1;
					UIPopTip.Inst.Pop("存档完成");
					Debug.Log((object)$"老存档系统共计耗时{Time.realtimeSinceStartup - startSaveTime}秒");
				}, null);
			});
		}
	}

	public static List<T> Clone<T>(object List)
	{
		using Stream stream = new System.IO.MemoryStream();
		BinaryFormatter binaryFormatter = new BinaryFormatter();
		((IFormatter)binaryFormatter).Serialize(stream, List);
		stream.Seek(0L, SeekOrigin.Begin);
		return ((IFormatter)binaryFormatter).Deserialize(stream) as List<T>;
	}

	public bool canClick(bool show = false, bool useCache = true)
	{
		if (!show && useCache && CanClickManager.Inst.IsFinshed)
		{
			return CanClickManager.Inst.Result;
		}
		CanClickManager.Inst.Result = false;
		CanClickManager.Inst.IsFinshed = true;
		CanClickManager.Inst.RefreshCanClick(show);
		return CanClickManager.Inst.Result;
	}

	public void playFader(string content, UnityAction action = null)
	{
		UI_Manager.inst.PlayeJieSuanAnimation(content, action);
	}

	private void playFaderOut()
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		CameraManager cameraManager = FungusManager.Instance.CameraManager;
		bool waitUntilFinished = true;
		cameraManager.ScreenFadeTexture = CameraManager.CreateColorTexture(new Color(0f, 0f, 0f), 32, 32);
		cameraManager.Fade(0f, 1f, delegate
		{
			_ = waitUntilFinished;
		});
	}

	public static string getScreenName()
	{
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		if (RandomFuBen.IsInRandomFuBen)
		{
			return (string)instance.getPlayer().RandomFuBenList[RandomFuBen.NowRanDomFuBenID.ToString()][(object)"UUID"];
		}
		Scene activeScene = SceneManager.GetActiveScene();
		return ((Scene)(ref activeScene)).name;
	}

	public static Vector3 ToScenece1080(Vector3 pos)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		float num = 1920f / (float)Screen.width;
		float num2 = 1080f / (float)Screen.height;
		return new Vector3(pos.x * num - 960f, pos.y * num2 - 540f, pos.z);
	}

	public static void Say(string text, int hero)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		GameObject val = new GameObject();
		val.AddComponent<Flowchart>();
		val.AddComponent<Say>();
		Say component = val.GetComponent<Say>();
		component.SetStandardText(text);
		component.pubAvatarIDSetType = StartFight.MonstarType.Normal;
		component.pubAvatarIntID = hero;
		component.OnEnter();
	}

	public static string getMonstarTitle(int monstarID)
	{
		string text = "";
		Avatar player = instance.getPlayer();
		if (monstarID == 1 && player.menPai != 0)
		{
			string str = getStr("menpai" + player.menPai);
			string str2 = jsonData.instance.ChengHaoJsonData[player.chengHao.ToString()]["Name"].Str;
			text = str + str2;
		}
		else if (jsonData.instance.AvatarJsonData.HasField(monstarID.ToString()))
		{
			text = jsonData.instance.AvatarJsonData[monstarID.ToString()]["Title"].Str;
		}
		else
		{
			text = "";
			Debug.LogError((object)$"获取敌人称号时出错，jsonData.instance.AvatarJsonData中没有id {monstarID}");
		}
		return text;
	}

	public static string GetPlayerTitle()
	{
		Avatar player = instance.getPlayer();
		string str = getStr("menpai" + player.menPai);
		int chengHao = player.chengHao;
		string text = instance.Code64ToString(jsonData.instance.ChengHaoJsonData[chengHao.ToString()]["Name"].str);
		return str + text;
	}

	public static string GetPlayerName()
	{
		Avatar player = instance.getPlayer();
		return player.firstName + player.lastName;
	}

	public static int getRandomInt(int start, int end)
	{
		int num = jsonData.GetRandom() % (end - start + 1);
		return start + num;
	}

	public static bool HasItems(JToken json, int item)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		foreach (JToken item2 in (JArray)json)
		{
			if ((int)item2 == item)
			{
				return true;
			}
		}
		return false;
	}

	public static int getRandomList(List<int> list)
	{
		int num = 0;
		foreach (int item in list)
		{
			num += item;
		}
		int num2 = jsonData.GetRandom() % num;
		int num3 = 0;
		int num4 = 0;
		foreach (int item2 in list)
		{
			num4 += item2;
			if (num4 > num2)
			{
				return num3;
			}
			num3++;
		}
		return 0;
	}

	public int GetRandomInt(int min, int max)
	{
		return random.Next(min, max + 1);
	}

	public static List<int> getNumRandomList(List<int> list, int num)
	{
		List<int> list2 = new List<int>();
		if (num > list.Count)
		{
			Debug.LogError((object)"随机数超出数量");
			return null;
		}
		for (int i = 0; i < num; i++)
		{
			int item = jsonData.GetRandom() % list.Count;
			if (!list2.Contains(item))
			{
				list2.Add(item);
			}
			else
			{
				i--;
			}
		}
		return list2;
	}

	public static JToken RandomGetArrayToken(JToken list)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		if (((JContainer)(JArray)list).Count > 0)
		{
			return list[(object)(jsonData.GetRandom() % ((JContainer)(JArray)list).Count)];
		}
		return null;
	}

	public static T RandomGetToken<T>(List<T> list)
	{
		return list[jsonData.GetRandom() % list.Count];
	}

	public static JToken FindJTokens(JToken list, FindTokenMethod unityAction)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		foreach (KeyValuePair<string, JToken> item in (JObject)list)
		{
			if (unityAction(item.Value))
			{
				return item.Value;
			}
		}
		return null;
	}

	public static List<JToken> FindAllJTokens(JToken list, FindTokenMethod unityAction)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		List<JToken> list2 = new List<JToken>();
		foreach (KeyValuePair<string, JToken> item in (JObject)list)
		{
			if (unityAction(item.Value))
			{
				list2.Add(item.Value);
			}
		}
		return list2;
	}

	public static bool ContensInt(JArray jArray, int item)
	{
		foreach (JToken item2 in jArray)
		{
			if ((int)item2 == item)
			{
				return true;
			}
		}
		return false;
	}

	public static int GetRandomByJToken(JToken list)
	{
		int num = 0;
		foreach (JToken item in (IEnumerable<JToken>)list)
		{
			num += (int)item;
		}
		if (num == 0)
		{
			return -1;
		}
		int num2 = jsonData.GetRandom() % num;
		int num3 = 0;
		foreach (JToken item2 in (IEnumerable<JToken>)list)
		{
			if ((int)item2 > num2)
			{
				return num3;
			}
			num2 -= (int)item2;
			num3++;
		}
		return -1;
	}

	public JSONObject getRandomListByPercent(List<JSONObject> list, string percent)
	{
		int num = 0;
		foreach (JSONObject item in list)
		{
			num += (int)item[percent].n;
		}
		if (num == 0)
		{
			return null;
		}
		int num2 = jsonData.GetRandom() % num;
		foreach (JSONObject item2 in list)
		{
			if ((int)item2[percent].n > num2)
			{
				return item2;
			}
			num2 -= (int)item2[percent].n;
		}
		return null;
	}

	public JToken getRandomListByPercent(List<JToken> list, string percent)
	{
		int num = 0;
		foreach (JToken item in list)
		{
			num += (int)item[(object)percent];
		}
		if (num == 0)
		{
			return null;
		}
		int num2 = jsonData.instance.QuikeGetRandom() % num;
		foreach (JToken item2 in list)
		{
			if ((int)item2[(object)percent] > num2)
			{
				return item2;
			}
			num2 -= (int)item2[(object)percent];
		}
		return null;
	}

	public static JSONObject getRandomList(List<JSONObject> list)
	{
		int count = list.Count;
		if (count <= 0)
		{
			Debug.LogError((object)"获取随机值出错");
			return null;
		}
		int index = jsonData.GetRandom() % count;
		return list[index];
	}

	public static List<int> JsonListToList(JSONObject json)
	{
		List<int> list = new List<int>();
		foreach (JSONObject item in json.list)
		{
			list.Add((int)item.n);
		}
		return list;
	}

	public static List<JSONObject> GetStaticNumJsonobj(JSONObject json, string name, int Num)
	{
		List<JSONObject> list = new List<JSONObject>();
		foreach (JSONObject item in json.list)
		{
			if ((int)item[name].n == Num)
			{
				list.Add(item);
			}
		}
		return list;
	}

	public bool IsInTime(string _NowTime, string _startTime, string _endTime)
	{
		return IsInTime(DateTime.Parse(_NowTime), DateTime.Parse(_startTime), DateTime.Parse(_endTime));
	}

	public bool IsInTime(DateTime _NowTime, DateTime _startTime, DateTime _endTime, int circulation = 0)
	{
		DateTime dateTime = _startTime;
		DateTime dateTime2 = _NowTime;
		if (circulation > 0 && dateTime.Year < dateTime2.Year && dateTime2.Year % circulation == dateTime.Year % circulation)
		{
			dateTime2 = new DateTime(dateTime.Year, dateTime2.Month, (dateTime2.Month == 2 && dateTime2.Day == 29) ? 28 : dateTime2.Day);
		}
		if (dateTime <= dateTime2 && dateTime2 <= _endTime)
		{
			return true;
		}
		return false;
	}

	public static void dictionaryAddNum(Dictionary<int, int> zhuyaoList, int key, int num)
	{
		if (zhuyaoList.ContainsKey(key))
		{
			zhuyaoList[key] += num;
		}
		else
		{
			zhuyaoList[key] = num;
		}
	}

	public static int getJsonobject(JSONObject aa, string name)
	{
		if (!aa.HasField(name))
		{
			return 0;
		}
		return (int)aa[name].n;
	}

	public static string getUUID()
	{
		Guid guid = default(Guid);
		return Guid.NewGuid().ToString("N");
	}

	public static JSONObject CreateItemSeid(int itemID)
	{
		JSONObject jSONObject = new JSONObject(JSONObject.Type.OBJECT);
		if (_ItemJsonData.DataDict.ContainsKey(itemID))
		{
			_ItemJsonData itemJsonData = _ItemJsonData.DataDict[itemID];
			if (itemJsonData.type == 9)
			{
				jSONObject.AddField("NaiJiu", 100);
			}
			if (itemJsonData.type == 14)
			{
				JToken val = jsonData.instance.LingZhouPinJie[itemJsonData.quality.ToString()];
				jSONObject.AddField("NaiJiu", (int)val[(object)"Naijiu"]);
			}
		}
		else
		{
			Debug.LogError((object)$"物品表中没有ID为{itemID}的物品");
		}
		return jSONObject;
	}

	public void nextQueen(float time)
	{
		((MonoBehaviour)this).Invoke("continueQueen", time);
	}

	public static void AddQueue(UnityAction cell)
	{
		Queue<UnityAction> queue = new Queue<UnityAction>();
		queue.Enqueue(cell);
		YSFuncList.Ints.AddFunc(queue);
	}

	public void ToolsStartCoroutine(IEnumerator Temp)
	{
		((MonoBehaviour)this).StartCoroutine(Temp);
	}

	private void continueQueen()
	{
		YSFuncList.Ints.Continue();
	}

	public void showFightUIPlan(string text, int itemID, int ItemNum)
	{
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		canClickFlag = false;
		Object obj = Resources.Load("uiPrefab/FightUIRoot");
		GameObject val = Object.Instantiate<GameObject>((GameObject)(object)((obj is GameObject) ? obj : null));
		val.transform.parent = GameObject.Find("UI Root (2D)").transform;
		val.transform.localScale = Vector3.one * 0.75f;
		val.transform.localPosition = Vector3.zero;
		FightUIRoot tempFightUIRoot = val.GetComponent<FightUIRoot>();
		tempFightUIRoot.setTitle("事件");
		tempFightUIRoot.label.text = text;
		tempFightUIRoot.confimBtn.onClick.Add(new EventDelegate(delegate
		{
			UltimateSurvival.MonoSingleton<UI_Backgroud>.Instance.Value = false;
			((Component)tempFightUIRoot).gameObject.SetActive(false);
			canClickFlag = true;
		}));
		JSONObject addItemList = new JSONObject(JSONObject.Type.ARRAY);
		tempFightUIRoot.addTempItem(ref addItemList, itemID, ItemNum);
		tempFightUIRoot.AvatarAddItem(addItemList);
	}

	public static string getCaiJiText(string desc, int id, int num, int addNum, int addTime)
	{
		return jsonData.instance.AllMapCaiJiMiaoShuBiao["1"][desc].Str.Replace("{ItemName}", jsonData.instance.ItemJsonData[id.ToString()]["name"].Str).Replace("{ItemNum}", num.ToString()).Replace("{AddNum}", addNum.ToString())
			.Replace("{AddTime}", addTime.ToString());
	}

	public JSONObject getWuJiangBangDing(int AvatarID)
	{
		foreach (JSONObject item in jsonData.instance.WuJiangBangDing.list.FindAll((JSONObject _json) => _json["avatar"].HasItem(AvatarID) ? true : false))
		{
			if (IsInTime(item["TimeStart"].str, item["TimeEnd"].str))
			{
				return item;
			}
		}
		return null;
	}

	public bool IsInTime(string StatrTime, string EndTime)
	{
		Avatar player = getPlayer();
		DateTime dateTime = DateTime.Parse(StatrTime);
		DateTime dateTime2 = DateTime.Parse(EndTime);
		DateTime nowTime = player.worldTimeMag.getNowTime();
		if (dateTime <= nowTime && nowTime <= dateTime2)
		{
			return true;
		}
		return false;
	}

	public static bool IsInNum(int num, int startNum, int EndNum)
	{
		if (num >= startNum)
		{
			return num <= EndNum;
		}
		return false;
	}

	public static DateTime GetEndTime(string startTime, int Day = 0, int Month = 0, int year = 0)
	{
		return DateTime.Parse(startTime).AddDays(Day).AddMonths(Month)
			.AddYears(year);
	}

	public static DateTime getShengYuShiJian(string nowTime, string endTime)
	{
		return getShengYuShiJian(DateTime.Parse(nowTime), DateTime.Parse(endTime));
	}

	public static DateTime getShengYuShiJian(DateTime nowTime, DateTime endTime)
	{
		if (endTime > nowTime)
		{
			TimeSpan timeSpan = endTime - nowTime;
			return new DateTime(1, 1, 1).AddDays(timeSpan.Days);
		}
		Debug.Log((object)$"获取剩余时间出现endTime>nowTime，endTime:{endTime}，nowTime：{nowTime}");
		return DateTime.MinValue;
	}

	public static string TimeToShengYuTime(DateTime check, string Title = "剩余时间")
	{
		return (Title + "{X}").Replace("{X}", check.Year - 1 + "年" + (check.Month - 1) + "月" + (check.Day - 1) + "日");
	}

	public static GameObject InstantiateGameObject(GameObject temp, Transform parent)
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		GameObject obj = Object.Instantiate<GameObject>(temp);
		obj.transform.SetParent(parent);
		obj.SetActive(true);
		obj.transform.localScale = Vector3.one;
		return obj;
	}

	public static string getLiDanLeiXinStr(int leixing)
	{
		if (leixing <= 0)
		{
			return "无";
		}
		return LianDanItemLeiXin.DataDict[leixing].name;
	}

	public static GameObject showSkillChoice()
	{
		Object obj = Resources.Load("uiPrefab/Fight/SkillChoic");
		return Object.Instantiate<GameObject>((GameObject)(object)((obj is GameObject) ? obj : null));
	}

	public static bool symbol(string type, int statr, int end)
	{
		switch (type)
		{
		case ">":
			if (statr > end)
			{
				return true;
			}
			break;
		case "<":
			if (statr < end)
			{
				return true;
			}
			break;
		case "=":
			if (statr == end)
			{
				return true;
			}
			break;
		}
		return false;
	}

	public static Dictionary<int, int> GetXiangSheng()
	{
		return new Dictionary<int, int>
		{
			[0] = 2,
			[1] = 3,
			[2] = 1,
			[3] = 4,
			[4] = 0
		};
	}

	public static Dictionary<int, int> GetXiangKe()
	{
		return new Dictionary<int, int>
		{
			[0] = 1,
			[1] = 4,
			[2] = 3,
			[3] = 0,
			[4] = 2
		};
	}

	public bool CheckBadWord(string input)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Invalid comparison between Unknown and I4
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Invalid comparison between Unknown and I4
		if ((int)Application.platform != 11 && (int)Application.platform != 8)
		{
			return true;
		}
		foreach (KeyValuePair<string, JToken> item in jsonData.instance.BadWord)
		{
			string value = (string)item.Value[(object)"name"];
			if (input.Contains(value))
			{
				return false;
			}
		}
		return true;
	}

	public int GetAddTime()
	{
		return SystemConfig.Inst.GetSaveTimes();
	}
}
