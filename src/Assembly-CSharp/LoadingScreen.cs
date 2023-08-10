using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
	[Tooltip("Scene retrieved to load. If empty, it will load main menu.")]
	public string sceneToLoad;

	[Header("Loading Bar")]
	[Tooltip("Loading Bar")]
	public Slider loadingBar;

	[Tooltip("Show loading bar or circular loading indicator.")]
	public bool showLoadingBar;

	[Tooltip("Loading Bar fill delay.")]
	public float fillDelay = 0.2f;

	[Tooltip("Loading Bar fill speed.")]
	public float fillSpeed = 0.2f;

	[Header("Circular Indicator")]
	[Tooltip("Circular loading delay.")]
	public GameObject circularIndicator;

	[Tooltip("Scene Load Delay.")]
	public float circularLoadDelay = 6f;

	[Tooltip("Circular Indicator rotation speed.")]
	public float circularIndicatorAnimSpeed = 1f;

	[Header("Loading Screen Image Transition")]
	[Tooltip("Loading Screen image")]
	public Image defaultLoadingScreenImage;

	[Tooltip("If it's true, images will show one after another, else any random image will be shown from below array.")]
	public bool showImageTransition = true;

	[Tooltip("If it's true, RANDOM images will show one after another, else any random image will be shown from below array.")]
	public bool showRandomImageTransition = true;

	[Tooltip("Add 1280x720 res images if it's landscape menu")]
	public Sprite[] LoadingScreenImages;

	[Tooltip("How long an image will be displayed")]
	[Range(3f, 10f)]
	public float transitionDuration;

	[Tooltip("Transition Fader")]
	public Animator transitionFader;

	[Header("Continue Text Option")]
	[Tooltip("If true, scene will load after clicking / touching the screen!")]
	public bool showContinueText;

	[Tooltip("Continue Text")]
	public GameObject PressAnyKeyToContinue;

	public GameObject LoadingText;

	[Header("Scene Specific Loading Screen")]
	[Tooltip("If you want to have specific loading screens for specific scene!")]
	public SceneSpecificLoading[] sceneSpecificLoading;

	[HideInInspector]
	public int selectedTab1;

	[HideInInspector]
	public int selectedTab2;

	[HideInInspector]
	public int selectedTab3;

	[HideInInspector]
	public string currentTab;

	private AsyncOperation AsyncLoadScence;

	private int i;

	private int lastSprite;

	private int cacheSprite;

	private bool tapInput;

	private void Start()
	{
		if (Tools.instance.isNewAvatar)
		{
			Tools.instance.isNewAvatar = false;
			((MonoBehaviour)this).StartCoroutine(NewPlayerLoadScene());
		}
		else
		{
			((MonoBehaviour)this).StartCoroutine(LoadScene());
		}
	}

	public void playMusic()
	{
	}

	private IEnumerator NewPlayerLoadScene()
	{
		((Component)this).GetComponent<Canvas>().renderMode = (RenderMode)0;
		RetrieveSceneToLoad();
		PanelMamager.inst.destoryUIGameObjet();
		if ((Object)(object)PanelMamager.inst.UIBlackMaskGameObject == (Object)null)
		{
			Object.Instantiate<GameObject>(ResManager.inst.LoadPrefab("BlackHide"));
		}
		if (showLoadingBar)
		{
			loadingBar.minValue = 0f;
			loadingBar.maxValue = 1f;
			loadingBar.value = 0f;
			((Component)loadingBar).gameObject.SetActive(true);
			circularIndicator.SetActive(false);
		}
		else
		{
			((Component)loadingBar).gameObject.SetActive(false);
			circularIndicator.SetActive(true);
			circularIndicator.GetComponent<Animator>().speed = circularIndicatorAnimSpeed;
		}
		EasyAudioUtility_SceneManager easyAudioUtility_SceneManager = Object.FindObjectOfType<EasyAudioUtility_SceneManager>();
		if (Object.op_Implicit((Object)(object)easyAudioUtility_SceneManager))
		{
			easyAudioUtility_SceneManager.FadeVolume(0f);
		}
		if (showImageTransition)
		{
			((Graphic)defaultLoadingScreenImage).color = Color.white;
			((MonoBehaviour)this).InvokeRepeating("StartImageTransition", 0f, transitionDuration);
		}
		else if (LoadingScreenImages.Length != 0)
		{
			((Graphic)defaultLoadingScreenImage).color = Color.white;
			defaultLoadingScreenImage.sprite = LoadingScreenImages[Random.Range(0, LoadingScreenImages.Length)];
		}
		AsyncLoadScence = SceneManager.LoadSceneAsync(sceneToLoad);
		AsyncLoadScence.allowSceneActivation = false;
		Object.Instantiate<GameObject>(ResManager.inst.LoadPrefab("GameObject"));
		while (AsyncLoadScence.progress <= 0.9f)
		{
			Slider obj = loadingBar;
			obj.value += 0.003f;
			if (AsyncLoadScence.progress == 0.9f)
			{
				break;
			}
			yield return (object)new WaitForEndOfFrame();
		}
		while (!FactoryManager.inst.createNewPlayerFactory.isCreateComplete)
		{
			Slider obj2 = loadingBar;
			obj2.value += 0.0015f;
			yield return (object)new WaitForEndOfFrame();
		}
		Tools.instance.getPlayer().StreamData.FungusSaveMgr.LoadTalk();
		if ((Object)(object)AuToSLMgr.Inst == (Object)null)
		{
		}
		while (loadingBar.value < 1f)
		{
			Slider obj3 = loadingBar;
			obj3.value += 0.002f;
			yield return (object)new WaitForEndOfFrame();
		}
		Tools.instance.isNeedSetTalk = true;
		Tools.jumpToName = sceneToLoad;
		Tools.instance.loadSceneType = 1;
		AsyncLoadScence.allowSceneActivation = true;
	}

	private IEnumerator LoadScene()
	{
		((Component)this).GetComponent<Canvas>().renderMode = (RenderMode)0;
		Tools.instance.isNeedSetTalk = false;
		RetrieveSceneToLoad();
		PanelMamager.inst.destoryUIGameObjet();
		if ((Object)(object)PanelMamager.inst.UIBlackMaskGameObject == (Object)null)
		{
			Object.Instantiate<GameObject>(ResManager.inst.LoadPrefab("BlackHide"));
		}
		if (showLoadingBar)
		{
			loadingBar.minValue = 0f;
			loadingBar.maxValue = 1f;
			loadingBar.value = 0f;
			((Component)loadingBar).gameObject.SetActive(true);
			circularIndicator.SetActive(false);
		}
		else
		{
			((Component)loadingBar).gameObject.SetActive(false);
			circularIndicator.SetActive(true);
			circularIndicator.GetComponent<Animator>().speed = circularIndicatorAnimSpeed;
		}
		EasyAudioUtility_SceneManager easyAudioUtility_SceneManager = Object.FindObjectOfType<EasyAudioUtility_SceneManager>();
		if (Object.op_Implicit((Object)(object)easyAudioUtility_SceneManager))
		{
			easyAudioUtility_SceneManager.FadeVolume(0f);
		}
		if (showImageTransition)
		{
			((Graphic)defaultLoadingScreenImage).color = Color.white;
			((MonoBehaviour)this).InvokeRepeating("StartImageTransition", 0f, transitionDuration);
		}
		else if (LoadingScreenImages.Length != 0)
		{
			((Graphic)defaultLoadingScreenImage).color = Color.white;
			defaultLoadingScreenImage.sprite = LoadingScreenImages[Random.Range(0, LoadingScreenImages.Length)];
		}
		AsyncLoadScence = SceneManager.LoadSceneAsync(sceneToLoad);
		AsyncLoadScence.allowSceneActivation = false;
		Object.Instantiate<GameObject>(ResManager.inst.LoadPrefab("GameObject"));
		while (AsyncLoadScence.progress <= 0.9f)
		{
			Slider obj = loadingBar;
			obj.value += 0.04f;
			if (AsyncLoadScence.progress == 0.9f)
			{
				break;
			}
			yield return (object)new WaitForEndOfFrame();
		}
		while (!FactoryManager.inst.loadPlayerDateFactory.isLoadComplete)
		{
			if (loadingBar.value <= 0.8f)
			{
				Slider obj2 = loadingBar;
				obj2.value += 0.001f;
			}
			yield return (object)new WaitForEndOfFrame();
		}
		while (true)
		{
			Slider obj3 = loadingBar;
			obj3.value += 0.03f;
			if (loadingBar.value >= 0.9f)
			{
				break;
			}
			yield return (object)new WaitForEndOfFrame();
		}
		if (Tools.instance.IsLoadData)
		{
			Tools.instance.getPlayer().StreamData.FungusSaveMgr.LoadTalk();
		}
		else
		{
			Tools.instance.IsLoadData = false;
		}
		_ = (Object)(object)AuToSLMgr.Inst == (Object)null;
		Tools.instance.isNeedSetTalk = true;
		Tools.jumpToName = sceneToLoad;
		Tools.instance.loadSceneType = 1;
		AsyncLoadScence.allowSceneActivation = true;
		Tools.instance.isNeedSetTalk = true;
		Tools.jumpToName = sceneToLoad;
		Tools.instance.loadSceneType = 1;
		AsyncLoadScence.allowSceneActivation = true;
	}

	private void RetrieveSceneToLoad()
	{
		sceneToLoad = PlayerPrefs.GetString("sceneToLoad");
		if (sceneToLoad == "")
		{
			sceneToLoad = "MainMenu";
		}
		PlayerPrefs.DeleteKey("sceneToLoad");
	}

	private void fillLoadingBar()
	{
		Slider obj = loadingBar;
		obj.value += (float)Random.Range(0, 10);
		if (loadingBar.value == 100f)
		{
			Debug.Log((object)"load scene");
			if (!showContinueText)
			{
				loadScene();
			}
			else
			{
				enableContinueText();
			}
			((MonoBehaviour)this).CancelInvoke("fillLoadingBar");
		}
	}

	private void StartImageTransition()
	{
		if (i < LoadingScreenImages.Length)
		{
			defaultLoadingScreenImage.sprite = LoadingScreenImages[i];
			if (showRandomImageTransition)
			{
				cacheSprite = lastSprite;
				lastSprite = Random.Range(0, LoadingScreenImages.Length);
				if (cacheSprite != lastSprite)
				{
					defaultLoadingScreenImage.sprite = LoadingScreenImages[lastSprite];
				}
				else
				{
					lastSprite = Random.Range(0, LoadingScreenImages.Length);
					defaultLoadingScreenImage.sprite = LoadingScreenImages[lastSprite];
				}
			}
			((MonoBehaviour)this).CancelInvoke("TransitionFader");
			((MonoBehaviour)this).Invoke("TransitionFader", transitionDuration - 0.5f);
			i++;
			return;
		}
		i = 0;
		defaultLoadingScreenImage.sprite = LoadingScreenImages[i];
		if (showRandomImageTransition)
		{
			cacheSprite = lastSprite;
			lastSprite = Random.Range(0, LoadingScreenImages.Length);
			if (cacheSprite != lastSprite)
			{
				defaultLoadingScreenImage.sprite = LoadingScreenImages[lastSprite];
			}
			else
			{
				lastSprite = Random.Range(0, LoadingScreenImages.Length);
				defaultLoadingScreenImage.sprite = LoadingScreenImages[lastSprite];
			}
		}
		((MonoBehaviour)this).CancelInvoke("TransitionFader");
		((MonoBehaviour)this).Invoke("TransitionFader", transitionDuration - 0.5f);
		i++;
	}

	private void TransitionFader()
	{
		transitionFader.Play("Transition");
	}

	private void enableContinueText()
	{
		tapInput = true;
		PressAnyKeyToContinue.SetActive(true);
		LoadingText.SetActive(false);
	}

	private void loadScene()
	{
		((Component)GameObject.Find("Fader").GetComponent<Animator>()).GetComponent<Animator>().Play("Fader In");
		((MonoBehaviour)this).Invoke("load", 0.5f);
	}

	private void load()
	{
		EasyAudioUtility_SceneManager easyAudioUtility_SceneManager = Object.FindObjectOfType<EasyAudioUtility_SceneManager>();
		if (Object.op_Implicit((Object)(object)easyAudioUtility_SceneManager))
		{
			easyAudioUtility_SceneManager.onSceneChange(sceneToLoad);
		}
		Tools.instance.loadMapScenes(sceneToLoad);
	}
}
