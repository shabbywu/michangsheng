using System;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001278 RID: 4728
	[CommandInfo("Narrative", "Say", "Writes text in a dialog box.", 0)]
	[AddComponentMenu("")]
	public class Say : Command, ILocalizable
	{
		// Token: 0x17000A7C RID: 2684
		// (set) Token: 0x060072A6 RID: 29350 RVA: 0x0004E17D File Offset: 0x0004C37D
		public StartFight.MonstarType pubAvatarIDSetType
		{
			set
			{
				this.AvatarIDSetType = value;
			}
		}

		// Token: 0x17000A7D RID: 2685
		// (set) Token: 0x060072A7 RID: 29351 RVA: 0x0004E186 File Offset: 0x0004C386
		public int pubAvatarIntID
		{
			set
			{
				this.AvatarIntID = value;
			}
		}

		// Token: 0x17000A7E RID: 2686
		// (set) Token: 0x060072A8 RID: 29352 RVA: 0x0004E18F File Offset: 0x0004C38F
		public bool SetfadeWhenDone
		{
			set
			{
				this.fadeWhenDone = value;
			}
		}

		// Token: 0x17000A7F RID: 2687
		// (set) Token: 0x060072A9 RID: 29353 RVA: 0x0004E198 File Offset: 0x0004C398
		public bool SetwaitForClick
		{
			set
			{
				this.waitForClick = value;
			}
		}

		// Token: 0x17000A80 RID: 2688
		// (get) Token: 0x060072AA RID: 29354 RVA: 0x0004E1A1 File Offset: 0x0004C3A1
		public virtual Character _Character
		{
			get
			{
				return this.character;
			}
		}

		// Token: 0x17000A81 RID: 2689
		// (get) Token: 0x060072AB RID: 29355 RVA: 0x0004E1A9 File Offset: 0x0004C3A9
		public virtual StartFight.MonstarType _AvatarIDSetType
		{
			get
			{
				return this.AvatarIDSetType;
			}
		}

		// Token: 0x17000A82 RID: 2690
		// (get) Token: 0x060072AC RID: 29356 RVA: 0x0004E1B1 File Offset: 0x0004C3B1
		// (set) Token: 0x060072AD RID: 29357 RVA: 0x0004E1B9 File Offset: 0x0004C3B9
		public virtual Sprite Portrait
		{
			get
			{
				return this.portrait;
			}
			set
			{
				this.portrait = value;
			}
		}

		// Token: 0x17000A83 RID: 2691
		// (get) Token: 0x060072AE RID: 29358 RVA: 0x0004E1C2 File Offset: 0x0004C3C2
		public virtual bool ExtendPrevious
		{
			get
			{
				return this.extendPrevious;
			}
		}

		// Token: 0x060072AF RID: 29359 RVA: 0x002A8BB8 File Offset: 0x002A6DB8
		public override void OnEnter()
		{
			Tools.instance.getPlayer().StreamData.FungusSaveMgr.SetCommand(this);
			if (!this.showAlways && this.executionCount >= this.showCount)
			{
				this.Continue();
				return;
			}
			this.executionCount++;
			if (this.character != null && this.character.SetSayDialog != null)
			{
				SayDialog.ActiveSayDialog = this.character.SetSayDialog;
			}
			if (this.setSayDialog != null)
			{
				SayDialog.ActiveSayDialog = this.setSayDialog;
			}
			SayDialog sayDialog = SayDialog.GetSayDialog();
			if (sayDialog == null)
			{
				this.Continue();
				return;
			}
			Flowchart flowchart = this.GetFlowchart();
			sayDialog.SetActive(true);
			int num = 0;
			if (this.AvatarIDSetType == StartFight.MonstarType.Normal)
			{
				num = this.AvatarIntID;
			}
			else if (this.AvatarID != null)
			{
				num = this.AvatarID.Value;
			}
			if (NpcJieSuanManager.inst.ImportantNpcBangDingDictionary.ContainsKey(num))
			{
				num = NpcJieSuanManager.inst.ImportantNpcBangDingDictionary[num];
			}
			sayDialog.SetCharacter(this.character, num);
			sayDialog.SetCharacterImage(this.portrait, num);
			Avatar player = Tools.instance.getPlayer();
			int npcid = NPCEx.NPCIDToNew(num);
			if (PlayerEx.IsDaoLv(npcid))
			{
				string daoLvNickName = PlayerEx.GetDaoLvNickName(npcid);
				this.storyText = this.storyText.Replace("{LastName}", player.lastName).Replace("{FirstName}", "").Replace("{gongzi}", daoLvNickName).Replace("{xiongdi}", daoLvNickName).Replace("{shidi}", daoLvNickName).Replace("{shixiong}", daoLvNickName).Replace("{xiaozi}", (player.Sex == 1) ? "小子" : "丫头").Replace("{ta}", (player.Sex == 1) ? "他" : "她").Replace("{menpai}", Tools.getStr("menpai" + player.menPai));
			}
			else
			{
				this.storyText = this.storyText.Replace("{LastName}", player.lastName).Replace("{FirstName}", player.firstName).Replace("{gongzi}", (player.Sex == 1) ? "公子" : "姑娘").Replace("{xiongdi}", (player.Sex == 1) ? "兄弟" : "姑娘").Replace("{shidi}", (player.Sex == 1) ? "师弟" : "师妹").Replace("{shixiong}", (player.Sex == 1) ? "师兄" : "师姐").Replace("{xiaozi}", (player.Sex == 1) ? "小子" : "丫头").Replace("{ta}", (player.Sex == 1) ? "他" : "她").Replace("{menpai}", Tools.getStr("menpai" + player.menPai));
			}
			string text = this.storyText;
			List<CustomTag> activeCustomTags = CustomTag.activeCustomTags;
			for (int i = 0; i < activeCustomTags.Count; i++)
			{
				CustomTag customTag = activeCustomTags[i];
				text = text.Replace(customTag.TagStartSymbol, customTag.ReplaceTagStartWith);
				if (customTag.TagEndSymbol != "" && customTag.ReplaceTagEndWith != "")
				{
					text = text.Replace(customTag.TagEndSymbol, customTag.ReplaceTagEndWith);
				}
			}
			string text2 = flowchart.SubstituteVariables(text);
			sayDialog.Say(text2, !this.extendPrevious, this.waitForClick, this.fadeWhenDone, this.stopVoiceover, this.waitForVO, this.voiceOverClip, delegate
			{
				this.Continue();
			});
		}

		// Token: 0x060072B0 RID: 29360 RVA: 0x002A8FA0 File Offset: 0x002A71A0
		public override string GetSummary()
		{
			string str = "";
			if (this.character != null)
			{
				str = this.character.NameText + ": ";
			}
			if (this.extendPrevious)
			{
				str = "EXTEND: ";
			}
			return str + "\"" + this.storyText + "\"";
		}

		// Token: 0x060072B1 RID: 29361 RVA: 0x0004E1CA File Offset: 0x0004C3CA
		public Say Clone()
		{
			return base.MemberwiseClone() as Say;
		}

		// Token: 0x060072B2 RID: 29362 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x060072B3 RID: 29363 RVA: 0x0004E1D7 File Offset: 0x0004C3D7
		public override void OnReset()
		{
			this.executionCount = 0;
		}

		// Token: 0x060072B4 RID: 29364 RVA: 0x002A8FFC File Offset: 0x002A71FC
		public override void OnStopExecuting()
		{
			SayDialog sayDialog = SayDialog.GetSayDialog();
			if (sayDialog == null)
			{
				return;
			}
			sayDialog.Stop();
		}

		// Token: 0x060072B5 RID: 29365 RVA: 0x0004E1E0 File Offset: 0x0004C3E0
		public virtual string GetStandardText()
		{
			return this.storyText;
		}

		// Token: 0x060072B6 RID: 29366 RVA: 0x0004E1E8 File Offset: 0x0004C3E8
		public virtual void SetStandardText(string standardText)
		{
			this.storyText = standardText;
		}

		// Token: 0x060072B7 RID: 29367 RVA: 0x0004E1F1 File Offset: 0x0004C3F1
		public virtual string GetDescription()
		{
			return this.description;
		}

		// Token: 0x060072B8 RID: 29368 RVA: 0x002A9020 File Offset: 0x002A7220
		public virtual string GetStringId()
		{
			string text = string.Concat(new object[]
			{
				"SAY.",
				this.GetFlowchartLocalizationId(),
				".",
				this.itemId,
				"."
			});
			if (this.character != null)
			{
				text += this.character.NameText;
			}
			return text;
		}

		// Token: 0x040064D3 RID: 25811
		[TextArea(5, 10)]
		[SerializeField]
		protected string storyText = "";

		// Token: 0x040064D4 RID: 25812
		[Tooltip("Notes about this story text for other authors, localization, etc.")]
		[SerializeField]
		protected string description = "";

		// Token: 0x040064D5 RID: 25813
		[Tooltip("设置对话ID的方式")]
		[SerializeField]
		protected StartFight.MonstarType AvatarIDSetType = StartFight.MonstarType.FungusVariable;

		// Token: 0x040064D6 RID: 25814
		[Tooltip("说话的英雄ID")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable AvatarID;

		// Token: 0x040064D7 RID: 25815
		[Tooltip("说话的英雄ID")]
		[SerializeField]
		protected int AvatarIntID;

		// Token: 0x040064D8 RID: 25816
		[Tooltip("Character that is speaking")]
		[SerializeField]
		protected Character character;

		// Token: 0x040064D9 RID: 25817
		[Tooltip("Portrait that represents speaking character")]
		[SerializeField]
		protected Sprite portrait;

		// Token: 0x040064DA RID: 25818
		[Tooltip("Voiceover audio to play when writing the text")]
		[SerializeField]
		protected AudioClip voiceOverClip;

		// Token: 0x040064DB RID: 25819
		[Tooltip("Always show this Say text when the command is executed multiple times")]
		[SerializeField]
		protected bool showAlways = true;

		// Token: 0x040064DC RID: 25820
		[Tooltip("Number of times to show this Say text when the command is executed multiple times")]
		[SerializeField]
		protected int showCount = 1;

		// Token: 0x040064DD RID: 25821
		[Tooltip("Type this text in the previous dialog box.")]
		[SerializeField]
		protected bool extendPrevious;

		// Token: 0x040064DE RID: 25822
		[Tooltip("Fade out the dialog box when writing has finished and not waiting for input.")]
		[SerializeField]
		protected bool fadeWhenDone = true;

		// Token: 0x040064DF RID: 25823
		[Tooltip("Wait for player to click before continuing.")]
		[SerializeField]
		protected bool waitForClick = true;

		// Token: 0x040064E0 RID: 25824
		[Tooltip("Stop playing voiceover when text finishes writing.")]
		[SerializeField]
		protected bool stopVoiceover = true;

		// Token: 0x040064E1 RID: 25825
		[Tooltip("Wait for the Voice Over to complete before continuing")]
		[SerializeField]
		protected bool waitForVO;

		// Token: 0x040064E2 RID: 25826
		[Tooltip("Sets the active Say dialog with a reference to a Say Dialog object in the scene. All story text will now display using this Say Dialog.")]
		[SerializeField]
		protected SayDialog setSayDialog;

		// Token: 0x040064E3 RID: 25827
		protected int executionCount;
	}
}
