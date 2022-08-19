using System;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000E27 RID: 3623
	[CommandInfo("Narrative", "Say", "Writes text in a dialog box.", 0)]
	[AddComponentMenu("")]
	public class Say : Command, ILocalizable
	{
		// Token: 0x17000815 RID: 2069
		// (set) Token: 0x06006618 RID: 26136 RVA: 0x00285220 File Offset: 0x00283420
		public StartFight.MonstarType pubAvatarIDSetType
		{
			set
			{
				this.AvatarIDSetType = value;
			}
		}

		// Token: 0x17000816 RID: 2070
		// (set) Token: 0x06006619 RID: 26137 RVA: 0x00285229 File Offset: 0x00283429
		public int pubAvatarIntID
		{
			set
			{
				this.AvatarIntID = value;
			}
		}

		// Token: 0x17000817 RID: 2071
		// (set) Token: 0x0600661A RID: 26138 RVA: 0x00285232 File Offset: 0x00283432
		public bool SetfadeWhenDone
		{
			set
			{
				this.fadeWhenDone = value;
			}
		}

		// Token: 0x17000818 RID: 2072
		// (set) Token: 0x0600661B RID: 26139 RVA: 0x0028523B File Offset: 0x0028343B
		public bool SetwaitForClick
		{
			set
			{
				this.waitForClick = value;
			}
		}

		// Token: 0x17000819 RID: 2073
		// (get) Token: 0x0600661C RID: 26140 RVA: 0x00285244 File Offset: 0x00283444
		public virtual Character _Character
		{
			get
			{
				return this.character;
			}
		}

		// Token: 0x1700081A RID: 2074
		// (get) Token: 0x0600661D RID: 26141 RVA: 0x0028524C File Offset: 0x0028344C
		public virtual StartFight.MonstarType _AvatarIDSetType
		{
			get
			{
				return this.AvatarIDSetType;
			}
		}

		// Token: 0x1700081B RID: 2075
		// (get) Token: 0x0600661E RID: 26142 RVA: 0x00285254 File Offset: 0x00283454
		// (set) Token: 0x0600661F RID: 26143 RVA: 0x0028525C File Offset: 0x0028345C
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

		// Token: 0x1700081C RID: 2076
		// (get) Token: 0x06006620 RID: 26144 RVA: 0x00285265 File Offset: 0x00283465
		public virtual bool ExtendPrevious
		{
			get
			{
				return this.extendPrevious;
			}
		}

		// Token: 0x06006621 RID: 26145 RVA: 0x00285270 File Offset: 0x00283470
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

		// Token: 0x06006622 RID: 26146 RVA: 0x00285658 File Offset: 0x00283858
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

		// Token: 0x06006623 RID: 26147 RVA: 0x002856B3 File Offset: 0x002838B3
		public Say Clone()
		{
			return base.MemberwiseClone() as Say;
		}

		// Token: 0x06006624 RID: 26148 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06006625 RID: 26149 RVA: 0x002856C0 File Offset: 0x002838C0
		public override void OnReset()
		{
			this.executionCount = 0;
		}

		// Token: 0x06006626 RID: 26150 RVA: 0x002856CC File Offset: 0x002838CC
		public override void OnStopExecuting()
		{
			SayDialog sayDialog = SayDialog.GetSayDialog();
			if (sayDialog == null)
			{
				return;
			}
			sayDialog.Stop();
		}

		// Token: 0x06006627 RID: 26151 RVA: 0x002856EF File Offset: 0x002838EF
		public virtual string GetStandardText()
		{
			return this.storyText;
		}

		// Token: 0x06006628 RID: 26152 RVA: 0x002856F7 File Offset: 0x002838F7
		public virtual void SetStandardText(string standardText)
		{
			this.storyText = standardText;
		}

		// Token: 0x06006629 RID: 26153 RVA: 0x00285700 File Offset: 0x00283900
		public virtual string GetDescription()
		{
			return this.description;
		}

		// Token: 0x0600662A RID: 26154 RVA: 0x00285708 File Offset: 0x00283908
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

		// Token: 0x0400578F RID: 22415
		[TextArea(5, 10)]
		[SerializeField]
		protected string storyText = "";

		// Token: 0x04005790 RID: 22416
		[Tooltip("Notes about this story text for other authors, localization, etc.")]
		[SerializeField]
		protected string description = "";

		// Token: 0x04005791 RID: 22417
		[Tooltip("设置对话ID的方式")]
		[SerializeField]
		protected StartFight.MonstarType AvatarIDSetType = StartFight.MonstarType.FungusVariable;

		// Token: 0x04005792 RID: 22418
		[Tooltip("说话的英雄ID")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable AvatarID;

		// Token: 0x04005793 RID: 22419
		[Tooltip("说话的英雄ID")]
		[SerializeField]
		protected int AvatarIntID;

		// Token: 0x04005794 RID: 22420
		[Tooltip("Character that is speaking")]
		[SerializeField]
		protected Character character;

		// Token: 0x04005795 RID: 22421
		[Tooltip("Portrait that represents speaking character")]
		[SerializeField]
		protected Sprite portrait;

		// Token: 0x04005796 RID: 22422
		[Tooltip("Voiceover audio to play when writing the text")]
		[SerializeField]
		protected AudioClip voiceOverClip;

		// Token: 0x04005797 RID: 22423
		[Tooltip("Always show this Say text when the command is executed multiple times")]
		[SerializeField]
		protected bool showAlways = true;

		// Token: 0x04005798 RID: 22424
		[Tooltip("Number of times to show this Say text when the command is executed multiple times")]
		[SerializeField]
		protected int showCount = 1;

		// Token: 0x04005799 RID: 22425
		[Tooltip("Type this text in the previous dialog box.")]
		[SerializeField]
		protected bool extendPrevious;

		// Token: 0x0400579A RID: 22426
		[Tooltip("Fade out the dialog box when writing has finished and not waiting for input.")]
		[SerializeField]
		protected bool fadeWhenDone = true;

		// Token: 0x0400579B RID: 22427
		[Tooltip("Wait for player to click before continuing.")]
		[SerializeField]
		protected bool waitForClick = true;

		// Token: 0x0400579C RID: 22428
		[Tooltip("Stop playing voiceover when text finishes writing.")]
		[SerializeField]
		protected bool stopVoiceover = true;

		// Token: 0x0400579D RID: 22429
		[Tooltip("Wait for the Voice Over to complete before continuing")]
		[SerializeField]
		protected bool waitForVO;

		// Token: 0x0400579E RID: 22430
		[Tooltip("Sets the active Say dialog with a reference to a Say Dialog object in the scene. All story text will now display using this Say Dialog.")]
		[SerializeField]
		protected SayDialog setSayDialog;

		// Token: 0x0400579F RID: 22431
		protected int executionCount;
	}
}
