using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Fungus;

public class SaveMenu : MonoBehaviour
{
	[Tooltip("The string key used to store save game data in Player Prefs. If you have multiple games defined in the same Unity project, use a unique key for each one.")]
	[SerializeField]
	protected string saveDataKey = "save_data";

	[Tooltip("Automatically load the most recently saved game on startup")]
	[SerializeField]
	protected bool loadOnStart = true;

	[Tooltip("Automatically save game to disk after each Save Point command executes. This also disables the Save and Load menu buttons.")]
	[SerializeField]
	protected bool autoSave;

	[Tooltip("Delete the save game data from disk when player restarts the game. Useful for testing, but best switched off for release builds.")]
	[SerializeField]
	protected bool restartDeletesSave;

	[Tooltip("The CanvasGroup containing the save menu buttons")]
	[SerializeField]
	protected CanvasGroup saveMenuGroup;

	[Tooltip("The button which hides / displays the save menu")]
	[SerializeField]
	protected Button saveMenuButton;

	[Tooltip("The button which saves the save history to disk")]
	[SerializeField]
	protected Button saveButton;

	[Tooltip("The button which loads the save history from disk")]
	[SerializeField]
	protected Button loadButton;

	[Tooltip("The button which rewinds the save history to the previous save point.")]
	[SerializeField]
	protected Button rewindButton;

	[Tooltip("The button which fast forwards the save history to the next save point.")]
	[SerializeField]
	protected Button forwardButton;

	[Tooltip("The button which restarts the game.")]
	[SerializeField]
	protected Button restartButton;

	[Tooltip("A scrollable text field used for debugging the save data. The text field should be disabled in normal use.")]
	[SerializeField]
	protected ScrollRect debugView;

	protected static bool saveMenuActive;

	protected AudioSource clickAudioSource;

	protected LTDescr fadeTween;

	protected static SaveMenu instance;

	protected static bool hasLoadedOnStart;

	public virtual string SaveDataKey => saveDataKey;

	protected virtual void Awake()
	{
		if ((Object)(object)instance != (Object)null)
		{
			Object.Destroy((Object)(object)((Component)this).gameObject);
			return;
		}
		instance = this;
		if ((Object)(object)((Component)this).transform.parent == (Object)null)
		{
			Object.DontDestroyOnLoad((Object)(object)this);
		}
		else
		{
			Debug.LogError((object)"Save Menu cannot be preserved across scene loads if it is a child of another GameObject.");
		}
		clickAudioSource = ((Component)this).GetComponent<AudioSource>();
	}

	protected virtual void Start()
	{
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		if (!saveMenuActive)
		{
			saveMenuGroup.alpha = 0f;
		}
		SaveManager saveManager = FungusManager.Instance.SaveManager;
		if (string.IsNullOrEmpty(saveManager.StartScene))
		{
			Scene activeScene = SceneManager.GetActiveScene();
			saveManager.StartScene = ((Scene)(ref activeScene)).name;
		}
		if (loadOnStart && !hasLoadedOnStart)
		{
			hasLoadedOnStart = true;
			if (saveManager.SaveDataExists(saveDataKey))
			{
				saveManager.Load(saveDataKey);
			}
		}
	}

	protected virtual void Update()
	{
		SaveManager saveManager = FungusManager.Instance.SaveManager;
		bool flag = !autoSave;
		if (((UIBehaviour)saveButton).IsActive() != flag)
		{
			((Component)saveButton).gameObject.SetActive(flag);
			((Component)loadButton).gameObject.SetActive(flag);
		}
		if (flag)
		{
			if ((Object)(object)saveButton != (Object)null)
			{
				((Selectable)saveButton).interactable = saveManager.NumSavePoints > 0 && saveMenuActive;
			}
			if ((Object)(object)loadButton != (Object)null)
			{
				((Selectable)loadButton).interactable = saveManager.SaveDataExists(saveDataKey) && saveMenuActive;
			}
		}
		if ((Object)(object)restartButton != (Object)null)
		{
			((Selectable)restartButton).interactable = saveMenuActive;
		}
		if ((Object)(object)rewindButton != (Object)null)
		{
			((Selectable)rewindButton).interactable = saveManager.NumSavePoints > 0 && saveMenuActive;
		}
		if ((Object)(object)forwardButton != (Object)null)
		{
			((Selectable)forwardButton).interactable = saveManager.NumRewoundSavePoints > 0 && saveMenuActive;
		}
		if (((Behaviour)debugView).enabled)
		{
			Text componentInChildren = ((Component)debugView).GetComponentInChildren<Text>();
			if ((Object)(object)componentInChildren != (Object)null)
			{
				componentInChildren.text = saveManager.GetDebugInfo();
			}
		}
	}

	protected virtual void OnEnable()
	{
		SaveManagerSignals.OnSavePointAdded += OnSavePointAdded;
	}

	protected virtual void OnDisable()
	{
		SaveManagerSignals.OnSavePointAdded -= OnSavePointAdded;
	}

	protected virtual void OnSavePointAdded(string savePointKey, string savePointDescription)
	{
		SaveManager saveManager = FungusManager.Instance.SaveManager;
		if (autoSave && saveManager.NumSavePoints > 0)
		{
			saveManager.Save(saveDataKey);
		}
	}

	protected void PlayClickSound()
	{
		if ((Object)(object)clickAudioSource != (Object)null)
		{
			clickAudioSource.Play();
		}
	}

	public virtual void ToggleSaveMenu()
	{
		if (fadeTween != null)
		{
			LeanTween.cancel(fadeTween.id, callOnComplete: true);
			fadeTween = null;
		}
		if (saveMenuActive)
		{
			LeanTween.value(((Component)saveMenuGroup).gameObject, saveMenuGroup.alpha, 0f, 0.2f).setEase(LeanTweenType.easeOutQuint).setOnUpdate(delegate(float t)
			{
				saveMenuGroup.alpha = t;
			})
				.setOnComplete((Action)delegate
				{
					saveMenuGroup.alpha = 0f;
				});
		}
		else
		{
			LeanTween.value(((Component)saveMenuGroup).gameObject, saveMenuGroup.alpha, 1f, 0.2f).setEase(LeanTweenType.easeOutQuint).setOnUpdate(delegate(float t)
			{
				saveMenuGroup.alpha = t;
			})
				.setOnComplete((Action)delegate
				{
					saveMenuGroup.alpha = 1f;
				});
		}
		saveMenuActive = !saveMenuActive;
	}

	public virtual void Save()
	{
		SaveManager saveManager = FungusManager.Instance.SaveManager;
		if (saveManager.NumSavePoints > 0)
		{
			PlayClickSound();
			saveManager.Save(saveDataKey);
		}
	}

	public virtual void Load()
	{
		SaveManager saveManager = FungusManager.Instance.SaveManager;
		if (saveManager.SaveDataExists(saveDataKey))
		{
			PlayClickSound();
			saveManager.Load(saveDataKey);
		}
	}

	public virtual void Rewind()
	{
		PlayClickSound();
		SaveManager saveManager = FungusManager.Instance.SaveManager;
		if (saveManager.NumSavePoints > 0)
		{
			saveManager.Rewind();
		}
	}

	public virtual void FastForward()
	{
		PlayClickSound();
		SaveManager saveManager = FungusManager.Instance.SaveManager;
		if (saveManager.NumRewoundSavePoints > 0)
		{
			saveManager.FastForward();
		}
	}

	public virtual void Restart()
	{
		SaveManager saveManager = FungusManager.Instance.SaveManager;
		if (string.IsNullOrEmpty(saveManager.StartScene))
		{
			Debug.LogError((object)"No start scene specified");
			return;
		}
		PlayClickSound();
		saveManager.ClearHistory();
		if (restartDeletesSave)
		{
			saveManager.Delete(saveDataKey);
		}
		SaveManagerSignals.DoSaveReset();
		SceneManager.LoadScene(saveManager.StartScene);
	}
}
