using System;
using System.Collections.Generic;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000DC2 RID: 3522
	[CommandInfo("Narrative", "Control Stage", "Controls the stage on which character portraits are displayed.", 0)]
	public class ControlStage : ControlWithDisplay<StageDisplayType>
	{
		// Token: 0x170007FA RID: 2042
		// (get) Token: 0x06006433 RID: 25651 RVA: 0x0027DE81 File Offset: 0x0027C081
		public virtual Stage _Stage
		{
			get
			{
				return this.stage;
			}
		}

		// Token: 0x170007FB RID: 2043
		// (get) Token: 0x06006434 RID: 25652 RVA: 0x0027DE89 File Offset: 0x0027C089
		public virtual bool UseDefaultSettings
		{
			get
			{
				return this.useDefaultSettings;
			}
		}

		// Token: 0x06006435 RID: 25653 RVA: 0x0027DE94 File Offset: 0x0027C094
		protected virtual void Show(Stage stage, bool visible)
		{
			float time = (this.fadeDuration == 0f) ? float.Epsilon : this.fadeDuration;
			float to = visible ? 1f : 0f;
			CanvasGroup canvasGroup = stage.GetComponentInChildren<CanvasGroup>();
			if (canvasGroup == null)
			{
				this.Continue();
				return;
			}
			LeanTween.value(canvasGroup.gameObject, canvasGroup.alpha, to, time).setOnUpdate(delegate(float alpha)
			{
				canvasGroup.alpha = alpha;
			}).setOnComplete(delegate()
			{
				this.OnComplete();
			});
		}

		// Token: 0x06006436 RID: 25654 RVA: 0x0027DF3C File Offset: 0x0027C13C
		protected virtual void MoveToFront(Stage stage)
		{
			List<Stage> activeStages = Stage.ActiveStages;
			for (int i = 0; i < activeStages.Count; i++)
			{
				Stage stage2 = activeStages[i];
				if (stage2 == stage)
				{
					stage2.PortraitCanvas.sortingOrder = 1;
				}
				else
				{
					stage2.PortraitCanvas.sortingOrder = 0;
				}
			}
		}

		// Token: 0x06006437 RID: 25655 RVA: 0x0027DF8C File Offset: 0x0027C18C
		protected virtual void UndimAllPortraits(Stage stage)
		{
			stage.DimPortraits = false;
			List<Character> charactersOnStage = stage.CharactersOnStage;
			for (int i = 0; i < charactersOnStage.Count; i++)
			{
				Character character = charactersOnStage[i];
				stage.SetDimmed(character, false);
			}
		}

		// Token: 0x06006438 RID: 25656 RVA: 0x0027DFC8 File Offset: 0x0027C1C8
		protected virtual void DimNonSpeakingPortraits(Stage stage)
		{
			stage.DimPortraits = true;
		}

		// Token: 0x06006439 RID: 25657 RVA: 0x0027DFD1 File Offset: 0x0027C1D1
		protected virtual void OnComplete()
		{
			if (this.waitUntilFinished)
			{
				this.Continue();
			}
		}

		// Token: 0x0600643A RID: 25658 RVA: 0x0027DFE4 File Offset: 0x0027C1E4
		public override void OnEnter()
		{
			if (this.IsDisplayNone<StageDisplayType>(this.display))
			{
				this.Continue();
				return;
			}
			if (this.stage == null)
			{
				this.stage = Object.FindObjectOfType<Stage>();
				if (this.stage == null)
				{
					this.Continue();
					return;
				}
			}
			if (this.display == StageDisplayType.Swap)
			{
				if (this.replacedStage == null)
				{
					this.replacedStage = Object.FindObjectOfType<Stage>();
				}
				if (this.replacedStage == null)
				{
					this.Continue();
					return;
				}
			}
			if (this.useDefaultSettings)
			{
				this.fadeDuration = this.stage.FadeDuration;
			}
			switch (this.display)
			{
			case StageDisplayType.Show:
				this.Show(this.stage, true);
				break;
			case StageDisplayType.Hide:
				this.Show(this.stage, false);
				break;
			case StageDisplayType.Swap:
				this.Show(this.stage, true);
				this.Show(this.replacedStage, false);
				break;
			case StageDisplayType.MoveToFront:
				this.MoveToFront(this.stage);
				break;
			case StageDisplayType.UndimAllPortraits:
				this.UndimAllPortraits(this.stage);
				break;
			case StageDisplayType.DimNonSpeakingPortraits:
				this.DimNonSpeakingPortraits(this.stage);
				break;
			}
			if (!this.waitUntilFinished)
			{
				this.Continue();
			}
		}

		// Token: 0x0600643B RID: 25659 RVA: 0x0027E120 File Offset: 0x0027C320
		public override string GetSummary()
		{
			if (this.display != StageDisplayType.None)
			{
				string str = StringFormatter.SplitCamelCase(this.display.ToString());
				string str2 = "";
				if (this.stage != null)
				{
					str2 = " \"" + this.stage.name + "\"";
				}
				return str + str2;
			}
			return "Error: No display selected";
		}

		// Token: 0x0600643C RID: 25660 RVA: 0x0027E190 File Offset: 0x0027C390
		public override Color GetButtonColor()
		{
			return new Color32(230, 200, 250, byte.MaxValue);
		}

		// Token: 0x0600643D RID: 25661 RVA: 0x0027E1B0 File Offset: 0x0027C3B0
		public override void OnCommandAdded(Block parentBlock)
		{
			this.display = StageDisplayType.Show;
		}

		// Token: 0x0400562E RID: 22062
		[Tooltip("Stage to display characters on")]
		[SerializeField]
		protected Stage stage;

		// Token: 0x0400562F RID: 22063
		[Tooltip("Stage to swap with")]
		[SerializeField]
		protected Stage replacedStage;

		// Token: 0x04005630 RID: 22064
		[Tooltip("Use Default Settings")]
		[SerializeField]
		protected bool useDefaultSettings = true;

		// Token: 0x04005631 RID: 22065
		[Tooltip("Fade Duration")]
		[SerializeField]
		protected float fadeDuration;

		// Token: 0x04005632 RID: 22066
		[Tooltip("Wait until the tween has finished before executing the next command")]
		[SerializeField]
		protected bool waitUntilFinished;
	}
}
