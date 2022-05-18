using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Fungus
{
	// Token: 0x020012F1 RID: 4849
	public class SaveMenu : MonoBehaviour
	{
		// Token: 0x0600761B RID: 30235 RVA: 0x002B2280 File Offset: 0x002B0480
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

		// Token: 0x0600761C RID: 30236 RVA: 0x002B22E0 File Offset: 0x002B04E0
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

		// Token: 0x0600761D RID: 30237 RVA: 0x002B2360 File Offset: 0x002B0560
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

		// Token: 0x0600761E RID: 30238 RVA: 0x0005074B File Offset: 0x0004E94B
		protected virtual void OnEnable()
		{
			SaveManagerSignals.OnSavePointAdded += this.OnSavePointAdded;
		}

		// Token: 0x0600761F RID: 30239 RVA: 0x0005075F File Offset: 0x0004E95F
		protected virtual void OnDisable()
		{
			SaveManagerSignals.OnSavePointAdded -= this.OnSavePointAdded;
		}

		// Token: 0x06007620 RID: 30240 RVA: 0x002B24B0 File Offset: 0x002B06B0
		protected virtual void OnSavePointAdded(string savePointKey, string savePointDescription)
		{
			SaveManager saveManager = FungusManager.Instance.SaveManager;
			if (this.autoSave && saveManager.NumSavePoints > 0)
			{
				saveManager.Save(this.saveDataKey);
			}
		}

		// Token: 0x06007621 RID: 30241 RVA: 0x00050773 File Offset: 0x0004E973
		protected void PlayClickSound()
		{
			if (this.clickAudioSource != null)
			{
				this.clickAudioSource.Play();
			}
		}

		// Token: 0x17000AF3 RID: 2803
		// (get) Token: 0x06007622 RID: 30242 RVA: 0x0005078E File Offset: 0x0004E98E
		public virtual string SaveDataKey
		{
			get
			{
				return this.saveDataKey;
			}
		}

		// Token: 0x06007623 RID: 30243 RVA: 0x002B24E8 File Offset: 0x002B06E8
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

		// Token: 0x06007624 RID: 30244 RVA: 0x002B25CC File Offset: 0x002B07CC
		public virtual void Save()
		{
			SaveManager saveManager = FungusManager.Instance.SaveManager;
			if (saveManager.NumSavePoints > 0)
			{
				this.PlayClickSound();
				saveManager.Save(this.saveDataKey);
			}
		}

		// Token: 0x06007625 RID: 30245 RVA: 0x002B2600 File Offset: 0x002B0800
		public virtual void Load()
		{
			SaveManager saveManager = FungusManager.Instance.SaveManager;
			if (saveManager.SaveDataExists(this.saveDataKey))
			{
				this.PlayClickSound();
				saveManager.Load(this.saveDataKey);
			}
		}

		// Token: 0x06007626 RID: 30246 RVA: 0x002B2638 File Offset: 0x002B0838
		public virtual void Rewind()
		{
			this.PlayClickSound();
			SaveManager saveManager = FungusManager.Instance.SaveManager;
			if (saveManager.NumSavePoints > 0)
			{
				saveManager.Rewind();
			}
		}

		// Token: 0x06007627 RID: 30247 RVA: 0x002B2668 File Offset: 0x002B0868
		public virtual void FastForward()
		{
			this.PlayClickSound();
			SaveManager saveManager = FungusManager.Instance.SaveManager;
			if (saveManager.NumRewoundSavePoints > 0)
			{
				saveManager.FastForward();
			}
		}

		// Token: 0x06007628 RID: 30248 RVA: 0x002B2698 File Offset: 0x002B0898
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

		// Token: 0x040066F6 RID: 26358
		[Tooltip("The string key used to store save game data in Player Prefs. If you have multiple games defined in the same Unity project, use a unique key for each one.")]
		[SerializeField]
		protected string saveDataKey = "save_data";

		// Token: 0x040066F7 RID: 26359
		[Tooltip("Automatically load the most recently saved game on startup")]
		[SerializeField]
		protected bool loadOnStart = true;

		// Token: 0x040066F8 RID: 26360
		[Tooltip("Automatically save game to disk after each Save Point command executes. This also disables the Save and Load menu buttons.")]
		[SerializeField]
		protected bool autoSave;

		// Token: 0x040066F9 RID: 26361
		[Tooltip("Delete the save game data from disk when player restarts the game. Useful for testing, but best switched off for release builds.")]
		[SerializeField]
		protected bool restartDeletesSave;

		// Token: 0x040066FA RID: 26362
		[Tooltip("The CanvasGroup containing the save menu buttons")]
		[SerializeField]
		protected CanvasGroup saveMenuGroup;

		// Token: 0x040066FB RID: 26363
		[Tooltip("The button which hides / displays the save menu")]
		[SerializeField]
		protected Button saveMenuButton;

		// Token: 0x040066FC RID: 26364
		[Tooltip("The button which saves the save history to disk")]
		[SerializeField]
		protected Button saveButton;

		// Token: 0x040066FD RID: 26365
		[Tooltip("The button which loads the save history from disk")]
		[SerializeField]
		protected Button loadButton;

		// Token: 0x040066FE RID: 26366
		[Tooltip("The button which rewinds the save history to the previous save point.")]
		[SerializeField]
		protected Button rewindButton;

		// Token: 0x040066FF RID: 26367
		[Tooltip("The button which fast forwards the save history to the next save point.")]
		[SerializeField]
		protected Button forwardButton;

		// Token: 0x04006700 RID: 26368
		[Tooltip("The button which restarts the game.")]
		[SerializeField]
		protected Button restartButton;

		// Token: 0x04006701 RID: 26369
		[Tooltip("A scrollable text field used for debugging the save data. The text field should be disabled in normal use.")]
		[SerializeField]
		protected ScrollRect debugView;

		// Token: 0x04006702 RID: 26370
		protected static bool saveMenuActive;

		// Token: 0x04006703 RID: 26371
		protected AudioSource clickAudioSource;

		// Token: 0x04006704 RID: 26372
		protected LTDescr fadeTween;

		// Token: 0x04006705 RID: 26373
		protected static SaveMenu instance;

		// Token: 0x04006706 RID: 26374
		protected static bool hasLoadedOnStart;
	}
}
