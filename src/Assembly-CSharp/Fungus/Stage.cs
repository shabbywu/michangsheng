using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Fungus;

[ExecuteInEditMode]
public class Stage : PortraitController
{
	[Tooltip("Canvas object containing the stage positions.")]
	[SerializeField]
	protected Canvas portraitCanvas;

	[Tooltip("Dim portraits when a character is not speaking.")]
	[SerializeField]
	protected bool dimPortraits;

	[Tooltip("Choose a dimColor")]
	[SerializeField]
	protected Color dimColor = new Color(0.5f, 0.5f, 0.5f, 1f);

	[Tooltip("Duration for fading character portraits in / out.")]
	[SerializeField]
	protected float fadeDuration = 0.5f;

	[Tooltip("Duration for moving characters to a new position")]
	[SerializeField]
	protected float moveDuration = 1f;

	[Tooltip("Ease type for the fade tween.")]
	[SerializeField]
	protected LeanTweenType fadeEaseType;

	[Tooltip("Constant offset to apply to portrait position.")]
	[SerializeField]
	protected Vector2 shiftOffset;

	[Tooltip("The position object where characters appear by default.")]
	[SerializeField]
	protected Image defaultPosition;

	[Tooltip("List of stage position rect transforms in the stage.")]
	[SerializeField]
	protected List<RectTransform> positions;

	protected List<Character> charactersOnStage = new List<Character>();

	protected static List<Stage> activeStages = new List<Stage>();

	public static List<Stage> ActiveStages => activeStages;

	public virtual Canvas PortraitCanvas => portraitCanvas;

	public virtual bool DimPortraits
	{
		get
		{
			return dimPortraits;
		}
		set
		{
			dimPortraits = value;
		}
	}

	public virtual Color DimColor
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return dimColor;
		}
		set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			dimColor = value;
		}
	}

	public virtual float FadeDuration
	{
		get
		{
			return fadeDuration;
		}
		set
		{
			fadeDuration = value;
		}
	}

	public virtual float MoveDuration
	{
		get
		{
			return moveDuration;
		}
		set
		{
			moveDuration = value;
		}
	}

	public virtual LeanTweenType FadeEaseType => fadeEaseType;

	public virtual Vector2 ShiftOffset => shiftOffset;

	public virtual Image DefaultPosition => defaultPosition;

	public virtual List<RectTransform> Positions => positions;

	public virtual List<Character> CharactersOnStage => charactersOnStage;

	protected virtual void OnEnable()
	{
		if (!activeStages.Contains(this))
		{
			activeStages.Add(this);
		}
	}

	protected virtual void OnDisable()
	{
		activeStages.Remove(this);
	}

	protected virtual void Start()
	{
		if (Application.isPlaying && (Object)(object)portraitCanvas != (Object)null)
		{
			((Component)portraitCanvas).gameObject.SetActive(true);
		}
	}

	public static Stage GetActiveStage()
	{
		if (activeStages == null || activeStages.Count == 0)
		{
			return null;
		}
		return activeStages[0];
	}

	public RectTransform GetPosition(string positionString)
	{
		if (string.IsNullOrEmpty(positionString))
		{
			return null;
		}
		for (int i = 0; i < positions.Count; i++)
		{
			if (string.Compare(((Object)positions[i]).name, positionString, ignoreCase: true) == 0)
			{
				return positions[i];
			}
		}
		return null;
	}
}
