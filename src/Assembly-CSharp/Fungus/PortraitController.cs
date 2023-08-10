using System;
using System.Collections;
using MoonSharp.Interpreter;
using UnityEngine;
using UnityEngine.UI;

namespace Fungus;

public class PortraitController : MonoBehaviour
{
	protected float waitTimer;

	protected Stage stage;

	protected virtual void Awake()
	{
		stage = ((Component)this).GetComponentInParent<Stage>();
	}

	protected virtual void FinishCommand(PortraitOptions options)
	{
		if (options.onComplete != null)
		{
			if (!options.waitUntilFinished)
			{
				options.onComplete();
			}
			else
			{
				((MonoBehaviour)this).StartCoroutine(WaitUntilFinished(options.fadeDuration, options.onComplete));
			}
		}
		else
		{
			((MonoBehaviour)this).StartCoroutine(WaitUntilFinished(options.fadeDuration));
		}
	}

	protected virtual PortraitOptions CleanPortraitOptions(PortraitOptions options)
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		if (options.useDefaultSettings)
		{
			options.fadeDuration = stage.FadeDuration;
			options.moveDuration = stage.MoveDuration;
			options.shiftOffset = stage.ShiftOffset;
		}
		if ((Object)(object)options.character.State.portrait == (Object)null)
		{
			options.character.State.portrait = options.character.ProfileSprite;
		}
		if ((Object)(object)options.portrait == (Object)null)
		{
			options.portrait = options.character.State.portrait;
		}
		if ((Object)(object)options.character.State.position == (Object)null)
		{
			options.character.State.position = ((Graphic)stage.DefaultPosition).rectTransform;
		}
		if ((Object)(object)options.toPosition == (Object)null)
		{
			options.toPosition = options.character.State.position;
		}
		if ((Object)(object)options.replacedCharacter != (Object)null && (Object)(object)options.replacedCharacter.State.position == (Object)null)
		{
			options.replacedCharacter.State.position = ((Graphic)stage.DefaultPosition).rectTransform;
		}
		if (options.display == DisplayType.Replace)
		{
			options.toPosition = options.replacedCharacter.State.position;
		}
		if ((Object)(object)options.fromPosition == (Object)null)
		{
			options.fromPosition = options.character.State.position;
		}
		if (!options.move)
		{
			options.fromPosition = options.toPosition;
		}
		if (options.display == DisplayType.Hide)
		{
			options.fromPosition = options.character.State.position;
		}
		if (options.character.State.facing == FacingDirection.None)
		{
			options.character.State.facing = options.character.PortraitsFace;
		}
		if (options.facing == FacingDirection.None)
		{
			options.facing = options.character.State.facing;
		}
		if ((Object)(object)options.character.State.portraitImage == (Object)null)
		{
			CreatePortraitObject(options.character, options.fadeDuration);
		}
		return options;
	}

	protected virtual void CreatePortraitObject(Character character, float fadeDuration)
	{
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		GameObject val = new GameObject(((Object)character).name, new Type[3]
		{
			typeof(RectTransform),
			typeof(CanvasRenderer),
			typeof(Image)
		});
		val.transform.SetParent(((Component)stage.PortraitCanvas).transform, true);
		Image component = val.GetComponent<Image>();
		component.preserveAspect = true;
		component.sprite = character.ProfileSprite;
		((Graphic)component).color = new Color(1f, 1f, 1f, 0f);
		float time = ((fadeDuration > 0f) ? fadeDuration : float.Epsilon);
		Transform transform = ((Component)component).transform;
		LeanTween.alpha((RectTransform)(object)((transform is RectTransform) ? transform : null), 1f, time).setEase(stage.FadeEaseType).setRecursive(useRecursion: false);
		character.State.portraitImage = component;
	}

	protected virtual IEnumerator WaitUntilFinished(float duration, Action onComplete = null)
	{
		waitTimer = duration;
		while (waitTimer > 0f)
		{
			waitTimer -= Time.deltaTime;
			yield return null;
		}
		yield return (object)new WaitForEndOfFrame();
		onComplete?.Invoke();
	}

	protected virtual void SetupPortrait(PortraitOptions options)
	{
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		SetRectTransform(((Graphic)options.character.State.portraitImage).rectTransform, options.fromPosition);
		if (options.character.State.facing != options.character.PortraitsFace)
		{
			((Transform)((Graphic)options.character.State.portraitImage).rectTransform).localScale = new Vector3(-1f, 1f, 1f);
		}
		else
		{
			((Transform)((Graphic)options.character.State.portraitImage).rectTransform).localScale = new Vector3(1f, 1f, 1f);
		}
		if (options.facing != options.character.PortraitsFace)
		{
			((Transform)((Graphic)options.character.State.portraitImage).rectTransform).localScale = new Vector3(-1f, 1f, 1f);
		}
		else
		{
			((Transform)((Graphic)options.character.State.portraitImage).rectTransform).localScale = new Vector3(1f, 1f, 1f);
		}
	}

	protected virtual void DoMoveTween(Character character, RectTransform fromPosition, RectTransform toPosition, float moveDuration, bool waitUntilFinished)
	{
		PortraitOptions portraitOptions = new PortraitOptions();
		portraitOptions.character = character;
		portraitOptions.fromPosition = fromPosition;
		portraitOptions.toPosition = toPosition;
		portraitOptions.moveDuration = moveDuration;
		portraitOptions.waitUntilFinished = waitUntilFinished;
		DoMoveTween(portraitOptions);
	}

	protected virtual void DoMoveTween(PortraitOptions options)
	{
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		CleanPortraitOptions(options);
		float time = ((options.moveDuration > 0f) ? options.moveDuration : float.Epsilon);
		LeanTween.move(((Component)options.character.State.portraitImage).gameObject, ((Transform)options.toPosition).position, time).setEase(stage.FadeEaseType);
		if (options.waitUntilFinished)
		{
			waitTimer = time;
		}
	}

	public static void SetRectTransform(RectTransform oldRectTransform, RectTransform newRectTransform)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		((Transform)oldRectTransform).eulerAngles = ((Transform)newRectTransform).eulerAngles;
		((Transform)oldRectTransform).position = ((Transform)newRectTransform).position;
		((Transform)oldRectTransform).rotation = ((Transform)newRectTransform).rotation;
		oldRectTransform.anchoredPosition = newRectTransform.anchoredPosition;
		oldRectTransform.sizeDelta = newRectTransform.sizeDelta;
		oldRectTransform.anchorMax = newRectTransform.anchorMax;
		oldRectTransform.anchorMin = newRectTransform.anchorMin;
		oldRectTransform.pivot = newRectTransform.pivot;
		((Transform)oldRectTransform).localScale = ((Transform)newRectTransform).localScale;
	}

	public virtual void RunPortraitCommand(PortraitOptions options, Action onComplete)
	{
		waitTimer = 0f;
		if ((Object)(object)options.character == (Object)null)
		{
			onComplete();
			return;
		}
		if (options.display == DisplayType.Replace && (Object)(object)options.replacedCharacter == (Object)null)
		{
			onComplete();
			return;
		}
		if (options.display == DisplayType.Hide && !options.character.State.onScreen)
		{
			onComplete();
			return;
		}
		options = CleanPortraitOptions(options);
		options.onComplete = onComplete;
		switch (options.display)
		{
		case DisplayType.Show:
			Show(options);
			break;
		case DisplayType.Hide:
			Hide(options);
			break;
		case DisplayType.Replace:
			Show(options);
			Hide(options.replacedCharacter, ((Object)options.replacedCharacter.State.position).name);
			break;
		case DisplayType.MoveToFront:
			MoveToFront(options);
			break;
		}
	}

	public virtual void MoveToFront(Character character)
	{
		PortraitOptions portraitOptions = new PortraitOptions();
		portraitOptions.character = character;
		MoveToFront(CleanPortraitOptions(portraitOptions));
	}

	public virtual void MoveToFront(PortraitOptions options)
	{
		((Component)options.character.State.portraitImage).transform.SetSiblingIndex(((Component)options.character.State.portraitImage).transform.parent.childCount);
		options.character.State.display = DisplayType.MoveToFront;
		FinishCommand(options);
	}

	public virtual void Show(Character character, string position)
	{
		PortraitOptions portraitOptions = new PortraitOptions();
		portraitOptions.character = character;
		portraitOptions.fromPosition = (portraitOptions.toPosition = stage.GetPosition(position));
		Show(portraitOptions);
	}

	public virtual void Show(Character character, string portrait, string fromPosition, string toPosition)
	{
		PortraitOptions portraitOptions = new PortraitOptions();
		portraitOptions.character = character;
		portraitOptions.portrait = character.GetPortrait(portrait);
		portraitOptions.fromPosition = stage.GetPosition(fromPosition);
		portraitOptions.toPosition = stage.GetPosition(toPosition);
		portraitOptions.move = true;
		Show(portraitOptions);
	}

	public virtual void Show(Table optionsTable)
	{
		Show(PortraitUtil.ConvertTableToPortraitOptions(optionsTable, stage));
	}

	public virtual void Show(PortraitOptions options)
	{
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		options = CleanPortraitOptions(options);
		if (options.shiftIntoPlace)
		{
			options.fromPosition = Object.Instantiate<RectTransform>(options.toPosition);
			if (options.offset == PositionOffset.OffsetLeft)
			{
				options.fromPosition.anchoredPosition = new Vector2(options.fromPosition.anchoredPosition.x - Mathf.Abs(options.shiftOffset.x), options.fromPosition.anchoredPosition.y - Mathf.Abs(options.shiftOffset.y));
			}
			else if (options.offset == PositionOffset.OffsetRight)
			{
				options.fromPosition.anchoredPosition = new Vector2(options.fromPosition.anchoredPosition.x + Mathf.Abs(options.shiftOffset.x), options.fromPosition.anchoredPosition.y + Mathf.Abs(options.shiftOffset.y));
			}
			else
			{
				options.fromPosition.anchoredPosition = new Vector2(options.fromPosition.anchoredPosition.x, options.fromPosition.anchoredPosition.y);
			}
		}
		SetupPortrait(options);
		float time = ((options.fadeDuration > 0f) ? options.fadeDuration : float.Epsilon);
		if ((Object)(object)options.character.State.portraitImage != (Object)null && (Object)(object)options.character.State.portraitImage.sprite != (Object)null)
		{
			GameObject tempGO = Object.Instantiate<GameObject>(((Component)options.character.State.portraitImage).gameObject);
			tempGO.transform.SetParent(((Component)options.character.State.portraitImage).transform, false);
			tempGO.transform.localPosition = Vector3.zero;
			tempGO.transform.localScale = ((Transform)options.character.State.position).localScale;
			Image component = tempGO.GetComponent<Image>();
			component.sprite = options.character.State.portraitImage.sprite;
			component.preserveAspect = true;
			((Graphic)component).color = ((Graphic)options.character.State.portraitImage).color;
			LeanTween.alpha(((Graphic)component).rectTransform, 0f, time).setEase(stage.FadeEaseType).setOnComplete((Action)delegate
			{
				Object.Destroy((Object)(object)tempGO);
			})
				.setRecursive(useRecursion: false);
		}
		if ((Object)(object)options.character.State.portraitImage.sprite != (Object)(object)options.portrait || ((Graphic)options.character.State.portraitImage).color.a < 1f)
		{
			options.character.State.portraitImage.sprite = options.portrait;
			((Graphic)options.character.State.portraitImage).color = new Color(1f, 1f, 1f, 0f);
			LeanTween.alpha(((Graphic)options.character.State.portraitImage).rectTransform, 1f, time).setEase(stage.FadeEaseType).setRecursive(useRecursion: false);
		}
		DoMoveTween(options);
		FinishCommand(options);
		if (!stage.CharactersOnStage.Contains(options.character))
		{
			stage.CharactersOnStage.Add(options.character);
		}
		options.character.State.onScreen = true;
		options.character.State.display = DisplayType.Show;
		options.character.State.portrait = options.portrait;
		options.character.State.facing = options.facing;
		options.character.State.position = options.toPosition;
	}

	public virtual void ShowPortrait(Character character, string portrait)
	{
		PortraitOptions portraitOptions = new PortraitOptions();
		portraitOptions.character = character;
		portraitOptions.portrait = character.GetPortrait(portrait);
		if ((Object)(object)character.State.position == (Object)null)
		{
			portraitOptions.toPosition = (portraitOptions.fromPosition = stage.GetPosition("middle"));
		}
		else
		{
			portraitOptions.fromPosition = (portraitOptions.toPosition = character.State.position);
		}
		Show(portraitOptions);
	}

	public virtual void Hide(Character character)
	{
		PortraitOptions portraitOptions = new PortraitOptions();
		portraitOptions.character = character;
		Hide(portraitOptions);
	}

	public virtual void Hide(Character character, string toPosition)
	{
		PortraitOptions portraitOptions = new PortraitOptions();
		portraitOptions.character = character;
		portraitOptions.toPosition = stage.GetPosition(toPosition);
		portraitOptions.move = true;
		Hide(portraitOptions);
	}

	public virtual void Hide(Table optionsTable)
	{
		Hide(PortraitUtil.ConvertTableToPortraitOptions(optionsTable, stage));
	}

	public virtual void Hide(PortraitOptions options)
	{
		CleanPortraitOptions(options);
		if (options.character.State.display != 0)
		{
			SetupPortrait(options);
			float time = ((options.fadeDuration > 0f) ? options.fadeDuration : float.Epsilon);
			LeanTween.alpha(((Graphic)options.character.State.portraitImage).rectTransform, 0f, time).setEase(stage.FadeEaseType).setRecursive(useRecursion: false);
			DoMoveTween(options);
			stage.CharactersOnStage.Remove(options.character);
			options.character.State.onScreen = false;
			options.character.State.portrait = options.portrait;
			options.character.State.facing = options.facing;
			options.character.State.position = options.toPosition;
			options.character.State.display = DisplayType.Hide;
			FinishCommand(options);
		}
	}

	public virtual void SetDimmed(Character character, bool dimmedState)
	{
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		if (character.State.dimmed != dimmedState)
		{
			character.State.dimmed = dimmedState;
			Color to = (dimmedState ? stage.DimColor : Color.white);
			float time = ((stage.FadeDuration > 0f) ? stage.FadeDuration : float.Epsilon);
			LeanTween.color(((Graphic)character.State.portraitImage).rectTransform, to, time).setEase(stage.FadeEaseType).setRecursive(useRecursion: false);
		}
	}
}
