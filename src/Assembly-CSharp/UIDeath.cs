using System.Collections;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YSGame;
using YSGame.TuJian;
using script.NewLianDan;

public class UIDeath : MonoBehaviour
{
	public static UIDeath Inst;

	public GameObject ScaleObj;

	public GameObject PressAnyKey;

	public Text PressAnyKeyReturnText;

	public List<GameObject> DeathAnimList;

	public List<Color> DeathColorList;

	private bool isOpen;

	private bool needClose;

	private bool needShow;

	private bool checkPress;

	private DeathType showDeathType;

	private void Awake()
	{
		Inst = this;
		SceneManager.activeSceneChanged += delegate
		{
			if (needShow)
			{
				if ((Object)(object)LianQiTotalManager.inst != (Object)null)
				{
					Object.Destroy((Object)(object)((Component)LianQiTotalManager.inst).gameObject);
				}
				if ((Object)(object)LianDanUIMag.Instance != (Object)null)
				{
					Object.Destroy((Object)(object)((Component)LianDanUIMag.Instance).gameObject);
				}
				Show();
			}
			if (needClose)
			{
				Close();
			}
		};
	}

	private void Update()
	{
		if (isOpen && checkPress && Input.anyKeyDown)
		{
			BackToMainMenu();
		}
	}

	public void Show(DeathType deathType)
	{
		ESCCloseManager.Inst.CloseAll();
		TuJianManager.Inst.UnlockDeath((int)deathType);
		showDeathType = deathType;
		needShow = true;
		Tools.instance.loadOtherScenes("DeathScene");
	}

	private void Show()
	{
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)FpUIMag.inst != (Object)null)
		{
			Object.Destroy((Object)(object)((Component)FpUIMag.inst).gameObject);
		}
		if ((Object)(object)TpUIMag.inst != (Object)null)
		{
			Object.Destroy((Object)(object)((Component)TpUIMag.inst).gameObject);
		}
		((Component)this).transform.SetAsLastSibling();
		needShow = false;
		MusicMag.instance.PlayMusicImmediately("死亡");
		foreach (GameObject deathAnim in DeathAnimList)
		{
			deathAnim.SetActive(false);
		}
		checkPress = false;
		PressAnyKey.SetActive(false);
		ScaleObj.SetActive(true);
		int num = (int)(showDeathType - 1);
		if (DeathAnimList.Count > num)
		{
			try
			{
				DeathAnimList[num].SetActive(true);
			}
			catch
			{
				Debug.Log((object)"");
			}
			((Graphic)PressAnyKeyReturnText).color = DeathColorList[num];
			((MonoBehaviour)this).StartCoroutine("ShowPressAnyKey");
		}
		else
		{
			Debug.LogError((object)$"死亡动画{showDeathType}播放失败，没有将引用加入列表");
		}
		isOpen = true;
	}

	private IEnumerator ShowPressAnyKey()
	{
		yield return (object)new WaitForSeconds(3f);
		PressAnyKey.SetActive(true);
		checkPress = true;
	}

	private void Close()
	{
		needClose = false;
		isOpen = false;
		ScaleObj.SetActive(false);
	}

	public void BackToMainMenu()
	{
		YSSaveGame.Reset();
		KBEngineApp.app.entities[10] = null;
		KBEngineApp.app.entities.Remove(10);
		needClose = true;
		Tools.instance.loadOtherScenes("MainMenu");
	}
}
