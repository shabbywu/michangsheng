using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000E11 RID: 3601
	[CommandInfo("Narrative", "Portrait", "Controls a character portrait.", 0)]
	public class Portrait : ControlWithDisplay<DisplayType>
	{
		// Token: 0x17000807 RID: 2055
		// (get) Token: 0x060065A4 RID: 26020 RVA: 0x00283B3E File Offset: 0x00281D3E
		// (set) Token: 0x060065A5 RID: 26021 RVA: 0x00283B46 File Offset: 0x00281D46
		public virtual Stage _Stage
		{
			get
			{
				return this.stage;
			}
			set
			{
				this.stage = value;
			}
		}

		// Token: 0x17000808 RID: 2056
		// (get) Token: 0x060065A6 RID: 26022 RVA: 0x00283B4F File Offset: 0x00281D4F
		// (set) Token: 0x060065A7 RID: 26023 RVA: 0x00283B57 File Offset: 0x00281D57
		public virtual Character _Character
		{
			get
			{
				return this.character;
			}
			set
			{
				this.character = value;
			}
		}

		// Token: 0x17000809 RID: 2057
		// (get) Token: 0x060065A8 RID: 26024 RVA: 0x00283B60 File Offset: 0x00281D60
		// (set) Token: 0x060065A9 RID: 26025 RVA: 0x00283B68 File Offset: 0x00281D68
		public virtual Sprite _Portrait
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

		// Token: 0x1700080A RID: 2058
		// (get) Token: 0x060065AA RID: 26026 RVA: 0x00283B71 File Offset: 0x00281D71
		// (set) Token: 0x060065AB RID: 26027 RVA: 0x00283B79 File Offset: 0x00281D79
		public virtual PositionOffset Offset
		{
			get
			{
				return this.offset;
			}
			set
			{
				this.offset = value;
			}
		}

		// Token: 0x1700080B RID: 2059
		// (get) Token: 0x060065AC RID: 26028 RVA: 0x00283B82 File Offset: 0x00281D82
		// (set) Token: 0x060065AD RID: 26029 RVA: 0x00283B8A File Offset: 0x00281D8A
		public virtual RectTransform FromPosition
		{
			get
			{
				return this.fromPosition;
			}
			set
			{
				this.fromPosition = value;
			}
		}

		// Token: 0x1700080C RID: 2060
		// (get) Token: 0x060065AE RID: 26030 RVA: 0x00283B93 File Offset: 0x00281D93
		// (set) Token: 0x060065AF RID: 26031 RVA: 0x00283B9B File Offset: 0x00281D9B
		public virtual RectTransform ToPosition
		{
			get
			{
				return this.toPosition;
			}
			set
			{
				this.toPosition = value;
			}
		}

		// Token: 0x1700080D RID: 2061
		// (get) Token: 0x060065B0 RID: 26032 RVA: 0x00283BA4 File Offset: 0x00281DA4
		// (set) Token: 0x060065B1 RID: 26033 RVA: 0x00283BAC File Offset: 0x00281DAC
		public virtual FacingDirection Facing
		{
			get
			{
				return this.facing;
			}
			set
			{
				this.facing = value;
			}
		}

		// Token: 0x1700080E RID: 2062
		// (get) Token: 0x060065B2 RID: 26034 RVA: 0x00283BB5 File Offset: 0x00281DB5
		// (set) Token: 0x060065B3 RID: 26035 RVA: 0x00283BBD File Offset: 0x00281DBD
		public virtual bool UseDefaultSettings
		{
			get
			{
				return this.useDefaultSettings;
			}
			set
			{
				this.useDefaultSettings = value;
			}
		}

		// Token: 0x1700080F RID: 2063
		// (get) Token: 0x060065B4 RID: 26036 RVA: 0x00283BC6 File Offset: 0x00281DC6
		// (set) Token: 0x060065B5 RID: 26037 RVA: 0x00283BCE File Offset: 0x00281DCE
		public virtual bool Move
		{
			get
			{
				return this.move;
			}
			set
			{
				this.move = value;
			}
		}

		// Token: 0x17000810 RID: 2064
		// (get) Token: 0x060065B6 RID: 26038 RVA: 0x00283BD7 File Offset: 0x00281DD7
		// (set) Token: 0x060065B7 RID: 26039 RVA: 0x00283BDF File Offset: 0x00281DDF
		public virtual bool ShiftIntoPlace
		{
			get
			{
				return this.shiftIntoPlace;
			}
			set
			{
				this.shiftIntoPlace = value;
			}
		}

		// Token: 0x060065B8 RID: 26040 RVA: 0x00283BE8 File Offset: 0x00281DE8
		public override void OnEnter()
		{
			if (this.stage == null)
			{
				this.stage = Object.FindObjectOfType<Stage>();
				if (this.stage == null)
				{
					this.Continue();
					return;
				}
			}
			if (this.IsDisplayNone<DisplayType>(this.display))
			{
				this.Continue();
				return;
			}
			PortraitOptions portraitOptions = new PortraitOptions(true);
			portraitOptions.character = this.character;
			portraitOptions.replacedCharacter = this.replacedCharacter;
			portraitOptions.portrait = this.portrait;
			portraitOptions.display = this.display;
			portraitOptions.offset = this.offset;
			portraitOptions.fromPosition = this.fromPosition;
			portraitOptions.toPosition = this.toPosition;
			portraitOptions.facing = this.facing;
			portraitOptions.useDefaultSettings = this.useDefaultSettings;
			portraitOptions.fadeDuration = this.fadeDuration;
			portraitOptions.moveDuration = this.moveDuration;
			portraitOptions.shiftOffset = this.shiftOffset;
			portraitOptions.move = this.move;
			portraitOptions.shiftIntoPlace = this.shiftIntoPlace;
			portraitOptions.waitUntilFinished = this.waitUntilFinished;
			this.stage.RunPortraitCommand(portraitOptions, new Action(this.Continue));
		}

		// Token: 0x060065B9 RID: 26041 RVA: 0x00283D0C File Offset: 0x00281F0C
		public override string GetSummary()
		{
			if (this.display == DisplayType.None && this.character == null)
			{
				return "Error: No character or display selected";
			}
			if (this.display == DisplayType.None)
			{
				return "Error: No display selected";
			}
			if (this.character == null)
			{
				return "Error: No character selected";
			}
			string text = "";
			string text2 = "";
			string text3 = "";
			string text4 = "";
			string text5 = "";
			string text6 = StringFormatter.SplitCamelCase(this.display.ToString());
			if (this.display == DisplayType.Replace && this.replacedCharacter != null)
			{
				text6 = text6 + " \"" + this.replacedCharacter.name + "\" with";
			}
			string name = this.character.name;
			if (this.stage != null)
			{
				text3 = " on \"" + this.stage.name + "\"";
			}
			if (this.portrait != null)
			{
				text4 = " " + this.portrait.name;
			}
			if (this.shiftIntoPlace)
			{
				if (this.offset != PositionOffset.None)
				{
					text = this.offset.ToString();
					text = " from \"" + text + "\"";
				}
			}
			else if (this.fromPosition != null)
			{
				text = " from \"" + this.fromPosition.name + "\"";
			}
			if (this.toPosition != null)
			{
				string str;
				if (this.move)
				{
					str = " to ";
				}
				else
				{
					str = " at ";
				}
				text2 = str + "\"" + this.toPosition.name + "\"";
			}
			if (this.facing != FacingDirection.None)
			{
				if (this.facing == FacingDirection.Left)
				{
					text5 = "<--";
				}
				if (this.facing == FacingDirection.Right)
				{
					text5 = "-->";
				}
				text5 = " facing \"" + text5 + "\"";
			}
			return string.Concat(new string[]
			{
				text6,
				" \"",
				name,
				text4,
				"\"",
				text3,
				text5,
				text,
				text2
			});
		}

		// Token: 0x060065BA RID: 26042 RVA: 0x0027E190 File Offset: 0x0027C390
		public override Color GetButtonColor()
		{
			return new Color32(230, 200, 250, byte.MaxValue);
		}

		// Token: 0x060065BB RID: 26043 RVA: 0x00283F47 File Offset: 0x00282147
		public override void OnCommandAdded(Block parentBlock)
		{
			this.display = DisplayType.Show;
		}

		// Token: 0x04005747 RID: 22343
		[Tooltip("Stage to display portrait on")]
		[SerializeField]
		protected Stage stage;

		// Token: 0x04005748 RID: 22344
		[Tooltip("Character to display")]
		[SerializeField]
		protected Character character;

		// Token: 0x04005749 RID: 22345
		[Tooltip("Character to swap with")]
		[SerializeField]
		protected Character replacedCharacter;

		// Token: 0x0400574A RID: 22346
		[Tooltip("Portrait to display")]
		[SerializeField]
		protected Sprite portrait;

		// Token: 0x0400574B RID: 22347
		[Tooltip("Move the portrait from/to this offset position")]
		[SerializeField]
		protected PositionOffset offset;

		// Token: 0x0400574C RID: 22348
		[Tooltip("Move the portrait from this position")]
		[SerializeField]
		protected RectTransform fromPosition;

		// Token: 0x0400574D RID: 22349
		[Tooltip("Move the portrait to this position")]
		[SerializeField]
		protected RectTransform toPosition;

		// Token: 0x0400574E RID: 22350
		[Tooltip("Direction character is facing")]
		[SerializeField]
		protected FacingDirection facing;

		// Token: 0x0400574F RID: 22351
		[Tooltip("Use Default Settings")]
		[SerializeField]
		protected bool useDefaultSettings = true;

		// Token: 0x04005750 RID: 22352
		[Tooltip("Fade Duration")]
		[SerializeField]
		protected float fadeDuration = 0.5f;

		// Token: 0x04005751 RID: 22353
		[Tooltip("Movement Duration")]
		[SerializeField]
		protected float moveDuration = 1f;

		// Token: 0x04005752 RID: 22354
		[Tooltip("Shift Offset")]
		[SerializeField]
		protected Vector2 shiftOffset;

		// Token: 0x04005753 RID: 22355
		[Tooltip("Move portrait into new position")]
		[SerializeField]
		protected bool move;

		// Token: 0x04005754 RID: 22356
		[Tooltip("Start from offset position")]
		[SerializeField]
		protected bool shiftIntoPlace;

		// Token: 0x04005755 RID: 22357
		[Tooltip("Wait until the tween has finished before executing the next command")]
		[SerializeField]
		protected bool waitUntilFinished;
	}
}
