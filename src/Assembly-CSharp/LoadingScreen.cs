using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Token: 0x02000180 RID: 384
public class LoadingScreen : MonoBehaviour
{
	// Token: 0x06000CE4 RID: 3300 RVA: 0x0000EA7F File Offset: 0x0000CC7F
	private void Start()
	{
		if (Tools.instance.isNewAvatar)
		{
			Tools.instance.isNewAvatar = false;
			base.StartCoroutine(this.NewPlayerLoadScene());
			return;
		}
		base.StartCoroutine(this.LoadScene());
	}

	// Token: 0x06000CE5 RID: 3301 RVA: 0x000042DD File Offset: 0x000024DD
	public void playMusic()
	{
	}

	// Token: 0x06000CE6 RID: 3302 RVA: 0x0000EAB3 File Offset: 0x0000CCB3
	private IEnumerator NewPlayerLoadScene()
	{
		base.GetComponent<Canvas>().renderMode = 0;
		this.RetrieveSceneToLoad();
		PanelMamager.inst.destoryUIGameObjet();
		if (PanelMamager.inst.UIBlackMaskGameObject == null)
		{
			Object.Instantiate<GameObject>(ResManager.inst.LoadPrefab("BlackHide"));
		}
		if (this.showLoadingBar)
		{
			this.loadingBar.minValue = 0f;
			this.loadingBar.maxValue = 1f;
			this.loadingBar.value = 0f;
			this.loadingBar.gameObject.SetActive(true);
			this.circularIndicator.SetActive(false);
		}
		else
		{
			this.loadingBar.gameObject.SetActive(false);
			this.circularIndicator.SetActive(true);
			this.circularIndicator.GetComponent<Animator>().speed = this.circularIndicatorAnimSpeed;
		}
		EasyAudioUtility_SceneManager easyAudioUtility_SceneManager = Object.FindObjectOfType<EasyAudioUtility_SceneManager>();
		if (easyAudioUtility_SceneManager)
		{
			easyAudioUtility_SceneManager.FadeVolume(0f);
		}
		if (this.showImageTransition)
		{
			this.defaultLoadingScreenImage.color = Color.white;
			base.InvokeRepeating("StartImageTransition", 0f, this.transitionDuration);
		}
		else if (this.LoadingScreenImages.Length != 0)
		{
			this.defaultLoadingScreenImage.color = Color.white;
			this.defaultLoadingScreenImage.sprite = this.LoadingScreenImages[Random.Range(0, this.LoadingScreenImages.Length)];
		}
		this.AsyncLoadScence = SceneManager.LoadSceneAsync(this.sceneToLoad);
		this.AsyncLoadScence.allowSceneActivation = false;
		Object.Instantiate<GameObject>(ResManager.inst.LoadPrefab("GameObject"));
		while (this.AsyncLoadScence.progress <= 0.9f)
		{
			this.loadingBar.value += 0.003f;
			if (this.AsyncLoadScence.progress == 0.9f)
			{
				IL_23A:
				while (!FactoryManager.inst.createNewPlayerFactory.isCreateComplete)
				{
					this.loadingBar.value += 0.0015f;
					yield return new WaitForEndOfFrame();
				}
				Tools.instance.getPlayer().StreamData.FungusSaveMgr.LoadTalk();
				if (AuToSLMgr.Inst == null)
				{
				}
				while (this.loadingBar.value < 1f)
				{
					this.loadingBar.value += 0.002f;
					yield return new WaitForEndOfFrame();
				}
				Tools.instance.isNeedSetTalk = true;
				Tools.jumpToName = this.sceneToLoad;
				Tools.instance.loadSceneType = 1;
				this.AsyncLoadScence.allowSceneActivation = true;
				yield break;
			}
			yield return new WaitForEndOfFrame();
		}
		goto IL_23A;
	}

	// Token: 0x06000CE7 RID: 3303 RVA: 0x0000EAC2 File Offset: 0x0000CCC2
	private IEnumerator LoadScene()
	{
		base.GetComponent<Canvas>().renderMode = 0;
		Tools.instance.isNeedSetTalk = false;
		this.RetrieveSceneToLoad();
		PanelMamager.inst.destoryUIGameObjet();
		if (PanelMamager.inst.UIBlackMaskGameObject == null)
		{
			Object.Instantiate<GameObject>(ResManager.inst.LoadPrefab("BlackHide"));
		}
		if (this.showLoadingBar)
		{
			this.loadingBar.minValue = 0f;
			this.loadingBar.maxValue = 1f;
			this.loadingBar.value = 0f;
			this.loadingBar.gameObject.SetActive(true);
			this.circularIndicator.SetActive(false);
		}
		else
		{
			this.loadingBar.gameObject.SetActive(false);
			this.circularIndicator.SetActive(true);
			this.circularIndicator.GetComponent<Animator>().speed = this.circularIndicatorAnimSpeed;
		}
		EasyAudioUtility_SceneManager easyAudioUtility_SceneManager = Object.FindObjectOfType<EasyAudioUtility_SceneManager>();
		if (easyAudioUtility_SceneManager)
		{
			easyAudioUtility_SceneManager.FadeVolume(0f);
		}
		if (this.showImageTransition)
		{
			this.defaultLoadingScreenImage.color = Color.white;
			base.InvokeRepeating("StartImageTransition", 0f, this.transitionDuration);
		}
		else if (this.LoadingScreenImages.Length != 0)
		{
			this.defaultLoadingScreenImage.color = Color.white;
			this.defaultLoadingScreenImage.sprite = this.LoadingScreenImages[Random.Range(0, this.LoadingScreenImages.Length)];
		}
		this.AsyncLoadScence = SceneManager.LoadSceneAsync(this.sceneToLoad);
		this.AsyncLoadScence.allowSceneActivation = false;
		Object.Instantiate<GameObject>(ResManager.inst.LoadPrefab("GameObject"));
		while (this.AsyncLoadScence.progress <= 0.9f)
		{
			this.loadingBar.value += 0.04f;
			if (this.AsyncLoadScence.progress == 0.9f)
			{
				IL_257:
				while (!FactoryManager.inst.loadPlayerDateFactory.isLoadComplete)
				{
					if (this.loadingBar.value <= 0.8f)
					{
						this.loadingBar.value += 0.001f;
					}
					yield return new WaitForEndOfFrame();
				}
				for (;;)
				{
					this.loadingBar.value += 0.03f;
					if (this.loadingBar.value >= 0.9f)
					{
						break;
					}
					yield return new WaitForEndOfFrame();
				}
				if (Tools.instance.IsLoadData)
				{
					Tools.instance.getPlayer().StreamData.FungusSaveMgr.LoadTalk();
				}
				else
				{
					Tools.instance.IsLoadData = false;
				}
				AuToSLMgr.Inst == null;
				Tools.instance.isNeedSetTalk = true;
				Tools.jumpToName = this.sceneToLoad;
				Tools.instance.loadSceneType = 1;
				this.AsyncLoadScence.allowSceneActivation = true;
				Tools.instance.isNeedSetTalk = true;
				Tools.jumpToName = this.sceneToLoad;
				Tools.instance.loadSceneType = 1;
				this.AsyncLoadScence.allowSceneActivation = true;
				yield break;
			}
			yield return new WaitForEndOfFrame();
		}
		goto IL_257;
	}

	// Token: 0x06000CE8 RID: 3304 RVA: 0x0000EAD1 File Offset: 0x0000CCD1
	private void RetrieveSceneToLoad()
	{
		this.sceneToLoad = PlayerPrefs.GetString("sceneToLoad");
		if (this.sceneToLoad == "")
		{
			this.sceneToLoad = "MainMenu";
		}
		PlayerPrefs.DeleteKey("sceneToLoad");
	}

	// Token: 0x06000CE9 RID: 3305 RVA: 0x00099440 File Offset: 0x00097640
	private void fillLoadingBar()
	{
		this.loadingBar.value += (float)Random.Range(0, 10);
		if (this.loadingBar.value == 100f)
		{
			Debug.Log("load scene");
			if (!this.showContinueText)
			{
				this.loadScene();
			}
			else
			{
				this.enableContinueText();
			}
			base.CancelInvoke("fillLoadingBar");
		}
	}

	// Token: 0x06000CEA RID: 3306 RVA: 0x000994A8 File Offset: 0x000976A8
	private void StartImageTransition()
	{
		if (this.i < this.LoadingScreenImages.Length)
		{
			this.defaultLoadingScreenImage.sprite = this.LoadingScreenImages[this.i];
			if (this.showRandomImageTransition)
			{
				this.cacheSprite = this.lastSprite;
				this.lastSprite = Random.Range(0, this.LoadingScreenImages.Length);
				if (this.cacheSprite != this.lastSprite)
				{
					this.defaultLoadingScreenImage.sprite = this.LoadingScreenImages[this.lastSprite];
				}
				else
				{
					this.lastSprite = Random.Range(0, this.LoadingScreenImages.Length);
					this.defaultLoadingScreenImage.sprite = this.LoadingScreenImages[this.lastSprite];
				}
			}
			base.CancelInvoke("TransitionFader");
			base.Invoke("TransitionFader", this.transitionDuration - 0.5f);
			this.i++;
			return;
		}
		this.i = 0;
		this.defaultLoadingScreenImage.sprite = this.LoadingScreenImages[this.i];
		if (this.showRandomImageTransition)
		{
			this.cacheSprite = this.lastSprite;
			this.lastSprite = Random.Range(0, this.LoadingScreenImages.Length);
			if (this.cacheSprite != this.lastSprite)
			{
				this.defaultLoadingScreenImage.sprite = this.LoadingScreenImages[this.lastSprite];
			}
			else
			{
				this.lastSprite = Random.Range(0, this.LoadingScreenImages.Length);
				this.defaultLoadingScreenImage.sprite = this.LoadingScreenImages[this.lastSprite];
			}
		}
		base.CancelInvoke("TransitionFader");
		base.Invoke("TransitionFader", this.transitionDuration - 0.5f);
		this.i++;
	}

	// Token: 0x06000CEB RID: 3307 RVA: 0x0000EB0A File Offset: 0x0000CD0A
	private void TransitionFader()
	{
		this.transitionFader.Play("Transition");
	}

	// Token: 0x06000CEC RID: 3308 RVA: 0x0000EB1C File Offset: 0x0000CD1C
	private void enableContinueText()
	{
		this.tapInput = true;
		this.PressAnyKeyToContinue.SetActive(true);
		this.LoadingText.SetActive(false);
	}

	// Token: 0x06000CED RID: 3309 RVA: 0x0000EB3D File Offset: 0x0000CD3D
	private void loadScene()
	{
		GameObject.Find("Fader").GetComponent<Animator>().GetComponent<Animator>().Play("Fader In");
		base.Invoke("load", 0.5f);
	}

	// Token: 0x06000CEE RID: 3310 RVA: 0x00099658 File Offset: 0x00097858
	private void load()
	{
		EasyAudioUtility_SceneManager easyAudioUtility_SceneManager = Object.FindObjectOfType<EasyAudioUtility_SceneManager>();
		if (easyAudioUtility_SceneManager)
		{
			easyAudioUtility_SceneManager.onSceneChange(this.sceneToLoad);
		}
		Tools.instance.loadMapScenes(this.sceneToLoad, true);
	}

	// Token: 0x04000A0B RID: 2571
	[Tooltip("Scene retrieved to load. If empty, it will load main menu.")]
	public string sceneToLoad;

	// Token: 0x04000A0C RID: 2572
	[Header("Loading Bar")]
	[Tooltip("Loading Bar")]
	public Slider loadingBar;

	// Token: 0x04000A0D RID: 2573
	[Tooltip("Show loading bar or circular loading indicator.")]
	public bool showLoadingBar;

	// Token: 0x04000A0E RID: 2574
	[Tooltip("Loading Bar fill delay.")]
	public float fillDelay = 0.2f;

	// Token: 0x04000A0F RID: 2575
	[Tooltip("Loading Bar fill speed.")]
	public float fillSpeed = 0.2f;

	// Token: 0x04000A10 RID: 2576
	[Header("Circular Indicator")]
	[Tooltip("Circular loading delay.")]
	public GameObject circularIndicator;

	// Token: 0x04000A11 RID: 2577
	[Tooltip("Scene Load Delay.")]
	public float circularLoadDelay = 6f;

	// Token: 0x04000A12 RID: 2578
	[Tooltip("Circular Indicator rotation speed.")]
	public float circularIndicatorAnimSpeed = 1f;

	// Token: 0x04000A13 RID: 2579
	[Header("Loading Screen Image Transition")]
	[Tooltip("Loading Screen image")]
	public Image defaultLoadingScreenImage;

	// Token: 0x04000A14 RID: 2580
	[Tooltip("If it's true, images will show one after another, else any random image will be shown from below array.")]
	public bool showImageTransition = true;

	// Token: 0x04000A15 RID: 2581
	[Tooltip("If it's true, RANDOM images will show one after another, else any random image will be shown from below array.")]
	public bool showRandomImageTransition = true;

	// Token: 0x04000A16 RID: 2582
	[Tooltip("Add 1280x720 res images if it's landscape menu")]
	public Sprite[] LoadingScreenImages;

	// Token: 0x04000A17 RID: 2583
	[Tooltip("How long an image will be displayed")]
	[Range(3f, 10f)]
	public float transitionDuration;

	// Token: 0x04000A18 RID: 2584
	[Tooltip("Transition Fader")]
	public Animator transitionFader;

	// Token: 0x04000A19 RID: 2585
	[Header("Continue Text Option")]
	[Tooltip("If true, scene will load after clicking / touching the screen!")]
	public bool showContinueText;

	// Token: 0x04000A1A RID: 2586
	[Tooltip("Continue Text")]
	public GameObject PressAnyKeyToContinue;

	// Token: 0x04000A1B RID: 2587
	public GameObject LoadingText;

	// Token: 0x04000A1C RID: 2588
	[Header("Scene Specific Loading Screen")]
	[Tooltip("If you want to have specific loading screens for specific scene!")]
	public SceneSpecificLoading[] sceneSpecificLoading;

	// Token: 0x04000A1D RID: 2589
	[HideInInspector]
	public int selectedTab1;

	// Token: 0x04000A1E RID: 2590
	[HideInInspector]
	public int selectedTab2;

	// Token: 0x04000A1F RID: 2591
	[HideInInspector]
	public int selectedTab3;

	// Token: 0x04000A20 RID: 2592
	[HideInInspector]
	public string currentTab;

	// Token: 0x04000A21 RID: 2593
	private AsyncOperation AsyncLoadScence;

	// Token: 0x04000A22 RID: 2594
	private int i;

	// Token: 0x04000A23 RID: 2595
	private int lastSprite;

	// Token: 0x04000A24 RID: 2596
	private int cacheSprite;

	// Token: 0x04000A25 RID: 2597
	private bool tapInput;
}
