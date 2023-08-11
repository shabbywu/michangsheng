using System;
using UnityEngine;
using UnityEngine.UI;

namespace Fungus;

public class NarrativeLogMenu : MonoBehaviour
{
	[Tooltip("Show the Narrative Log Menu")]
	[SerializeField]
	protected bool showLog = true;

	[Tooltip("Show previous lines instead of previous and current")]
	[SerializeField]
	protected bool previousLines = true;

	[Tooltip("A scrollable text field used for displaying conversation history.")]
	[SerializeField]
	protected ScrollRect narrativeLogView;

	protected TextAdapter narLogViewtextAdapter = new TextAdapter();

	[Tooltip("The CanvasGroup containing the save menu buttons")]
	[SerializeField]
	protected CanvasGroup narrativeLogMenuGroup;

	protected static bool narrativeLogActive;

	protected AudioSource clickAudioSource;

	protected LTDescr fadeTween;

	protected static NarrativeLogMenu instance;

	protected virtual void Awake()
	{
		if (showLog)
		{
			if ((Object)(object)instance != (Object)null)
			{
				Object.Destroy((Object)(object)((Component)this).gameObject);
				return;
			}
			instance = this;
			Object.DontDestroyOnLoad((Object)(object)this);
			clickAudioSource = ((Component)this).GetComponent<AudioSource>();
		}
		else
		{
			GameObject.Find("NarrativeLogView").SetActive(false);
			((Behaviour)this).enabled = false;
		}
		narLogViewtextAdapter.InitFromGameObject(((Component)narrativeLogView).gameObject, includeChildren: true);
	}

	protected virtual void Start()
	{
		if (!narrativeLogActive)
		{
			narrativeLogMenuGroup.alpha = 0f;
		}
		UpdateNarrativeLogText();
	}

	protected virtual void OnEnable()
	{
		WriterSignals.OnWriterState += OnWriterState;
		SaveManagerSignals.OnSavePointLoaded += OnSavePointLoaded;
		SaveManagerSignals.OnSaveReset += OnSaveReset;
		BlockSignals.OnBlockEnd += OnBlockEnd;
		NarrativeLog.OnNarrativeAdded += OnNarrativeAdded;
	}

	protected virtual void OnDisable()
	{
		WriterSignals.OnWriterState -= OnWriterState;
		SaveManagerSignals.OnSavePointLoaded -= OnSavePointLoaded;
		SaveManagerSignals.OnSaveReset -= OnSaveReset;
		BlockSignals.OnBlockEnd -= OnBlockEnd;
		NarrativeLog.OnNarrativeAdded -= OnNarrativeAdded;
	}

	protected virtual void OnNarrativeAdded()
	{
		UpdateNarrativeLogText();
	}

	protected virtual void OnWriterState(Writer writer, WriterState writerState)
	{
		if (writerState == WriterState.Start)
		{
			UpdateNarrativeLogText();
		}
	}

	protected virtual void OnSavePointLoaded(string savePointKey)
	{
		UpdateNarrativeLogText();
	}

	protected virtual void OnSaveReset()
	{
		FungusManager.Instance.NarrativeLog.Clear();
		UpdateNarrativeLogText();
	}

	protected virtual void OnBlockEnd(Block block)
	{
		bool flag = previousLines;
		previousLines = false;
		UpdateNarrativeLogText();
		previousLines = flag;
	}

	protected void UpdateNarrativeLogText()
	{
		if (((Behaviour)narrativeLogView).enabled)
		{
			narLogViewtextAdapter.Text = FungusManager.Instance.NarrativeLog.GetPrettyHistory();
			Canvas.ForceUpdateCanvases();
			narrativeLogView.verticalNormalizedPosition = 0f;
			Canvas.ForceUpdateCanvases();
		}
	}

	protected void PlayClickSound()
	{
		if ((Object)(object)clickAudioSource != (Object)null)
		{
			clickAudioSource.Play();
		}
	}

	public virtual void ToggleNarrativeLogView()
	{
		if (fadeTween != null)
		{
			LeanTween.cancel(fadeTween.id, callOnComplete: true);
			fadeTween = null;
		}
		if (narrativeLogActive)
		{
			LeanTween.value(((Component)narrativeLogMenuGroup).gameObject, narrativeLogMenuGroup.alpha, 0f, 0.2f).setEase(LeanTweenType.easeOutQuint).setOnUpdate(delegate(float t)
			{
				narrativeLogMenuGroup.alpha = t;
			})
				.setOnComplete((Action)delegate
				{
					narrativeLogMenuGroup.alpha = 0f;
				});
		}
		else
		{
			LeanTween.value(((Component)narrativeLogMenuGroup).gameObject, narrativeLogMenuGroup.alpha, 1f, 0.2f).setEase(LeanTweenType.easeOutQuint).setOnUpdate(delegate(float t)
			{
				narrativeLogMenuGroup.alpha = t;
			})
				.setOnComplete((Action)delegate
				{
					narrativeLogMenuGroup.alpha = 1f;
				});
		}
		narrativeLogActive = !narrativeLogActive;
	}
}
