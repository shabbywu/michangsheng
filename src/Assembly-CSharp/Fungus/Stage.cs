using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Fungus
{
	// Token: 0x02000E87 RID: 3719
	[ExecuteInEditMode]
	public class Stage : PortraitController
	{
		// Token: 0x06006968 RID: 26984 RVA: 0x002910A4 File Offset: 0x0028F2A4
		protected virtual void OnEnable()
		{
			if (!Stage.activeStages.Contains(this))
			{
				Stage.activeStages.Add(this);
			}
		}

		// Token: 0x06006969 RID: 26985 RVA: 0x002910BE File Offset: 0x0028F2BE
		protected virtual void OnDisable()
		{
			Stage.activeStages.Remove(this);
		}

		// Token: 0x0600696A RID: 26986 RVA: 0x002910CC File Offset: 0x0028F2CC
		protected virtual void Start()
		{
			if (Application.isPlaying && this.portraitCanvas != null)
			{
				this.portraitCanvas.gameObject.SetActive(true);
			}
		}

		// Token: 0x17000880 RID: 2176
		// (get) Token: 0x0600696B RID: 26987 RVA: 0x002910F4 File Offset: 0x0028F2F4
		public static List<Stage> ActiveStages
		{
			get
			{
				return Stage.activeStages;
			}
		}

		// Token: 0x0600696C RID: 26988 RVA: 0x002910FB File Offset: 0x0028F2FB
		public static Stage GetActiveStage()
		{
			if (Stage.activeStages == null || Stage.activeStages.Count == 0)
			{
				return null;
			}
			return Stage.activeStages[0];
		}

		// Token: 0x17000881 RID: 2177
		// (get) Token: 0x0600696D RID: 26989 RVA: 0x0029111D File Offset: 0x0028F31D
		public virtual Canvas PortraitCanvas
		{
			get
			{
				return this.portraitCanvas;
			}
		}

		// Token: 0x17000882 RID: 2178
		// (get) Token: 0x0600696E RID: 26990 RVA: 0x00291125 File Offset: 0x0028F325
		// (set) Token: 0x0600696F RID: 26991 RVA: 0x0029112D File Offset: 0x0028F32D
		public virtual bool DimPortraits
		{
			get
			{
				return this.dimPortraits;
			}
			set
			{
				this.dimPortraits = value;
			}
		}

		// Token: 0x17000883 RID: 2179
		// (get) Token: 0x06006970 RID: 26992 RVA: 0x00291136 File Offset: 0x0028F336
		// (set) Token: 0x06006971 RID: 26993 RVA: 0x0029113E File Offset: 0x0028F33E
		public virtual Color DimColor
		{
			get
			{
				return this.dimColor;
			}
			set
			{
				this.dimColor = value;
			}
		}

		// Token: 0x17000884 RID: 2180
		// (get) Token: 0x06006972 RID: 26994 RVA: 0x00291147 File Offset: 0x0028F347
		// (set) Token: 0x06006973 RID: 26995 RVA: 0x0029114F File Offset: 0x0028F34F
		public virtual float FadeDuration
		{
			get
			{
				return this.fadeDuration;
			}
			set
			{
				this.fadeDuration = value;
			}
		}

		// Token: 0x17000885 RID: 2181
		// (get) Token: 0x06006974 RID: 26996 RVA: 0x00291158 File Offset: 0x0028F358
		// (set) Token: 0x06006975 RID: 26997 RVA: 0x00291160 File Offset: 0x0028F360
		public virtual float MoveDuration
		{
			get
			{
				return this.moveDuration;
			}
			set
			{
				this.moveDuration = value;
			}
		}

		// Token: 0x17000886 RID: 2182
		// (get) Token: 0x06006976 RID: 26998 RVA: 0x00291169 File Offset: 0x0028F369
		public virtual LeanTweenType FadeEaseType
		{
			get
			{
				return this.fadeEaseType;
			}
		}

		// Token: 0x17000887 RID: 2183
		// (get) Token: 0x06006977 RID: 26999 RVA: 0x00291171 File Offset: 0x0028F371
		public virtual Vector2 ShiftOffset
		{
			get
			{
				return this.shiftOffset;
			}
		}

		// Token: 0x17000888 RID: 2184
		// (get) Token: 0x06006978 RID: 27000 RVA: 0x00291179 File Offset: 0x0028F379
		public virtual Image DefaultPosition
		{
			get
			{
				return this.defaultPosition;
			}
		}

		// Token: 0x17000889 RID: 2185
		// (get) Token: 0x06006979 RID: 27001 RVA: 0x00291181 File Offset: 0x0028F381
		public virtual List<RectTransform> Positions
		{
			get
			{
				return this.positions;
			}
		}

		// Token: 0x1700088A RID: 2186
		// (get) Token: 0x0600697A RID: 27002 RVA: 0x00291189 File Offset: 0x0028F389
		public virtual List<Character> CharactersOnStage
		{
			get
			{
				return this.charactersOnStage;
			}
		}

		// Token: 0x0600697B RID: 27003 RVA: 0x00291194 File Offset: 0x0028F394
		public RectTransform GetPosition(string positionString)
		{
			if (string.IsNullOrEmpty(positionString))
			{
				return null;
			}
			for (int i = 0; i < this.positions.Count; i++)
			{
				if (string.Compare(this.positions[i].name, positionString, true) == 0)
				{
					return this.positions[i];
				}
			}
			return null;
		}

		// Token: 0x04005960 RID: 22880
		[Tooltip("Canvas object containing the stage positions.")]
		[SerializeField]
		protected Canvas portraitCanvas;

		// Token: 0x04005961 RID: 22881
		[Tooltip("Dim portraits when a character is not speaking.")]
		[SerializeField]
		protected bool dimPortraits;

		// Token: 0x04005962 RID: 22882
		[Tooltip("Choose a dimColor")]
		[SerializeField]
		protected Color dimColor = new Color(0.5f, 0.5f, 0.5f, 1f);

		// Token: 0x04005963 RID: 22883
		[Tooltip("Duration for fading character portraits in / out.")]
		[SerializeField]
		protected float fadeDuration = 0.5f;

		// Token: 0x04005964 RID: 22884
		[Tooltip("Duration for moving characters to a new position")]
		[SerializeField]
		protected float moveDuration = 1f;

		// Token: 0x04005965 RID: 22885
		[Tooltip("Ease type for the fade tween.")]
		[SerializeField]
		protected LeanTweenType fadeEaseType;

		// Token: 0x04005966 RID: 22886
		[Tooltip("Constant offset to apply to portrait position.")]
		[SerializeField]
		protected Vector2 shiftOffset;

		// Token: 0x04005967 RID: 22887
		[Tooltip("The position object where characters appear by default.")]
		[SerializeField]
		protected Image defaultPosition;

		// Token: 0x04005968 RID: 22888
		[Tooltip("List of stage position rect transforms in the stage.")]
		[SerializeField]
		protected List<RectTransform> positions;

		// Token: 0x04005969 RID: 22889
		protected List<Character> charactersOnStage = new List<Character>();

		// Token: 0x0400596A RID: 22890
		protected static List<Stage> activeStages = new List<Stage>();
	}
}
