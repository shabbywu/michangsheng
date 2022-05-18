using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000178 RID: 376
public class UIController : MonoBehaviour
{
	// Token: 0x06000CA8 RID: 3240 RVA: 0x0000E75D File Offset: 0x0000C95D
	private IEnumerator Start()
	{
		this.fader = Object.FindObjectOfType<Fader>();
		yield return new WaitForSeconds(0.5f);
		yield break;
	}

	// Token: 0x06000CA9 RID: 3241 RVA: 0x0000E76C File Offset: 0x0000C96C
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

	// Token: 0x06000CAA RID: 3242 RVA: 0x00098C14 File Offset: 0x00096E14
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

	// Token: 0x06000CAB RID: 3243 RVA: 0x00098CE8 File Offset: 0x00096EE8
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

	// Token: 0x06000CAC RID: 3244 RVA: 0x0000E7A1 File Offset: 0x0000C9A1
	public void hideMenus()
	{
		this.saveMenu.SetActive(false);
		this.pauseMenu.SetActive(false);
	}

	// Token: 0x06000CAD RID: 3245 RVA: 0x0000E7BB File Offset: 0x0000C9BB
	public void goToMainMenu()
	{
		Object.Destroy(GameObject.FindGameObjectWithTag("Player"));
		Time.timeScale = 1f;
		PlayerPrefs.SetString("sceneToLoad", "");
		this.hideMenus();
		this.fader.FadeIntoLevel("LoadingScreen");
	}

	// Token: 0x06000CAE RID: 3246 RVA: 0x0000E7FB File Offset: 0x0000C9FB
	public void openLoadGame()
	{
		base.GetComponent<Animator>().Play("loadGameOpen");
		this.initLoadGameMenu();
	}

	// Token: 0x06000CAF RID: 3247 RVA: 0x0000E813 File Offset: 0x0000CA13
	public void closeLoadGame()
	{
		base.GetComponent<Animator>().Play("loadGameClose");
	}

	// Token: 0x06000CB0 RID: 3248 RVA: 0x00098D74 File Offset: 0x00096F74
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

	// Token: 0x06000CB1 RID: 3249 RVA: 0x0000E825 File Offset: 0x0000CA25
	public bool canOpen()
	{
		return this.openPMenu;
	}

	// Token: 0x040009D9 RID: 2521
	[Tooltip("Usee Blur in Pause Menu?")]
	public bool useBlur;

	// Token: 0x040009DA RID: 2522
	[Header("Both UI Panels")]
	public GameObject saveMenu;

	// Token: 0x040009DB RID: 2523
	public GameObject pauseMenu;

	// Token: 0x040009DC RID: 2524
	private Fader fader;

	// Token: 0x040009DD RID: 2525
	[HideInInspector]
	public bool isOpen;

	// Token: 0x040009DE RID: 2526
	private Canvas[] allUI;

	// Token: 0x040009DF RID: 2527
	public List<LoadSlotIdentifier> loadSlots;

	// Token: 0x040009E0 RID: 2528
	[HideInInspector]
	public bool openPMenu = true;
}
