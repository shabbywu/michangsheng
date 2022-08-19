using System;
using UnityEngine;
using UnityEngine.UI;

namespace Fungus
{
	// Token: 0x02000E79 RID: 3705
	public class NarrativeLogMenu : MonoBehaviour
	{
		// Token: 0x060068D6 RID: 26838 RVA: 0x0028E67C File Offset: 0x0028C87C
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

		// Token: 0x060068D7 RID: 26839 RVA: 0x0028E6F2 File Offset: 0x0028C8F2
		protected virtual void Start()
		{
			if (!NarrativeLogMenu.narrativeLogActive)
			{
				this.narrativeLogMenuGroup.alpha = 0f;
			}
			this.UpdateNarrativeLogText();
		}

		// Token: 0x060068D8 RID: 26840 RVA: 0x0028E714 File Offset: 0x0028C914
		protected virtual void OnEnable()
		{
			WriterSignals.OnWriterState += this.OnWriterState;
			SaveManagerSignals.OnSavePointLoaded += this.OnSavePointLoaded;
			SaveManagerSignals.OnSaveReset += this.OnSaveReset;
			BlockSignals.OnBlockEnd += this.OnBlockEnd;
			NarrativeLog.OnNarrativeAdded += this.OnNarrativeAdded;
		}

		// Token: 0x060068D9 RID: 26841 RVA: 0x0028E77C File Offset: 0x0028C97C
		protected virtual void OnDisable()
		{
			WriterSignals.OnWriterState -= this.OnWriterState;
			SaveManagerSignals.OnSavePointLoaded -= this.OnSavePointLoaded;
			SaveManagerSignals.OnSaveReset -= this.OnSaveReset;
			BlockSignals.OnBlockEnd -= this.OnBlockEnd;
			NarrativeLog.OnNarrativeAdded -= this.OnNarrativeAdded;
		}

		// Token: 0x060068DA RID: 26842 RVA: 0x0028E7E3 File Offset: 0x0028C9E3
		protected virtual void OnNarrativeAdded()
		{
			this.UpdateNarrativeLogText();
		}

		// Token: 0x060068DB RID: 26843 RVA: 0x0028E7EB File Offset: 0x0028C9EB
		protected virtual void OnWriterState(Writer writer, WriterState writerState)
		{
			if (writerState == WriterState.Start)
			{
				this.UpdateNarrativeLogText();
			}
		}

		// Token: 0x060068DC RID: 26844 RVA: 0x0028E7E3 File Offset: 0x0028C9E3
		protected virtual void OnSavePointLoaded(string savePointKey)
		{
			this.UpdateNarrativeLogText();
		}

		// Token: 0x060068DD RID: 26845 RVA: 0x0028E7F7 File Offset: 0x0028C9F7
		protected virtual void OnSaveReset()
		{
			FungusManager.Instance.NarrativeLog.Clear();
			this.UpdateNarrativeLogText();
		}

		// Token: 0x060068DE RID: 26846 RVA: 0x0028E810 File Offset: 0x0028CA10
		protected virtual void OnBlockEnd(Block block)
		{
			bool flag = this.previousLines;
			this.previousLines = false;
			this.UpdateNarrativeLogText();
			this.previousLines = flag;
		}

		// Token: 0x060068DF RID: 26847 RVA: 0x0028E838 File Offset: 0x0028CA38
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

		// Token: 0x060068E0 RID: 26848 RVA: 0x0028E887 File Offset: 0x0028CA87
		protected void PlayClickSound()
		{
			if (this.clickAudioSource != null)
			{
				this.clickAudioSource.Play();
			}
		}

		// Token: 0x060068E1 RID: 26849 RVA: 0x0028E8A4 File Offset: 0x0028CAA4
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

		// Token: 0x040058FC RID: 22780
		[Tooltip("Show the Narrative Log Menu")]
		[SerializeField]
		protected bool showLog = true;

		// Token: 0x040058FD RID: 22781
		[Tooltip("Show previous lines instead of previous and current")]
		[SerializeField]
		protected bool previousLines = true;

		// Token: 0x040058FE RID: 22782
		[Tooltip("A scrollable text field used for displaying conversation history.")]
		[SerializeField]
		protected ScrollRect narrativeLogView;

		// Token: 0x040058FF RID: 22783
		protected TextAdapter narLogViewtextAdapter = new TextAdapter();

		// Token: 0x04005900 RID: 22784
		[Tooltip("The CanvasGroup containing the save menu buttons")]
		[SerializeField]
		protected CanvasGroup narrativeLogMenuGroup;

		// Token: 0x04005901 RID: 22785
		protected static bool narrativeLogActive;

		// Token: 0x04005902 RID: 22786
		protected AudioSource clickAudioSource;

		// Token: 0x04005903 RID: 22787
		protected LTDescr fadeTween;

		// Token: 0x04005904 RID: 22788
		protected static NarrativeLogMenu instance;
	}
}
