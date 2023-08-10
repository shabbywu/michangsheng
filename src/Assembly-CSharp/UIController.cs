using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
	[Tooltip("Usee Blur in Pause Menu?")]
	public bool useBlur;

	[Header("Both UI Panels")]
	public GameObject saveMenu;

	public GameObject pauseMenu;

	private Fader fader;

	[HideInInspector]
	public bool isOpen;

	private Canvas[] allUI;

	public List<LoadSlotIdentifier> loadSlots;

	[HideInInspector]
	public bool openPMenu = true;

	private IEnumerator Start()
	{
		fader = Object.FindObjectOfType<Fader>();
		yield return (object)new WaitForSeconds(0.5f);
	}

	private void Update()
	{
		if (!saveMenu.active && canOpen() && Input.GetKeyDown((KeyCode)27))
		{
			if (!isOpen)
			{
				openPauseMenu();
			}
			else
			{
				closePauseMenu();
			}
		}
	}

	public void openPauseMenu()
	{
		allUI = Object.FindObjectsOfType<Canvas>();
		for (int i = 0; i < allUI.Length; i++)
		{
			if (((Object)allUI[i]).name != "Fader")
			{
				((Component)allUI[i]).gameObject.SetActive(false);
			}
		}
		saveMenu.SetActive(false);
		pauseMenu.SetActive(true);
		((Component)this).GetComponent<SaveGameUI>().playClickSound();
		((Component)this).GetComponent<Animator>().Play("OpenPauseMenu");
		Time.timeScale = 0.0001f;
		isOpen = true;
		((Component)this).GetComponent<PauseMenuOptions>().Init();
		if (useBlur && Object.op_Implicit((Object)(object)((Component)Camera.main).GetComponent<Animator>()))
		{
			((Component)Camera.main).GetComponent<Animator>().Play("BlurOff");
		}
	}

	public void closePauseMenu()
	{
		for (int i = 0; i < allUI.Length; i++)
		{
			((Component)allUI[i]).gameObject.SetActive(true);
		}
		Time.timeScale = 1f;
		((Component)this).GetComponent<SaveGameUI>().playClickSound();
		((Component)this).GetComponent<Animator>().Play("ClosePauseMenu");
		isOpen = false;
		if (useBlur && Object.op_Implicit((Object)(object)((Component)Camera.main).GetComponent<Animator>()))
		{
			((Component)Camera.main).GetComponent<Animator>().Play("BlurOff");
		}
	}

	public void hideMenus()
	{
		saveMenu.SetActive(false);
		pauseMenu.SetActive(false);
	}

	public void goToMainMenu()
	{
		Object.Destroy((Object)(object)GameObject.FindGameObjectWithTag("Player"));
		Time.timeScale = 1f;
		PlayerPrefs.SetString("sceneToLoad", "");
		hideMenus();
		fader.FadeIntoLevel("LoadingScreen");
	}

	public void openLoadGame()
	{
		((Component)this).GetComponent<Animator>().Play("loadGameOpen");
		initLoadGameMenu();
	}

	public void closeLoadGame()
	{
		((Component)this).GetComponent<Animator>().Play("loadGameClose");
	}

	private void initLoadGameMenu()
	{
		if (loadSlots.Count <= 0)
		{
			return;
		}
		foreach (LoadSlotIdentifier loadSlot in loadSlots)
		{
			loadSlot.Init();
		}
	}

	public bool canOpen()
	{
		return openPMenu;
	}
}
