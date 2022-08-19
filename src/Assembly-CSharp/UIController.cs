using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000FC RID: 252
public class UIController : MonoBehaviour
{
	// Token: 0x06000BA9 RID: 2985 RVA: 0x0004717D File Offset: 0x0004537D
	private IEnumerator Start()
	{
		this.fader = Object.FindObjectOfType<Fader>();
		yield return new WaitForSeconds(0.5f);
		yield break;
	}

	// Token: 0x06000BAA RID: 2986 RVA: 0x0004718C File Offset: 0x0004538C
	private void Update()
	{
		if (!this.saveMenu.active && this.canOpen() && Input.GetKeyDown(27))
		{
			if (!this.isOpen)
			{
				this.openPauseMenu();
				return;
			}
			this.closePauseMenu();
		}
	}

	// Token: 0x06000BAB RID: 2987 RVA: 0x000471C4 File Offset: 0x000453C4
	public void openPauseMenu()
	{
		this.allUI = Object.FindObjectsOfType<Canvas>();
		for (int i = 0; i < this.allUI.Length; i++)
		{
			if (this.allUI[i].name != "Fader")
			{
				this.allUI[i].gameObject.SetActive(false);
			}
		}
		this.saveMenu.SetActive(false);
		this.pauseMenu.SetActive(true);
		base.GetComponent<SaveGameUI>().playClickSound();
		base.GetComponent<Animator>().Play("OpenPauseMenu");
		Time.timeScale = 0.0001f;
		this.isOpen = true;
		base.GetComponent<PauseMenuOptions>().Init();
		if (this.useBlur && Camera.main.GetComponent<Animator>())
		{
			Camera.main.GetComponent<Animator>().Play("BlurOff");
		}
	}

	// Token: 0x06000BAC RID: 2988 RVA: 0x00047298 File Offset: 0x00045498
	public void closePauseMenu()
	{
		for (int i = 0; i < this.allUI.Length; i++)
		{
			this.allUI[i].gameObject.SetActive(true);
		}
		Time.timeScale = 1f;
		base.GetComponent<SaveGameUI>().playClickSound();
		base.GetComponent<Animator>().Play("ClosePauseMenu");
		this.isOpen = false;
		if (this.useBlur && Camera.main.GetComponent<Animator>())
		{
			Camera.main.GetComponent<Animator>().Play("BlurOff");
		}
	}

	// Token: 0x06000BAD RID: 2989 RVA: 0x00047324 File Offset: 0x00045524
	public void hideMenus()
	{
		this.saveMenu.SetActive(false);
		this.pauseMenu.SetActive(false);
	}

	// Token: 0x06000BAE RID: 2990 RVA: 0x0004733E File Offset: 0x0004553E
	public void goToMainMenu()
	{
		Object.Destroy(GameObject.FindGameObjectWithTag("Player"));
		Time.timeScale = 1f;
		PlayerPrefs.SetString("sceneToLoad", "");
		this.hideMenus();
		this.fader.FadeIntoLevel("LoadingScreen");
	}

	// Token: 0x06000BAF RID: 2991 RVA: 0x0004737E File Offset: 0x0004557E
	public void openLoadGame()
	{
		base.GetComponent<Animator>().Play("loadGameOpen");
		this.initLoadGameMenu();
	}

	// Token: 0x06000BB0 RID: 2992 RVA: 0x00047396 File Offset: 0x00045596
	public void closeLoadGame()
	{
		base.GetComponent<Animator>().Play("loadGameClose");
	}

	// Token: 0x06000BB1 RID: 2993 RVA: 0x000473A8 File Offset: 0x000455A8
	private void initLoadGameMenu()
	{
		if (this.loadSlots.Count > 0)
		{
			foreach (LoadSlotIdentifier loadSlotIdentifier in this.loadSlots)
			{
				loadSlotIdentifier.Init();
			}
		}
	}

	// Token: 0x06000BB2 RID: 2994 RVA: 0x00047408 File Offset: 0x00045608
	public bool canOpen()
	{
		return this.openPMenu;
	}

	// Token: 0x040007F0 RID: 2032
	[Tooltip("Usee Blur in Pause Menu?")]
	public bool useBlur;

	// Token: 0x040007F1 RID: 2033
	[Header("Both UI Panels")]
	public GameObject saveMenu;

	// Token: 0x040007F2 RID: 2034
	public GameObject pauseMenu;

	// Token: 0x040007F3 RID: 2035
	private Fader fader;

	// Token: 0x040007F4 RID: 2036
	[HideInInspector]
	public bool isOpen;

	// Token: 0x040007F5 RID: 2037
	private Canvas[] allUI;

	// Token: 0x040007F6 RID: 2038
	public List<LoadSlotIdentifier> loadSlots;

	// Token: 0x040007F7 RID: 2039
	[HideInInspector]
	public bool openPMenu = true;
}
