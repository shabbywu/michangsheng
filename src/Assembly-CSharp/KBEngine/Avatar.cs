using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;
using Bag;
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

namespace KBEngine
{
	// Token: 0x02000FF5 RID: 4085
	public class Avatar : AvatarBase
	{
		// Token: 0x170008AF RID: 2223
		// (get) Token: 0x06006104 RID: 24836 RVA: 0x00043643 File Offset: 0x00041843
		public new CardMag crystal
		{
			get
			{
				return this.cardMag;
			}
		}

		// Token: 0x170008B0 RID: 2224
		// (get) Token: 0x06006106 RID: 24838 RVA: 0x0026B87C File Offset: 0x00269A7C
		// (set) Token: 0x06006105 RID: 24837 RVA: 0x0004364B File Offset: 0x0004184B
		public int shengShi
		{
			get
			{
				int num = 0;
				foreach (KeyValuePair<int, int> keyValuePair in this.fightTemp.tempShenShi)
				{
					num += keyValuePair.Value;
				}
				return this._shengShi + num + this.getStaticSkillAddSum(2) + this.getEquipAddSum(4);
			}
			set
			{
				this._shengShi = value;
			}
		}

		// Token: 0x170008B1 RID: 2225
		// (get) Token: 0x06006108 RID: 24840 RVA: 0x0026B8F4 File Offset: 0x00269AF4
		// (set) Token: 0x06006107 RID: 24839 RVA: 0x00043654 File Offset: 0x00041854
		public int dunSu
		{
			get
			{
				int num = 0;
				foreach (KeyValuePair<int, int> keyValuePair in this.fightTemp.tempDunSu)
				{
					num += keyValuePair.Value;
				}
				return this._dunSu + num + this.getStaticSkillAddSum(8) + this.getEquipAddSum(8);
			}
			set
			{
				this._dunSu = value;
			}
		}

		// Token: 0x170008B2 RID: 2226
		// (get) Token: 0x0600610A RID: 24842 RVA: 0x0026B96C File Offset: 0x00269B6C
		// (set) Token: 0x06006109 RID: 24841 RVA: 0x0004365D File Offset: 0x0004185D
		public int HP_Max
		{
			get
			{
				int num = 0;
				foreach (KeyValuePair<int, int> keyValuePair in this.fightTemp.TempHP_Max)
				{
					num += keyValuePair.Value;
				}
				return this._HP_Max + num + this.getStaticSkillAddSum(3) + this.getEquipAddSum(3) + this.getJieDanSkillAddHP();
			}
			set
			{
				this._HP_Max = value;
			}
		}

		// Token: 0x170008B3 RID: 2227
		// (get) Token: 0x0600610B RID: 24843 RVA: 0x0026B9E8 File Offset: 0x00269BE8
		public List<int> GetLingGeng
		{
			get
			{
				List<int> temp = new List<int>();
				this.LingGeng.ForEach(delegate(int aa)
				{
					temp.Add(aa);
				});
				foreach (KeyValuePair<int, int> keyValuePair in this.getJieDanAddLingGen())
				{
					List<int> temp2 = temp;
					int key = keyValuePair.Key;
					temp2[key] += keyValuePair.Value;
				}
				return temp;
			}
		}

		// Token: 0x170008B4 RID: 2228
		// (get) Token: 0x0600610D RID: 24845 RVA: 0x0004366F File Offset: 0x0004186F
		// (set) Token: 0x0600610C RID: 24844 RVA: 0x00043666 File Offset: 0x00041866
		public int xinjin
		{
			get
			{
				return this._xinjin;
			}
			set
			{
				this._xinjin = value;
			}
		}

		// Token: 0x170008B5 RID: 2229
		// (get) Token: 0x0600610F RID: 24847 RVA: 0x0004368F File Offset: 0x0004188F
		// (set) Token: 0x0600610E RID: 24846 RVA: 0x00043677 File Offset: 0x00041877
		public new int ZiZhi
		{
			get
			{
				return this.ZiZhi + this.getStaticSkillAddSum(6);
			}
			set
			{
				this.ZiZhi = ((value > 200) ? 200 : value);
			}
		}

		// Token: 0x170008B6 RID: 2230
		// (get) Token: 0x06006111 RID: 24849 RVA: 0x000436B7 File Offset: 0x000418B7
		// (set) Token: 0x06006110 RID: 24848 RVA: 0x0004369F File Offset: 0x0004189F
		public new uint wuXin
		{
			get
			{
				return this.wuXin + (uint)this.getStaticSkillAddSum(7);
			}
			set
			{
				this.wuXin = ((value > 200U) ? 200U : value);
			}
		}

		// Token: 0x170008B7 RID: 2231
		// (get) Token: 0x06006113 RID: 24851 RVA: 0x000436E7 File Offset: 0x000418E7
		// (set) Token: 0x06006112 RID: 24850 RVA: 0x000436C7 File Offset: 0x000418C7
		public int ZhuJiJinDu
		{
			get
			{
				return this._ZhuJiJinDu;
			}
			set
			{
				this._ZhuJiJinDu = value;
				if (ZhuJiManager.inst != null)
				{
					ZhuJiManager.inst.updateJinDu();
				}
			}
		}

		// Token: 0x170008B8 RID: 2232
		// (get) Token: 0x06006114 RID: 24852 RVA: 0x000436EF File Offset: 0x000418EF
		public int NowDrawCardNum
		{
			get
			{
				return (int)jsonData.instance.DrawCardToLevelJsonData[string.Concat(this.getLevelType())]["rundDraw"].n;
			}
		}

		// Token: 0x170008B9 RID: 2233
		// (get) Token: 0x06006115 RID: 24853 RVA: 0x00043720 File Offset: 0x00041920
		public int NowStartCardNum
		{
			get
			{
				return (int)jsonData.instance.DrawCardToLevelJsonData[string.Concat(this.getLevelType())]["StartCard"].n;
			}
		}

		// Token: 0x170008BA RID: 2234
		// (get) Token: 0x06006116 RID: 24854 RVA: 0x0026BA8C File Offset: 0x00269C8C
		public uint NowCard
		{
			get
			{
				if (this.BuffSeidFlag.ContainsKey(23))
				{
					return 0U;
				}
				return (uint)Mathf.Clamp((int)jsonData.instance.DrawCardToLevelJsonData[string.Concat(this.getLevelType())]["MaxDraw"].n + this.fightTemp.tempNowCard, 0, 99999);
			}
		}

		// Token: 0x170008BB RID: 2235
		// (get) Token: 0x06006118 RID: 24856 RVA: 0x0004375A File Offset: 0x0004195A
		// (set) Token: 0x06006117 RID: 24855 RVA: 0x00043751 File Offset: 0x00041951
		public int WuDaoDian
		{
			get
			{
				return this._WuDaoDian;
			}
			set
			{
				this._WuDaoDian = value;
			}
		}

		// Token: 0x170008BC RID: 2236
		// (get) Token: 0x0600611A RID: 24858 RVA: 0x00043770 File Offset: 0x00041970
		// (set) Token: 0x06006119 RID: 24857 RVA: 0x00043762 File Offset: 0x00041962
		public List<int> NowRoundUsedCard
		{
			get
			{
				return this.fightTemp.NowRoundUsedCard;
			}
			set
			{
				this.fightTemp.NowRoundUsedCard = value;
			}
		}

		// Token: 0x170008BD RID: 2237
		// (get) Token: 0x0600611C RID: 24860 RVA: 0x0004378B File Offset: 0x0004198B
		// (set) Token: 0x0600611B RID: 24859 RVA: 0x0004377D File Offset: 0x0004197D
		public List<int> UsedSkills
		{
			get
			{
				return this.fightTemp.UsedSkills;
			}
			set
			{
				this.fightTemp.UsedSkills = value;
			}
		}

		// Token: 0x170008BE RID: 2238
		// (get) Token: 0x0600611D RID: 24861 RVA: 0x00043798 File Offset: 0x00041998
		public int useSkillNum
		{
			get
			{
				return this.fightTemp.NowRoundUsedSkills.Count;
			}
		}

		// Token: 0x0600611E RID: 24862 RVA: 0x000437AA File Offset: 0x000419AA
		public int getStaticSkillAddSum(int seid)
		{
			return 0 + this.DictionyGetSum(this.StaticSkillSeidFlag, seid) + this.DictionyGetSum(this.wuDaoMag.WuDaoSkillSeidFlag, seid) + this.DictionyGetSum(this.JieDanSkillSeidFlag, seid);
		}

		// Token: 0x0600611F RID: 24863 RVA: 0x0026BAF0 File Offset: 0x00269CF0
		public int getEquipAddSum(int seid)
		{
			int num = 0;
			if (!this.EquipSeidFlag.ContainsKey(seid))
			{
				return 0;
			}
			foreach (KeyValuePair<int, int> keyValuePair in this.EquipSeidFlag[seid])
			{
				num += keyValuePair.Value;
			}
			return num;
		}

		// Token: 0x06006120 RID: 24864 RVA: 0x0026BB60 File Offset: 0x00269D60
		public int getJieDanSkillAddHP()
		{
			int num = 0;
			foreach (SkillItem skillItem in this.hasJieDanSkillList)
			{
				num += this.GetLeveUpAddHPMax((int)jsonData.instance.JieDanBiao[skillItem.itemId.ToString()]["HP"].n);
			}
			int num2 = (int)((float)num * ((float)this.getStaticSkillAddSum(12) / 100f));
			if (this.level >= 10)
			{
				num *= 2;
			}
			return num + num2;
		}

		// Token: 0x06006121 RID: 24865 RVA: 0x0026BC08 File Offset: 0x00269E08
		public float getJieDanSkillAddExp()
		{
			int num = 100;
			foreach (SkillItem skillItem in this.hasJieDanSkillList)
			{
				if (this.level >= 10)
				{
					num += (int)jsonData.instance.JieDanBiao[skillItem.itemId.ToString()]["EXP"].n * 2;
				}
				else
				{
					num += (int)jsonData.instance.JieDanBiao[skillItem.itemId.ToString()]["EXP"].n;
				}
			}
			return (float)num / 100f;
		}

		// Token: 0x06006122 RID: 24866 RVA: 0x0026BCC8 File Offset: 0x00269EC8
		public Dictionary<int, int> getJieDanAddLingGen()
		{
			Dictionary<int, int> dictionary = new Dictionary<int, int>();
			foreach (SkillItem skillItem in this.hasJieDanSkillList)
			{
				int num = 0;
				JSONObject jsonobject = jsonData.instance.JieDanBiao[skillItem.itemId.ToString()];
				foreach (JSONObject jsonobject2 in jsonobject["LinGengType"].list)
				{
					dictionary[jsonobject2.I] = jsonobject["LinGengZongShu"][num].I;
					num++;
				}
			}
			return dictionary;
		}

		// Token: 0x06006123 RID: 24867 RVA: 0x000437DC File Offset: 0x000419DC
		public int GetBaseShenShi()
		{
			return this._shengShi + this.getStaticSkillAddSum(2) + this.getEquipAddSum(4);
		}

		// Token: 0x06006124 RID: 24868 RVA: 0x000437F4 File Offset: 0x000419F4
		public int GetBaseDunSu()
		{
			return this._dunSu + this.getStaticSkillAddSum(8) + this.getEquipAddSum(8);
		}

		// Token: 0x06006125 RID: 24869 RVA: 0x0004380C File Offset: 0x00041A0C
		public Avatar cloneAvatar()
		{
			return base.MemberwiseClone() as Avatar;
		}

		// Token: 0x06006126 RID: 24870 RVA: 0x0026BDB0 File Offset: 0x00269FB0
		public int DictionyGetSum(Dictionary<int, Dictionary<int, int>> seidflag, int seid)
		{
			int num = 0;
			if (!seidflag.ContainsKey(seid))
			{
				return 0;
			}
			foreach (KeyValuePair<int, int> keyValuePair in seidflag[seid])
			{
				num += keyValuePair.Value;
			}
			return num;
		}

		// Token: 0x06006127 RID: 24871 RVA: 0x0026BE18 File Offset: 0x0026A018
		public int RandomSeedNext()
		{
			int num = new Random(PlayerEx.Player.RandomSeed.I).Next();
			this.RandomSeed = new JSONObject(num);
			return num;
		}

		// Token: 0x06006128 RID: 24872 RVA: 0x0026BE4C File Offset: 0x0026A04C
		public void AddFriend(int npcId)
		{
			if (npcId == 0)
			{
				return;
			}
			if (NpcJieSuanManager.inst.ImportantNpcBangDingDictionary.ContainsKey(npcId))
			{
				npcId = NpcJieSuanManager.inst.ImportantNpcBangDingDictionary[npcId];
			}
			if (this.emailDateMag.IsFriend(npcId))
			{
				return;
			}
			this.emailDateMag.cyNpcList.Add(npcId);
		}

		// Token: 0x06006129 RID: 24873 RVA: 0x0026BEA4 File Offset: 0x0026A0A4
		public void AddLingGan(int addNum)
		{
			this.LingGan += addNum;
			int lingGanMax = this.GetLingGanMax();
			if (this.LingGan > lingGanMax)
			{
				this.LingGan = lingGanMax;
			}
			this.LunDaoState = this.GetLunDaoState();
		}

		// Token: 0x0600612A RID: 24874 RVA: 0x00043819 File Offset: 0x00041A19
		public void ReduceLingGan(int num)
		{
			this.LingGan -= num;
			if (this.LingGan < 0)
			{
				this.LingGan = 0;
			}
			this.LunDaoState = this.GetLunDaoState();
		}

		// Token: 0x0600612B RID: 24875 RVA: 0x0026BEE4 File Offset: 0x0026A0E4
		public int GetLingGanMax()
		{
			return jsonData.instance.LingGanMaxData[this.GetXinJingLevel().ToString()]["lingGanShangXian"].I;
		}

		// Token: 0x0600612C RID: 24876 RVA: 0x0026BF20 File Offset: 0x0026A120
		public int GetLunDaoState()
		{
			foreach (JSONObject jsonobject in jsonData.instance.LingGanLevelData.list)
			{
				if (this.LingGan >= jsonobject["lingGanQuJian"].I)
				{
					return jsonobject["id"].I;
				}
			}
			return 0;
		}

		// Token: 0x0600612D RID: 24877 RVA: 0x0026BFA4 File Offset: 0x0026A1A4
		public bool ISStudyDanFan(int id)
		{
			if (!jsonData.instance.LianDanDanFangBiao.HasField(id.ToString()))
			{
				Debug.LogError("丹方出错丹方表ID：" + id);
				return false;
			}
			JSONObject jsonobject = jsonData.instance.LianDanDanFangBiao[id.ToString()];
			List<int> danyao = new List<int>();
			List<int> num = new List<int>();
			this.getDanYaoTypeAndNum(id, danyao, num);
			return this.getDanFang((int)jsonobject["ItemID"].n, danyao, num) != null;
		}

		// Token: 0x0600612E RID: 24878 RVA: 0x0026C02C File Offset: 0x0026A22C
		public int ItemSeid27Days()
		{
			if (this.ItemBuffList.ContainsKey("27") && (bool)this.ItemBuffList["27"]["start"])
			{
				string s = (string)this.ItemBuffList["27"]["StartTime"];
				(int)this.ItemBuffList["27"]["AIType"];
				int months = (int)this.ItemBuffList["27"]["ContinueTime"];
				DateTime startTime = DateTime.Parse(s);
				DateTime nowTime = this.worldTimeMag.getNowTime();
				DateTime dateTime = startTime.AddMonths(months);
				if (Tools.instance.IsInTime(nowTime, startTime, dateTime, 0))
				{
					return (dateTime - nowTime).Days;
				}
				this.ItemBuffList["27"]["start"] = false;
			}
			return 0;
		}

		// Token: 0x0600612F RID: 24879 RVA: 0x0026C130 File Offset: 0x0026A330
		public void SetMenPaiHaoGandu(int MenPaiID, int Value)
		{
			int num = this.MenPaiHaoGanDu.HasField(string.Concat(MenPaiID)) ? ((int)this.MenPaiHaoGanDu[string.Concat(MenPaiID)].n) : 0;
			this.MenPaiHaoGanDu.SetField(string.Concat(MenPaiID), num + Value);
			if (Value > 0)
			{
				UIPopTip.Inst.Pop(string.Format("你在{0}的声望提升了{1}", ShiLiHaoGanDuName.DataDict[MenPaiID].ChinaText, Value), PopTipIconType.上箭头);
				return;
			}
			if (Value < 0)
			{
				UIPopTip.Inst.Pop(string.Format("你在{0}的声望下降了{1}", ShiLiHaoGanDuName.DataDict[MenPaiID].ChinaText, -Value), PopTipIconType.下箭头);
			}
		}

		// Token: 0x06006130 RID: 24880 RVA: 0x00043845 File Offset: 0x00041A45
		public void setAvatarHaoGandu(int AvatarID, int AddHaoGanduNum)
		{
			NPCEx.AddFavor(AvatarID, AddHaoGanduNum, true, true);
		}

		// Token: 0x06006131 RID: 24881 RVA: 0x0026C1F0 File Offset: 0x0026A3F0
		public void getDanYaoTypeAndNum(int id, List<int> danyao, List<int> num)
		{
			JSONObject jsonobject = jsonData.instance.LianDanDanFangBiao[id.ToString()];
			for (int i = 1; i <= 5; i++)
			{
				danyao.Add((int)jsonobject["value" + i].n);
				num.Add((int)jsonobject["num" + i].n);
			}
		}

		// Token: 0x06006132 RID: 24882 RVA: 0x0026C264 File Offset: 0x0026A464
		public JSONObject getDanFang(int danyaoID, List<int> danyao, List<int> num)
		{
			return Tools.instance.getPlayer().DanFang.list.Find(delegate(JSONObject aa)
			{
				if (danyaoID == (int)aa["ID"].n)
				{
					bool flag = true;
					for (int i = 0; i < aa["Type"].list.Count; i++)
					{
						if (danyao[i] != (int)aa["Type"][i].n)
						{
							flag = false;
						}
						if (num[i] != (int)aa["Num"][i].n)
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

		// Token: 0x06006133 RID: 24883 RVA: 0x0026C2B4 File Offset: 0x0026A4B4
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
			}
			Debug.LogError(string.Format("不存在该门派{0}", menpai));
			return 9;
		}

		// Token: 0x06006134 RID: 24884 RVA: 0x0026C314 File Offset: 0x0026A514
		public int GetZhangMenId(int shili)
		{
			int zhangMenChengHaoId = Tools.instance.getPlayer().GetZhangMenChengHaoId(shili);
			foreach (JSONObject jsonobject in jsonData.instance.AvatarJsonData.list)
			{
				int i = jsonobject["id"].I;
				if (i >= 20000 && jsonobject["ChengHaoID"].I == zhangMenChengHaoId)
				{
					return i;
				}
			}
			Debug.LogError(string.Format("不存在当前势力的掌门，势力Id{0}", shili));
			return -1;
		}

		// Token: 0x06006135 RID: 24885 RVA: 0x0026C3C8 File Offset: 0x0026A5C8
		public void AddDandu(int num)
		{
			this.Dandu += (this.TianFuID.HasField(string.Concat(18)) ? (num * 2) : num);
			if (this.Dandu >= 120)
			{
				UIDeath.Inst.Show(DeathType.毒发身亡);
			}
		}

		// Token: 0x06006136 RID: 24886 RVA: 0x0026C418 File Offset: 0x0026A618
		public void AddYaoCaiShuXin(int itemID, int index)
		{
			if (!this.YaoCaiShuXin.HasField(itemID + "_" + index))
			{
				if (index == 1)
				{
					TuJianManager.Inst.UnlockYaoYin(itemID);
				}
				else if (index == 2)
				{
					TuJianManager.Inst.UnlockZhuYao(itemID);
				}
				else if (index == 3)
				{
					TuJianManager.Inst.UnlockFuYao(itemID);
				}
				this.YaoCaiShuXin.AddField(itemID + "_" + index, 1);
			}
		}

		// Token: 0x06006137 RID: 24887 RVA: 0x00043850 File Offset: 0x00041A50
		public bool hasYaocaiShuXin(int itemID, int index)
		{
			return this.YaoCaiShuXin.HasField(itemID + "_" + index);
		}

		// Token: 0x06006138 RID: 24888 RVA: 0x0026C49C File Offset: 0x0026A69C
		public bool getItemHasTianFu15(int quality)
		{
			return this.TianFuID.HasField(string.Concat(15)) && this.TianFuID["15"].list.Find((JSONObject aa) => (int)aa.n == quality) != null;
		}

		// Token: 0x06006139 RID: 24889 RVA: 0x0026C4FC File Offset: 0x0026A6FC
		public int getZhuXiuSkill()
		{
			foreach (SkillItem skillItem in this.equipStaticSkillList)
			{
				if (skillItem.itemIndex == 0)
				{
					return Tools.instance.getStaticSkillKeyByID(skillItem.itemId);
				}
			}
			return -1;
		}

		// Token: 0x0600613A RID: 24890 RVA: 0x0026C568 File Offset: 0x0026A768
		public bool GetHasYaoYinShuXin(int itemID, int quality)
		{
			bool itemHasTianFu = this.getItemHasTianFu15(quality);
			return this.hasYaocaiShuXin(itemID, 1) || itemHasTianFu;
		}

		// Token: 0x0600613B RID: 24891 RVA: 0x0026C588 File Offset: 0x0026A788
		public bool GetHasZhuYaoShuXin(int itemID, int quality)
		{
			bool itemHasTianFu = this.getItemHasTianFu15(quality);
			return this.hasYaocaiShuXin(itemID, 2) || itemHasTianFu;
		}

		// Token: 0x0600613C RID: 24892 RVA: 0x0026C5A8 File Offset: 0x0026A7A8
		public bool GetHasFuYaoShuXin(int itemID, int quality)
		{
			bool itemHasTianFu = this.getItemHasTianFu15(quality);
			return this.hasYaocaiShuXin(itemID, 3) || itemHasTianFu;
		}

		// Token: 0x0600613D RID: 24893 RVA: 0x00043873 File Offset: 0x00041A73
		public void UnLockCaoYaoData(int caoYaoId)
		{
			this.AddYaoCaiShuXin(caoYaoId, 1);
			this.AddYaoCaiShuXin(caoYaoId, 2);
			this.AddYaoCaiShuXin(caoYaoId, 3);
		}

		// Token: 0x0600613E RID: 24894 RVA: 0x0026C5C8 File Offset: 0x0026A7C8
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
			if (this.DanFang.list.Find(delegate(JSONObject aa)
			{
				if (danyaoID == (int)aa["ID"].n)
				{
					bool flag = true;
					for (int j = 0; j < aa["Type"].list.Count; j++)
					{
						if (yaolei[j] != (int)aa["Type"][j].n)
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
			JSONObject jsonobject = new JSONObject(JSONObject.Type.OBJECT);
			JSONObject jsonobject2 = new JSONObject(JSONObject.Type.ARRAY);
			JSONObject jsonobject3 = new JSONObject(JSONObject.Type.ARRAY);
			foreach (int val in yaolei)
			{
				jsonobject2.Add(val);
			}
			foreach (int val2 in YaoLeiNum)
			{
				jsonobject3.Add(val2);
			}
			jsonobject.AddField("ID", danyaoID);
			jsonobject.AddField("Type", jsonobject2);
			jsonobject.AddField("Num", jsonobject3);
			this.DanFang.Add(jsonobject);
		}

		// Token: 0x0600613F RID: 24895 RVA: 0x0004388D File Offset: 0x00041A8D
		public void statiReduceDandu(int num)
		{
			this.Dandu -= num;
			if (this.Dandu < 0)
			{
				this.Dandu = 0;
			}
		}

		// Token: 0x06006140 RID: 24896 RVA: 0x0026C740 File Offset: 0x0026A940
		public void AddMoney(int AddNum)
		{
			if (AddNum == 0)
			{
				return;
			}
			int num = (int)this.money + AddNum;
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
				this.money = (ulong)num;
				return;
			}
			this.money = 0UL;
		}

		// Token: 0x06006141 RID: 24897 RVA: 0x0026C7C8 File Offset: 0x0026A9C8
		public void ReduceDandu(int num)
		{
			int danDuLevel = this.GetDanDuLevel();
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
			this.Dandu -= num * num2;
			this.Dandu -= num * this.getStaticSkillAddSum(11);
			if (this.Dandu < 0)
			{
				this.Dandu = 0;
			}
		}

		// Token: 0x06006142 RID: 24898 RVA: 0x000438AD File Offset: 0x00041AAD
		public int GetDanDuLevel()
		{
			if (this.Dandu >= 120)
			{
				return 5;
			}
			if (this.Dandu >= 100)
			{
				return 4;
			}
			if (this.Dandu >= 70)
			{
				return 3;
			}
			if (this.Dandu >= 50)
			{
				return 2;
			}
			if (this.Dandu >= 20)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x06006143 RID: 24899 RVA: 0x0026C82C File Offset: 0x0026AA2C
		public int GetXinJingLevel()
		{
			foreach (JSONObject jsonobject in jsonData.instance.XinJinJsonData.list)
			{
				if ((int)jsonobject["Max"].n > this.xinjin)
				{
					return (int)jsonobject["id"].n;
				}
			}
			return jsonData.instance.XinJinJsonData.Count;
		}

		// Token: 0x06006144 RID: 24900 RVA: 0x0026C8C0 File Offset: 0x0026AAC0
		public int getXinJinGuanlianType()
		{
			int xinJingLevel = this.GetXinJingLevel();
			int levelType = this.getLevelType();
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

		// Token: 0x06006145 RID: 24901 RVA: 0x000438EC File Offset: 0x00041AEC
		public int getLevelType()
		{
			return (int)((this.level - 1) / 3 + 1);
		}

		// Token: 0x06006146 RID: 24902 RVA: 0x000438FA File Offset: 0x00041AFA
		public void setSkillConfigIndex(int index)
		{
			this.nowConfigEquipSkill = index;
			this.equipSkillList = this.configEquipSkill[index];
		}

		// Token: 0x06006147 RID: 24903 RVA: 0x00043911 File Offset: 0x00041B11
		public void setStatikConfigIndex(int index)
		{
			this.nowConfigEquipStaticSkill = index;
			this.equipStaticSkillList = this.configEquipStaticSkill[index];
		}

		// Token: 0x06006148 RID: 24904 RVA: 0x0026C8EC File Offset: 0x0026AAEC
		public void setItemConfigIndex(int index)
		{
			this.configEquipItem[this.nowConfigEquipItem].values.Clear();
			this.equipItemList.values.ForEach(delegate(ITEM_INFO i)
			{
				this.configEquipItem[this.nowConfigEquipItem].values.Add(i);
			});
			this.nowConfigEquipItem = index;
			this.equipItemList.values = new List<ITEM_INFO>();
			foreach (ITEM_INFO item_INFO in this.configEquipItem[index].values)
			{
				if (item_INFO.itemId != 0)
				{
					ITEM_INFO item = this.FindItemByUUID(item_INFO.uuid);
					this.equipItemList.values.Add(item);
				}
			}
			PlayerBeiBaoManager.inst.restartEquips();
			Singleton.inventory.LoadInventory();
		}

		// Token: 0x06006149 RID: 24905 RVA: 0x0026C9C4 File Offset: 0x0026ABC4
		public void addHasSkillList(int SkillId)
		{
			using (List<SkillItem>.Enumerator enumerator = this.hasSkillList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.itemId == SkillId)
					{
						return;
					}
				}
			}
			SkillItem skillItem = new SkillItem();
			skillItem.itemId = SkillId;
			this.hasSkillList.Add(skillItem);
			if (base.isPlayer())
			{
				TuJianManager.Inst.UnlockSkill(SkillId);
			}
		}

		// Token: 0x0600614A RID: 24906 RVA: 0x0026CA48 File Offset: 0x0026AC48
		public void addHasStaticSkillList(int SkillId, int _level = 1)
		{
			using (List<SkillItem>.Enumerator enumerator = this.hasStaticSkillList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.itemId == SkillId)
					{
						return;
					}
				}
			}
			SkillItem skillItem = new SkillItem();
			skillItem.itemId = SkillId;
			skillItem.level = _level;
			this.hasStaticSkillList.Add(skillItem);
			if (base.isPlayer())
			{
				TuJianManager.Inst.UnlockGongFa(SkillId);
			}
		}

		// Token: 0x0600614B RID: 24907 RVA: 0x0026CAD0 File Offset: 0x0026ACD0
		public void addJieDanSkillList(int SkillId)
		{
			using (List<SkillItem>.Enumerator enumerator = this.hasJieDanSkillList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.itemId == SkillId)
					{
						return;
					}
				}
			}
			SkillItem skillItem = new SkillItem();
			skillItem.itemId = SkillId;
			this.hasJieDanSkillList.Add(skillItem);
		}

		// Token: 0x0600614C RID: 24908 RVA: 0x00043928 File Offset: 0x00041B28
		public void MonstarEndRound()
		{
			RoundManager.instance.autoRemoveCard(this);
			Event.fireOut("endRound", new object[]
			{
				this
			});
		}

		// Token: 0x0600614D RID: 24909 RVA: 0x00043949 File Offset: 0x00041B49
		public void AvatarEndRound()
		{
			if (base.isPlayer())
			{
				RoundManager.instance.PlayerEndRound(true);
				return;
			}
			this.MonstarEndRound();
		}

		// Token: 0x0600614E RID: 24910 RVA: 0x00043965 File Offset: 0x00041B65
		public void joinMenPai(int menPaiID)
		{
			this.menPai = (ushort)menPaiID;
		}

		// Token: 0x0600614F RID: 24911 RVA: 0x0004396F File Offset: 0x00041B6F
		public void AllMapAddHP(int num, DeathType Type = DeathType.身死道消)
		{
			this.HP += num;
			if (this.HP > this.HP_Max)
			{
				this.HP = this.HP_Max;
			}
			if (this.HP <= 0)
			{
				UIDeath.Inst.Show(Type);
			}
		}

		// Token: 0x06006150 RID: 24912 RVA: 0x000439AD File Offset: 0x00041BAD
		public void AllMapAddHPMax(int num)
		{
			this._HP_Max += num;
		}

		// Token: 0x06006151 RID: 24913 RVA: 0x000439BD File Offset: 0x00041BBD
		public void addShenShi(int num)
		{
			this.shengShi = (int)this.ADDIntToUint((uint)this._shengShi, num);
		}

		// Token: 0x06006152 RID: 24914 RVA: 0x000439D2 File Offset: 0x00041BD2
		public void addShoYuan(int num)
		{
			this.shouYuan = this.ADDIntToUint(this.shouYuan, num);
		}

		// Token: 0x06006153 RID: 24915 RVA: 0x000439E7 File Offset: 0x00041BE7
		public void addShaQi(int num)
		{
			this.shaQi = this.ADDIntToUint(this.shaQi, num);
		}

		// Token: 0x06006154 RID: 24916 RVA: 0x000439FC File Offset: 0x00041BFC
		public void addZiZhi(int num)
		{
			this.ZiZhi = (int)this.ADDIntToUint((uint)this.ZiZhi, num);
		}

		// Token: 0x06006155 RID: 24917 RVA: 0x00043A11 File Offset: 0x00041C11
		public void addWuXin(int num)
		{
			this.wuXin = this.ADDIntToUint(this.wuXin, num);
		}

		// Token: 0x06006156 RID: 24918 RVA: 0x0026CB40 File Offset: 0x0026AD40
		public uint ADDIntToUint(uint real, int value)
		{
			int num = (int)(real + (uint)value);
			if (num < 0)
			{
				real = 0U;
			}
			else
			{
				real = (uint)num;
			}
			return real;
		}

		// Token: 0x06006157 RID: 24919 RVA: 0x0026CB60 File Offset: 0x0026AD60
		public void AddTime(int addday, int addMonth = 0, int Addyear = 0)
		{
			DateTime nowTime = this.worldTimeMag.getNowTime();
			DateTime dateTime = this.worldTimeMag.addTime(addday, addMonth, Addyear);
			this.zulinContorl.addTime(addday, addMonth, Addyear);
			this.RefreshSeaBossData();
			int num = (dateTime.Year - nowTime.Year) * 12 + (dateTime.Month - nowTime.Month);
			int num2 = dateTime.Year - nowTime.Year;
			if (dateTime.Year - nowTime.Year > 0)
			{
				this.age += (uint)(dateTime.Year - nowTime.Year);
			}
			if (this.age > this.shouYuan)
			{
				UIDeath.Inst.Show(DeathType.寿元已尽);
				return;
			}
			if (num > 0)
			{
				if (this.equipStaticSkillList != null)
				{
					float num3 = this.getTimeExpSpeed() * (float)num;
					this.addEXP((int)num3);
				}
				this.AddLingGan(num);
				DongFuManager.LingTianAddTime(num);
			}
			if (num2 > 0)
			{
				this.ReduceDandu(num2);
				try
				{
					this.chenghaomag.TimeAddMoney(num2);
					this.randomFuBenMag.AutoSetRandomFuBen();
				}
				catch (Exception ex)
				{
					Debug.LogError(ex);
				}
			}
			try
			{
				Loom.RunAsync(delegate
				{
					object updateTask = LockMag.updateTask;
					lock (updateTask)
					{
						this.nomelTaskMag.restAllTaskType();
						this.nomelTaskMag.ResetAllStaticNTask();
						this.seaNodeMag.SetLuanLiuLv();
						Loom.QueueOnMainThread(delegate(object obj)
						{
							GaoShiManager.OnAddTime();
						}, null);
					}
				});
				this.wuDaoMag.AutoReomveLingGuang();
				if (this.taskMag._TaskData.Count > 0)
				{
					JSONObject jsonobject = this.taskMag._TaskData["Task"];
					foreach (string text in jsonobject.keys)
					{
						if (!CyRandomTaskData.DataDict.ContainsKey(int.Parse(text)))
						{
							bool flag = false;
							if ((!jsonobject[text].HasField("disableTask") || !jsonobject[text]["disableTask"].b) && jsonobject[text].HasField("curTime"))
							{
								DateTime startTime = DateTime.Parse(jsonobject[text]["curTime"].str);
								if (jsonobject[text]["continueTime"].I > 0)
								{
									DateTime endTime = startTime.AddMonths(jsonobject[text]["continueTime"].I);
									if (!Tools.instance.IsInTime(this.worldTimeMag.getNowTime(), startTime, endTime, 0))
									{
										jsonobject[text].SetField("disableTask", true);
										flag = true;
									}
								}
								if (!flag && jsonobject[text].HasField("EndTime") && !Tools.instance.IsInTime(this.worldTimeMag.nowTime, jsonobject[text]["EndTime"].str))
								{
									jsonobject[text].SetField("disableTask", true);
								}
							}
						}
					}
					this.StreamData.TaskMag.CheckHasOut();
				}
			}
			catch (Exception ex2)
			{
				Debug.LogError(ex2);
			}
			if (MapNodeManager.inst != null)
			{
				MapNodeManager.inst.UpdateAllNode();
			}
			this.updateChuanYingFu();
			this.StreamData.PaiMaiDataMag.AuToUpDate();
		}

		// Token: 0x06006158 RID: 24920 RVA: 0x0026CED4 File Offset: 0x0026B0D4
		public void RefreshSeaBossData()
		{
			foreach (SeaHaiYuJiZhiShuaXin seaHaiYuJiZhiShuaXin in SeaHaiYuJiZhiShuaXin.DataList)
			{
				if (!this.EndlessSeaBoss.HasField(seaHaiYuJiZhiShuaXin.id.ToString()))
				{
					JSONObject jsonobject = new JSONObject(JSONObject.Type.OBJECT);
					jsonobject.SetField("CD", 0);
					jsonobject.SetField("LastTime", this.worldTimeMag.nowTime);
					this.EndlessSeaBoss.SetField(seaHaiYuJiZhiShuaXin.id.ToString(), jsonobject);
				}
				JSONObject jsonobject2 = this.EndlessSeaBoss[seaHaiYuJiZhiShuaXin.id.ToString()];
				DateTime dateTime = DateTime.Parse(jsonobject2["LastTime"].str);
				DateTime nowTime = this.worldTimeMag.getNowTime();
				int i = jsonobject2["CD"].I;
				if (nowTime >= dateTime.AddYears(i))
				{
					JSONObject jsonobject3 = new JSONObject(JSONObject.Type.OBJECT);
					int num = Random.Range(seaHaiYuJiZhiShuaXin.CD[0], seaHaiYuJiZhiShuaXin.CD[1]);
					jsonobject3.SetField("CD", num);
					jsonobject3.SetField("LastTime", this.worldTimeMag.nowTime);
					int num2 = seaHaiYuJiZhiShuaXin.ID[this.RandomSeedNext() % seaHaiYuJiZhiShuaXin.ID.Count];
					jsonobject3.SetField("JiZhiID", num2);
					SeaJiZhiID seaJiZhiID = SeaJiZhiID.DataDict[num2];
					if (seaJiZhiID.Type == 0)
					{
						int val = seaJiZhiID.AvatarID[this.RandomSeedNext() % seaJiZhiID.AvatarID.Count];
						jsonobject3.SetField("AvatarID", val);
					}
					int num3 = seaHaiYuJiZhiShuaXin.WeiZhi[this.RandomSeedNext() % seaHaiYuJiZhiShuaXin.WeiZhi.Count];
					jsonobject3.SetField("Pos", num3);
					jsonobject3.SetField("Close", false);
					this.EndlessSeaBoss.SetField(seaHaiYuJiZhiShuaXin.id.ToString(), jsonobject3);
					if (seaJiZhiID.Type == 0)
					{
						Debug.Log(string.Format("海域{0}坐标{1}刷新了boss{2}，机制ID{3}，刷新时间{4}，刷新CD{5}", new object[]
						{
							seaHaiYuJiZhiShuaXin.id,
							num3,
							jsonobject3["AvatarID"].I,
							seaJiZhiID.id,
							this.worldTimeMag.nowTime,
							num
						}));
					}
					else if (seaJiZhiID.Type == 1)
					{
						Debug.Log(string.Format("海域{0}坐标{1}刷新了副本{2}，机制ID{3}，刷新时间{4}，刷新CD{5}", new object[]
						{
							seaHaiYuJiZhiShuaXin.id,
							num3,
							seaJiZhiID.FuBenType,
							seaJiZhiID.id,
							this.worldTimeMag.nowTime,
							num
						}));
					}
				}
			}
		}

		// Token: 0x06006159 RID: 24921 RVA: 0x0026D1E0 File Offset: 0x0026B3E0
		public void updateChuanYingFu()
		{
			if (this.emailDateMag.IsStopAll)
			{
				return;
			}
			string nowTime = this.worldTimeMag.nowTime;
			int level = (int)this.level;
			int num = 0;
			try
			{
				List<string> list = new List<string>();
				int i = 0;
				while (i < this.ToalChuanYingFuList.Count)
				{
					if (!(this.ToalChuanYingFuList[i]["StarTime"].str != "") || this.ToalChuanYingFuList[i]["StarTime"].str == null)
					{
						goto IL_12C;
					}
					string str = this.ToalChuanYingFuList[i]["StarTime"].str;
					if (this.ToalChuanYingFuList[i]["EndTime"].str != "" && this.ToalChuanYingFuList[i]["EndTime"].str != null)
					{
						string str2 = this.ToalChuanYingFuList[i]["EndTime"].str;
						if (Tools.instance.IsInTime(nowTime, str, str2))
						{
							goto IL_12C;
						}
					}
					else if (!(DateTime.Parse(nowTime) < DateTime.Parse(str)))
					{
						goto IL_12C;
					}
					IL_3AB:
					i++;
					continue;
					IL_12C:
					if (this.ToalChuanYingFuList[i]["Level"].Count <= 0 || (level >= this.ToalChuanYingFuList[i]["Level"][0].I && level <= this.ToalChuanYingFuList[i]["Level"][1].I))
					{
						if (this.ToalChuanYingFuList[i]["HaoGanDu"].I > 0)
						{
							int num2 = NPCEx.NPCIDToNew(this.ToalChuanYingFuList[i]["AvatarID"].I);
							if (jsonData.instance.AvatarRandomJsonData[num2.ToString()]["HaoGanDu"].I <= this.ToalChuanYingFuList[i]["HaoGanDu"].I)
							{
								goto IL_3AB;
							}
						}
						if (this.ToalChuanYingFuList[i]["EventValue"].Count > 0)
						{
							string str3 = this.ToalChuanYingFuList[i]["fuhao"].str;
							int i2 = this.ToalChuanYingFuList[i]["EventValue"][0].I;
							int i3 = this.ToalChuanYingFuList[i]["EventValue"][1].I;
							int num3 = GlobalValue.Get(i2, "Avatar.updateChuanYingFu");
							if (str3 == "=")
							{
								if (num3 != i3)
								{
									goto IL_3AB;
								}
							}
							else if (str3 == ">")
							{
								if (num3 <= i3)
								{
									goto IL_3AB;
								}
							}
							else if (num3 >= i3)
							{
								goto IL_3AB;
							}
						}
						if (this.ToalChuanYingFuList[i]["IsOnly"].I == 1)
						{
							list.Add(this.ToalChuanYingFuList[i]["id"].I.ToString());
						}
						else if (this.ToalChuanYingFuList[i]["IsOnly"].I == 2 && this.nomelTaskMag.IsNTaskStart(this.ToalChuanYingFuList[i]["WeiTuo"].I))
						{
							goto IL_3AB;
						}
						this.chuanYingManager.addChuanYingFu(this.ToalChuanYingFuList[i]["id"].I);
						goto IL_3AB;
					}
					goto IL_3AB;
				}
				for (int j = 0; j < list.Count; j++)
				{
					this.HasSendChuanYingFuList.SetField(list[j], this.ToalChuanYingFuList[list[j]]);
					this.ToalChuanYingFuList.RemoveField(list[j]);
				}
				list = new List<string>();
				for (int k = 0; k < this.NoGetChuanYingList.Count; k++)
				{
					string str4 = this.NoGetChuanYingList[k]["sendTime"].str;
					if (DateTime.Parse(str4) <= DateTime.Parse(nowTime))
					{
						int i4 = this.NoGetChuanYingList[k]["id"].I;
						list.Add(this.NoGetChuanYingList[k]["id"].I.ToString());
						this.NewChuanYingList.SetField(this.NoGetChuanYingList[k]["id"].I.ToString(), this.NoGetChuanYingList[k]);
						this.emailDateMag.OldToPlayer(this.NewChuanYingList[i4.ToString()]["AvatarID"].I, i4, str4);
						if (this.NoGetChuanYingList.HasField("IsAdd") && this.NoGetChuanYingList[k]["IsAdd"].I == 1)
						{
							int i5 = this.NoGetChuanYingList[k]["WeiTuo"].I;
							if (!this.nomelTaskMag.IsNTaskStart(i5))
							{
								this.nomelTaskMag.StartNTask(i5, 0);
								UIPopTip.Inst.Pop("获得一条新的委托任务", PopTipIconType.任务进度);
							}
						}
					}
				}
				if (list.Count > 0)
				{
					this.chuanYingManager.NewTipsSum = list.Count;
					for (int l = 0; l < list.Count; l++)
					{
						this.NoGetChuanYingList.SetField(list[l], this.ToalChuanYingFuList[list[l]]);
						this.ToalChuanYingFuList.RemoveField(list[l]);
					}
				}
			}
			catch (Exception ex)
			{
				Debug.Log(ex);
				Debug.LogError(string.Format("{0}", num));
			}
		}

		// Token: 0x0600615A RID: 24922 RVA: 0x0026D834 File Offset: 0x0026BA34
		public void setMonstarDeath()
		{
			int num = 0;
			List<int> list = new List<int>();
			for (int i = 0; i < jsonData.instance.AvatarRandomJsonData.Count; i++)
			{
				if (num == 0)
				{
					num++;
				}
				else
				{
					string text = jsonData.instance.AvatarRandomJsonData.keys[i];
					if (int.Parse(text) < 20000 && jsonData.instance.AvatarJsonData.HasField(text))
					{
						int num2 = (int)jsonData.instance.AvatarJsonData[text]["shouYuan"].n;
						if (num2 > 5000)
						{
							num++;
						}
						else
						{
							try
							{
								if (DateTime.Parse(jsonData.instance.AvatarRandomJsonData[i]["BirthdayTime"].str).AddYears(num2) < this.worldTimeMag.getNowTime())
								{
									int.Parse(text);
									list.Add(int.Parse(text));
								}
							}
							catch (Exception)
							{
								UIPopTip.Inst.Pop("设置NPC死亡出现错误，重置NPC数据以解决问题。", PopTipIconType.叹号);
								break;
							}
							num++;
						}
					}
				}
			}
			for (int j = 0; j < list.Count; j++)
			{
				jsonData.instance.setMonstarDeath(list[j], false);
			}
		}

		// Token: 0x0600615B RID: 24923 RVA: 0x00043A26 File Offset: 0x00041C26
		public GameObject createCanvasDeath()
		{
			return Object.Instantiate<GameObject>(Resources.Load("uiPrefab/CanvasDeath") as GameObject);
		}

		// Token: 0x0600615C RID: 24924 RVA: 0x00043A3C File Offset: 0x00041C3C
		public float AddZiZhiSpeed(float speed)
		{
			return speed * ((float)this.ZiZhi / 100f);
		}

		// Token: 0x0600615D RID: 24925 RVA: 0x0026D988 File Offset: 0x0026BB88
		public float getTimeExpSpeed()
		{
			int staticID = this.getStaticID();
			if (staticID != 0)
			{
				float n = jsonData.instance.StaticSkillJsonData[string.Concat(staticID)]["Skill_Speed"].n;
				float num = (n + this.AddZiZhiSpeed(n)) * this.getJieDanSkillAddExp();
				if (this.TianFuID.HasField(string.Concat(12)))
				{
					float num2 = this.TianFuID["12"].n / 100f;
					num += num * num2;
				}
				return num;
			}
			return 0f;
		}

		// Token: 0x0600615E RID: 24926 RVA: 0x0026DA1C File Offset: 0x0026BC1C
		public int getStaticID()
		{
			foreach (SkillItem skillItem in this.equipStaticSkillList)
			{
				if (skillItem.itemIndex == 0)
				{
					return Tools.instance.getStaticSkillKeyByID(skillItem.itemId);
				}
			}
			return 0;
		}

		// Token: 0x0600615F RID: 24927 RVA: 0x0026DA88 File Offset: 0x0026BC88
		public int getStaticDunSu()
		{
			foreach (SkillItem skillItem in this.equipStaticSkillList)
			{
				if (skillItem.itemIndex == 5)
				{
					return Tools.instance.getStaticSkillKeyByID(skillItem.itemId);
				}
			}
			return 0;
		}

		// Token: 0x06006160 RID: 24928 RVA: 0x0026DAF4 File Offset: 0x0026BCF4
		public void addEXP(int num)
		{
			if (num < 0 && (long)(-(long)num) > (long)this.exp)
			{
				num = -(int)this.exp;
			}
			this.exp += (ulong)((long)num);
			if (jsonData.instance.LevelUpDataJsonData[string.Concat(this.level)] == null)
			{
				return;
			}
			if (this.exp >= (ulong)jsonData.instance.LevelUpDataJsonData[string.Concat(this.level)]["MaxExp"].n && this.level % 3 != 0)
			{
				this.exp -= (ulong)jsonData.instance.LevelUpDataJsonData[string.Concat(this.level)]["MaxExp"].n;
				this.levelUp();
				if (jsonData.instance.LevelUpDataJsonData[string.Concat(this.level)] != null && this.exp >= (ulong)jsonData.instance.LevelUpDataJsonData[string.Concat(this.level)]["MaxExp"].n)
				{
					this.addEXP(0);
					return;
				}
			}
			else if (this.exp >= (ulong)jsonData.instance.LevelUpDataJsonData[string.Concat(this.level)]["MaxExp"].n && this.level % 3 == 0)
			{
				this.exp = (ulong)jsonData.instance.LevelUpDataJsonData[string.Concat(this.level)]["MaxExp"].n;
				if (this.showTupo == 0)
				{
					ResManager.inst.LoadPrefab("PingJingTips").Inst(null).GetComponent<PingJingUIMag>().Show();
					this.showTupo = 1;
					return;
				}
				if (Tools.instance.ShowPingJin)
				{
					UIPopTip.Inst.Pop("你已经到了瓶颈，无法获取经验", PopTipIconType.叹号);
					Tools.instance.ShowPingJin = false;
				}
			}
		}

		// Token: 0x06006161 RID: 24929 RVA: 0x00043A4D File Offset: 0x00041C4D
		public void AddHp(int addNum)
		{
			this.HP += addNum;
			if (this.HP > this.HP_Max)
			{
				this.HP = this.HP_Max;
			}
			if (this.HP <= 0)
			{
				this.die();
			}
		}

		// Token: 0x06006162 RID: 24930 RVA: 0x00043A86 File Offset: 0x00041C86
		public int GetLeveUpAddHPMax(int addHpNum)
		{
			if (!this.TianFuID.HasField(string.Concat(13)))
			{
				return addHpNum;
			}
			return addHpNum + (int)((float)addHpNum * (this.TianFuID["13"].n / 100f));
		}

		// Token: 0x06006163 RID: 24931 RVA: 0x0026DD0C File Offset: 0x0026BF0C
		public int GetTianFuAddCaoYaoCaiJi(int num)
		{
			int num2 = num;
			if (this.TianFuID.HasField(string.Concat(21)))
			{
				num2 = num + (int)((float)num * (this.TianFuID["21"].n / 100f));
			}
			return num2 + (int)((float)num2 * ((float)this.getStaticSkillAddSum(10) / 100f));
		}

		// Token: 0x06006164 RID: 24932 RVA: 0x0026DD70 File Offset: 0x0026BF70
		public void levelUp()
		{
			JSONObject jsonobject = jsonData.instance.LevelUpDataJsonData[string.Concat(this.level)];
			if (jsonobject == null)
			{
				return;
			}
			int hp_Max = this.HP_Max;
			int shengShi = this.shengShi;
			uint shouYuan = this.shouYuan;
			int dunSu = this.dunSu;
			this.level += 1;
			this._HP_Max += this.GetLeveUpAddHPMax((int)jsonobject["AddHp"].n);
			this.HP = this.HP_Max;
			this._shengShi += (int)jsonobject["AddShenShi"].n;
			this._dunSu += (int)jsonobject["AddDunSu"].n;
			this.shouYuan += (uint)jsonobject["AddShouYuan"].n;
			bool isBigTuPo = false;
			if (this.level > 1 && this.level % 3 == 1)
			{
				Avatar.UnlockShenXianDouFa((int)(this.level / 3 - 1));
				this.Dandu = 0;
				if (this.TianFuID.HasField("22"))
				{
					for (int i = 1; i <= jsonData.instance.CrateAvatarSeidJsonData[22][this.TianFuID[22.ToString()].I].keys.Count - 1; i++)
					{
						if ((int)this.level == jsonData.instance.CrateAvatarSeidJsonData[22][this.TianFuID[22.ToString()].I]["value" + i][0].I)
						{
							this.WuDaoDian += jsonData.instance.CrateAvatarSeidJsonData[22][this.TianFuID[22.ToString()].I]["value" + i][1].I;
							break;
						}
					}
				}
				isBigTuPo = true;
				this.AddLingGan(100);
			}
			else
			{
				this.Dandu -= 20;
				if (this.Dandu < 0)
				{
					this.Dandu = 0;
				}
				this.AddLingGan(50);
			}
			string desc = "\u3000\u3000周边天地的灵气突然开始涌入你的体内，你感到体内的真元犹如沸腾的开水一般，迅速流动起来。灵气的波动足足持续了一个时辰才平息下来，你终于冲破瓶颈，境界提升至" + LevelUpDataJsonData.DataDict[(int)this.level].Name;
			ResManager.inst.LoadPrefab("LevelUpPanel").Inst(null).GetComponent<TuPoUIMag>().ShowTuPo((int)this.level, hp_Max, this.HP, shengShi, this.shengShi, (int)shouYuan, (int)this.shouYuan, dunSu, this.dunSu, isBigTuPo, desc);
		}

		// Token: 0x06006165 RID: 24933 RVA: 0x0026E03C File Offset: 0x0026C23C
		public void AllMapSetNode()
		{
			foreach (JSONObject node in jsonData.instance.AllMapLuDainType.list)
			{
				this.resetNode(node);
			}
		}

		// Token: 0x06006166 RID: 24934 RVA: 0x0026E098 File Offset: 0x0026C298
		public void ResetAllEndlessNode()
		{
			int num = 1;
			foreach (JToken jtoken in this.EndlessSea["SafeLv"])
			{
				if (!this.EndlessSeaRandomNode.ContainsKey(string.Concat(num)))
				{
					this.EndlessSeaRandomNode[string.Concat(num)] = new JObject();
				}
				this.ResetEndlessNode((JObject)this.EndlessSeaRandomNode[string.Concat(num)], num);
				num++;
			}
		}

		// Token: 0x06006167 RID: 24935 RVA: 0x0026E144 File Offset: 0x0026C344
		public int GetDaHaiIDBySeaID(int SeaID)
		{
			foreach (KeyValuePair<string, JToken> keyValuePair in jsonData.instance.EndlessSeaHaiYuData)
			{
				if (Tools.ContensInt((JArray)keyValuePair.Value["shuxing"], SeaID))
				{
					return (int)keyValuePair.Value["id"];
				}
			}
			return -1;
		}

		// Token: 0x06006168 RID: 24936 RVA: 0x0026E1C8 File Offset: 0x0026C3C8
		public void ResetEndlessNode(JObject seaNode, int SeaID)
		{
			if (seaNode.ContainsKey("resetTime"))
			{
				DateTime startTime = DateTime.Parse((string)seaNode["resetTime"]);
				DateTime endTime = startTime.AddMonths((int)seaNode["CD"]);
				if (Tools.instance.IsInTime(this.worldTimeMag.getNowTime(), startTime, endTime, 0))
				{
					return;
				}
			}
			FuBenMap fuBenMap = new FuBenMap(7, 7);
			JToken endlessSeaNPCGouChengData = jsonData.instance.EndlessSeaNPCGouChengData;
			int Sealeve = this.seaNodeMag.GetSeaIDLV(SeaID);
			List<JToken> list = Tools.FindAllJTokens(endlessSeaNPCGouChengData, (JToken aa) => (int)aa["qujian"][0] <= Sealeve && Sealeve <= (int)aa["qujian"][1]);
			JArray jarray = new JArray();
			foreach (JToken cc in list)
			{
				List<int> list2 = this.SetSeaNodeList(cc, SeaID);
				for (int i = 0; i < list2.Count; i++)
				{
					int num = jsonData.GetRandom() % 7;
					int num2 = jsonData.GetRandom() % 7;
					JObject jobject = this.CreateSeaMonstar(SeaID, list2[i], fuBenMap.mapIndex[num, num2]);
					jarray.Add(jobject);
				}
			}
			seaNode["Monstar"] = jarray;
			seaNode["resetTime"] = this.worldTimeMag.nowTime;
			JToken jtoken = jsonData.instance.EndlessSeaSafeLvData[Sealeve.ToString()]["resetTime"];
			seaNode["CD"] = Tools.getRandomInt((int)jtoken[0], (int)jtoken[1]);
		}

		// Token: 0x06006169 RID: 24937 RVA: 0x0026E38C File Offset: 0x0026C58C
		public List<int> SetSeaNodeList(JToken _cc, int SeaID)
		{
			int randomInt = Tools.getRandomInt((int)_cc["max"][0], (int)_cc["max"][1]);
			int Type = (int)_cc["Type"];
			JToken endlessSeaNPCData = jsonData.instance.EndlessSeaNPCData;
			int dahaiyu = this.GetDaHaiIDBySeaID(SeaID);
			List<JToken> list = Tools.FindAllJTokens(endlessSeaNPCData, delegate(JToken aa)
			{
				if ((int)aa["EventType"] != Type)
				{
					return false;
				}
				if (!Tools.ContensInt((JArray)aa["shuxing"], dahaiyu))
				{
					return false;
				}
				if (((JArray)aa["EventValue"]).Count > 0 && !Avatar.ManZuValue((int)aa["EventValue"][0], (int)aa["EventValue"][1], (string)aa["fuhao"]))
				{
					return false;
				}
				int seaIDLV = this.seaNodeMag.GetSeaIDLV(SeaID);
				if ((int)aa["EventLv"][0] > seaIDLV || seaIDLV > (int)aa["EventLv"][1])
				{
					return false;
				}
				if ((int)aa["NowSeaOnce"] == 1)
				{
					foreach (JToken jtoken in jsonData.instance.EndlessSeaHaiYuData[dahaiyu.ToString()]["shuxing"])
					{
						if (this.EndlessSeaRandomNode.ContainsKey(string.Concat((int)jtoken)) && ((JObject)this.EndlessSeaRandomNode[string.Concat((int)jtoken)]).ContainsKey("Monstar"))
						{
							using (IEnumerator<JToken> enumerator2 = this.EndlessSeaRandomNode[string.Concat((int)jtoken)]["Monstar"].GetEnumerator())
							{
								while (enumerator2.MoveNext())
								{
									if ((int)enumerator2.Current["monstarId"] == (int)aa["id"])
									{
										return false;
									}
								}
							}
						}
					}
					return true;
				}
				return true;
			});
			List<int> list2 = new List<int>();
			int num = 0;
			if (list.Count == 0)
			{
				return list2;
			}
			int num2 = 0;
			while (num2 < randomInt && num < 100)
			{
				JToken randomListByPercent = Tools.instance.getRandomListByPercent(list, "percent");
				if ((int)randomListByPercent["NowSeaOnce"] == 1 && list2.Contains((int)randomListByPercent["id"]))
				{
					num2--;
					num++;
				}
				else
				{
					list2.Add((int)randomListByPercent["id"]);
				}
				num2++;
			}
			return list2;
		}

		// Token: 0x0600616A RID: 24938 RVA: 0x0026E4BC File Offset: 0x0026C6BC
		public JObject CreateSeaMonstar(int seaId, int monstarID, int index)
		{
			JObject jobject = new JObject();
			jobject["uuid"] = Tools.getUUID();
			jobject["monstarId"] = monstarID;
			jobject["index"] = index;
			jobject["StartTime"] = this.worldTimeMag.nowTime;
			return jobject;
		}

		// Token: 0x0600616B RID: 24939 RVA: 0x0026E520 File Offset: 0x0026C720
		public void resetNode(JSONObject node)
		{
			if (!this.AllMapRandomNode.HasField(string.Concat((int)node["id"].n)))
			{
				JSONObject jsonobject = new JSONObject(JSONObject.Type.OBJECT);
				jsonobject.AddField("resetTime", "0001-01-01");
				jsonobject.AddField("Type", -1);
				jsonobject.AddField("EventId", 0);
				jsonobject.AddField("Reset", true);
				this.AllMapRandomNode.AddField(string.Concat((int)node["id"].n), jsonobject);
			}
			Avatar avatar = Tools.instance.getPlayer();
			JSONObject jsonobject2 = this.AllMapRandomNode[string.Concat((int)node["id"].n)];
			DateTime dateTime = DateTime.Parse(jsonobject2["resetTime"].str);
			DateTime now = this.worldTimeMag.getNowTime();
			if (jsonobject2["Reset"].b || !jsonData.instance.AllMapReset.HasField(string.Concat((int)jsonobject2["Type"].n)) || now > dateTime.AddMonths((int)jsonData.instance.AllMapReset[string.Concat((int)jsonobject2["Type"].n)]["resetTiem"].n))
			{
				jsonobject2.SetField("resetTime", this.worldTimeMag.nowTime);
				jsonobject2.SetField("Reset", false);
				if ((int)node["MapType"].n == 1 || (int)node["MapType"].n == 0)
				{
					List<JSONObject> list = jsonData.instance.AllMapReset.list.FindAll((JSONObject aa) => (int)aa["Type"].n == (int)node["MapType"].n && (int)this.level >= (int)aa["qujian"][0].n && (int)this.level <= (int)aa["qujian"][1].n);
					List<JSONObject> _tempJsond = new List<JSONObject>();
					list.ForEach(delegate(JSONObject aa)
					{
						_tempJsond.Add(new JSONObject(aa.ToString(), -2, false, false));
					});
					Dictionary<int, int> dictionary = new Dictionary<int, int>();
					using (List<JSONObject>.Enumerator enumerator = this.AllMapRandomNode.list.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							JSONObject _tempMapNode = enumerator.Current;
							if (!dictionary.ContainsKey((int)_tempMapNode["Type"].n))
							{
								dictionary[(int)_tempMapNode["Type"].n] = 1;
							}
							else
							{
								Dictionary<int, int> dictionary2 = dictionary;
								int key = (int)_tempMapNode["Type"].n;
								dictionary2[key]++;
							}
							if ((int)_tempMapNode["Type"].n >= 0 && (int)jsonData.instance.AllMapReset[(int)_tempMapNode["Type"].n]["max"].n > 0 && dictionary[(int)_tempMapNode["Type"].n] >= (int)jsonData.instance.AllMapReset[(int)_tempMapNode["Type"].n]["max"].n)
							{
								JSONObject jsonobject3 = _tempJsond.Find((JSONObject _acs) => (int)_acs["id"].n == (int)_tempMapNode["Type"].n);
								if (jsonobject3 != null)
								{
									jsonobject3.SetField("percent", 0);
								}
							}
							Transform transform = AllMapManage.instance.AllNodeGameobjGroup.transform.Find(string.Concat((int)node["id"].n));
							if (transform == null)
							{
								Debug.LogError(("路点出错" + (int)node["id"].n) ?? "");
							}
							using (List<int>.Enumerator enumerator2 = transform.GetComponent<MapComponent>().nextIndex.GetEnumerator())
							{
								while (enumerator2.MoveNext())
								{
									int _nnnn = enumerator2.Current;
									if (this.AllMapRandomNode.HasField(string.Concat(_nnnn)) && (int)jsonData.instance.AllMapReset[string.Concat((int)this.AllMapRandomNode[string.Concat(_nnnn)]["Type"].n)]["CanSame"].n == 0)
									{
										JSONObject jsonobject4 = _tempJsond.Find((JSONObject _acs) => (int)_acs["id"].n == (int)this.AllMapRandomNode[string.Concat(_nnnn)]["Type"].n);
										if (jsonobject4 != null)
										{
											jsonobject4.SetField("percent", 0);
										}
									}
								}
							}
						}
					}
					JSONObject json = Tools.instance.getRandomListByPercent(_tempJsond, "percent");
					jsonobject2.SetField("Type", (int)json["id"].n);
					List<JSONObject> list2 = jsonData.instance.MapRandomJsonData.list.FindAll(delegate(JSONObject aa)
					{
						if (aa["EventValue"].list.Count > 0 && !Avatar.ManZuValue((int)aa["EventValue"][0].n, (int)aa["EventValue"][1].n, aa["fuhao"].str))
						{
							return false;
						}
						if (aa["StartTime"].str != "")
						{
							DateTime t = DateTime.Parse(aa["StartTime"].str);
							DateTime t2 = DateTime.Parse(aa["EndTime"].str);
							if (!(now >= t) || !(now <= t2))
							{
								return false;
							}
						}
						if (avatar.SuiJiShiJian.HasField(Tools.getScreenName()) && avatar.SuiJiShiJian[Tools.getScreenName()].list.Find((JSONObject _aa) => (int)_aa.n == (int)aa["id"].n) != null)
						{
							return false;
						}
						foreach (JSONObject jsonobject5 in this.AllMapRandomNode.list)
						{
							int num2 = (int)jsonobject5["EventId"].n;
							if (jsonData.instance.MapRandomJsonData.HasField(num2.ToString()))
							{
								JSONObject jsonobject6 = jsonData.instance.MapRandomJsonData[num2.ToString()];
								if ((int)aa["id"].n == (int)jsonobject6["id"].n && (int)jsonobject6["once"].n == 1)
								{
									return false;
								}
							}
						}
						if ((int)aa["EventType"].n == (int)json["id"].n)
						{
							return aa["EventLv"].list.Find((JSONObject _aa) => (int)_aa.n == (int)Tools.instance.getPlayer().level) != null;
						}
						return false;
					});
					if (list2.Count > 0)
					{
						JSONObject randomListByPercent = Tools.instance.getRandomListByPercent(list2, "percent");
						jsonobject2.SetField("Type", (int)randomListByPercent["EventType"].n);
						jsonobject2.SetField("EventId", (int)randomListByPercent["id"].n);
						int num = (int)randomListByPercent["EventType"].n;
						return;
					}
					if ((int)node["MapType"].n == 0)
					{
						jsonobject2.SetField("Type", 2);
						return;
					}
					jsonobject2.SetField("Type", 5);
				}
			}
		}

		// Token: 0x0600616C RID: 24940 RVA: 0x0026EBE0 File Offset: 0x0026CDE0
		public static bool ManZuValue(int staticValueID, int num, string type)
		{
			int num2 = GlobalValue.Get(staticValueID, string.Format("Avatar.ManZuValue({0}, {1}, {2})", staticValueID, num, type));
			if (type == "=")
			{
				return num2 == num;
			}
			if (type == "<")
			{
				return num2 < num;
			}
			return !(type == ">") || num2 > num;
		}

		// Token: 0x0600616D RID: 24941 RVA: 0x0026EC44 File Offset: 0x0026CE44
		public void WorldsetRandomFace()
		{
			PlayerSetRandomFace component = ((GameObject)this.renderObj).transform.GetChild(0).GetChild(0).GetComponent<PlayerSetRandomFace>();
			if (this.fightTemp.MonstarID > 0 && component != null)
			{
				component.randomAvatar(this.fightTemp.MonstarID);
			}
		}

		// Token: 0x0600616E RID: 24942 RVA: 0x00043AC4 File Offset: 0x00041CC4
		public void discardCard(Card card)
		{
			Event.fireOut("discardCard", new object[]
			{
				this,
				card
			});
		}

		// Token: 0x0600616F RID: 24943 RVA: 0x0026EC9C File Offset: 0x0026CE9C
		public void MonstarAddStaticSkill()
		{
			foreach (SkillItem skillItem in this.equipStaticSkillList)
			{
				StaticSkill staticSkill = new StaticSkill(skillItem.itemId, 0, 5);
				this.StaticSkill.Add(staticSkill);
				staticSkill.Puting(this, this, 1);
				this.addYuanYingStaticSkill(skillItem, skillItem.itemId);
			}
		}

		// Token: 0x06006170 RID: 24944 RVA: 0x0026ED18 File Offset: 0x0026CF18
		public void addStaticSkill()
		{
			foreach (SkillItem skillItem in this.equipStaticSkillList)
			{
				int staticSkillKeyByID = Tools.instance.getStaticSkillKeyByID(skillItem.itemId);
				StaticSkill staticSkill = new StaticSkill(staticSkillKeyByID, 0, 5);
				this.StaticSkill.Add(staticSkill);
				staticSkill.Puting(this, this, 1);
				this.addYuanYingStaticSkill(skillItem, staticSkillKeyByID);
			}
		}

		// Token: 0x06006171 RID: 24945 RVA: 0x0026ED9C File Offset: 0x0026CF9C
		public void addYuanYingStaticSkill(SkillItem _skill, int skillid)
		{
			if (_skill.itemIndex == 6)
			{
				int i = jsonData.instance.StaticSkillJsonData[string.Concat(skillid)]["Skill_LV"].I;
				int i2 = jsonData.instance.StaticSkillJsonData[string.Concat(skillid)]["AttackType"].I;
				JSONObject yuanYingBiao = jsonData.instance.YuanYingBiao;
				if (yuanYingBiao.keys.Count > 0)
				{
					foreach (string index in yuanYingBiao.keys)
					{
						if (i2 == yuanYingBiao[index]["value1"].I && i == yuanYingBiao[index]["value2"].I)
						{
							for (int j = 0; j < yuanYingBiao[index]["value3"].Count; j++)
							{
								if (yuanYingBiao[index]["target"].I == 1)
								{
									this.spell.addDBuff(yuanYingBiao[index]["value3"][j].I, yuanYingBiao[index]["value4"][j].I);
								}
								else
								{
									this.OtherAvatar.spell.addDBuff(yuanYingBiao[index]["value3"][j].I, yuanYingBiao[index]["value4"][j].I);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06006172 RID: 24946 RVA: 0x0026EF8C File Offset: 0x0026D18C
		public string getYuanYingStaticDesc(SkillItem _skill, int skillid)
		{
			if (_skill.itemIndex == 6)
			{
				int i = jsonData.instance.StaticSkillJsonData[string.Concat(skillid)]["Skill_LV"].I;
				int i2 = jsonData.instance.StaticSkillJsonData[string.Concat(skillid)]["AttackType"].I;
				JSONObject yuanYingBiao = jsonData.instance.YuanYingBiao;
				if (yuanYingBiao.keys.Count > 0)
				{
					foreach (string index in yuanYingBiao.keys)
					{
						if (i2 == yuanYingBiao[index]["value1"].I && i == yuanYingBiao[index]["value2"].I)
						{
							return yuanYingBiao[index]["desc"].Str;
						}
					}
				}
			}
			return "";
		}

		// Token: 0x06006173 RID: 24947 RVA: 0x0026F0AC File Offset: 0x0026D2AC
		public void addWuDaoSeid()
		{
			foreach (SkillItem skillItem in this.wuDaoMag.GetAllWuDaoSkills())
			{
				new WuDaoStaticSkill(skillItem.itemId, 0, 5).Puting(this, this, 1);
			}
		}

		// Token: 0x06006174 RID: 24948 RVA: 0x0026F110 File Offset: 0x0026D310
		public void addJieDanSeid()
		{
			foreach (SkillItem skillItem in this.hasJieDanSkillList)
			{
				new JieDanSkill(skillItem.itemId, 0, 5).Puting(this, this, 1);
			}
		}

		// Token: 0x06006175 RID: 24949 RVA: 0x0026F170 File Offset: 0x0026D370
		public void addEquipSeid()
		{
			int num = 0;
			int num2 = 1;
			if (base.isPlayer())
			{
				foreach (UIFightWeaponItem uifightWeaponItem in UIFightPanel.Inst.FightWeapon)
				{
					uifightWeaponItem.Clear();
				}
			}
			bool flag = false;
			foreach (ITEM_INFO item_INFO in this.equipItemList.values)
			{
				foreach (JSONObject jsonobject in jsonData.instance.ItemJsonData[string.Concat(item_INFO.itemId)]["seid"].list)
				{
					if (jsonobject.I == 1)
					{
						int buffid = (int)jsonData.instance.EquipSeidJsonData[1][string.Concat(item_INFO.itemId)]["value1"].n;
						if (base.isPlayer() && item_INFO.Seid.HasField("ItemSeids") && item_INFO.Seid["ItemSeids"].list.Count > 0)
						{
							int key = (int)jsonData.instance.EquipSeidJsonData[1][string.Concat(item_INFO.itemId)]["value1"].n;
							if (!this.fightTemp.LianQiBuffEquipDictionary.ContainsKey(key))
							{
								this.fightTemp.LianQiBuffEquipDictionary.Add(key, item_INFO.Seid["ItemSeids"]);
								this.fightTemp.LianQiEquipDictionary.Add(key, item_INFO.Seid);
							}
							List<JSONObject> list = item_INFO.Seid["ItemSeids"].list;
							bool flag2 = true;
							for (int i = 0; i < list.Count; i++)
							{
								if (list[i]["id"].I == 62 && (float)this.HP / (float)this.HP_Max * 100f > list[i]["value1"][0].n)
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
											this.spell.addBuff(list[j]["value1"][k].I, list[j]["value2"][k].I);
										}
									}
								}
							}
						}
						this.spell.addDBuff(buffid);
					}
					if (jsonobject.I == 2)
					{
						int i2 = jsonData.instance.EquipSeidJsonData[2][string.Concat(item_INFO.itemId)]["value1"].I;
						if (item_INFO.Seid.HasField("ItemSeids"))
						{
							foreach (JSONObject jsonobject2 in item_INFO.Seid["ItemSeids"].list)
							{
								if (jsonobject2["id"].I == jsonobject.I)
								{
									i2 = jsonobject2["value1"].I;
								}
							}
						}
						Skill skill = new Skill(i2, 0, 10);
						if (item_INFO.Seid.HasField("AttackType"))
						{
							if (num == i2)
							{
								num += 5;
								skill.skill_ID = num;
								jsonData.instance.skillJsonData[num.ToString()].SetField("AttackType", item_INFO.Seid["AttackType"]);
								_skillJsonData.DataDict[num].AttackType = item_INFO.Seid["AttackType"].ToList();
							}
							else
							{
								num = i2;
								jsonData.instance.skillJsonData[num.ToString()].SetField("AttackType", item_INFO.Seid["AttackType"]);
								_skillJsonData.DataDict[num].AttackType = item_INFO.Seid["AttackType"].ToList();
							}
						}
						if (item_INFO.Seid.HasField("SkillSeids"))
						{
							skill.ItemAddSeid = item_INFO.Seid["SkillSeids"];
						}
						if (item_INFO.Seid.HasField("Damage"))
						{
							skill.Damage = item_INFO.Seid["Damage"].I;
						}
						if (item_INFO.Seid.HasField("Name"))
						{
							skill.skill_Name = item_INFO.Seid["Name"].str;
						}
						if (item_INFO.Seid.HasField("SeidDesc"))
						{
							skill.skill_Desc = item_INFO.Seid["SeidDesc"].str;
						}
						if (item_INFO.Seid.HasField("ItemIcon"))
						{
							skill.skill_Icon.ToString();
							skill.skill_Icon = ResManager.inst.LoadTexture2D(item_INFO.Seid["ItemIcon"].str);
						}
						this.skill.Add(skill);
						if (base.isPlayer() && UIFightPanel.Inst.FightWeapon != null && UIFightPanel.Inst.FightWeapon.Count > 1)
						{
							UIFightWeaponItem uifightWeaponItem2 = UIFightPanel.Inst.FightWeapon[0];
							if (num2 == 2)
							{
								uifightWeaponItem2 = UIFightPanel.Inst.FightWeapon[1];
							}
							uifightWeaponItem2.gameObject.SetActive(true);
							uifightWeaponItem2.SetWeapon(skill, item_INFO);
							num2++;
						}
						if (!flag)
						{
							flag = true;
							FightFaBaoShow componentInChildren = (this.renderObj as GameObject).GetComponentInChildren<FightFaBaoShow>();
							if (componentInChildren != null)
							{
								componentInChildren.SetWeapon(this, item_INFO);
							}
							else
							{
								Debug.LogError("没有查找到法宝显示组件，需要程序检查");
							}
						}
					}
				}
			}
		}

		// Token: 0x06006176 RID: 24950 RVA: 0x00043ADE File Offset: 0x00041CDE
		public void onCrystalChanged(CardMag oldValue)
		{
			Event.fireOut("crtstalChanged", new object[]
			{
				this,
				oldValue
			});
			MessageMag.Instance.Send("Fight_CardChange", null);
		}

		// Token: 0x06006177 RID: 24951 RVA: 0x0026F864 File Offset: 0x0026DA64
		public void FightClearSkill(int startIndex, int endIndex)
		{
			if (base.isPlayer())
			{
				int num = 0;
				foreach (UIFightSkillItem uifightSkillItem in UIFightPanel.Inst.FightSkills)
				{
					if (num >= startIndex && num < endIndex)
					{
						uifightSkillItem.Clear();
					}
					num++;
				}
			}
		}

		// Token: 0x06006178 RID: 24952 RVA: 0x0026F8D0 File Offset: 0x0026DAD0
		public void FightAddSkill(int skillID, int startIndex, int endIndex)
		{
			Skill item = new Skill(skillID, 0, 10);
			this.skill.Add(item);
			if (base.isPlayer())
			{
				int num = 0;
				foreach (UIFightSkillItem uifightSkillItem in UIFightPanel.Inst.FightSkills)
				{
					if (num >= startIndex && num < endIndex && !uifightSkillItem.HasSkill)
					{
						uifightSkillItem.SetSkill(item);
						break;
					}
					num++;
				}
			}
		}

		// Token: 0x06006179 RID: 24953 RVA: 0x0026F960 File Offset: 0x0026DB60
		public void addSkill()
		{
			foreach (SkillItem skillItem in this.equipSkillList)
			{
				int skillKeyByID = Tools.instance.getSkillKeyByID(skillItem.itemId, this);
				if (skillKeyByID == -1)
				{
					Debug.LogError("找不到技能ID：" + skillItem.itemId);
				}
				Skill item = new Skill(skillKeyByID, 0, 10);
				this.skill.Add(item);
				if (base.isPlayer())
				{
					UIFightPanel.Inst.FightSkills[skillItem.itemIndex].SetSkill(item);
				}
			}
		}

		// Token: 0x0600617A RID: 24954 RVA: 0x0026FA14 File Offset: 0x0026DC14
		public override void __init__()
		{
			this.ai = new AI(this);
			this.combat = new Combat(this);
			this.spell = new Spell(this);
			this.jieyin = new JieYin(this);
			this.dialogMsg = new Dialog(this);
			this.buffmag = new BuffMag(this);
			this.wuDaoMag = new WuDaoMag(this);
			this.chuanYingManager = new ChuanYingManager(this);
			this.jianLingManager = new JianLingManager(this);
			this.taskMag = new TaskMag(this);
			this.cardMag = new CardMag(this);
			this.zulinContorl = new ZulinContorl(this);
			this.fubenContorl = new FubenContrl(this);
			this.nomelTaskMag = new NomelTaskMag(this);
			this.chenghaomag = new chenghaoMag(this);
			this.fightTemp = new FightTempValue();
			this.randomFuBenMag = new RandomFuBenMag(this);
			this.seaNodeMag = new SeaNodeMag(this);
			if (base.isPlayer())
			{
				Event.registerIn("relive", this, "relive");
				Event.registerIn("updatePlayer", this, "updatePlayer");
				Event.registerIn("sendChatMessage", this, "sendChatMessage");
			}
		}

		// Token: 0x0600617B RID: 24955 RVA: 0x0026FB34 File Offset: 0x0026DD34
		public void setHP(int hp)
		{
			if (RoundManager.instance != null && RoundManager.instance.IsVirtual)
			{
				return;
			}
			List<int> list = new List<int>();
			list.Add(hp);
			this.spell.onBuffTickByType(10, list);
			if ((this.state == 1 || this.OtherAvatar.state == 1) && hp < Tools.instance.getPlayer().HP)
			{
				return;
			}
			if (hp > this.HP_Max)
			{
				hp = this.HP_Max;
			}
			if (RoundManager.instance != null)
			{
				if (hp > this.HP)
				{
					this.fightTemp.SetHealHP(hp - this.HP);
				}
				if (hp < this.HP)
				{
					this.fightTemp.SetRoundLossHP(this.HP - hp);
				}
				if (RoundManager.instance.PlayerFightEventProcessor != null)
				{
					RoundManager.instance.PlayerFightEventProcessor.OnUpdateHP();
				}
			}
			this.HP = hp;
			int showHP = this.HP;
			Queue<UnityAction> queue = new Queue<UnityAction>();
			UnityAction item = delegate()
			{
				this.fightTemp.showNowHp = showHP;
				YSFuncList.Ints.Continue();
			};
			queue.Enqueue(item);
			YSFuncList.Ints.AddFunc(queue);
			YSFuncList.Ints.Start();
			if (hp <= 0 && this.OtherAvatar.state != 1)
			{
				this.die();
				if (base.isPlayer())
				{
					return;
				}
			}
			else if (hp <= 0 && this.OtherAvatar.state == 1)
			{
				return;
			}
			Event.fireOut("set_HP", new object[]
			{
				this,
				this.HP
			});
		}

		// Token: 0x0600617C RID: 24956 RVA: 0x0026FCB8 File Offset: 0x0026DEB8
		public void SetChengHaoId(int id)
		{
			this.chengHao = id;
			if (id >= 7 && id <= 10 && !this.StreamData.MenPaiTaskMag.IsInit)
			{
				this.StreamData.MenPaiTaskMag.NextTime = NpcJieSuanManager.inst.GetNowTime();
				this.StreamData.MenPaiTaskMag.IsInit = true;
			}
		}

		// Token: 0x0600617D RID: 24957 RVA: 0x0026FD14 File Offset: 0x0026DF14
		public void die()
		{
			MonstarMag monstarmag = Tools.instance.monstarMag;
			if (monstarmag.shouldReloadSaveHp())
			{
				this.HP = Tools.instance.monstarMag.gameStartHP;
			}
			this.state = 1;
			if (!base.isPlayer())
			{
				Avatar player = PlayerEx.Player;
				if (monstarmag.shouldReloadSaveHp())
				{
					player.HP = Tools.instance.monstarMag.gameStartHP;
				}
				MusicMag.instance.PlayEffectMusic(6, 1f);
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
					this.shaQi += 1U;
				}
				if (monstarmag.MonstarCanDeath())
				{
					jsonData.instance.setMonstarDeath(Tools.instance.MonstarID, true);
				}
				try
				{
					monstarmag.AddKillAvatarWuDao(Tools.instance.getPlayer(), Tools.instance.MonstarID);
				}
				catch (Exception)
				{
				}
				Tools.instance.AutoSetSeaMonstartDie();
			}
			else if (base.isPlayer())
			{
				Tools.instance.CaiYaoData = null;
				GlobalValue.SetTalk(1, 3, "Avatar.die");
				if (monstarmag.ReloadHpType() == 2)
				{
					this.HP = 1;
				}
				if (monstarmag.PlayerNotDeath())
				{
					Queue<UnityAction> queue = new Queue<UnityAction>();
					UnityAction item = delegate()
					{
						Tools.instance.loadMapScenes(Tools.instance.FinalScene, true);
						YSFuncList.Ints.Continue();
					};
					queue.Enqueue(item);
					YSFuncList.Ints.AddFunc(queue);
					return;
				}
				if (monstarmag.isInFubenNotDeath())
				{
					Tools.instance.getPlayer().fubenContorl.outFuBen(true);
					return;
				}
				Queue<UnityAction> queue2 = new Queue<UnityAction>();
				UnityAction item2 = delegate()
				{
					int num = monstarmag.FightLose();
					UIDeath.Inst.Show((DeathType)num);
					YSFuncList.Ints.Continue();
				};
				queue2.Enqueue(item2);
				YSFuncList.Ints.AddFunc(queue2);
				return;
			}
			Event.fireOut("set_state", new object[]
			{
				this,
				1
			});
		}

		// Token: 0x0600617E RID: 24958 RVA: 0x00043B08 File Offset: 0x00041D08
		public override void onDestroy()
		{
			if (base.isPlayer())
			{
				Event.deregisterIn(this);
			}
		}

		// Token: 0x0600617F RID: 24959 RVA: 0x00043B19 File Offset: 0x00041D19
		public void gameFinsh()
		{
			base.cellCall("gameFinsh", Array.Empty<object>());
		}

		// Token: 0x06006180 RID: 24960 RVA: 0x00043B2B File Offset: 0x00041D2B
		public virtual void updatePlayer(float x, float y, float z, float yaw)
		{
			this.position.x = x;
			this.position.y = y;
			this.position.z = z;
			this.direction.z = yaw;
		}

		// Token: 0x06006181 RID: 24961 RVA: 0x0026FF6C File Offset: 0x0026E16C
		public override void onEnterWorld()
		{
			base.onEnterWorld();
			if (base.isPlayer())
			{
				Event.fireOut("onAvatarEnterWorld", new object[]
				{
					KBEngineApp.app.entity_uuid,
					this.id,
					this
				});
				SkillBox.inst.pull();
			}
		}

		// Token: 0x06006182 RID: 24962 RVA: 0x0026FFC8 File Offset: 0x0026E1C8
		public void sendChatMessage(string msg)
		{
			object name = this.name;
			base.baseCall("sendChatMessage", new object[]
			{
				(string)name + ": " + msg
			});
		}

		// Token: 0x06006183 RID: 24963 RVA: 0x00043B5E File Offset: 0x00041D5E
		public override void ReceiveChatMessage(string msg)
		{
			Event.fireOut("ReceiveChatMessage", new object[]
			{
				msg
			});
		}

		// Token: 0x06006184 RID: 24964 RVA: 0x00043B74 File Offset: 0x00041D74
		public void relive(byte type)
		{
			base.cellCall("relive", new object[]
			{
				type
			});
		}

		// Token: 0x06006185 RID: 24965 RVA: 0x00270004 File Offset: 0x0026E204
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

		// Token: 0x06006186 RID: 24966 RVA: 0x00270040 File Offset: 0x0026E240
		public bool HasDunShuSkill()
		{
			foreach (SkillItem skillItem in this.equipStaticSkillList)
			{
				if (jsonData.instance.StaticSkillJsonData[string.Concat(Tools.instance.getStaticSkillKeyByID(skillItem.itemId))]["seid"].list.Find((JSONObject aa) => (int)aa.n == 9) != null)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06006187 RID: 24967 RVA: 0x002700F4 File Offset: 0x0026E2F4
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

		// Token: 0x06006188 RID: 24968 RVA: 0x00043B90 File Offset: 0x00041D90
		public override void recvSkill(int attacker, int skillID)
		{
			Event.fireOut("recvSkill", new object[]
			{
				attacker,
				skillID
			});
		}

		// Token: 0x06006189 RID: 24969 RVA: 0x00270128 File Offset: 0x0026E328
		public override void onAddSkill(int skillID)
		{
			Dbg.DEBUG_MSG(string.Concat(new object[]
			{
				this.className,
				"::onAddSkill(",
				skillID,
				")"
			}));
			Skill skill = new Skill();
			skill.id = skillID;
			skill.name = skillID + " ";
			skill.displayType = (Skill_DisplayType)jsonData.instance.skillJsonData[string.Concat(skillID)]["Skill_DisplayType"].n;
			skill.canUseDistMax = jsonData.instance.skillJsonData[string.Concat(skillID)]["canUseDistMax"].n;
			skill.skillEffect = jsonData.instance.skillJsonData[string.Concat(skillID)]["skillEffect"].str;
			string name = Regex.Unescape(jsonData.instance.skillJsonData[string.Concat(skillID)]["name"].str);
			skill.name = name;
			skill.coolTime = jsonData.instance.skillJsonData[string.Concat(skillID)]["CD"].n;
			skill.restCoolTimer = skill.coolTime;
			SkillBox.inst.add(skill);
			Event.fireOut("setSkillButton", Array.Empty<object>());
		}

		// Token: 0x0600618A RID: 24970 RVA: 0x00043BB4 File Offset: 0x00041DB4
		public override void clearSkills()
		{
			SkillBox.inst.clear();
			base.cellCall("requestPull", Array.Empty<object>());
		}

		// Token: 0x0600618B RID: 24971 RVA: 0x00043BD0 File Offset: 0x00041DD0
		public void createBuild(ulong BuildId, Vector3 positon, Vector3 direction)
		{
			base.baseCall("createBuild", new object[]
			{
				BuildId,
				positon,
				direction
			});
		}

		// Token: 0x0600618C RID: 24972 RVA: 0x002702A4 File Offset: 0x0026E4A4
		public override void onRemoveSkill(int skillID)
		{
			Dbg.DEBUG_MSG(string.Concat(new object[]
			{
				this.className,
				"::onRemoveSkill(",
				skillID,
				")"
			}));
			Event.fireOut("onRemoveSkill", new object[]
			{
				this
			});
			SkillBox.inst.remove(skillID);
		}

		// Token: 0x0600618D RID: 24973 RVA: 0x0011EE44 File Offset: 0x0011D044
		public override void recvDamage(int attackerID, int skillID, int damageType, int damage)
		{
			Entity entity = KBEngineApp.app.findEntity(attackerID);
			Event.fireOut("recvDamage", new object[]
			{
				this,
				entity,
				skillID,
				damageType,
				damage
			});
		}

		// Token: 0x0600618E RID: 24974 RVA: 0x00270304 File Offset: 0x0026E504
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
					foreach (List<int> list2 in avatar2.buffmag.getBuffBySeid(30))
					{
						int num2 = (int)jsonData.instance.BuffSeidJsonData[30][string.Concat(list2[2])]["value1"].n;
						int num3 = (int)jsonData.instance.BuffSeidJsonData[30][string.Concat(list2[2])]["value2"].n;
						avatar2.recvDamage(avatar2, avatar2.OtherAvatar, 10001 + num3, num2 * list2[1], 0);
					}
				}
				foreach (JSONObject jsonobject in jsonData.instance.skillJsonData[string.Concat(skillId)]["seid"].list)
				{
					if (65 == (int)jsonobject.n)
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
					Event.fireOut("recvDamage", new object[]
					{
						avatar2.OtherAvatar,
						avatar2.OtherAvatar,
						skillId,
						0,
						-damage
					});
				}
				if (!(RoundManager.instance != null) || !RoundManager.instance.IsVirtual)
				{
					avatar.fightTemp.SetRoundDamage(avatar, damage, skillId);
					avatar2.fightTemp.SetRoundReceiveDamage(avatar, damage, skillId);
				}
				if (damage > 0)
				{
					avatar2.spell.onRemoveBuffByType(9, damage);
				}
				avatar2.spell.onRemoveBuffByType(11, 1);
				Event.fireOut("recvDamage", new object[]
				{
					avatar2,
					_attaker,
					skillId,
					0,
					damage
				});
				avatar2.setHP(avatar2.HP - damage);
				if (damage > 0)
				{
					avatar2.spell.onBuffTickByType(42, list);
				}
			}
			return damage;
		}

		// Token: 0x0600618F RID: 24975 RVA: 0x000112BB File Offset: 0x0000F4BB
		public void continuFunc()
		{
			YSFuncList.Ints.Continue();
		}

		// Token: 0x06006190 RID: 24976 RVA: 0x002706B8 File Offset: 0x0026E8B8
		public card addCrystal(int CrystalType, int num = 1)
		{
			List<int> list = new List<int>();
			list.Add(num);
			list.Add(CrystalType);
			this.spell.onBuffTickByType(25, list);
			return this.crystal.addCard(CrystalType, num);
		}

		// Token: 0x06006191 RID: 24977 RVA: 0x00043BFE File Offset: 0x00041DFE
		public void removeCrystal(int CrystalType, int num = 1)
		{
			this.crystal.removeCard(CrystalType, num);
		}

		// Token: 0x06006192 RID: 24978 RVA: 0x00043C0D File Offset: 0x00041E0D
		public void removeCrystal(card CrystalType)
		{
			this.crystal.removeCard(CrystalType);
		}

		// Token: 0x06006193 RID: 24979 RVA: 0x002706F4 File Offset: 0x0026E8F4
		public void UseCryStal(int CrystalType, int num = 1)
		{
			this.NowRoundUsedCard.Add(CrystalType);
			List<int> list = new List<int>();
			list.Add(num);
			list.Add(CrystalType);
			this.spell.onBuffTickByType(27, list);
			this.removeCrystal(CrystalType, num);
		}

		// Token: 0x06006194 RID: 24980 RVA: 0x00270738 File Offset: 0x0026E938
		public void AbandonCryStal(int CrystalType, int num = 1)
		{
			List<int> list = new List<int>();
			list.Add(num);
			list.Add(CrystalType);
			this.removeCrystal(CrystalType, num);
			this.spell.onBuffTickByType(26, list);
		}

		// Token: 0x06006195 RID: 24981 RVA: 0x00270770 File Offset: 0x0026E970
		public void AbandonCryStal(card CrystalType, int num = 1)
		{
			List<int> list = new List<int>();
			list.Add(num);
			list.Add(CrystalType.cardType);
			this.removeCrystal(CrystalType);
			this.spell.onBuffTickByType(26, list);
		}

		// Token: 0x06006196 RID: 24982 RVA: 0x002707AC File Offset: 0x0026E9AC
		public bool checkHasStudyWuDaoSkillByID(int id)
		{
			using (List<SkillItem>.Enumerator enumerator = this.wuDaoMag.GetAllWuDaoSkills().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.itemId == id)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06006197 RID: 24983 RVA: 0x0027080C File Offset: 0x0026EA0C
		public void equipItem(int itemID)
		{
			foreach (ITEM_INFO item_INFO in this.equipItemList.values)
			{
				if (jsonData.instance.ItemJsonData[string.Concat(item_INFO.itemId)]["type"].str == "Skill" && item_INFO.itemId == itemID)
				{
					return;
				}
			}
			ITEM_INFO item_INFO2 = new ITEM_INFO();
			item_INFO2.itemId = itemID;
			this.equipItemList.values.Add(item_INFO2);
		}

		// Token: 0x06006198 RID: 24984 RVA: 0x002708C0 File Offset: 0x0026EAC0
		public void UnEquipItem(int itemID)
		{
			ITEM_INFO item = new ITEM_INFO();
			foreach (ITEM_INFO item_INFO in this.equipItemList.values)
			{
				if (item_INFO.itemId == itemID)
				{
					item = item_INFO;
				}
			}
			this.equipItemList.values.Remove(item);
		}

		// Token: 0x06006199 RID: 24985 RVA: 0x00270934 File Offset: 0x0026EB34
		public void equipSkill(int SkillID, int index = 0)
		{
			this.equipSkillList.RemoveAll((SkillItem aa) => index == aa.itemIndex);
			using (List<SkillItem>.Enumerator enumerator = this.equipSkillList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.itemId == SkillID)
					{
						return;
					}
				}
			}
			SkillItem skillItem = new SkillItem();
			skillItem.itemId = SkillID;
			skillItem.itemIndex = index;
			this.equipSkillList.Add(skillItem);
			PlayTutorial.CheckSkillTask();
		}

		// Token: 0x0600619A RID: 24986 RVA: 0x002709D8 File Offset: 0x0026EBD8
		public void equipStaticSkill(int SkillID, int index = 0)
		{
			this.equipStaticSkillList.RemoveAll((SkillItem aa) => index == aa.itemIndex);
			using (List<SkillItem>.Enumerator enumerator = this.equipStaticSkillList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.itemId == SkillID)
					{
						return;
					}
				}
			}
			SkillItem skillItem = new SkillItem();
			skillItem.itemId = SkillID;
			skillItem.itemIndex = index;
			this.equipStaticSkillList.Add(skillItem);
			GUIPackage.StaticSkill.resetSeid(this);
			PlayTutorial.CheckGongFaTask();
		}

		// Token: 0x0600619B RID: 24987 RVA: 0x00270A84 File Offset: 0x0026EC84
		public void YSequipItem(string UUID, int index = 0, int key = 0)
		{
			ITEM_INFO item_INFO = this.FindItemByUUID(UUID);
			List<ITEM_INFO> list = new List<ITEM_INFO>();
			foreach (ITEM_INFO item_INFO2 in this.equipItemList.values)
			{
				if (_ItemJsonData.DataDict[item_INFO2.itemId].type == _ItemJsonData.DataDict[item_INFO.itemId].type)
				{
					if (this.checkHasStudyWuDaoSkillByID(2231) && (Singleton.equip.Equip[key].UUID == UUID || Singleton.equip.Equip[key].itemID == -1))
					{
						index = key;
					}
					else
					{
						list.Add(item_INFO2);
					}
				}
			}
			foreach (ITEM_INFO item_INFO3 in list)
			{
				this.YSUnequipItem(item_INFO3.uuid, 0);
			}
			item_INFO.itemIndex = index;
			this.equipItemList.values.Add(item_INFO);
			Equips.resetEquipSeid(this);
		}

		// Token: 0x0600619C RID: 24988 RVA: 0x00270BC8 File Offset: 0x0026EDC8
		public _ItemJsonData GetEquipLingZhouData()
		{
			foreach (ITEM_INFO item_INFO in this.equipItemList.values)
			{
				if (_ItemJsonData.DataDict[item_INFO.itemId].type == 14)
				{
					return _ItemJsonData.DataDict[item_INFO.itemId];
				}
			}
			return null;
		}

		// Token: 0x0600619D RID: 24989 RVA: 0x00270C48 File Offset: 0x0026EE48
		public JToken GetNowLingZhouShuXinJson()
		{
			_ItemJsonData equipLingZhouData = this.GetEquipLingZhouData();
			if (equipLingZhouData != null)
			{
				return jsonData.instance.LingZhouPinJie[equipLingZhouData.quality.ToString()];
			}
			return null;
		}

		// Token: 0x0600619E RID: 24990 RVA: 0x00270C7C File Offset: 0x0026EE7C
		public void ReduceLingZhouNaiJiu(BaseItem baseItem, int num)
		{
			baseItem.Seid.SetField("NaiJiu", baseItem.Seid["NaiJiu"].I - num);
			if (baseItem.Seid["NaiJiu"].I <= 0)
			{
				Tools.instance.RemoveItem(baseItem.Uid, 1);
			}
		}

		// Token: 0x0600619F RID: 24991 RVA: 0x00043C1B File Offset: 0x00041E1B
		public void removeEquipItem(string UUID)
		{
			this.YSUnequipItem(UUID, 0);
			this.removeItem(UUID);
		}

		// Token: 0x060061A0 RID: 24992 RVA: 0x00270CDC File Offset: 0x0026EEDC
		public BaseItem GetLingZhou()
		{
			Dictionary<int, BaseItem> curEquipDict = Tools.instance.getPlayer().StreamData.FangAnData.GetCurEquipDict();
			foreach (int key in curEquipDict.Keys)
			{
				BaseItem baseItem = curEquipDict[key];
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

		// Token: 0x060061A1 RID: 24993 RVA: 0x00270DAC File Offset: 0x0026EFAC
		public void YSequipItem(int itemID, int index = 0)
		{
			List<ITEM_INFO> list = new List<ITEM_INFO>();
			foreach (ITEM_INFO item_INFO in this.equipItemList.values)
			{
				if (_ItemJsonData.DataDict[item_INFO.itemId].type == _ItemJsonData.DataDict[itemID].type)
				{
					list.Add(item_INFO);
				}
			}
			foreach (ITEM_INFO item in list)
			{
				this.equipItemList.values.Remove(item);
			}
			ITEM_INFO item_INFO2 = new ITEM_INFO();
			item_INFO2.itemId = itemID;
			item_INFO2.itemIndex = index;
			this.equipItemList.values.Add(item_INFO2);
			this.removeItem(itemID);
			Equips.resetEquipSeid(this);
		}

		// Token: 0x060061A2 RID: 24994 RVA: 0x00270EB0 File Offset: 0x0026F0B0
		public void UnEquipSkill(int SkillID)
		{
			SkillItem item = new SkillItem();
			foreach (SkillItem skillItem in this.equipSkillList)
			{
				if (skillItem.itemId == SkillID)
				{
					item = skillItem;
					break;
				}
			}
			this.equipSkillList.Remove(item);
		}

		// Token: 0x060061A3 RID: 24995 RVA: 0x00270F1C File Offset: 0x0026F11C
		public void UnEquipStaticSkill(int SkillID)
		{
			SkillItem item = new SkillItem();
			foreach (SkillItem skillItem in this.equipStaticSkillList)
			{
				if (skillItem.itemId == SkillID)
				{
					item = skillItem;
					break;
				}
			}
			this.equipStaticSkillList.Remove(item);
			GUIPackage.StaticSkill.resetSeid(this);
			if (this.HP > this.HP_Max)
			{
				this.HP = this.HP_Max;
			}
		}

		// Token: 0x060061A4 RID: 24996 RVA: 0x00270FA8 File Offset: 0x0026F1A8
		public void YSUnequipItem(string UUID, int index = 0)
		{
			ITEM_INFO item_INFO = this.FindItemByUUID(UUID);
			if (item_INFO == null && UUID != "")
			{
				this.itemList.values.Add(this.FindEquipItemByUUID(UUID));
				item_INFO = this.FindItemByUUID(UUID);
			}
			item_INFO.itemIndex = index;
			this.removeEquip(UUID);
			Equips.resetEquipSeid(this);
		}

		// Token: 0x060061A5 RID: 24997 RVA: 0x00271000 File Offset: 0x0026F200
		public void removeEquip(string UUID)
		{
			this.equipItemList.values.RemoveAll((ITEM_INFO aa) => aa.uuid == UUID);
		}

		// Token: 0x060061A6 RID: 24998 RVA: 0x00271038 File Offset: 0x0026F238
		public void removeEquip(int id, int sum)
		{
			for (int i = 0; i < sum; i++)
			{
				this.removeEquipByItemID(id);
			}
		}

		// Token: 0x060061A7 RID: 24999 RVA: 0x00271058 File Offset: 0x0026F258
		private void removeEquipByItemID(int id)
		{
			ITEM_INFO item_INFO = new ITEM_INFO();
			for (int i = 0; i < this.equipItemList.values.Count; i++)
			{
				if (this.equipItemList.values[i].itemId == id)
				{
					item_INFO = this.equipItemList.values[i];
					break;
				}
			}
			this.equipItemList.values.Remove(item_INFO);
			string uuid = item_INFO.uuid;
			this.removeItem(uuid);
		}

		// Token: 0x060061A8 RID: 25000 RVA: 0x002710D4 File Offset: 0x0026F2D4
		public void YSUnequipItem(int itemID)
		{
			ITEM_INFO item_INFO = new ITEM_INFO();
			foreach (ITEM_INFO item_INFO2 in this.equipItemList.values)
			{
				if (item_INFO2.itemId == itemID)
				{
					item_INFO = item_INFO2;
					break;
				}
			}
			this.addItem(itemID, item_INFO.Seid, 1);
			this.equipItemList.values.Remove(item_INFO);
		}

		// Token: 0x060061A9 RID: 25001 RVA: 0x00271158 File Offset: 0x0026F358
		public int getItemNum(int itemID)
		{
			int num = 0;
			int num2 = (int)jsonData.instance.ItemJsonData[string.Concat(itemID)]["maxNum"].n;
			foreach (ITEM_INFO item_INFO in this.itemList.values)
			{
				if (item_INFO.itemId == itemID && (ulong)item_INFO.itemCount <= (ulong)((long)num2))
				{
					num += (int)item_INFO.itemCount;
				}
			}
			foreach (ITEM_INFO item_INFO2 in this.equipItemList.values)
			{
				if (item_INFO2.itemId == itemID && (ulong)item_INFO2.itemCount <= (ulong)((long)num2))
				{
					num += (int)item_INFO2.itemCount;
				}
			}
			return num;
		}

		// Token: 0x060061AA RID: 25002 RVA: 0x00271258 File Offset: 0x0026F458
		public ITEM_INFO getItemInfo(int itemID)
		{
			foreach (ITEM_INFO item_INFO in this.itemList.values)
			{
				if (item_INFO.itemId == itemID)
				{
					return item_INFO;
				}
			}
			return null;
		}

		// Token: 0x060061AB RID: 25003 RVA: 0x002712BC File Offset: 0x0026F4BC
		public bool YuJianFeiXing()
		{
			foreach (SkillItem skillItem in this.equipStaticSkillList)
			{
				int staticSkillKeyByID = Tools.instance.getStaticSkillKeyByID(skillItem.itemId);
				if (StaticSkillJsonData.DataDict[staticSkillKeyByID].seid.Contains(9))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060061AC RID: 25004 RVA: 0x0027133C File Offset: 0x0026F53C
		public void addItem(int itemID, int Count, JSONObject _seid, bool ShowText = false)
		{
			if (!_ItemJsonData.DataDict.ContainsKey(itemID))
			{
				Debug.LogError(string.Format("添加物品出现异常，不存在ID为{0}的物品", itemID));
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
				JSONObject jsonobject = jsonData.instance.ItemJsonData[itemID.ToString()];
				if (itemJsonData.seid.Contains(21))
				{
					item item = new item(itemID);
					for (int i = 0; i < Count; i++)
					{
						item.gongneng(null, false);
					}
					return;
				}
			}
			catch (Exception ex)
			{
				Debug.LogError(ex);
			}
			this.addItem(itemID, _seid, Count);
			if (Singleton.inventory != null)
			{
				for (int j = 0; j < Count; j++)
				{
					Singleton.inventory.AddItem(itemID);
				}
			}
		}

		// Token: 0x060061AD RID: 25005 RVA: 0x00271434 File Offset: 0x0026F634
		public void AddEquip(int itemID, string uuid, JSONObject _seid)
		{
			if (!_ItemJsonData.DataDict.ContainsKey(itemID))
			{
				Debug.LogError(string.Format("添加物品出现异常，不存在ID为{0}的物品", itemID));
				return;
			}
			_ItemJsonData itemJsonData = _ItemJsonData.DataDict[itemID];
			if (_seid != null && _seid.HasField("isPaiMai"))
			{
				_seid.RemoveField("isPaiMai");
			}
			try
			{
				JSONObject jsonobject = jsonData.instance.ItemJsonData[itemID.ToString()];
				if (itemJsonData.seid.Contains(21))
				{
					item item = new item(itemID);
					for (int i = 0; i < 1; i++)
					{
						item.gongneng(null, false);
					}
					return;
				}
			}
			catch (Exception ex)
			{
				Debug.LogError(ex);
			}
			this.AddEquip(itemID, _seid, uuid);
			if (Singleton.inventory != null)
			{
				for (int j = 0; j < 1; j++)
				{
					Singleton.inventory.AddItem(itemID);
				}
			}
		}

		// Token: 0x060061AE RID: 25006 RVA: 0x00271518 File Offset: 0x0026F718
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
					ITEM_INFO item_INFO = new ITEM_INFO();
					item_INFO.uuid = uid;
					item_INFO.itemId = itemID;
					item_INFO.itemCount = 1U;
					item_INFO.Seid = _seid;
					this.itemList.values.Add(item_INFO);
				}
				return;
			}
			if (this.getItemNum(itemID) == 0)
			{
				ITEM_INFO item_INFO2 = new ITEM_INFO();
				item_INFO2.uuid = uid;
				item_INFO2.itemId = itemID;
				item_INFO2.itemCount = 1U;
				this.itemList.values.Add(item_INFO2);
				return;
			}
			this.getItemInfo(itemID).itemCount += 1U;
		}

		// Token: 0x060061AF RID: 25007 RVA: 0x002715D8 File Offset: 0x0026F7D8
		public ITEM_INFO FindItemByUUID(string itemUUId)
		{
			return this.itemList.values.Find((ITEM_INFO aa) => aa.uuid == itemUUId);
		}

		// Token: 0x060061B0 RID: 25008 RVA: 0x00271610 File Offset: 0x0026F810
		public ITEM_INFO FindEquipItemByUUID(string itemUUId)
		{
			return this.equipItemList.values.Find((ITEM_INFO aa) => aa.uuid == itemUUId);
		}

		// Token: 0x060061B1 RID: 25009 RVA: 0x00271648 File Offset: 0x0026F848
		public void addItem(int itemID, JSONObject _seid, int count = 1)
		{
			if (_seid == null)
			{
				_seid = new JSONObject(JSONObject.Type.OBJECT);
			}
			_ItemJsonData itemJsonData = _ItemJsonData.DataDict[itemID];
			if (itemJsonData.type == 6 && !this.YaoCaiIsGet.HasItem(itemID))
			{
				this.YaoCaiIsGet.Add(itemID);
			}
			if (itemJsonData.maxNum == 1 || (_seid != null && _seid.Count > 0))
			{
				if (count > 50)
				{
					Debug.LogError("警告获取的不能堆叠物品" + itemID + "超过50个无法直接添加");
					UIPopTip.Inst.Pop("警告获取的不能堆叠物品" + itemID + "超过50个无法直接添加", PopTipIconType.叹号);
					return;
				}
				for (int i = 0; i < count; i++)
				{
					ITEM_INFO item_INFO = new ITEM_INFO();
					item_INFO.uuid = Tools.getUUID();
					item_INFO.itemId = itemID;
					item_INFO.itemCount = 1U;
					item_INFO.Seid = _seid;
					this.itemList.values.Add(item_INFO);
				}
			}
			else if (this.getItemNum(itemID) == 0)
			{
				ITEM_INFO item_INFO2 = new ITEM_INFO();
				item_INFO2.uuid = Tools.getUUID();
				item_INFO2.itemId = itemID;
				item_INFO2.itemCount = (uint)count;
				this.itemList.values.Add(item_INFO2);
			}
			else
			{
				this.getItemInfo(itemID).itemCount += (uint)count;
			}
			if (base.isPlayer() && itemJsonData.TuJianType > 0)
			{
				TuJianManager.Inst.UnlockItem(itemID);
				if (itemJsonData.TuJianType == 1)
				{
					if (this.GetHasYaoYinShuXin(itemID, itemJsonData.quality))
					{
						TuJianManager.Inst.UnlockYaoYin(itemID);
					}
					if (this.GetHasZhuYaoShuXin(itemID, itemJsonData.quality))
					{
						TuJianManager.Inst.UnlockZhuYao(itemID);
					}
					if (this.GetHasFuYaoShuXin(itemID, itemJsonData.quality))
					{
						TuJianManager.Inst.UnlockFuYao(itemID);
					}
				}
			}
		}

		// Token: 0x060061B2 RID: 25010 RVA: 0x002717F4 File Offset: 0x0026F9F4
		public int getRemoveItemNum(int itemID)
		{
			int num = 0;
			try
			{
				foreach (ITEM_INFO item_INFO in this.itemList.values)
				{
					if (item_INFO.itemId == itemID)
					{
						num += (int)item_INFO.itemCount;
					}
				}
			}
			catch (Exception)
			{
				Debug.Log(string.Format("出错物品ID:{0}------------------------------------------", itemID));
			}
			return num;
		}

		// Token: 0x060061B3 RID: 25011 RVA: 0x00271880 File Offset: 0x0026FA80
		public void removeItem(int itemID, int Count)
		{
			for (int i = 0; i < Count; i++)
			{
				this.removeItem(itemID);
			}
		}

		// Token: 0x060061B4 RID: 25012 RVA: 0x002718A0 File Offset: 0x0026FAA0
		public void removeItem(string UUID, int Count)
		{
			for (int i = 0; i < Count; i++)
			{
				this.removeItem(UUID);
			}
		}

		// Token: 0x060061B5 RID: 25013 RVA: 0x002718C0 File Offset: 0x0026FAC0
		public void removeItem(string UUID)
		{
			ITEM_INFO item_INFO = this.FindItemByUUID(UUID);
			if (item_INFO == null)
			{
				return;
			}
			item_INFO.itemCount -= 1U;
			if (item_INFO.itemCount <= 0U)
			{
				this.itemList.values.Remove(item_INFO);
			}
		}

		// Token: 0x060061B6 RID: 25014 RVA: 0x00271904 File Offset: 0x0026FB04
		public void removeItem(int itemID)
		{
			if (this.getRemoveItemNum(itemID) > 0)
			{
				ITEM_INFO itemInfo = this.getItemInfo(itemID);
				itemInfo.itemCount -= 1U;
				if (itemInfo.itemCount <= 0U)
				{
					this.itemList.values.Remove(itemInfo);
				}
			}
		}

		// Token: 0x060061B7 RID: 25015 RVA: 0x0027194C File Offset: 0x0026FB4C
		public void Load(int id, int index)
		{
			if (File.Exists(Paths.GetSavePath() + "/StreamData" + Tools.instance.getSaveID(id, index) + ".sav"))
			{
				FileStream fileStream = new FileStream(Paths.GetSavePath() + "/StreamData" + Tools.instance.getSaveID(id, index) + ".sav", FileMode.Open, FileAccess.Read, FileShare.Read);
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				this.StreamData = (StreamData)binaryFormatter.Deserialize(fileStream);
				fileStream.Close();
			}
			else
			{
				this.StreamData = new StreamData();
			}
			this.StreamData.FangAnData.LoadHandle();
		}

		// Token: 0x060061B8 RID: 25016 RVA: 0x002719E4 File Offset: 0x0026FBE4
		public void Save(int id, int index)
		{
			StreamData streamData = this.StreamData;
			streamData.FangAnData.SaveHandle();
			FileStream fileStream = new FileStream(Paths.GetSavePath() + "/StreamData" + Tools.instance.getSaveID(id, index) + ".sav", FileMode.Create);
			new BinaryFormatter().Serialize(fileStream, streamData);
			fileStream.Close();
		}

		// Token: 0x060061B9 RID: 25017 RVA: 0x000042DD File Offset: 0x000024DD
		public void createItem()
		{
		}

		// Token: 0x060061BA RID: 25018 RVA: 0x00271A3C File Offset: 0x0026FC3C
		public bool hasItem(int itemID)
		{
			bool result = false;
			using (List<ITEM_INFO>.Enumerator enumerator = this.itemList.values.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.itemId == itemID)
					{
						result = true;
					}
				}
			}
			return result;
		}

		// Token: 0x060061BB RID: 25019 RVA: 0x00043C2C File Offset: 0x00041E2C
		public void SortItem()
		{
			this.itemList.values.Sort(delegate(ITEM_INFO a, ITEM_INFO b)
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
			});
		}

		// Token: 0x060061BC RID: 25020 RVA: 0x00271A9C File Offset: 0x0026FC9C
		public static void UnlockShenXianDouFa(int index)
		{
			int num = index + 100;
			int num2 = 10001 + index;
			if (YSSaveGame.GetInt("SaveAvatar" + num, 0) != 0)
			{
				return;
			}
			UIPopTip.Inst.Pop("已开启新的神仙斗法", PopTipIconType.叹号);
			YSSaveGame.save("SaveAvatar" + num, 1, "-1");
			YSSaveGame.save("SaveDFAvatar" + num, 2, "-1");
			JSONObject jsonobject = new JSONObject(JSONObject.Type.OBJECT);
			jsonobject.SetField("1", jsonData.instance.AvatarRandomJsonData[num2.ToString()]);
			YSSaveGame.save("AvatarRandomJsonData" + Tools.instance.getSaveID(num, 0), jsonobject, "-1");
		}

		// Token: 0x060061BD RID: 25021 RVA: 0x00043965 File Offset: 0x00041B65
		public void SetMenPai(int id)
		{
			this.menPai = (ushort)id;
		}

		// Token: 0x060061BE RID: 25022 RVA: 0x00043C5D File Offset: 0x00041E5D
		public void SetLingGen(int id, int value)
		{
			this.LingGeng[id] = value;
		}

		// Token: 0x060061BF RID: 25023 RVA: 0x000042DD File Offset: 0x000024DD
		[Obsolete]
		public void startFight(int fightID)
		{
		}

		// Token: 0x060061C0 RID: 25024 RVA: 0x00043C6C File Offset: 0x00041E6C
		[Obsolete]
		public void reqItemList()
		{
			base.baseCall("reqItemList", Array.Empty<object>());
		}

		// Token: 0x060061C1 RID: 25025 RVA: 0x00043C7E File Offset: 0x00041E7E
		[Obsolete]
		public void dropRequest(ulong itemUUID)
		{
			base.baseCall("dropRequest", new object[]
			{
				itemUUID
			});
		}

		// Token: 0x060061C2 RID: 25026 RVA: 0x00271B60 File Offset: 0x0026FD60
		[Obsolete]
		public void swapItemRequest(int srcIndex, int dstIndex)
		{
			ulong num = this.itemIndex2Uids[srcIndex];
			ulong num2 = this.itemIndex2Uids[dstIndex];
			this.itemIndex2Uids[srcIndex] = num2;
			if (num2 != 0UL)
			{
				this.itemDict[num2].itemIndex = srcIndex;
			}
			this.itemIndex2Uids[dstIndex] = num;
			if (num != 0UL)
			{
				this.itemDict[num].itemIndex = dstIndex;
			}
			base.baseCall("swapItemRequest", new object[]
			{
				srcIndex,
				dstIndex
			});
		}

		// Token: 0x060061C3 RID: 25027 RVA: 0x00043C9A File Offset: 0x00041E9A
		[Obsolete]
		public void equipItemRequest(ulong itemUUID)
		{
			base.baseCall("equipItemRequest", new object[]
			{
				itemUUID
			});
		}

		// Token: 0x060061C4 RID: 25028 RVA: 0x00043CB6 File Offset: 0x00041EB6
		[Obsolete]
		public void UnEquipItemRequest(ulong itemUUID)
		{
			base.baseCall("UnEquipItemRequest", new object[]
			{
				itemUUID
			});
		}

		// Token: 0x060061C5 RID: 25029 RVA: 0x00271BE0 File Offset: 0x0026FDE0
		[Obsolete]
		public override object getDefinedProperty(string name)
		{
			uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
			if (num <= 1437283280U)
			{
				if (num <= 611129337U)
				{
					if (num != 339128649U)
					{
						if (num == 611129337U)
						{
							if (name == "MP_Max")
							{
								return this.MP_Max;
							}
						}
					}
					else if (name == "roleTypeCell")
					{
						return this.roleTypeCell;
					}
				}
				else if (num != 853522520U)
				{
					if (num != 1331921123U)
					{
						if (num == 1437283280U)
						{
							if (name == "HP_Max")
							{
								return this.HP_Max;
							}
						}
					}
					else if (name == "equipWeapon")
					{
						return this.equipWeapon;
					}
				}
				else if (name == "MP")
				{
					return this.MP;
				}
			}
			else if (num <= 2016490230U)
			{
				if (num != 1802331040U)
				{
					if (num != 1894470373U)
					{
						if (num == 2016490230U)
						{
							if (name == "state")
							{
								return this.state;
							}
						}
					}
					else if (name == "HP")
					{
						return this.HP;
					}
				}
				else if (name == "roleSurfaceCall")
				{
					return this.roleSurfaceCall;
				}
			}
			else if (num != 2369371622U)
			{
				if (num != 2471448074U)
				{
					if (num == 2610554845U)
					{
						if (name == "level")
						{
							return this.level;
						}
					}
				}
				else if (name == "position")
				{
					return this.position;
				}
			}
			else if (name == "name")
			{
				return this.name;
			}
			return null;
		}

		// Token: 0x060061C6 RID: 25030 RVA: 0x00043CD2 File Offset: 0x00041ED2
		[Obsolete]
		public void CreateAvaterCall(int AvaterID)
		{
			base.baseCall("CreateAvaterCall", new object[]
			{
				AvaterID
			});
		}

		// Token: 0x060061C7 RID: 25031 RVA: 0x00271DF4 File Offset: 0x0026FFF4
		[Obsolete]
		public void useItemRequest(ulong itemIndex)
		{
			if (!ConsumeLimitCD.instance.isWaiting())
			{
				base.baseCall("useItemRequest", new object[]
				{
					itemIndex
				});
				ConsumeLimitCD.instance.Start(2f);
				return;
			}
			UIPopTip.Inst.Pop("物品使用冷却中", PopTipIconType.叹号);
		}

		// Token: 0x060061C8 RID: 25032 RVA: 0x00271E48 File Offset: 0x00270048
		[Obsolete]
		public override void onAttack_MaxChanged(int old)
		{
			object obj = this.attack_Max;
			Event.fireOut("set_attack_Max", new object[]
			{
				obj
			});
		}

		// Token: 0x060061C9 RID: 25033 RVA: 0x00271E78 File Offset: 0x00270078
		[Obsolete]
		public override void onAttack_MinChanged(int old)
		{
			object obj = this.attack_Min;
			Event.fireOut("set_attack_Min", new object[]
			{
				obj
			});
		}

		// Token: 0x060061CA RID: 25034 RVA: 0x00271EA8 File Offset: 0x002700A8
		[Obsolete]
		public override void onDefenceChanged(int old)
		{
			object obj = this.defence;
			Event.fireOut("set_defence", new object[]
			{
				obj
			});
		}

		// Token: 0x060061CB RID: 25035 RVA: 0x00271ED8 File Offset: 0x002700D8
		[Obsolete]
		public override void onRatingChanged(int old)
		{
			object obj = this.rating;
			Event.fireOut("set_rating", new object[]
			{
				obj
			});
		}

		// Token: 0x060061CC RID: 25036 RVA: 0x00271F08 File Offset: 0x00270108
		[Obsolete]
		public override void onDodgeChanged(int old)
		{
			object obj = this.dodge;
			Event.fireOut("set_dodge", new object[]
			{
				obj
			});
		}

		// Token: 0x060061CD RID: 25037 RVA: 0x00271F38 File Offset: 0x00270138
		[Obsolete]
		public override void onStrengthChanged(int old)
		{
			object obj = this.strength;
			Event.fireOut("set_strength", new object[]
			{
				obj
			});
		}

		// Token: 0x060061CE RID: 25038 RVA: 0x00271F68 File Offset: 0x00270168
		[Obsolete]
		public override void onDexterityChanged(int old)
		{
			object obj = this.dexterity;
			Event.fireOut("set_dexterity", new object[]
			{
				obj
			});
		}

		// Token: 0x060061CF RID: 25039 RVA: 0x00271F98 File Offset: 0x00270198
		[Obsolete]
		public override void onExpChanged(ulong old)
		{
			object obj = this.exp;
			Event.fireOut("set_exp", new object[]
			{
				obj
			});
		}

		// Token: 0x060061D0 RID: 25040 RVA: 0x00271FC8 File Offset: 0x002701C8
		[Obsolete]
		public override void onLevelChanged(ushort old)
		{
			object obj = this.level;
			Event.fireOut("set_level", new object[]
			{
				obj
			});
		}

		// Token: 0x060061D1 RID: 25041 RVA: 0x00043CEE File Offset: 0x00041EEE
		[Obsolete]
		public override void onCrystalChanged(List<int> oldValue)
		{
			Event.fireOut("crtstalChanged", new object[]
			{
				this,
				oldValue
			});
		}

		// Token: 0x060061D2 RID: 25042 RVA: 0x00271FF8 File Offset: 0x002701F8
		[Obsolete]
		public override void onStaminaChanged(int old)
		{
			object obj = this.stamina;
			Event.fireOut("set_stamina", new object[]
			{
				obj
			});
		}

		// Token: 0x060061D3 RID: 25043 RVA: 0x00043D08 File Offset: 0x00041F08
		[Obsolete]
		public void dialog(int targetID, uint dialogID)
		{
			this.dialogMsg.dialog(targetID, dialogID);
		}

		// Token: 0x060061D4 RID: 25044 RVA: 0x00043D17 File Offset: 0x00041F17
		[Obsolete]
		public void messagelog(int targetID, uint dialogID)
		{
			this.dialogMsg.messagelog(targetID, dialogID);
		}

		// Token: 0x060061D5 RID: 25045 RVA: 0x00272028 File Offset: 0x00270228
		[Obsolete]
		public override void dropItem_re(int itemId, ulong itemUUId)
		{
			int itemIndex = this.itemDict[itemUUId].itemIndex;
			this.itemDict.Remove(itemUUId);
			this.itemIndex2Uids[itemIndex] = 0UL;
			Event.fireOut("dropItem_re", new object[]
			{
				itemIndex,
				itemUUId
			});
		}

		// Token: 0x060061D6 RID: 25046 RVA: 0x00043D26 File Offset: 0x00041F26
		[Obsolete]
		public override void pickUp_re(ITEM_INFO itemInfo)
		{
			Event.fireOut("pickUp_re", new object[]
			{
				itemInfo
			});
			this.itemDict[itemInfo.UUID] = itemInfo;
		}

		// Token: 0x060061D7 RID: 25047 RVA: 0x00272080 File Offset: 0x00270280
		[Obsolete]
		public override void equipItemRequest_re(ITEM_INFO itemInfo, ITEM_INFO equipItemInfo)
		{
			Event.fireOut("equipItemRequest_re", new object[]
			{
				itemInfo,
				equipItemInfo
			});
			ulong uuid = itemInfo.UUID;
			ulong uuid2 = equipItemInfo.UUID;
			if (uuid == 0UL && uuid2 != 0UL)
			{
				this.equipItemDict[uuid2] = equipItemInfo;
				this.itemDict.Remove(uuid2);
				return;
			}
			if (uuid != 0UL && uuid2 != 0UL)
			{
				this.itemDict.Remove(uuid2);
				this.equipItemDict[uuid2] = equipItemInfo;
				this.equipItemDict.Remove(uuid);
				this.itemDict[uuid] = itemInfo;
				return;
			}
			if (uuid != 0UL && uuid2 == 0UL)
			{
				this.equipItemDict.Remove(uuid);
				this.itemDict[uuid] = itemInfo;
			}
		}

		// Token: 0x060061D8 RID: 25048 RVA: 0x00272130 File Offset: 0x00270330
		[Obsolete]
		public override void onReqItemList(ITEM_INFO_LIST infos, ITEM_INFO_LIST equipInfos)
		{
			this.itemDict.Clear();
			List<ITEM_INFO> values = infos.values;
			for (int i = 0; i < values.Count; i++)
			{
				ITEM_INFO item_INFO = values[i];
				this.itemDict.Add(item_INFO.UUID, item_INFO);
				this.itemIndex2Uids[item_INFO.itemIndex] = item_INFO.UUID;
			}
			this.equipItemDict.Clear();
			List<ITEM_INFO> values2 = equipInfos.values;
			for (int j = 0; j < values2.Count; j++)
			{
				ITEM_INFO item_INFO2 = values2[j];
				this.equipItemDict.Add(item_INFO2.UUID, item_INFO2);
				this.equipIndex2Uids[item_INFO2.itemIndex] = item_INFO2.UUID;
			}
			Event.fireOut("onReqItemList", new object[]
			{
				this.itemDict,
				this.equipItemDict
			});
		}

		// Token: 0x060061D9 RID: 25049 RVA: 0x00043D4E File Offset: 0x00041F4E
		[Obsolete]
		public override void errorInfo(int errorCode)
		{
			Dbg.DEBUG_MSG("errorInfo(" + errorCode + ")");
		}

		// Token: 0x060061DA RID: 25050 RVA: 0x0027220C File Offset: 0x0027040C
		[Obsolete]
		public virtual void onEquipWeaponChanged(object old)
		{
			object obj = this.equipWeapon;
			Event.fireOut("set_equipWeapon", new object[]
			{
				this,
				(int)obj
			});
		}

		// Token: 0x060061DB RID: 25051 RVA: 0x00043D6A File Offset: 0x00041F6A
		[Obsolete]
		public override void dialog_setContent(int talkerId, List<uint> dialogs, List<string> dialogsTitles, string title, string body, string sayname)
		{
			Event.fireOut("dialog_setContent", new object[]
			{
				talkerId,
				dialogs,
				dialogsTitles,
				title,
				body,
				sayname
			});
		}

		// Token: 0x060061DC RID: 25052 RVA: 0x00043D9C File Offset: 0x00041F9C
		[Obsolete]
		public void messagelog_setContent(int talkerId, string title, string body, string sayname)
		{
			Event.fireOut("messagelog_setContent", new object[]
			{
				talkerId,
				title,
				body,
				sayname
			});
		}

		// Token: 0x060061DD RID: 25053 RVA: 0x00043DC4 File Offset: 0x00041FC4
		[Obsolete]
		public override void dialog_close()
		{
			Event.fireOut("dialog_close", Array.Empty<object>());
		}

		// Token: 0x060061DE RID: 25054 RVA: 0x00043DD5 File Offset: 0x00041FD5
		[Obsolete]
		public void StartCrafting(int itemID, int Count)
		{
			base.baseCall("StartCrafting", new object[]
			{
				itemID,
				Count
			});
		}

		// Token: 0x060061DF RID: 25055 RVA: 0x00043DFA File Offset: 0x00041FFA
		[Obsolete]
		public void CancelCrafting()
		{
			base.baseCall("CancelCrafting", Array.Empty<object>());
		}

		// Token: 0x060061E0 RID: 25056 RVA: 0x00043E0C File Offset: 0x0004200C
		[Obsolete]
		public void backToHome()
		{
			base.baseCall("backToHome", Array.Empty<object>());
		}

		// Token: 0x060061E1 RID: 25057 RVA: 0x00043E1E File Offset: 0x0004201E
		[Obsolete]
		public override void PlayerAddGoods(ITEM_INFO_LIST Infos, ushort day, ushort exp)
		{
			Event.fireOut("showCollect", new object[]
			{
				Infos,
				day,
				exp
			});
		}

		// Token: 0x060061E2 RID: 25058 RVA: 0x00043E46 File Offset: 0x00042046
		[Obsolete]
		public override void setPlayerTime(uint time)
		{
			Event.fireOut("setPlayerTime", new object[]
			{
				time
			});
		}

		// Token: 0x060061E3 RID: 25059 RVA: 0x00043E61 File Offset: 0x00042061
		[Obsolete]
		public override void GameErrorMsg(string msg)
		{
			Event.fireOut("GameErrorMsg", new object[]
			{
				msg
			});
		}

		// Token: 0x060061E4 RID: 25060 RVA: 0x00043E77 File Offset: 0x00042077
		[Obsolete]
		public void DayZombie()
		{
			base.cellCall("DayZombie", Array.Empty<object>());
		}

		// Token: 0x060061E5 RID: 25061 RVA: 0x00043E89 File Offset: 0x00042089
		[Obsolete]
		public override void PlayerLvUP()
		{
			Event.fireOut("PlayerLvUP", Array.Empty<object>());
		}

		// Token: 0x060061E6 RID: 25062 RVA: 0x0001C722 File Offset: 0x0001A922
		[Obsolete]
		public override void createItem(ITEM_INFO arg1)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060061E7 RID: 25063 RVA: 0x00043E9A File Offset: 0x0004209A
		[Obsolete]
		public override void onBuffsChanged(List<ushort> oldValue)
		{
			if (this.renderObj != null)
			{
				Event.fireOut("set_Buffs", new object[]
				{
					this,
					oldValue,
					this.buffs
				});
			}
		}

		// Token: 0x060061E8 RID: 25064 RVA: 0x0001C722 File Offset: 0x0001A922
		[Obsolete]
		public override void onStartGame()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060061E9 RID: 25065 RVA: 0x00272248 File Offset: 0x00270448
		[Obsolete]
		public override void onHPChanged(int oldValue)
		{
			object definedProperty = this.getDefinedProperty("HP");
			Event.fireOut("set_HP", new object[]
			{
				this,
				definedProperty
			});
		}

		// Token: 0x060061EA RID: 25066 RVA: 0x00043EC5 File Offset: 0x000420C5
		[Obsolete]
		public override void onMPChanged(int oldValue)
		{
			this.getDefinedProperty("MP");
		}

		// Token: 0x060061EB RID: 25067 RVA: 0x0027227C File Offset: 0x0027047C
		[Obsolete]
		public override void on_HP_MaxChanged(int oldValue)
		{
			object definedProperty = this.getDefinedProperty("HP_Max");
			Event.fireOut("set_HP_Max", new object[]
			{
				this,
				definedProperty
			});
		}

		// Token: 0x060061EC RID: 25068 RVA: 0x00043ED3 File Offset: 0x000420D3
		[Obsolete]
		public override void onMP_MaxChanged(int oldValue)
		{
			this.getDefinedProperty("MP_Max");
		}

		// Token: 0x060061ED RID: 25069 RVA: 0x002722B0 File Offset: 0x002704B0
		[Obsolete]
		public override void onNameChanged(string oldValue)
		{
			object definedProperty = this.getDefinedProperty("name");
			Event.fireOut("set_name", new object[]
			{
				this,
				definedProperty
			});
		}

		// Token: 0x060061EE RID: 25070 RVA: 0x002722E4 File Offset: 0x002704E4
		[Obsolete]
		public override void onStateChanged(sbyte oldValue)
		{
			object definedProperty = this.getDefinedProperty("state");
			Event.fireOut("set_state", new object[]
			{
				this,
				definedProperty
			});
		}

		// Token: 0x060061EF RID: 25071 RVA: 0x000042DD File Offset: 0x000024DD
		[Obsolete]
		public override void onSubStateChanged(byte oldValue)
		{
		}

		// Token: 0x060061F0 RID: 25072 RVA: 0x000042DD File Offset: 0x000024DD
		[Obsolete]
		public override void onUtypeChanged(uint oldValue)
		{
		}

		// Token: 0x060061F1 RID: 25073 RVA: 0x000042DD File Offset: 0x000024DD
		[Obsolete]
		public override void onUidChanged(uint oldValue)
		{
		}

		// Token: 0x060061F2 RID: 25074 RVA: 0x000042DD File Offset: 0x000024DD
		[Obsolete]
		public override void onSpaceUTypeChanged(uint oldValue)
		{
		}

		// Token: 0x060061F3 RID: 25075 RVA: 0x00043EE1 File Offset: 0x000420E1
		[Obsolete]
		public override void onMoveSpeedChanged(byte oldValue)
		{
			this.getDefinedProperty("moveSpeed");
		}

		// Token: 0x060061F4 RID: 25076 RVA: 0x00043EEF File Offset: 0x000420EF
		[Obsolete]
		public override void onHungerChanged(short oldValue)
		{
			Event.fireOut("set_Hunger", new object[]
			{
				oldValue
			});
		}

		// Token: 0x060061F5 RID: 25077 RVA: 0x00043F0A File Offset: 0x0004210A
		[Obsolete]
		public override void onThirstChanged(short oldValue)
		{
			Event.fireOut("set_Thirst", new object[]
			{
				oldValue
			});
		}

		// Token: 0x04005BEB RID: 23531
		public string lastScence = "AllMaps";

		// Token: 0x04005BEC RID: 23532
		public string lastFuBenScence = "";

		// Token: 0x04005BED RID: 23533
		public string NowFuBen = "";

		// Token: 0x04005BEE RID: 23534
		public int BanBenHao = 1;

		// Token: 0x04005BEF RID: 23535
		public int NowRandomFuBenID;

		// Token: 0x04005BF0 RID: 23536
		public int showSkillName;

		// Token: 0x04005BF1 RID: 23537
		public int showStaticSkillDengJi;

		// Token: 0x04005BF2 RID: 23538
		public int chengHao;

		// Token: 0x04005BF3 RID: 23539
		public CardMag cardMag;

		// Token: 0x04005BF4 RID: 23540
		public Combat combat;

		// Token: 0x04005BF5 RID: 23541
		public AI ai;

		// Token: 0x04005BF6 RID: 23542
		public Spell spell;

		// Token: 0x04005BF7 RID: 23543
		public JieYin jieyin;

		// Token: 0x04005BF8 RID: 23544
		public Dialog dialogMsg;

		// Token: 0x04005BF9 RID: 23545
		public BuffMag buffmag;

		// Token: 0x04005BFA RID: 23546
		public WuDaoMag wuDaoMag;

		// Token: 0x04005BFB RID: 23547
		public WorldTime worldTimeMag = new WorldTime();

		// Token: 0x04005BFC RID: 23548
		public EmailDataMag emailDateMag = new EmailDataMag();

		// Token: 0x04005BFD RID: 23549
		public StreamData StreamData = new StreamData();

		// Token: 0x04005BFE RID: 23550
		public TaskMag taskMag;

		// Token: 0x04005BFF RID: 23551
		public FightTempValue fightTemp;

		// Token: 0x04005C00 RID: 23552
		public ZulinContorl zulinContorl;

		// Token: 0x04005C01 RID: 23553
		public FubenContrl fubenContorl;

		// Token: 0x04005C02 RID: 23554
		public NomelTaskMag nomelTaskMag;

		// Token: 0x04005C03 RID: 23555
		public chenghaoMag chenghaomag;

		// Token: 0x04005C04 RID: 23556
		public RandomFuBenMag randomFuBenMag;

		// Token: 0x04005C05 RID: 23557
		public SeaNodeMag seaNodeMag;

		// Token: 0x04005C06 RID: 23558
		public ChuanYingManager chuanYingManager;

		// Token: 0x04005C07 RID: 23559
		public JianLingManager jianLingManager;

		// Token: 0x04005C08 RID: 23560
		public static SkillBox skillbox = new SkillBox();

		// Token: 0x04005C09 RID: 23561
		public Dictionary<ulong, ITEM_INFO> itemDict = new Dictionary<ulong, ITEM_INFO>();

		// Token: 0x04005C0A RID: 23562
		public Dictionary<ulong, ITEM_INFO> equipItemDict = new Dictionary<ulong, ITEM_INFO>();

		// Token: 0x04005C0B RID: 23563
		public List<Skill> skill = new List<Skill>();

		// Token: 0x04005C0C RID: 23564
		public List<StaticSkill> StaticSkill = new List<StaticSkill>();

		// Token: 0x04005C0D RID: 23565
		private ulong[] itemIndex2Uids = new ulong[50];

		// Token: 0x04005C0E RID: 23566
		private ulong[] equipIndex2Uids = new ulong[5];

		// Token: 0x04005C0F RID: 23567
		public List<List<int>> bufflist = new List<List<int>>();

		// Token: 0x04005C10 RID: 23568
		public Dictionary<int, Dictionary<int, int>> SkillSeidFlag = new Dictionary<int, Dictionary<int, int>>();

		// Token: 0x04005C11 RID: 23569
		public Dictionary<int, Dictionary<int, int>> BuffSeidFlag = new Dictionary<int, Dictionary<int, int>>();

		// Token: 0x04005C12 RID: 23570
		public Dictionary<int, Dictionary<int, int>> StaticSkillSeidFlag = new Dictionary<int, Dictionary<int, int>>();

		// Token: 0x04005C13 RID: 23571
		public Dictionary<int, Dictionary<int, int>> EquipSeidFlag = new Dictionary<int, Dictionary<int, int>>();

		// Token: 0x04005C14 RID: 23572
		public Dictionary<int, Dictionary<int, int>> JieDanSkillSeidFlag = new Dictionary<int, Dictionary<int, int>>();

		// Token: 0x04005C15 RID: 23573
		public Dictionary<int, int> DrawWeight = new Dictionary<int, int>();

		// Token: 0x04005C16 RID: 23574
		public int NowMapIndex = 101;

		// Token: 0x04005C17 RID: 23575
		public int SkillRemoveCardNum;

		// Token: 0x04005C18 RID: 23576
		public int nowConfigEquipSkill;

		// Token: 0x04005C19 RID: 23577
		public int nowConfigEquipStaticSkill;

		// Token: 0x04005C1A RID: 23578
		public int nowConfigEquipItem;

		// Token: 0x04005C1B RID: 23579
		public List<SkillItem>[] configEquipSkill = new List<SkillItem>[]
		{
			new List<SkillItem>(),
			new List<SkillItem>(),
			new List<SkillItem>(),
			new List<SkillItem>(),
			new List<SkillItem>()
		};

		// Token: 0x04005C1C RID: 23580
		public List<SkillItem>[] configEquipStaticSkill = new List<SkillItem>[]
		{
			new List<SkillItem>(),
			new List<SkillItem>(),
			new List<SkillItem>(),
			new List<SkillItem>(),
			new List<SkillItem>()
		};

		// Token: 0x04005C1D RID: 23581
		public ITEM_INFO_LIST[] configEquipItem = new ITEM_INFO_LIST[]
		{
			new ITEM_INFO_LIST(),
			new ITEM_INFO_LIST(),
			new ITEM_INFO_LIST(),
			new ITEM_INFO_LIST(),
			new ITEM_INFO_LIST()
		};

		// Token: 0x04005C1E RID: 23582
		public List<SkillItem> equipSkillList = new List<SkillItem>();

		// Token: 0x04005C1F RID: 23583
		public List<SkillItem> equipStaticSkillList = new List<SkillItem>();

		// Token: 0x04005C20 RID: 23584
		public List<SkillItem> hasJieDanSkillList = new List<SkillItem>();

		// Token: 0x04005C21 RID: 23585
		public List<SkillItem> hasSkillList = new List<SkillItem>();

		// Token: 0x04005C22 RID: 23586
		public List<SkillItem> hasStaticSkillList = new List<SkillItem>();

		// Token: 0x04005C23 RID: 23587
		public Avatar OtherAvatar;

		// Token: 0x04005C24 RID: 23588
		public int showTupo;

		// Token: 0x04005C25 RID: 23589
		public int _xinjin;

		// Token: 0x04005C26 RID: 23590
		public string firstName = "";

		// Token: 0x04005C27 RID: 23591
		public string lastName = "";

		// Token: 0x04005C28 RID: 23592
		public int Sex = 1;

		// Token: 0x04005C29 RID: 23593
		public int nowPaiMaiCompereAvatarID;

		// Token: 0x04005C2A RID: 23594
		public int nowPaiMaiID;

		// Token: 0x04005C2B RID: 23595
		public int _WuDaoDian;

		// Token: 0x04005C2C RID: 23596
		public int _JieYingJinMai;

		// Token: 0x04005C2D RID: 23597
		public int _JieYingYiZHi;

		// Token: 0x04005C2E RID: 23598
		public AvatarStaticValue StaticValue = new AvatarStaticValue();

		// Token: 0x04005C2F RID: 23599
		public JSONObject AvatarGotChuanGong = new JSONObject();

		// Token: 0x04005C30 RID: 23600
		public JSONObject AvatarQieCuo = new JSONObject();

		// Token: 0x04005C31 RID: 23601
		public JSONObject SuiJiShiJian = new JSONObject(JSONObject.Type.OBJECT);

		// Token: 0x04005C32 RID: 23602
		public JSONObject ZuLin = new JSONObject(JSONObject.Type.OBJECT);

		// Token: 0x04005C33 RID: 23603
		public JSONObject FuBen = new JSONObject(JSONObject.Type.OBJECT);

		// Token: 0x04005C34 RID: 23604
		public JSONObject CanJiaPaiMai = new JSONObject(JSONObject.Type.OBJECT);

		// Token: 0x04005C35 RID: 23605
		public JSONObject NaiYaoXin = new JSONObject(JSONObject.Type.OBJECT);

		// Token: 0x04005C36 RID: 23606
		public JSONObject DanFang = new JSONObject(JSONObject.Type.ARRAY);

		// Token: 0x04005C37 RID: 23607
		public JSONObject YaoCaiShuXin = new JSONObject(JSONObject.Type.OBJECT);

		// Token: 0x04005C38 RID: 23608
		public JSONObject YaoCaiChanDi = new JSONObject(JSONObject.Type.ARRAY);

		// Token: 0x04005C39 RID: 23609
		public JSONObject YaoCaiIsGet = new JSONObject(JSONObject.Type.ARRAY);

		// Token: 0x04005C3A RID: 23610
		public JSONObject AllMapRandomNode = new JSONObject(JSONObject.Type.OBJECT);

		// Token: 0x04005C3B RID: 23611
		public JSONObject MenPaiHaoGanDu = new JSONObject(JSONObject.Type.OBJECT);

		// Token: 0x04005C3C RID: 23612
		public JSONObject NomelTaskJson = new JSONObject(JSONObject.Type.OBJECT);

		// Token: 0x04005C3D RID: 23613
		public JSONObject NomelTaskFlag = new JSONObject(JSONObject.Type.OBJECT);

		// Token: 0x04005C3E RID: 23614
		public JSONObject LingGuang = new JSONObject(JSONObject.Type.ARRAY);

		// Token: 0x04005C3F RID: 23615
		public JSONObject TianFuID = new JSONObject(JSONObject.Type.OBJECT);

		// Token: 0x04005C40 RID: 23616
		public JSONObject SelectTianFuID = new JSONObject(JSONObject.Type.ARRAY);

		// Token: 0x04005C41 RID: 23617
		public JSONObject openPanelList = new JSONObject();

		// Token: 0x04005C42 RID: 23618
		public JSONObject NoGetChuanYingList = new JSONObject();

		// Token: 0x04005C43 RID: 23619
		public JSONObject NewChuanYingList = new JSONObject();

		// Token: 0x04005C44 RID: 23620
		public JSONObject HasReadChuanYingList = new JSONObject();

		// Token: 0x04005C45 RID: 23621
		public JSONObject TieJianHongDianList = new JSONObject();

		// Token: 0x04005C46 RID: 23622
		public JSONObject ToalChuanYingFuList = new JSONObject();

		// Token: 0x04005C47 RID: 23623
		public JSONObject HasSendChuanYingFuList = new JSONObject();

		// Token: 0x04005C48 RID: 23624
		public JSONObject PaiMaiMaxMoneyAvatarDate = new JSONObject();

		// Token: 0x04005C49 RID: 23625
		public JSONObject WuDaoKillAvatar = new JSONObject(JSONObject.Type.ARRAY);

		// Token: 0x04005C4A RID: 23626
		public JSONObject HasLianZhiDanYao = new JSONObject(JSONObject.Type.ARRAY);

		// Token: 0x04005C4B RID: 23627
		public JSONObject AvatarChengJiuData = new JSONObject(JSONObject.Type.OBJECT);

		// Token: 0x04005C4C RID: 23628
		public JSONObject AvatarHasAchivement = new JSONObject(JSONObject.Type.ARRAY);

		// Token: 0x04005C4D RID: 23629
		public JSONObject ShangJinPingFen = new JSONObject(JSONObject.Type.OBJECT);

		// Token: 0x04005C4E RID: 23630
		public JSONObject ShiLiChengHaoLevel = new JSONObject(JSONObject.Type.OBJECT);

		// Token: 0x04005C4F RID: 23631
		public int NPCCreateIndex = 20000;

		// Token: 0x04005C50 RID: 23632
		public JSONObject AvatarFengLu = new JSONObject(JSONObject.Type.ARRAY);

		// Token: 0x04005C51 RID: 23633
		public JSONObject ZengLi = new JSONObject(JSONObject.Type.OBJECT);

		// Token: 0x04005C52 RID: 23634
		public JSONObject TeatherId = new JSONObject(JSONObject.Type.ARRAY);

		// Token: 0x04005C53 RID: 23635
		public JSONObject DaoLvId = new JSONObject(JSONObject.Type.ARRAY);

		// Token: 0x04005C54 RID: 23636
		public JSONObject Brother = new JSONObject(JSONObject.Type.ARRAY);

		// Token: 0x04005C55 RID: 23637
		public JSONObject TuDiId = new JSONObject(JSONObject.Type.ARRAY);

		// Token: 0x04005C56 RID: 23638
		public JSONObject DaTingId = new JSONObject(JSONObject.Type.ARRAY);

		// Token: 0x04005C57 RID: 23639
		public int IsShowXuanWo;

		// Token: 0x04005C58 RID: 23640
		public JSONObject PlayTutorialData = new JSONObject(JSONObject.Type.OBJECT);

		// Token: 0x04005C59 RID: 23641
		public JSONObject ShuangXiuData = new JSONObject(JSONObject.Type.OBJECT);

		// Token: 0x04005C5A RID: 23642
		public JSONObject DaoLvChengHu = new JSONObject(JSONObject.Type.OBJECT);

		// Token: 0x04005C5B RID: 23643
		public JSONObject DongFuData = new JSONObject(JSONObject.Type.OBJECT);

		// Token: 0x04005C5C RID: 23644
		public JSONObject NowDongFuID = new JSONObject(JSONObject.Type.NUMBER);

		// Token: 0x04005C5D RID: 23645
		public JSONObject GaoShi = new JSONObject(JSONObject.Type.OBJECT);

		// Token: 0x04005C5E RID: 23646
		public JSONObject SeaTanSuoDu = new JSONObject(JSONObject.Type.OBJECT);

		// Token: 0x04005C5F RID: 23647
		public JSONObject HuaShenStartXianXing = new JSONObject(0);

		// Token: 0x04005C60 RID: 23648
		public JSONObject TianJie = new JSONObject(JSONObject.Type.OBJECT);

		// Token: 0x04005C61 RID: 23649
		public JSONObject TianJieCanLingWuSkills = new JSONObject(JSONObject.Type.ARRAY);

		// Token: 0x04005C62 RID: 23650
		public JSONObject TianJieYiLingWuSkills = new JSONObject(JSONObject.Type.ARRAY);

		// Token: 0x04005C63 RID: 23651
		public JSONObject TianJieEquipedSkills = new JSONObject(JSONObject.Type.ARRAY);

		// Token: 0x04005C64 RID: 23652
		public JSONObject TianJieSkillRecordValue = new JSONObject(JSONObject.Type.OBJECT);

		// Token: 0x04005C65 RID: 23653
		public string TianJieBeforeShenYouSceneName = "";

		// Token: 0x04005C66 RID: 23654
		public JSONObject HuaShenWuDao = new JSONObject(0);

		// Token: 0x04005C67 RID: 23655
		public JSONObject HuaShenLingYuSkill = new JSONObject(0);

		// Token: 0x04005C68 RID: 23656
		public JSONObject HideHaiYuTanSuo = new JSONObject(JSONObject.Type.ARRAY);

		// Token: 0x04005C69 RID: 23657
		public JSONObject FightCostRecord = new JSONObject(JSONObject.Type.OBJECT);

		// Token: 0x04005C6A RID: 23658
		public JSONObject JianLingUnlockedXianSuo = new JSONObject(JSONObject.Type.OBJECT);

		// Token: 0x04005C6B RID: 23659
		public JSONObject JianLingUnlockedZhenXiang = new JSONObject(JSONObject.Type.OBJECT);

		// Token: 0x04005C6C RID: 23660
		public int JianLingExJiYiHuiFuDu;

		// Token: 0x04005C6D RID: 23661
		public JSONObject ShengPingRecord = new JSONObject(JSONObject.Type.OBJECT);

		// Token: 0x04005C6E RID: 23662
		public JSONObject OnceShow = new JSONObject(JSONObject.Type.ARRAY);

		// Token: 0x04005C6F RID: 23663
		public JSONObject Face = new JSONObject(0);

		// Token: 0x04005C70 RID: 23664
		public JSONObject RandomSeed = new JSONObject(0);

		// Token: 0x04005C71 RID: 23665
		public JSONObject LingHeCaiJi = new JSONObject(JSONObject.Type.OBJECT);

		// Token: 0x04005C72 RID: 23666
		public string NextCreateTime = "0010-1-1";

		// Token: 0x04005C73 RID: 23667
		public int LunDaoState = 3;

		// Token: 0x04005C74 RID: 23668
		public int LingGan = 20;

		// Token: 0x04005C75 RID: 23669
		public int WuDaoZhi;

		// Token: 0x04005C76 RID: 23670
		public int lastYear = 1;

		// Token: 0x04005C77 RID: 23671
		public int fakeTimes;

		// Token: 0x04005C78 RID: 23672
		public int deathType;

		// Token: 0x04005C79 RID: 23673
		public int WuDaoZhiLevel;

		// Token: 0x04005C7A RID: 23674
		public int BiGuanLingGuangTime;

		// Token: 0x04005C7B RID: 23675
		public JSONObject WuDaoJson = new JSONObject(JSONObject.Type.OBJECT);

		// Token: 0x04005C7C RID: 23676
		public JObject RandomFuBenList = new JObject();

		// Token: 0x04005C7D RID: 23677
		public JObject EndlessSea = new JObject();

		// Token: 0x04005C7E RID: 23678
		public JObject StaticNTaskTime = new JObject();

		// Token: 0x04005C7F RID: 23679
		public JObject EndlessSeaRandomNode = new JObject();

		// Token: 0x04005C80 RID: 23680
		public JObject EndlessSeaAvatarSeeIsland = new JObject();

		// Token: 0x04005C81 RID: 23681
		public JSONObject EndlessSeaBoss = new JSONObject(JSONObject.Type.OBJECT);

		// Token: 0x04005C82 RID: 23682
		public JObject ItemBuffList = new JObject();

		// Token: 0x04005C83 RID: 23683
		public JSONObject TaskZhuiZhong = new JSONObject();

		// Token: 0x04005C84 RID: 23684
		public int Dandu;

		// Token: 0x04005C85 RID: 23685
		private int _ZhuJiJinDu;
	}
}
