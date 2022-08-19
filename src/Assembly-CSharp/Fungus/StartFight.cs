using System;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;
using UnityEngine.SceneManagement;
using YSGame;

namespace Fungus
{
	// Token: 0x02000FA3 RID: 4003
	[CommandInfo("YSTools", "StartFight", "开始战斗模块", 0)]
	[AddComponentMenu("")]
	public class StartFight : Command
	{
		// Token: 0x170008F2 RID: 2290
		// (get) Token: 0x06006FB2 RID: 28594 RVA: 0x002A74D9 File Offset: 0x002A56D9
		public StartFight.MonstarType _MonstarSetType
		{
			get
			{
				return this.MonstarSetType;
			}
		}

		// Token: 0x170008F3 RID: 2291
		// (get) Token: 0x06006FB3 RID: 28595 RVA: 0x002A74E1 File Offset: 0x002A56E1
		public StartFight.MonstarType _BackgroundSetType
		{
			get
			{
				return this.BackgroundSetType;
			}
		}

		// Token: 0x06006FB4 RID: 28596 RVA: 0x002A74EC File Offset: 0x002A56EC
		public override void OnEnter()
		{
			int num = 0;
			if (this.MonstarFungusID != null)
			{
				num = this.MonstarFungusID.Value;
			}
			string seaRemoveNPCUUID = "";
			if (this.SeaRemoveNPCUUID != null)
			{
				seaRemoveNPCUUID = this.SeaRemoveNPCUUID.Value;
			}
			try
			{
				StartFight.Do(this.MonstarID, this.CanFpRun, this.MonstarSetType, this.FightType, num, this.FightTalk, this.FightCard, this.BackgroundID, this.FightMusic, this.SeaRemoveNPCFlag, seaRemoveNPCUUID, this.MonstarAddBuffList, this.HeroAddBuffList);
			}
			catch (Exception ex)
			{
				if (FpUIMag.inst != null)
				{
					FpUIMag.inst.Close();
				}
				Debug.LogError(ex);
				Debug.LogError("错误NPCId：" + num);
				UIPopTip.Inst.Pop("获取NPC错误，请排查是否有mod冲突等问题", PopTipIconType.叹号);
			}
			this.Continue();
		}

		// Token: 0x06006FB5 RID: 28597 RVA: 0x002A75DC File Offset: 0x002A57DC
		public static void Do(int MonstarID, int CanFpRun, StartFight.MonstarType MonstarSetType, StartFight.FightEnumType FightType, int MonstarFungusID, int FightTalk, int FightCard, int BackgroundID, string FightMusic, bool SeaRemoveNPCFlag, string SeaRemoveNPCUUID, List<StarttFightAddBuff> MonstarAddBuffList, List<StarttFightAddBuff> HeroAddBuffList)
		{
			Avatar player = Tools.instance.getPlayer();
			Tools.instance.CanFpRun = CanFpRun;
			if (MonstarID > 0 && NpcJieSuanManager.inst.ImportantNpcBangDingDictionary.ContainsKey(MonstarID))
			{
				MonstarID = NpcJieSuanManager.inst.ImportantNpcBangDingDictionary[MonstarID];
			}
			else if (MonstarSetType == StartFight.MonstarType.FungusVariable && NpcJieSuanManager.inst.ImportantNpcBangDingDictionary.ContainsKey(MonstarFungusID))
			{
				MonstarFungusID = NpcJieSuanManager.inst.ImportantNpcBangDingDictionary[MonstarFungusID];
			}
			foreach (StarttFightAddBuff starttFightAddBuff in MonstarAddBuffList)
			{
				if (!Tools.instance.monstarMag.monstarAddBuff.ContainsKey(starttFightAddBuff.buffID))
				{
					Tools.instance.monstarMag.monstarAddBuff.Add(starttFightAddBuff.buffID, starttFightAddBuff.BuffNum);
				}
			}
			foreach (StarttFightAddBuff starttFightAddBuff2 in HeroAddBuffList)
			{
				if (!Tools.instance.monstarMag.HeroAddBuff.ContainsKey(starttFightAddBuff2.buffID))
				{
					Tools.instance.monstarMag.HeroAddBuff.Add(starttFightAddBuff2.buffID, starttFightAddBuff2.BuffNum);
				}
			}
			if (FightType > (StartFight.FightEnumType)0)
			{
				Tools.instance.monstarMag.FightType = FightType;
			}
			else
			{
				Tools.instance.monstarMag.FightType = StartFight.FightEnumType.Normal;
			}
			if (FightType == StartFight.FightEnumType.JieDan)
			{
				StartFight.LoadTuPo("YSNewJieDanFight", MonstarID);
			}
			else if (FightType == StartFight.FightEnumType.JieYing)
			{
				StartFight.LoadTuPo("YSNewYuanYingFight", MonstarID);
			}
			else if (FightType == StartFight.FightEnumType.ZhuJi)
			{
				StartFight.LoadTuPo("YSNewZhuJi", MonstarID);
			}
			else if (FightType == StartFight.FightEnumType.HuaShen)
			{
				StartFight.LoadTuPo("YSNewHuaShen", MonstarID);
			}
			else if (FightType == StartFight.FightEnumType.FeiSheng)
			{
				StartFight.LoadTuPo("YSNewTianJieFight", MonstarID);
			}
			else if (FightType == StartFight.FightEnumType.天劫秘术领悟)
			{
				GlobalValue.SetTalk(0, 7001, "StartFight 天劫秘术领悟");
				Tools.instance.MonstarID = MonstarID;
				Tools.instance.FinalScene = SceneManager.GetActiveScene().name;
				Tools.instance.loadOtherScenes("YSNewFight");
				UINPCJiaoHu.AllShouldHide = false;
			}
			else if (MonstarSetType == StartFight.MonstarType.Normal)
			{
				Tools.instance.startFight(MonstarID);
			}
			else
			{
				if (NpcJieSuanManager.inst.ImportantNpcBangDingDictionary.ContainsKey(MonstarFungusID))
				{
					MonstarFungusID = NpcJieSuanManager.inst.ImportantNpcBangDingDictionary[MonstarFungusID];
				}
				Tools.instance.startFight(MonstarFungusID);
			}
			Tools.instance.monstarMag.gameStartHP = player.HP;
			Tools.instance.monstarMag.FightTalkID = FightTalk;
			Tools.instance.monstarMag.FightCardID = FightCard;
			Tools.instance.monstarMag.FightImageID = BackgroundID;
			MusicMag.instance.playMusic(FightMusic);
			if (SeaRemoveNPCFlag)
			{
				Tools.instance.StartRemoveSeaMonstarFight(SeaRemoveNPCUUID);
			}
		}

		// Token: 0x06006FB6 RID: 28598 RVA: 0x002A78C4 File Offset: 0x002A5AC4
		public static void LoadTuPo(string tuPoSceneName, int MonstarID)
		{
			Object.Instantiate<GameObject>(ResManager.inst.LoadPrefab("TpPanel"));
			TpUIMag.inst.call = delegate()
			{
				Tools.instance.MonstarID = MonstarID;
				Tools.instance.FinalScene = SceneManager.GetActiveScene().name;
				Tools.instance.loadOtherScenes(tuPoSceneName);
			};
		}

		// Token: 0x06006FB7 RID: 28599 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x04005C25 RID: 23589
		[Tooltip("设置怪物ID的方式")]
		[SerializeField]
		protected StartFight.MonstarType MonstarSetType;

		// Token: 0x04005C26 RID: 23590
		[Tooltip("怪物的ID")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable MonstarFungusID;

		// Token: 0x04005C27 RID: 23591
		[Tooltip("怪物的ID")]
		[SerializeField]
		public int MonstarID;

		// Token: 0x04005C28 RID: 23592
		[Tooltip("给怪物加buff")]
		[SerializeField]
		protected List<StarttFightAddBuff> MonstarAddBuffList = new List<StarttFightAddBuff>();

		// Token: 0x04005C29 RID: 23593
		[Tooltip("给主角加buff")]
		[SerializeField]
		protected List<StarttFightAddBuff> HeroAddBuffList = new List<StarttFightAddBuff>();

		// Token: 0x04005C2A RID: 23594
		[Tooltip("战斗的类型")]
		[SerializeField]
		protected StartFight.FightEnumType FightType = StartFight.FightEnumType.Normal;

		// Token: 0x04005C2B RID: 23595
		[Tooltip("设置战场背景的方式")]
		[SerializeField]
		protected StartFight.MonstarType BackgroundSetType;

		// Token: 0x04005C2C RID: 23596
		[Tooltip("背景的ID")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable BackgroundFungusID;

		// Token: 0x04005C2D RID: 23597
		[Tooltip("背景的ID")]
		[SerializeField]
		protected int BackgroundID;

		// Token: 0x04005C2E RID: 23598
		[Tooltip("开启战场对话")]
		[SerializeField]
		protected int FightTalk;

		// Token: 0x04005C2F RID: 23599
		[Tooltip("开启固定抽排")]
		[SerializeField]
		protected int FightCard;

		// Token: 0x04005C30 RID: 23600
		[Tooltip("是否能战前逃跑")]
		[SerializeField]
		protected int CanFpRun = 1;

		// Token: 0x04005C31 RID: 23601
		[Tooltip("战场音乐")]
		[SerializeField]
		protected string FightMusic = "战斗1";

		// Token: 0x04005C32 RID: 23602
		[Tooltip("是否开启海域移除NPC")]
		[SerializeField]
		protected bool SeaRemoveNPCFlag;

		// Token: 0x04005C33 RID: 23603
		[Tooltip("海域移除NPC的编号UUID")]
		[VariableProperty(new Type[]
		{
			typeof(StringVariable)
		})]
		[SerializeField]
		protected StringVariable SeaRemoveNPCUUID;

		// Token: 0x02001729 RID: 5929
		public enum FightEnumType
		{
			// Token: 0x04007528 RID: 29992
			Normal = 1,
			// Token: 0x04007529 RID: 29993
			XingMo,
			// Token: 0x0400752A RID: 29994
			DuJie,
			// Token: 0x0400752B RID: 29995
			LeiTai,
			// Token: 0x0400752C RID: 29996
			HuanJin,
			// Token: 0x0400752D RID: 29997
			BossZhan,
			// Token: 0x0400752E RID: 29998
			DiFangTaoLi,
			// Token: 0x0400752F RID: 29999
			QieCuo,
			// Token: 0x04007530 RID: 30000
			XinShouYinDao,
			// Token: 0x04007531 RID: 30001
			DouFa,
			// Token: 0x04007532 RID: 30002
			JieDan,
			// Token: 0x04007533 RID: 30003
			ZhangLaoZhan,
			// Token: 0x04007534 RID: 30004
			BuShaDuiShou,
			// Token: 0x04007535 RID: 30005
			JieYing,
			// Token: 0x04007536 RID: 30006
			ZhuJi,
			// Token: 0x04007537 RID: 30007
			古树根须,
			// Token: 0x04007538 RID: 30008
			生死比试,
			// Token: 0x04007539 RID: 30009
			HuaShen,
			// Token: 0x0400753A RID: 30010
			FeiSheng,
			// Token: 0x0400753B RID: 30011
			无装备无丹药擂台,
			// Token: 0x0400753C RID: 30012
			天劫秘术领悟
		}

		// Token: 0x0200172A RID: 5930
		public enum MonstarType
		{
			// Token: 0x0400753E RID: 30014
			Normal,
			// Token: 0x0400753F RID: 30015
			FungusVariable
		}
	}
}
