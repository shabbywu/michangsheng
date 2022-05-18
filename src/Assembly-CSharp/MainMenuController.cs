using System;
using UnityEngine;
using YSGame;

// Token: 0x0200018B RID: 395
public class MainMenuController : MonoBehaviour
{
	// Token: 0x06000D22 RID: 3362 RVA: 0x0000EDB9 File Offset: 0x0000CFB9
	private void Start()
	{
		MusicMag.instance.playMusic(0);
		this.anim = base.GetComponent<Animator>();
		PlayerPrefs.SetInt("quickSaveSlot", this.quickSaveSlotID);
	}

	// Token: 0x06000D23 RID: 3363 RVA: 0x0000EDE2 File Offset: 0x0000CFE2
	public void openOptions()
	{
		this.MainOptionsPanel.SetActive(true);
		this.StartGameOptionsPanel.SetActive(false);
		this.ReconfigPanel.SetActive(false);
		this.anim.Play("buttonTweenAnims_on");
		this.playClickSound();
	}

	// Token: 0x06000D24 RID: 3364 RVA: 0x0000EE1E File Offset: 0x0000D01E
	public void openStartGameOptions()
	{
		this.MainOptionsPanel.SetActive(false);
		this.StartGameOptionsPanel.SetActive(true);
		this.ReconfigPanel.SetActive(false);
		this.anim.Play("buttonTweenAnims_on");
		this.playClickSound();
	}

	// Token: 0x06000D25 RID: 3365 RVA: 0x0000EE5A File Offset: 0x0000D05A
	public void openReconfig()
	{
		this.MainOptionsPanel.SetActive(false);
		this.StartGameOptionsPanel.SetActive(false);
		this.ReconfigPanel.SetActive(true);
		this.anim.Play("OptTweenAnim_off");
		this.playClickSound();
	}

	// Token: 0x06000D26 RID: 3366 RVA: 0x0009AB24 File Offset: 0x00098D24
	public void openOptions_Game()
	{
		this.GamePanel.SetActive(true);
		this.ControlsPanel.SetActive(false);
		this.GfxPanel.SetActive(false);
		this.LoadGamePanel.SetActive(false);
		this.anim.Play("OptTweenAnim_on");
		this.playClickSound();
	}

	// Token: 0x06000D27 RID: 3367 RVA: 0x0009AB78 File Offset: 0x00098D78
	public void openOptions_Controls()
	{
		this.GamePanel.SetActive(false);
		this.ControlsPanel.SetActive(true);
		this.GfxPanel.SetActive(false);
		this.LoadGamePanel.SetActive(false);
		this.anim.Play("OptTweenAnim_on");
		this.playClickSound();
	}

	// Token: 0x06000D28 RID: 3368 RVA: 0x0009ABCC File Offset: 0x00098DCC
	public void openOptions_Gfx()
	{
		this.GamePanel.SetActive(false);
		this.ControlsPanel.SetActive(false);
		this.GfxPanel.SetActive(true);
		this.LoadGamePanel.SetActive(false);
		this.anim.Play("OptTweenAnim_on");
		this.playClickSound();
	}

	// Token: 0x06000D29 RID: 3369 RVA: 0x0000EE96 File Offset: 0x0000D096
	public void openContinue_Load()
	{
		this.LoadAvatarPanel.SetActive(true);
	}

	// Token: 0x06000D2A RID: 3370 RVA: 0x0000EEA4 File Offset: 0x0000D0A4
	public void openDouFa()
	{
		if (!this.shengXianMag.isInit)
		{
			this.shengXianMag.reloadUI();
		}
		this.DouFaPanel.SetActive(true);
	}

	// Token: 0x06000D2B RID: 3371 RVA: 0x0000EECA File Offset: 0x0000D0CA
	public void YsCloseLoadGameUI()
	{
		this.LoadAvatarCellPanel.gameObject.SetActive(false);
	}

	// Token: 0x06000D2C RID: 3372 RVA: 0x0000EEDD File Offset: 0x0000D0DD
	public void setStartGameOptionsPanel(bool b)
	{
		this.StartGameOptionsPanel.SetActive(b);
	}

	// Token: 0x06000D2D RID: 3373 RVA: 0x0000EEEB File Offset: 0x0000D0EB
	public void YSOpenSet()
	{
		this.Set_UI.SetActive(true);
		this.Set_UI.GetComponentInChildren<Animation>().Play("tankuanglachuputong");
	}

	// Token: 0x06000D2E RID: 3374 RVA: 0x0000EF0F File Offset: 0x0000D10F
	public void YSCloseSet()
	{
		this.Set_UI.SetActive(false);
	}

	// Token: 0x06000D2F RID: 3375 RVA: 0x0000EF1D File Offset: 0x0000D11D
	public void openContinue_LoadAvatar()
	{
		if (!base.GetComponent<LevelSelectAvatarManager>().isInit)
		{
			base.GetComponent<LevelSelectAvatarManager>().reloadUI();
		}
		base.GetComponent<LevelSelectAvatarManager>().openLevelSelect();
	}

	// Token: 0x06000D30 RID: 3376 RVA: 0x0009AC20 File Offset: 0x00098E20
	public void newGame()
	{
		if (!base.GetComponent<LevelSelectManager>())
		{
			PlayerPrefs.SetString("sceneToLoad", this.newGameSceneName);
			Object.FindObjectOfType<Fader>().FadeIntoLevel("LoadingScreen");
		}
		else
		{
			base.GetComponent<LevelSelectManager>().openLevelSelect();
		}
		PlayerPrefs.DeleteKey("slotLoaded_");
	}

	// Token: 0x06000D31 RID: 3377 RVA: 0x0000EF42 File Offset: 0x0000D142
	public void back_options()
	{
		this.anim.Play("buttonTweenAnims_off");
		this.playClickSound();
	}

	// Token: 0x06000D32 RID: 3378 RVA: 0x0000EF5A File Offset: 0x0000D15A
	public void back_options_panels()
	{
		this.anim.Play("OptTweenAnim_off");
		this.playClickSound();
	}

	// Token: 0x06000D33 RID: 3379 RVA: 0x0000EF72 File Offset: 0x0000D172
	public void Quit()
	{
		Application.Quit();
	}

	// Token: 0x06000D34 RID: 3380 RVA: 0x0000EF79 File Offset: 0x0000D179
	public void playHoverClip()
	{
		if (EasyAudioUtility.instance != null)
		{
			EasyAudioUtility.instance.Play("Hover");
		}
	}

	// Token: 0x06000D35 RID: 3381 RVA: 0x0000EF97 File Offset: 0x0000D197
	private void playClickSound()
	{
		if (EasyAudioUtility.instance != null)
		{
			EasyAudioUtility.instance.Play("Click");
		}
	}

	// Token: 0x04000A56 RID: 2646
	private Animator anim;

	// Token: 0x04000A57 RID: 2647
	public string newGameSceneName;

	// Token: 0x04000A58 RID: 2648
	public int quickSaveSlotID;

	// Token: 0x04000A59 RID: 2649
	[Header("Options Panel")]
	public GameObject MainOptionsPanel;

	// Token: 0x04000A5A RID: 2650
	public GameObject StartGameOptionsPanel;

	// Token: 0x04000A5B RID: 2651
	public GameObject ReconfigPanel;

	// Token: 0x04000A5C RID: 2652
	public GameObject GamePanel;

	// Token: 0x04000A5D RID: 2653
	public GameObject ControlsPanel;

	// Token: 0x04000A5E RID: 2654
	public GameObject GfxPanel;

	// Token: 0x04000A5F RID: 2655
	public GameObject LoadGamePanel;

	// Token: 0x04000A60 RID: 2656
	public GameObject LoadAvatarPanel;

	// Token: 0x04000A61 RID: 2657
	public GameObject LoadAvatarCellPanel;

	// Token: 0x04000A62 RID: 2658
	public GameObject DouFaPanel;

	// Token: 0x04000A63 RID: 2659
	public LevelSelectShengXianMag shengXianMag;

	// Token: 0x04000A64 RID: 2660
	public GameObject Set_UI;
}
