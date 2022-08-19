using System;
using System.Collections.Generic;
using KBEngine;
using PaiMai;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F15 RID: 3861
	[CommandInfo("PaiMai", "PaiMaiSay", "Writes text in a dialog box.", 0)]
	[AddComponentMenu("")]
	public class PaiMaiSay : Command, ILocalizable
	{
		// Token: 0x170008EB RID: 2283
		// (set) Token: 0x06006D89 RID: 28041 RVA: 0x002A3715 File Offset: 0x002A1915
		public StartFight.MonstarType pubAvatarIDSetType
		{
			set
			{
				this.AvatarIDSetType = value;
			}
		}

		// Token: 0x170008EC RID: 2284
		// (set) Token: 0x06006D8A RID: 28042 RVA: 0x002A371E File Offset: 0x002A191E
		public bool SetfadeWhenDone
		{
			set
			{
				this.fadeWhenDone = value;
			}
		}

		// Token: 0x170008ED RID: 2285
		// (set) Token: 0x06006D8B RID: 28043 RVA: 0x002A3727 File Offset: 0x002A1927
		public bool SetwaitForClick
		{
			set
			{
				this.waitForClick = value;
			}
		}

		// Token: 0x170008EE RID: 2286
		// (get) Token: 0x06006D8C RID: 28044 RVA: 0x002A3730 File Offset: 0x002A1930
		public virtual Character _Character
		{
			get
			{
				return this.character;
			}
		}

		// Token: 0x170008EF RID: 2287
		// (get) Token: 0x06006D8D RID: 28045 RVA: 0x002A3738 File Offset: 0x002A1938
		public virtual StartFight.MonstarType _AvatarIDSetType
		{
			get
			{
				return this.AvatarIDSetType;
			}
		}

		// Token: 0x170008F0 RID: 2288
		// (get) Token: 0x06006D8E RID: 28046 RVA: 0x002A3740 File Offset: 0x002A1940
		// (set) Token: 0x06006D8F RID: 28047 RVA: 0x002A3748 File Offset: 0x002A1948
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

		// Token: 0x170008F1 RID: 2289
		// (get) Token: 0x06006D90 RID: 28048 RVA: 0x002A3751 File Offset: 0x002A1951
		public virtual bool ExtendPrevious
		{
			get
			{
				return this.extendPrevious;
			}
		}

		// Token: 0x06006D91 RID: 28049 RVA: 0x002A375C File Offset: 0x002A195C
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

		// Token: 0x06006D92 RID: 28050 RVA: 0x002A3964 File Offset: 0x002A1B64
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

		// Token: 0x06006D93 RID: 28051 RVA: 0x002856B3 File Offset: 0x002838B3
		public Say Clone()
		{
			return base.MemberwiseClone() as Say;
		}

		// Token: 0x06006D94 RID: 28052 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06006D95 RID: 28053 RVA: 0x002A39BF File Offset: 0x002A1BBF
		public override void OnReset()
		{
			this.executionCount = 0;
		}

		// Token: 0x06006D96 RID: 28054 RVA: 0x002A39C8 File Offset: 0x002A1BC8
		public override void OnStopExecuting()
		{
			SayDialog sayDialog = SayDialog.GetSayDialog();
			if (sayDialog == null)
			{
				return;
			}
			sayDialog.Stop();
		}

		// Token: 0x06006D97 RID: 28055 RVA: 0x002A39EB File Offset: 0x002A1BEB
		public virtual string GetStandardText()
		{
			return this.storyText;
		}

		// Token: 0x06006D98 RID: 28056 RVA: 0x002A39F3 File Offset: 0x002A1BF3
		public virtual void SetStandardText(string standardText)
		{
			this.storyText = standardText;
		}

		// Token: 0x06006D99 RID: 28057 RVA: 0x002A39FC File Offset: 0x002A1BFC
		public virtual string GetDescription()
		{
			return this.description;
		}

		// Token: 0x06006D9A RID: 28058 RVA: 0x002A3A04 File Offset: 0x002A1C04
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

		// Token: 0x04005B32 RID: 23346
		[TextArea(5, 10)]
		[SerializeField]
		protected string storyText = "";

		// Token: 0x04005B33 RID: 23347
		[Tooltip("Notes about this story text for other authors, localization, etc.")]
		[SerializeField]
		protected string description = "";

		// Token: 0x04005B34 RID: 23348
		[Tooltip("设置对话ID的方式")]
		[SerializeField]
		protected StartFight.MonstarType AvatarIDSetType = StartFight.MonstarType.FungusVariable;

		// Token: 0x04005B35 RID: 23349
		[Tooltip("Character that is speaking")]
		[SerializeField]
		protected Character character;

		// Token: 0x04005B36 RID: 23350
		[Tooltip("Portrait that represents speaking character")]
		[SerializeField]
		protected Sprite portrait;

		// Token: 0x04005B37 RID: 23351
		[Tooltip("Voiceover audio to play when writing the text")]
		[SerializeField]
		protected AudioClip voiceOverClip;

		// Token: 0x04005B38 RID: 23352
		[Tooltip("Always show this Say text when the command is executed multiple times")]
		[SerializeField]
		protected bool showAlways = true;

		// Token: 0x04005B39 RID: 23353
		[Tooltip("Number of times to show this Say text when the command is executed multiple times")]
		[SerializeField]
		protected int showCount = 1;

		// Token: 0x04005B3A RID: 23354
		[Tooltip("Type this text in the previous dialog box.")]
		[SerializeField]
		protected bool extendPrevious;

		// Token: 0x04005B3B RID: 23355
		[Tooltip("Fade out the dialog box when writing has finished and not waiting for input.")]
		[SerializeField]
		protected bool fadeWhenDone = true;

		// Token: 0x04005B3C RID: 23356
		[Tooltip("Wait for player to click before continuing.")]
		[SerializeField]
		protected bool waitForClick = true;

		// Token: 0x04005B3D RID: 23357
		[Tooltip("Stop playing voiceover when text finishes writing.")]
		[SerializeField]
		protected bool stopVoiceover = true;

		// Token: 0x04005B3E RID: 23358
		[Tooltip("Wait for the Voice Over to complete before continuing")]
		[SerializeField]
		protected bool waitForVO;

		// Token: 0x04005B3F RID: 23359
		[Tooltip("Sets the active Say dialog with a reference to a Say Dialog object in the scene. All story text will now display using this Say Dialog.")]
		[SerializeField]
		protected SayDialog setSayDialog;

		// Token: 0x04005B40 RID: 23360
		protected int executionCount;
	}
}
