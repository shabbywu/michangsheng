using System;
using UnityEngine;

namespace Fungus;

public class PortraitOptions
{
	public Character character;

	public Character replacedCharacter;

	public Sprite portrait;

	public DisplayType display;

	public PositionOffset offset;

	public RectTransform fromPosition;

	public RectTransform toPosition;

	public FacingDirection facing;

	public bool useDefaultSettings;

	public float fadeDuration;

	public float moveDuration;

	public Vector2 shiftOffset;

	public bool move;

	public bool shiftIntoPlace;

	public bool waitUntilFinished;

	public Action onComplete;

	public PortraitOptions(bool useDefaultSettings = true)
	{
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		character = null;
		replacedCharacter = null;
		portrait = null;
		display = DisplayType.None;
		offset = PositionOffset.None;
		fromPosition = null;
		toPosition = null;
		facing = FacingDirection.None;
		shiftOffset = new Vector2(0f, 0f);
		move = false;
		shiftIntoPlace = false;
		waitUntilFinished = false;
		onComplete = null;
		fadeDuration = 0.5f;
		moveDuration = 1f;
		this.useDefaultSettings = useDefaultSettings;
	}
}
