using System;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;
using UnityEngine.SceneManagement;
using YSGame;

namespace Fungus
{
	// Token: 0x02001452 RID: 5202
	[CommandInfo("YSTools", "StartFight", "开始战斗模块", 0)]
	[AddComponentMenu("")]
	public class StartFight : Command
	{
		// Token: 0x17000B9F RID: 2975
		// (get) Token: 0x06007D90 RID: 32144 RVA: 0x00054EBF File Offset: 0x000530BF
		public StartFight.MonstarType _MonstarSetType
		{
			get
			{
				return this.MonstarSetType;
			}
		}

		// Token: 0x17000BA0 RID: 2976
		// (get) Token: 0x06007D91 RID: 32145 RVA: 0x00054EC7 File Offset: 0x000530C7
		public StartFight.MonstarType _BackgroundSetType
		{
			get
			{
				return this.BackgroundSetType;
			}
		}

		// Token: 0x06007D92 RID: 32146 RVA: 0x002C69B0 File Offset: 0x002C4BB0
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
				UIPopTip.Inst.Pop("获取NPC错误，请将存档发送给策划", PopTipIconType.叹号);
			}
			this.Continue();
		}

		// Token: 0x06007D93 RID: 32147 RVA: 0x002C6AA0 File Offset: 0x002C4CA0
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
			if (FightType > (StartFight.FightEnumType)0)
			{
				Tools.instance.monstarMag.FightType = FightType;
			}
			else
			{
				Tools.instance.monstarMag.FightType = StartFight.FightEnumType.Normal;
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

		// Token: 0x06007D94 RID: 32148 RVA: 0x002C6D88 File Offset: 0x002C4F88
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

		// Token: 0x06007D95 RID: 32149 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x04006AF4 RID: 27380
		[Tooltip("设置怪物ID的方式")]
		[SerializeField]
		protected StartFight.MonstarType MonstarSetType;

		// Token: 0x04006AF5 RID: 27381
		[Tooltip("怪物的ID")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable MonstarFungusID;

		// Token: 0x04006AF6 RID: 27382
		[Tooltip("怪物的ID")]
		[SerializeField]
		public int MonstarID;

		// Token: 0x04006AF7 RID: 27383
		[Tooltip("给怪物加buff")]
		[SerializeField]
		protected List<StarttFightAddBuff> MonstarAddBuffList = new List<StarttFightAddBuff>();

		// Token: 0x04006AF8 RID: 27384
		[Tooltip("给主角加buff")]
		[SerializeField]
		protected List<StarttFightAddBuff> HeroAddBuffList = new List<StarttFightAddBuff>();

		// Token: 0x04006AF9 RID: 27385
		[Tooltip("战斗的类型")]
		[SerializeField]
		protected StartFight.FightEnumType FightType = StartFight.FightEnumType.Normal;

		// Token: 0x04006AFA RID: 27386
		[Tooltip("设置战场背景的方式")]
		[SerializeField]
		protected StartFight.MonstarType BackgroundSetType;

		// Token: 0x04006AFB RID: 27387
		[Tooltip("背景的ID")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable BackgroundFungusID;

		// Token: 0x04006AFC RID: 27388
		[Tooltip("背景的ID")]
		[SerializeField]
		protected int BackgroundID;

		// Token: 0x04006AFD RID: 27389
		[Tooltip("开启战场对话")]
		[SerializeField]
		protected int FightTalk;

		// Token: 0x04006AFE RID: 27390
		[Tooltip("开启固定抽排")]
		[SerializeField]
		protected int FightCard;

		// Token: 0x04006AFF RID: 27391
		[Tooltip("是否能战前逃跑")]
		[SerializeField]
		protected int CanFpRun = 1;

		// Token: 0x04006B00 RID: 27392
		[Tooltip("战场音乐")]
		[SerializeField]
		protected string FightMusic = "战斗1";

		// Token: 0x04006B01 RID: 27393
		[Tooltip("是否开启海域移除NPC")]
		[SerializeField]
		protected bool SeaRemoveNPCFlag;

		// Token: 0x04006B02 RID: 27394
		[Tooltip("海域移除NPC的编号UUID")]
		[VariableProperty(new Type[]
		{
			typeof(StringVariable)
		})]
		[SerializeField]
		protected StringVariable SeaRemoveNPCUUID;

		// Token: 0x02001453 RID: 5203
		public enum FightEnumType
		{
			// Token: 0x04006B04 RID: 27396
			Normal = 1,
			// Token: 0x04006B05 RID: 27397
			XingMo,
			// Token: 0x04006B06 RID: 27398
			DuJie,
			// Token: 0x04006B07 RID: 27399
			LeiTai,
			// Token: 0x04006B08 RID: 27400
			HuanJin,
			// Token: 0x04006B09 RID: 27401
			BossZhan,
			// Token: 0x04006B0A RID: 27402
			DiFangTaoLi,
			// Token: 0x04006B0B RID: 27403
			QieCuo,
			// Token: 0x04006B0C RID: 27404
			XinShouYinDao,
			// Token: 0x04006B0D RID: 27405
			DouFa,
			// Token: 0x04006B0E RID: 27406
			JieDan,
			// Token: 0x04006B0F RID: 27407
			ZhangLaoZhan,
			// Token: 0x04006B10 RID: 27408
			BuShaDuiShou,
			// Token: 0x04006B11 RID: 27409
			JieYing,
			// Token: 0x04006B12 RID: 27410
			ZhuJi,
			// Token: 0x04006B13 RID: 27411
			古树根须,
			// Token: 0x04006B14 RID: 27412
			生死比试,
			// Token: 0x04006B15 RID: 27413
			HuaShen,
			// Token: 0x04006B16 RID: 27414
			FeiSheng,
			// Token: 0x04006B17 RID: 27415
			无装备无丹药擂台,
			// Token: 0x04006B18 RID: 27416
			天劫秘术领悟
		}

		// Token: 0x02001454 RID: 5204
		public enum MonstarType
		{
			// Token: 0x04006B1A RID: 27418
			Normal,
			// Token: 0x04006B1B RID: 27419
			FungusVariable
		}
	}
}
