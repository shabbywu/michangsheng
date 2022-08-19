using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Fungus
{
	// Token: 0x02000E82 RID: 3714
	public class SaveMenu : MonoBehaviour
	{
		// Token: 0x06006928 RID: 26920 RVA: 0x0028FEA0 File Offset: 0x0028E0A0
		protected virtual void Awake()
		{
			if (SaveMenu.instance != null)
			{
				Object.Destroy(base.gameObject);
				return;
			}
			SaveMenu.instance = this;
			if (base.transform.parent == null)
			{
				Object.DontDestroyOnLoad(this);
			}
			else
			{
				Debug.LogError("Save Menu cannot be preserved across scene loads if it is a child of another GameObject.");
			}
			this.clickAudioSource = base.GetComponent<AudioSource>();
		}

		// Token: 0x06006929 RID: 26921 RVA: 0x0028FF00 File Offset: 0x0028E100
		protected virtual void Start()
		{
			if (!SaveMenu.saveMenuActive)
			{
				this.saveMenuGroup.alpha = 0f;
			}
			SaveManager saveManager = FungusManager.Instance.SaveManager;
			if (string.IsNullOrEmpty(saveManager.StartScene))
			{
				saveManager.StartScene = SceneManager.GetActiveScene().name;
			}
			if (this.loadOnStart && !SaveMenu.hasLoadedOnStart)
			{
				SaveMenu.hasLoadedOnStart = true;
				if (saveManager.SaveDataExists(this.saveDataKey))
				{
					saveManager.Load(this.saveDataKey);
				}
			}
		}

		// Token: 0x0600692A RID: 26922 RVA: 0x0028FF80 File Offset: 0x0028E180
		protected virtual void Update()
		{
			SaveManager saveManager = FungusManager.Instance.SaveManager;
			bool flag = !this.autoSave;
			if (this.saveButton.IsActive() != flag)
			{
				this.saveButton.gameObject.SetActive(flag);
				this.loadButton.gameObject.SetActive(flag);
			}
			if (flag)
			{
				if (this.saveButton != null)
				{
					this.saveButton.interactable = (saveManager.NumSavePoints > 0 && SaveMenu.saveMenuActive);
				}
				if (this.loadButton != null)
				{
					this.loadButton.interactable = (saveManager.SaveDataExists(this.saveDataKey) && SaveMenu.saveMenuActive);
				}
			}
			if (this.restartButton != null)
			{
				this.restartButton.interactable = SaveMenu.saveMenuActive;
			}
			if (this.rewindButton != null)
			{
				this.rewindButton.interactable = (saveManager.NumSavePoints > 0 && SaveMenu.saveMenuActive);
			}
			if (this.forwardButton != null)
			{
				this.forwardButton.interactable = (saveManager.NumRewoundSavePoints > 0 && SaveMenu.saveMenuActive);
			}
			if (this.debugView.enabled)
			{
				Text componentInChildren = this.debugView.GetComponentInChildren<Text>();
				if (componentInChildren != null)
				{
					componentInChildren.text = saveManager.GetDebugInfo();
				}
			}
		}

		// Token: 0x0600692B RID: 26923 RVA: 0x002900CE File Offset: 0x0028E2CE
		protected virtual void OnEnable()
		{
			SaveManagerSignals.OnSavePointAdded += this.OnSavePointAdded;
		}

		// Token: 0x0600692C RID: 26924 RVA: 0x002900E2 File Offset: 0x0028E2E2
		protected virtual void OnDisable()
		{
			SaveManagerSignals.OnSavePointAdded -= this.OnSavePointAdded;
		}

		// Token: 0x0600692D RID: 26925 RVA: 0x002900F8 File Offset: 0x0028E2F8
		protected virtual void OnSavePointAdded(string savePointKey, string savePointDescription)
		{
			SaveManager saveManager = FungusManager.Instance.SaveManager;
			if (this.autoSave && saveManager.NumSavePoints > 0)
			{
				saveManager.Save(this.saveDataKey);
			}
		}

		// Token: 0x0600692E RID: 26926 RVA: 0x0029012D File Offset: 0x0028E32D
		protected void PlayClickSound()
		{
			if (this.clickAudioSource != null)
			{
				this.clickAudioSource.Play();
			}
		}

		// Token: 0x17000878 RID: 2168
		// (get) Token: 0x0600692F RID: 26927 RVA: 0x00290148 File Offset: 0x0028E348
		public virtual string SaveDataKey
		{
			get
			{
				return this.saveDataKey;
			}
		}

		// Token: 0x06006930 RID: 26928 RVA: 0x00290150 File Offset: 0x0028E350
		public virtual void ToggleSaveMenu()
		{
			if (this.fadeTween != null)
			{
				LeanTween.cancel(this.fadeTween.id, true);
				this.fadeTween = null;
			}
			if (SaveMenu.saveMenuActive)
			{
				LeanTween.value(this.saveMenuGroup.gameObject, this.saveMenuGroup.alpha, 0f, 0.2f).setEase(LeanTweenType.easeOutQuint).setOnUpdate(delegate(float t)
				{
					this.saveMenuGroup.alpha = t;
				}).setOnComplete(delegate()
				{
					this.saveMenuGroup.alpha = 0f;
				});
			}
			else
			{
				LeanTween.value(this.saveMenuGroup.gameObject, this.saveMenuGroup.alpha, 1f, 0.2f).setEase(LeanTweenType.easeOutQuint).setOnUpdate(delegate(float t)
				{
					this.saveMenuGroup.alpha = t;
				}).setOnComplete(delegate()
				{
					this.saveMenuGroup.alpha = 1f;
				});
			}
			SaveMenu.saveMenuActive = !SaveMenu.saveMenuActive;
		}

		// Token: 0x06006931 RID: 26929 RVA: 0x00290234 File Offset: 0x0028E434
		public virtual void Save()
		{
			SaveManager saveManager = FungusManager.Instance.SaveManager;
			if (saveManager.NumSavePoints > 0)
			{
				this.PlayClickSound();
				saveManager.Save(this.saveDataKey);
			}
		}

		// Token: 0x06006932 RID: 26930 RVA: 0x00290268 File Offset: 0x0028E468
		public virtual void Load()
		{
			SaveManager saveManager = FungusManager.Instance.SaveManager;
			if (saveManager.SaveDataExists(this.saveDataKey))
			{
				this.PlayClickSound();
				saveManager.Load(this.saveDataKey);
			}
		}

		// Token: 0x06006933 RID: 26931 RVA: 0x002902A0 File Offset: 0x0028E4A0
		public virtual void Rewind()
		{
			this.PlayClickSound();
			SaveManager saveManager = FungusManager.Instance.SaveManager;
			if (saveManager.NumSavePoints > 0)
			{
				saveManager.Rewind();
			}
		}

		// Token: 0x06006934 RID: 26932 RVA: 0x002902D0 File Offset: 0x0028E4D0
		public virtual void FastForward()
		{
			this.PlayClickSound();
			SaveManager saveManager = FungusManager.Instance.SaveManager;
			if (saveManager.NumRewoundSavePoints > 0)
			{
				saveManager.FastForward();
			}
		}

		// Token: 0x06006935 RID: 26933 RVA: 0x00290300 File Offset: 0x0028E500
		public virtual void Restart()
		{
			SaveManager saveManager = FungusManager.Instance.SaveManager;
			if (string.IsNullOrEmpty(saveManager.StartScene))
			{
				Debug.LogError("No start scene specified");
				return;
			}
			this.PlayClickSound();
			saveManager.ClearHistory();
			if (this.restartDeletesSave)
			{
				saveManager.Delete(this.saveDataKey);
			}
			SaveManagerSignals.DoSaveReset();
			SceneManager.LoadScene(saveManager.StartScene);
		}

		// Token: 0x04005924 RID: 22820
		[Tooltip("The string key used to store save game data in Player Prefs. If you have multiple games defined in the same Unity project, use a unique key for each one.")]
		[SerializeField]
		protected string saveDataKey = "save_data";

		// Token: 0x04005925 RID: 22821
		[Tooltip("Automatically load the most recently saved game on startup")]
		[SerializeField]
		protected bool loadOnStart = true;

		// Token: 0x04005926 RID: 22822
		[Tooltip("Automatically save game to disk after each Save Point command executes. This also disables the Save and Load menu buttons.")]
		[SerializeField]
		protected bool autoSave;

		// Token: 0x04005927 RID: 22823
		[Tooltip("Delete the save game data from disk when player restarts the game. Useful for testing, but best switched off for release builds.")]
		[SerializeField]
		protected bool restartDeletesSave;

		// Token: 0x04005928 RID: 22824
		[Tooltip("The CanvasGroup containing the save menu buttons")]
		[SerializeField]
		protected CanvasGroup saveMenuGroup;

		// Token: 0x04005929 RID: 22825
		[Tooltip("The button which hides / displays the save menu")]
		[SerializeField]
		protected Button saveMenuButton;

		// Token: 0x0400592A RID: 22826
		[Tooltip("The button which saves the save history to disk")]
		[SerializeField]
		protected Button saveButton;

		// Token: 0x0400592B RID: 22827
		[Tooltip("The button which loads the save history from disk")]
		[SerializeField]
		protected Button loadButton;

		// Token: 0x0400592C RID: 22828
		[Tooltip("The button which rewinds the save history to the previous save point.")]
		[SerializeField]
		protected Button rewindButton;

		// Token: 0x0400592D RID: 22829
		[Tooltip("The button which fast forwards the save history to the next save point.")]
		[SerializeField]
		protected Button forwardButton;

		// Token: 0x0400592E RID: 22830
		[Tooltip("The button which restarts the game.")]
		[SerializeField]
		protected Button restartButton;

		// Token: 0x0400592F RID: 22831
		[Tooltip("A scrollable text field used for debugging the save data. The text field should be disabled in normal use.")]
		[SerializeField]
		protected ScrollRect debugView;

		// Token: 0x04005930 RID: 22832
		protected static bool saveMenuActive;

		// Token: 0x04005931 RID: 22833
		protected AudioSource clickAudioSource;

		// Token: 0x04005932 RID: 22834
		protected LTDescr fadeTween;

		// Token: 0x04005933 RID: 22835
		protected static SaveMenu instance;

		// Token: 0x04005934 RID: 22836
		protected static bool hasLoadedOnStart;
	}
}
