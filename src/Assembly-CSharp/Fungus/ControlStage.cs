using System;
using System.Collections.Generic;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020011FA RID: 4602
	[CommandInfo("Narrative", "Control Stage", "Controls the stage on which character portraits are displayed.", 0)]
	public class ControlStage : ControlWithDisplay<StageDisplayType>
	{
		// Token: 0x17000A5D RID: 2653
		// (get) Token: 0x060070A9 RID: 28841 RVA: 0x0004C854 File Offset: 0x0004AA54
		public virtual Stage _Stage
		{
			get
			{
				return this.stage;
			}
		}

		// Token: 0x17000A5E RID: 2654
		// (get) Token: 0x060070AA RID: 28842 RVA: 0x0004C85C File Offset: 0x0004AA5C
		public virtual bool UseDefaultSettings
		{
			get
			{
				return this.useDefaultSettings;
			}
		}

		// Token: 0x060070AB RID: 28843 RVA: 0x002A2F28 File Offset: 0x002A1128
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

		// Token: 0x060070AC RID: 28844 RVA: 0x002A2FD0 File Offset: 0x002A11D0
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

		// Token: 0x060070AD RID: 28845 RVA: 0x002A3020 File Offset: 0x002A1220
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

		// Token: 0x060070AE RID: 28846 RVA: 0x0004C864 File Offset: 0x0004AA64
		protected virtual void DimNonSpeakingPortraits(Stage stage)
		{
			stage.DimPortraits = true;
		}

		// Token: 0x060070AF RID: 28847 RVA: 0x0004C86D File Offset: 0x0004AA6D
		protected virtual void OnComplete()
		{
			if (this.waitUntilFinished)
			{
				this.Continue();
			}
		}

		// Token: 0x060070B0 RID: 28848 RVA: 0x002A305C File Offset: 0x002A125C
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

		// Token: 0x060070B1 RID: 28849 RVA: 0x002A3198 File Offset: 0x002A1398
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

		// Token: 0x060070B2 RID: 28850 RVA: 0x0004C87D File Offset: 0x0004AA7D
		public override Color GetButtonColor()
		{
			return new Color32(230, 200, 250, byte.MaxValue);
		}

		// Token: 0x060070B3 RID: 28851 RVA: 0x0004C89D File Offset: 0x0004AA9D
		public override void OnCommandAdded(Block parentBlock)
		{
			this.display = StageDisplayType.Show;
		}

		// Token: 0x04006328 RID: 25384
		[Tooltip("Stage to display characters on")]
		[SerializeField]
		protected Stage stage;

		// Token: 0x04006329 RID: 25385
		[Tooltip("Stage to swap with")]
		[SerializeField]
		protected Stage replacedStage;

		// Token: 0x0400632A RID: 25386
		[Tooltip("Use Default Settings")]
		[SerializeField]
		protected bool useDefaultSettings = true;

		// Token: 0x0400632B RID: 25387
		[Tooltip("Fade Duration")]
		[SerializeField]
		protected float fadeDuration;

		// Token: 0x0400632C RID: 25388
		[Tooltip("Wait until the tween has finished before executing the next command")]
		[SerializeField]
		protected bool waitUntilFinished;
	}
}
