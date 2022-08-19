using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;
using Bag;
using Fungus;
using GUIPackage;
using JSONClass;
using Newtonsoft.Json.Linq;
using PingJing;
using script.MenPaiTask;
using TuPo;
using UnityEngine;
using UnityEngine.Events;
using YSGame;
using YSGame.Fight;
using YSGame.TuJian;

namespace KBEngine
{
	// Token: 0x02000C6A RID: 3178
	public class Avatar : AvatarBase
	{
		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x060056B5 RID: 22197 RVA: 0x0023F38D File Offset: 0x0023D58D
		public ElderTaskMag ElderTaskMag
		{
			get
			{
				return this.StreamData.ZhangLaoTaskMag;
			}
		}

		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x060056B6 RID: 22198 RVA: 0x0023F39A File Offset: 0x0023D59A
		public new CardMag crystal
		{
			get
			{
				return this.cardMag;
			}
		}

		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x060056B8 RID: 22200 RVA: 0x0023F3AC File Offset: 0x0023D5AC
		// (set) Token: 0x060056B7 RID: 22199 RVA: 0x0023F3A2 File Offset: 0x0023D5A2
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

		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x060056BA RID: 22202 RVA: 0x0023F430 File Offset: 0x0023D630
		// (set) Token: 0x060056B9 RID: 22201 RVA: 0x0023F424 File Offset: 0x0023D624
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

		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x060056BC RID: 22204 RVA: 0x0023F4B4 File Offset: 0x0023D6B4
		// (set) Token: 0x060056BB RID: 22203 RVA: 0x0023F4A8 File Offset: 0x0023D6A8
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

		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x060056BD RID: 22205 RVA: 0x0023F530 File Offset: 0x0023D730
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

		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x060056BF RID: 22207 RVA: 0x0023F5DD File Offset: 0x0023D7DD
		// (set) Token: 0x060056BE RID: 22206 RVA: 0x0023F5D4 File Offset: 0x0023D7D4
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

		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x060056C1 RID: 22209 RVA: 0x0023F5FD File Offset: 0x0023D7FD
		// (set) Token: 0x060056C0 RID: 22208 RVA: 0x0023F5E5 File Offset: 0x0023D7E5
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

		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x060056C3 RID: 22211 RVA: 0x0023F625 File Offset: 0x0023D825
		// (set) Token: 0x060056C2 RID: 22210 RVA: 0x0023F60D File Offset: 0x0023D80D
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

		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x060056C5 RID: 22213 RVA: 0x0023F655 File Offset: 0x0023D855
		// (set) Token: 0x060056C4 RID: 22212 RVA: 0x0023F635 File Offset: 0x0023D835
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

		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x060056C6 RID: 22214 RVA: 0x0023F65D File Offset: 0x0023D85D
		public int NowDrawCardNum
		{
			get
			{
				return (int)jsonData.instance.DrawCardToLevelJsonData[string.Concat((int)this.level)]["rundDraw"].n;
			}
		}

		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x060056C7 RID: 22215 RVA: 0x0023F68E File Offset: 0x0023D88E
		public int NowStartCardNum
		{
			get
			{
				return (int)jsonData.instance.DrawCardToLevelJsonData[string.Concat((int)this.level)]["StartCard"].n;
			}
		}

		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x060056C8 RID: 22216 RVA: 0x0023F6C0 File Offset: 0x0023D8C0
		public uint NowCard
		{
			get
			{
				if (this.BuffSeidFlag.ContainsKey(23))
				{
					return 0U;
				}
				return (uint)Mathf.Clamp((int)jsonData.instance.DrawCardToLevelJsonData[string.Concat((int)this.level)]["MaxDraw"].n + this.fightTemp.tempNowCard, 0, 99999);
			}
		}

		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x060056CA RID: 22218 RVA: 0x0023F72D File Offset: 0x0023D92D
		// (set) Token: 0x060056C9 RID: 22217 RVA: 0x0023F724 File Offset: 0x0023D924
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

		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x060056CC RID: 22220 RVA: 0x0023F743 File Offset: 0x0023D943
		// (set) Token: 0x060056CB RID: 22219 RVA: 0x0023F735 File Offset: 0x0023D935
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

		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x060056CE RID: 22222 RVA: 0x0023F75E File Offset: 0x0023D95E
		// (set) Token: 0x060056CD RID: 22221 RVA: 0x0023F750 File Offset: 0x0023D950
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

		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x060056CF RID: 22223 RVA: 0x0023F76B File Offset: 0x0023D96B
		public int useSkillNum
		{
			get
			{
				return this.fightTemp.NowRoundUsedSkills.Count;
			}
		}

		// Token: 0x060056D0 RID: 22224 RVA: 0x0023F77D File Offset: 0x0023D97D
		public int getStaticSkillAddSum(int seid)
		{
			return 0 + this.DictionyGetSum(this.StaticSkillSeidFlag, seid) + this.DictionyGetSum(this.wuDaoMag.WuDaoSkillSeidFlag, seid) + this.DictionyGetSum(this.JieDanSkillSeidFlag, seid);
		}

		// Token: 0x060056D1 RID: 22225 RVA: 0x0023F7B0 File Offset: 0x0023D9B0
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

		// Token: 0x060056D2 RID: 22226 RVA: 0x0023F820 File Offset: 0x0023DA20
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

		// Token: 0x060056D3 RID: 22227 RVA: 0x0023F8C8 File Offset: 0x0023DAC8
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

		// Token: 0x060056D4 RID: 22228 RVA: 0x0023F988 File Offset: 0x0023DB88
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

		// Token: 0x060056D5 RID: 22229 RVA: 0x0023FA70 File Offset: 0x0023DC70
		public int GetBaseShenShi()
		{
			return this._shengShi + this.getStaticSkillAddSum(2) + this.getEquipAddSum(4);
		}

		// Token: 0x060056D6 RID: 22230 RVA: 0x0023FA88 File Offset: 0x0023DC88
		public int GetBaseDunSu()
		{
			return this._dunSu + this.getStaticSkillAddSum(8) + this.getEquipAddSum(8);
		}

		// Token: 0x060056D7 RID: 22231 RVA: 0x0023FAA0 File Offset: 0x0023DCA0
		public Avatar cloneAvatar()
		{
			return base.MemberwiseClone() as Avatar;
		}

		// Token: 0x060056D8 RID: 22232 RVA: 0x0023FAB0 File Offset: 0x0023DCB0
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

		// Token: 0x060056D9 RID: 22233 RVA: 0x0023FB18 File Offset: 0x0023DD18
		public int RandomSeedNext()
		{
			int num = new Random(PlayerEx.Player.RandomSeed.I).Next();
			this.RandomSeed = new JSONObject(num);
			return num;
		}

		// Token: 0x060056DA RID: 22234 RVA: 0x0023FB4C File Offset: 0x0023DD4C
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

		// Token: 0x060056DB RID: 22235 RVA: 0x0023FBA4 File Offset: 0x0023DDA4
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

		// Token: 0x060056DC RID: 22236 RVA: 0x0023FBE2 File Offset: 0x0023DDE2
		public void ReduceLingGan(int num)
		{
			this.LingGan -= num;
			if (this.LingGan < 0)
			{
				this.LingGan = 0;
			}
			this.LunDaoState = this.GetLunDaoState();
		}

		// Token: 0x060056DD RID: 22237 RVA: 0x0023FC10 File Offset: 0x0023DE10
		public int GetLingGanMax()
		{
			return jsonData.instance.LingGanMaxData[this.GetXinJingLevel().ToString()]["lingGanShangXian"].I;
		}

		// Token: 0x060056DE RID: 22238 RVA: 0x0023FC4C File Offset: 0x0023DE4C
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

		// Token: 0x060056DF RID: 22239 RVA: 0x0023FCD0 File Offset: 0x0023DED0
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
			return this.getDanFang(jsonobject["ItemID"].I, danyao, num) != null;
		}

		// Token: 0x060056E0 RID: 22240 RVA: 0x0023FD58 File Offset: 0x0023DF58
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

		// Token: 0x060056E1 RID: 22241 RVA: 0x0023FE5C File Offset: 0x0023E05C
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

		// Token: 0x060056E2 RID: 22242 RVA: 0x0023FF1A File Offset: 0x0023E11A
		public void setAvatarHaoGandu(int AvatarID, int AddHaoGanduNum)
		{
			NPCEx.AddFavor(AvatarID, AddHaoGanduNum, true, true);
		}

		// Token: 0x060056E3 RID: 22243 RVA: 0x0023FF28 File Offset: 0x0023E128
		public void getDanYaoTypeAndNum(int id, List<int> danyao, List<int> num)
		{
			JSONObject jsonobject = jsonData.instance.LianDanDanFangBiao[id.ToString()];
			for (int i = 1; i <= 5; i++)
			{
				danyao.Add((int)jsonobject["value" + i].n);
				num.Add((int)jsonobject["num" + i].n);
			}
		}

		// Token: 0x060056E4 RID: 22244 RVA: 0x0023FF9C File Offset: 0x0023E19C
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

		// Token: 0x060056E5 RID: 22245 RVA: 0x0023FFEC File Offset: 0x0023E1EC
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

		// Token: 0x060056E6 RID: 22246 RVA: 0x0024004C File Offset: 0x0023E24C
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

		// Token: 0x060056E7 RID: 22247 RVA: 0x00240100 File Offset: 0x0023E300
		public void AddDandu(int num)
		{
			this.Dandu += (this.TianFuID.HasField(string.Concat(18)) ? (num * 2) : num);
			if (this.Dandu >= 120)
			{
				UIDeath.Inst.Show(DeathType.毒发身亡);
			}
		}

		// Token: 0x060056E8 RID: 22248 RVA: 0x00240150 File Offset: 0x0023E350
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

		// Token: 0x060056E9 RID: 22249 RVA: 0x002401D3 File Offset: 0x0023E3D3
		public bool hasYaocaiShuXin(int itemID, int index)
		{
			return this.YaoCaiShuXin.HasField(itemID + "_" + index);
		}

		// Token: 0x060056EA RID: 22250 RVA: 0x002401F8 File Offset: 0x0023E3F8
		public bool getItemHasTianFu15(int quality)
		{
			return this.TianFuID.HasField(string.Concat(15)) && this.TianFuID["15"].list.Find((JSONObject aa) => (int)aa.n == quality) != null;
		}

		// Token: 0x060056EB RID: 22251 RVA: 0x00240258 File Offset: 0x0023E458
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

		// Token: 0x060056EC RID: 22252 RVA: 0x002402C4 File Offset: 0x0023E4C4
		public bool GetHasYaoYinShuXin(int itemID, int quality)
		{
			bool itemHasTianFu = this.getItemHasTianFu15(quality);
			return this.hasYaocaiShuXin(itemID, 1) || itemHasTianFu;
		}

		// Token: 0x060056ED RID: 22253 RVA: 0x002402E4 File Offset: 0x0023E4E4
		public bool GetHasZhuYaoShuXin(int itemID, int quality)
		{
			bool itemHasTianFu = this.getItemHasTianFu15(quality);
			return this.hasYaocaiShuXin(itemID, 2) || itemHasTianFu;
		}

		// Token: 0x060056EE RID: 22254 RVA: 0x00240304 File Offset: 0x0023E504
		public bool GetHasFuYaoShuXin(int itemID, int quality)
		{
			bool itemHasTianFu = this.getItemHasTianFu15(quality);
			return this.hasYaocaiShuXin(itemID, 3) || itemHasTianFu;
		}

		// Token: 0x060056EF RID: 22255 RVA: 0x00240323 File Offset: 0x0023E523
		public void UnLockCaoYaoData(int caoYaoId)
		{
			this.AddYaoCaiShuXin(caoYaoId, 1);
			this.AddYaoCaiShuXin(caoYaoId, 2);
			this.AddYaoCaiShuXin(caoYaoId, 3);
		}

		// Token: 0x060056F0 RID: 22256 RVA: 0x00240340 File Offset: 0x0023E540
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

		// Token: 0x060056F1 RID: 22257 RVA: 0x002404B8 File Offset: 0x0023E6B8
		public void statiReduceDandu(int num)
		{
			this.Dandu -= num;
			if (this.Dandu < 0)
			{
				this.Dandu = 0;
			}
		}

		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x060056F2 RID: 22258 RVA: 0x002404D8 File Offset: 0x0023E6D8
		public List<int> HasDefeatNpcList
		{
			get
			{
				return this.StreamData.HasDefeatNpcList;
			}
		}

		// Token: 0x060056F3 RID: 22259 RVA: 0x002404E8 File Offset: 0x0023E6E8
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

		// Token: 0x060056F4 RID: 22260 RVA: 0x00240570 File Offset: 0x0023E770
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

		// Token: 0x060056F5 RID: 22261 RVA: 0x002405D3 File Offset: 0x0023E7D3
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

		// Token: 0x060056F6 RID: 22262 RVA: 0x00240614 File Offset: 0x0023E814
		public int GetXinJingLevel()
		{
			foreach (JSONObject jsonobject in jsonData.instance.XinJinJsonData.list)
			{
				if (jsonobject["Max"].I > this.xinjin)
				{
					return jsonobject["id"].I;
				}
			}
			return jsonData.instance.XinJinJsonData.Count;
		}

		// Token: 0x060056F7 RID: 22263 RVA: 0x002406A8 File Offset: 0x0023E8A8
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

		// Token: 0x060056F8 RID: 22264 RVA: 0x002406D4 File Offset: 0x0023E8D4
		public int getLevelType()
		{
			return (int)((this.level - 1) / 3 + 1);
		}

		// Token: 0x060056F9 RID: 22265 RVA: 0x002406E2 File Offset: 0x0023E8E2
		public void setSkillConfigIndex(int index)
		{
			this.nowConfigEquipSkill = index;
			this.equipSkillList = this.configEquipSkill[index];
		}

		// Token: 0x060056FA RID: 22266 RVA: 0x002406F9 File Offset: 0x0023E8F9
		public void setStatikConfigIndex(int index)
		{
			this.nowConfigEquipStaticSkill = index;
			this.equipStaticSkillList = this.configEquipStaticSkill[index];
		}

		// Token: 0x060056FB RID: 22267 RVA: 0x00240710 File Offset: 0x0023E910
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

		// Token: 0x060056FC RID: 22268 RVA: 0x002407E8 File Offset: 0x0023E9E8
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

		// Token: 0x060056FD RID: 22269 RVA: 0x0024086C File Offset: 0x0023EA6C
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

		// Token: 0x060056FE RID: 22270 RVA: 0x002408F4 File Offset: 0x0023EAF4
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

		// Token: 0x060056FF RID: 22271 RVA: 0x00240964 File Offset: 0x0023EB64
		public void MonstarEndRound()
		{
			RoundManager.instance.autoRemoveCard(this);
			Event.fireOut("endRound", new object[]
			{
				this
			});
		}

		// Token: 0x06005700 RID: 22272 RVA: 0x00240985 File Offset: 0x0023EB85
		public void AvatarEndRound()
		{
			if (base.isPlayer())
			{
				RoundManager.instance.PlayerEndRound(true);
				return;
			}
			this.MonstarEndRound();
		}

		// Token: 0x06005701 RID: 22273 RVA: 0x002409A1 File Offset: 0x0023EBA1
		public void joinMenPai(int menPaiID)
		{
			this.menPai = (ushort)menPaiID;
		}

		// Token: 0x06005702 RID: 22274 RVA: 0x002409AB File Offset: 0x0023EBAB
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

		// Token: 0x06005703 RID: 22275 RVA: 0x002409E9 File Offset: 0x0023EBE9
		public void AllMapAddHPMax(int num)
		{
			this._HP_Max += num;
		}

		// Token: 0x06005704 RID: 22276 RVA: 0x002409F9 File Offset: 0x0023EBF9
		public void addShenShi(int num)
		{
			this.shengShi = (int)this.ADDIntToUint((uint)this._shengShi, num);
		}

		// Token: 0x06005705 RID: 22277 RVA: 0x00240A0E File Offset: 0x0023EC0E
		public void addShoYuan(int num)
		{
			this.shouYuan = this.ADDIntToUint(this.shouYuan, num);
		}

		// Token: 0x06005706 RID: 22278 RVA: 0x00240A23 File Offset: 0x0023EC23
		public void addShaQi(int num)
		{
			this.shaQi = this.ADDIntToUint(this.shaQi, num);
		}

		// Token: 0x06005707 RID: 22279 RVA: 0x00240A38 File Offset: 0x0023EC38
		public void addZiZhi(int num)
		{
			this.ZiZhi = (int)this.ADDIntToUint((uint)this.ZiZhi, num);
		}

		// Token: 0x06005708 RID: 22280 RVA: 0x00240A4D File Offset: 0x0023EC4D
		public void addWuXin(int num)
		{
			this.wuXin = this.ADDIntToUint(this.wuXin, num);
		}

		// Token: 0x06005709 RID: 22281 RVA: 0x00240A64 File Offset: 0x0023EC64
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

		// Token: 0x0600570A RID: 22282 RVA: 0x00240A84 File Offset: 0x0023EC84
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

		// Token: 0x0600570B RID: 22283 RVA: 0x00240DF8 File Offset: 0x0023EFF8
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

		// Token: 0x0600570C RID: 22284 RVA: 0x00241104 File Offset: 0x0023F304
		public float GetShenShiArea()
		{
			return Mathf.Pow((float)this.shengShi, 0.2f) / 2f + 0.1f * (float)this.shengShi;
		}

		// Token: 0x0600570D RID: 22285 RVA: 0x0024112C File Offset: 0x0023F32C
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
				for (int i = 0; i < this.ToalChuanYingFuList.Count; i++)
				{
					int i2 = this.ToalChuanYingFuList[i]["AvatarID"].I;
					if (this.ToalChuanYingFuList[i]["IsAlive"].I != 1 || NPCEx.NPCIDToNew(i2) >= 20000)
					{
						if (this.ToalChuanYingFuList[i]["NPCLevel"].Count > 0)
						{
							int i3 = this.ToalChuanYingFuList[i]["NPCLevel"][0].I;
							int i4 = this.ToalChuanYingFuList[i]["NPCLevel"][1].I;
							int num2 = NPCEx.NPCIDToNew(i2);
							if (num2 < 20000)
							{
								goto IL_4B1;
							}
							int i5 = jsonData.instance.AvatarJsonData[num2.ToString()]["Level"].I;
							if (i5 < i3 || i5 > i4)
							{
								goto IL_4B1;
							}
						}
						if (this.ToalChuanYingFuList[i]["StarTime"].str != "" && this.ToalChuanYingFuList[i]["StarTime"].str != null)
						{
							string str = this.ToalChuanYingFuList[i]["StarTime"].str;
							if (this.ToalChuanYingFuList[i]["EndTime"].str != "" && this.ToalChuanYingFuList[i]["EndTime"].str != null)
							{
								string str2 = this.ToalChuanYingFuList[i]["EndTime"].str;
								if (!Tools.instance.IsInTime(nowTime, str, str2))
								{
									goto IL_4B1;
								}
							}
							else if (DateTime.Parse(nowTime) < DateTime.Parse(str))
							{
								goto IL_4B1;
							}
						}
						if (this.ToalChuanYingFuList[i]["Level"].Count <= 0 || (level >= this.ToalChuanYingFuList[i]["Level"][0].I && level <= this.ToalChuanYingFuList[i]["Level"][1].I))
						{
							if (this.ToalChuanYingFuList[i]["HaoGanDu"].I > 0)
							{
								int num3 = NPCEx.NPCIDToNew(this.ToalChuanYingFuList[i]["AvatarID"].I);
								if (jsonData.instance.AvatarRandomJsonData[num3.ToString()]["HaoGanDu"].I <= this.ToalChuanYingFuList[i]["HaoGanDu"].I)
								{
									goto IL_4B1;
								}
							}
							if (this.ToalChuanYingFuList[i]["EventValue"].Count > 0)
							{
								string str3 = this.ToalChuanYingFuList[i]["fuhao"].str;
								int i6 = this.ToalChuanYingFuList[i]["EventValue"][0].I;
								int i7 = this.ToalChuanYingFuList[i]["EventValue"][1].I;
								int num4 = GlobalValue.Get(i6, "Avatar.updateChuanYingFu");
								if (str3 == "=")
								{
									if (num4 != i7)
									{
										goto IL_4B1;
									}
								}
								else if (str3 == ">")
								{
									if (num4 <= i7)
									{
										goto IL_4B1;
									}
								}
								else if (num4 >= i7)
								{
									goto IL_4B1;
								}
							}
							if (this.ToalChuanYingFuList[i]["IsOnly"].I == 1)
							{
								list.Add(this.ToalChuanYingFuList[i]["id"].I.ToString());
							}
							else if (this.ToalChuanYingFuList[i]["IsOnly"].I == 2 && this.nomelTaskMag.IsNTaskStart(this.ToalChuanYingFuList[i]["WeiTuo"].I))
							{
								goto IL_4B1;
							}
							this.chuanYingManager.addChuanYingFu(this.ToalChuanYingFuList[i]["id"].I);
						}
					}
					IL_4B1:;
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
						int i8 = this.NoGetChuanYingList[k]["id"].I;
						list.Add(this.NoGetChuanYingList[k]["id"].I.ToString());
						this.NewChuanYingList.SetField(this.NoGetChuanYingList[k]["id"].I.ToString(), this.NoGetChuanYingList[k]);
						this.emailDateMag.OldToPlayer(this.NewChuanYingList[i8.ToString()]["AvatarID"].I, i8, str4);
						if (this.NoGetChuanYingList.HasField("IsAdd") && this.NoGetChuanYingList[k]["IsAdd"].I == 1)
						{
							int i9 = this.NoGetChuanYingList[k]["WeiTuo"].I;
							if (!this.nomelTaskMag.IsNTaskStart(i9))
							{
								this.nomelTaskMag.StartNTask(i9, 0);
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

		// Token: 0x0600570E RID: 22286 RVA: 0x00241888 File Offset: 0x0023FA88
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

		// Token: 0x0600570F RID: 22287 RVA: 0x002419DC File Offset: 0x0023FBDC
		public GameObject createCanvasDeath()
		{
			return Object.Instantiate<GameObject>(Resources.Load("uiPrefab/CanvasDeath") as GameObject);
		}

		// Token: 0x06005710 RID: 22288 RVA: 0x002419F2 File Offset: 0x0023FBF2
		public float AddZiZhiSpeed(float speed)
		{
			return speed * ((float)this.ZiZhi / 100f);
		}

		// Token: 0x06005711 RID: 22289 RVA: 0x00241A04 File Offset: 0x0023FC04
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

		// Token: 0x06005712 RID: 22290 RVA: 0x00241A98 File Offset: 0x0023FC98
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

		// Token: 0x06005713 RID: 22291 RVA: 0x00241B04 File Offset: 0x0023FD04
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

		// Token: 0x06005714 RID: 22292 RVA: 0x00241B70 File Offset: 0x0023FD70
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

		// Token: 0x06005715 RID: 22293 RVA: 0x00241D88 File Offset: 0x0023FF88
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

		// Token: 0x06005716 RID: 22294 RVA: 0x00241DC1 File Offset: 0x0023FFC1
		public int GetLeveUpAddHPMax(int addHpNum)
		{
			if (!this.TianFuID.HasField(string.Concat(13)))
			{
				return addHpNum;
			}
			return addHpNum + (int)((float)addHpNum * (this.TianFuID["13"].n / 100f));
		}

		// Token: 0x06005717 RID: 22295 RVA: 0x00241E00 File Offset: 0x00240000
		public int GetTianFuAddCaoYaoCaiJi(int num)
		{
			int num2 = num;
			if (this.TianFuID.HasField(string.Concat(21)))
			{
				num2 = num + (int)((float)num * (this.TianFuID["21"].n / 100f));
			}
			return num2 + (int)((float)num2 * ((float)this.getStaticSkillAddSum(10) / 100f));
		}

		// Token: 0x06005718 RID: 22296 RVA: 0x00241E64 File Offset: 0x00240064
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

		// Token: 0x06005719 RID: 22297 RVA: 0x00242130 File Offset: 0x00240330
		public void AllMapSetNode()
		{
			foreach (JSONObject node in jsonData.instance.AllMapLuDainType.list)
			{
				this.resetNode(node);
			}
		}

		// Token: 0x0600571A RID: 22298 RVA: 0x0024218C File Offset: 0x0024038C
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

		// Token: 0x0600571B RID: 22299 RVA: 0x00242238 File Offset: 0x00240438
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

		// Token: 0x0600571C RID: 22300 RVA: 0x002422BC File Offset: 0x002404BC
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

		// Token: 0x0600571D RID: 22301 RVA: 0x00242480 File Offset: 0x00240680
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

		// Token: 0x0600571E RID: 22302 RVA: 0x002425B0 File Offset: 0x002407B0
		public JObject CreateSeaMonstar(int seaId, int monstarID, int index)
		{
			JObject jobject = new JObject();
			jobject["uuid"] = Tools.getUUID();
			jobject["monstarId"] = monstarID;
			jobject["index"] = index;
			jobject["StartTime"] = this.worldTimeMag.nowTime;
			return jobject;
		}

		// Token: 0x0600571F RID: 22303 RVA: 0x00242614 File Offset: 0x00240814
		public void resetNode(JSONObject node)
		{
			if (!this.AllMapRandomNode.HasField(string.Concat(node["id"].I)))
			{
				JSONObject jsonobject = new JSONObject(JSONObject.Type.OBJECT);
				jsonobject.AddField("resetTime", "0001-01-01");
				jsonobject.AddField("Type", -1);
				jsonobject.AddField("EventId", 0);
				jsonobject.AddField("Reset", true);
				this.AllMapRandomNode.AddField(string.Concat(node["id"].I), jsonobject);
			}
			Avatar avatar = Tools.instance.getPlayer();
			JSONObject jsonobject2 = this.AllMapRandomNode[string.Concat(node["id"].I)];
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
							if (_tempMapNode["Type"].I >= 0 && (int)jsonData.instance.AllMapReset[(int)_tempMapNode["Type"].n]["max"].n > 0 && dictionary[(int)_tempMapNode["Type"].n] >= (int)jsonData.instance.AllMapReset[(int)_tempMapNode["Type"].n]["max"].n)
							{
								JSONObject jsonobject3 = _tempJsond.Find((JSONObject _acs) => _acs["id"].I == _tempMapNode["Type"].I);
								if (jsonobject3 != null)
								{
									jsonobject3.SetField("percent", 0);
								}
							}
							Transform transform = AllMapManage.instance.AllNodeGameobjGroup.transform.Find(string.Concat(node["id"].I));
							if (transform == null)
							{
								Debug.LogError(("路点出错" + node["id"].I) ?? "");
							}
							using (List<int>.Enumerator enumerator2 = transform.GetComponent<MapComponent>().nextIndex.GetEnumerator())
							{
								while (enumerator2.MoveNext())
								{
									int _nnnn = enumerator2.Current;
									if (this.AllMapRandomNode.HasField(string.Concat(_nnnn)) && (int)jsonData.instance.AllMapReset[string.Concat((int)this.AllMapRandomNode[string.Concat(_nnnn)]["Type"].n)]["CanSame"].n == 0)
									{
										JSONObject jsonobject4 = _tempJsond.Find((JSONObject _acs) => _acs["id"].I == this.AllMapRandomNode[string.Concat(_nnnn)]["Type"].I);
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
					jsonobject2.SetField("Type", json["id"].I);
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
						if (avatar.SuiJiShiJian.HasField(Tools.getScreenName()) && avatar.SuiJiShiJian[Tools.getScreenName()].list.Find((JSONObject _aa) => _aa.I == aa["id"].I) != null)
						{
							return false;
						}
						foreach (JSONObject jsonobject5 in this.AllMapRandomNode.list)
						{
							int i = jsonobject5["EventId"].I;
							if (jsonData.instance.MapRandomJsonData.HasField(i.ToString()))
							{
								JSONObject jsonobject6 = jsonData.instance.MapRandomJsonData[i.ToString()];
								if (aa["id"].I == jsonobject6["id"].I && jsonobject6["once"].I == 1)
								{
									return false;
								}
							}
						}
						if (aa["EventType"].I == json["id"].I)
						{
							return aa["EventLv"].list.Find((JSONObject _aa) => (int)_aa.n == (int)Tools.instance.getPlayer().level) != null;
						}
						return false;
					});
					if (list2.Count > 0)
					{
						JSONObject randomListByPercent = Tools.instance.getRandomListByPercent(list2, "percent");
						jsonobject2.SetField("Type", (int)randomListByPercent["EventType"].n);
						jsonobject2.SetField("EventId", randomListByPercent["id"].I);
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

		// Token: 0x06005720 RID: 22304 RVA: 0x00242CCC File Offset: 0x00240ECC
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

		// Token: 0x06005721 RID: 22305 RVA: 0x00242D30 File Offset: 0x00240F30
		public void WorldsetRandomFace()
		{
			PlayerSetRandomFace component = ((GameObject)this.renderObj).transform.GetChild(0).GetChild(0).GetComponent<PlayerSetRandomFace>();
			if (this.fightTemp.MonstarID > 0 && component != null)
			{
				component.randomAvatar(this.fightTemp.MonstarID);
			}
		}

		// Token: 0x06005722 RID: 22306 RVA: 0x00242D87 File Offset: 0x00240F87
		public void discardCard(Card card)
		{
			Event.fireOut("discardCard", new object[]
			{
				this,
				card
			});
		}

		// Token: 0x06005723 RID: 22307 RVA: 0x00242DA4 File Offset: 0x00240FA4
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

		// Token: 0x06005724 RID: 22308 RVA: 0x00242E20 File Offset: 0x00241020
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

		// Token: 0x06005725 RID: 22309 RVA: 0x00242EA4 File Offset: 0x002410A4
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

		// Token: 0x06005726 RID: 22310 RVA: 0x00243094 File Offset: 0x00241294
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

		// Token: 0x06005727 RID: 22311 RVA: 0x002431B4 File Offset: 0x002413B4
		public void addWuDaoSeid()
		{
			foreach (SkillItem skillItem in this.wuDaoMag.GetAllWuDaoSkills())
			{
				new WuDaoStaticSkill(skillItem.itemId, 0, 5).Puting(this, this, 1);
			}
		}

		// Token: 0x06005728 RID: 22312 RVA: 0x00243218 File Offset: 0x00241418
		public void addJieDanSeid()
		{
			foreach (SkillItem skillItem in this.hasJieDanSkillList)
			{
				new JieDanSkill(skillItem.itemId, 0, 5).Puting(this, this, 1);
			}
		}

		// Token: 0x06005729 RID: 22313 RVA: 0x00243278 File Offset: 0x00241478
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

		// Token: 0x0600572A RID: 22314 RVA: 0x0024396C File Offset: 0x00241B6C
		public void onCrystalChanged(CardMag oldValue)
		{
			Event.fireOut("crtstalChanged", new object[]
			{
				this,
				oldValue
			});
			MessageMag.Instance.Send("Fight_CardChange", null);
		}

		// Token: 0x0600572B RID: 22315 RVA: 0x00243998 File Offset: 0x00241B98
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

		// Token: 0x0600572C RID: 22316 RVA: 0x00243A04 File Offset: 0x00241C04
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

		// Token: 0x0600572D RID: 22317 RVA: 0x00243A94 File Offset: 0x00241C94
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

		// Token: 0x0600572E RID: 22318 RVA: 0x00243B48 File Offset: 0x00241D48
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

		// Token: 0x0600572F RID: 22319 RVA: 0x00243C68 File Offset: 0x00241E68
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
				if (base.isPlayer())
				{
					this.die();
					return;
				}
				if (RoundManager.instance != null)
				{
					World.GameOver();
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

		// Token: 0x06005730 RID: 22320 RVA: 0x00243E00 File Offset: 0x00242000
		public void SetChengHaoId(int id)
		{
			this.chengHao = id;
			if (id >= 7 && id <= 10 && !this.StreamData.MenPaiTaskMag.IsInit)
			{
				this.StreamData.MenPaiTaskMag.NextTime = NpcJieSuanManager.inst.GetNowTime();
				this.StreamData.MenPaiTaskMag.IsInit = true;
			}
		}

		// Token: 0x06005731 RID: 22321 RVA: 0x00243E5C File Offset: 0x0024205C
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
				if (Tools.instance.monstarMag.FightType == StartFight.FightEnumType.FeiSheng)
				{
					TianJieManager.Inst.DuJieSuccess(true);
				}
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

		// Token: 0x06005732 RID: 22322 RVA: 0x002440D8 File Offset: 0x002422D8
		public override void onDestroy()
		{
			if (base.isPlayer())
			{
				Event.deregisterIn(this);
			}
		}

		// Token: 0x06005733 RID: 22323 RVA: 0x002440E9 File Offset: 0x002422E9
		public void gameFinsh()
		{
			base.cellCall("gameFinsh", Array.Empty<object>());
		}

		// Token: 0x06005734 RID: 22324 RVA: 0x002440FB File Offset: 0x002422FB
		public virtual void updatePlayer(float x, float y, float z, float yaw)
		{
			this.position.x = x;
			this.position.y = y;
			this.position.z = z;
			this.direction.z = yaw;
		}

		// Token: 0x06005735 RID: 22325 RVA: 0x00244130 File Offset: 0x00242330
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

		// Token: 0x06005736 RID: 22326 RVA: 0x0024418C File Offset: 0x0024238C
		public void sendChatMessage(string msg)
		{
			object name = this.name;
			base.baseCall("sendChatMessage", new object[]
			{
				(string)name + ": " + msg
			});
		}

		// Token: 0x06005737 RID: 22327 RVA: 0x002441C5 File Offset: 0x002423C5
		public override void ReceiveChatMessage(string msg)
		{
			Event.fireOut("ReceiveChatMessage", new object[]
			{
				msg
			});
		}

		// Token: 0x06005738 RID: 22328 RVA: 0x002441DB File Offset: 0x002423DB
		public void relive(byte type)
		{
			base.cellCall("relive", new object[]
			{
				type
			});
		}

		// Token: 0x06005739 RID: 22329 RVA: 0x002441F8 File Offset: 0x002423F8
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

		// Token: 0x0600573A RID: 22330 RVA: 0x00244234 File Offset: 0x00242434
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

		// Token: 0x0600573B RID: 22331 RVA: 0x002442E8 File Offset: 0x002424E8
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

		// Token: 0x0600573C RID: 22332 RVA: 0x0024431B File Offset: 0x0024251B
		public override void recvSkill(int attacker, int skillID)
		{
			Event.fireOut("recvSkill", new object[]
			{
				attacker,
				skillID
			});
		}

		// Token: 0x0600573D RID: 22333 RVA: 0x00244340 File Offset: 0x00242540
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

		// Token: 0x0600573E RID: 22334 RVA: 0x002444BB File Offset: 0x002426BB
		public override void clearSkills()
		{
			SkillBox.inst.clear();
			base.cellCall("requestPull", Array.Empty<object>());
		}

		// Token: 0x0600573F RID: 22335 RVA: 0x002444D7 File Offset: 0x002426D7
		public void createBuild(ulong BuildId, Vector3 positon, Vector3 direction)
		{
			base.baseCall("createBuild", new object[]
			{
				BuildId,
				positon,
				direction
			});
		}

		// Token: 0x06005740 RID: 22336 RVA: 0x00244508 File Offset: 0x00242708
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

		// Token: 0x06005741 RID: 22337 RVA: 0x00244568 File Offset: 0x00242768
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

		// Token: 0x06005742 RID: 22338 RVA: 0x002445B8 File Offset: 0x002427B8
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

		// Token: 0x06005743 RID: 22339 RVA: 0x000656B8 File Offset: 0x000638B8
		public void continuFunc()
		{
			YSFuncList.Ints.Continue();
		}

		// Token: 0x06005744 RID: 22340 RVA: 0x0024496C File Offset: 0x00242B6C
		public card addCrystal(int CrystalType, int num = 1)
		{
			List<int> list = new List<int>();
			list.Add(num);
			list.Add(CrystalType);
			this.spell.onBuffTickByType(25, list);
			return this.crystal.addCard(CrystalType, num);
		}

		// Token: 0x06005745 RID: 22341 RVA: 0x002449A8 File Offset: 0x00242BA8
		public void removeCrystal(int CrystalType, int num = 1)
		{
			this.crystal.removeCard(CrystalType, num);
		}

		// Token: 0x06005746 RID: 22342 RVA: 0x002449B7 File Offset: 0x00242BB7
		public void removeCrystal(card CrystalType)
		{
			this.crystal.removeCard(CrystalType);
		}

		// Token: 0x06005747 RID: 22343 RVA: 0x002449C8 File Offset: 0x00242BC8
		public void UseCryStal(int CrystalType, int num = 1)
		{
			for (int i = 0; i < num; i++)
			{
				this.NowRoundUsedCard.Add(CrystalType);
			}
			List<int> list = new List<int>();
			list.Add(num);
			list.Add(CrystalType);
			this.spell.onBuffTickByType(27, list);
			this.removeCrystal(CrystalType, num);
		}

		// Token: 0x06005748 RID: 22344 RVA: 0x00244A18 File Offset: 0x00242C18
		public void AbandonCryStal(int CrystalType, int num = 1)
		{
			List<int> list = new List<int>();
			list.Add(num);
			list.Add(CrystalType);
			this.removeCrystal(CrystalType, num);
			this.spell.onBuffTickByType(26, list);
		}

		// Token: 0x06005749 RID: 22345 RVA: 0x00244A50 File Offset: 0x00242C50
		public void AbandonCryStal(card CrystalType, int num = 1)
		{
			List<int> list = new List<int>();
			list.Add(num);
			list.Add(CrystalType.cardType);
			this.removeCrystal(CrystalType);
			this.spell.onBuffTickByType(26, list);
		}

		// Token: 0x0600574A RID: 22346 RVA: 0x00244A8C File Offset: 0x00242C8C
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

		// Token: 0x0600574B RID: 22347 RVA: 0x00244AEC File Offset: 0x00242CEC
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

		// Token: 0x0600574C RID: 22348 RVA: 0x00244BA0 File Offset: 0x00242DA0
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

		// Token: 0x0600574D RID: 22349 RVA: 0x00244C14 File Offset: 0x00242E14
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

		// Token: 0x0600574E RID: 22350 RVA: 0x00244CB8 File Offset: 0x00242EB8
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

		// Token: 0x0600574F RID: 22351 RVA: 0x00244D64 File Offset: 0x00242F64
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

		// Token: 0x06005750 RID: 22352 RVA: 0x00244EA8 File Offset: 0x002430A8
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

		// Token: 0x06005751 RID: 22353 RVA: 0x00244F28 File Offset: 0x00243128
		public JToken GetNowLingZhouShuXinJson()
		{
			_ItemJsonData equipLingZhouData = this.GetEquipLingZhouData();
			if (equipLingZhouData != null)
			{
				return jsonData.instance.LingZhouPinJie[equipLingZhouData.quality.ToString()];
			}
			return null;
		}

		// Token: 0x06005752 RID: 22354 RVA: 0x00244F5C File Offset: 0x0024315C
		public void ReduceLingZhouNaiJiu(BaseItem baseItem, int num)
		{
			baseItem.Seid.SetField("NaiJiu", baseItem.Seid["NaiJiu"].I - num);
			if (baseItem.Seid["NaiJiu"].I <= 0)
			{
				Tools.instance.RemoveItem(baseItem.Uid, 1);
			}
		}

		// Token: 0x06005753 RID: 22355 RVA: 0x00244FB9 File Offset: 0x002431B9
		public void removeEquipItem(string UUID)
		{
			this.YSUnequipItem(UUID, 0);
			this.removeItem(UUID);
		}

		// Token: 0x06005754 RID: 22356 RVA: 0x00244FCC File Offset: 0x002431CC
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

		// Token: 0x06005755 RID: 22357 RVA: 0x0024509C File Offset: 0x0024329C
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

		// Token: 0x06005756 RID: 22358 RVA: 0x002451A0 File Offset: 0x002433A0
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

		// Token: 0x06005757 RID: 22359 RVA: 0x0024520C File Offset: 0x0024340C
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

		// Token: 0x06005758 RID: 22360 RVA: 0x00245298 File Offset: 0x00243498
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

		// Token: 0x06005759 RID: 22361 RVA: 0x002452F0 File Offset: 0x002434F0
		public void removeEquip(string UUID)
		{
			this.equipItemList.values.RemoveAll((ITEM_INFO aa) => aa.uuid == UUID);
		}

		// Token: 0x0600575A RID: 22362 RVA: 0x00245328 File Offset: 0x00243528
		public void removeEquip(int id, int sum)
		{
			for (int i = 0; i < sum; i++)
			{
				this.removeEquipByItemID(id);
			}
		}

		// Token: 0x0600575B RID: 22363 RVA: 0x00245348 File Offset: 0x00243548
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

		// Token: 0x0600575C RID: 22364 RVA: 0x002453C4 File Offset: 0x002435C4
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

		// Token: 0x0600575D RID: 22365 RVA: 0x00245448 File Offset: 0x00243648
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

		// Token: 0x0600575E RID: 22366 RVA: 0x00245548 File Offset: 0x00243748
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

		// Token: 0x0600575F RID: 22367 RVA: 0x002455AC File Offset: 0x002437AC
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

		// Token: 0x06005760 RID: 22368 RVA: 0x0024562C File Offset: 0x0024382C
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

		// Token: 0x06005761 RID: 22369 RVA: 0x00245724 File Offset: 0x00243924
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

		// Token: 0x06005762 RID: 22370 RVA: 0x00245808 File Offset: 0x00243A08
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

		// Token: 0x06005763 RID: 22371 RVA: 0x002458C8 File Offset: 0x00243AC8
		public ITEM_INFO FindItemByUUID(string itemUUId)
		{
			return this.itemList.values.Find((ITEM_INFO aa) => aa.uuid == itemUUId);
		}

		// Token: 0x06005764 RID: 22372 RVA: 0x00245900 File Offset: 0x00243B00
		public ITEM_INFO FindEquipItemByUUID(string itemUUId)
		{
			return this.equipItemList.values.Find((ITEM_INFO aa) => aa.uuid == itemUUId);
		}

		// Token: 0x06005765 RID: 22373 RVA: 0x00245938 File Offset: 0x00243B38
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

		// Token: 0x06005766 RID: 22374 RVA: 0x00245AE4 File Offset: 0x00243CE4
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

		// Token: 0x06005767 RID: 22375 RVA: 0x00245B70 File Offset: 0x00243D70
		public void removeItem(int itemID, int Count)
		{
			for (int i = 0; i < Count; i++)
			{
				this.removeItem(itemID);
			}
		}

		// Token: 0x06005768 RID: 22376 RVA: 0x00245B90 File Offset: 0x00243D90
		public void removeItem(string UUID, int Count)
		{
			for (int i = 0; i < Count; i++)
			{
				this.removeItem(UUID);
			}
		}

		// Token: 0x06005769 RID: 22377 RVA: 0x00245BB0 File Offset: 0x00243DB0
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

		// Token: 0x0600576A RID: 22378 RVA: 0x00245BF4 File Offset: 0x00243DF4
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

		// Token: 0x0600576B RID: 22379 RVA: 0x00245C3C File Offset: 0x00243E3C
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

		// Token: 0x0600576C RID: 22380 RVA: 0x00245CD4 File Offset: 0x00243ED4
		public void Save(int id, int index)
		{
			StreamData streamData = this.StreamData;
			streamData.FangAnData.SaveHandle();
			FileStream fileStream = new FileStream(Paths.GetSavePath() + "/StreamData" + Tools.instance.getSaveID(id, index) + ".sav", FileMode.Create);
			new BinaryFormatter().Serialize(fileStream, streamData);
			fileStream.Close();
		}

		// Token: 0x0600576D RID: 22381 RVA: 0x00004095 File Offset: 0x00002295
		public void createItem()
		{
		}

		// Token: 0x0600576E RID: 22382 RVA: 0x00245D2C File Offset: 0x00243F2C
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

		// Token: 0x0600576F RID: 22383 RVA: 0x00245D8C File Offset: 0x00243F8C
		public void SortItem()
		{
			this.itemList.values.Sort(delegate(ITEM_INFO a, ITEM_INFO b)
			{
				int result;
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
						result = num4.CompareTo(num3);
					}
					else if (itemJsonData.type != itemJsonData2.type)
					{
						result = itemJsonData.type.CompareTo(itemJsonData2.type);
					}
					else if (itemJsonData.id != itemJsonData2.id)
					{
						result = itemJsonData.id.CompareTo(itemJsonData2.id);
					}
					else
					{
						result = num.CompareTo(num2);
					}
				}
				catch
				{
					result = 1;
				}
				return result;
			});
		}

		// Token: 0x06005770 RID: 22384 RVA: 0x00245DC0 File Offset: 0x00243FC0
		public static void UnlockShenXianDouFa(int index)
		{
			int num = index + 100;
			int num2 = 10001 + index;
			if (YSGame.YSSaveGame.GetInt("SaveAvatar" + num, 0) != 0)
			{
				return;
			}
			UIPopTip.Inst.Pop("已开启新的神仙斗法", PopTipIconType.叹号);
			YSGame.YSSaveGame.save("SaveAvatar" + num, 1, "-1");
			YSGame.YSSaveGame.save("SaveDFAvatar" + num, 2, "-1");
			JSONObject jsonobject = new JSONObject(JSONObject.Type.OBJECT);
			jsonobject.SetField("1", jsonData.instance.AvatarRandomJsonData[num2.ToString()]);
			YSGame.YSSaveGame.save("AvatarRandomJsonData" + Tools.instance.getSaveID(num, 0), jsonobject, "-1");
		}

		// Token: 0x06005771 RID: 22385 RVA: 0x002409A1 File Offset: 0x0023EBA1
		public void SetMenPai(int id)
		{
			this.menPai = (ushort)id;
		}

		// Token: 0x06005772 RID: 22386 RVA: 0x00245E82 File Offset: 0x00244082
		public void SetLingGen(int id, int value)
		{
			this.LingGeng[id] = value;
		}

		// Token: 0x06005773 RID: 22387 RVA: 0x00004095 File Offset: 0x00002295
		[Obsolete]
		public void startFight(int fightID)
		{
		}

		// Token: 0x06005774 RID: 22388 RVA: 0x00245E91 File Offset: 0x00244091
		[Obsolete]
		public void reqItemList()
		{
			base.baseCall("reqItemList", Array.Empty<object>());
		}

		// Token: 0x06005775 RID: 22389 RVA: 0x00245EA3 File Offset: 0x002440A3
		[Obsolete]
		public void dropRequest(ulong itemUUID)
		{
			base.baseCall("dropRequest", new object[]
			{
				itemUUID
			});
		}

		// Token: 0x06005776 RID: 22390 RVA: 0x00245EC0 File Offset: 0x002440C0
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

		// Token: 0x06005777 RID: 22391 RVA: 0x00245F3E File Offset: 0x0024413E
		[Obsolete]
		public void equipItemRequest(ulong itemUUID)
		{
			base.baseCall("equipItemRequest", new object[]
			{
				itemUUID
			});
		}

		// Token: 0x06005778 RID: 22392 RVA: 0x00245F5A File Offset: 0x0024415A
		[Obsolete]
		public void UnEquipItemRequest(ulong itemUUID)
		{
			base.baseCall("UnEquipItemRequest", new object[]
			{
				itemUUID
			});
		}

		// Token: 0x06005779 RID: 22393 RVA: 0x00245F78 File Offset: 0x00244178
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

		// Token: 0x0600577A RID: 22394 RVA: 0x00246189 File Offset: 0x00244389
		[Obsolete]
		public void CreateAvaterCall(int AvaterID)
		{
			base.baseCall("CreateAvaterCall", new object[]
			{
				AvaterID
			});
		}

		// Token: 0x0600577B RID: 22395 RVA: 0x002461A8 File Offset: 0x002443A8
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

		// Token: 0x0600577C RID: 22396 RVA: 0x002461FC File Offset: 0x002443FC
		[Obsolete]
		public override void onAttack_MaxChanged(int old)
		{
			object obj = this.attack_Max;
			Event.fireOut("set_attack_Max", new object[]
			{
				obj
			});
		}

		// Token: 0x0600577D RID: 22397 RVA: 0x0024622C File Offset: 0x0024442C
		[Obsolete]
		public override void onAttack_MinChanged(int old)
		{
			object obj = this.attack_Min;
			Event.fireOut("set_attack_Min", new object[]
			{
				obj
			});
		}

		// Token: 0x0600577E RID: 22398 RVA: 0x0024625C File Offset: 0x0024445C
		[Obsolete]
		public override void onDefenceChanged(int old)
		{
			object obj = this.defence;
			Event.fireOut("set_defence", new object[]
			{
				obj
			});
		}

		// Token: 0x0600577F RID: 22399 RVA: 0x0024628C File Offset: 0x0024448C
		[Obsolete]
		public override void onRatingChanged(int old)
		{
			object obj = this.rating;
			Event.fireOut("set_rating", new object[]
			{
				obj
			});
		}

		// Token: 0x06005780 RID: 22400 RVA: 0x002462BC File Offset: 0x002444BC
		[Obsolete]
		public override void onDodgeChanged(int old)
		{
			object obj = this.dodge;
			Event.fireOut("set_dodge", new object[]
			{
				obj
			});
		}

		// Token: 0x06005781 RID: 22401 RVA: 0x002462EC File Offset: 0x002444EC
		[Obsolete]
		public override void onStrengthChanged(int old)
		{
			object obj = this.strength;
			Event.fireOut("set_strength", new object[]
			{
				obj
			});
		}

		// Token: 0x06005782 RID: 22402 RVA: 0x0024631C File Offset: 0x0024451C
		[Obsolete]
		public override void onDexterityChanged(int old)
		{
			object obj = this.dexterity;
			Event.fireOut("set_dexterity", new object[]
			{
				obj
			});
		}

		// Token: 0x06005783 RID: 22403 RVA: 0x0024634C File Offset: 0x0024454C
		[Obsolete]
		public override void onExpChanged(ulong old)
		{
			object obj = this.exp;
			Event.fireOut("set_exp", new object[]
			{
				obj
			});
		}

		// Token: 0x06005784 RID: 22404 RVA: 0x0024637C File Offset: 0x0024457C
		[Obsolete]
		public override void onLevelChanged(ushort old)
		{
			object obj = this.level;
			Event.fireOut("set_level", new object[]
			{
				obj
			});
		}

		// Token: 0x06005785 RID: 22405 RVA: 0x002463A9 File Offset: 0x002445A9
		[Obsolete]
		public override void onCrystalChanged(List<int> oldValue)
		{
			Event.fireOut("crtstalChanged", new object[]
			{
				this,
				oldValue
			});
		}

		// Token: 0x06005786 RID: 22406 RVA: 0x002463C4 File Offset: 0x002445C4
		[Obsolete]
		public override void onStaminaChanged(int old)
		{
			object obj = this.stamina;
			Event.fireOut("set_stamina", new object[]
			{
				obj
			});
		}

		// Token: 0x06005787 RID: 22407 RVA: 0x002463F1 File Offset: 0x002445F1
		[Obsolete]
		public void dialog(int targetID, uint dialogID)
		{
			this.dialogMsg.dialog(targetID, dialogID);
		}

		// Token: 0x06005788 RID: 22408 RVA: 0x00246400 File Offset: 0x00244600
		[Obsolete]
		public void messagelog(int targetID, uint dialogID)
		{
			this.dialogMsg.messagelog(targetID, dialogID);
		}

		// Token: 0x06005789 RID: 22409 RVA: 0x00246410 File Offset: 0x00244610
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

		// Token: 0x0600578A RID: 22410 RVA: 0x00246468 File Offset: 0x00244668
		[Obsolete]
		public override void pickUp_re(ITEM_INFO itemInfo)
		{
			Event.fireOut("pickUp_re", new object[]
			{
				itemInfo
			});
			this.itemDict[itemInfo.UUID] = itemInfo;
		}

		// Token: 0x0600578B RID: 22411 RVA: 0x00246490 File Offset: 0x00244690
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

		// Token: 0x0600578C RID: 22412 RVA: 0x00246540 File Offset: 0x00244740
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

		// Token: 0x0600578D RID: 22413 RVA: 0x00246619 File Offset: 0x00244819
		[Obsolete]
		public override void errorInfo(int errorCode)
		{
			Dbg.DEBUG_MSG("errorInfo(" + errorCode + ")");
		}

		// Token: 0x0600578E RID: 22414 RVA: 0x00246638 File Offset: 0x00244838
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

		// Token: 0x0600578F RID: 22415 RVA: 0x00246673 File Offset: 0x00244873
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

		// Token: 0x06005790 RID: 22416 RVA: 0x002466A5 File Offset: 0x002448A5
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

		// Token: 0x06005791 RID: 22417 RVA: 0x002466CD File Offset: 0x002448CD
		[Obsolete]
		public override void dialog_close()
		{
			Event.fireOut("dialog_close", Array.Empty<object>());
		}

		// Token: 0x06005792 RID: 22418 RVA: 0x002466DE File Offset: 0x002448DE
		[Obsolete]
		public void StartCrafting(int itemID, int Count)
		{
			base.baseCall("StartCrafting", new object[]
			{
				itemID,
				Count
			});
		}

		// Token: 0x06005793 RID: 22419 RVA: 0x00246703 File Offset: 0x00244903
		[Obsolete]
		public void CancelCrafting()
		{
			base.baseCall("CancelCrafting", Array.Empty<object>());
		}

		// Token: 0x06005794 RID: 22420 RVA: 0x00246715 File Offset: 0x00244915
		[Obsolete]
		public void backToHome()
		{
			base.baseCall("backToHome", Array.Empty<object>());
		}

		// Token: 0x06005795 RID: 22421 RVA: 0x00246727 File Offset: 0x00244927
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

		// Token: 0x06005796 RID: 22422 RVA: 0x0024674F File Offset: 0x0024494F
		[Obsolete]
		public override void setPlayerTime(uint time)
		{
			Event.fireOut("setPlayerTime", new object[]
			{
				time
			});
		}

		// Token: 0x06005797 RID: 22423 RVA: 0x0024676A File Offset: 0x0024496A
		[Obsolete]
		public override void GameErrorMsg(string msg)
		{
			Event.fireOut("GameErrorMsg", new object[]
			{
				msg
			});
		}

		// Token: 0x06005798 RID: 22424 RVA: 0x00246780 File Offset: 0x00244980
		[Obsolete]
		public void DayZombie()
		{
			base.cellCall("DayZombie", Array.Empty<object>());
		}

		// Token: 0x06005799 RID: 22425 RVA: 0x00246792 File Offset: 0x00244992
		[Obsolete]
		public override void PlayerLvUP()
		{
			Event.fireOut("PlayerLvUP", Array.Empty<object>());
		}

		// Token: 0x0600579A RID: 22426 RVA: 0x000DBFA9 File Offset: 0x000DA1A9
		[Obsolete]
		public override void createItem(ITEM_INFO arg1)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600579B RID: 22427 RVA: 0x002467A3 File Offset: 0x002449A3
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

		// Token: 0x0600579C RID: 22428 RVA: 0x000DBFA9 File Offset: 0x000DA1A9
		[Obsolete]
		public override void onStartGame()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600579D RID: 22429 RVA: 0x002467D0 File Offset: 0x002449D0
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

		// Token: 0x0600579E RID: 22430 RVA: 0x00246801 File Offset: 0x00244A01
		[Obsolete]
		public override void onMPChanged(int oldValue)
		{
			this.getDefinedProperty("MP");
		}

		// Token: 0x0600579F RID: 22431 RVA: 0x00246810 File Offset: 0x00244A10
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

		// Token: 0x060057A0 RID: 22432 RVA: 0x00246841 File Offset: 0x00244A41
		[Obsolete]
		public override void onMP_MaxChanged(int oldValue)
		{
			this.getDefinedProperty("MP_Max");
		}

		// Token: 0x060057A1 RID: 22433 RVA: 0x00246850 File Offset: 0x00244A50
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

		// Token: 0x060057A2 RID: 22434 RVA: 0x00246884 File Offset: 0x00244A84
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

		// Token: 0x060057A3 RID: 22435 RVA: 0x00004095 File Offset: 0x00002295
		[Obsolete]
		public override void onSubStateChanged(byte oldValue)
		{
		}

		// Token: 0x060057A4 RID: 22436 RVA: 0x00004095 File Offset: 0x00002295
		[Obsolete]
		public override void onUtypeChanged(uint oldValue)
		{
		}

		// Token: 0x060057A5 RID: 22437 RVA: 0x00004095 File Offset: 0x00002295
		[Obsolete]
		public override void onUidChanged(uint oldValue)
		{
		}

		// Token: 0x060057A6 RID: 22438 RVA: 0x00004095 File Offset: 0x00002295
		[Obsolete]
		public override void onSpaceUTypeChanged(uint oldValue)
		{
		}

		// Token: 0x060057A7 RID: 22439 RVA: 0x002468B5 File Offset: 0x00244AB5
		[Obsolete]
		public override void onMoveSpeedChanged(byte oldValue)
		{
			this.getDefinedProperty("moveSpeed");
		}

		// Token: 0x060057A8 RID: 22440 RVA: 0x002468C3 File Offset: 0x00244AC3
		[Obsolete]
		public override void onHungerChanged(short oldValue)
		{
			Event.fireOut("set_Hunger", new object[]
			{
				oldValue
			});
		}

		// Token: 0x060057A9 RID: 22441 RVA: 0x002468DE File Offset: 0x00244ADE
		[Obsolete]
		public override void onThirstChanged(short oldValue)
		{
			Event.fireOut("set_Thirst", new object[]
			{
				oldValue
			});
		}

		// Token: 0x04005131 RID: 20785
		public string lastScence = "AllMaps";

		// Token: 0x04005132 RID: 20786
		public string lastFuBenScence = "";

		// Token: 0x04005133 RID: 20787
		public string NowFuBen = "";

		// Token: 0x04005134 RID: 20788
		public int BanBenHao = 1;

		// Token: 0x04005135 RID: 20789
		public int NowRandomFuBenID;

		// Token: 0x04005136 RID: 20790
		public int showSkillName;

		// Token: 0x04005137 RID: 20791
		public int showStaticSkillDengJi;

		// Token: 0x04005138 RID: 20792
		public int chengHao;

		// Token: 0x04005139 RID: 20793
		public CardMag cardMag;

		// Token: 0x0400513A RID: 20794
		public Combat combat;

		// Token: 0x0400513B RID: 20795
		public AI ai;

		// Token: 0x0400513C RID: 20796
		public Spell spell;

		// Token: 0x0400513D RID: 20797
		public JieYin jieyin;

		// Token: 0x0400513E RID: 20798
		public Dialog dialogMsg;

		// Token: 0x0400513F RID: 20799
		public BuffMag buffmag;

		// Token: 0x04005140 RID: 20800
		public WuDaoMag wuDaoMag;

		// Token: 0x04005141 RID: 20801
		public WorldTime worldTimeMag = new WorldTime();

		// Token: 0x04005142 RID: 20802
		public EmailDataMag emailDateMag = new EmailDataMag();

		// Token: 0x04005143 RID: 20803
		public StreamData StreamData = new StreamData();

		// Token: 0x04005144 RID: 20804
		public int ExchangeMeetingID;

		// Token: 0x04005145 RID: 20805
		public TaskMag taskMag;

		// Token: 0x04005146 RID: 20806
		public FightTempValue fightTemp;

		// Token: 0x04005147 RID: 20807
		public ZulinContorl zulinContorl;

		// Token: 0x04005148 RID: 20808
		public FubenContrl fubenContorl;

		// Token: 0x04005149 RID: 20809
		public NomelTaskMag nomelTaskMag;

		// Token: 0x0400514A RID: 20810
		public chenghaoMag chenghaomag;

		// Token: 0x0400514B RID: 20811
		public RandomFuBenMag randomFuBenMag;

		// Token: 0x0400514C RID: 20812
		public SeaNodeMag seaNodeMag;

		// Token: 0x0400514D RID: 20813
		public ChuanYingManager chuanYingManager;

		// Token: 0x0400514E RID: 20814
		public JianLingManager jianLingManager;

		// Token: 0x0400514F RID: 20815
		public static SkillBox skillbox = new SkillBox();

		// Token: 0x04005150 RID: 20816
		public Dictionary<ulong, ITEM_INFO> itemDict = new Dictionary<ulong, ITEM_INFO>();

		// Token: 0x04005151 RID: 20817
		public Dictionary<ulong, ITEM_INFO> equipItemDict = new Dictionary<ulong, ITEM_INFO>();

		// Token: 0x04005152 RID: 20818
		public List<Skill> skill = new List<Skill>();

		// Token: 0x04005153 RID: 20819
		public List<StaticSkill> StaticSkill = new List<StaticSkill>();

		// Token: 0x04005154 RID: 20820
		private ulong[] itemIndex2Uids = new ulong[50];

		// Token: 0x04005155 RID: 20821
		private ulong[] equipIndex2Uids = new ulong[5];

		// Token: 0x04005156 RID: 20822
		public List<List<int>> bufflist = new List<List<int>>();

		// Token: 0x04005157 RID: 20823
		public Dictionary<int, Dictionary<int, int>> SkillSeidFlag = new Dictionary<int, Dictionary<int, int>>();

		// Token: 0x04005158 RID: 20824
		public Dictionary<int, Dictionary<int, int>> BuffSeidFlag = new Dictionary<int, Dictionary<int, int>>();

		// Token: 0x04005159 RID: 20825
		public Dictionary<int, Dictionary<int, int>> StaticSkillSeidFlag = new Dictionary<int, Dictionary<int, int>>();

		// Token: 0x0400515A RID: 20826
		public Dictionary<int, Dictionary<int, int>> EquipSeidFlag = new Dictionary<int, Dictionary<int, int>>();

		// Token: 0x0400515B RID: 20827
		public Dictionary<int, Dictionary<int, int>> JieDanSkillSeidFlag = new Dictionary<int, Dictionary<int, int>>();

		// Token: 0x0400515C RID: 20828
		public Dictionary<int, int> DrawWeight = new Dictionary<int, int>();

		// Token: 0x0400515D RID: 20829
		public int NowMapIndex = 101;

		// Token: 0x0400515E RID: 20830
		public int SkillRemoveCardNum;

		// Token: 0x0400515F RID: 20831
		public int nowConfigEquipSkill;

		// Token: 0x04005160 RID: 20832
		public int nowConfigEquipStaticSkill;

		// Token: 0x04005161 RID: 20833
		public int nowConfigEquipItem;

		// Token: 0x04005162 RID: 20834
		public List<SkillItem>[] configEquipSkill = new List<SkillItem>[]
		{
			new List<SkillItem>(),
			new List<SkillItem>(),
			new List<SkillItem>(),
			new List<SkillItem>(),
			new List<SkillItem>()
		};

		// Token: 0x04005163 RID: 20835
		public List<SkillItem>[] configEquipStaticSkill = new List<SkillItem>[]
		{
			new List<SkillItem>(),
			new List<SkillItem>(),
			new List<SkillItem>(),
			new List<SkillItem>(),
			new List<SkillItem>()
		};

		// Token: 0x04005164 RID: 20836
		public ITEM_INFO_LIST[] configEquipItem = new ITEM_INFO_LIST[]
		{
			new ITEM_INFO_LIST(),
			new ITEM_INFO_LIST(),
			new ITEM_INFO_LIST(),
			new ITEM_INFO_LIST(),
			new ITEM_INFO_LIST()
		};

		// Token: 0x04005165 RID: 20837
		public List<SkillItem> equipSkillList = new List<SkillItem>();

		// Token: 0x04005166 RID: 20838
		public List<SkillItem> equipStaticSkillList = new List<SkillItem>();

		// Token: 0x04005167 RID: 20839
		public List<SkillItem> hasJieDanSkillList = new List<SkillItem>();

		// Token: 0x04005168 RID: 20840
		public List<SkillItem> hasSkillList = new List<SkillItem>();

		// Token: 0x04005169 RID: 20841
		public List<SkillItem> hasStaticSkillList = new List<SkillItem>();

		// Token: 0x0400516A RID: 20842
		public Avatar OtherAvatar;

		// Token: 0x0400516B RID: 20843
		public int showTupo;

		// Token: 0x0400516C RID: 20844
		public int _xinjin;

		// Token: 0x0400516D RID: 20845
		public string firstName = "";

		// Token: 0x0400516E RID: 20846
		public string lastName = "";

		// Token: 0x0400516F RID: 20847
		public int Sex = 1;

		// Token: 0x04005170 RID: 20848
		public int nowPaiMaiCompereAvatarID;

		// Token: 0x04005171 RID: 20849
		public int nowPaiMaiID;

		// Token: 0x04005172 RID: 20850
		public int _WuDaoDian;

		// Token: 0x04005173 RID: 20851
		public int _JieYingJinMai;

		// Token: 0x04005174 RID: 20852
		public int _JieYingYiZHi;

		// Token: 0x04005175 RID: 20853
		public AvatarStaticValue StaticValue = new AvatarStaticValue();

		// Token: 0x04005176 RID: 20854
		public JSONObject AvatarGotChuanGong = new JSONObject();

		// Token: 0x04005177 RID: 20855
		public JSONObject AvatarQieCuo = new JSONObject();

		// Token: 0x04005178 RID: 20856
		public JSONObject SuiJiShiJian = new JSONObject(JSONObject.Type.OBJECT);

		// Token: 0x04005179 RID: 20857
		public JSONObject ZuLin = new JSONObject(JSONObject.Type.OBJECT);

		// Token: 0x0400517A RID: 20858
		public JSONObject FuBen = new JSONObject(JSONObject.Type.OBJECT);

		// Token: 0x0400517B RID: 20859
		public JSONObject CanJiaPaiMai = new JSONObject(JSONObject.Type.OBJECT);

		// Token: 0x0400517C RID: 20860
		public JSONObject NaiYaoXin = new JSONObject(JSONObject.Type.OBJECT);

		// Token: 0x0400517D RID: 20861
		public JSONObject DanFang = new JSONObject(JSONObject.Type.ARRAY);

		// Token: 0x0400517E RID: 20862
		public JSONObject YaoCaiShuXin = new JSONObject(JSONObject.Type.OBJECT);

		// Token: 0x0400517F RID: 20863
		public JSONObject YaoCaiChanDi = new JSONObject(JSONObject.Type.ARRAY);

		// Token: 0x04005180 RID: 20864
		public JSONObject YaoCaiIsGet = new JSONObject(JSONObject.Type.ARRAY);

		// Token: 0x04005181 RID: 20865
		public JSONObject AllMapRandomNode = new JSONObject(JSONObject.Type.OBJECT);

		// Token: 0x04005182 RID: 20866
		public JSONObject MenPaiHaoGanDu = new JSONObject(JSONObject.Type.OBJECT);

		// Token: 0x04005183 RID: 20867
		public JSONObject NomelTaskJson = new JSONObject(JSONObject.Type.OBJECT);

		// Token: 0x04005184 RID: 20868
		public JSONObject NomelTaskFlag = new JSONObject(JSONObject.Type.OBJECT);

		// Token: 0x04005185 RID: 20869
		public JSONObject LingGuang = new JSONObject(JSONObject.Type.ARRAY);

		// Token: 0x04005186 RID: 20870
		public JSONObject TianFuID = new JSONObject(JSONObject.Type.OBJECT);

		// Token: 0x04005187 RID: 20871
		public JSONObject SelectTianFuID = new JSONObject(JSONObject.Type.ARRAY);

		// Token: 0x04005188 RID: 20872
		public JSONObject openPanelList = new JSONObject();

		// Token: 0x04005189 RID: 20873
		public JSONObject NoGetChuanYingList = new JSONObject();

		// Token: 0x0400518A RID: 20874
		public JSONObject NewChuanYingList = new JSONObject();

		// Token: 0x0400518B RID: 20875
		public JSONObject HasReadChuanYingList = new JSONObject();

		// Token: 0x0400518C RID: 20876
		public JSONObject TieJianHongDianList = new JSONObject();

		// Token: 0x0400518D RID: 20877
		public JSONObject ToalChuanYingFuList = new JSONObject();

		// Token: 0x0400518E RID: 20878
		public JSONObject HasSendChuanYingFuList = new JSONObject();

		// Token: 0x0400518F RID: 20879
		public JSONObject PaiMaiMaxMoneyAvatarDate = new JSONObject();

		// Token: 0x04005190 RID: 20880
		public JSONObject WuDaoKillAvatar = new JSONObject(JSONObject.Type.ARRAY);

		// Token: 0x04005191 RID: 20881
		public JSONObject HasLianZhiDanYao = new JSONObject(JSONObject.Type.ARRAY);

		// Token: 0x04005192 RID: 20882
		public JSONObject AvatarChengJiuData = new JSONObject(JSONObject.Type.OBJECT);

		// Token: 0x04005193 RID: 20883
		public JSONObject AvatarHasAchivement = new JSONObject(JSONObject.Type.ARRAY);

		// Token: 0x04005194 RID: 20884
		public JSONObject ShangJinPingFen = new JSONObject(JSONObject.Type.OBJECT);

		// Token: 0x04005195 RID: 20885
		public JSONObject ShiLiChengHaoLevel = new JSONObject(JSONObject.Type.OBJECT);

		// Token: 0x04005196 RID: 20886
		public int NPCCreateIndex = 20000;

		// Token: 0x04005197 RID: 20887
		public JSONObject AvatarFengLu = new JSONObject(JSONObject.Type.ARRAY);

		// Token: 0x04005198 RID: 20888
		public JSONObject ZengLi = new JSONObject(JSONObject.Type.OBJECT);

		// Token: 0x04005199 RID: 20889
		public JSONObject TeatherId = new JSONObject(JSONObject.Type.ARRAY);

		// Token: 0x0400519A RID: 20890
		public JSONObject DaoLvId = new JSONObject(JSONObject.Type.ARRAY);

		// Token: 0x0400519B RID: 20891
		public JSONObject Brother = new JSONObject(JSONObject.Type.ARRAY);

		// Token: 0x0400519C RID: 20892
		public JSONObject TuDiId = new JSONObject(JSONObject.Type.ARRAY);

		// Token: 0x0400519D RID: 20893
		public JSONObject DaTingId = new JSONObject(JSONObject.Type.ARRAY);

		// Token: 0x0400519E RID: 20894
		public int IsShowXuanWo;

		// Token: 0x0400519F RID: 20895
		public JSONObject PlayTutorialData = new JSONObject(JSONObject.Type.OBJECT);

		// Token: 0x040051A0 RID: 20896
		public JSONObject ShuangXiuData = new JSONObject(JSONObject.Type.OBJECT);

		// Token: 0x040051A1 RID: 20897
		public JSONObject DaoLvChengHu = new JSONObject(JSONObject.Type.OBJECT);

		// Token: 0x040051A2 RID: 20898
		public JSONObject DongFuData = new JSONObject(JSONObject.Type.OBJECT);

		// Token: 0x040051A3 RID: 20899
		public JSONObject NowDongFuID = new JSONObject(JSONObject.Type.NUMBER);

		// Token: 0x040051A4 RID: 20900
		public JSONObject GaoShi = new JSONObject(JSONObject.Type.OBJECT);

		// Token: 0x040051A5 RID: 20901
		public JSONObject SeaTanSuoDu = new JSONObject(JSONObject.Type.OBJECT);

		// Token: 0x040051A6 RID: 20902
		public JSONObject HuaShenStartXianXing = new JSONObject(0);

		// Token: 0x040051A7 RID: 20903
		public JSONObject TianJie = new JSONObject(JSONObject.Type.OBJECT);

		// Token: 0x040051A8 RID: 20904
		public JSONObject TianJieCanLingWuSkills = new JSONObject(JSONObject.Type.ARRAY);

		// Token: 0x040051A9 RID: 20905
		public JSONObject TianJieYiLingWuSkills = new JSONObject(JSONObject.Type.ARRAY);

		// Token: 0x040051AA RID: 20906
		public JSONObject TianJieEquipedSkills = new JSONObject(JSONObject.Type.ARRAY);

		// Token: 0x040051AB RID: 20907
		public JSONObject TianJieSkillRecordValue = new JSONObject(JSONObject.Type.OBJECT);

		// Token: 0x040051AC RID: 20908
		public JSONObject TianJieCanGuanNPCs = new JSONObject(JSONObject.Type.ARRAY);

		// Token: 0x040051AD RID: 20909
		public string TianJieBeforeShenYouSceneName = "";

		// Token: 0x040051AE RID: 20910
		public JSONObject HuaShenWuDao = new JSONObject(0);

		// Token: 0x040051AF RID: 20911
		public JSONObject HuaShenLingYuSkill = new JSONObject(0);

		// Token: 0x040051B0 RID: 20912
		public JSONObject HideHaiYuTanSuo = new JSONObject(JSONObject.Type.ARRAY);

		// Token: 0x040051B1 RID: 20913
		public JSONObject FightCostRecord = new JSONObject(JSONObject.Type.OBJECT);

		// Token: 0x040051B2 RID: 20914
		public JSONObject JianLingUnlockedXianSuo = new JSONObject(JSONObject.Type.OBJECT);

		// Token: 0x040051B3 RID: 20915
		public JSONObject JianLingUnlockedZhenXiang = new JSONObject(JSONObject.Type.OBJECT);

		// Token: 0x040051B4 RID: 20916
		public int JianLingExJiYiHuiFuDu;

		// Token: 0x040051B5 RID: 20917
		public JSONObject ShengPingRecord = new JSONObject(JSONObject.Type.OBJECT);

		// Token: 0x040051B6 RID: 20918
		public JSONObject OnceShow = new JSONObject(JSONObject.Type.ARRAY);

		// Token: 0x040051B7 RID: 20919
		public int Face;

		// Token: 0x040051B8 RID: 20920
		public string FaceWorkshop = "";

		// Token: 0x040051B9 RID: 20921
		public JSONObject RandomSeed = new JSONObject(0);

		// Token: 0x040051BA RID: 20922
		public JSONObject LingHeCaiJi = new JSONObject(JSONObject.Type.OBJECT);

		// Token: 0x040051BB RID: 20923
		public string NextCreateTime = "0010-1-1";

		// Token: 0x040051BC RID: 20924
		public int LunDaoState = 3;

		// Token: 0x040051BD RID: 20925
		public int LingGan = 20;

		// Token: 0x040051BE RID: 20926
		public int WuDaoZhi;

		// Token: 0x040051BF RID: 20927
		public int lastYear = 1;

		// Token: 0x040051C0 RID: 20928
		public int fakeTimes;

		// Token: 0x040051C1 RID: 20929
		public int deathType;

		// Token: 0x040051C2 RID: 20930
		public int WuDaoZhiLevel;

		// Token: 0x040051C3 RID: 20931
		public int BiGuanLingGuangTime;

		// Token: 0x040051C4 RID: 20932
		public JSONObject WuDaoJson = new JSONObject(JSONObject.Type.OBJECT);

		// Token: 0x040051C5 RID: 20933
		public JObject RandomFuBenList = new JObject();

		// Token: 0x040051C6 RID: 20934
		public JObject EndlessSea = new JObject();

		// Token: 0x040051C7 RID: 20935
		public JObject StaticNTaskTime = new JObject();

		// Token: 0x040051C8 RID: 20936
		public JObject EndlessSeaRandomNode = new JObject();

		// Token: 0x040051C9 RID: 20937
		public JObject EndlessSeaAvatarSeeIsland = new JObject();

		// Token: 0x040051CA RID: 20938
		public JSONObject EndlessSeaBoss = new JSONObject(JSONObject.Type.OBJECT);

		// Token: 0x040051CB RID: 20939
		public JObject ItemBuffList = new JObject();

		// Token: 0x040051CC RID: 20940
		public JSONObject TaskZhuiZhong = new JSONObject();

		// Token: 0x040051CD RID: 20941
		public int Dandu;

		// Token: 0x040051CE RID: 20942
		private int _ZhuJiJinDu;

		// Token: 0x040051CF RID: 20943
		public int AliveFriendCount;

		// Token: 0x040051D0 RID: 20944
		public bool IsCanSetFace;
	}
}
