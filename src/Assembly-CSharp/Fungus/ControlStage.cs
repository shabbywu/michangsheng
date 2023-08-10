using System;
using System.Collections.Generic;
using UnityEngine;

namespace Fungus;

[CommandInfo("Narrative", "Control Stage", "Controls the stage on which character portraits are displayed.", 0)]
public class ControlStage : ControlWithDisplay<StageDisplayType>
{
	[Tooltip("Stage to display characters on")]
	[SerializeField]
	protected Stage stage;

	[Tooltip("Stage to swap with")]
	[SerializeField]
	protected Stage replacedStage;

	[Tooltip("Use Default Settings")]
	[SerializeField]
	protected bool useDefaultSettings = true;

	[Tooltip("Fade Duration")]
	[SerializeField]
	protected float fadeDuration;

	[Tooltip("Wait until the tween has finished before executing the next command")]
	[SerializeField]
	protected bool waitUntilFinished;

	public virtual Stage _Stage => stage;

	public virtual bool UseDefaultSettings => useDefaultSettings;

	protected virtual void Show(Stage stage, bool visible)
	{
		float time = ((fadeDuration == 0f) ? float.Epsilon : fadeDuration);
		float to = (visible ? 1f : 0f);
		CanvasGroup canvasGroup = ((Component)stage).GetComponentInChildren<CanvasGroup>();
		if ((Object)(object)canvasGroup == (Object)null)
		{
			Continue();
			return;
		}
		LeanTween.value(((Component)canvasGroup).gameObject, canvasGroup.alpha, to, time).setOnUpdate(delegate(float alpha)
		{
			canvasGroup.alpha = alpha;
		}).setOnComplete((Action)delegate
		{
			OnComplete();
		});
	}

	protected virtual void MoveToFront(Stage stage)
	{
		List<Stage> activeStages = Stage.ActiveStages;
		for (int i = 0; i < activeStages.Count; i++)
		{
			Stage stage2 = activeStages[i];
			if ((Object)(object)stage2 == (Object)(object)stage)
			{
				stage2.PortraitCanvas.sortingOrder = 1;
			}
			else
			{
				stage2.PortraitCanvas.sortingOrder = 0;
			}
		}
	}

	protected virtual void UndimAllPortraits(Stage stage)
	{
		stage.DimPortraits = false;
		List<Character> charactersOnStage = stage.CharactersOnStage;
		for (int i = 0; i < charactersOnStage.Count; i++)
		{
			Character character = charactersOnStage[i];
			stage.SetDimmed(character, dimmedState: false);
		}
	}

	protected virtual void DimNonSpeakingPortraits(Stage stage)
	{
		stage.DimPortraits = true;
	}

	protected virtual void OnComplete()
	{
		if (waitUntilFinished)
		{
			Continue();
		}
	}

	public override void OnEnter()
	{
		if (IsDisplayNone(display))
		{
			Continue();
			return;
		}
		if ((Object)(object)stage == (Object)null)
		{
			stage = Object.FindObjectOfType<Stage>();
			if ((Object)(object)stage == (Object)null)
			{
				Continue();
				return;
			}
		}
		if (display == StageDisplayType.Swap)
		{
			if ((Object)(object)replacedStage == (Object)null)
			{
				replacedStage = Object.FindObjectOfType<Stage>();
			}
			if ((Object)(object)replacedStage == (Object)null)
			{
				Continue();
				return;
			}
		}
		if (useDefaultSettings)
		{
			fadeDuration = stage.FadeDuration;
		}
		switch (display)
		{
		case StageDisplayType.Show:
			Show(stage, visible: true);
			break;
		case StageDisplayType.Hide:
			Show(stage, visible: false);
			break;
		case StageDisplayType.Swap:
			Show(stage, visible: true);
			Show(replacedStage, visible: false);
			break;
		case StageDisplayType.MoveToFront:
			MoveToFront(stage);
			break;
		case StageDisplayType.UndimAllPortraits:
			UndimAllPortraits(stage);
			break;
		case StageDisplayType.DimNonSpeakingPortraits:
			DimNonSpeakingPortraits(stage);
			break;
		}
		if (!waitUntilFinished)
		{
			Continue();
		}
	}

	public override string GetSummary()
	{
		string text = "";
		if (display != 0)
		{
			text = StringFormatter.SplitCamelCase(display.ToString());
			string text2 = "";
			if ((Object)(object)stage != (Object)null)
			{
				text2 = " \"" + ((Object)stage).name + "\"";
			}
			return text + text2;
		}
		return "Error: No display selected";
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)230, (byte)200, (byte)250, byte.MaxValue));
	}

	public override void OnCommandAdded(Block parentBlock)
	{
		display = StageDisplayType.Show;
	}
}
