using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Token: 0x02000102 RID: 258
public class LoadingScreen : MonoBehaviour
{
	// Token: 0x06000BD9 RID: 3033 RVA: 0x00047BF4 File Offset: 0x00045DF4
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

	// Token: 0x06000BDA RID: 3034 RVA: 0x00004095 File Offset: 0x00002295
	public void playMusic()
	{
	}

	// Token: 0x06000BDB RID: 3035 RVA: 0x00047C28 File Offset: 0x00045E28
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

	// Token: 0x06000BDC RID: 3036 RVA: 0x00047C37 File Offset: 0x00045E37
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

	// Token: 0x06000BDD RID: 3037 RVA: 0x00047C46 File Offset: 0x00045E46
	private void RetrieveSceneToLoad()
	{
		this.sceneToLoad = PlayerPrefs.GetString("sceneToLoad");
		if (this.sceneToLoad == "")
		{
			this.sceneToLoad = "MainMenu";
		}
		PlayerPrefs.DeleteKey("sceneToLoad");
	}

	// Token: 0x06000BDE RID: 3038 RVA: 0x00047C80 File Offset: 0x00045E80
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

	// Token: 0x06000BDF RID: 3039 RVA: 0x00047CE8 File Offset: 0x00045EE8
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

	// Token: 0x06000BE0 RID: 3040 RVA: 0x00047E98 File Offset: 0x00046098
	private void TransitionFader()
	{
		this.transitionFader.Play("Transition");
	}

	// Token: 0x06000BE1 RID: 3041 RVA: 0x00047EAA File Offset: 0x000460AA
	private void enableContinueText()
	{
		this.tapInput = true;
		this.PressAnyKeyToContinue.SetActive(true);
		this.LoadingText.SetActive(false);
	}

	// Token: 0x06000BE2 RID: 3042 RVA: 0x00047ECB File Offset: 0x000460CB
	private void loadScene()
	{
		GameObject.Find("Fader").GetComponent<Animator>().GetComponent<Animator>().Play("Fader In");
		base.Invoke("load", 0.5f);
	}

	// Token: 0x06000BE3 RID: 3043 RVA: 0x00047EFC File Offset: 0x000460FC
	private void load()
	{
		EasyAudioUtility_SceneManager easyAudioUtility_SceneManager = Object.FindObjectOfType<EasyAudioUtility_SceneManager>();
		if (easyAudioUtility_SceneManager)
		{
			easyAudioUtility_SceneManager.onSceneChange(this.sceneToLoad);
		}
		Tools.instance.loadMapScenes(this.sceneToLoad, true);
	}

	// Token: 0x0400081C RID: 2076
	[Tooltip("Scene retrieved to load. If empty, it will load main menu.")]
	public string sceneToLoad;

	// Token: 0x0400081D RID: 2077
	[Header("Loading Bar")]
	[Tooltip("Loading Bar")]
	public Slider loadingBar;

	// Token: 0x0400081E RID: 2078
	[Tooltip("Show loading bar or circular loading indicator.")]
	public bool showLoadingBar;

	// Token: 0x0400081F RID: 2079
	[Tooltip("Loading Bar fill delay.")]
	public float fillDelay = 0.2f;

	// Token: 0x04000820 RID: 2080
	[Tooltip("Loading Bar fill speed.")]
	public float fillSpeed = 0.2f;

	// Token: 0x04000821 RID: 2081
	[Header("Circular Indicator")]
	[Tooltip("Circular loading delay.")]
	public GameObject circularIndicator;

	// Token: 0x04000822 RID: 2082
	[Tooltip("Scene Load Delay.")]
	public float circularLoadDelay = 6f;

	// Token: 0x04000823 RID: 2083
	[Tooltip("Circular Indicator rotation speed.")]
	public float circularIndicatorAnimSpeed = 1f;

	// Token: 0x04000824 RID: 2084
	[Header("Loading Screen Image Transition")]
	[Tooltip("Loading Screen image")]
	public Image defaultLoadingScreenImage;

	// Token: 0x04000825 RID: 2085
	[Tooltip("If it's true, images will show one after another, else any random image will be shown from below array.")]
	public bool showImageTransition = true;

	// Token: 0x04000826 RID: 2086
	[Tooltip("If it's true, RANDOM images will show one after another, else any random image will be shown from below array.")]
	public bool showRandomImageTransition = true;

	// Token: 0x04000827 RID: 2087
	[Tooltip("Add 1280x720 res images if it's landscape menu")]
	public Sprite[] LoadingScreenImages;

	// Token: 0x04000828 RID: 2088
	[Tooltip("How long an image will be displayed")]
	[Range(3f, 10f)]
	public float transitionDuration;

	// Token: 0x04000829 RID: 2089
	[Tooltip("Transition Fader")]
	public Animator transitionFader;

	// Token: 0x0400082A RID: 2090
	[Header("Continue Text Option")]
	[Tooltip("If true, scene will load after clicking / touching the screen!")]
	public bool showContinueText;

	// Token: 0x0400082B RID: 2091
	[Tooltip("Continue Text")]
	public GameObject PressAnyKeyToContinue;

	// Token: 0x0400082C RID: 2092
	public GameObject LoadingText;

	// Token: 0x0400082D RID: 2093
	[Header("Scene Specific Loading Screen")]
	[Tooltip("If you want to have specific loading screens for specific scene!")]
	public SceneSpecificLoading[] sceneSpecificLoading;

	// Token: 0x0400082E RID: 2094
	[HideInInspector]
	public int selectedTab1;

	// Token: 0x0400082F RID: 2095
	[HideInInspector]
	public int selectedTab2;

	// Token: 0x04000830 RID: 2096
	[HideInInspector]
	public int selectedTab3;

	// Token: 0x04000831 RID: 2097
	[HideInInspector]
	public string currentTab;

	// Token: 0x04000832 RID: 2098
	private AsyncOperation AsyncLoadScence;

	// Token: 0x04000833 RID: 2099
	private int i;

	// Token: 0x04000834 RID: 2100
	private int lastSprite;

	// Token: 0x04000835 RID: 2101
	private int cacheSprite;

	// Token: 0x04000836 RID: 2102
	private bool tapInput;
}
