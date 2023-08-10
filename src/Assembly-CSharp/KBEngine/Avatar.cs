using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;
using Bag;
using Fungus;
using GUIPackage;
using JSONClass;
using Newtonsoft.Json.Linq;
using PingJing;
using TuPo;
using UnityEngine;
using UnityEngine.Events;
using YSGame;
using YSGame.Fight;
using YSGame.TuJian;
using script.MenPaiTask;

namespace KBEngine;

public class Avatar : AvatarBase
{
	[Serializable]
	[CompilerGenerated]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9 = new _003C_003Ec();

		public static Action<object> _003C_003E9__262_1;

		public static Predicate<JSONObject> _003C_003E9__283_6;

		public static UnityAction _003C_003E9__301_0;

		public static Predicate<JSONObject> _003C_003E9__310_0;

		public static Comparison<ITEM_INFO> _003C_003E9__364_0;

		internal void _003CAddTime_003Eb__262_1(object obj)
		{
			GaoShiManager.OnAddTime();
		}

		internal bool _003CresetNode_003Eb__283_6(JSONObject _aa)
		{
			return (int)_aa.n == Tools.instance.getPlayer().level;
		}

		internal void _003Cdie_003Eb__301_0()
		{
			Tools.instance.loadMapScenes(Tools.instance.FinalScene);
			YSFuncList.Ints.Continue();
		}

		internal bool _003CHasDunShuSkill_003Eb__310_0(JSONObject aa)
		{
			return (int)aa.n == 9;
		}

		internal int _003CSortItem_003Eb__364_0(ITEM_INFO a, ITEM_INFO b)
		{
			try
			{
				_ItemJsonData itemJsonData = _ItemJsonData.DataDict[a.itemId];
				_ItemJsonData itemJsonData2 = _ItemJsonData.DataDict[b.itemId];
				JSONObject seid = a.Seid;
				JSONObject seid2 = b.Seid;
				int num = itemJsonData.GetHashCode();
				int num2 = itemJsonData2.GetHashCode();
				int num3 = itemJsonData.quality;
				int num4 = itemJsonData2.quality;
				if (seid != null && seid.HasField("quality"))
				{
					num3 = seid["quality"].I;
					num += seid.GetHashCode();
				}
				if (seid2 != null && seid2.HasField("quality"))
				{
					num4 = seid2["quality"].I;
					num2 += seid2.GetHashCode();
				}
				if (itemJsonData.type == 3 || itemJsonData.type == 4)
				{
					num3 *= 2;
				}
				if (itemJsonData2.type == 3 || itemJsonData2.type == 4)
				{
					num4 *= 2;
				}
				if (itemJsonData.type == 0 || itemJsonData.type == 1 || itemJsonData.type == 2)
				{
					num3++;
				}
				if (itemJsonData2.type == 0 || itemJsonData2.type == 1 || itemJsonData2.type == 2)
				{
					num4++;
				}
				if (num3 != num4)
				{
					return num4.CompareTo(num3);
				}
				if (itemJsonData.type != itemJsonData2.type)
				{
					return itemJsonData.type.CompareTo(itemJsonData2.type);
				}
				if (itemJsonData.id != itemJsonData2.id)
				{
					return itemJsonData.id.CompareTo(itemJsonData2.id);
				}
				return num.CompareTo(num2);
			}
			catch
			{
				return 1;
			}
		}
	}

	public string lastScence = "AllMaps";

	public string lastFuBenScence = "";

	public string NowFuBen = "";

	public int BanBenHao = 1;

	public int NowRandomFuBenID;

	public int showSkillName;

	public int showStaticSkillDengJi;

	public int chengHao;

	public CardMag cardMag;

	public Combat combat;

	public AI ai;

	public Spell spell;

	public JieYin jieyin;

	public Dialog dialogMsg;

	public BuffMag buffmag;

	public WuDaoMag wuDaoMag;

	public WorldTime worldTimeMag = new WorldTime();

	public EmailDataMag emailDateMag = new EmailDataMag();

	public StreamData StreamData = new StreamData();

	public int ExchangeMeetingID;

	public TaskMag taskMag;

	public FightTempValue fightTemp;

	public ZulinContorl zulinContorl;

	public FubenContrl fubenContorl;

	public NomelTaskMag nomelTaskMag;

	public chenghaoMag chenghaomag;

	public RandomFuBenMag randomFuBenMag;

	public SeaNodeMag seaNodeMag;

	public ChuanYingManager chuanYingManager;

	public JianLingManager jianLingManager;

	public static SkillBox skillbox = new SkillBox();

	public Dictionary<ulong, ITEM_INFO> itemDict = new Dictionary<ulong, ITEM_INFO>();

	public Dictionary<ulong, ITEM_INFO> equipItemDict = new Dictionary<ulong, ITEM_INFO>();

	public List<GUIPackage.Skill> skill = new List<GUIPackage.Skill>();

	public List<StaticSkill> StaticSkill = new List<StaticSkill>();

	private ulong[] itemIndex2Uids = new ulong[50];

	private ulong[] equipIndex2Uids = new ulong[5];

	public List<List<int>> bufflist = new List<List<int>>();

	public Dictionary<int, Dictionary<int, int>> SkillSeidFlag = new Dictionary<int, Dictionary<int, int>>();

	public Dictionary<int, Dictionary<int, int>> BuffSeidFlag = new Dictionary<int, Dictionary<int, int>>();

	public Dictionary<int, Dictionary<int, int>> StaticSkillSeidFlag = new Dictionary<int, Dictionary<int, int>>();

	public Dictionary<int, Dictionary<int, int>> EquipSeidFlag = new Dictionary<int, Dictionary<int, int>>();

	public Dictionary<int, Dictionary<int, int>> JieDanSkillSeidFlag = new Dictionary<int, Dictionary<int, int>>();

	public Dictionary<int, int> DrawWeight = new Dictionary<int, int>();

	public int NowMapIndex = 101;

	public int SkillRemoveCardNum;

	public int nowConfigEquipSkill;

	public int nowConfigEquipStaticSkill;

	public int nowConfigEquipItem;

	public List<SkillItem>[] configEquipSkill = new List<SkillItem>[5]
	{
		new List<SkillItem>(),
		new List<SkillItem>(),
		new List<SkillItem>(),
		new List<SkillItem>(),
		new List<SkillItem>()
	};

	public List<SkillItem>[] configEquipStaticSkill = new List<SkillItem>[5]
	{
		new List<SkillItem>(),
		new List<SkillItem>(),
		new List<SkillItem>(),
		new List<SkillItem>(),
		new List<SkillItem>()
	};

	public ITEM_INFO_LIST[] configEquipItem = new ITEM_INFO_LIST[5]
	{
		new ITEM_INFO_LIST(),
		new ITEM_INFO_LIST(),
		new ITEM_INFO_LIST(),
		new ITEM_INFO_LIST(),
		new ITEM_INFO_LIST()
	};

	public List<SkillItem> equipSkillList = new List<SkillItem>();

	public List<SkillItem> equipStaticSkillList = new List<SkillItem>();

	public List<SkillItem> hasJieDanSkillList = new List<SkillItem>();

	public List<SkillItem> hasSkillList = new List<SkillItem>();

	public List<SkillItem> hasStaticSkillList = new List<SkillItem>();

	public Avatar OtherAvatar;

	public int showTupo;

	public int _xinjin;

	public string firstName = "";

	public string lastName = "";

	public int Sex = 1;

	public int nowPaiMaiCompereAvatarID;

	public int nowPaiMaiID;

	public int _WuDaoDian;

	public int _JieYingJinMai;

	public int _JieYingYiZHi;

	public AvatarStaticValue StaticValue = new AvatarStaticValue();

	public JSONObject AvatarGotChuanGong = new JSONObject();

	public JSONObject AvatarQieCuo = new JSONObject();

	public JSONObject SuiJiShiJian = new JSONObject(JSONObject.Type.OBJECT);

	public JSONObject ZuLin = new JSONObject(JSONObject.Type.OBJECT);

	public JSONObject FuBen = new JSONObject(JSONObject.Type.OBJECT);

	public JSONObject CanJiaPaiMai = new JSONObject(JSONObject.Type.OBJECT);

	public JSONObject NaiYaoXin = new JSONObject(JSONObject.Type.OBJECT);

	public JSONObject DanFang = new JSONObject(JSONObject.Type.ARRAY);

	public JSONObject YaoCaiShuXin = new JSONObject(JSONObject.Type.OBJECT);

	public JSONObject YaoCaiChanDi = new JSONObject(JSONObject.Type.ARRAY);

	public JSONObject YaoCaiIsGet = new JSONObject(JSONObject.Type.ARRAY);

	public JSONObject AllMapRandomNode = new JSONObject(JSONObject.Type.OBJECT);

	public JSONObject MenPaiHaoGanDu = new JSONObject(JSONObject.Type.OBJECT);

	public JSONObject NomelTaskJson = new JSONObject(JSONObject.Type.OBJECT);

	public JSONObject NomelTaskFlag = new JSONObject(JSONObject.Type.OBJECT);

	public JSONObject LingGuang = new JSONObject(JSONObject.Type.ARRAY);

	public JSONObject TianFuID = new JSONObject(JSONObject.Type.OBJECT);

	public JSONObject SelectTianFuID = new JSONObject(JSONObject.Type.ARRAY);

	public JSONObject openPanelList = new JSONObject();

	public JSONObject NoGetChuanYingList = new JSONObject();

	public JSONObject NewChuanYingList = new JSONObject();

	public JSONObject HasReadChuanYingList = new JSONObject();

	public JSONObject TieJianHongDianList = new JSONObject();

	public JSONObject ToalChuanYingFuList = new JSONObject();

	public JSONObject HasSendChuanYingFuList = new JSONObject();

	public JSONObject PaiMaiMaxMoneyAvatarDate = new JSONObject();

	public JSONObject WuDaoKillAvatar = new JSONObject(JSONObject.Type.ARRAY);

	public JSONObject HasLianZhiDanYao = new JSONObject(JSONObject.Type.ARRAY);

	public JSONObject AvatarChengJiuData = new JSONObject(JSONObject.Type.OBJECT);

	public JSONObject AvatarHasAchivement = new JSONObject(JSONObject.Type.ARRAY);

	public JSONObject ShangJinPingFen = new JSONObject(JSONObject.Type.OBJECT);

	public JSONObject ShiLiChengHaoLevel = new JSONObject(JSONObject.Type.OBJECT);

	public int NPCCreateIndex = 20000;

	public JSONObject AvatarFengLu = new JSONObject(JSONObject.Type.ARRAY);

	public JSONObject ZengLi = new JSONObject(JSONObject.Type.OBJECT);

	public JSONObject TeatherId = new JSONObject(JSONObject.Type.ARRAY);

	public JSONObject DaoLvId = new JSONObject(JSONObject.Type.ARRAY);

	public JSONObject Brother = new JSONObject(JSONObject.Type.ARRAY);

	public JSONObject TuDiId = new JSONObject(JSONObject.Type.ARRAY);

	public JSONObject DaTingId = new JSONObject(JSONObject.Type.ARRAY);

	public int IsShowXuanWo;

	public JSONObject PlayTutorialData = new JSONObject(JSONObject.Type.OBJECT);

	public JSONObject ShuangXiuData = new JSONObject(JSONObject.Type.OBJECT);

	public JSONObject DaoLvChengHu = new JSONObject(JSONObject.Type.OBJECT);

	public JSONObject DongFuData = new JSONObject(JSONObject.Type.OBJECT);

	public JSONObject NowDongFuID = new JSONObject(JSONObject.Type.NUMBER);

	public JSONObject GaoShi = new JSONObject(JSONObject.Type.OBJECT);

	public JSONObject SeaTanSuoDu = new JSONObject(JSONObject.Type.OBJECT);

	public JSONObject HuaShenStartXianXing = new JSONObject(0);

	public JSONObject TianJie = new JSONObject(JSONObject.Type.OBJECT);

	public JSONObject TianJieCanLingWuSkills = new JSONObject(JSONObject.Type.ARRAY);

	public JSONObject TianJieYiLingWuSkills = new JSONObject(JSONObject.Type.ARRAY);

	public JSONObject TianJieEquipedSkills = new JSONObject(JSONObject.Type.ARRAY);

	public JSONObject TianJieSkillRecordValue = new JSONObject(JSONObject.Type.OBJECT);

	public JSONObject TianJieCanGuanNPCs = new JSONObject(JSONObject.Type.ARRAY);

	public string TianJieBeforeShenYouSceneName = "";

	public JSONObject HuaShenWuDao = new JSONObject(0);

	public JSONObject HuaShenLingYuSkill = new JSONObject(0);

	public JSONObject HideHaiYuTanSuo = new JSONObject(JSONObject.Type.ARRAY);

	public JSONObject FightCostRecord = new JSONObject(JSONObject.Type.OBJECT);

	public JSONObject JianLingUnlockedXianSuo = new JSONObject(JSONObject.Type.OBJECT);

	public JSONObject JianLingUnlockedZhenXiang = new JSONObject(JSONObject.Type.OBJECT);

	public int JianLingExJiYiHuiFuDu;

	public JSONObject ShengPingRecord = new JSONObject(JSONObject.Type.OBJECT);

	public JSONObject OnceShow = new JSONObject(JSONObject.Type.ARRAY);

	public int Face;

	public string FaceWorkshop = "";

	public JSONObject RandomSeed = new JSONObject(0);

	public JSONObject LingHeCaiJi = new JSONObject(JSONObject.Type.OBJECT);

	public string NextCreateTime = "0010-1-1";

	public int LunDaoState = 3;

	public int LingGan = 20;

	public int WuDaoZhi;

	public int lastYear = 1;

	public int fakeTimes;

	public int deathType;

	public int WuDaoZhiLevel;

	public int BiGuanLingGuangTime;

	public JSONObject WuDaoJson = new JSONObject(JSONObject.Type.OBJECT);

	public JObject RandomFuBenList = new JObject();

	public JObject EndlessSea = new JObject();

	public JObject StaticNTaskTime = new JObject();

	public JObject EndlessSeaRandomNode = new JObject();

	public JObject EndlessSeaAvatarSeeIsland = new JObject();

	public JSONObject EndlessSeaBoss = new JSONObject(JSONObject.Type.OBJECT);

	public JObject ItemBuffList = new JObject();

	public JSONObject TaskZhuiZhong = new JSONObject();

	public int Dandu;

	private int _ZhuJiJinDu;

	public int AliveFriendCount;

	public bool IsCanSetFace;

	public ElderTaskMag ElderTaskMag => StreamData.ZhangLaoTaskMag;

	public new CardMag crystal => cardMag;

	public int shengShi
	{
		get
		{
			int num = 0;
			foreach (KeyValuePair<int, int> item in fightTemp.tempShenShi)
			{
				num += item.Value;
			}
			return _shengShi + num + getStaticSkillAddSum(2) + getEquipAddSum(4);
		}
		set
		{
			_shengShi = value;
		}
	}

	public int dunSu
	{
		get
		{
			int num = 0;
			foreach (KeyValuePair<int, int> item in fightTemp.tempDunSu)
			{
				num += item.Value;
			}
			return _dunSu + num + getStaticSkillAddSum(8) + getEquipAddSum(8);
		}
		set
		{
			_dunSu = value;
		}
	}

	public int HP_Max
	{
		get
		{
			int num = 0;
			foreach (KeyValuePair<int, int> item in fightTemp.TempHP_Max)
			{
				num += item.Value;
			}
			return _HP_Max + num + getStaticSkillAddSum(3) + getEquipAddSum(3) + getJieDanSkillAddHP();
		}
		set
		{
			_HP_Max = value;
		}
	}

	public List<int> GetLingGeng
	{
		get
		{
			List<int> temp = new List<int>();
			LingGeng.ForEach(delegate(int aa)
			{
				temp.Add(aa);
			});
			foreach (KeyValuePair<int, int> item in getJieDanAddLingGen())
			{
				temp[item.Key] += item.Value;
			}
			return temp;
		}
	}

	public int xinjin
	{
		get
		{
			return _xinjin;
		}
		set
		{
			_xinjin = value;
		}
	}

	public new int ZiZhi
	{
		get
		{
			return base.ZiZhi + getStaticSkillAddSum(6);
		}
		set
		{
			base.ZiZhi = ((value > 200) ? 200 : value);
		}
	}

	public new uint wuXin
	{
		get
		{
			return base.wuXin + (uint)getStaticSkillAddSum(7);
		}
		set
		{
			base.wuXin = ((value > 200) ? 200u : value);
		}
	}

	public int ZhuJiJinDu
	{
		get
		{
			return _ZhuJiJinDu;
		}
		set
		{
			_ZhuJiJinDu = value;
			if ((Object)(object)ZhuJiManager.inst != (Object)null)
			{
				ZhuJiManager.inst.updateJinDu();
			}
		}
	}

	public int NowDrawCardNum => (int)jsonData.instance.DrawCardToLevelJsonData[string.Concat((int)level)]["rundDraw"].n;

	public int NowStartCardNum => (int)jsonData.instance.DrawCardToLevelJsonData[string.Concat((int)level)]["StartCard"].n;

	public uint NowCard
	{
		get
		{
			if (BuffSeidFlag.ContainsKey(23))
			{
				return 0u;
			}
			return (uint)Mathf.Clamp((int)jsonData.instance.DrawCardToLevelJsonData[string.Concat((int)level)]["MaxDraw"].n + fightTemp.tempNowCard, 0, 99999);
		}
	}

	public int WuDaoDian
	{
		get
		{
			return _WuDaoDian;
		}
		set
		{
			_WuDaoDian = value;
		}
	}

	public List<int> NowRoundUsedCard
	{
		get
		{
			return fightTemp.NowRoundUsedCard;
		}
		set
		{
			fightTemp.NowRoundUsedCard = value;
		}
	}

	public List<int> UsedSkills
	{
		get
		{
			return fightTemp.UsedSkills;
		}
		set
		{
			fightTemp.UsedSkills = value;
		}
	}

	public int useSkillNum => fightTemp.NowRoundUsedSkills.Count;

	public List<int> HasDefeatNpcList => StreamData.HasDefeatNpcList;

	public int getStaticSkillAddSum(int seid)
	{
		return 0 + DictionyGetSum(StaticSkillSeidFlag, seid) + DictionyGetSum(wuDaoMag.WuDaoSkillSeidFlag, seid) + DictionyGetSum(JieDanSkillSeidFlag, seid);
	}

	public int getEquipAddSum(int seid)
	{
		int num = 0;
		if (!EquipSeidFlag.ContainsKey(seid))
		{
			return 0;
		}
		foreach (KeyValuePair<int, int> item in EquipSeidFlag[seid])
		{
			num += item.Value;
		}
		return num;
	}

	public int getJieDanSkillAddHP()
	{
		int num = 0;
		foreach (SkillItem hasJieDanSkill in hasJieDanSkillList)
		{
			num += GetLeveUpAddHPMax((int)jsonData.instance.JieDanBiao[hasJieDanSkill.itemId.ToString()]["HP"].n);
		}
		int num2 = (int)((float)num * ((float)getStaticSkillAddSum(12) / 100f));
		if (level >= 10)
		{
			num *= 2;
		}
		return num + num2;
	}

	public float getJieDanSkillAddExp()
	{
		int num = 100;
		foreach (SkillItem hasJieDanSkill in hasJieDanSkillList)
		{
			num = ((level < 10) ? (num + (int)jsonData.instance.JieDanBiao[hasJieDanSkill.itemId.ToString()]["EXP"].n) : (num + (int)jsonData.instance.JieDanBiao[hasJieDanSkill.itemId.ToString()]["EXP"].n * 2));
		}
		return (float)num / 100f;
	}

	public Dictionary<int, int> getJieDanAddLingGen()
	{
		Dictionary<int, int> dictionary = new Dictionary<int, int>();
		foreach (SkillItem hasJieDanSkill in hasJieDanSkillList)
		{
			int num = 0;
			JSONObject jSONObject = jsonData.instance.JieDanBiao[hasJieDanSkill.itemId.ToString()];
			foreach (JSONObject item in jSONObject["LinGengType"].list)
			{
				dictionary[item.I] = jSONObject["LinGengZongShu"][num].I;
				num++;
			}
		}
		return dictionary;
	}

	public int GetBaseShenShi()
	{
		return _shengShi + getStaticSkillAddSum(2) + getEquipAddSum(4);
	}

	public int GetBaseDunSu()
	{
		return _dunSu + getStaticSkillAddSum(8) + getEquipAddSum(8);
	}

	public Avatar cloneAvatar()
	{
		return MemberwiseClone() as Avatar;
	}

	public int DictionyGetSum(Dictionary<int, Dictionary<int, int>> seidflag, int seid)
	{
		int num = 0;
		if (!seidflag.ContainsKey(seid))
		{
			return 0;
		}
		foreach (KeyValuePair<int, int> item in seidflag[seid])
		{
			num += item.Value;
		}
		return num;
	}

	public int RandomSeedNext()
	{
		int num = new Random(PlayerEx.Player.RandomSeed.I).Next();
		RandomSeed = new JSONObject(num);
		return num;
	}

	public void AddFriend(int npcId)
	{
		if (npcId != 0)
		{
			if (NpcJieSuanManager.inst.ImportantNpcBangDingDictionary.ContainsKey(npcId))
			{
				npcId = NpcJieSuanManager.inst.ImportantNpcBangDingDictionary[npcId];
			}
			if (!emailDateMag.IsFriend(npcId))
			{
				emailDateMag.cyNpcList.Add(npcId);
			}
		}
	}

	public void AddLingGan(int addNum)
	{
		LingGan += addNum;
		int lingGanMax = GetLingGanMax();
		if (LingGan > lingGanMax)
		{
			LingGan = lingGanMax;
		}
		LunDaoState = GetLunDaoState();
	}

	public void ReduceLingGan(int num)
	{
		LingGan -= num;
		if (LingGan < 0)
		{
			LingGan = 0;
		}
		LunDaoState = GetLunDaoState();
	}

	public int GetLingGanMax()
	{
		return jsonData.instance.LingGanMaxData[GetXinJingLevel().ToString()]["lingGanShangXian"].I;
	}

	public int GetLunDaoState()
	{
		foreach (JSONObject item in jsonData.instance.LingGanLevelData.list)
		{
			if (LingGan >= item["lingGanQuJian"].I)
			{
				return item["id"].I;
			}
		}
		return 0;
	}

	public bool ISStudyDanFan(int id)
	{
		if (!jsonData.instance.LianDanDanFangBiao.HasField(id.ToString()))
		{
			Debug.LogError((object)("丹方出错丹方表ID：" + id));
			return false;
		}
		JSONObject jSONObject = jsonData.instance.LianDanDanFangBiao[id.ToString()];
		List<int> danyao = new List<int>();
		List<int> num = new List<int>();
		getDanYaoTypeAndNum(id, danyao, num);
		if (getDanFang(jSONObject["ItemID"].I, danyao, num) != null)
		{
			return true;
		}
		return false;
	}

	public int ItemSeid27Days()
	{
		if (ItemBuffList.ContainsKey("27") && (bool)ItemBuffList["27"][(object)"start"])
		{
			string s = (string)ItemBuffList["27"][(object)"StartTime"];
			_ = (int)ItemBuffList["27"][(object)"AIType"];
			int months = (int)ItemBuffList["27"][(object)"ContinueTime"];
			DateTime startTime = DateTime.Parse(s);
			DateTime nowTime = worldTimeMag.getNowTime();
			DateTime dateTime = startTime.AddMonths(months);
			if (Tools.instance.IsInTime(nowTime, startTime, dateTime))
			{
				return (dateTime - nowTime).Days;
			}
			ItemBuffList["27"][(object)"start"] = JToken.op_Implicit(false);
		}
		return 0;
	}

	public void SetMenPaiHaoGandu(int MenPaiID, int Value)
	{
		int num = (MenPaiHaoGanDu.HasField(string.Concat(MenPaiID)) ? ((int)MenPaiHaoGanDu[string.Concat(MenPaiID)].n) : 0);
		MenPaiHaoGanDu.SetField(string.Concat(MenPaiID), num + Value);
		if (Value > 0)
		{
			UIPopTip.Inst.Pop($"你在{ShiLiHaoGanDuName.DataDict[MenPaiID].ChinaText}的声望提升了{Value}", PopTipIconType.上箭头);
		}
		else if (Value < 0)
		{
			UIPopTip.Inst.Pop($"你在{ShiLiHaoGanDuName.DataDict[MenPaiID].ChinaText}的声望下降了{-Value}", PopTipIconType.下箭头);
		}
	}

	public void setAvatarHaoGandu(int AvatarID, int AddHaoGanduNum)
	{
		NPCEx.AddFavor(AvatarID, AddHaoGanduNum);
	}

	public void getDanYaoTypeAndNum(int id, List<int> danyao, List<int> num)
	{
		JSONObject jSONObject = jsonData.instance.LianDanDanFangBiao[id.ToString()];
		for (int i = 1; i <= 5; i++)
		{
			danyao.Add((int)jSONObject["value" + i].n);
			num.Add((int)jSONObject["num" + i].n);
		}
	}

	public JSONObject getDanFang(int danyaoID, List<int> danyao, List<int> num)
	{
		return Tools.instance.getPlayer().DanFang.list.Find(delegate(JSONObject aa)
		{
			if (danyaoID == aa["ID"].I)
			{
				bool flag = true;
				for (int i = 0; i < aa["Type"].list.Count; i++)
				{
					if (danyao[i] != aa["Type"][i].I)
					{
						flag = false;
					}
					if (num[i] != aa["Num"][i].I)
					{
						flag = false;
					}
				}
				if (flag)
				{
					return true;
				}
			}
			return false;
		});
	}

	public int GetZhangMenChengHaoId(int menpai)
	{
		switch (menpai)
		{
		case 1:
			return 9;
		case 3:
			return 109;
		case 4:
			return 209;
		case 5:
			return 309;
		case 6:
			return 409;
		default:
			Debug.LogError((object)$"不存在该门派{menpai}");
			return 9;
		}
	}

	public int GetZhangMenId(int shili)
	{
		int zhangMenChengHaoId = Tools.instance.getPlayer().GetZhangMenChengHaoId(shili);
		int num = 0;
		foreach (JSONObject item in jsonData.instance.AvatarJsonData.list)
		{
			num = item["id"].I;
			if (num >= 20000 && item["ChengHaoID"].I == zhangMenChengHaoId)
			{
				return num;
			}
		}
		Debug.LogError((object)$"不存在当前势力的掌门，势力Id{shili}");
		return -1;
	}

	public void AddDandu(int num)
	{
		Dandu += (TianFuID.HasField(string.Concat(18)) ? (num * 2) : num);
		if (Dandu >= 120)
		{
			UIDeath.Inst.Show(DeathType.毒发身亡);
		}
	}

	public void AddYaoCaiShuXin(int itemID, int index)
	{
		if (!YaoCaiShuXin.HasField(itemID + "_" + index))
		{
			switch (index)
			{
			case 1:
				TuJianManager.Inst.UnlockYaoYin(itemID);
				break;
			case 2:
				TuJianManager.Inst.UnlockZhuYao(itemID);
				break;
			case 3:
				TuJianManager.Inst.UnlockFuYao(itemID);
				break;
			}
			YaoCaiShuXin.AddField(itemID + "_" + index, 1);
		}
	}

	public bool hasYaocaiShuXin(int itemID, int index)
	{
		return YaoCaiShuXin.HasField(itemID + "_" + index);
	}

	public bool getItemHasTianFu15(int quality)
	{
		if (TianFuID.HasField(string.Concat(15)))
		{
			return TianFuID["15"].list.Find((JSONObject aa) => (int)aa.n == quality) != null;
		}
		return false;
	}

	public int getZhuXiuSkill()
	{
		foreach (SkillItem equipStaticSkill in equipStaticSkillList)
		{
			if (equipStaticSkill.itemIndex == 0)
			{
				return Tools.instance.getStaticSkillKeyByID(equipStaticSkill.itemId);
			}
		}
		return -1;
	}

	public bool GetHasYaoYinShuXin(int itemID, int quality)
	{
		bool itemHasTianFu = getItemHasTianFu15(quality);
		return hasYaocaiShuXin(itemID, 1) || itemHasTianFu;
	}

	public bool GetHasZhuYaoShuXin(int itemID, int quality)
	{
		bool itemHasTianFu = getItemHasTianFu15(quality);
		return hasYaocaiShuXin(itemID, 2) || itemHasTianFu;
	}

	public bool GetHasFuYaoShuXin(int itemID, int quality)
	{
		bool itemHasTianFu = getItemHasTianFu15(quality);
		return hasYaocaiShuXin(itemID, 3) || itemHasTianFu;
	}

	public void UnLockCaoYaoData(int caoYaoId)
	{
		AddYaoCaiShuXin(caoYaoId, 1);
		AddYaoCaiShuXin(caoYaoId, 2);
		AddYaoCaiShuXin(caoYaoId, 3);
	}

	public void addDanFang(int danyaoID, List<int> yaolei, List<int> YaoLeiNum)
	{
		TuJianManager.Inst.UnlockItem(danyaoID);
		for (int i = 0; i < yaolei.Count; i++)
		{
			if (yaolei[i] <= 0)
			{
				yaolei[i] = 0;
				YaoLeiNum[i] = 0;
			}
		}
		if (DanFang.list.Find(delegate(JSONObject aa)
		{
			if (danyaoID == aa["ID"].I)
			{
				bool flag = true;
				for (int j = 0; j < aa["Type"].list.Count; j++)
				{
					if (yaolei[j] != aa["Type"][j].I)
					{
						flag = false;
					}
					if (YaoLeiNum[j] != (int)aa["Num"][j].n)
					{
						flag = false;
					}
				}
				if (flag)
				{
					return true;
				}
			}
			return false;
		}) != null)
		{
			return;
		}
		JSONObject jSONObject = new JSONObject(JSONObject.Type.OBJECT);
		JSONObject jSONObject2 = new JSONObject(JSONObject.Type.ARRAY);
		JSONObject jSONObject3 = new JSONObject(JSONObject.Type.ARRAY);
		foreach (int item in yaolei)
		{
			jSONObject2.Add(item);
		}
		foreach (int item2 in YaoLeiNum)
		{
			jSONObject3.Add(item2);
		}
		jSONObject.AddField("ID", danyaoID);
		jSONObject.AddField("Type", jSONObject2);
		jSONObject.AddField("Num", jSONObject3);
		DanFang.Add(jSONObject);
	}

	public void statiReduceDandu(int num)
	{
		Dandu -= num;
		if (Dandu < 0)
		{
			Dandu = 0;
		}
	}

	public void AddMoney(int AddNum)
	{
		if (AddNum != 0)
		{
			int num = (int)money + AddNum;
			if (AddNum >= 0)
			{
				UIPopTip.Inst.Pop(Tools.getStr("AddMoney1").Replace("{X}", AddNum.ToString()), PopTipIconType.上箭头);
			}
			else
			{
				UIPopTip.Inst.Pop(Tools.getStr("AddMoney2").Replace("{X}", (-AddNum).ToString()), PopTipIconType.下箭头);
			}
			if (num >= 0)
			{
				money = (uint)num;
			}
			else
			{
				money = 0uL;
			}
		}
	}

	public void ReduceDandu(int num)
	{
		int danDuLevel = GetDanDuLevel();
		int num2 = 0;
		if (danDuLevel < 2)
		{
			num2 = 2;
		}
		else if (danDuLevel < 3)
		{
			num2 = 1;
		}
		else if (danDuLevel >= 3)
		{
			num2 = 0;
		}
		Dandu -= num * num2;
		Dandu -= num * getStaticSkillAddSum(11);
		if (Dandu < 0)
		{
			Dandu = 0;
		}
	}

	public int GetDanDuLevel()
	{
		if (Dandu >= 120)
		{
			return 5;
		}
		if (Dandu >= 100)
		{
			return 4;
		}
		if (Dandu >= 70)
		{
			return 3;
		}
		if (Dandu >= 50)
		{
			return 2;
		}
		if (Dandu >= 20)
		{
			return 1;
		}
		return 0;
	}

	public int GetXinJingLevel()
	{
		foreach (JSONObject item in jsonData.instance.XinJinJsonData.list)
		{
			if (item["Max"].I > xinjin)
			{
				return item["id"].I;
			}
		}
		return jsonData.instance.XinJinJsonData.Count;
	}

	public int getXinJinGuanlianType()
	{
		int xinJingLevel = GetXinJingLevel();
		int levelType = getLevelType();
		if (xinJingLevel - levelType == 1)
		{
			return 2;
		}
		if (xinJingLevel - levelType < 1)
		{
			return 1;
		}
		return 3;
	}

	public int getLevelType()
	{
		return (level - 1) / 3 + 1;
	}

	public void setSkillConfigIndex(int index)
	{
		nowConfigEquipSkill = index;
		equipSkillList = configEquipSkill[index];
	}

	public void setStatikConfigIndex(int index)
	{
		nowConfigEquipStaticSkill = index;
		equipStaticSkillList = configEquipStaticSkill[index];
	}

	public void setItemConfigIndex(int index)
	{
		configEquipItem[nowConfigEquipItem].values.Clear();
		equipItemList.values.ForEach(delegate(ITEM_INFO i)
		{
			configEquipItem[nowConfigEquipItem].values.Add(i);
		});
		nowConfigEquipItem = index;
		equipItemList.values = new List<ITEM_INFO>();
		foreach (ITEM_INFO value in configEquipItem[index].values)
		{
			if (value.itemId != 0)
			{
				ITEM_INFO item = FindItemByUUID(value.uuid);
				equipItemList.values.Add(item);
			}
		}
		PlayerBeiBaoManager.inst.restartEquips();
		Singleton.inventory.LoadInventory();
	}

	public void addHasSkillList(int SkillId)
	{
		foreach (SkillItem hasSkill in hasSkillList)
		{
			if (hasSkill.itemId == SkillId)
			{
				return;
			}
		}
		SkillItem skillItem = new SkillItem();
		skillItem.itemId = SkillId;
		hasSkillList.Add(skillItem);
		if (isPlayer())
		{
			TuJianManager.Inst.UnlockSkill(SkillId);
		}
	}

	public void addHasStaticSkillList(int SkillId, int _level = 1)
	{
		foreach (SkillItem hasStaticSkill in hasStaticSkillList)
		{
			if (hasStaticSkill.itemId == SkillId)
			{
				return;
			}
		}
		SkillItem skillItem = new SkillItem();
		skillItem.itemId = SkillId;
		skillItem.level = _level;
		hasStaticSkillList.Add(skillItem);
		if (isPlayer())
		{
			TuJianManager.Inst.UnlockGongFa(SkillId);
		}
	}

	public void addJieDanSkillList(int SkillId)
	{
		foreach (SkillItem hasJieDanSkill in hasJieDanSkillList)
		{
			if (hasJieDanSkill.itemId == SkillId)
			{
				return;
			}
		}
		SkillItem skillItem = new SkillItem();
		skillItem.itemId = SkillId;
		hasJieDanSkillList.Add(skillItem);
	}

	public void MonstarEndRound()
	{
		RoundManager.instance.autoRemoveCard(this);
		Event.fireOut("endRound", this);
	}

	public void AvatarEndRound()
	{
		if (isPlayer())
		{
			RoundManager.instance.PlayerEndRound();
		}
		else
		{
			MonstarEndRound();
		}
	}

	public void joinMenPai(int menPaiID)
	{
		menPai = (ushort)menPaiID;
	}

	public void AllMapAddHP(int num, DeathType Type = DeathType.身死道消)
	{
		HP += num;
		if (HP > HP_Max)
		{
			HP = HP_Max;
		}
		if (HP <= 0)
		{
			UIDeath.Inst.Show(Type);
		}
	}

	public void AllMapAddHPMax(int num)
	{
		_HP_Max += num;
	}

	public void addShenShi(int num)
	{
		shengShi = (int)ADDIntToUint((uint)_shengShi, num);
	}

	public void addShoYuan(int num)
	{
		shouYuan = ADDIntToUint(shouYuan, num);
	}

	public void addShaQi(int num)
	{
		shaQi = ADDIntToUint(shaQi, num);
	}

	public void addZiZhi(int num)
	{
		ZiZhi = (int)ADDIntToUint((uint)base.ZiZhi, num);
	}

	public void addWuXin(int num)
	{
		wuXin = ADDIntToUint(base.wuXin, num);
	}

	public uint ADDIntToUint(uint real, int value)
	{
		int num = (int)real + value;
		real = ((num >= 0) ? ((uint)num) : 0u);
		return real;
	}

	public void AddTime(int addday, int addMonth = 0, int Addyear = 0)
	{
		DateTime nowTime = worldTimeMag.getNowTime();
		DateTime dateTime = worldTimeMag.addTime(addday, addMonth, Addyear);
		zulinContorl.addTime(addday, addMonth, Addyear);
		RefreshSeaBossData();
		int num = (dateTime.Year - nowTime.Year) * 12 + (dateTime.Month - nowTime.Month);
		int num2 = dateTime.Year - nowTime.Year;
		if (dateTime.Year - nowTime.Year > 0)
		{
			age += (uint)(dateTime.Year - nowTime.Year);
		}
		if (age > shouYuan)
		{
			UIDeath.Inst.Show(DeathType.寿元已尽);
			return;
		}
		if (num > 0)
		{
			if (equipStaticSkillList != null)
			{
				float num3 = getTimeExpSpeed() * (float)num;
				addEXP((int)num3);
			}
			AddLingGan(num);
			DongFuManager.LingTianAddTime(num);
		}
		if (num2 > 0)
		{
			ReduceDandu(num2);
			try
			{
				chenghaomag.TimeAddMoney(num2);
				randomFuBenMag.AutoSetRandomFuBen();
			}
			catch (Exception ex)
			{
				Debug.LogError((object)ex);
			}
		}
		try
		{
			Loom.RunAsync(delegate
			{
				lock (LockMag.updateTask)
				{
					nomelTaskMag.restAllTaskType();
					nomelTaskMag.ResetAllStaticNTask();
					seaNodeMag.SetLuanLiuLv();
					Loom.QueueOnMainThread(delegate
					{
						GaoShiManager.OnAddTime();
					}, null);
				}
			});
			wuDaoMag.AutoReomveLingGuang();
			if (taskMag._TaskData.Count > 0)
			{
				JSONObject jSONObject = taskMag._TaskData["Task"];
				foreach (string key in jSONObject.keys)
				{
					if (CyRandomTaskData.DataDict.ContainsKey(int.Parse(key)))
					{
						continue;
					}
					bool flag = false;
					if ((jSONObject[key].HasField("disableTask") && jSONObject[key]["disableTask"].b) || !jSONObject[key].HasField("curTime"))
					{
						continue;
					}
					DateTime startTime = DateTime.Parse(jSONObject[key]["curTime"].str);
					if (jSONObject[key]["continueTime"].I > 0)
					{
						DateTime endTime = startTime.AddMonths(jSONObject[key]["continueTime"].I);
						if (!Tools.instance.IsInTime(worldTimeMag.getNowTime(), startTime, endTime))
						{
							jSONObject[key].SetField("disableTask", val: true);
							flag = true;
						}
					}
					if (!flag && jSONObject[key].HasField("EndTime") && !Tools.instance.IsInTime(worldTimeMag.nowTime, jSONObject[key]["EndTime"].str))
					{
						jSONObject[key].SetField("disableTask", val: true);
					}
				}
				StreamData.TaskMag.CheckHasOut();
			}
		}
		catch (Exception ex2)
		{
			Debug.LogError((object)ex2);
		}
		if ((Object)(object)MapNodeManager.inst != (Object)null)
		{
			MapNodeManager.inst.UpdateAllNode();
		}
		updateChuanYingFu();
		StreamData.PaiMaiDataMag.AuToUpDate();
	}

	public void RefreshSeaBossData()
	{
		foreach (SeaHaiYuJiZhiShuaXin data in SeaHaiYuJiZhiShuaXin.DataList)
		{
			if (!EndlessSeaBoss.HasField(data.id.ToString()))
			{
				JSONObject jSONObject = new JSONObject(JSONObject.Type.OBJECT);
				jSONObject.SetField("CD", 0);
				jSONObject.SetField("LastTime", worldTimeMag.nowTime);
				EndlessSeaBoss.SetField(data.id.ToString(), jSONObject);
			}
			JSONObject jSONObject2 = EndlessSeaBoss[data.id.ToString()];
			DateTime dateTime = DateTime.Parse(jSONObject2["LastTime"].str);
			DateTime nowTime = worldTimeMag.getNowTime();
			int i = jSONObject2["CD"].I;
			if (nowTime >= dateTime.AddYears(i))
			{
				JSONObject jSONObject3 = new JSONObject(JSONObject.Type.OBJECT);
				int num = Random.Range(data.CD[0], data.CD[1]);
				jSONObject3.SetField("CD", num);
				jSONObject3.SetField("LastTime", worldTimeMag.nowTime);
				int num2 = data.ID[RandomSeedNext() % data.ID.Count];
				jSONObject3.SetField("JiZhiID", num2);
				SeaJiZhiID seaJiZhiID = SeaJiZhiID.DataDict[num2];
				if (seaJiZhiID.Type == 0)
				{
					int val = seaJiZhiID.AvatarID[RandomSeedNext() % seaJiZhiID.AvatarID.Count];
					jSONObject3.SetField("AvatarID", val);
				}
				int num3 = data.WeiZhi[RandomSeedNext() % data.WeiZhi.Count];
				jSONObject3.SetField("Pos", num3);
				jSONObject3.SetField("Close", val: false);
				EndlessSeaBoss.SetField(data.id.ToString(), jSONObject3);
				if (seaJiZhiID.Type == 0)
				{
					Debug.Log((object)string.Format("海域{0}坐标{1}刷新了boss{2}，机制ID{3}，刷新时间{4}，刷新CD{5}", data.id, num3, jSONObject3["AvatarID"].I, seaJiZhiID.id, worldTimeMag.nowTime, num));
				}
				else if (seaJiZhiID.Type == 1)
				{
					Debug.Log((object)$"海域{data.id}坐标{num3}刷新了副本{seaJiZhiID.FuBenType}，机制ID{seaJiZhiID.id}，刷新时间{worldTimeMag.nowTime}，刷新CD{num}");
				}
			}
		}
	}

	public float GetShenShiArea()
	{
		return Mathf.Pow((float)shengShi, 0.2f) / 2f + 0.1f * (float)shengShi;
	}

	public void updateChuanYingFu()
	{
		if (emailDateMag.IsStopAll)
		{
			return;
		}
		string nowTime = worldTimeMag.nowTime;
		int num = level;
		int num2 = 0;
		try
		{
			List<string> list = new List<string>();
			for (int i = 0; i < ToalChuanYingFuList.Count; i++)
			{
				int i2 = ToalChuanYingFuList[i]["AvatarID"].I;
				if (ToalChuanYingFuList[i]["IsAlive"].I == 1 && NPCEx.NPCIDToNew(i2) < 20000)
				{
					continue;
				}
				if (ToalChuanYingFuList[i]["NPCLevel"].Count > 0)
				{
					int i3 = ToalChuanYingFuList[i]["NPCLevel"][0].I;
					int i4 = ToalChuanYingFuList[i]["NPCLevel"][1].I;
					int num3 = NPCEx.NPCIDToNew(i2);
					if (num3 < 20000)
					{
						continue;
					}
					int i5 = jsonData.instance.AvatarJsonData[num3.ToString()]["Level"].I;
					if (i5 < i3 || i5 > i4)
					{
						continue;
					}
				}
				if (ToalChuanYingFuList[i]["StarTime"].str != "" && ToalChuanYingFuList[i]["StarTime"].str != null)
				{
					string str = ToalChuanYingFuList[i]["StarTime"].str;
					if (ToalChuanYingFuList[i]["EndTime"].str != "" && ToalChuanYingFuList[i]["EndTime"].str != null)
					{
						string str2 = ToalChuanYingFuList[i]["EndTime"].str;
						if (!Tools.instance.IsInTime(nowTime, str, str2))
						{
							continue;
						}
					}
					else if (DateTime.Parse(nowTime) < DateTime.Parse(str))
					{
						continue;
					}
				}
				if (ToalChuanYingFuList[i]["Level"].Count > 0 && (num < ToalChuanYingFuList[i]["Level"][0].I || num > ToalChuanYingFuList[i]["Level"][1].I))
				{
					continue;
				}
				if (ToalChuanYingFuList[i]["HaoGanDu"].I > 0)
				{
					int num4 = NPCEx.NPCIDToNew(ToalChuanYingFuList[i]["AvatarID"].I);
					if (jsonData.instance.AvatarRandomJsonData[num4.ToString()]["HaoGanDu"].I <= ToalChuanYingFuList[i]["HaoGanDu"].I)
					{
						continue;
					}
				}
				if (ToalChuanYingFuList[i]["EventValue"].Count > 0)
				{
					string str3 = ToalChuanYingFuList[i]["fuhao"].str;
					int i6 = ToalChuanYingFuList[i]["EventValue"][0].I;
					int i7 = ToalChuanYingFuList[i]["EventValue"][1].I;
					int num5 = GlobalValue.Get(i6, "Avatar.updateChuanYingFu");
					if (str3 == "=")
					{
						if (num5 != i7)
						{
							continue;
						}
					}
					else if (str3 == ">")
					{
						if (num5 <= i7)
						{
							continue;
						}
					}
					else if (num5 >= i7)
					{
						continue;
					}
				}
				if (ToalChuanYingFuList[i]["IsOnly"].I == 1)
				{
					list.Add(ToalChuanYingFuList[i]["id"].I.ToString());
				}
				else if (ToalChuanYingFuList[i]["IsOnly"].I == 2 && nomelTaskMag.IsNTaskStart(ToalChuanYingFuList[i]["WeiTuo"].I))
				{
					continue;
				}
				chuanYingManager.addChuanYingFu(ToalChuanYingFuList[i]["id"].I);
			}
			for (int j = 0; j < list.Count; j++)
			{
				HasSendChuanYingFuList.SetField(list[j], ToalChuanYingFuList[list[j]]);
				ToalChuanYingFuList.RemoveField(list[j]);
			}
			list = new List<string>();
			string text = "";
			for (int k = 0; k < NoGetChuanYingList.Count; k++)
			{
				text = NoGetChuanYingList[k]["sendTime"].str;
				if (!(DateTime.Parse(text) <= DateTime.Parse(nowTime)))
				{
					continue;
				}
				int i8 = NoGetChuanYingList[k]["id"].I;
				list.Add(NoGetChuanYingList[k]["id"].I.ToString());
				NewChuanYingList.SetField(NoGetChuanYingList[k]["id"].I.ToString(), NoGetChuanYingList[k]);
				emailDateMag.OldToPlayer(NewChuanYingList[i8.ToString()]["AvatarID"].I, i8, text);
				if (NoGetChuanYingList.HasField("IsAdd") && NoGetChuanYingList[k]["IsAdd"].I == 1)
				{
					int i9 = NoGetChuanYingList[k]["WeiTuo"].I;
					if (!nomelTaskMag.IsNTaskStart(i9))
					{
						nomelTaskMag.StartNTask(i9, 0);
						UIPopTip.Inst.Pop("获得一条新的委托任务", PopTipIconType.任务进度);
					}
				}
			}
			if (list.Count > 0)
			{
				chuanYingManager.NewTipsSum = list.Count;
				for (int l = 0; l < list.Count; l++)
				{
					NoGetChuanYingList.SetField(list[l], ToalChuanYingFuList[list[l]]);
					ToalChuanYingFuList.RemoveField(list[l]);
				}
			}
		}
		catch (Exception ex)
		{
			Debug.Log((object)ex);
			Debug.LogError((object)$"{num2}");
		}
	}

	public void setMonstarDeath()
	{
		int num = 0;
		List<int> list = new List<int>();
		for (int i = 0; i < jsonData.instance.AvatarRandomJsonData.Count; i++)
		{
			if (num == 0)
			{
				num++;
				continue;
			}
			string text = jsonData.instance.AvatarRandomJsonData.keys[i];
			if (int.Parse(text) >= 20000 || !jsonData.instance.AvatarJsonData.HasField(text))
			{
				continue;
			}
			int num2 = (int)jsonData.instance.AvatarJsonData[text]["shouYuan"].n;
			if (num2 > 5000)
			{
				num++;
				continue;
			}
			try
			{
				if (DateTime.Parse(jsonData.instance.AvatarRandomJsonData[i]["BirthdayTime"].str).AddYears(num2) < worldTimeMag.getNowTime())
				{
					int.Parse(text);
					list.Add(int.Parse(text));
				}
			}
			catch (Exception)
			{
				UIPopTip.Inst.Pop("设置NPC死亡出现错误，重置NPC数据以解决问题。");
				break;
			}
			num++;
		}
		for (int j = 0; j < list.Count; j++)
		{
			jsonData.instance.setMonstarDeath(list[j], isNeed: false);
		}
	}

	public GameObject createCanvasDeath()
	{
		Object obj = Resources.Load("uiPrefab/CanvasDeath");
		return Object.Instantiate<GameObject>((GameObject)(object)((obj is GameObject) ? obj : null));
	}

	public float AddZiZhiSpeed(float speed)
	{
		return speed * ((float)ZiZhi / 100f);
	}

	public float getTimeExpSpeed()
	{
		int staticID = getStaticID();
		if (staticID != 0)
		{
			float n = jsonData.instance.StaticSkillJsonData[string.Concat(staticID)]["Skill_Speed"].n;
			float num = (n + AddZiZhiSpeed(n)) * getJieDanSkillAddExp();
			if (TianFuID.HasField(string.Concat(12)))
			{
				float num2 = TianFuID["12"].n / 100f;
				num += num * num2;
			}
			return num;
		}
		return 0f;
	}

	public int getStaticID()
	{
		foreach (SkillItem equipStaticSkill in equipStaticSkillList)
		{
			if (equipStaticSkill.itemIndex == 0)
			{
				return Tools.instance.getStaticSkillKeyByID(equipStaticSkill.itemId);
			}
		}
		return 0;
	}

	public int getStaticDunSu()
	{
		foreach (SkillItem equipStaticSkill in equipStaticSkillList)
		{
			if (equipStaticSkill.itemIndex == 5)
			{
				return Tools.instance.getStaticSkillKeyByID(equipStaticSkill.itemId);
			}
		}
		return 0;
	}

	public void addEXP(int num)
	{
		if (num < 0 && (ulong)(-num) > exp)
		{
			num = -(int)exp;
		}
		exp += (ulong)num;
		if (jsonData.instance.LevelUpDataJsonData[string.Concat(level)] == null)
		{
			return;
		}
		if (exp >= (ulong)jsonData.instance.LevelUpDataJsonData[string.Concat(level)]["MaxExp"].n && level % 3 != 0)
		{
			exp -= (ulong)jsonData.instance.LevelUpDataJsonData[string.Concat(level)]["MaxExp"].n;
			levelUp();
			if (jsonData.instance.LevelUpDataJsonData[string.Concat(level)] != null && exp >= (ulong)jsonData.instance.LevelUpDataJsonData[string.Concat(level)]["MaxExp"].n)
			{
				addEXP(0);
			}
		}
		else if (exp >= (ulong)jsonData.instance.LevelUpDataJsonData[string.Concat(level)]["MaxExp"].n && level % 3 == 0)
		{
			exp = (ulong)jsonData.instance.LevelUpDataJsonData[string.Concat(level)]["MaxExp"].n;
			if (showTupo == 0)
			{
				ResManager.inst.LoadPrefab("PingJingTips").Inst().GetComponent<PingJingUIMag>()
					.Show();
				showTupo = 1;
			}
			else if (Tools.instance.ShowPingJin)
			{
				UIPopTip.Inst.Pop("你已经到了瓶颈，无法获取经验");
				Tools.instance.ShowPingJin = false;
			}
		}
	}

	public void AddHp(int addNum)
	{
		HP += addNum;
		if (HP > HP_Max)
		{
			HP = HP_Max;
		}
		if (HP <= 0)
		{
			die();
		}
	}

	public int GetLeveUpAddHPMax(int addHpNum)
	{
		if (!TianFuID.HasField(string.Concat(13)))
		{
			return addHpNum;
		}
		return addHpNum + (int)((float)addHpNum * (TianFuID["13"].n / 100f));
	}

	public int GetTianFuAddCaoYaoCaiJi(int num)
	{
		int num2 = num;
		if (TianFuID.HasField(string.Concat(21)))
		{
			num2 = num + (int)((float)num * (TianFuID["21"].n / 100f));
		}
		return num2 + (int)((float)num2 * ((float)getStaticSkillAddSum(10) / 100f));
	}

	public void levelUp()
	{
		JSONObject jSONObject = jsonData.instance.LevelUpDataJsonData[string.Concat(level)];
		if (jSONObject == null)
		{
			return;
		}
		int hP_Max = HP_Max;
		int oldShenShi = shengShi;
		uint oldShouYuan = shouYuan;
		int oldDunSu = dunSu;
		level++;
		_HP_Max += GetLeveUpAddHPMax((int)jSONObject["AddHp"].n);
		HP = HP_Max;
		_shengShi += (int)jSONObject["AddShenShi"].n;
		_dunSu += (int)jSONObject["AddDunSu"].n;
		shouYuan += (uint)jSONObject["AddShouYuan"].n;
		bool isBigTuPo = false;
		if (level > 1 && level % 3 == 1)
		{
			UnlockShenXianDouFa(level / 3 - 1);
			Dandu = 0;
			if (TianFuID.HasField("22"))
			{
				for (int i = 1; i <= jsonData.instance.CrateAvatarSeidJsonData[22][TianFuID[22.ToString()].I].keys.Count - 1; i++)
				{
					if (level == jsonData.instance.CrateAvatarSeidJsonData[22][TianFuID[22.ToString()].I]["value" + i][0].I)
					{
						WuDaoDian += jsonData.instance.CrateAvatarSeidJsonData[22][TianFuID[22.ToString()].I]["value" + i][1].I;
						break;
					}
				}
			}
			isBigTuPo = true;
			AddLingGan(100);
		}
		else
		{
			Dandu -= 20;
			if (Dandu < 0)
			{
				Dandu = 0;
			}
			AddLingGan(50);
		}
		string desc = "\u3000\u3000周边天地的灵气突然开始涌入你的体内，你感到体内的真元犹如沸腾的开水一般，迅速流动起来。灵气的波动足足持续了一个时辰才平息下来，你终于冲破瓶颈，境界提升至" + LevelUpDataJsonData.DataDict[level].Name;
		ResManager.inst.LoadPrefab("LevelUpPanel").Inst().GetComponent<TuPoUIMag>()
			.ShowTuPo(level, hP_Max, HP, oldShenShi, shengShi, (int)oldShouYuan, (int)shouYuan, oldDunSu, dunSu, isBigTuPo, desc);
	}

	public void AllMapSetNode()
	{
		foreach (JSONObject item in jsonData.instance.AllMapLuDainType.list)
		{
			resetNode(item);
		}
	}

	public void ResetAllEndlessNode()
	{
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Expected O, but got Unknown
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Expected O, but got Unknown
		int num = 1;
		foreach (JToken item in (IEnumerable<JToken>)EndlessSea["SafeLv"])
		{
			_ = item;
			if (!EndlessSeaRandomNode.ContainsKey(string.Concat(num)))
			{
				EndlessSeaRandomNode[string.Concat(num)] = (JToken)new JObject();
			}
			ResetEndlessNode((JObject)EndlessSeaRandomNode[string.Concat(num)], num);
			num++;
		}
	}

	public int GetDaHaiIDBySeaID(int SeaID)
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Expected O, but got Unknown
		foreach (KeyValuePair<string, JToken> endlessSeaHaiYuDatum in jsonData.instance.EndlessSeaHaiYuData)
		{
			if (Tools.ContensInt((JArray)endlessSeaHaiYuDatum.Value[(object)"shuxing"], SeaID))
			{
				return (int)endlessSeaHaiYuDatum.Value[(object)"id"];
			}
		}
		return -1;
	}

	public void ResetEndlessNode(JObject seaNode, int SeaID)
	{
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Expected O, but got Unknown
		if (seaNode.ContainsKey("resetTime"))
		{
			DateTime startTime = DateTime.Parse((string)seaNode["resetTime"]);
			DateTime endTime = startTime.AddMonths((int)seaNode["CD"]);
			if (Tools.instance.IsInTime(worldTimeMag.getNowTime(), startTime, endTime))
			{
				return;
			}
		}
		FuBenMap fuBenMap = new FuBenMap(7, 7);
		JObject endlessSeaNPCGouChengData = jsonData.instance.EndlessSeaNPCGouChengData;
		int Sealeve = seaNodeMag.GetSeaIDLV(SeaID);
		List<JToken> list = Tools.FindAllJTokens((JToken)(object)endlessSeaNPCGouChengData, (JToken aa) => ((int)aa[(object)"qujian"][(object)0] <= Sealeve && Sealeve <= (int)aa[(object)"qujian"][(object)1]) ? true : false);
		JArray val = new JArray();
		foreach (JToken item in list)
		{
			List<int> list2 = SetSeaNodeList(item, SeaID);
			for (int i = 0; i < list2.Count; i++)
			{
				int num = jsonData.GetRandom() % 7;
				int num2 = jsonData.GetRandom() % 7;
				JObject val2 = CreateSeaMonstar(SeaID, list2[i], fuBenMap.mapIndex[num, num2]);
				val.Add((JToken)(object)val2);
			}
		}
		seaNode["Monstar"] = (JToken)(object)val;
		seaNode["resetTime"] = JToken.op_Implicit(worldTimeMag.nowTime);
		JToken val3 = jsonData.instance.EndlessSeaSafeLvData[Sealeve.ToString()][(object)"resetTime"];
		seaNode["CD"] = JToken.op_Implicit(Tools.getRandomInt((int)val3[(object)0], (int)val3[(object)1]));
	}

	public List<int> SetSeaNodeList(JToken _cc, int SeaID)
	{
		int randomInt = Tools.getRandomInt((int)_cc[(object)"max"][(object)0], (int)_cc[(object)"max"][(object)1]);
		int Type = (int)_cc[(object)"Type"];
		JObject endlessSeaNPCData = jsonData.instance.EndlessSeaNPCData;
		int dahaiyu = GetDaHaiIDBySeaID(SeaID);
		List<JToken> list = Tools.FindAllJTokens((JToken)(object)endlessSeaNPCData, delegate(JToken aa)
		{
			//IL_0025: Unknown result type (might be due to invalid IL or missing references)
			//IL_0035: Expected O, but got Unknown
			//IL_0044: Unknown result type (might be due to invalid IL or missing references)
			//IL_0186: Unknown result type (might be due to invalid IL or missing references)
			if ((int)aa[(object)"EventType"] != Type)
			{
				return false;
			}
			if (!Tools.ContensInt((JArray)aa[(object)"shuxing"], dahaiyu))
			{
				return false;
			}
			if (((JContainer)(JArray)aa[(object)"EventValue"]).Count > 0 && !ManZuValue((int)aa[(object)"EventValue"][(object)0], (int)aa[(object)"EventValue"][(object)1], (string)aa[(object)"fuhao"]))
			{
				return false;
			}
			int seaIDLV = seaNodeMag.GetSeaIDLV(SeaID);
			if ((int)aa[(object)"EventLv"][(object)0] > seaIDLV || seaIDLV > (int)aa[(object)"EventLv"][(object)1])
			{
				return false;
			}
			if ((int)aa[(object)"NowSeaOnce"] == 1)
			{
				foreach (JToken item in (IEnumerable<JToken>)jsonData.instance.EndlessSeaHaiYuData[dahaiyu.ToString()][(object)"shuxing"])
				{
					if (EndlessSeaRandomNode.ContainsKey(string.Concat((int)item)) && ((JObject)EndlessSeaRandomNode[string.Concat((int)item)]).ContainsKey("Monstar"))
					{
						foreach (JToken item2 in (IEnumerable<JToken>)EndlessSeaRandomNode[string.Concat((int)item)][(object)"Monstar"])
						{
							if ((int)item2[(object)"monstarId"] == (int)aa[(object)"id"])
							{
								return false;
							}
						}
					}
				}
			}
			return true;
		});
		List<int> list2 = new List<int>();
		int num = 0;
		if (list.Count == 0)
		{
			return list2;
		}
		for (int i = 0; i < randomInt; i++)
		{
			if (num >= 100)
			{
				break;
			}
			JToken randomListByPercent = Tools.instance.getRandomListByPercent(list, "percent");
			if ((int)randomListByPercent[(object)"NowSeaOnce"] == 1 && list2.Contains((int)randomListByPercent[(object)"id"]))
			{
				i--;
				num++;
			}
			else
			{
				list2.Add((int)randomListByPercent[(object)"id"]);
			}
		}
		return list2;
	}

	public JObject CreateSeaMonstar(int seaId, int monstarID, int index)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Expected O, but got Unknown
		return new JObject
		{
			["uuid"] = JToken.op_Implicit(Tools.getUUID()),
			["monstarId"] = JToken.op_Implicit(monstarID),
			["index"] = JToken.op_Implicit(index),
			["StartTime"] = JToken.op_Implicit(worldTimeMag.nowTime)
		};
	}

	public void resetNode(JSONObject node)
	{
		if (!AllMapRandomNode.HasField(string.Concat(node["id"].I)))
		{
			JSONObject jSONObject = new JSONObject(JSONObject.Type.OBJECT);
			jSONObject.AddField("resetTime", "0001-01-01");
			jSONObject.AddField("Type", -1);
			jSONObject.AddField("EventId", 0);
			jSONObject.AddField("Reset", val: true);
			AllMapRandomNode.AddField(string.Concat(node["id"].I), jSONObject);
		}
		Avatar avatar = Tools.instance.getPlayer();
		JSONObject jSONObject2 = AllMapRandomNode[string.Concat(node["id"].I)];
		DateTime dateTime = DateTime.Parse(jSONObject2["resetTime"].str);
		DateTime now = worldTimeMag.getNowTime();
		if (!jSONObject2["Reset"].b && jsonData.instance.AllMapReset.HasField(string.Concat((int)jSONObject2["Type"].n)) && !(now > dateTime.AddMonths((int)jsonData.instance.AllMapReset[string.Concat((int)jSONObject2["Type"].n)]["resetTiem"].n)))
		{
			return;
		}
		jSONObject2.SetField("resetTime", worldTimeMag.nowTime);
		jSONObject2.SetField("Reset", val: false);
		if ((int)node["MapType"].n != 1 && (int)node["MapType"].n != 0)
		{
			return;
		}
		List<JSONObject> list = jsonData.instance.AllMapReset.list.FindAll((JSONObject aa) => (int)aa["Type"].n == (int)node["MapType"].n && level >= (int)aa["qujian"][0].n && level <= (int)aa["qujian"][1].n);
		List<JSONObject> _tempJsond = new List<JSONObject>();
		list.ForEach(delegate(JSONObject aa)
		{
			_tempJsond.Add(new JSONObject(aa.ToString()));
		});
		Dictionary<int, int> dictionary = new Dictionary<int, int>();
		foreach (JSONObject _tempMapNode in AllMapRandomNode.list)
		{
			if (dictionary.ContainsKey((int)_tempMapNode["Type"].n))
			{
				dictionary[(int)_tempMapNode["Type"].n]++;
			}
			else
			{
				int num2 = (dictionary[(int)_tempMapNode["Type"].n] = 1);
			}
			if (_tempMapNode["Type"].I >= 0 && (int)jsonData.instance.AllMapReset[(int)_tempMapNode["Type"].n]["max"].n > 0 && dictionary[(int)_tempMapNode["Type"].n] >= (int)jsonData.instance.AllMapReset[(int)_tempMapNode["Type"].n]["max"].n)
			{
				_tempJsond.Find((JSONObject _acs) => _acs["id"].I == _tempMapNode["Type"].I)?.SetField("percent", 0);
			}
			Transform obj = AllMapManage.instance.AllNodeGameobjGroup.transform.Find(string.Concat(node["id"].I));
			if ((Object)(object)obj == (Object)null)
			{
				Debug.LogError((object)(("路点出错" + node["id"].I) ?? ""));
			}
			foreach (int _nnnn in ((Component)obj).GetComponent<MapComponent>().nextIndex)
			{
				if (AllMapRandomNode.HasField(string.Concat(_nnnn)) && (int)jsonData.instance.AllMapReset[string.Concat((int)AllMapRandomNode[string.Concat(_nnnn)]["Type"].n)]["CanSame"].n == 0)
				{
					_tempJsond.Find((JSONObject _acs) => _acs["id"].I == AllMapRandomNode[string.Concat(_nnnn)]["Type"].I)?.SetField("percent", 0);
				}
			}
		}
		JSONObject json = Tools.instance.getRandomListByPercent(_tempJsond, "percent");
		jSONObject2.SetField("Type", json["id"].I);
		List<JSONObject> list2 = jsonData.instance.MapRandomJsonData.list.FindAll(delegate(JSONObject aa)
		{
			if (aa["EventValue"].list.Count > 0 && !ManZuValue((int)aa["EventValue"][0].n, (int)aa["EventValue"][1].n, aa["fuhao"].str))
			{
				return false;
			}
			if (aa["StartTime"].str != "")
			{
				DateTime dateTime2 = DateTime.Parse(aa["StartTime"].str);
				DateTime dateTime3 = DateTime.Parse(aa["EndTime"].str);
				if (!(now >= dateTime2) || !(now <= dateTime3))
				{
					return false;
				}
			}
			if (avatar.SuiJiShiJian.HasField(Tools.getScreenName()) && avatar.SuiJiShiJian[Tools.getScreenName()].list.Find((JSONObject _aa) => _aa.I == aa["id"].I) != null)
			{
				return false;
			}
			foreach (JSONObject item in AllMapRandomNode.list)
			{
				int i = item["EventId"].I;
				if (jsonData.instance.MapRandomJsonData.HasField(i.ToString()))
				{
					JSONObject jSONObject3 = jsonData.instance.MapRandomJsonData[i.ToString()];
					if (aa["id"].I == jSONObject3["id"].I && jSONObject3["once"].I == 1)
					{
						return false;
					}
				}
			}
			return aa["EventType"].I == json["id"].I && aa["EventLv"].list.Find((JSONObject _aa) => (int)_aa.n == Tools.instance.getPlayer().level) != null;
		});
		if (list2.Count > 0)
		{
			JSONObject randomListByPercent = Tools.instance.getRandomListByPercent(list2, "percent");
			jSONObject2.SetField("Type", (int)randomListByPercent["EventType"].n);
			jSONObject2.SetField("EventId", randomListByPercent["id"].I);
			_ = (int)randomListByPercent["EventType"].n;
			_ = 2;
		}
		else if ((int)node["MapType"].n == 0)
		{
			jSONObject2.SetField("Type", 2);
		}
		else
		{
			jSONObject2.SetField("Type", 5);
		}
	}

	public static bool ManZuValue(int staticValueID, int num, string type)
	{
		int num2 = GlobalValue.Get(staticValueID, $"Avatar.ManZuValue({staticValueID}, {num}, {type})");
		return type switch
		{
			"=" => num2 == num, 
			"<" => num2 < num, 
			">" => num2 > num, 
			_ => true, 
		};
	}

	public void WorldsetRandomFace()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		PlayerSetRandomFace component = ((Component)((GameObject)renderObj).transform.GetChild(0).GetChild(0)).GetComponent<PlayerSetRandomFace>();
		if (fightTemp.MonstarID > 0 && (Object)(object)component != (Object)null)
		{
			component.randomAvatar(fightTemp.MonstarID);
		}
	}

	public void discardCard(Card card)
	{
		Event.fireOut("discardCard", this, card);
	}

	public void MonstarAddStaticSkill()
	{
		foreach (SkillItem equipStaticSkill in equipStaticSkillList)
		{
			StaticSkill staticSkill = new StaticSkill(equipStaticSkill.itemId, 0, 5);
			StaticSkill.Add(staticSkill);
			staticSkill.Puting(this, this, 1);
			addYuanYingStaticSkill(equipStaticSkill, equipStaticSkill.itemId);
		}
	}

	public void addStaticSkill()
	{
		foreach (SkillItem equipStaticSkill in equipStaticSkillList)
		{
			int staticSkillKeyByID = Tools.instance.getStaticSkillKeyByID(equipStaticSkill.itemId);
			StaticSkill staticSkill = new StaticSkill(staticSkillKeyByID, 0, 5);
			StaticSkill.Add(staticSkill);
			staticSkill.Puting(this, this, 1);
			addYuanYingStaticSkill(equipStaticSkill, staticSkillKeyByID);
		}
	}

	public void addYuanYingStaticSkill(SkillItem _skill, int skillid)
	{
		if (_skill.itemIndex != 6)
		{
			return;
		}
		int i = jsonData.instance.StaticSkillJsonData[string.Concat(skillid)]["Skill_LV"].I;
		int i2 = jsonData.instance.StaticSkillJsonData[string.Concat(skillid)]["AttackType"].I;
		JSONObject yuanYingBiao = jsonData.instance.YuanYingBiao;
		if (yuanYingBiao.keys.Count <= 0)
		{
			return;
		}
		foreach (string key in yuanYingBiao.keys)
		{
			if (i2 != yuanYingBiao[key]["value1"].I || i != yuanYingBiao[key]["value2"].I)
			{
				continue;
			}
			for (int j = 0; j < yuanYingBiao[key]["value3"].Count; j++)
			{
				if (yuanYingBiao[key]["target"].I == 1)
				{
					spell.addDBuff(yuanYingBiao[key]["value3"][j].I, yuanYingBiao[key]["value4"][j].I);
				}
				else
				{
					OtherAvatar.spell.addDBuff(yuanYingBiao[key]["value3"][j].I, yuanYingBiao[key]["value4"][j].I);
				}
			}
		}
	}

	public string getYuanYingStaticDesc(SkillItem _skill, int skillid)
	{
		if (_skill.itemIndex == 6)
		{
			int i = jsonData.instance.StaticSkillJsonData[string.Concat(skillid)]["Skill_LV"].I;
			int i2 = jsonData.instance.StaticSkillJsonData[string.Concat(skillid)]["AttackType"].I;
			JSONObject yuanYingBiao = jsonData.instance.YuanYingBiao;
			if (yuanYingBiao.keys.Count > 0)
			{
				foreach (string key in yuanYingBiao.keys)
				{
					if (i2 == yuanYingBiao[key]["value1"].I && i == yuanYingBiao[key]["value2"].I)
					{
						return yuanYingBiao[key]["desc"].Str;
					}
				}
			}
		}
		return "";
	}

	public void addWuDaoSeid()
	{
		foreach (SkillItem allWuDaoSkill in wuDaoMag.GetAllWuDaoSkills())
		{
			new WuDaoStaticSkill(allWuDaoSkill.itemId, 0, 5).Puting(this, this, 1);
		}
	}

	public void addJieDanSeid()
	{
		foreach (SkillItem hasJieDanSkill in hasJieDanSkillList)
		{
			new JieDanSkill(hasJieDanSkill.itemId, 0, 5).Puting(this, this, 1);
		}
	}

	public void addEquipSeid()
	{
		int num = 0;
		int num2 = 1;
		if (isPlayer())
		{
			foreach (UIFightWeaponItem item in UIFightPanel.Inst.FightWeapon)
			{
				item.Clear();
			}
		}
		bool flag = false;
		foreach (ITEM_INFO value in equipItemList.values)
		{
			foreach (JSONObject item2 in jsonData.instance.ItemJsonData[string.Concat(value.itemId)]["seid"].list)
			{
				if (item2.I == 1)
				{
					int buffid = (int)jsonData.instance.EquipSeidJsonData[1][string.Concat(value.itemId)]["value1"].n;
					if (isPlayer() && value.Seid.HasField("ItemSeids") && value.Seid["ItemSeids"].list.Count > 0)
					{
						int key = (int)jsonData.instance.EquipSeidJsonData[1][string.Concat(value.itemId)]["value1"].n;
						if (!fightTemp.LianQiBuffEquipDictionary.ContainsKey(key))
						{
							fightTemp.LianQiBuffEquipDictionary.Add(key, value.Seid["ItemSeids"]);
							fightTemp.LianQiEquipDictionary.Add(key, value.Seid);
						}
						List<JSONObject> list = value.Seid["ItemSeids"].list;
						bool flag2 = true;
						for (int i = 0; i < list.Count; i++)
						{
							if (list[i]["id"].I == 62 && (float)HP / (float)HP_Max * 100f > list[i]["value1"][0].n)
							{
								flag2 = false;
							}
						}
						if (flag2)
						{
							for (int j = 0; j < list.Count; j++)
							{
								if (list[j]["id"].I == 64)
								{
									for (int k = 0; k < list[j]["value1"].Count; k++)
									{
										spell.addBuff(list[j]["value1"][k].I, list[j]["value2"][k].I);
									}
								}
							}
						}
					}
					spell.addDBuff(buffid);
				}
				if (item2.I != 2)
				{
					continue;
				}
				int i2 = jsonData.instance.EquipSeidJsonData[2][string.Concat(value.itemId)]["value1"].I;
				if (value.Seid.HasField("ItemSeids"))
				{
					foreach (JSONObject item3 in value.Seid["ItemSeids"].list)
					{
						if (item3["id"].I == item2.I)
						{
							i2 = item3["value1"].I;
						}
					}
				}
				GUIPackage.Skill skill = new GUIPackage.Skill(i2, 0, 10);
				if (value.Seid.HasField("AttackType"))
				{
					if (num == i2)
					{
						num = (skill.skill_ID = num + 5);
						jsonData.instance.skillJsonData[num.ToString()].SetField("AttackType", value.Seid["AttackType"]);
						_skillJsonData.DataDict[num].AttackType = value.Seid["AttackType"].ToList();
					}
					else
					{
						num = i2;
						jsonData.instance.skillJsonData[num.ToString()].SetField("AttackType", value.Seid["AttackType"]);
						_skillJsonData.DataDict[num].AttackType = value.Seid["AttackType"].ToList();
					}
				}
				if (value.Seid.HasField("SkillSeids"))
				{
					skill.ItemAddSeid = value.Seid["SkillSeids"];
				}
				if (value.Seid.HasField("Damage"))
				{
					skill.Damage = value.Seid["Damage"].I;
				}
				if (value.Seid.HasField("Name"))
				{
					skill.skill_Name = value.Seid["Name"].str;
				}
				if (value.Seid.HasField("SeidDesc"))
				{
					skill.skill_Desc = value.Seid["SeidDesc"].str;
				}
				if (value.Seid.HasField("ItemIcon"))
				{
					((object)skill.skill_Icon).ToString();
					skill.skill_Icon = ResManager.inst.LoadTexture2D(value.Seid["ItemIcon"].str);
				}
				this.skill.Add(skill);
				if (isPlayer() && UIFightPanel.Inst.FightWeapon != null && UIFightPanel.Inst.FightWeapon.Count > 1)
				{
					UIFightWeaponItem uIFightWeaponItem = UIFightPanel.Inst.FightWeapon[0];
					if (num2 == 2)
					{
						uIFightWeaponItem = UIFightPanel.Inst.FightWeapon[1];
					}
					((Component)uIFightWeaponItem).gameObject.SetActive(true);
					uIFightWeaponItem.SetWeapon(skill, value);
					num2++;
				}
				if (!flag)
				{
					flag = true;
					object obj = renderObj;
					FightFaBaoShow componentInChildren = ((GameObject)((obj is GameObject) ? obj : null)).GetComponentInChildren<FightFaBaoShow>();
					if ((Object)(object)componentInChildren != (Object)null)
					{
						componentInChildren.SetWeapon(this, value);
					}
					else
					{
						Debug.LogError((object)"没有查找到法宝显示组件，需要程序检查");
					}
				}
			}
		}
	}

	public void onCrystalChanged(CardMag oldValue)
	{
		Event.fireOut("crtstalChanged", this, oldValue);
		MessageMag.Instance.Send("Fight_CardChange");
	}

	public void FightClearSkill(int startIndex, int endIndex)
	{
		if (!isPlayer())
		{
			return;
		}
		int num = 0;
		foreach (UIFightSkillItem fightSkill in UIFightPanel.Inst.FightSkills)
		{
			if (num >= startIndex && num < endIndex)
			{
				fightSkill.Clear();
			}
			num++;
		}
	}

	public void FightAddSkill(int skillID, int startIndex, int endIndex)
	{
		GUIPackage.Skill item = new GUIPackage.Skill(skillID, 0, 10);
		skill.Add(item);
		if (!isPlayer())
		{
			return;
		}
		int num = 0;
		foreach (UIFightSkillItem fightSkill in UIFightPanel.Inst.FightSkills)
		{
			if (num >= startIndex && num < endIndex && !fightSkill.HasSkill)
			{
				fightSkill.SetSkill(item);
				break;
			}
			num++;
		}
	}

	public void addSkill()
	{
		foreach (SkillItem equipSkill in equipSkillList)
		{
			int skillKeyByID = Tools.instance.getSkillKeyByID(equipSkill.itemId, this);
			if (skillKeyByID == -1)
			{
				Debug.LogError((object)("找不到技能ID：" + equipSkill.itemId));
			}
			GUIPackage.Skill item = new GUIPackage.Skill(skillKeyByID, 0, 10);
			skill.Add(item);
			if (isPlayer())
			{
				UIFightPanel.Inst.FightSkills[equipSkill.itemIndex].SetSkill(item);
			}
		}
	}

	public override void __init__()
	{
		ai = new AI(this);
		combat = new Combat(this);
		spell = new Spell(this);
		jieyin = new JieYin(this);
		dialogMsg = new Dialog(this);
		buffmag = new BuffMag(this);
		wuDaoMag = new WuDaoMag(this);
		chuanYingManager = new ChuanYingManager(this);
		jianLingManager = new JianLingManager(this);
		taskMag = new TaskMag(this);
		cardMag = new CardMag(this);
		zulinContorl = new ZulinContorl(this);
		fubenContorl = new FubenContrl(this);
		nomelTaskMag = new NomelTaskMag(this);
		chenghaomag = new chenghaoMag(this);
		fightTemp = new FightTempValue();
		randomFuBenMag = new RandomFuBenMag(this);
		seaNodeMag = new SeaNodeMag(this);
		if (isPlayer())
		{
			Event.registerIn("relive", this, "relive");
			Event.registerIn("updatePlayer", this, "updatePlayer");
			Event.registerIn("sendChatMessage", this, "sendChatMessage");
		}
	}

	public void setHP(int hp)
	{
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Expected O, but got Unknown
		if ((Object)(object)RoundManager.instance != (Object)null && RoundManager.instance.IsVirtual)
		{
			return;
		}
		List<int> list = new List<int>();
		list.Add(hp);
		spell.onBuffTickByType(10, list);
		if ((state == 1 || OtherAvatar.state == 1) && hp < Tools.instance.getPlayer().HP)
		{
			return;
		}
		if (hp > HP_Max)
		{
			hp = HP_Max;
		}
		if ((Object)(object)RoundManager.instance != (Object)null)
		{
			if (hp > HP)
			{
				fightTemp.SetHealHP(hp - HP);
			}
			if (hp < HP)
			{
				fightTemp.SetRoundLossHP(HP - hp);
			}
			if (RoundManager.instance.PlayerFightEventProcessor != null)
			{
				RoundManager.instance.PlayerFightEventProcessor.OnUpdateHP();
			}
		}
		HP = hp;
		int showHP = HP;
		Queue<UnityAction> queue = new Queue<UnityAction>();
		UnityAction item = (UnityAction)delegate
		{
			fightTemp.showNowHp = showHP;
			YSFuncList.Ints.Continue();
		};
		queue.Enqueue(item);
		YSFuncList.Ints.AddFunc(queue);
		YSFuncList.Ints.Start();
		if (hp <= 0 && OtherAvatar.state != 1)
		{
			if (isPlayer())
			{
				die();
				return;
			}
			if ((Object)(object)RoundManager.instance != (Object)null)
			{
				World.GameOver();
			}
		}
		else if (hp <= 0 && OtherAvatar.state == 1)
		{
			return;
		}
		Event.fireOut("set_HP", this, HP);
	}

	public void SetChengHaoId(int id)
	{
		chengHao = id;
		if (id >= 7 && id <= 10 && !StreamData.MenPaiTaskMag.IsInit)
		{
			StreamData.MenPaiTaskMag.NextTime = NpcJieSuanManager.inst.GetNowTime();
			StreamData.MenPaiTaskMag.IsInit = true;
		}
	}

	public void die()
	{
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_0229: Expected O, but got Unknown
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Expected O, but got Unknown
		MonstarMag monstarmag = Tools.instance.monstarMag;
		if (monstarmag.shouldReloadSaveHp())
		{
			HP = Tools.instance.monstarMag.gameStartHP;
		}
		state = 1;
		if (!isPlayer())
		{
			Avatar player = PlayerEx.Player;
			if (monstarmag.shouldReloadSaveHp())
			{
				player.HP = Tools.instance.monstarMag.gameStartHP;
			}
			MusicMag.instance.PlayEffectMusic(6);
			List<int> flag = new List<int>();
			player.spell.onBuffTickByType(22, flag);
			GlobalValue.SetTalk(1, 2, "Avatar.die");
			player.StaticValue.talk[1] = 2;
			if (GlobalValue.Get(401, "Avatar.die") == Tools.instance.MonstarID)
			{
				player.nomelTaskMag.AutoNTaskSetKillAvatar(Tools.instance.MonstarID);
			}
			if (monstarmag.PlayerAddShaQi())
			{
				shaQi++;
			}
			if (monstarmag.MonstarCanDeath())
			{
				jsonData.instance.setMonstarDeath(Tools.instance.MonstarID);
			}
			try
			{
				monstarmag.AddKillAvatarWuDao(Tools.instance.getPlayer(), Tools.instance.MonstarID);
			}
			catch (Exception)
			{
			}
			Tools.instance.AutoSetSeaMonstartDie();
			if (Tools.instance.monstarMag.FightType == StartFight.FightEnumType.FeiSheng)
			{
				TianJieManager.Inst.DuJieSuccess(win: true);
			}
		}
		else if (isPlayer())
		{
			Tools.instance.CaiYaoData = null;
			GlobalValue.SetTalk(1, 3, "Avatar.die");
			if (monstarmag.ReloadHpType() == 2)
			{
				HP = 1;
			}
			if (monstarmag.PlayerNotDeath())
			{
				Queue<UnityAction> queue = new Queue<UnityAction>();
				object obj = _003C_003Ec._003C_003E9__301_0;
				if (obj == null)
				{
					UnityAction val = delegate
					{
						Tools.instance.loadMapScenes(Tools.instance.FinalScene);
						YSFuncList.Ints.Continue();
					};
					_003C_003Ec._003C_003E9__301_0 = val;
					obj = (object)val;
				}
				UnityAction item = (UnityAction)obj;
				queue.Enqueue(item);
				YSFuncList.Ints.AddFunc(queue);
			}
			else if (monstarmag.isInFubenNotDeath())
			{
				Tools.instance.getPlayer().fubenContorl.outFuBen();
			}
			else
			{
				Queue<UnityAction> queue2 = new Queue<UnityAction>();
				UnityAction item2 = (UnityAction)delegate
				{
					int num = monstarmag.FightLose();
					UIDeath.Inst.Show((DeathType)num);
					YSFuncList.Ints.Continue();
				};
				queue2.Enqueue(item2);
				YSFuncList.Ints.AddFunc(queue2);
			}
			return;
		}
		Event.fireOut("set_state", this, (sbyte)1);
	}

	public override void onDestroy()
	{
		if (isPlayer())
		{
			Event.deregisterIn(this);
		}
	}

	public void gameFinsh()
	{
		cellCall("gameFinsh");
	}

	public virtual void updatePlayer(float x, float y, float z, float yaw)
	{
		position.x = x;
		position.y = y;
		position.z = z;
		direction.z = yaw;
	}

	public override void onEnterWorld()
	{
		base.onEnterWorld();
		if (isPlayer())
		{
			Event.fireOut("onAvatarEnterWorld", KBEngineApp.app.entity_uuid, id, this);
			SkillBox.inst.pull();
		}
	}

	public void sendChatMessage(string msg)
	{
		object obj = name;
		baseCall("sendChatMessage", (string)obj + ": " + msg);
	}

	public override void ReceiveChatMessage(string msg)
	{
		Event.fireOut("ReceiveChatMessage", msg);
	}

	public void relive(byte type)
	{
		cellCall("relive", type);
	}

	public int useTargetSkill(int skillID, int targetID)
	{
		Skill skill = SkillBox.inst.get(skillID);
		if (skill == null)
		{
			return 4;
		}
		SCEntityObject target = new SCEntityObject(targetID);
		int num = skill.validCast(this, target);
		if (num == 0)
		{
			skill.use(this, target);
			return num;
		}
		return num;
	}

	public bool HasDunShuSkill()
	{
		foreach (SkillItem equipStaticSkill in equipStaticSkillList)
		{
			if (jsonData.instance.StaticSkillJsonData[string.Concat(Tools.instance.getStaticSkillKeyByID(equipStaticSkill.itemId))]["seid"].list.Find((JSONObject aa) => (int)aa.n == 9) != null)
			{
				return true;
			}
		}
		return false;
	}

	public int useTargetSkill(int skillID)
	{
		Skill skill = SkillBox.inst.get(skillID);
		if (skill == null)
		{
			return 4;
		}
		int num = skill.validCast(this);
		if (num == 0)
		{
			skill.use(this);
			return num;
		}
		return num;
	}

	public override void recvSkill(int attacker, int skillID)
	{
		Event.fireOut("recvSkill", attacker, skillID);
	}

	public override void onAddSkill(int skillID)
	{
		Dbg.DEBUG_MSG(className + "::onAddSkill(" + skillID + ")");
		Skill skill = new Skill();
		skill.id = skillID;
		skill.name = skillID + " ";
		skill.displayType = (Skill_DisplayType)jsonData.instance.skillJsonData[string.Concat(skillID)]["Skill_DisplayType"].n;
		skill.canUseDistMax = jsonData.instance.skillJsonData[string.Concat(skillID)]["canUseDistMax"].n;
		skill.skillEffect = jsonData.instance.skillJsonData[string.Concat(skillID)]["skillEffect"].str;
		string text = Regex.Unescape(jsonData.instance.skillJsonData[string.Concat(skillID)]["name"].str);
		skill.name = text;
		skill.coolTime = jsonData.instance.skillJsonData[string.Concat(skillID)]["CD"].n;
		skill.restCoolTimer = skill.coolTime;
		SkillBox.inst.add(skill);
		Event.fireOut("setSkillButton");
	}

	public override void clearSkills()
	{
		SkillBox.inst.clear();
		cellCall("requestPull");
	}

	public void createBuild(ulong BuildId, Vector3 positon, Vector3 direction)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		baseCall("createBuild", BuildId, positon, direction);
	}

	public override void onRemoveSkill(int skillID)
	{
		Dbg.DEBUG_MSG(className + "::onRemoveSkill(" + skillID + ")");
		Event.fireOut("onRemoveSkill", this);
		SkillBox.inst.remove(skillID);
	}

	public override void recvDamage(int attackerID, int skillID, int damageType, int damage)
	{
		Entity entity = KBEngineApp.app.findEntity(attackerID);
		Event.fireOut("recvDamage", this, entity, skillID, damageType, damage);
	}

	public int recvDamage(Entity _attaker, Entity _receiver, int skillId, int damage, int type = 0)
	{
		Avatar avatar = (Avatar)_attaker;
		Avatar avatar2 = (Avatar)_receiver;
		int num = damage;
		if (type == 0)
		{
			List<int> list = new List<int>();
			list.Add(damage);
			list.Add(skillId);
			if (damage < 0)
			{
				avatar2.spell.onBuffTickByType(6, list);
			}
			if (damage > 0)
			{
				avatar.spell.onBuffTickByType(31, list);
				avatar2.spell.onBuffTickByType(7, list);
			}
			if (list.Count > 0)
			{
				damage = list[0];
			}
			if (num > 0)
			{
				if (damage < 0)
				{
					damage = 0;
				}
			}
			else if (num < 0 && damage > 0)
			{
				damage = 0;
			}
			if (avatar2.HP - damage <= 0)
			{
				avatar2.spell.onBuffTickByType(28, list);
			}
			if (avatar2.buffmag.HasBuffSeid(30) && num > 0 && damage == 0)
			{
				foreach (List<int> item in avatar2.buffmag.getBuffBySeid(30))
				{
					int num2 = (int)jsonData.instance.BuffSeidJsonData[30][string.Concat(item[2])]["value1"].n;
					int num3 = (int)jsonData.instance.BuffSeidJsonData[30][string.Concat(item[2])]["value2"].n;
					avatar2.recvDamage(avatar2, avatar2.OtherAvatar, 10001 + num3, num2 * item[1]);
				}
			}
			foreach (JSONObject item2 in jsonData.instance.skillJsonData[string.Concat(skillId)]["seid"].list)
			{
				if (65 == (int)item2.n)
				{
					int num4 = (int)jsonData.instance.SkillSeidJsonData[65][string.Concat(skillId)]["value1"].n;
					if (damage > num4)
					{
						damage = num4;
					}
				}
			}
			if (avatar2.buffmag.HasBuffSeid(19) || avatar2.OtherAvatar.buffmag.HasBuffSeid(19))
			{
				avatar2.OtherAvatar.setHP(avatar2.OtherAvatar.HP - damage);
			}
			if (avatar2.OtherAvatar.buffmag.HasBuffSeid(90) && damage > 0)
			{
				avatar2.OtherAvatar.setHP(avatar2.OtherAvatar.HP + damage);
				Event.fireOut("recvDamage", avatar2.OtherAvatar, avatar2.OtherAvatar, skillId, 0, -damage);
			}
			if (!((Object)(object)RoundManager.instance != (Object)null) || !RoundManager.instance.IsVirtual)
			{
				avatar.fightTemp.SetRoundDamage(avatar, damage, skillId);
				avatar2.fightTemp.SetRoundReceiveDamage(avatar, damage, skillId);
			}
			if (damage > 0)
			{
				avatar2.spell.onRemoveBuffByType(9, damage);
			}
			avatar2.spell.onRemoveBuffByType(11);
			Event.fireOut("recvDamage", avatar2, _attaker, skillId, 0, damage);
			avatar2.setHP(avatar2.HP - damage);
			if (damage > 0)
			{
				avatar2.spell.onBuffTickByType(42, list);
			}
		}
		return damage;
	}

	public void continuFunc()
	{
		YSFuncList.Ints.Continue();
	}

	public card addCrystal(int CrystalType, int num = 1)
	{
		List<int> list = new List<int>();
		list.Add(num);
		list.Add(CrystalType);
		spell.onBuffTickByType(25, list);
		return crystal.addCard(CrystalType, num);
	}

	public void removeCrystal(int CrystalType, int num = 1)
	{
		crystal.removeCard(CrystalType, num);
	}

	public void removeCrystal(card CrystalType)
	{
		crystal.removeCard(CrystalType);
	}

	public void UseCryStal(int CrystalType, int num = 1)
	{
		for (int i = 0; i < num; i++)
		{
			NowRoundUsedCard.Add(CrystalType);
		}
		List<int> list = new List<int>();
		list.Add(num);
		list.Add(CrystalType);
		spell.onBuffTickByType(27, list);
		removeCrystal(CrystalType, num);
	}

	public void AbandonCryStal(int CrystalType, int num = 1)
	{
		List<int> list = new List<int>();
		list.Add(num);
		list.Add(CrystalType);
		removeCrystal(CrystalType, num);
		spell.onBuffTickByType(26, list);
	}

	public void AbandonCryStal(card CrystalType, int num = 1)
	{
		List<int> list = new List<int>();
		list.Add(num);
		list.Add(CrystalType.cardType);
		removeCrystal(CrystalType);
		spell.onBuffTickByType(26, list);
	}

	public bool checkHasStudyWuDaoSkillByID(int id)
	{
		foreach (SkillItem allWuDaoSkill in wuDaoMag.GetAllWuDaoSkills())
		{
			if (allWuDaoSkill.itemId == id)
			{
				return true;
			}
		}
		return false;
	}

	public void equipItem(int itemID)
	{
		foreach (ITEM_INFO value in equipItemList.values)
		{
			if (jsonData.instance.ItemJsonData[string.Concat(value.itemId)]["type"].str == "Skill" && value.itemId == itemID)
			{
				return;
			}
		}
		ITEM_INFO iTEM_INFO = new ITEM_INFO();
		iTEM_INFO.itemId = itemID;
		equipItemList.values.Add(iTEM_INFO);
	}

	public void UnEquipItem(int itemID)
	{
		ITEM_INFO item = new ITEM_INFO();
		foreach (ITEM_INFO value in equipItemList.values)
		{
			if (value.itemId == itemID)
			{
				item = value;
			}
		}
		equipItemList.values.Remove(item);
	}

	public void equipSkill(int SkillID, int index = 0)
	{
		equipSkillList.RemoveAll((SkillItem aa) => index == aa.itemIndex);
		foreach (SkillItem equipSkill in equipSkillList)
		{
			if (equipSkill.itemId == SkillID)
			{
				return;
			}
		}
		SkillItem skillItem = new SkillItem();
		skillItem.itemId = SkillID;
		skillItem.itemIndex = index;
		equipSkillList.Add(skillItem);
		PlayTutorial.CheckSkillTask();
	}

	public void equipStaticSkill(int SkillID, int index = 0)
	{
		equipStaticSkillList.RemoveAll((SkillItem aa) => index == aa.itemIndex);
		foreach (SkillItem equipStaticSkill in equipStaticSkillList)
		{
			if (equipStaticSkill.itemId == SkillID)
			{
				return;
			}
		}
		SkillItem skillItem = new SkillItem();
		skillItem.itemId = SkillID;
		skillItem.itemIndex = index;
		equipStaticSkillList.Add(skillItem);
		GUIPackage.StaticSkill.resetSeid(this);
		PlayTutorial.CheckGongFaTask();
	}

	public void YSequipItem(string UUID, int index = 0, int key = 0)
	{
		ITEM_INFO iTEM_INFO = FindItemByUUID(UUID);
		List<ITEM_INFO> list = new List<ITEM_INFO>();
		foreach (ITEM_INFO value in equipItemList.values)
		{
			if (_ItemJsonData.DataDict[value.itemId].type == _ItemJsonData.DataDict[iTEM_INFO.itemId].type)
			{
				if (checkHasStudyWuDaoSkillByID(2231) && (Singleton.equip.Equip[key].UUID == UUID || Singleton.equip.Equip[key].itemID == -1))
				{
					index = key;
				}
				else
				{
					list.Add(value);
				}
			}
		}
		foreach (ITEM_INFO item in list)
		{
			YSUnequipItem(item.uuid);
		}
		iTEM_INFO.itemIndex = index;
		equipItemList.values.Add(iTEM_INFO);
		Equips.resetEquipSeid(this);
	}

	public _ItemJsonData GetEquipLingZhouData()
	{
		foreach (ITEM_INFO value in equipItemList.values)
		{
			if (_ItemJsonData.DataDict[value.itemId].type == 14)
			{
				return _ItemJsonData.DataDict[value.itemId];
			}
		}
		return null;
	}

	public JToken GetNowLingZhouShuXinJson()
	{
		_ItemJsonData equipLingZhouData = GetEquipLingZhouData();
		if (equipLingZhouData != null)
		{
			return jsonData.instance.LingZhouPinJie[equipLingZhouData.quality.ToString()];
		}
		return null;
	}

	public void ReduceLingZhouNaiJiu(BaseItem baseItem, int num)
	{
		baseItem.Seid.SetField("NaiJiu", baseItem.Seid["NaiJiu"].I - num);
		if (baseItem.Seid["NaiJiu"].I <= 0)
		{
			Tools.instance.RemoveItem(baseItem.Uid);
		}
	}

	public void removeEquipItem(string UUID)
	{
		YSUnequipItem(UUID);
		removeItem(UUID);
	}

	public BaseItem GetLingZhou()
	{
		Dictionary<int, BaseItem> curEquipDict = Tools.instance.getPlayer().StreamData.FangAnData.GetCurEquipDict();
		BaseItem baseItem = null;
		foreach (int key in curEquipDict.Keys)
		{
			baseItem = curEquipDict[key];
			if (_ItemJsonData.DataDict[baseItem.Id].type == 14)
			{
				if (baseItem.Seid == null)
				{
					baseItem.Seid = Tools.CreateItemSeid(baseItem.Id);
				}
				else if (!baseItem.Seid.HasField("NaiJiu"))
				{
					baseItem.Seid = Tools.CreateItemSeid(baseItem.Id);
				}
				return baseItem;
			}
		}
		return null;
	}

	public void YSequipItem(int itemID, int index = 0)
	{
		List<ITEM_INFO> list = new List<ITEM_INFO>();
		foreach (ITEM_INFO value in equipItemList.values)
		{
			if (_ItemJsonData.DataDict[value.itemId].type == _ItemJsonData.DataDict[itemID].type)
			{
				list.Add(value);
			}
		}
		foreach (ITEM_INFO item in list)
		{
			equipItemList.values.Remove(item);
		}
		ITEM_INFO iTEM_INFO = new ITEM_INFO();
		iTEM_INFO.itemId = itemID;
		iTEM_INFO.itemIndex = index;
		equipItemList.values.Add(iTEM_INFO);
		removeItem(itemID);
		Equips.resetEquipSeid(this);
	}

	public void UnEquipSkill(int SkillID)
	{
		SkillItem item = new SkillItem();
		foreach (SkillItem equipSkill in equipSkillList)
		{
			if (equipSkill.itemId == SkillID)
			{
				item = equipSkill;
				break;
			}
		}
		equipSkillList.Remove(item);
	}

	public void UnEquipStaticSkill(int SkillID)
	{
		SkillItem item = new SkillItem();
		foreach (SkillItem equipStaticSkill in equipStaticSkillList)
		{
			if (equipStaticSkill.itemId == SkillID)
			{
				item = equipStaticSkill;
				break;
			}
		}
		equipStaticSkillList.Remove(item);
		GUIPackage.StaticSkill.resetSeid(this);
		if (HP > HP_Max)
		{
			HP = HP_Max;
		}
	}

	public void YSUnequipItem(string UUID, int index = 0)
	{
		ITEM_INFO iTEM_INFO = FindItemByUUID(UUID);
		if (iTEM_INFO == null && UUID != "")
		{
			itemList.values.Add(FindEquipItemByUUID(UUID));
			iTEM_INFO = FindItemByUUID(UUID);
		}
		iTEM_INFO.itemIndex = index;
		removeEquip(UUID);
		Equips.resetEquipSeid(this);
	}

	public void removeEquip(string UUID)
	{
		equipItemList.values.RemoveAll((ITEM_INFO aa) => aa.uuid == UUID);
	}

	public void removeEquip(int id, int sum)
	{
		for (int i = 0; i < sum; i++)
		{
			removeEquipByItemID(id);
		}
	}

	private void removeEquipByItemID(int id)
	{
		ITEM_INFO iTEM_INFO = new ITEM_INFO();
		for (int i = 0; i < equipItemList.values.Count; i++)
		{
			if (equipItemList.values[i].itemId == id)
			{
				iTEM_INFO = equipItemList.values[i];
				break;
			}
		}
		equipItemList.values.Remove(iTEM_INFO);
		string uuid = iTEM_INFO.uuid;
		removeItem(uuid);
	}

	public void YSUnequipItem(int itemID)
	{
		ITEM_INFO iTEM_INFO = new ITEM_INFO();
		foreach (ITEM_INFO value in equipItemList.values)
		{
			if (value.itemId == itemID)
			{
				iTEM_INFO = value;
				break;
			}
		}
		addItem(itemID, iTEM_INFO.Seid);
		equipItemList.values.Remove(iTEM_INFO);
	}

	public int getItemNum(int itemID)
	{
		int num = 0;
		int num2 = (int)jsonData.instance.ItemJsonData[string.Concat(itemID)]["maxNum"].n;
		foreach (ITEM_INFO value in itemList.values)
		{
			if (value.itemId == itemID && value.itemCount <= num2)
			{
				num += (int)value.itemCount;
			}
		}
		foreach (ITEM_INFO value2 in equipItemList.values)
		{
			if (value2.itemId == itemID && value2.itemCount <= num2)
			{
				num += (int)value2.itemCount;
			}
		}
		return num;
	}

	public ITEM_INFO getItemInfo(int itemID)
	{
		foreach (ITEM_INFO value in itemList.values)
		{
			if (value.itemId == itemID)
			{
				return value;
			}
		}
		return null;
	}

	public bool YuJianFeiXing()
	{
		foreach (SkillItem equipStaticSkill in equipStaticSkillList)
		{
			int staticSkillKeyByID = Tools.instance.getStaticSkillKeyByID(equipStaticSkill.itemId);
			if (StaticSkillJsonData.DataDict[staticSkillKeyByID].seid.Contains(9))
			{
				return true;
			}
		}
		return false;
	}

	public void addItem(int itemID, int Count, JSONObject _seid, bool ShowText = false)
	{
		if (!_ItemJsonData.DataDict.ContainsKey(itemID))
		{
			Debug.LogError((object)$"添加物品出现异常，不存在ID为{itemID}的物品");
			return;
		}
		if (_seid != null && _seid.HasField("isPaiMai"))
		{
			_seid.RemoveField("isPaiMai");
		}
		_ItemJsonData itemJsonData = _ItemJsonData.DataDict[itemID];
		if (ShowText)
		{
			UIPopTip.Inst.PopAddItem(itemJsonData.name, Count);
		}
		try
		{
			_ = jsonData.instance.ItemJsonData[itemID.ToString()];
			if (itemJsonData.seid.Contains(21))
			{
				item item = new item(itemID);
				for (int i = 0; i < Count; i++)
				{
					item.gongneng();
				}
				return;
			}
		}
		catch (Exception ex)
		{
			Debug.LogError((object)ex);
		}
		addItem(itemID, _seid, Count);
		if ((Object)(object)Singleton.inventory != (Object)null)
		{
			for (int j = 0; j < Count; j++)
			{
				Singleton.inventory.AddItem(itemID);
			}
		}
	}

	public void AddEquip(int itemID, string uuid, JSONObject _seid)
	{
		if (!_ItemJsonData.DataDict.ContainsKey(itemID))
		{
			Debug.LogError((object)$"添加物品出现异常，不存在ID为{itemID}的物品");
			return;
		}
		_ItemJsonData itemJsonData = _ItemJsonData.DataDict[itemID];
		if (_seid != null && _seid.HasField("isPaiMai"))
		{
			_seid.RemoveField("isPaiMai");
		}
		try
		{
			_ = jsonData.instance.ItemJsonData[itemID.ToString()];
			if (itemJsonData.seid.Contains(21))
			{
				item item = new item(itemID);
				for (int i = 0; i < 1; i++)
				{
					item.gongneng();
				}
				return;
			}
		}
		catch (Exception ex)
		{
			Debug.LogError((object)ex);
		}
		AddEquip(itemID, _seid, uuid);
		if ((Object)(object)Singleton.inventory != (Object)null)
		{
			for (int j = 0; j < 1; j++)
			{
				Singleton.inventory.AddItem(itemID);
			}
		}
	}

	private void AddEquip(int itemID, JSONObject _seid, string uid)
	{
		if (_seid == null)
		{
			_seid = new JSONObject(JSONObject.Type.OBJECT);
		}
		if (_ItemJsonData.DataDict[itemID].maxNum == 1 || _seid.Count > 0)
		{
			for (int i = 0; i < 1; i++)
			{
				ITEM_INFO iTEM_INFO = new ITEM_INFO();
				iTEM_INFO.uuid = uid;
				iTEM_INFO.itemId = itemID;
				iTEM_INFO.itemCount = 1u;
				iTEM_INFO.Seid = _seid;
				itemList.values.Add(iTEM_INFO);
			}
		}
		else if (getItemNum(itemID) == 0)
		{
			ITEM_INFO iTEM_INFO2 = new ITEM_INFO();
			iTEM_INFO2.uuid = uid;
			iTEM_INFO2.itemId = itemID;
			iTEM_INFO2.itemCount = 1u;
			itemList.values.Add(iTEM_INFO2);
		}
		else
		{
			getItemInfo(itemID).itemCount++;
		}
	}

	public ITEM_INFO FindItemByUUID(string itemUUId)
	{
		return itemList.values.Find((ITEM_INFO aa) => aa.uuid == itemUUId);
	}

	public ITEM_INFO FindEquipItemByUUID(string itemUUId)
	{
		return equipItemList.values.Find((ITEM_INFO aa) => aa.uuid == itemUUId);
	}

	public void addItem(int itemID, JSONObject _seid, int count = 1)
	{
		if (_seid == null)
		{
			_seid = new JSONObject(JSONObject.Type.OBJECT);
		}
		_ItemJsonData itemJsonData = _ItemJsonData.DataDict[itemID];
		if (itemJsonData.type == 6 && !YaoCaiIsGet.HasItem(itemID))
		{
			YaoCaiIsGet.Add(itemID);
		}
		if (itemJsonData.maxNum == 1 || (_seid != null && _seid.Count > 0))
		{
			if (count > 50)
			{
				Debug.LogError((object)("警告获取的不能堆叠物品" + itemID + "超过50个无法直接添加"));
				UIPopTip.Inst.Pop("警告获取的不能堆叠物品" + itemID + "超过50个无法直接添加");
				return;
			}
			for (int i = 0; i < count; i++)
			{
				ITEM_INFO iTEM_INFO = new ITEM_INFO();
				iTEM_INFO.uuid = Tools.getUUID();
				iTEM_INFO.itemId = itemID;
				iTEM_INFO.itemCount = 1u;
				iTEM_INFO.Seid = _seid;
				itemList.values.Add(iTEM_INFO);
			}
		}
		else if (getItemNum(itemID) == 0)
		{
			ITEM_INFO iTEM_INFO2 = new ITEM_INFO();
			iTEM_INFO2.uuid = Tools.getUUID();
			iTEM_INFO2.itemId = itemID;
			iTEM_INFO2.itemCount = (uint)count;
			itemList.values.Add(iTEM_INFO2);
		}
		else
		{
			getItemInfo(itemID).itemCount += (uint)count;
		}
		if (!isPlayer() || itemJsonData.TuJianType <= 0)
		{
			return;
		}
		TuJianManager.Inst.UnlockItem(itemID);
		if (itemJsonData.TuJianType == 1)
		{
			if (GetHasYaoYinShuXin(itemID, itemJsonData.quality))
			{
				TuJianManager.Inst.UnlockYaoYin(itemID);
			}
			if (GetHasZhuYaoShuXin(itemID, itemJsonData.quality))
			{
				TuJianManager.Inst.UnlockZhuYao(itemID);
			}
			if (GetHasFuYaoShuXin(itemID, itemJsonData.quality))
			{
				TuJianManager.Inst.UnlockFuYao(itemID);
			}
		}
	}

	public int getRemoveItemNum(int itemID)
	{
		int num = 0;
		try
		{
			foreach (ITEM_INFO value in itemList.values)
			{
				if (value.itemId == itemID)
				{
					num += (int)value.itemCount;
				}
			}
		}
		catch (Exception)
		{
			Debug.Log((object)$"出错物品ID:{itemID}------------------------------------------");
		}
		return num;
	}

	public void removeItem(int itemID, int Count)
	{
		for (int i = 0; i < Count; i++)
		{
			removeItem(itemID);
		}
	}

	public void removeItem(string UUID, int Count)
	{
		for (int i = 0; i < Count; i++)
		{
			removeItem(UUID);
		}
	}

	public void removeItem(string UUID)
	{
		ITEM_INFO iTEM_INFO = FindItemByUUID(UUID);
		if (iTEM_INFO != null)
		{
			iTEM_INFO.itemCount--;
			if (iTEM_INFO.itemCount == 0)
			{
				itemList.values.Remove(iTEM_INFO);
			}
		}
	}

	public void removeItem(int itemID)
	{
		if (getRemoveItemNum(itemID) > 0)
		{
			ITEM_INFO itemInfo = getItemInfo(itemID);
			itemInfo.itemCount--;
			if (itemInfo.itemCount == 0)
			{
				itemList.values.Remove(itemInfo);
			}
		}
	}

	public void Load(int id, int index)
	{
		if (File.Exists(Paths.GetSavePath() + "/StreamData" + Tools.instance.getSaveID(id, index) + ".sav"))
		{
			FileStream fileStream = new FileStream(Paths.GetSavePath() + "/StreamData" + Tools.instance.getSaveID(id, index) + ".sav", FileMode.Open, FileAccess.Read, FileShare.Read);
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			StreamData = (StreamData)binaryFormatter.Deserialize(fileStream);
			fileStream.Close();
		}
		else
		{
			StreamData = new StreamData();
		}
		StreamData.FangAnData.LoadHandle();
	}

	public void Save(int id, int index)
	{
		StreamData streamData = StreamData;
		streamData.FangAnData.SaveHandle();
		FileStream fileStream = new FileStream(Paths.GetSavePath() + "/StreamData" + Tools.instance.getSaveID(id, index) + ".sav", FileMode.Create);
		new BinaryFormatter().Serialize(fileStream, streamData);
		fileStream.Close();
	}

	public void createItem()
	{
	}

	public bool hasItem(int itemID)
	{
		bool result = false;
		foreach (ITEM_INFO value in itemList.values)
		{
			if (value.itemId == itemID)
			{
				result = true;
			}
		}
		return result;
	}

	public void SortItem()
	{
		itemList.values.Sort(delegate(ITEM_INFO a, ITEM_INFO b)
		{
			try
			{
				_ItemJsonData itemJsonData = _ItemJsonData.DataDict[a.itemId];
				_ItemJsonData itemJsonData2 = _ItemJsonData.DataDict[b.itemId];
				JSONObject seid = a.Seid;
				JSONObject seid2 = b.Seid;
				int num = itemJsonData.GetHashCode();
				int num2 = itemJsonData2.GetHashCode();
				int num3 = itemJsonData.quality;
				int num4 = itemJsonData2.quality;
				if (seid != null && seid.HasField("quality"))
				{
					num3 = seid["quality"].I;
					num += seid.GetHashCode();
				}
				if (seid2 != null && seid2.HasField("quality"))
				{
					num4 = seid2["quality"].I;
					num2 += seid2.GetHashCode();
				}
				if (itemJsonData.type == 3 || itemJsonData.type == 4)
				{
					num3 *= 2;
				}
				if (itemJsonData2.type == 3 || itemJsonData2.type == 4)
				{
					num4 *= 2;
				}
				if (itemJsonData.type == 0 || itemJsonData.type == 1 || itemJsonData.type == 2)
				{
					num3++;
				}
				if (itemJsonData2.type == 0 || itemJsonData2.type == 1 || itemJsonData2.type == 2)
				{
					num4++;
				}
				if (num3 != num4)
				{
					return num4.CompareTo(num3);
				}
				if (itemJsonData.type != itemJsonData2.type)
				{
					return itemJsonData.type.CompareTo(itemJsonData2.type);
				}
				if (itemJsonData.id != itemJsonData2.id)
				{
					return itemJsonData.id.CompareTo(itemJsonData2.id);
				}
				return num.CompareTo(num2);
			}
			catch
			{
				return 1;
			}
		});
	}

	public static void UnlockShenXianDouFa(int index)
	{
		int num = index + 100;
		int num2 = 10001 + index;
		if (YSGame.YSSaveGame.GetInt("SaveAvatar" + num) == 0)
		{
			UIPopTip.Inst.Pop("已开启新的神仙斗法");
			YSGame.YSSaveGame.save("SaveAvatar" + num, 1);
			YSGame.YSSaveGame.save("SaveDFAvatar" + num, 2);
			JSONObject jSONObject = new JSONObject(JSONObject.Type.OBJECT);
			jSONObject.SetField("1", jsonData.instance.AvatarRandomJsonData[num2.ToString()]);
			YSGame.YSSaveGame.save("AvatarRandomJsonData" + Tools.instance.getSaveID(num, 0), jSONObject);
		}
	}

	public void SetMenPai(int id)
	{
		menPai = (ushort)id;
	}

	public void SetLingGen(int id, int value)
	{
		LingGeng[id] = value;
	}

	[Obsolete]
	public void startFight(int fightID)
	{
	}

	[Obsolete]
	public void reqItemList()
	{
		baseCall("reqItemList");
	}

	[Obsolete]
	public void dropRequest(ulong itemUUID)
	{
		baseCall("dropRequest", itemUUID);
	}

	[Obsolete]
	public void swapItemRequest(int srcIndex, int dstIndex)
	{
		ulong num = itemIndex2Uids[srcIndex];
		ulong num2 = itemIndex2Uids[dstIndex];
		itemIndex2Uids[srcIndex] = num2;
		if (num2 != 0L)
		{
			itemDict[num2].itemIndex = srcIndex;
		}
		itemIndex2Uids[dstIndex] = num;
		if (num != 0L)
		{
			itemDict[num].itemIndex = dstIndex;
		}
		baseCall("swapItemRequest", srcIndex, dstIndex);
	}

	[Obsolete]
	public void equipItemRequest(ulong itemUUID)
	{
		baseCall("equipItemRequest", itemUUID);
	}

	[Obsolete]
	public void UnEquipItemRequest(ulong itemUUID)
	{
		baseCall("UnEquipItemRequest", itemUUID);
	}

	[Obsolete]
	public override object getDefinedProperty(string name)
	{
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		return name switch
		{
			"roleTypeCell" => roleTypeCell, 
			"roleSurfaceCall" => roleSurfaceCall, 
			"position" => position, 
			"HP_Max" => HP_Max, 
			"HP" => HP, 
			"state" => state, 
			"name" => base.name, 
			"equipWeapon" => equipWeapon, 
			"MP" => MP, 
			"MP_Max" => MP_Max, 
			"level" => level, 
			_ => null, 
		};
	}

	[Obsolete]
	public void CreateAvaterCall(int AvaterID)
	{
		baseCall("CreateAvaterCall", AvaterID);
	}

	[Obsolete]
	public void useItemRequest(ulong itemIndex)
	{
		if (!ConsumeLimitCD.instance.isWaiting())
		{
			baseCall("useItemRequest", itemIndex);
			ConsumeLimitCD.instance.Start(2f);
		}
		else
		{
			UIPopTip.Inst.Pop("物品使用冷却中");
		}
	}

	[Obsolete]
	public override void onAttack_MaxChanged(int old)
	{
		object obj = attack_Max;
		Event.fireOut("set_attack_Max", obj);
	}

	[Obsolete]
	public override void onAttack_MinChanged(int old)
	{
		object obj = attack_Min;
		Event.fireOut("set_attack_Min", obj);
	}

	[Obsolete]
	public override void onDefenceChanged(int old)
	{
		object obj = defence;
		Event.fireOut("set_defence", obj);
	}

	[Obsolete]
	public override void onRatingChanged(int old)
	{
		object obj = rating;
		Event.fireOut("set_rating", obj);
	}

	[Obsolete]
	public override void onDodgeChanged(int old)
	{
		object obj = dodge;
		Event.fireOut("set_dodge", obj);
	}

	[Obsolete]
	public override void onStrengthChanged(int old)
	{
		object obj = strength;
		Event.fireOut("set_strength", obj);
	}

	[Obsolete]
	public override void onDexterityChanged(int old)
	{
		object obj = dexterity;
		Event.fireOut("set_dexterity", obj);
	}

	[Obsolete]
	public override void onExpChanged(ulong old)
	{
		object obj = exp;
		Event.fireOut("set_exp", obj);
	}

	[Obsolete]
	public override void onLevelChanged(ushort old)
	{
		object obj = level;
		Event.fireOut("set_level", obj);
	}

	[Obsolete]
	public override void onCrystalChanged(List<int> oldValue)
	{
		Event.fireOut("crtstalChanged", this, oldValue);
	}

	[Obsolete]
	public override void onStaminaChanged(int old)
	{
		object obj = stamina;
		Event.fireOut("set_stamina", obj);
	}

	[Obsolete]
	public void dialog(int targetID, uint dialogID)
	{
		dialogMsg.dialog(targetID, dialogID);
	}

	[Obsolete]
	public void messagelog(int targetID, uint dialogID)
	{
		dialogMsg.messagelog(targetID, dialogID);
	}

	[Obsolete]
	public override void dropItem_re(int itemId, ulong itemUUId)
	{
		int itemIndex = itemDict[itemUUId].itemIndex;
		itemDict.Remove(itemUUId);
		itemIndex2Uids[itemIndex] = 0uL;
		Event.fireOut("dropItem_re", itemIndex, itemUUId);
	}

	[Obsolete]
	public override void pickUp_re(ITEM_INFO itemInfo)
	{
		Event.fireOut("pickUp_re", itemInfo);
		itemDict[itemInfo.UUID] = itemInfo;
	}

	[Obsolete]
	public override void equipItemRequest_re(ITEM_INFO itemInfo, ITEM_INFO equipItemInfo)
	{
		Event.fireOut("equipItemRequest_re", itemInfo, equipItemInfo);
		ulong uUID = itemInfo.UUID;
		ulong uUID2 = equipItemInfo.UUID;
		if (uUID == 0L && uUID2 != 0L)
		{
			equipItemDict[uUID2] = equipItemInfo;
			itemDict.Remove(uUID2);
		}
		else if (uUID != 0L && uUID2 != 0L)
		{
			itemDict.Remove(uUID2);
			equipItemDict[uUID2] = equipItemInfo;
			equipItemDict.Remove(uUID);
			itemDict[uUID] = itemInfo;
		}
		else if (uUID != 0L && uUID2 == 0L)
		{
			equipItemDict.Remove(uUID);
			itemDict[uUID] = itemInfo;
		}
	}

	[Obsolete]
	public override void onReqItemList(ITEM_INFO_LIST infos, ITEM_INFO_LIST equipInfos)
	{
		itemDict.Clear();
		List<ITEM_INFO> values = infos.values;
		for (int i = 0; i < values.Count; i++)
		{
			ITEM_INFO iTEM_INFO = values[i];
			itemDict.Add(iTEM_INFO.UUID, iTEM_INFO);
			itemIndex2Uids[iTEM_INFO.itemIndex] = iTEM_INFO.UUID;
		}
		equipItemDict.Clear();
		List<ITEM_INFO> values2 = equipInfos.values;
		for (int j = 0; j < values2.Count; j++)
		{
			ITEM_INFO iTEM_INFO2 = values2[j];
			equipItemDict.Add(iTEM_INFO2.UUID, iTEM_INFO2);
			equipIndex2Uids[iTEM_INFO2.itemIndex] = iTEM_INFO2.UUID;
		}
		Event.fireOut("onReqItemList", itemDict, equipItemDict);
	}

	[Obsolete]
	public override void errorInfo(int errorCode)
	{
		Dbg.DEBUG_MSG("errorInfo(" + errorCode + ")");
	}

	[Obsolete]
	public virtual void onEquipWeaponChanged(object old)
	{
		object obj = equipWeapon;
		Event.fireOut("set_equipWeapon", this, (int)obj);
	}

	[Obsolete]
	public override void dialog_setContent(int talkerId, List<uint> dialogs, List<string> dialogsTitles, string title, string body, string sayname)
	{
		Event.fireOut("dialog_setContent", talkerId, dialogs, dialogsTitles, title, body, sayname);
	}

	[Obsolete]
	public void messagelog_setContent(int talkerId, string title, string body, string sayname)
	{
		Event.fireOut("messagelog_setContent", talkerId, title, body, sayname);
	}

	[Obsolete]
	public override void dialog_close()
	{
		Event.fireOut("dialog_close");
	}

	[Obsolete]
	public void StartCrafting(int itemID, int Count)
	{
		baseCall("StartCrafting", itemID, Count);
	}

	[Obsolete]
	public void CancelCrafting()
	{
		baseCall("CancelCrafting");
	}

	[Obsolete]
	public void backToHome()
	{
		baseCall("backToHome");
	}

	[Obsolete]
	public override void PlayerAddGoods(ITEM_INFO_LIST Infos, ushort day, ushort exp)
	{
		Event.fireOut("showCollect", Infos, day, exp);
	}

	[Obsolete]
	public override void setPlayerTime(uint time)
	{
		Event.fireOut("setPlayerTime", time);
	}

	[Obsolete]
	public override void GameErrorMsg(string msg)
	{
		Event.fireOut("GameErrorMsg", msg);
	}

	[Obsolete]
	public void DayZombie()
	{
		cellCall("DayZombie");
	}

	[Obsolete]
	public override void PlayerLvUP()
	{
		Event.fireOut("PlayerLvUP");
	}

	[Obsolete]
	public override void createItem(ITEM_INFO arg1)
	{
		throw new NotImplementedException();
	}

	[Obsolete]
	public override void onBuffsChanged(List<ushort> oldValue)
	{
		if (renderObj != null)
		{
			Event.fireOut("set_Buffs", this, oldValue, buffs);
		}
	}

	[Obsolete]
	public override void onStartGame()
	{
		throw new NotImplementedException();
	}

	[Obsolete]
	public override void onHPChanged(int oldValue)
	{
		object definedProperty = getDefinedProperty("HP");
		Event.fireOut("set_HP", this, definedProperty);
	}

	[Obsolete]
	public override void onMPChanged(int oldValue)
	{
		getDefinedProperty("MP");
	}

	[Obsolete]
	public override void on_HP_MaxChanged(int oldValue)
	{
		object definedProperty = getDefinedProperty("HP_Max");
		Event.fireOut("set_HP_Max", this, definedProperty);
	}

	[Obsolete]
	public override void onMP_MaxChanged(int oldValue)
	{
		getDefinedProperty("MP_Max");
	}

	[Obsolete]
	public override void onNameChanged(string oldValue)
	{
		object definedProperty = getDefinedProperty("name");
		Event.fireOut("set_name", this, definedProperty);
	}

	[Obsolete]
	public override void onStateChanged(sbyte oldValue)
	{
		object definedProperty = getDefinedProperty("state");
		Event.fireOut("set_state", this, definedProperty);
	}

	[Obsolete]
	public override void onSubStateChanged(byte oldValue)
	{
	}

	[Obsolete]
	public override void onUtypeChanged(uint oldValue)
	{
	}

	[Obsolete]
	public override void onUidChanged(uint oldValue)
	{
	}

	[Obsolete]
	public override void onSpaceUTypeChanged(uint oldValue)
	{
	}

	[Obsolete]
	public override void onMoveSpeedChanged(byte oldValue)
	{
		getDefinedProperty("moveSpeed");
	}

	[Obsolete]
	public override void onHungerChanged(short oldValue)
	{
		Event.fireOut("set_Hunger", oldValue);
	}

	[Obsolete]
	public override void onThirstChanged(short oldValue)
	{
		Event.fireOut("set_Thirst", oldValue);
	}
}
