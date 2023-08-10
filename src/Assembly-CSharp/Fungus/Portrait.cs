using UnityEngine;

namespace Fungus;

[CommandInfo("Narrative", "Portrait", "Controls a character portrait.", 0)]
public class Portrait : ControlWithDisplay<DisplayType>
{
	[Tooltip("Stage to display portrait on")]
	[SerializeField]
	protected Stage stage;

	[Tooltip("Character to display")]
	[SerializeField]
	protected Character character;

	[Tooltip("Character to swap with")]
	[SerializeField]
	protected Character replacedCharacter;

	[Tooltip("Portrait to display")]
	[SerializeField]
	protected Sprite portrait;

	[Tooltip("Move the portrait from/to this offset position")]
	[SerializeField]
	protected PositionOffset offset;

	[Tooltip("Move the portrait from this position")]
	[SerializeField]
	protected RectTransform fromPosition;

	[Tooltip("Move the portrait to this position")]
	[SerializeField]
	protected RectTransform toPosition;

	[Tooltip("Direction character is facing")]
	[SerializeField]
	protected FacingDirection facing;

	[Tooltip("Use Default Settings")]
	[SerializeField]
	protected bool useDefaultSettings = true;

	[Tooltip("Fade Duration")]
	[SerializeField]
	protected float fadeDuration = 0.5f;

	[Tooltip("Movement Duration")]
	[SerializeField]
	protected float moveDuration = 1f;

	[Tooltip("Shift Offset")]
	[SerializeField]
	protected Vector2 shiftOffset;

	[Tooltip("Move portrait into new position")]
	[SerializeField]
	protected bool move;

	[Tooltip("Start from offset position")]
	[SerializeField]
	protected bool shiftIntoPlace;

	[Tooltip("Wait until the tween has finished before executing the next command")]
	[SerializeField]
	protected bool waitUntilFinished;

	public virtual Stage _Stage
	{
		get
		{
			return stage;
		}
		set
		{
			stage = value;
		}
	}

	public virtual Character _Character
	{
		get
		{
			return character;
		}
		set
		{
			character = value;
		}
	}

	public virtual Sprite _Portrait
	{
		get
		{
			return portrait;
		}
		set
		{
			portrait = value;
		}
	}

	public virtual PositionOffset Offset
	{
		get
		{
			return offset;
		}
		set
		{
			offset = value;
		}
	}

	public virtual RectTransform FromPosition
	{
		get
		{
			return fromPosition;
		}
		set
		{
			fromPosition = value;
		}
	}

	public virtual RectTransform ToPosition
	{
		get
		{
			return toPosition;
		}
		set
		{
			toPosition = value;
		}
	}

	public virtual FacingDirection Facing
	{
		get
		{
			return facing;
		}
		set
		{
			facing = value;
		}
	}

	public virtual bool UseDefaultSettings
	{
		get
		{
			return useDefaultSettings;
		}
		set
		{
			useDefaultSettings = value;
		}
	}

	public virtual bool Move
	{
		get
		{
			return move;
		}
		set
		{
			move = value;
		}
	}

	public virtual bool ShiftIntoPlace
	{
		get
		{
			return shiftIntoPlace;
		}
		set
		{
			shiftIntoPlace = value;
		}
	}

	public override void OnEnter()
	{
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)stage == (Object)null)
		{
			stage = Object.FindObjectOfType<Stage>();
			if ((Object)(object)stage == (Object)null)
			{
				Continue();
				return;
			}
		}
		if (IsDisplayNone(display))
		{
			Continue();
			return;
		}
		PortraitOptions portraitOptions = new PortraitOptions();
		portraitOptions.character = character;
		portraitOptions.replacedCharacter = replacedCharacter;
		portraitOptions.portrait = portrait;
		portraitOptions.display = display;
		portraitOptions.offset = offset;
		portraitOptions.fromPosition = fromPosition;
		portraitOptions.toPosition = toPosition;
		portraitOptions.facing = facing;
		portraitOptions.useDefaultSettings = useDefaultSettings;
		portraitOptions.fadeDuration = fadeDuration;
		portraitOptions.moveDuration = moveDuration;
		portraitOptions.shiftOffset = shiftOffset;
		portraitOptions.move = move;
		portraitOptions.shiftIntoPlace = shiftIntoPlace;
		portraitOptions.waitUntilFinished = waitUntilFinished;
		stage.RunPortraitCommand(portraitOptions, Continue);
	}

	public override string GetSummary()
	{
		if (display == DisplayType.None && (Object)(object)character == (Object)null)
		{
			return "Error: No character or display selected";
		}
		if (display == DisplayType.None)
		{
			return "Error: No display selected";
		}
		if ((Object)(object)character == (Object)null)
		{
			return "Error: No character selected";
		}
		string text = "";
		string text2 = "";
		string text3 = "";
		string text4 = "";
		string text5 = "";
		string text6 = "";
		string text7 = "";
		text = StringFormatter.SplitCamelCase(display.ToString());
		if (display == DisplayType.Replace && (Object)(object)replacedCharacter != (Object)null)
		{
			text = text + " \"" + ((Object)replacedCharacter).name + "\" with";
		}
		text2 = ((Object)character).name;
		if ((Object)(object)stage != (Object)null)
		{
			text5 = " on \"" + ((Object)stage).name + "\"";
		}
		if ((Object)(object)portrait != (Object)null)
		{
			text6 = " " + ((Object)portrait).name;
		}
		if (shiftIntoPlace)
		{
			if (offset != 0)
			{
				text3 = offset.ToString();
				text3 = " from \"" + text3 + "\"";
			}
		}
		else if ((Object)(object)fromPosition != (Object)null)
		{
			text3 = " from \"" + ((Object)fromPosition).name + "\"";
		}
		if ((Object)(object)toPosition != (Object)null)
		{
			string text8 = "";
			text8 = ((!move) ? " at " : " to ");
			text4 = text8 + "\"" + ((Object)toPosition).name + "\"";
		}
		if (facing != 0)
		{
			if (facing == FacingDirection.Left)
			{
				text7 = "<--";
			}
			if (facing == FacingDirection.Right)
			{
				text7 = "-->";
			}
			text7 = " facing \"" + text7 + "\"";
		}
		return text + " \"" + text2 + text6 + "\"" + text5 + text7 + text3 + text4;
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)230, (byte)200, (byte)250, byte.MaxValue));
	}

	public override void OnCommandAdded(Block parentBlock)
	{
		display = DisplayType.Show;
	}
}
