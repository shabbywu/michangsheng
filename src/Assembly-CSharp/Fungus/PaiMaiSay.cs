using System;
using System.Collections.Generic;
using KBEngine;
using PaiMai;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020013C8 RID: 5064
	[CommandInfo("PaiMai", "PaiMaiSay", "Writes text in a dialog box.", 0)]
	[AddComponentMenu("")]
	public class PaiMaiSay : Command, ILocalizable
	{
		// Token: 0x17000B98 RID: 2968
		// (set) Token: 0x06007B72 RID: 31602 RVA: 0x00054245 File Offset: 0x00052445
		public StartFight.MonstarType pubAvatarIDSetType
		{
			set
			{
				this.AvatarIDSetType = value;
			}
		}

		// Token: 0x17000B99 RID: 2969
		// (set) Token: 0x06007B73 RID: 31603 RVA: 0x0005424E File Offset: 0x0005244E
		public bool SetfadeWhenDone
		{
			set
			{
				this.fadeWhenDone = value;
			}
		}

		// Token: 0x17000B9A RID: 2970
		// (set) Token: 0x06007B74 RID: 31604 RVA: 0x00054257 File Offset: 0x00052457
		public bool SetwaitForClick
		{
			set
			{
				this.waitForClick = value;
			}
		}

		// Token: 0x17000B9B RID: 2971
		// (get) Token: 0x06007B75 RID: 31605 RVA: 0x00054260 File Offset: 0x00052460
		public virtual Character _Character
		{
			get
			{
				return this.character;
			}
		}

		// Token: 0x17000B9C RID: 2972
		// (get) Token: 0x06007B76 RID: 31606 RVA: 0x00054268 File Offset: 0x00052468
		public virtual StartFight.MonstarType _AvatarIDSetType
		{
			get
			{
				return this.AvatarIDSetType;
			}
		}

		// Token: 0x17000B9D RID: 2973
		// (get) Token: 0x06007B77 RID: 31607 RVA: 0x00054270 File Offset: 0x00052470
		// (set) Token: 0x06007B78 RID: 31608 RVA: 0x00054278 File Offset: 0x00052478
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

		// Token: 0x17000B9E RID: 2974
		// (get) Token: 0x06007B79 RID: 31609 RVA: 0x00054281 File Offset: 0x00052481
		public virtual bool ExtendPrevious
		{
			get
			{
				return this.extendPrevious;
			}
		}

		// Token: 0x06007B7A RID: 31610 RVA: 0x002C3B64 File Offset: 0x002C1D64
		public override void OnEnter()
		{
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
			PaiMaiSayData paiMaiSayData = (PaiMaiSayData)BindData.Get("PaiMaiSayData");
			int id = paiMaiSayData.Id;
			this.storyText = paiMaiSayData.Msg;
			Action onComplete = paiMaiSayData.Action;
			sayDialog.SetCharacter(this.character, id);
			sayDialog.SetCharacterImage(this.portrait, id);
			Avatar player = Tools.instance.getPlayer();
			this.storyText = this.storyText.Replace("{LastName}", player.lastName).Replace("{FirstName}", player.firstName);
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
				if (onComplete != null)
				{
					onComplete();
				}
				this.Continue();
			});
		}

		// Token: 0x06007B7B RID: 31611 RVA: 0x002C3D6C File Offset: 0x002C1F6C
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

		// Token: 0x06007B7C RID: 31612 RVA: 0x0004E1CA File Offset: 0x0004C3CA
		public Say Clone()
		{
			return base.MemberwiseClone() as Say;
		}

		// Token: 0x06007B7D RID: 31613 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06007B7E RID: 31614 RVA: 0x00054289 File Offset: 0x00052489
		public override void OnReset()
		{
			this.executionCount = 0;
		}

		// Token: 0x06007B7F RID: 31615 RVA: 0x002A8FFC File Offset: 0x002A71FC
		public override void OnStopExecuting()
		{
			SayDialog sayDialog = SayDialog.GetSayDialog();
			if (sayDialog == null)
			{
				return;
			}
			sayDialog.Stop();
		}

		// Token: 0x06007B80 RID: 31616 RVA: 0x00054292 File Offset: 0x00052492
		public virtual string GetStandardText()
		{
			return this.storyText;
		}

		// Token: 0x06007B81 RID: 31617 RVA: 0x0005429A File Offset: 0x0005249A
		public virtual void SetStandardText(string standardText)
		{
			this.storyText = standardText;
		}

		// Token: 0x06007B82 RID: 31618 RVA: 0x000542A3 File Offset: 0x000524A3
		public virtual string GetDescription()
		{
			return this.description;
		}

		// Token: 0x06007B83 RID: 31619 RVA: 0x002C3DC8 File Offset: 0x002C1FC8
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

		// Token: 0x040069F9 RID: 27129
		[TextArea(5, 10)]
		[SerializeField]
		protected string storyText = "";

		// Token: 0x040069FA RID: 27130
		[Tooltip("Notes about this story text for other authors, localization, etc.")]
		[SerializeField]
		protected string description = "";

		// Token: 0x040069FB RID: 27131
		[Tooltip("设置对话ID的方式")]
		[SerializeField]
		protected StartFight.MonstarType AvatarIDSetType = StartFight.MonstarType.FungusVariable;

		// Token: 0x040069FC RID: 27132
		[Tooltip("Character that is speaking")]
		[SerializeField]
		protected Character character;

		// Token: 0x040069FD RID: 27133
		[Tooltip("Portrait that represents speaking character")]
		[SerializeField]
		protected Sprite portrait;

		// Token: 0x040069FE RID: 27134
		[Tooltip("Voiceover audio to play when writing the text")]
		[SerializeField]
		protected AudioClip voiceOverClip;

		// Token: 0x040069FF RID: 27135
		[Tooltip("Always show this Say text when the command is executed multiple times")]
		[SerializeField]
		protected bool showAlways = true;

		// Token: 0x04006A00 RID: 27136
		[Tooltip("Number of times to show this Say text when the command is executed multiple times")]
		[SerializeField]
		protected int showCount = 1;

		// Token: 0x04006A01 RID: 27137
		[Tooltip("Type this text in the previous dialog box.")]
		[SerializeField]
		protected bool extendPrevious;

		// Token: 0x04006A02 RID: 27138
		[Tooltip("Fade out the dialog box when writing has finished and not waiting for input.")]
		[SerializeField]
		protected bool fadeWhenDone = true;

		// Token: 0x04006A03 RID: 27139
		[Tooltip("Wait for player to click before continuing.")]
		[SerializeField]
		protected bool waitForClick = true;

		// Token: 0x04006A04 RID: 27140
		[Tooltip("Stop playing voiceover when text finishes writing.")]
		[SerializeField]
		protected bool stopVoiceover = true;

		// Token: 0x04006A05 RID: 27141
		[Tooltip("Wait for the Voice Over to complete before continuing")]
		[SerializeField]
		protected bool waitForVO;

		// Token: 0x04006A06 RID: 27142
		[Tooltip("Sets the active Say dialog with a reference to a Say Dialog object in the scene. All story text will now display using this Say Dialog.")]
		[SerializeField]
		protected SayDialog setSayDialog;

		// Token: 0x04006A07 RID: 27143
		protected int executionCount;
	}
}
