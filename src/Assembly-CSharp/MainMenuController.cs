using System;
using UnityEngine;
using YSGame;

// Token: 0x02000108 RID: 264
public class MainMenuController : MonoBehaviour
{
	// Token: 0x06000C07 RID: 3079 RVA: 0x00048F52 File Offset: 0x00047152
	private void Start()
	{
		MusicMag.instance.playMusic(0);
		this.anim = base.GetComponent<Animator>();
		PlayerPrefs.SetInt("quickSaveSlot", this.quickSaveSlotID);
	}

	// Token: 0x06000C08 RID: 3080 RVA: 0x00048F7B File Offset: 0x0004717B
	public void openOptions()
	{
		this.MainOptionsPanel.SetActive(true);
		this.StartGameOptionsPanel.SetActive(false);
		this.ReconfigPanel.SetActive(false);
		this.anim.Play("buttonTweenAnims_on");
		this.playClickSound();
	}

	// Token: 0x06000C09 RID: 3081 RVA: 0x00048FB7 File Offset: 0x000471B7
	public void openStartGameOptions()
	{
		this.MainOptionsPanel.SetActive(false);
		this.StartGameOptionsPanel.SetActive(true);
		this.ReconfigPanel.SetActive(false);
		this.anim.Play("buttonTweenAnims_on");
		this.playClickSound();
	}

	// Token: 0x06000C0A RID: 3082 RVA: 0x00048FF3 File Offset: 0x000471F3
	public void openReconfig()
	{
		this.MainOptionsPanel.SetActive(false);
		this.StartGameOptionsPanel.SetActive(false);
		this.ReconfigPanel.SetActive(true);
		this.anim.Play("OptTweenAnim_off");
		this.playClickSound();
	}

	// Token: 0x06000C0B RID: 3083 RVA: 0x00049030 File Offset: 0x00047230
	public void openOptions_Game()
	{
		this.GamePanel.SetActive(true);
		this.ControlsPanel.SetActive(false);
		this.GfxPanel.SetActive(false);
		this.LoadGamePanel.SetActive(false);
		this.anim.Play("OptTweenAnim_on");
		this.playClickSound();
	}

	// Token: 0x06000C0C RID: 3084 RVA: 0x00049084 File Offset: 0x00047284
	public void openOptions_Controls()
	{
		this.GamePanel.SetActive(false);
		this.ControlsPanel.SetActive(true);
		this.GfxPanel.SetActive(false);
		this.LoadGamePanel.SetActive(false);
		this.anim.Play("OptTweenAnim_on");
		this.playClickSound();
	}

	// Token: 0x06000C0D RID: 3085 RVA: 0x000490D8 File Offset: 0x000472D8
	public void openOptions_Gfx()
	{
		this.GamePanel.SetActive(false);
		this.ControlsPanel.SetActive(false);
		this.GfxPanel.SetActive(true);
		this.LoadGamePanel.SetActive(false);
		this.anim.Play("OptTweenAnim_on");
		this.playClickSound();
	}

	// Token: 0x06000C0E RID: 3086 RVA: 0x0004912B File Offset: 0x0004732B
	public void openContinue_Load()
	{
		this.LoadAvatarPanel.SetActive(true);
	}

	// Token: 0x06000C0F RID: 3087 RVA: 0x00049139 File Offset: 0x00047339
	public void openDouFa()
	{
		if (!this.shengXianMag.isInit)
		{
			this.shengXianMag.reloadUI();
		}
		this.DouFaPanel.SetActive(true);
	}

	// Token: 0x06000C10 RID: 3088 RVA: 0x0004915F File Offset: 0x0004735F
	public void YsCloseLoadGameUI()
	{
		this.LoadAvatarCellPanel.gameObject.SetActive(false);
	}

	// Token: 0x06000C11 RID: 3089 RVA: 0x00049172 File Offset: 0x00047372
	public void setStartGameOptionsPanel(bool b)
	{
		this.StartGameOptionsPanel.SetActive(b);
	}

	// Token: 0x06000C12 RID: 3090 RVA: 0x00049180 File Offset: 0x00047380
	public void YSOpenSet()
	{
		this.Set_UI.SetActive(true);
		this.Set_UI.GetComponentInChildren<Animation>().Play("tankuanglachuputong");
	}

	// Token: 0x06000C13 RID: 3091 RVA: 0x000491A4 File Offset: 0x000473A4
	public void YSCloseSet()
	{
		this.Set_UI.SetActive(false);
	}

	// Token: 0x06000C14 RID: 3092 RVA: 0x000491B2 File Offset: 0x000473B2
	public void openContinue_LoadAvatar()
	{
		if (!base.GetComponent<LevelSelectAvatarManager>().isInit)
		{
			base.GetComponent<LevelSelectAvatarManager>().reloadUI();
		}
		base.GetComponent<LevelSelectAvatarManager>().openLevelSelect();
	}

	// Token: 0x06000C15 RID: 3093 RVA: 0x000491D8 File Offset: 0x000473D8
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

	// Token: 0x06000C16 RID: 3094 RVA: 0x00049228 File Offset: 0x00047428
	public void back_options()
	{
		this.anim.Play("buttonTweenAnims_off");
		this.playClickSound();
	}

	// Token: 0x06000C17 RID: 3095 RVA: 0x00049240 File Offset: 0x00047440
	public void back_options_panels()
	{
		this.anim.Play("OptTweenAnim_off");
		this.playClickSound();
	}

	// Token: 0x06000C18 RID: 3096 RVA: 0x00049258 File Offset: 0x00047458
	public void Quit()
	{
		Application.Quit();
	}

	// Token: 0x06000C19 RID: 3097 RVA: 0x0004925F File Offset: 0x0004745F
	public void playHoverClip()
	{
		if (EasyAudioUtility.instance != null)
		{
			EasyAudioUtility.instance.Play("Hover");
		}
	}

	// Token: 0x06000C1A RID: 3098 RVA: 0x0004927D File Offset: 0x0004747D
	private void playClickSound()
	{
		if (EasyAudioUtility.instance != null)
		{
			EasyAudioUtility.instance.Play("Click");
		}
	}

	// Token: 0x0400085D RID: 2141
	private Animator anim;

	// Token: 0x0400085E RID: 2142
	public string newGameSceneName;

	// Token: 0x0400085F RID: 2143
	public int quickSaveSlotID;

	// Token: 0x04000860 RID: 2144
	[Header("Options Panel")]
	public GameObject MainOptionsPanel;

	// Token: 0x04000861 RID: 2145
	public GameObject StartGameOptionsPanel;

	// Token: 0x04000862 RID: 2146
	public GameObject ReconfigPanel;

	// Token: 0x04000863 RID: 2147
	public GameObject GamePanel;

	// Token: 0x04000864 RID: 2148
	public GameObject ControlsPanel;

	// Token: 0x04000865 RID: 2149
	public GameObject GfxPanel;

	// Token: 0x04000866 RID: 2150
	public GameObject LoadGamePanel;

	// Token: 0x04000867 RID: 2151
	public GameObject LoadAvatarPanel;

	// Token: 0x04000868 RID: 2152
	public GameObject LoadAvatarCellPanel;

	// Token: 0x04000869 RID: 2153
	public GameObject DouFaPanel;

	// Token: 0x0400086A RID: 2154
	public LevelSelectShengXianMag shengXianMag;

	// Token: 0x0400086B RID: 2155
	public GameObject Set_UI;
}
