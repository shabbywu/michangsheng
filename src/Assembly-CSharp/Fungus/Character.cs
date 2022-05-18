using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus
{
	// Token: 0x020012C2 RID: 4802
	[ExecuteInEditMode]
	public class Character : MonoBehaviour, ILocalizable
	{
		// Token: 0x06007444 RID: 29764 RVA: 0x0004F5BC File Offset: 0x0004D7BC
		protected virtual void OnEnable()
		{
			if (!Character.activeCharacters.Contains(this))
			{
				Character.activeCharacters.Add(this);
			}
		}

		// Token: 0x06007445 RID: 29765 RVA: 0x0004F5D6 File Offset: 0x0004D7D6
		protected virtual void OnDisable()
		{
			Character.activeCharacters.Remove(this);
		}

		// Token: 0x17000A9E RID: 2718
		// (get) Token: 0x06007446 RID: 29766 RVA: 0x0004F5E4 File Offset: 0x0004D7E4
		public static List<Character> ActiveCharacters
		{
			get
			{
				return Character.activeCharacters;
			}
		}

		// Token: 0x17000A9F RID: 2719
		// (get) Token: 0x06007447 RID: 29767 RVA: 0x0004F5EB File Offset: 0x0004D7EB
		public virtual string NameText
		{
			get
			{
				return this.nameText;
			}
		}

		// Token: 0x17000AA0 RID: 2720
		// (get) Token: 0x06007448 RID: 29768 RVA: 0x0004F5F3 File Offset: 0x0004D7F3
		public virtual Color NameColor
		{
			get
			{
				return this.nameColor;
			}
		}

		// Token: 0x17000AA1 RID: 2721
		// (get) Token: 0x06007449 RID: 29769 RVA: 0x0004F5FB File Offset: 0x0004D7FB
		public virtual AudioClip SoundEffect
		{
			get
			{
				return this.soundEffect;
			}
		}

		// Token: 0x17000AA2 RID: 2722
		// (get) Token: 0x0600744A RID: 29770 RVA: 0x0004F603 File Offset: 0x0004D803
		public virtual List<Sprite> Portraits
		{
			get
			{
				return this.portraits;
			}
		}

		// Token: 0x17000AA3 RID: 2723
		// (get) Token: 0x0600744B RID: 29771 RVA: 0x0004F60B File Offset: 0x0004D80B
		public virtual FacingDirection PortraitsFace
		{
			get
			{
				return this.portraitsFace;
			}
		}

		// Token: 0x17000AA4 RID: 2724
		// (get) Token: 0x0600744C RID: 29772 RVA: 0x0004F613 File Offset: 0x0004D813
		// (set) Token: 0x0600744D RID: 29773 RVA: 0x0004F61B File Offset: 0x0004D81B
		public virtual Sprite ProfileSprite { get; set; }

		// Token: 0x17000AA5 RID: 2725
		// (get) Token: 0x0600744E RID: 29774 RVA: 0x0004F624 File Offset: 0x0004D824
		public virtual PortraitState State
		{
			get
			{
				return this.portaitState;
			}
		}

		// Token: 0x17000AA6 RID: 2726
		// (get) Token: 0x0600744F RID: 29775 RVA: 0x0004F62C File Offset: 0x0004D82C
		public virtual SayDialog SetSayDialog
		{
			get
			{
				return this.setSayDialog;
			}
		}

		// Token: 0x06007450 RID: 29776 RVA: 0x0004F634 File Offset: 0x0004D834
		public string GetObjectName()
		{
			return base.gameObject.name;
		}

		// Token: 0x06007451 RID: 29777 RVA: 0x0004F641 File Offset: 0x0004D841
		public virtual bool NameStartsWith(string matchString)
		{
			return base.name.StartsWith(matchString, true, CultureInfo.CurrentCulture) || this.nameText.StartsWith(matchString, true, CultureInfo.CurrentCulture);
		}

		// Token: 0x06007452 RID: 29778 RVA: 0x002ADD18 File Offset: 0x002ABF18
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

		// Token: 0x06007453 RID: 29779 RVA: 0x0004F5EB File Offset: 0x0004D7EB
		public virtual string GetStandardText()
		{
			return this.nameText;
		}

		// Token: 0x06007454 RID: 29780 RVA: 0x0004F66B File Offset: 0x0004D86B
		public virtual void SetStandardText(string standardText)
		{
			this.nameText = standardText;
		}

		// Token: 0x06007455 RID: 29781 RVA: 0x0004F674 File Offset: 0x0004D874
		public virtual string GetDescription()
		{
			return this.description;
		}

		// Token: 0x06007456 RID: 29782 RVA: 0x0004F67C File Offset: 0x0004D87C
		public virtual string GetStringId()
		{
			return "CHARACTER." + this.nameText;
		}

		// Token: 0x0400661D RID: 26141
		[Tooltip("Character name as displayed in Say Dialog.")]
		[SerializeField]
		protected string nameText;

		// Token: 0x0400661E RID: 26142
		[Tooltip("Color to display the character name in Say Dialog.")]
		[SerializeField]
		protected Color nameColor = Color.white;

		// Token: 0x0400661F RID: 26143
		[Tooltip("Sound effect to play when this character is speaking.")]
		[SerializeField]
		protected AudioClip soundEffect;

		// Token: 0x04006620 RID: 26144
		[Tooltip("List of portrait images that can be displayed for this character.")]
		[SerializeField]
		protected List<Sprite> portraits;

		// Token: 0x04006621 RID: 26145
		[Tooltip("Direction that portrait sprites face.")]
		[SerializeField]
		protected FacingDirection portraitsFace;

		// Token: 0x04006622 RID: 26146
		[Tooltip("Sets the active Say dialog with a reference to a Say Dialog object in the scene. This Say Dialog will be used whenever the character speaks.")]
		[SerializeField]
		protected SayDialog setSayDialog;

		// Token: 0x04006623 RID: 26147
		[FormerlySerializedAs("notes")]
		[TextArea(5, 10)]
		[SerializeField]
		protected string description;

		// Token: 0x04006624 RID: 26148
		protected PortraitState portaitState = new PortraitState();

		// Token: 0x04006625 RID: 26149
		protected static List<Character> activeCharacters = new List<Character>();
	}
}
