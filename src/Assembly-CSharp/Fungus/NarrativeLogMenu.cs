using System;
using UnityEngine;
using UnityEngine.UI;

namespace Fungus
{
	// Token: 0x020012E4 RID: 4836
	public class NarrativeLogMenu : MonoBehaviour
	{
		// Token: 0x060075BD RID: 30141 RVA: 0x002B0D40 File Offset: 0x002AEF40
		protected virtual void Awake()
		{
			if (this.showLog)
			{
				if (NarrativeLogMenu.instance != null)
				{
					Object.Destroy(base.gameObject);
					return;
				}
				NarrativeLogMenu.instance = this;
				Object.DontDestroyOnLoad(this);
				this.clickAudioSource = base.GetComponent<AudioSource>();
			}
			else
			{
				GameObject.Find("NarrativeLogView").SetActive(false);
				base.enabled = false;
			}
			this.narLogViewtextAdapter.InitFromGameObject(this.narrativeLogView.gameObject, true);
		}

		// Token: 0x060075BE RID: 30142 RVA: 0x0005036F File Offset: 0x0004E56F
		protected virtual void Start()
		{
			if (!NarrativeLogMenu.narrativeLogActive)
			{
				this.narrativeLogMenuGroup.alpha = 0f;
			}
			this.UpdateNarrativeLogText();
		}

		// Token: 0x060075BF RID: 30143 RVA: 0x002B0DB8 File Offset: 0x002AEFB8
		protected virtual void OnEnable()
		{
			WriterSignals.OnWriterState += this.OnWriterState;
			SaveManagerSignals.OnSavePointLoaded += this.OnSavePointLoaded;
			SaveManagerSignals.OnSaveReset += this.OnSaveReset;
			BlockSignals.OnBlockEnd += this.OnBlockEnd;
			NarrativeLog.OnNarrativeAdded += this.OnNarrativeAdded;
		}

		// Token: 0x060075C0 RID: 30144 RVA: 0x002B0E20 File Offset: 0x002AF020
		protected virtual void OnDisable()
		{
			WriterSignals.OnWriterState -= this.OnWriterState;
			SaveManagerSignals.OnSavePointLoaded -= this.OnSavePointLoaded;
			SaveManagerSignals.OnSaveReset -= this.OnSaveReset;
			BlockSignals.OnBlockEnd -= this.OnBlockEnd;
			NarrativeLog.OnNarrativeAdded -= this.OnNarrativeAdded;
		}

		// Token: 0x060075C1 RID: 30145 RVA: 0x0005038E File Offset: 0x0004E58E
		protected virtual void OnNarrativeAdded()
		{
			this.UpdateNarrativeLogText();
		}

		// Token: 0x060075C2 RID: 30146 RVA: 0x00050396 File Offset: 0x0004E596
		protected virtual void OnWriterState(Writer writer, WriterState writerState)
		{
			if (writerState == WriterState.Start)
			{
				this.UpdateNarrativeLogText();
			}
		}

		// Token: 0x060075C3 RID: 30147 RVA: 0x0005038E File Offset: 0x0004E58E
		protected virtual void OnSavePointLoaded(string savePointKey)
		{
			this.UpdateNarrativeLogText();
		}

		// Token: 0x060075C4 RID: 30148 RVA: 0x000503A2 File Offset: 0x0004E5A2
		protected virtual void OnSaveReset()
		{
			FungusManager.Instance.NarrativeLog.Clear();
			this.UpdateNarrativeLogText();
		}

		// Token: 0x060075C5 RID: 30149 RVA: 0x002B0E88 File Offset: 0x002AF088
		protected virtual void OnBlockEnd(Block block)
		{
			bool flag = this.previousLines;
			this.previousLines = false;
			this.UpdateNarrativeLogText();
			this.previousLines = flag;
		}

		// Token: 0x060075C6 RID: 30150 RVA: 0x002B0EB0 File Offset: 0x002AF0B0
		protected void UpdateNarrativeLogText()
		{
			if (this.narrativeLogView.enabled)
			{
				this.narLogViewtextAdapter.Text = FungusManager.Instance.NarrativeLog.GetPrettyHistory(false);
				Canvas.ForceUpdateCanvases();
				this.narrativeLogView.verticalNormalizedPosition = 0f;
				Canvas.ForceUpdateCanvases();
			}
		}

		// Token: 0x060075C7 RID: 30151 RVA: 0x000503B9 File Offset: 0x0004E5B9
		protected void PlayClickSound()
		{
			if (this.clickAudioSource != null)
			{
				this.clickAudioSource.Play();
			}
		}

		// Token: 0x060075C8 RID: 30152 RVA: 0x002B0F00 File Offset: 0x002AF100
		public virtual void ToggleNarrativeLogView()
		{
			if (this.fadeTween != null)
			{
				LeanTween.cancel(this.fadeTween.id, true);
				this.fadeTween = null;
			}
			if (NarrativeLogMenu.narrativeLogActive)
			{
				LeanTween.value(this.narrativeLogMenuGroup.gameObject, this.narrativeLogMenuGroup.alpha, 0f, 0.2f).setEase(LeanTweenType.easeOutQuint).setOnUpdate(delegate(float t)
				{
					this.narrativeLogMenuGroup.alpha = t;
				}).setOnComplete(delegate()
				{
					this.narrativeLogMenuGroup.alpha = 0f;
				});
			}
			else
			{
				LeanTween.value(this.narrativeLogMenuGroup.gameObject, this.narrativeLogMenuGroup.alpha, 1f, 0.2f).setEase(LeanTweenType.easeOutQuint).setOnUpdate(delegate(float t)
				{
					this.narrativeLogMenuGroup.alpha = t;
				}).setOnComplete(delegate()
				{
					this.narrativeLogMenuGroup.alpha = 1f;
				});
			}
			NarrativeLogMenu.narrativeLogActive = !NarrativeLogMenu.narrativeLogActive;
		}

		// Token: 0x040066C4 RID: 26308
		[Tooltip("Show the Narrative Log Menu")]
		[SerializeField]
		protected bool showLog = true;

		// Token: 0x040066C5 RID: 26309
		[Tooltip("Show previous lines instead of previous and current")]
		[SerializeField]
		protected bool previousLines = true;

		// Token: 0x040066C6 RID: 26310
		[Tooltip("A scrollable text field used for displaying conversation history.")]
		[SerializeField]
		protected ScrollRect narrativeLogView;

		// Token: 0x040066C7 RID: 26311
		protected TextAdapter narLogViewtextAdapter = new TextAdapter();

		// Token: 0x040066C8 RID: 26312
		[Tooltip("The CanvasGroup containing the save menu buttons")]
		[SerializeField]
		protected CanvasGroup narrativeLogMenuGroup;

		// Token: 0x040066C9 RID: 26313
		protected static bool narrativeLogActive;

		// Token: 0x040066CA RID: 26314
		protected AudioSource clickAudioSource;

		// Token: 0x040066CB RID: 26315
		protected LTDescr fadeTween;

		// Token: 0x040066CC RID: 26316
		protected static NarrativeLogMenu instance;
	}
}
