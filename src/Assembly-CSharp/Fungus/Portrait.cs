using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x0200125E RID: 4702
	[CommandInfo("Narrative", "Portrait", "Controls a character portrait.", 0)]
	public class Portrait : ControlWithDisplay<DisplayType>
	{
		// Token: 0x17000A6E RID: 2670
		// (get) Token: 0x06007232 RID: 29234 RVA: 0x0004DBB1 File Offset: 0x0004BDB1
		// (set) Token: 0x06007233 RID: 29235 RVA: 0x0004DBB9 File Offset: 0x0004BDB9
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

		// Token: 0x17000A6F RID: 2671
		// (get) Token: 0x06007234 RID: 29236 RVA: 0x0004DBC2 File Offset: 0x0004BDC2
		// (set) Token: 0x06007235 RID: 29237 RVA: 0x0004DBCA File Offset: 0x0004BDCA
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

		// Token: 0x17000A70 RID: 2672
		// (get) Token: 0x06007236 RID: 29238 RVA: 0x0004DBD3 File Offset: 0x0004BDD3
		// (set) Token: 0x06007237 RID: 29239 RVA: 0x0004DBDB File Offset: 0x0004BDDB
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

		// Token: 0x17000A71 RID: 2673
		// (get) Token: 0x06007238 RID: 29240 RVA: 0x0004DBE4 File Offset: 0x0004BDE4
		// (set) Token: 0x06007239 RID: 29241 RVA: 0x0004DBEC File Offset: 0x0004BDEC
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

		// Token: 0x17000A72 RID: 2674
		// (get) Token: 0x0600723A RID: 29242 RVA: 0x0004DBF5 File Offset: 0x0004BDF5
		// (set) Token: 0x0600723B RID: 29243 RVA: 0x0004DBFD File Offset: 0x0004BDFD
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

		// Token: 0x17000A73 RID: 2675
		// (get) Token: 0x0600723C RID: 29244 RVA: 0x0004DC06 File Offset: 0x0004BE06
		// (set) Token: 0x0600723D RID: 29245 RVA: 0x0004DC0E File Offset: 0x0004BE0E
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

		// Token: 0x17000A74 RID: 2676
		// (get) Token: 0x0600723E RID: 29246 RVA: 0x0004DC17 File Offset: 0x0004BE17
		// (set) Token: 0x0600723F RID: 29247 RVA: 0x0004DC1F File Offset: 0x0004BE1F
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

		// Token: 0x17000A75 RID: 2677
		// (get) Token: 0x06007240 RID: 29248 RVA: 0x0004DC28 File Offset: 0x0004BE28
		// (set) Token: 0x06007241 RID: 29249 RVA: 0x0004DC30 File Offset: 0x0004BE30
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

		// Token: 0x17000A76 RID: 2678
		// (get) Token: 0x06007242 RID: 29250 RVA: 0x0004DC39 File Offset: 0x0004BE39
		// (set) Token: 0x06007243 RID: 29251 RVA: 0x0004DC41 File Offset: 0x0004BE41
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

		// Token: 0x17000A77 RID: 2679
		// (get) Token: 0x06007244 RID: 29252 RVA: 0x0004DC4A File Offset: 0x0004BE4A
		// (set) Token: 0x06007245 RID: 29253 RVA: 0x0004DC52 File Offset: 0x0004BE52
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

		// Token: 0x06007246 RID: 29254 RVA: 0x002A7AA8 File Offset: 0x002A5CA8
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

		// Token: 0x06007247 RID: 29255 RVA: 0x002A7BCC File Offset: 0x002A5DCC
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

		// Token: 0x06007248 RID: 29256 RVA: 0x0004C87D File Offset: 0x0004AA7D
		public override Color GetButtonColor()
		{
			return new Color32(230, 200, 250, byte.MaxValue);
		}

		// Token: 0x06007249 RID: 29257 RVA: 0x0004DC5B File Offset: 0x0004BE5B
		public override void OnCommandAdded(Block parentBlock)
		{
			this.display = DisplayType.Show;
		}

		// Token: 0x0400647C RID: 25724
		[Tooltip("Stage to display portrait on")]
		[SerializeField]
		protected Stage stage;

		// Token: 0x0400647D RID: 25725
		[Tooltip("Character to display")]
		[SerializeField]
		protected Character character;

		// Token: 0x0400647E RID: 25726
		[Tooltip("Character to swap with")]
		[SerializeField]
		protected Character replacedCharacter;

		// Token: 0x0400647F RID: 25727
		[Tooltip("Portrait to display")]
		[SerializeField]
		protected Sprite portrait;

		// Token: 0x04006480 RID: 25728
		[Tooltip("Move the portrait from/to this offset position")]
		[SerializeField]
		protected PositionOffset offset;

		// Token: 0x04006481 RID: 25729
		[Tooltip("Move the portrait from this position")]
		[SerializeField]
		protected RectTransform fromPosition;

		// Token: 0x04006482 RID: 25730
		[Tooltip("Move the portrait to this position")]
		[SerializeField]
		protected RectTransform toPosition;

		// Token: 0x04006483 RID: 25731
		[Tooltip("Direction character is facing")]
		[SerializeField]
		protected FacingDirection facing;

		// Token: 0x04006484 RID: 25732
		[Tooltip("Use Default Settings")]
		[SerializeField]
		protected bool useDefaultSettings = true;

		// Token: 0x04006485 RID: 25733
		[Tooltip("Fade Duration")]
		[SerializeField]
		protected float fadeDuration = 0.5f;

		// Token: 0x04006486 RID: 25734
		[Tooltip("Movement Duration")]
		[SerializeField]
		protected float moveDuration = 1f;

		// Token: 0x04006487 RID: 25735
		[Tooltip("Shift Offset")]
		[SerializeField]
		protected Vector2 shiftOffset;

		// Token: 0x04006488 RID: 25736
		[Tooltip("Move portrait into new position")]
		[SerializeField]
		protected bool move;

		// Token: 0x04006489 RID: 25737
		[Tooltip("Start from offset position")]
		[SerializeField]
		protected bool shiftIntoPlace;

		// Token: 0x0400648A RID: 25738
		[Tooltip("Wait until the tween has finished before executing the next command")]
		[SerializeField]
		protected bool waitUntilFinished;
	}
}
