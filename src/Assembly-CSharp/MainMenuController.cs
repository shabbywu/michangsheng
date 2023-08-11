using UnityEngine;
using YSGame;

public class MainMenuController : MonoBehaviour
{
	private Animator anim;

	public string newGameSceneName;

	public int quickSaveSlotID;

	[Header("Options Panel")]
	public GameObject MainOptionsPanel;

	public GameObject StartGameOptionsPanel;

	public GameObject ReconfigPanel;

	public GameObject GamePanel;

	public GameObject ControlsPanel;

	public GameObject GfxPanel;

	public GameObject LoadGamePanel;

	public GameObject LoadAvatarPanel;

	public GameObject LoadAvatarCellPanel;

	public GameObject DouFaPanel;

	public LevelSelectShengXianMag shengXianMag;

	public GameObject Set_UI;

	private void Start()
	{
		MusicMag.instance.playMusic(0);
		anim = ((Component)this).GetComponent<Animator>();
		PlayerPrefs.SetInt("quickSaveSlot", quickSaveSlotID);
	}

	public void openOptions()
	{
		MainOptionsPanel.SetActive(true);
		StartGameOptionsPanel.SetActive(false);
		ReconfigPanel.SetActive(false);
		anim.Play("buttonTweenAnims_on");
		playClickSound();
	}

	public void openStartGameOptions()
	{
		MainOptionsPanel.SetActive(false);
		StartGameOptionsPanel.SetActive(true);
		ReconfigPanel.SetActive(false);
		anim.Play("buttonTweenAnims_on");
		playClickSound();
	}

	public void openReconfig()
	{
		MainOptionsPanel.SetActive(false);
		StartGameOptionsPanel.SetActive(false);
		ReconfigPanel.SetActive(true);
		anim.Play("OptTweenAnim_off");
		playClickSound();
	}

	public void openOptions_Game()
	{
		GamePanel.SetActive(true);
		ControlsPanel.SetActive(false);
		GfxPanel.SetActive(false);
		LoadGamePanel.SetActive(false);
		anim.Play("OptTweenAnim_on");
		playClickSound();
	}

	public void openOptions_Controls()
	{
		GamePanel.SetActive(false);
		ControlsPanel.SetActive(true);
		GfxPanel.SetActive(false);
		LoadGamePanel.SetActive(false);
		anim.Play("OptTweenAnim_on");
		playClickSound();
	}

	public void openOptions_Gfx()
	{
		GamePanel.SetActive(false);
		ControlsPanel.SetActive(false);
		GfxPanel.SetActive(true);
		LoadGamePanel.SetActive(false);
		anim.Play("OptTweenAnim_on");
		playClickSound();
	}

	public void openContinue_Load()
	{
		LoadAvatarPanel.SetActive(true);
	}

	public void openDouFa()
	{
		if (!shengXianMag.isInit)
		{
			shengXianMag.reloadUI();
		}
		DouFaPanel.SetActive(true);
	}

	public void YsCloseLoadGameUI()
	{
		LoadAvatarCellPanel.gameObject.SetActive(false);
	}

	public void setStartGameOptionsPanel(bool b)
	{
		StartGameOptionsPanel.SetActive(b);
	}

	public void YSOpenSet()
	{
		Set_UI.SetActive(true);
		Set_UI.GetComponentInChildren<Animation>().Play("tankuanglachuputong");
	}

	public void YSCloseSet()
	{
		Set_UI.SetActive(false);
	}

	public void openContinue_LoadAvatar()
	{
		if (!((Component)this).GetComponent<LevelSelectAvatarManager>().isInit)
		{
			((Component)this).GetComponent<LevelSelectAvatarManager>().reloadUI();
		}
		((Component)this).GetComponent<LevelSelectAvatarManager>().openLevelSelect();
	}

	public void newGame()
	{
		if (!Object.op_Implicit((Object)(object)((Component)this).GetComponent<LevelSelectManager>()))
		{
			PlayerPrefs.SetString("sceneToLoad", newGameSceneName);
			Object.FindObjectOfType<Fader>().FadeIntoLevel("LoadingScreen");
		}
		else
		{
			((Component)this).GetComponent<LevelSelectManager>().openLevelSelect();
		}
		PlayerPrefs.DeleteKey("slotLoaded_");
	}

	public void back_options()
	{
		anim.Play("buttonTweenAnims_off");
		playClickSound();
	}

	public void back_options_panels()
	{
		anim.Play("OptTweenAnim_off");
		playClickSound();
	}

	public void Quit()
	{
		Application.Quit();
	}

	public void playHoverClip()
	{
		if ((Object)(object)EasyAudioUtility.instance != (Object)null)
		{
			EasyAudioUtility.instance.Play("Hover");
		}
	}

	private void playClickSound()
	{
		if ((Object)(object)EasyAudioUtility.instance != (Object)null)
		{
			EasyAudioUtility.instance.Play("Click");
		}
	}
}
