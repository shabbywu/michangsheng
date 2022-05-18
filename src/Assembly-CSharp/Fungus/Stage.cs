using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Fungus
{
	// Token: 0x020012F8 RID: 4856
	[ExecuteInEditMode]
	public class Stage : PortraitController
	{
		// Token: 0x06007667 RID: 30311 RVA: 0x00050955 File Offset: 0x0004EB55
		protected virtual void OnEnable()
		{
			if (!Stage.activeStages.Contains(this))
			{
				Stage.activeStages.Add(this);
			}
		}

		// Token: 0x06007668 RID: 30312 RVA: 0x0005096F File Offset: 0x0004EB6F
		protected virtual void OnDisable()
		{
			Stage.activeStages.Remove(this);
		}

		// Token: 0x06007669 RID: 30313 RVA: 0x0005097D File Offset: 0x0004EB7D
		protected virtual void Start()
		{
			if (Application.isPlaying && this.portraitCanvas != null)
			{
				this.portraitCanvas.gameObject.SetActive(true);
			}
		}

		// Token: 0x17000AFF RID: 2815
		// (get) Token: 0x0600766A RID: 30314 RVA: 0x000509A5 File Offset: 0x0004EBA5
		public static List<Stage> ActiveStages
		{
			get
			{
				return Stage.activeStages;
			}
		}

		// Token: 0x0600766B RID: 30315 RVA: 0x000509AC File Offset: 0x0004EBAC
		public static Stage GetActiveStage()
		{
			if (Stage.activeStages == null || Stage.activeStages.Count == 0)
			{
				return null;
			}
			return Stage.activeStages[0];
		}

		// Token: 0x17000B00 RID: 2816
		// (get) Token: 0x0600766C RID: 30316 RVA: 0x000509CE File Offset: 0x0004EBCE
		public virtual Canvas PortraitCanvas
		{
			get
			{
				return this.portraitCanvas;
			}
		}

		// Token: 0x17000B01 RID: 2817
		// (get) Token: 0x0600766D RID: 30317 RVA: 0x000509D6 File Offset: 0x0004EBD6
		// (set) Token: 0x0600766E RID: 30318 RVA: 0x000509DE File Offset: 0x0004EBDE
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

		// Token: 0x17000B02 RID: 2818
		// (get) Token: 0x0600766F RID: 30319 RVA: 0x000509E7 File Offset: 0x0004EBE7
		// (set) Token: 0x06007670 RID: 30320 RVA: 0x000509EF File Offset: 0x0004EBEF
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

		// Token: 0x17000B03 RID: 2819
		// (get) Token: 0x06007671 RID: 30321 RVA: 0x000509F8 File Offset: 0x0004EBF8
		// (set) Token: 0x06007672 RID: 30322 RVA: 0x00050A00 File Offset: 0x0004EC00
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

		// Token: 0x17000B04 RID: 2820
		// (get) Token: 0x06007673 RID: 30323 RVA: 0x00050A09 File Offset: 0x0004EC09
		// (set) Token: 0x06007674 RID: 30324 RVA: 0x00050A11 File Offset: 0x0004EC11
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

		// Token: 0x17000B05 RID: 2821
		// (get) Token: 0x06007675 RID: 30325 RVA: 0x00050A1A File Offset: 0x0004EC1A
		public virtual LeanTweenType FadeEaseType
		{
			get
			{
				return this.fadeEaseType;
			}
		}

		// Token: 0x17000B06 RID: 2822
		// (get) Token: 0x06007676 RID: 30326 RVA: 0x00050A22 File Offset: 0x0004EC22
		public virtual Vector2 ShiftOffset
		{
			get
			{
				return this.shiftOffset;
			}
		}

		// Token: 0x17000B07 RID: 2823
		// (get) Token: 0x06007677 RID: 30327 RVA: 0x00050A2A File Offset: 0x0004EC2A
		public virtual Image DefaultPosition
		{
			get
			{
				return this.defaultPosition;
			}
		}

		// Token: 0x17000B08 RID: 2824
		// (get) Token: 0x06007678 RID: 30328 RVA: 0x00050A32 File Offset: 0x0004EC32
		public virtual List<RectTransform> Positions
		{
			get
			{
				return this.positions;
			}
		}

		// Token: 0x17000B09 RID: 2825
		// (get) Token: 0x06007679 RID: 30329 RVA: 0x00050A3A File Offset: 0x0004EC3A
		public virtual List<Character> CharactersOnStage
		{
			get
			{
				return this.charactersOnStage;
			}
		}

		// Token: 0x0600767A RID: 30330 RVA: 0x002B354C File Offset: 0x002B174C
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

		// Token: 0x04006741 RID: 26433
		[Tooltip("Canvas object containing the stage positions.")]
		[SerializeField]
		protected Canvas portraitCanvas;

		// Token: 0x04006742 RID: 26434
		[Tooltip("Dim portraits when a character is not speaking.")]
		[SerializeField]
		protected bool dimPortraits;

		// Token: 0x04006743 RID: 26435
		[Tooltip("Choose a dimColor")]
		[SerializeField]
		protected Color dimColor = new Color(0.5f, 0.5f, 0.5f, 1f);

		// Token: 0x04006744 RID: 26436
		[Tooltip("Duration for fading character portraits in / out.")]
		[SerializeField]
		protected float fadeDuration = 0.5f;

		// Token: 0x04006745 RID: 26437
		[Tooltip("Duration for moving characters to a new position")]
		[SerializeField]
		protected float moveDuration = 1f;

		// Token: 0x04006746 RID: 26438
		[Tooltip("Ease type for the fade tween.")]
		[SerializeField]
		protected LeanTweenType fadeEaseType;

		// Token: 0x04006747 RID: 26439
		[Tooltip("Constant offset to apply to portrait position.")]
		[SerializeField]
		protected Vector2 shiftOffset;

		// Token: 0x04006748 RID: 26440
		[Tooltip("The position object where characters appear by default.")]
		[SerializeField]
		protected Image defaultPosition;

		// Token: 0x04006749 RID: 26441
		[Tooltip("List of stage position rect transforms in the stage.")]
		[SerializeField]
		protected List<RectTransform> positions;

		// Token: 0x0400674A RID: 26442
		protected List<Character> charactersOnStage = new List<Character>();

		// Token: 0x0400674B RID: 26443
		protected static List<Stage> activeStages = new List<Stage>();
	}
}
