using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus
{
	// Token: 0x02000E65 RID: 3685
	[ExecuteInEditMode]
	public class Character : MonoBehaviour, ILocalizable
	{
		// Token: 0x06006792 RID: 26514 RVA: 0x0028ADC8 File Offset: 0x00288FC8
		protected virtual void OnEnable()
		{
			if (!Character.activeCharacters.Contains(this))
			{
				Character.activeCharacters.Add(this);
			}
		}

		// Token: 0x06006793 RID: 26515 RVA: 0x0028ADE2 File Offset: 0x00288FE2
		protected virtual void OnDisable()
		{
			Character.activeCharacters.Remove(this);
		}

		// Token: 0x1700082D RID: 2093
		// (get) Token: 0x06006794 RID: 26516 RVA: 0x0028ADF0 File Offset: 0x00288FF0
		public static List<Character> ActiveCharacters
		{
			get
			{
				return Character.activeCharacters;
			}
		}

		// Token: 0x1700082E RID: 2094
		// (get) Token: 0x06006795 RID: 26517 RVA: 0x0028ADF7 File Offset: 0x00288FF7
		public virtual string NameText
		{
			get
			{
				return this.nameText;
			}
		}

		// Token: 0x1700082F RID: 2095
		// (get) Token: 0x06006796 RID: 26518 RVA: 0x0028ADFF File Offset: 0x00288FFF
		public virtual Color NameColor
		{
			get
			{
				return this.nameColor;
			}
		}

		// Token: 0x17000830 RID: 2096
		// (get) Token: 0x06006797 RID: 26519 RVA: 0x0028AE07 File Offset: 0x00289007
		public virtual AudioClip SoundEffect
		{
			get
			{
				return this.soundEffect;
			}
		}

		// Token: 0x17000831 RID: 2097
		// (get) Token: 0x06006798 RID: 26520 RVA: 0x0028AE0F File Offset: 0x0028900F
		public virtual List<Sprite> Portraits
		{
			get
			{
				return this.portraits;
			}
		}

		// Token: 0x17000832 RID: 2098
		// (get) Token: 0x06006799 RID: 26521 RVA: 0x0028AE17 File Offset: 0x00289017
		public virtual FacingDirection PortraitsFace
		{
			get
			{
				return this.portraitsFace;
			}
		}

		// Token: 0x17000833 RID: 2099
		// (get) Token: 0x0600679A RID: 26522 RVA: 0x0028AE1F File Offset: 0x0028901F
		// (set) Token: 0x0600679B RID: 26523 RVA: 0x0028AE27 File Offset: 0x00289027
		public virtual Sprite ProfileSprite { get; set; }

		// Token: 0x17000834 RID: 2100
		// (get) Token: 0x0600679C RID: 26524 RVA: 0x0028AE30 File Offset: 0x00289030
		public virtual PortraitState State
		{
			get
			{
				return this.portaitState;
			}
		}

		// Token: 0x17000835 RID: 2101
		// (get) Token: 0x0600679D RID: 26525 RVA: 0x0028AE38 File Offset: 0x00289038
		public virtual SayDialog SetSayDialog
		{
			get
			{
				return this.setSayDialog;
			}
		}

		// Token: 0x0600679E RID: 26526 RVA: 0x0028AE40 File Offset: 0x00289040
		public string GetObjectName()
		{
			return base.gameObject.name;
		}

		// Token: 0x0600679F RID: 26527 RVA: 0x0028AE4D File Offset: 0x0028904D
		public virtual bool NameStartsWith(string matchString)
		{
			return base.name.StartsWith(matchString, true, CultureInfo.CurrentCulture) || this.nameText.StartsWith(matchString, true, CultureInfo.CurrentCulture);
		}

		// Token: 0x060067A0 RID: 26528 RVA: 0x0028AE78 File Offset: 0x00289078
		public virtual Sprite GetPortrait(string portraitString)
		{
			if (string.IsNullOrEmpty(portraitString))
			{
				return null;
			}
			for (int i = 0; i < this.portraits.Count; i++)
			{
				if (this.portraits[i] != null && string.Compare(this.portraits[i].name, portraitString, true) == 0)
				{
					return this.portraits[i];
				}
			}
			return null;
		}

		// Token: 0x060067A1 RID: 26529 RVA: 0x0028ADF7 File Offset: 0x00288FF7
		public virtual string GetStandardText()
		{
			return this.nameText;
		}

		// Token: 0x060067A2 RID: 26530 RVA: 0x0028AEE1 File Offset: 0x002890E1
		public virtual void SetStandardText(string standardText)
		{
			this.nameText = standardText;
		}

		// Token: 0x060067A3 RID: 26531 RVA: 0x0028AEEA File Offset: 0x002890EA
		public virtual string GetDescription()
		{
			return this.description;
		}

		// Token: 0x060067A4 RID: 26532 RVA: 0x0028AEF2 File Offset: 0x002890F2
		public virtual string GetStringId()
		{
			return "CHARACTER." + this.nameText;
		}

		// Token: 0x04005885 RID: 22661
		[Tooltip("Character name as displayed in Say Dialog.")]
		[SerializeField]
		protected string nameText;

		// Token: 0x04005886 RID: 22662
		[Tooltip("Color to display the character name in Say Dialog.")]
		[SerializeField]
		protected Color nameColor = Color.white;

		// Token: 0x04005887 RID: 22663
		[Tooltip("Sound effect to play when this character is speaking.")]
		[SerializeField]
		protected AudioClip soundEffect;

		// Token: 0x04005888 RID: 22664
		[Tooltip("List of portrait images that can be displayed for this character.")]
		[SerializeField]
		protected List<Sprite> portraits;

		// Token: 0x04005889 RID: 22665
		[Tooltip("Direction that portrait sprites face.")]
		[SerializeField]
		protected FacingDirection portraitsFace;

		// Token: 0x0400588A RID: 22666
		[Tooltip("Sets the active Say dialog with a reference to a Say Dialog object in the scene. This Say Dialog will be used whenever the character speaks.")]
		[SerializeField]
		protected SayDialog setSayDialog;

		// Token: 0x0400588B RID: 22667
		[FormerlySerializedAs("notes")]
		[TextArea(5, 10)]
		[SerializeField]
		protected string description;

		// Token: 0x0400588C RID: 22668
		protected PortraitState portaitState = new PortraitState();

		// Token: 0x0400588D RID: 22669
		protected static List<Character> activeCharacters = new List<Character>();
	}
}
