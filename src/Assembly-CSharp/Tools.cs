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
using PaiMai;
using QiYu;
using script.NewLianDan;
using script.Submit;
using Tab;
using UltimateSurvival;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using YSGame;
using YSGame.TuJian;

// Token: 0x020002DE RID: 734
public class Tools : MonoBehaviour
{
	// Token: 0x17000271 RID: 625
	// (get) Token: 0x0600161C RID: 5660 RVA: 0x00013C1C File Offset: 0x00011E1C
	// (set) Token: 0x0600161B RID: 5659 RVA: 0x00013C13 File Offset: 0x00011E13
	public bool isNeedSetTalk
	{
		get
		{
			return this._isNeedSetTalk;
		}
		set
		{
			this._isNeedSetTalk = value;
		}
	}

	// Token: 0x0600161D RID: 5661 RVA: 0x00013C24 File Offset: 0x00011E24
	private void Awake()
	{
		Tools.instance = this;
		this.random = new Random();
		this.monstarMag = new MonstarMag();
	}

	// Token: 0x0600161E RID: 5662 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x0600161F RID: 5663 RVA: 0x00013C42 File Offset: 0x00011E42
	public void SetCaiYaoData(CaiYao.ItemData data)
	{
		this.CaiYaoData = new CaiYao.ItemData(data.ItemId, data.ItemNum, data.AddNum, data.AddTime, data.HasEnemy, data.FirstEnemyId, data.ScondEnemyId);
	}

	// Token: 0x06001620 RID: 5664 RVA: 0x000C68E4 File Offset: 0x000C4AE4
	public void startNomalFight(int monstarID)
	{
		Tools.instance.getPlayer();
		((StartFight)Object.Instantiate<GameObject>(Resources.Load("talkPrefab/OptionPrefab/OptionFight") as GameObject).GetComponentInChildren<Flowchart>().FindBlock("Splash").CommandList[0]).MonstarID = monstarID;
	}

	// Token: 0x06001621 RID: 5665 RVA: 0x000C6938 File Offset: 0x000C4B38
	public void startFight(int monstarID)
	{
		try
		{
			if (FpUIMag.inst == null)
			{
				this.MonstarID = monstarID;
				Object.Instantiate<GameObject>(ResManager.inst.LoadPrefab("FpPanel"));
				FpUIMag.inst.Init();
			}
		}
		catch (Exception ex)
		{
			if (FpUIMag.inst != null)
			{
				FpUIMag.inst.Close();
			}
			Debug.LogError(ex);
			Debug.LogError("错误NPCId：" + monstarID);
			UIPopTip.Inst.Pop("获取NPC错误，请将存档发送给策划", PopTipIconType.叹号);
		}
	}

	// Token: 0x06001622 RID: 5666 RVA: 0x000C69D0 File Offset: 0x000C4BD0
	public static void ClearObj(Transform obj)
	{
		foreach (object obj2 in obj.parent)
		{
			Transform transform = (Transform)obj2;
			if (transform.gameObject.activeSelf)
			{
				Object.Destroy(transform.gameObject);
			}
		}
	}

	// Token: 0x06001623 RID: 5667 RVA: 0x000C6A3C File Offset: 0x000C4C3C
	public static void ClearChild(Transform obj)
	{
		foreach (object obj2 in obj)
		{
			Transform transform = (Transform)obj2;
			if (transform.gameObject.activeSelf)
			{
				Object.Destroy(transform.gameObject);
			}
		}
	}

	// Token: 0x06001624 RID: 5668 RVA: 0x000C6AA4 File Offset: 0x000C4CA4
	public static string setColorByID(string name, int id)
	{
		int num;
		try
		{
			num = jsonData.instance.ItemJsonData[id.ToString()]["quality"].I;
		}
		catch (Exception)
		{
			Debug.LogError(string.Format("该ID不存在品阶,ID:{0}", id));
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

	// Token: 0x06001625 RID: 5669 RVA: 0x000C6B9C File Offset: 0x000C4D9C
	public static JSONObject getSatticSkillItem(int skillId)
	{
		foreach (KeyValuePair<string, JSONObject> keyValuePair in jsonData.instance.ItemJsonData)
		{
			if (keyValuePair.Value["type"].I == 4)
			{
				float num = 0f;
				if (float.TryParse(keyValuePair.Value["desc"].str, out num) && (int)num == skillId)
				{
					return keyValuePair.Value;
				}
			}
		}
		return null;
	}

	// Token: 0x06001626 RID: 5670 RVA: 0x000C6C3C File Offset: 0x000C4E3C
	public static int GetStaticSkillBookItemIDByStaticSkillID(int staticSkillID)
	{
		foreach (_ItemJsonData itemJsonData in _ItemJsonData.DataList)
		{
			if (itemJsonData.type == 4)
			{
				float num = 0f;
				if (float.TryParse(itemJsonData.desc, out num) && (int)num == staticSkillID)
				{
					return itemJsonData.id;
				}
			}
		}
		return -1;
	}

	// Token: 0x06001627 RID: 5671 RVA: 0x00013C79 File Offset: 0x00011E79
	public void StartRemoveSeaMonstarFight(string MonstarUUID)
	{
		this.SeaRemoveMonstarUUID = MonstarUUID;
		this.SeaRemoveMonstarFlag = true;
	}

	// Token: 0x06001628 RID: 5672 RVA: 0x00013C89 File Offset: 0x00011E89
	public void AutoSetSeaMonstartDie()
	{
		if (this.SeaRemoveMonstarFlag)
		{
			this.SeaRemoveMonstarFlag = false;
			Tools.instance.getPlayer().seaNodeMag.RemoveSeaMonstar(this.SeaRemoveMonstarUUID);
		}
	}

	// Token: 0x06001629 RID: 5673 RVA: 0x000C6CB8 File Offset: 0x000C4EB8
	public void AutoSeatSeaRunAway(bool isFp = false)
	{
		if (!PlayerEx.Player.lastScence.StartsWith("Sea"))
		{
			return;
		}
		if (!this.SeaRemoveMonstarFlag)
		{
			return;
		}
		if (isFp)
		{
			this.FinalScene = SceneManager.GetActiveScene().name;
		}
		Avatar player = this.getPlayer();
		this.SeaRemoveMonstarFlag = false;
		int nowIndex = player.fubenContorl[this.FinalScene].NowIndex;
		int num = 0;
		List<int> list = new List<int>();
		if (int.TryParse(this.FinalScene.Replace("Sea", ""), out num))
		{
			foreach (int num2 in EndlessSeaMag.GetAroundIndexList(nowIndex, 1, true))
			{
				if (num2 != nowIndex)
				{
					int inSeaID = player.seaNodeMag.GetInSeaID(num2, EndlessSeaMag.MapWide);
					if (Tools.ContensInt((JArray)jsonData.instance.EndlessSeaHaiYuData[num.ToString()]["shuxing"], inSeaID))
					{
						list.Add(num2);
					}
				}
			}
			if (list.Count > 0)
			{
				player.fubenContorl[this.FinalScene].NowIndex = list[jsonData.GetRandom() % list.Count];
			}
		}
	}

	// Token: 0x0600162A RID: 5674 RVA: 0x000C6E10 File Offset: 0x000C5010
	public static int CalcLingWuTime(int bookItemID)
	{
		if (!_ItemJsonData.DataDict.ContainsKey(bookItemID))
		{
			Debug.LogError(string.Format("计算领悟时间出错，没有ID为{0}的书籍", bookItemID));
			return 10000;
		}
		_ItemJsonData itemJsonData = _ItemJsonData.DataDict[bookItemID];
		return Tools.CalcLingWuOrTuPoTime(itemJsonData.StuTime, itemJsonData.wuDao);
	}

	// Token: 0x0600162B RID: 5675 RVA: 0x000C6E64 File Offset: 0x000C5064
	public static int CalcTuPoTime(int staticSkillID)
	{
		StaticSkillJsonData staticSkillJsonData = StaticSkillJsonData.DataDict[staticSkillID + 1];
		int staticSkillBookItemIDByStaticSkillID = Tools.GetStaticSkillBookItemIDByStaticSkillID(staticSkillJsonData.Skill_ID);
		if (!_ItemJsonData.DataDict.ContainsKey(staticSkillBookItemIDByStaticSkillID))
		{
			Debug.LogError(string.Format("计算突破功法时间出错，没有ID为{0}的书籍", staticSkillBookItemIDByStaticSkillID));
			return 10000;
		}
		_ItemJsonData itemJsonData = _ItemJsonData.DataDict[staticSkillBookItemIDByStaticSkillID];
		return Tools.CalcLingWuOrTuPoTime(staticSkillJsonData.Skill_castTime, itemJsonData.wuDao);
	}

	// Token: 0x0600162C RID: 5676 RVA: 0x000C6ED0 File Offset: 0x000C50D0
	public static int CalcLingWuOrTuPoTime(int baseTime, List<int> wuDao)
	{
		float num = 0f;
		Avatar player = PlayerEx.Player;
		float num3;
		if (player.wuXin > 100U)
		{
			int num2 = Mathf.Min((int)player.wuXin, 200);
			num3 = 0.5f - (float)(num2 - 100) / 400f;
		}
		else
		{
			num3 = 1f - player.wuXin / 200f;
		}
		if (wuDao.Count > 0)
		{
			int num4 = wuDao.Count / 2;
			for (int i = 0; i < wuDao.Count; i += 2)
			{
				int wuDaoType = wuDao[i];
				int wuDaoLevelByType = player.wuDaoMag.getWuDaoLevelByType(wuDaoType);
				float num5 = jsonData.instance.WuDaoJinJieJson[wuDaoLevelByType.ToString()]["JiaCheng"].n;
				num5 /= (float)num4;
				num += num5;
			}
		}
		num = 1f - num;
		num = Mathf.Clamp(num, 0f, 1f);
		return (int)((float)baseTime * num3 * num);
	}

	// Token: 0x0600162D RID: 5677 RVA: 0x00013CB4 File Offset: 0x00011EB4
	public static int DayToYear(int day)
	{
		return day / 365;
	}

	// Token: 0x0600162E RID: 5678 RVA: 0x00013CBD File Offset: 0x00011EBD
	public static int DayToMonth(int day)
	{
		return (day - 365 * Tools.DayToYear(day)) / 30;
	}

	// Token: 0x0600162F RID: 5679 RVA: 0x00013CD0 File Offset: 0x00011ED0
	public static int DayToDay(int day)
	{
		return day - 365 * Tools.DayToYear(day) - 30 * Tools.DayToMonth(day);
	}

	// Token: 0x06001630 RID: 5680 RVA: 0x00013CEA File Offset: 0x00011EEA
	public static string getStr(string str)
	{
		if (StrTextJsonData.DataDict.ContainsKey(str))
		{
			return StrTextJsonData.DataDict[str].ChinaText;
		}
		Debug.LogError("JSONClass.StrTextJsonData.DataDict[" + str + "]不存在");
		return "None";
	}

	// Token: 0x06001631 RID: 5681 RVA: 0x00013D24 File Offset: 0x00011F24
	public Avatar getPlayer()
	{
		return (Avatar)KBEngineApp.app.player();
	}

	// Token: 0x06001632 RID: 5682 RVA: 0x000C6FC4 File Offset: 0x000C51C4
	public bool CheckHasTianFu(int id)
	{
		return this.getPlayer().SelectTianFuID.list.Find((JSONObject aa) => (int)aa.n == id) != null;
	}

	// Token: 0x06001633 RID: 5683 RVA: 0x00013D35 File Offset: 0x00011F35
	public bool CheckHasTianFuSeid(int seid)
	{
		return this.getPlayer().TianFuID.HasField(seid.ToString());
	}

	// Token: 0x06001634 RID: 5684 RVA: 0x000C7004 File Offset: 0x000C5204
	public void ResetEquipSeid()
	{
		Avatar player = Tools.instance.getPlayer();
		player.EquipSeidFlag = new Dictionary<int, Dictionary<int, int>>();
		Dictionary<int, BaseItem> curEquipDict = player.StreamData.FangAnData.GetCurEquipDict();
		foreach (int key in curEquipDict.Keys)
		{
			if (curEquipDict[key].Seid == null && curEquipDict[key].Seid.HasField("ItemSeids"))
			{
				using (List<JSONObject>.Enumerator enumerator2 = curEquipDict[key].Seid["ItemSeids"].list.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						JSONObject itemAddSeid = enumerator2.Current;
						new Equips(curEquipDict[key].Id, 0, 5)
						{
							ItemAddSeid = itemAddSeid
						}.Puting(player, player, 2);
					}
					continue;
				}
			}
			new Equips(curEquipDict[key].Id, 0, 5).Puting(player, player, 2);
		}
		player.nowConfigEquipItem = player.StreamData.FangAnData.CurEquipIndex;
		player.equipItemList.values = player.StreamData.FangAnData.CurEquipDictToOldList();
	}

	// Token: 0x06001635 RID: 5685 RVA: 0x000C7168 File Offset: 0x000C5368
	public void NewAddItem(int id, int count, JSONObject seid, string uuid = "无", bool ShowText = false)
	{
		Avatar player = this.getPlayer();
		int type = _ItemJsonData.DataDict[id].type;
		if (type <= 2 || type == 14)
		{
			if (uuid == "无")
			{
				uuid = Tools.getUUID();
			}
			if (seid == null)
			{
				seid = Tools.CreateItemSeid(id);
			}
			if (seid != null && seid.HasField("isPaiMai"))
			{
				seid.RemoveField("isPaiMai");
			}
			player.AddEquip(id, uuid, seid);
			return;
		}
		player.addItem(id, count, seid, ShowText);
	}

	// Token: 0x06001636 RID: 5686 RVA: 0x000C71E8 File Offset: 0x000C53E8
	public void RemoveItem(int id, int count = 1)
	{
		Avatar player = this.getPlayer();
		for (int i = count; i > 0; i--)
		{
			if (!this.RemoveEquip(id))
			{
				player.removeItem(id);
			}
		}
		this.ResetEquipSeid();
	}

	// Token: 0x06001637 RID: 5687 RVA: 0x000C7220 File Offset: 0x000C5420
	public void RemoveItem(string uuid, int count = 1)
	{
		Avatar player = this.getPlayer();
		for (int i = count; i > 0; i--)
		{
			this.RemoveEquip(uuid);
			player.removeItem(uuid);
		}
		this.ResetEquipSeid();
	}

	// Token: 0x06001638 RID: 5688 RVA: 0x000C7254 File Offset: 0x000C5454
	private void RemoveEquip(string uuid)
	{
		Dictionary<int, Dictionary<int, BaseItem>> equipDictionary = this.getPlayer().StreamData.FangAnData.EquipDictionary;
		Dictionary<int, List<int>> dictionary = new Dictionary<int, List<int>>();
		foreach (int key in equipDictionary.Keys)
		{
			foreach (int num in equipDictionary[key].Keys)
			{
				if (equipDictionary[key][num].Uid == uuid)
				{
					if (!dictionary.ContainsKey(key))
					{
						dictionary.Add(key, new List<int>
						{
							num
						});
					}
					else
					{
						dictionary[key].Add(num);
					}
				}
			}
		}
		if (dictionary.Count > 0)
		{
			foreach (int key2 in dictionary.Keys)
			{
				foreach (int key3 in dictionary[key2])
				{
					equipDictionary[key2].Remove(key3);
					if (equipDictionary[key2].Count < 1)
					{
						equipDictionary.Remove(key2);
					}
				}
			}
		}
	}

	// Token: 0x06001639 RID: 5689 RVA: 0x000C7400 File Offset: 0x000C5600
	private bool RemoveEquip(int id)
	{
		Dictionary<int, BaseItem> curEquipDict = this.getPlayer().StreamData.FangAnData.GetCurEquipDict();
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

	// Token: 0x0600163A RID: 5690 RVA: 0x00013D53 File Offset: 0x00011F53
	public string Code64ToString(string aa)
	{
		return aa.ToCN();
	}

	// Token: 0x0600163B RID: 5691 RVA: 0x00013D5B File Offset: 0x00011F5B
	public static string Code64(string aa)
	{
		return aa.ToCN();
	}

	// Token: 0x0600163C RID: 5692 RVA: 0x00013D63 File Offset: 0x00011F63
	public static string getDescByID(string desstr, int skillID)
	{
		return Tools.getDesc(desstr, _skillJsonData.DataDict[skillID].HP);
	}

	// Token: 0x0600163D RID: 5693 RVA: 0x000C7480 File Offset: 0x000C5680
	private static void setAttackTxt(ref string desstr, int __attack)
	{
		string text = desstr.Substring(0, desstr.IndexOf("（"));
		string text2 = desstr.Substring(desstr.IndexOf("）"), desstr.Length - desstr.IndexOf("）"));
		string text3 = text2.Substring(1, text2.Length - 1);
		int length = desstr.Length - text3.Length - text.Length - 2;
		string expression = desstr.Substring(desstr.IndexOf("（") + 1, length).Replace("attack", string.Concat(__attack));
		object obj = new DataTable().Compute(expression, "");
		desstr = string.Concat(new object[]
		{
			text,
			"[FF00FF]",
			obj,
			"[-]",
			text3
		});
		if (desstr.IndexOf("attack") > 0)
		{
			Tools.setAttackTxt(ref desstr, __attack);
		}
	}

	// Token: 0x0600163E RID: 5694 RVA: 0x00013D7B File Offset: 0x00011F7B
	public static string getDesc(string desstr, int __attack)
	{
		if (desstr.IndexOf("attack") > 0)
		{
			Tools.setAttackTxt(ref desstr, __attack);
		}
		return desstr;
	}

	// Token: 0x0600163F RID: 5695 RVA: 0x000C7578 File Offset: 0x000C5778
	public string getSkillDesc(int skillID)
	{
		return Tools.getDesc(this.Code64ToString(jsonData.instance.skillJsonData[string.Concat(skillID)]["descr"].str), (int)jsonData.instance.skillJsonData[string.Concat(skillID)]["HP"].n);
	}

	// Token: 0x06001640 RID: 5696 RVA: 0x000C75E4 File Offset: 0x000C57E4
	public string getSkillName(int skillID, bool includecolor = false)
	{
		JSONObject jsonobject = jsonData.instance.skillJsonData[string.Concat(skillID)];
		string text = Tools.instance.Code64ToString(jsonobject["name"].str);
		string str = "";
		string str2 = "";
		return str + text.Replace("1", "").Replace("2", "").Replace("3", "").Replace("4", "").Replace("5", "") + str2;
	}

	// Token: 0x06001641 RID: 5697 RVA: 0x000C7684 File Offset: 0x000C5884
	public string getStaticSkillName(int skillID, bool includecolor = false)
	{
		JSONObject jsonobject = jsonData.instance.StaticSkillJsonData[string.Concat(skillID)];
		string text = Tools.instance.Code64ToString(jsonobject["name"].str);
		string str = "";
		string str2 = "";
		return str + text.Replace("1", "").Replace("2", "").Replace("3", "").Replace("4", "").Replace("5", "") + str2;
	}

	// Token: 0x06001642 RID: 5698 RVA: 0x00013D94 File Offset: 0x00011F94
	public Sprite getLevelSprite(int level, Rect rect)
	{
		return Sprite.Create((Texture2D)Resources.Load("NewUI/Fight/LevelIcon/icon_" + level), rect, new Vector2(0.5f, 0.5f));
	}

	// Token: 0x06001643 RID: 5699 RVA: 0x000C7724 File Offset: 0x000C5924
	public string getSkillText(int skillID)
	{
		JSONObject jsonobject = jsonData.instance.skillJsonData[string.Concat(skillID)];
		string skillDesc = Tools.instance.getSkillDesc(skillID);
		Tools.instance.getSkillName(skillID, false);
		return "[FF0000]说明:[-] " + skillDesc;
	}

	// Token: 0x06001644 RID: 5700 RVA: 0x000C7770 File Offset: 0x000C5970
	public void setAvaterCanAttack(Entity targAvater)
	{
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
			Debug.LogError(ex.ToString());
		}
	}

	// Token: 0x06001645 RID: 5701 RVA: 0x000C77F8 File Offset: 0x000C59F8
	public JSONObject readMGconfig()
	{
		JSONObject result;
		try
		{
			MonoBehaviour.print("Unity2:" + Application.persistentDataPath);
			StreamReader streamReader = new StreamReader(Application.persistentDataPath + "/MGconfig.txt");
			string text = streamReader.ReadToEnd();
			MonoBehaviour.print("Unity3:" + text);
			JSONObject jsonobject = new JSONObject(text, -2, false, false);
			streamReader.Close();
			MonoBehaviour.print("Unity5:");
			result = jsonobject;
		}
		catch (Exception)
		{
			StreamWriter streamWriter = new StreamWriter(Application.persistentDataPath + "/MGconfig.txt", false, Encoding.UTF8);
			streamWriter.Write("1");
			MonoBehaviour.print("Unity4:");
			streamWriter.Close();
			result = new JSONObject(JSONObject.Type.OBJECT);
		}
		return result;
	}

	// Token: 0x06001646 RID: 5702 RVA: 0x000C78B4 File Offset: 0x000C5AB4
	public void saveMGConfig(string encodedString)
	{
		MonoBehaviour.print("Unity99:");
		MonoBehaviour.print("Unity:" + Application.persistentDataPath + encodedString);
		StreamWriter streamWriter = new StreamWriter(Application.persistentDataPath + "/MGconfig.txt", false, Encoding.UTF8);
		streamWriter.Write("123");
		streamWriter.Close();
	}

	// Token: 0x06001647 RID: 5703 RVA: 0x000C790C File Offset: 0x000C5B0C
	public string Encryption(string express)
	{
		string result;
		using (RSACryptoServiceProvider rsacryptoServiceProvider = new RSACryptoServiceProvider(new CspParameters
		{
			KeyContainerName = "oa_erp_dowork_lgmg"
		}))
		{
			byte[] bytes = Encoding.Default.GetBytes(express);
			result = Convert.ToBase64String(rsacryptoServiceProvider.Encrypt(bytes, false));
		}
		return result;
	}

	// Token: 0x06001648 RID: 5704 RVA: 0x000C7968 File Offset: 0x000C5B68
	public string Decrypt(string ciphertext)
	{
		string @string;
		using (RSACryptoServiceProvider rsacryptoServiceProvider = new RSACryptoServiceProvider(new CspParameters
		{
			KeyContainerName = "oa_erp_dowork_lgmg"
		}))
		{
			byte[] rgb = Convert.FromBase64String(ciphertext);
			byte[] bytes = rsacryptoServiceProvider.Decrypt(rgb, false);
			@string = Encoding.Default.GetString(bytes);
		}
		return @string;
	}

	// Token: 0x06001649 RID: 5705 RVA: 0x000C79C4 File Offset: 0x000C5BC4
	public void loadMapScenes(string name, bool LastSceneIsValue = true)
	{
		if (name != "LianDan" && LastSceneIsValue)
		{
			Tools.instance.getPlayer().lastScence = name;
		}
		Tools.jumpToName = name;
		this.loadSceneType = 1;
		SceneManager.LoadScene(name);
		if (PanelMamager.inst.UIBlackMaskGameObject == null)
		{
			Object.Instantiate<GameObject>(ResManager.inst.LoadPrefab("BlackHide"));
		}
		PanelMamager.inst.UIBlackMaskGameObject.SetActive(false);
		PanelMamager.inst.UIBlackMaskGameObject.SetActive(true);
		if (PanelMamager.inst.UISceneGameObject == null)
		{
			SceneManager.LoadScene("UIScene", 1);
		}
		if (PanelMamager.inst.UISceneGameObject != null)
		{
			Transform transform = PanelMamager.inst.UISceneGameObject.transform.Find("ThreeSceneNpcCanvas");
			if (transform != null)
			{
				transform.gameObject.SetActive(true);
			}
		}
		if (UI_Manager.inst != null)
		{
			if (ThreeSceernUIFab.inst != null)
			{
				Object.Destroy(ThreeSceernUIFab.inst.gameObject);
			}
			if (ThreeSceneMagFab.inst != null)
			{
				Object.Destroy(ThreeSceneMagFab.inst.gameObject);
			}
		}
		this.isNeedSetTalk = true;
		this.CanOpenTab = true;
	}

	// Token: 0x0600164A RID: 5706 RVA: 0x00013DC5 File Offset: 0x00011FC5
	public void loadOtherScenes(string name)
	{
		this.loadSceneType = 0;
		this.ohtherSceneName = name;
		this.isNeedSetTalk = true;
		if (SingletonMono<TabUIMag>.Instance != null)
		{
			SingletonMono<TabUIMag>.Instance.TryEscClose();
		}
		SceneManager.LoadScene("NextScene");
		this.CanOpenTab = false;
	}

	// Token: 0x0600164B RID: 5707 RVA: 0x00013E05 File Offset: 0x00012005
	public static void startPaiMai()
	{
		Tools.instance.loadOtherScenes("PaiMai");
	}

	// Token: 0x0600164C RID: 5708 RVA: 0x000C7AFC File Offset: 0x000C5CFC
	public bool isEquip(int id)
	{
		bool result = false;
		if ((int)jsonData.instance.ItemJsonData[string.Concat(id)]["vagueType"].n == 0)
		{
			result = true;
		}
		return result;
	}

	// Token: 0x0600164D RID: 5709 RVA: 0x000042DD File Offset: 0x000024DD
	public void findSkillBy()
	{
	}

	// Token: 0x0600164E RID: 5710 RVA: 0x00013E16 File Offset: 0x00012016
	public int getSkillIDByKey(int key)
	{
		if (key <= 0)
		{
			return -1;
		}
		return _skillJsonData.DataDict[key].Skill_ID;
	}

	// Token: 0x0600164F RID: 5711 RVA: 0x00013E2E File Offset: 0x0001202E
	public int getStaticSkillIDByKey(int key)
	{
		if (key < 0)
		{
			return -1;
		}
		return StaticSkillJsonData.DataDict[key].Skill_ID;
	}

	// Token: 0x06001650 RID: 5712 RVA: 0x000C7B3C File Offset: 0x000C5D3C
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
			foreach (_skillJsonData skillJsonData in list)
			{
				if (skillJsonData.Skill_Lv == levelType)
				{
					return skillJsonData.id;
				}
			}
			return -1;
		}
		return -1;
	}

	// Token: 0x06001651 RID: 5713 RVA: 0x000C7BEC File Offset: 0x000C5DEC
	public int getStaticSkillKeyByID(int ID)
	{
		Avatar avatar = (Avatar)KBEngineApp.app.player();
		foreach (StaticSkillJsonData staticSkillJsonData in StaticSkillJsonData.DataList)
		{
			if (staticSkillJsonData.Skill_ID == ID)
			{
				foreach (SkillItem skillItem in avatar.hasStaticSkillList)
				{
					if (skillItem.level == staticSkillJsonData.Skill_Lv && skillItem.itemId == ID)
					{
						return staticSkillJsonData.id;
					}
				}
			}
		}
		return -1;
	}

	// Token: 0x06001652 RID: 5714 RVA: 0x000C7CB4 File Offset: 0x000C5EB4
	public int getStaticSkillKeyByID(int ID, int level)
	{
		foreach (StaticSkillJsonData staticSkillJsonData in StaticSkillJsonData.DataList)
		{
			if (staticSkillJsonData.Skill_ID == ID && staticSkillJsonData.Skill_Lv == level)
			{
				return staticSkillJsonData.id;
			}
		}
		return -1;
	}

	// Token: 0x06001653 RID: 5715 RVA: 0x000C7D20 File Offset: 0x000C5F20
	public static void Save(string objectName, object o, string gamePath = "-1")
	{
		FieldInfo[] fields = o.GetType().GetFields();
		JSONObject jsonobject = new JSONObject(JSONObject.Type.OBJECT);
		int i = 0;
		while (i < fields.Length)
		{
			string text = objectName + "." + fields[i].Name;
			string name = fields[i].FieldType.Name;
			uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
			if (num <= 1615808600U)
			{
				if (num <= 1108380682U)
				{
					if (num <= 765439473U)
					{
						if (num != 697196164U)
						{
							if (num == 765439473U)
							{
								if (name == "Int16")
								{
									goto IL_36C;
								}
							}
						}
						else if (name == "Int64")
						{
							goto IL_36C;
						}
					}
					else if (num != 815609665U)
					{
						if (num != 851515688U)
						{
							if (num == 1108380682U)
							{
								if (name == "WorldTime")
								{
									WorldTime worldTime = (WorldTime)fields[i].GetValue(o);
									jsonobject.AddField(text + ".nowTime", worldTime.nowTime ?? "");
								}
							}
						}
						else if (name == "ITEM_INFO_LIST")
						{
							int num2 = 0;
							foreach (ITEM_INFO item_INFO in ((ITEM_INFO_LIST)fields[i].GetValue(o)).values)
							{
								jsonobject.AddField(text + ".UUID." + num2, item_INFO.uuid);
								jsonobject.AddField(text + ".id." + num2, item_INFO.itemId);
								jsonobject.AddField(text + ".count." + num2, (int)item_INFO.itemCount);
								jsonobject.AddField(text + ".index." + num2, item_INFO.itemIndex);
								jsonobject.AddField(text + ".Seid." + num2, item_INFO.Seid);
								num2++;
							}
							jsonobject.AddField(text + ".Num", num2);
						}
					}
					else if (name == "uInt")
					{
						goto IL_36C;
					}
				}
				else if (num <= 1323747186U)
				{
					if (num != 1283547685U)
					{
						if (num == 1323747186U)
						{
							if (name == "UInt16")
							{
								goto IL_36C;
							}
						}
					}
					else if (name == "Float")
					{
						jsonobject.AddField(text, (float)fields[i].GetValue(o));
					}
				}
				else if (num != 1324880019U)
				{
					if (num != 1438686222U)
					{
						if (num == 1615808600U)
						{
							if (name == "String")
							{
								jsonobject.AddField(text, fields[i].GetValue(o).ToString());
							}
						}
					}
					else if (name == "List`1[]")
					{
						if (fields[i].Name == "configEquipSkill" || fields[i].Name == "configEquipStaticSkill")
						{
							int num3 = 0;
							foreach (List<SkillItem> list in (List<SkillItem>[])fields[i].GetValue(o))
							{
								int num4 = 0;
								foreach (SkillItem skillItem in list)
								{
									jsonobject.AddField(string.Concat(new object[]
									{
										text,
										num3,
										".UUID.",
										num4
									}), skillItem.uuid);
									jsonobject.AddField(string.Concat(new object[]
									{
										text,
										num3,
										".id.",
										num4
									}), skillItem.itemId);
									jsonobject.AddField(string.Concat(new object[]
									{
										text,
										num3,
										".level.",
										num4
									}), skillItem.level);
									jsonobject.AddField(string.Concat(new object[]
									{
										text,
										num3,
										".index.",
										num4
									}), skillItem.itemIndex);
									jsonobject.AddField(string.Concat(new object[]
									{
										text,
										num3,
										".Seid.",
										num4
									}), skillItem.Seid);
									num4++;
								}
								jsonobject.AddField(text + num3 + ".Num", num4);
								num3++;
							}
						}
					}
				}
				else if (name == "UInt64")
				{
					goto IL_36C;
				}
			}
			else if (num <= 2388225411U)
			{
				if (num <= 1907276658U)
				{
					if (num != 1731900476U)
					{
						if (num == 1907276658U)
						{
							if (name == "JObject")
							{
								JSONObject obj = new JSONObject(((JObject)fields[i].GetValue(o)).ToString(), -2, false, false);
								jsonobject.AddField(text + "._JObject", obj);
							}
						}
					}
					else if (name == "ITEM_INFO_LIST[]")
					{
						int num5 = 0;
						foreach (ITEM_INFO_LIST item_INFO_LIST in (ITEM_INFO_LIST[])fields[i].GetValue(o))
						{
							int num6 = 0;
							foreach (ITEM_INFO item_INFO2 in item_INFO_LIST.values)
							{
								jsonobject.AddField(string.Concat(new object[]
								{
									text,
									num5,
									".UUID.",
									num6
								}), item_INFO2.uuid);
								jsonobject.AddField(string.Concat(new object[]
								{
									text,
									num5,
									".id.",
									num6
								}), item_INFO2.itemId);
								jsonobject.AddField(string.Concat(new object[]
								{
									text,
									num5,
									".count.",
									num6
								}), (int)item_INFO2.itemCount);
								jsonobject.AddField(string.Concat(new object[]
								{
									text,
									num5,
									".index.",
									num6
								}), item_INFO2.itemIndex);
								jsonobject.AddField(string.Concat(new object[]
								{
									text,
									num5,
									".Seid.",
									num6
								}), item_INFO2.Seid);
								num6++;
							}
							jsonobject.AddField(text + num5 + ".Num", num6);
							num5++;
						}
					}
				}
				else if (num != 1926157539U)
				{
					if (num != 1966515832U)
					{
						if (num == 2388225411U)
						{
							if (name == "TaskMag")
							{
								TaskMag taskMag = (TaskMag)fields[i].GetValue(o);
								jsonobject.AddField(text + "._TaskData", taskMag._TaskData);
							}
						}
					}
					else if (name == "JSONObject")
					{
						JSONObject obj2 = (JSONObject)fields[i].GetValue(o);
						jsonobject.AddField(text + "._JSONObject", obj2);
					}
				}
				else if (name == "AvatarStaticValue")
				{
					AvatarStaticValue avatarStaticValue = (AvatarStaticValue)fields[i].GetValue(o);
					for (int k = 0; k < 2500; k++)
					{
						jsonobject.AddField(text + ".Value." + k, avatarStaticValue.Value[k]);
					}
					jsonobject.AddField(text + ".talk", avatarStaticValue.talk[0]);
				}
			}
			else if (num <= 2935746502U)
			{
				if (num != 2711245919U)
				{
					if (num == 2935746502U)
					{
						if (name == "List`1")
						{
							if (fields[i].Name == "hasSkillList" || fields[i].Name == "hasStaticSkillList" || fields[i].Name == "hasJieDanSkillList" || fields[i].Name == "equipSkillList" || fields[i].Name == "equipStaticSkillList")
							{
								int num7 = 0;
								foreach (SkillItem skillItem2 in ((List<SkillItem>)fields[i].GetValue(o)))
								{
									jsonobject.AddField(text + ".UUID." + num7, skillItem2.uuid);
									jsonobject.AddField(text + ".id." + num7, skillItem2.itemId);
									jsonobject.AddField(text + ".level." + num7, skillItem2.level);
									jsonobject.AddField(text + ".index." + num7, skillItem2.itemIndex);
									jsonobject.AddField(text + ".Seid." + num7, skillItem2.Seid);
									num7++;
								}
								jsonobject.AddField(text + ".Num", num7);
							}
							if (fields[i].Name == "bufflist")
							{
								JSONObject jsonobject2 = new JSONObject(JSONObject.Type.ARRAY);
								foreach (List<int> list2 in ((List<List<int>>)fields[i].GetValue(o)))
								{
									JSONObject jsonobject3 = new JSONObject(JSONObject.Type.ARRAY);
									foreach (int val in list2)
									{
										jsonobject3.Add(val);
									}
									jsonobject2.Add(jsonobject3);
								}
								jsonobject.AddField(text + ".bufflist", jsonobject2);
							}
							if (fields[i].Name == "LingGeng")
							{
								int num8 = 0;
								foreach (int val2 in ((List<int>)fields[i].GetValue(o)))
								{
									jsonobject.AddField(text + ".id." + num8, val2);
									num8++;
								}
								jsonobject.AddField(text + ".Num", num8);
							}
						}
					}
				}
				else if (name == "Int32")
				{
					goto IL_36C;
				}
			}
			else if (num != 2940794790U)
			{
				if (num != 3538687084U)
				{
					if (num == 4168357374U)
					{
						if (name == "Int")
						{
							goto IL_36C;
						}
					}
				}
				else if (name == "UInt32")
				{
					goto IL_36C;
				}
			}
			else if (name == "EmailDataMag")
			{
				EmailDataMag graph = (EmailDataMag)fields[i].GetValue(o);
				FileStream fileStream = new FileStream(Paths.GetSavePath() + "/EmailDataMag" + objectName.Replace("Avatar", "") + ".sav", FileMode.Create);
				new BinaryFormatter().Serialize(fileStream, graph);
				fileStream.Close();
			}
			IL_C2B:
			i++;
			continue;
			IL_36C:
			long val3 = Convert.ToInt64(fields[i].GetValue(o));
			jsonobject.AddField(text, val3);
			goto IL_C2B;
		}
		YSGame.YSSaveGame.save(objectName, jsonobject, gamePath);
	}

	// Token: 0x06001654 RID: 5716 RVA: 0x000C8A1C File Offset: 0x000C6C1C
	public static void GetValue<T>(string objectName, Avatar avatar) where T : Avatar, new()
	{
		JSONObject jsonObject = YSGame.YSSaveGame.GetJsonObject(objectName, null);
		FieldInfo[] fields = avatar.GetType().GetFields();
		int i = 0;
		while (i < fields.Length)
		{
			string text = objectName + "." + fields[i].Name;
			string name = fields[i].FieldType.Name;
			uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
			if (num <= 1615808600U)
			{
				if (num <= 1108380682U)
				{
					if (num <= 765439473U)
					{
						if (num != 697196164U)
						{
							if (num == 765439473U)
							{
								if (name == "Int16")
								{
									goto IL_3B4;
								}
							}
						}
						else if (name == "Int64")
						{
							goto IL_38A;
						}
					}
					else if (num != 815609665U)
					{
						if (num != 851515688U)
						{
							if (num == 1108380682U)
							{
								if (name == "WorldTime")
								{
									WorldTime worldTime = new WorldTime();
									worldTime.isLoadDate = true;
									worldTime.nowTime = YSGame.YSSaveGame.GetString(jsonObject, text + ".nowTime", "0016-1-1");
									fields[i].SetValue(avatar, worldTime);
								}
							}
						}
						else if (name == "ITEM_INFO_LIST")
						{
							int @int = YSGame.YSSaveGame.GetInt(jsonObject, text + ".Num", -1);
							ITEM_INFO_LIST item_INFO_LIST = new ITEM_INFO_LIST();
							for (int j = 0; j < @int; j++)
							{
								ITEM_INFO item_INFO = new ITEM_INFO();
								item_INFO.uuid = YSGame.YSSaveGame.GetString(jsonObject, text + ".UUID." + j, "");
								if (item_INFO.uuid == "")
								{
									item_INFO.uuid = Tools.getUUID();
								}
								item_INFO.itemId = YSGame.YSSaveGame.GetInt(jsonObject, text + ".id." + j, 0);
								item_INFO.itemCount = (uint)YSGame.YSSaveGame.GetInt(jsonObject, text + ".count." + j, 0);
								item_INFO.itemIndex = YSGame.YSSaveGame.GetInt(jsonObject, text + ".index." + j, 0);
								JSONObject jsonObject2 = YSGame.YSSaveGame.GetJsonObject(jsonObject, text + ".Seid." + j, null);
								if (jsonObject2 != null)
								{
									item_INFO.Seid = jsonObject2;
								}
								item_INFO_LIST.values.Add(item_INFO);
							}
							fields[i].SetValue(avatar, item_INFO_LIST);
						}
					}
					else if (name == "uInt")
					{
						goto IL_38A;
					}
				}
				else if (num <= 1323747186U)
				{
					if (num != 1283547685U)
					{
						if (num == 1323747186U)
						{
							if (name == "UInt16")
							{
								goto IL_3B4;
							}
						}
					}
					else if (name == "Float")
					{
						fields[i].SetValue(avatar, YSGame.YSSaveGame.GetFloat(jsonObject, text, 0f));
					}
				}
				else if (num != 1324880019U)
				{
					if (num != 1438686222U)
					{
						if (num == 1615808600U)
						{
							if (name == "String")
							{
								if (YSGame.YSSaveGame.GetString(jsonObject, text, "") != "")
								{
									fields[i].SetValue(avatar, YSGame.YSSaveGame.GetString(jsonObject, text, ""));
								}
							}
						}
					}
					else if (name == "List`1[]")
					{
						if (fields[i].Name == "configEquipSkill" || fields[i].Name == "configEquipStaticSkill")
						{
							List<SkillItem>[] array = new List<SkillItem>[]
							{
								new List<SkillItem>(),
								new List<SkillItem>(),
								new List<SkillItem>(),
								new List<SkillItem>(),
								new List<SkillItem>()
							};
							int num2 = 0;
							foreach (List<SkillItem> list in array)
							{
								int int2 = YSGame.YSSaveGame.GetInt(jsonObject, text + num2 + ".Num", -1);
								for (int l = 0; l < int2; l++)
								{
									SkillItem skillItem = new SkillItem();
									skillItem.uuid = YSGame.YSSaveGame.GetString(jsonObject, string.Concat(new object[]
									{
										text,
										num2,
										".UUID.",
										l
									}), "");
									if (skillItem.uuid == "")
									{
										skillItem.uuid = Tools.getUUID();
									}
									skillItem.itemId = YSGame.YSSaveGame.GetInt(jsonObject, string.Concat(new object[]
									{
										text,
										num2,
										".id.",
										l
									}), 0);
									skillItem.level = YSGame.YSSaveGame.GetInt(jsonObject, string.Concat(new object[]
									{
										text,
										num2,
										".level.",
										l
									}), 0);
									skillItem.itemIndex = YSGame.YSSaveGame.GetInt(jsonObject, string.Concat(new object[]
									{
										text,
										num2,
										".index.",
										l
									}), 0);
									JSONObject jsonObject3 = YSGame.YSSaveGame.GetJsonObject(jsonObject, string.Concat(new object[]
									{
										text,
										num2,
										".Seid.",
										l
									}), null);
									if (jsonObject3 != null)
									{
										skillItem.Seid = jsonObject3;
									}
									array[num2].Add(skillItem);
								}
								num2++;
							}
							fields[i].SetValue(avatar, array);
						}
					}
				}
				else if (name == "UInt64")
				{
					goto IL_3B4;
				}
			}
			else if (num <= 2388225411U)
			{
				if (num <= 1907276658U)
				{
					if (num != 1731900476U)
					{
						if (num == 1907276658U)
						{
							if (name == "JObject")
							{
								JSONObject jsonObject4 = YSGame.YSSaveGame.GetJsonObject(jsonObject, text + "._JObject", null);
								if (jsonObject4 != null)
								{
									JObject value = JObject.Parse(jsonObject4.ToString());
									fields[i].SetValue(avatar, value);
								}
							}
						}
					}
					else if (name == "ITEM_INFO_LIST[]")
					{
						ITEM_INFO_LIST[] array3 = new ITEM_INFO_LIST[]
						{
							new ITEM_INFO_LIST(),
							new ITEM_INFO_LIST(),
							new ITEM_INFO_LIST(),
							new ITEM_INFO_LIST(),
							new ITEM_INFO_LIST()
						};
						int num3 = 0;
						foreach (ITEM_INFO_LIST item_INFO_LIST2 in array3)
						{
							int int3 = YSGame.YSSaveGame.GetInt(jsonObject, text + num3 + ".Num", -1);
							for (int m = 0; m < int3; m++)
							{
								ITEM_INFO item_INFO2 = new ITEM_INFO();
								item_INFO2.uuid = YSGame.YSSaveGame.GetString(jsonObject, string.Concat(new object[]
								{
									text,
									num3,
									".UUID.",
									m
								}), "");
								if (item_INFO2.uuid == "")
								{
									item_INFO2.uuid = Tools.getUUID();
								}
								item_INFO2.itemId = YSGame.YSSaveGame.GetInt(jsonObject, string.Concat(new object[]
								{
									text,
									num3,
									".id.",
									m
								}), 0);
								item_INFO2.itemCount = (uint)YSGame.YSSaveGame.GetInt(jsonObject, string.Concat(new object[]
								{
									text,
									num3,
									".count.",
									m
								}), 0);
								item_INFO2.itemIndex = YSGame.YSSaveGame.GetInt(jsonObject, string.Concat(new object[]
								{
									text,
									num3,
									".index.",
									m
								}), 0);
								JSONObject jsonObject5 = YSGame.YSSaveGame.GetJsonObject(jsonObject, string.Concat(new object[]
								{
									text,
									num3,
									".Seid.",
									m
								}), null);
								if (jsonObject5 != null)
								{
									item_INFO2.Seid = jsonObject5;
								}
								item_INFO_LIST2.values.Add(item_INFO2);
							}
							num3++;
						}
						fields[i].SetValue(avatar, array3);
					}
				}
				else if (num != 1926157539U)
				{
					if (num != 1966515832U)
					{
						if (num == 2388225411U)
						{
							if (name == "TaskMag")
							{
								TaskMag taskMag = new TaskMag(avatar);
								JSONObject jsonObject6 = YSGame.YSSaveGame.GetJsonObject(jsonObject, text + "._TaskData", null);
								taskMag._TaskData = jsonObject6;
								fields[i].SetValue(avatar, taskMag);
							}
						}
					}
					else if (name == "JSONObject")
					{
						JSONObject jsonObject7 = YSGame.YSSaveGame.GetJsonObject(jsonObject, text + "._JSONObject", null);
						if (jsonObject7 != null)
						{
							fields[i].SetValue(avatar, jsonObject7);
						}
					}
				}
				else if (name == "AvatarStaticValue")
				{
					AvatarStaticValue avatarStaticValue = new AvatarStaticValue();
					for (int n = 0; n < 2500; n++)
					{
						avatarStaticValue.Value[n] = YSGame.YSSaveGame.GetInt(jsonObject, text + ".Value." + n, 0);
					}
					avatarStaticValue.talk[0] = YSGame.YSSaveGame.GetInt(jsonObject, text + ".talk", 0);
					fields[i].SetValue(avatar, avatarStaticValue);
				}
			}
			else if (num <= 2935746502U)
			{
				if (num != 2711245919U)
				{
					if (num == 2935746502U)
					{
						if (name == "List`1")
						{
							if (fields[i].Name == "hasSkillList" || fields[i].Name == "hasStaticSkillList" || fields[i].Name == "hasJieDanSkillList" || fields[i].Name == "equipSkillList" || fields[i].Name == "equipStaticSkillList")
							{
								int int4 = YSGame.YSSaveGame.GetInt(jsonObject, text + ".Num", -1);
								List<SkillItem> list2 = new List<SkillItem>();
								for (int num4 = 0; num4 < int4; num4++)
								{
									SkillItem skillItem2 = new SkillItem();
									skillItem2.uuid = YSGame.YSSaveGame.GetString(jsonObject, text + ".UUID." + num4, "");
									if (skillItem2.uuid == "")
									{
										skillItem2.uuid = Tools.getUUID();
									}
									skillItem2.itemId = YSGame.YSSaveGame.GetInt(jsonObject, text + ".id." + num4, 0);
									skillItem2.level = YSGame.YSSaveGame.GetInt(jsonObject, text + ".level." + num4, 0);
									skillItem2.itemIndex = YSGame.YSSaveGame.GetInt(jsonObject, text + ".index." + num4, 0);
									JSONObject jsonObject8 = YSGame.YSSaveGame.GetJsonObject(jsonObject, text + ".Seid." + num4, null);
									if (jsonObject8 != null)
									{
										skillItem2.Seid = jsonObject8;
									}
									list2.Add(skillItem2);
								}
								fields[i].SetValue(avatar, list2);
							}
							if (fields[i].Name == "bufflist")
							{
								JSONObject jsonObject9 = YSGame.YSSaveGame.GetJsonObject(jsonObject, text + ".bufflist", null);
								if (jsonObject9 == null || jsonObject9.Count <= 0)
								{
									goto IL_F26;
								}
								List<List<int>> list3 = new List<List<int>>();
								foreach (JSONObject jsonobject in jsonObject9.list)
								{
									List<int> list4 = new List<int>();
									foreach (JSONObject jsonobject2 in jsonobject.list)
									{
										list4.Add(jsonobject2.I);
									}
									list3.Add(list4);
								}
								fields[i].SetValue(avatar, list3);
							}
							if (fields[i].Name == "LingGeng")
							{
								int int5 = YSGame.YSSaveGame.GetInt(jsonObject, text + ".Num", 5);
								List<int> list5 = new List<int>();
								for (int num5 = 0; num5 < int5; num5++)
								{
									list5.Add(YSGame.YSSaveGame.GetInt(jsonObject, text + ".id." + num5, 1));
								}
								fields[i].SetValue(avatar, list5);
							}
						}
					}
				}
				else if (name == "Int32")
				{
					goto IL_38A;
				}
			}
			else if (num != 2940794790U)
			{
				if (num != 3538687084U)
				{
					if (num == 4168357374U)
					{
						if (name == "Int")
						{
							goto IL_38A;
						}
					}
				}
				else if (name == "UInt32")
				{
					goto IL_3B4;
				}
			}
			else if (name == "EmailDataMag")
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
						JSONObject jsonObject10 = YSGame.YSSaveGame.GetJsonObject(jsonObject, text + ".newEmailDictionary", null);
						if (jsonObject10 != null)
						{
							emailDataMag.InitNewJson(jsonObject10);
						}
						JSONObject jsonObject11 = YSGame.YSSaveGame.GetJsonObject(jsonObject, text + ".hasReadEmailDictionary", null);
						if (jsonObject11 != null)
						{
							emailDataMag.InitHasReadJson(jsonObject11);
						}
						JSONObject jsonObject12 = YSGame.YSSaveGame.GetJsonObject(jsonObject, text + ".cyNpcList", null);
						if (jsonObject12 != null)
						{
							emailDataMag.cyNpcList = jsonObject12.ToList();
						}
					}
				}
				catch (Exception)
				{
					Debug.LogError("传音符错误,清空修复");
					emailDataMag = new EmailDataMag();
				}
				fields[i].SetValue(avatar, emailDataMag);
			}
			IL_F26:
			i++;
			continue;
			IL_38A:
			if (YSGame.YSSaveGame.GetInt(jsonObject, text, 0) != 0)
			{
				fields[i].SetValue(avatar, YSGame.YSSaveGame.GetInt(jsonObject, text, 0));
				goto IL_F26;
			}
			goto IL_F26;
			IL_3B4:
			if (YSGame.YSSaveGame.GetInt(jsonObject, text, 0) == 0)
			{
				goto IL_F26;
			}
			if (fields[i].FieldType.Name == "UInt32")
			{
				fields[i].SetValue(avatar, Convert.ToUInt32(YSGame.YSSaveGame.GetInt(jsonObject, text, 0)));
			}
			if (fields[i].FieldType.Name == "UInt16")
			{
				fields[i].SetValue(avatar, Convert.ToUInt16(YSGame.YSSaveGame.GetInt(jsonObject, text, 0)));
			}
			if (fields[i].FieldType.Name == "Int16")
			{
				fields[i].SetValue(avatar, Convert.ToInt16(YSGame.YSSaveGame.GetInt(jsonObject, text, 0)));
			}
			if (fields[i].FieldType.Name == "UInt64")
			{
				fields[i].SetValue(avatar, Convert.ToUInt64(YSGame.YSSaveGame.GetInt(jsonObject, text, 0)));
				goto IL_F26;
			}
			goto IL_F26;
		}
	}

	// Token: 0x06001655 RID: 5717 RVA: 0x00013E46 File Offset: 0x00012046
	public string getSaveID(int id, int index)
	{
		return id + "_" + index;
	}

	// Token: 0x06001656 RID: 5718 RVA: 0x00013E5E File Offset: 0x0001205E
	public void Save(int id, int index, Avatar _avatar = null)
	{
		this.saveGame(id, index, _avatar);
	}

	// Token: 0x06001657 RID: 5719 RVA: 0x00013E69 File Offset: 0x00012069
	public void playerSaveGame(int id, int index, Avatar _avatar = null)
	{
		if (jsonData.instance.saveState == 1)
		{
			UIPopTip.Inst.Pop("存档未完成,请稍等", PopTipIconType.叹号);
			return;
		}
		this.saveGame(id, index, _avatar);
	}

	// Token: 0x06001658 RID: 5720 RVA: 0x000C9984 File Offset: 0x000C7B84
	public void AuToLoad()
	{
		if (SayDialog.GetSayDialog().gameObject != null)
		{
			Object.Destroy(SayDialog.GetSayDialog().gameObject);
		}
		if (FpUIMag.inst != null)
		{
			Object.Destroy(FpUIMag.inst.gameObject);
		}
		if (LianDanUIMag.Instance != null)
		{
			Object.Destroy(LianDanUIMag.Instance.gameObject);
		}
		if (TpUIMag.inst != null)
		{
			Object.Destroy(TpUIMag.inst.gameObject);
		}
		if (SubmitUIMag.Inst != null)
		{
			SubmitUIMag.Inst.Close();
		}
		if (QiYuUIMag.Inst != null)
		{
			Object.Destroy(QiYuUIMag.Inst.gameObject);
		}
		if (CaiYaoUIMag.Inst != null)
		{
			Object.Destroy(CaiYaoUIMag.Inst.gameObject);
		}
		if (PanelMamager.inst.UISceneGameObject != null)
		{
			PanelMamager.inst.UISceneGameObject.SetActive(false);
		}
		if (SingletonMono<TabUIMag>.Instance != null)
		{
			SingletonMono<TabUIMag>.Instance.TryEscClose();
		}
		if (LianQiTotalManager.inst != null)
		{
			Object.Destroy(LianQiTotalManager.inst.gameObject);
		}
		if (SingletonMono<PaiMaiUiMag>.Instance != null)
		{
			Object.Destroy(SingletonMono<PaiMaiUiMag>.Instance.gameObject);
			Time.timeScale = 1f;
		}
		ESCCloseManager.Inst.CloseAll();
		int @int = PlayerPrefs.GetInt("NowPlayerFileAvatar");
		YSGame.YSSaveGame.Reset();
		KBEngineApp.app.entities[10] = null;
		KBEngineApp.app.entities.Remove(10);
		GameObject gameObject = new GameObject();
		gameObject.AddComponent<StartGame>();
		gameObject.GetComponent<StartGame>().startGame(@int, 1, -1);
	}

	// Token: 0x06001659 RID: 5721 RVA: 0x00013E92 File Offset: 0x00012092
	public void AuToSave()
	{
		if (SingletonMono<TabUIMag>.Instance != null)
		{
			SingletonMono<TabUIMag>.Instance.TryEscClose();
		}
		this.playerSaveGame(PlayerPrefs.GetInt("NowPlayerFileAvatar"), 1, null);
	}

	// Token: 0x0600165A RID: 5722 RVA: 0x000C9B28 File Offset: 0x000C7D28
	public void saveGame(int id, int index, Avatar _avatar = null)
	{
		if (!NpcJieSuanManager.inst.isCanJieSuan)
		{
			UIPopTip.Inst.Pop("正在结算中不能存档", PopTipIconType.叹号);
			return;
		}
		if (FpUIMag.inst != null || TpUIMag.inst != null || UINPCJiaoHu.Inst.NowIsJiaoHu2)
		{
			UIPopTip.Inst.Pop("当前状态不能存档", PopTipIconType.叹号);
			return;
		}
		if (!jsonData.instance.SaveLock)
		{
			if (index == 0)
			{
				if (SystemConfig.Inst.GetSaveTimes() == -1)
				{
					return;
				}
				DateTime now = DateTime.Now;
				if (!(now >= this.NextSaveTime))
				{
					return;
				}
				this.NextSaveTime = this.NextSaveTime.AddMinutes((double)this.GetAddTime());
				if (this.NextSaveTime < now)
				{
					this.NextSaveTime = now.AddMinutes((double)this.GetAddTime());
				}
			}
			Avatar avatar = (Avatar)KBEngineApp.app.player();
			if (_avatar != null)
			{
				avatar = _avatar;
			}
			string gamePath = Paths.GetSavePath();
			GameVersion.inst.GetGameVersion();
			JSONObject arr = JSONObject.arr;
			JSONObject jsonobject = new JSONObject();
			int level = (int)avatar.level;
			jsonobject.SetField("firstName", avatar.firstName);
			jsonobject.SetField("lastName", avatar.lastName);
			jsonobject.SetField("gameTime", avatar.worldTimeMag.nowTime);
			jsonobject.SetField("avatarLevel", (int)avatar.level);
			YSGame.YSSaveGame.save("AvatarInfo" + Tools.instance.getSaveID(id, index), jsonobject, gamePath);
			this.getPlayer().StreamData.FungusSaveMgr.SaveData();
			Tools.Save("Avatar" + this.getSaveID(id, index), avatar, gamePath);
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
			avatar.Save(id, index);
			jsonData.instance.saveState = 1;
			UIPopTip.Inst.Pop("开始存档,请等待存档完成", PopTipIconType.叹号);
			Action<object> <>9__1;
			Loom.RunAsync(delegate
			{
				jsonData.instance.SaveLock = true;
				YSGame.YSSaveGame.save("AvatarBackpackJsonData" + Tools.instance.getSaveID(id, index), AvatarBackpackJsonDataClone, gamePath);
				YSGame.YSSaveGame.save("GameVersion" + Tools.instance.getSaveID(id, index), GameVersion.inst.GetGameVersion(), gamePath);
				YSGame.YSSaveGame.save("AvatarBackpackJsonData" + Tools.instance.getSaveID(id, index), AvatarBackpackJsonDataClone, gamePath);
				YSGame.YSSaveGame.save("AvatarRandomJsonData" + Tools.instance.getSaveID(id, index), AvatarRandomJsonDataClone, gamePath);
				YSGame.YSSaveGame.save("NpcBackpack" + Tools.instance.getSaveID(id, index), FactoryManager.inst.loadPlayerDateFactory.savePackDate(AvatarBackpackJsonDataClone), gamePath);
				YSGame.YSSaveGame.save("NpcJsonData" + Tools.instance.getSaveID(id, index), FactoryManager.inst.loadPlayerDateFactory.savaNpcDate(AvatarJsonDateClone), gamePath);
				YSGame.YSSaveGame.save("DeathNpcJsonData" + Tools.instance.getSaveID(id, index), deathNPCJsonDate, gamePath);
				YSGame.YSSaveGame.save("OnlyChengHao" + Tools.instance.getSaveID(id, index), npcOnlyChenghHao, gamePath);
				YSGame.YSSaveGame.save("SaveAvatar" + this.getSaveID(id, index), 1, gamePath);
				YSGame.YSSaveGame.save("AvatarSavetime" + Tools.instance.getSaveID(id, index), DateTime.Now.ToString(), gamePath);
				YSGame.YSSaveGame.save("JieSuanData" + Tools.instance.getSaveID(id, index), FactoryManager.inst.loadPlayerDateFactory.saveJieSuanData(npcBigMapDictionary, npcThreeSenceDictionary, npcFuBenDictionary, JieSuanData), gamePath);
				YSGame.YSSaveGame.save("TuJianSave", TuJianManager.Inst.TuJianSave, gamePath);
				YSGame.YSSaveGame.save("IsComplete" + Tools.instance.getSaveID(id, index), "true", gamePath);
				jsonData.instance.SaveLock = false;
				jsonData.instance.saveState = 0;
				Action<object> taction;
				if ((taction = <>9__1) == null)
				{
					taction = (<>9__1 = delegate(object obj)
					{
						if (YSGame.YSSaveGame.HasFile(Paths.GetSavePath(), "MaxLevelJson"))
						{
							int @int = YSGame.YSSaveGame.GetInt("MaxLevelJson", 0);
							if (level > @int)
							{
								YSGame.YSSaveGame.save("MaxLevelJson", level, gamePath);
							}
						}
						if (global::SaveManager.inst != null)
						{
							global::SaveManager.inst.updateState();
						}
						jsonData.instance.saveState = -1;
						UIPopTip.Inst.Pop("存档完成", PopTipIconType.叹号);
					});
				}
				Loom.QueueOnMainThread(taction, null);
			});
		}
	}

	// Token: 0x0600165B RID: 5723 RVA: 0x000C9E84 File Offset: 0x000C8084
	public static List<T> Clone<T>(object List)
	{
		List<T> result;
		using (Stream stream = new System.IO.MemoryStream())
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			((IFormatter)binaryFormatter).Serialize(stream, List);
			stream.Seek(0L, SeekOrigin.Begin);
			result = (((IFormatter)binaryFormatter).Deserialize(stream) as List<T>);
		}
		return result;
	}

	// Token: 0x0600165C RID: 5724 RVA: 0x000C9ED8 File Offset: 0x000C80D8
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

	// Token: 0x0600165D RID: 5725 RVA: 0x00013EBE File Offset: 0x000120BE
	public void playFader(string content, UnityAction action = null)
	{
		UI_Manager.inst.PlayeJieSuanAnimation(content, action);
	}

	// Token: 0x0600165E RID: 5726 RVA: 0x000C9F30 File Offset: 0x000C8130
	private void playFaderOut()
	{
		CameraManager cameraManager = FungusManager.Instance.CameraManager;
		bool waitUntilFinished = true;
		cameraManager.ScreenFadeTexture = CameraManager.CreateColorTexture(new Color(0f, 0f, 0f), 32, 32);
		cameraManager.Fade(0f, 1f, delegate
		{
			bool waitUntilFinished = waitUntilFinished;
		});
	}

	// Token: 0x0600165F RID: 5727 RVA: 0x000C9F94 File Offset: 0x000C8194
	public static string getScreenName()
	{
		if (RandomFuBen.IsInRandomFuBen)
		{
			return (string)Tools.instance.getPlayer().RandomFuBenList[RandomFuBen.NowRanDomFuBenID.ToString()]["UUID"];
		}
		return SceneManager.GetActiveScene().name;
	}

	// Token: 0x06001660 RID: 5728 RVA: 0x000C9FE4 File Offset: 0x000C81E4
	public static Vector3 ToScenece1080(Vector3 pos)
	{
		float num = 1920f / (float)Screen.width;
		float num2 = 1080f / (float)Screen.height;
		return new Vector3(pos.x * num - 960f, pos.y * num2 - 540f, pos.z);
	}

	// Token: 0x06001661 RID: 5729 RVA: 0x00013ECC File Offset: 0x000120CC
	public static void Say(string text, int hero)
	{
		GameObject gameObject = new GameObject();
		gameObject.AddComponent<Flowchart>();
		gameObject.AddComponent<Say>();
		Say component = gameObject.GetComponent<Say>();
		component.SetStandardText(text);
		component.pubAvatarIDSetType = StartFight.MonstarType.Normal;
		component.pubAvatarIntID = hero;
		component.OnEnter();
	}

	// Token: 0x06001662 RID: 5730 RVA: 0x000CA034 File Offset: 0x000C8234
	public static string getMonstarTitle(int monstarID)
	{
		Avatar player = Tools.instance.getPlayer();
		string result;
		if (monstarID == 1 && player.menPai != 0)
		{
			string str = Tools.getStr("menpai" + player.menPai);
			int num = player.chengHao;
			if (num >= 6)
			{
				num--;
			}
			string str2 = jsonData.instance.ChengHaoJsonData[num.ToString()]["Name"].Str;
			result = str + str2;
		}
		else if (jsonData.instance.AvatarJsonData.HasField(monstarID.ToString()))
		{
			result = jsonData.instance.AvatarJsonData[monstarID.ToString()]["Title"].Str;
		}
		else
		{
			result = "";
			Debug.LogError(string.Format("获取敌人称号时出错，jsonData.instance.AvatarJsonData中没有id {0}", monstarID));
		}
		return result;
	}

	// Token: 0x06001663 RID: 5731 RVA: 0x000CA114 File Offset: 0x000C8314
	public static string GetPlayerTitle()
	{
		Avatar player = Tools.instance.getPlayer();
		string str = Tools.getStr("menpai" + player.menPai);
		int num = player.chengHao;
		if (player.menPai > 0 && player.chengHao >= 6)
		{
			num = player.chengHao - 1;
		}
		string str2 = Tools.instance.Code64ToString(jsonData.instance.ChengHaoJsonData[num.ToString()]["Name"].str);
		return str + str2;
	}

	// Token: 0x06001664 RID: 5732 RVA: 0x000CA1A0 File Offset: 0x000C83A0
	public static string GetPlayerName()
	{
		Avatar player = Tools.instance.getPlayer();
		return player.firstName + player.lastName;
	}

	// Token: 0x06001665 RID: 5733 RVA: 0x000CA1CC File Offset: 0x000C83CC
	public static int getRandomInt(int start, int end)
	{
		int num = jsonData.GetRandom() % (end - start + 1);
		return start + num;
	}

	// Token: 0x06001666 RID: 5734 RVA: 0x000CA1E8 File Offset: 0x000C83E8
	public static bool HasItems(JToken json, int item)
	{
		using (IEnumerator<JToken> enumerator = ((JArray)json).GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if ((int)enumerator.Current == item)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06001667 RID: 5735 RVA: 0x000CA23C File Offset: 0x000C843C
	public static int getRandomList(List<int> list)
	{
		int num = 0;
		foreach (int num2 in list)
		{
			num += num2;
		}
		int num3 = jsonData.GetRandom() % num;
		int num4 = 0;
		int num5 = 0;
		foreach (int num6 in list)
		{
			num5 += num6;
			if (num5 > num3)
			{
				return num4;
			}
			num4++;
		}
		return 0;
	}

	// Token: 0x06001668 RID: 5736 RVA: 0x00013F00 File Offset: 0x00012100
	public int GetRandomInt(int min, int max)
	{
		return this.random.Next(min, max + 1);
	}

	// Token: 0x06001669 RID: 5737 RVA: 0x000CA2E8 File Offset: 0x000C84E8
	public static List<int> getNumRandomList(List<int> list, int num)
	{
		List<int> list2 = new List<int>();
		if (num > list.Count)
		{
			Debug.LogError("随机数超出数量");
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

	// Token: 0x0600166A RID: 5738 RVA: 0x00013F11 File Offset: 0x00012111
	public static JToken RandomGetArrayToken(JToken list)
	{
		if (((JArray)list).Count > 0)
		{
			return list[jsonData.GetRandom() % ((JArray)list).Count];
		}
		return null;
	}

	// Token: 0x0600166B RID: 5739 RVA: 0x00013F3F File Offset: 0x0001213F
	public static T RandomGetToken<T>(List<T> list)
	{
		return list[jsonData.GetRandom() % list.Count];
	}

	// Token: 0x0600166C RID: 5740 RVA: 0x000CA340 File Offset: 0x000C8540
	public static JToken FindJTokens(JToken list, Tools.FindTokenMethod unityAction)
	{
		foreach (KeyValuePair<string, JToken> keyValuePair in ((JObject)list))
		{
			if (unityAction(keyValuePair.Value))
			{
				return keyValuePair.Value;
			}
		}
		return null;
	}

	// Token: 0x0600166D RID: 5741 RVA: 0x000CA3A4 File Offset: 0x000C85A4
	public static List<JToken> FindAllJTokens(JToken list, Tools.FindTokenMethod unityAction)
	{
		List<JToken> list2 = new List<JToken>();
		foreach (KeyValuePair<string, JToken> keyValuePair in ((JObject)list))
		{
			if (unityAction(keyValuePair.Value))
			{
				list2.Add(keyValuePair.Value);
			}
		}
		return list2;
	}

	// Token: 0x0600166E RID: 5742 RVA: 0x000CA410 File Offset: 0x000C8610
	public static bool ContensInt(JArray jArray, int item)
	{
		using (IEnumerator<JToken> enumerator = jArray.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if ((int)enumerator.Current == item)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x0600166F RID: 5743 RVA: 0x000CA460 File Offset: 0x000C8660
	public static int GetRandomByJToken(JToken list)
	{
		int num = 0;
		foreach (JToken jtoken in list)
		{
			num += (int)jtoken;
		}
		if (num == 0)
		{
			return -1;
		}
		int num2 = jsonData.GetRandom() % num;
		int num3 = 0;
		foreach (JToken jtoken2 in list)
		{
			if ((int)jtoken2 > num2)
			{
				return num3;
			}
			num2 -= (int)jtoken2;
			num3++;
		}
		return -1;
	}

	// Token: 0x06001670 RID: 5744 RVA: 0x000CA510 File Offset: 0x000C8710
	public JSONObject getRandomListByPercent(List<JSONObject> list, string percent)
	{
		int num = 0;
		foreach (JSONObject jsonobject in list)
		{
			num += (int)jsonobject[percent].n;
		}
		if (num == 0)
		{
			return null;
		}
		int num2 = jsonData.GetRandom() % num;
		foreach (JSONObject jsonobject2 in list)
		{
			if ((int)jsonobject2[percent].n > num2)
			{
				return jsonobject2;
			}
			num2 -= (int)jsonobject2[percent].n;
		}
		return null;
	}

	// Token: 0x06001671 RID: 5745 RVA: 0x000CA5DC File Offset: 0x000C87DC
	public JToken getRandomListByPercent(List<JToken> list, string percent)
	{
		int num = 0;
		foreach (JToken jtoken in list)
		{
			num += (int)jtoken[percent];
		}
		if (num == 0)
		{
			return null;
		}
		int num2 = jsonData.instance.QuikeGetRandom() % num;
		foreach (JToken jtoken2 in list)
		{
			if ((int)jtoken2[percent] > num2)
			{
				return jtoken2;
			}
			num2 -= (int)jtoken2[percent];
		}
		return null;
	}

	// Token: 0x06001672 RID: 5746 RVA: 0x000CA6A8 File Offset: 0x000C88A8
	public static JSONObject getRandomList(List<JSONObject> list)
	{
		int count = list.Count;
		if (count <= 0)
		{
			Debug.LogError("获取随机值出错");
			return null;
		}
		int index = jsonData.GetRandom() % count;
		return list[index];
	}

	// Token: 0x06001673 RID: 5747 RVA: 0x000CA6DC File Offset: 0x000C88DC
	public static List<int> JsonListToList(JSONObject json)
	{
		List<int> list = new List<int>();
		foreach (JSONObject jsonobject in json.list)
		{
			list.Add((int)jsonobject.n);
		}
		return list;
	}

	// Token: 0x06001674 RID: 5748 RVA: 0x000CA73C File Offset: 0x000C893C
	public static List<JSONObject> GetStaticNumJsonobj(JSONObject json, string name, int Num)
	{
		List<JSONObject> list = new List<JSONObject>();
		foreach (JSONObject jsonobject in json.list)
		{
			if ((int)jsonobject[name].n == Num)
			{
				list.Add(jsonobject);
			}
		}
		return list;
	}

	// Token: 0x06001675 RID: 5749 RVA: 0x00013F53 File Offset: 0x00012153
	public bool IsInTime(string _NowTime, string _startTime, string _endTime)
	{
		return this.IsInTime(DateTime.Parse(_NowTime), DateTime.Parse(_startTime), DateTime.Parse(_endTime), 0);
	}

	// Token: 0x06001676 RID: 5750 RVA: 0x000CA7A8 File Offset: 0x000C89A8
	public bool IsInTime(DateTime _NowTime, DateTime _startTime, DateTime _endTime, int circulation = 0)
	{
		DateTime t = _startTime;
		DateTime dateTime = _NowTime;
		if (circulation > 0 && t.Year < dateTime.Year && dateTime.Year % circulation == t.Year % circulation)
		{
			dateTime = new DateTime(t.Year, dateTime.Month, (dateTime.Month == 2 && dateTime.Day == 29) ? 28 : dateTime.Day);
		}
		return t <= dateTime && dateTime <= _endTime;
	}

	// Token: 0x06001677 RID: 5751 RVA: 0x000CA830 File Offset: 0x000C8A30
	public static void dictionaryAddNum(Dictionary<int, int> zhuyaoList, int key, int num)
	{
		if (zhuyaoList.ContainsKey(key))
		{
			zhuyaoList[key] += num;
			return;
		}
		zhuyaoList[key] = num;
	}

	// Token: 0x06001678 RID: 5752 RVA: 0x00013F6E File Offset: 0x0001216E
	public static int getJsonobject(JSONObject aa, string name)
	{
		if (!aa.HasField(name))
		{
			return 0;
		}
		return (int)aa[name].n;
	}

	// Token: 0x06001679 RID: 5753 RVA: 0x000CA864 File Offset: 0x000C8A64
	public static string getUUID()
	{
		Guid guid = default(Guid);
		return Guid.NewGuid().ToString("N");
	}

	// Token: 0x0600167A RID: 5754 RVA: 0x000CA88C File Offset: 0x000C8A8C
	public static JSONObject CreateItemSeid(int itemID)
	{
		JSONObject jsonobject = new JSONObject(JSONObject.Type.OBJECT);
		if (_ItemJsonData.DataDict.ContainsKey(itemID))
		{
			_ItemJsonData itemJsonData = _ItemJsonData.DataDict[itemID];
			if (itemJsonData.type == 9)
			{
				jsonobject.AddField("NaiJiu", 100);
			}
			if (itemJsonData.type == 14)
			{
				JToken jtoken = jsonData.instance.LingZhouPinJie[itemJsonData.quality.ToString()];
				jsonobject.AddField("NaiJiu", (int)jtoken["Naijiu"]);
			}
		}
		else
		{
			Debug.LogError(string.Format("物品表中没有ID为{0}的物品", itemID));
		}
		return jsonobject;
	}

	// Token: 0x0600167B RID: 5755 RVA: 0x00013F88 File Offset: 0x00012188
	public void nextQueen(float time)
	{
		base.Invoke("continueQueen", time);
	}

	// Token: 0x0600167C RID: 5756 RVA: 0x000CA928 File Offset: 0x000C8B28
	public static void AddQueue(UnityAction cell)
	{
		Queue<UnityAction> queue = new Queue<UnityAction>();
		queue.Enqueue(cell);
		YSFuncList.Ints.AddFunc(queue);
	}

	// Token: 0x0600167D RID: 5757 RVA: 0x00013F96 File Offset: 0x00012196
	public void ToolsStartCoroutine(IEnumerator Temp)
	{
		base.StartCoroutine(Temp);
	}

	// Token: 0x0600167E RID: 5758 RVA: 0x000112BB File Offset: 0x0000F4BB
	private void continueQueen()
	{
		YSFuncList.Ints.Continue();
	}

	// Token: 0x0600167F RID: 5759 RVA: 0x000CA950 File Offset: 0x000C8B50
	public void showFightUIPlan(string text, int itemID, int ItemNum)
	{
		Tools.canClickFlag = false;
		GameObject gameObject = Object.Instantiate<GameObject>(Resources.Load("uiPrefab/FightUIRoot") as GameObject);
		gameObject.transform.parent = GameObject.Find("UI Root (2D)").transform;
		gameObject.transform.localScale = Vector3.one * 0.75f;
		gameObject.transform.localPosition = Vector3.zero;
		FightUIRoot tempFightUIRoot = gameObject.GetComponent<FightUIRoot>();
		tempFightUIRoot.setTitle("事件");
		tempFightUIRoot.label.text = text;
		tempFightUIRoot.confimBtn.onClick.Add(new EventDelegate(delegate()
		{
			UltimateSurvival.MonoSingleton<UI_Backgroud>.Instance.Value = false;
			tempFightUIRoot.gameObject.SetActive(false);
			Tools.canClickFlag = true;
		}));
		JSONObject addItemList = new JSONObject(JSONObject.Type.ARRAY);
		tempFightUIRoot.addTempItem(ref addItemList, itemID, ItemNum, null);
		tempFightUIRoot.AvatarAddItem(addItemList);
	}

	// Token: 0x06001680 RID: 5760 RVA: 0x000CAA38 File Offset: 0x000C8C38
	public static string getCaiJiText(string desc, int id, int num, int addNum, int addTime)
	{
		return jsonData.instance.AllMapCaiJiMiaoShuBiao["1"][desc].Str.Replace("{ItemName}", jsonData.instance.ItemJsonData[id.ToString()]["name"].Str).Replace("{ItemNum}", num.ToString()).Replace("{AddNum}", addNum.ToString()).Replace("{AddTime}", addTime.ToString());
	}

	// Token: 0x06001681 RID: 5761 RVA: 0x000CAAC8 File Offset: 0x000C8CC8
	public JSONObject getWuJiangBangDing(int AvatarID)
	{
		foreach (JSONObject jsonobject in jsonData.instance.WuJiangBangDing.list.FindAll((JSONObject _json) => _json["avatar"].HasItem(AvatarID)))
		{
			if (this.IsInTime(jsonobject["TimeStart"].str, jsonobject["TimeEnd"].str))
			{
				return jsonobject;
			}
		}
		return null;
	}

	// Token: 0x06001682 RID: 5762 RVA: 0x000CAB6C File Offset: 0x000C8D6C
	public bool IsInTime(string StatrTime, string EndTime)
	{
		Avatar player = this.getPlayer();
		DateTime t = DateTime.Parse(StatrTime);
		DateTime t2 = DateTime.Parse(EndTime);
		DateTime nowTime = player.worldTimeMag.getNowTime();
		return t <= nowTime && nowTime <= t2;
	}

	// Token: 0x06001683 RID: 5763 RVA: 0x00013FA0 File Offset: 0x000121A0
	public static bool IsInNum(int num, int startNum, int EndNum)
	{
		return num >= startNum && num <= EndNum;
	}

	// Token: 0x06001684 RID: 5764 RVA: 0x000CABB0 File Offset: 0x000C8DB0
	public static DateTime GetEndTime(string startTime, int Day = 0, int Month = 0, int year = 0)
	{
		return DateTime.Parse(startTime).AddDays((double)Day).AddMonths(Month).AddYears(year);
	}

	// Token: 0x06001685 RID: 5765 RVA: 0x00013FAF File Offset: 0x000121AF
	public static DateTime getShengYuShiJian(string nowTime, string endTime)
	{
		return Tools.getShengYuShiJian(DateTime.Parse(nowTime), DateTime.Parse(endTime));
	}

	// Token: 0x06001686 RID: 5766 RVA: 0x000CABE0 File Offset: 0x000C8DE0
	public static DateTime getShengYuShiJian(DateTime nowTime, DateTime endTime)
	{
		if (endTime > nowTime)
		{
			TimeSpan timeSpan = endTime - nowTime;
			return new DateTime(1, 1, 1).AddDays((double)timeSpan.Days);
		}
		Debug.Log(string.Format("获取剩余时间出现endTime>nowTime，endTime:{0}，nowTime：{1}", endTime, nowTime));
		return DateTime.MinValue;
	}

	// Token: 0x06001687 RID: 5767 RVA: 0x000CAC38 File Offset: 0x000C8E38
	public static string TimeToShengYuTime(DateTime check, string Title = "剩余时间")
	{
		return (Title + "{X}").Replace("{X}", string.Concat(new object[]
		{
			check.Year - 1,
			"年",
			check.Month - 1,
			"月",
			check.Day - 1,
			"日"
		}));
	}

	// Token: 0x06001688 RID: 5768 RVA: 0x00013FC2 File Offset: 0x000121C2
	public static GameObject InstantiateGameObject(GameObject temp, Transform parent)
	{
		GameObject gameObject = Object.Instantiate<GameObject>(temp);
		gameObject.transform.SetParent(parent);
		gameObject.SetActive(true);
		gameObject.transform.localScale = Vector3.one;
		return gameObject;
	}

	// Token: 0x06001689 RID: 5769 RVA: 0x00013FED File Offset: 0x000121ED
	public static string getLiDanLeiXinStr(int leixing)
	{
		if (leixing <= 0)
		{
			return "无";
		}
		return LianDanItemLeiXin.DataDict[leixing].name;
	}

	// Token: 0x0600168A RID: 5770 RVA: 0x00014009 File Offset: 0x00012209
	public static GameObject showSkillChoice()
	{
		return Object.Instantiate<GameObject>(Resources.Load("uiPrefab/Fight/SkillChoic") as GameObject);
	}

	// Token: 0x0600168B RID: 5771 RVA: 0x0001401F File Offset: 0x0001221F
	public static bool symbol(string type, int statr, int end)
	{
		if (type == ">")
		{
			if (statr > end)
			{
				return true;
			}
		}
		else if (type == "<")
		{
			if (statr < end)
			{
				return true;
			}
		}
		else if (type == "=" && statr == end)
		{
			return true;
		}
		return false;
	}

	// Token: 0x0600168C RID: 5772 RVA: 0x0001405B File Offset: 0x0001225B
	public static Dictionary<int, int> GetXiangSheng()
	{
		Dictionary<int, int> dictionary = new Dictionary<int, int>();
		dictionary[0] = 2;
		dictionary[1] = 3;
		dictionary[2] = 1;
		dictionary[3] = 4;
		dictionary[4] = 0;
		return dictionary;
	}

	// Token: 0x0600168D RID: 5773 RVA: 0x0001408A File Offset: 0x0001228A
	public static Dictionary<int, int> GetXiangKe()
	{
		Dictionary<int, int> dictionary = new Dictionary<int, int>();
		dictionary[0] = 1;
		dictionary[1] = 4;
		dictionary[2] = 3;
		dictionary[3] = 0;
		dictionary[4] = 2;
		return dictionary;
	}

	// Token: 0x0600168E RID: 5774 RVA: 0x000CACB0 File Offset: 0x000C8EB0
	public bool CheckBadWord(string input)
	{
		foreach (KeyValuePair<string, JToken> keyValuePair in jsonData.instance.BadWord)
		{
			string value = (string)keyValuePair.Value["name"];
			if (input.Contains(value))
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x0600168F RID: 5775 RVA: 0x000140B9 File Offset: 0x000122B9
	public int GetAddTime()
	{
		return SystemConfig.Inst.GetSaveTimes();
	}

	// Token: 0x040011E7 RID: 4583
	public static Tools instance;

	// Token: 0x040011E8 RID: 4584
	public int MonstarID = 1;

	// Token: 0x040011E9 RID: 4585
	public int LunDaoNpcId = -1;

	// Token: 0x040011EA RID: 4586
	public List<int> LunTiList = new List<int>();

	// Token: 0x040011EB RID: 4587
	public bool IsSuiJiLunTi;

	// Token: 0x040011EC RID: 4588
	public int LunTiNum;

	// Token: 0x040011ED RID: 4589
	public int TargetLunTiNum;

	// Token: 0x040011EE RID: 4590
	public MonstarMag monstarMag;

	// Token: 0x040011EF RID: 4591
	public string FinalScene = "AllMaps";

	// Token: 0x040011F0 RID: 4592
	public int CanShowFightUI;

	// Token: 0x040011F1 RID: 4593
	public static bool canClickFlag = true;

	// Token: 0x040011F2 RID: 4594
	public int fubenLastIndex = -1;

	// Token: 0x040011F3 RID: 4595
	public CaiYao.ItemData CaiYaoData;

	// Token: 0x040011F4 RID: 4596
	public bool SeaRemoveMonstarFlag;

	// Token: 0x040011F5 RID: 4597
	public string SeaRemoveMonstarUUID = "";

	// Token: 0x040011F6 RID: 4598
	public bool ShowPingJin = true;

	// Token: 0x040011F7 RID: 4599
	public bool isInitSceneBtn;

	// Token: 0x040011F8 RID: 4600
	public int loadSceneType = -1;

	// Token: 0x040011F9 RID: 4601
	public string ohtherSceneName = "";

	// Token: 0x040011FA RID: 4602
	public int CanFpRun = 1;

	// Token: 0x040011FB RID: 4603
	public Random random;

	// Token: 0x040011FC RID: 4604
	public bool IsCanLoadSetTalk = true;

	// Token: 0x040011FD RID: 4605
	private bool _isNeedSetTalk;

	// Token: 0x040011FE RID: 4606
	public bool IsInDF;

	// Token: 0x040011FF RID: 4607
	public bool isNewAvatar;

	// Token: 0x04001200 RID: 4608
	public bool CanOpenTab = true;

	// Token: 0x04001201 RID: 4609
	public bool IsLoadData;

	// Token: 0x04001202 RID: 4610
	public static string jumpToName = "";

	// Token: 0x04001203 RID: 4611
	public DateTime NextSaveTime;

	// Token: 0x020002DF RID: 735
	// (Invoke) Token: 0x06001693 RID: 5779
	public delegate bool FindTokenMethod(JToken aa);
}
