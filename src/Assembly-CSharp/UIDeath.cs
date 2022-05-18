using System;
using System.Collections;
using System.Collections.Generic;
using KBEngine;
using script.NewLianDan;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YSGame;
using YSGame.TuJian;

// Token: 0x020003DF RID: 991
public class UIDeath : MonoBehaviour
{
	// Token: 0x06001B0D RID: 6925 RVA: 0x00016E54 File Offset: 0x00015054
	private void Awake()
	{
		UIDeath.Inst = this;
		SceneManager.activeSceneChanged += delegate(Scene s1, Scene s2)
		{
			if (this.needShow)
			{
				if (LianQiTotalManager.inst != null)
				{
					Object.Destroy(LianQiTotalManager.inst.gameObject);
				}
				if (LianDanUIMag.Instance != null)
				{
					Object.Destroy(LianDanUIMag.Instance.gameObject);
				}
				this.Show();
			}
			if (this.needClose)
			{
				this.Close();
			}
		};
	}

	// Token: 0x06001B0E RID: 6926 RVA: 0x00016E6D File Offset: 0x0001506D
	private void Update()
	{
		if (this.isOpen && this.checkPress && Input.anyKeyDown)
		{
			this.BackToMainMenu();
		}
	}

	// Token: 0x06001B0F RID: 6927 RVA: 0x00016E8C File Offset: 0x0001508C
	public void Show(DeathType deathType)
	{
		ESCCloseManager.Inst.CloseAll();
		TuJianManager.Inst.UnlockDeath((int)deathType);
		this.showDeathType = deathType;
		this.needShow = true;
		Tools.instance.loadOtherScenes("DeathScene");
	}

	// Token: 0x06001B10 RID: 6928 RVA: 0x000EF8F8 File Offset: 0x000EDAF8
	private void Show()
	{
		if (FpUIMag.inst != null)
		{
			Object.Destroy(FpUIMag.inst.gameObject);
		}
		if (TpUIMag.inst != null)
		{
			Object.Destroy(TpUIMag.inst.gameObject);
		}
		base.transform.SetAsLastSibling();
		this.needShow = false;
		MusicMag.instance.PlayMusicImmediately("死亡");
		foreach (GameObject gameObject in this.DeathAnimList)
		{
			gameObject.SetActive(false);
		}
		this.checkPress = false;
		this.PressAnyKey.SetActive(false);
		this.ScaleObj.SetActive(true);
		int num = this.showDeathType - DeathType.身死道消;
		if (this.DeathAnimList.Count > num)
		{
			try
			{
				this.DeathAnimList[num].SetActive(true);
			}
			catch
			{
				Debug.Log("");
			}
			this.PressAnyKeyReturnText.color = this.DeathColorList[num];
			base.StartCoroutine("ShowPressAnyKey");
		}
		else
		{
			Debug.LogError(string.Format("死亡动画{0}播放失败，没有将引用加入列表", this.showDeathType));
		}
		this.isOpen = true;
	}

	// Token: 0x06001B11 RID: 6929 RVA: 0x00016EC0 File Offset: 0x000150C0
	private IEnumerator ShowPressAnyKey()
	{
		yield return new WaitForSeconds(3f);
		this.PressAnyKey.SetActive(true);
		this.checkPress = true;
		yield break;
	}

	// Token: 0x06001B12 RID: 6930 RVA: 0x00016ECF File Offset: 0x000150CF
	private void Close()
	{
		this.needClose = false;
		this.isOpen = false;
		this.ScaleObj.SetActive(false);
	}

	// Token: 0x06001B13 RID: 6931 RVA: 0x000EFA4C File Offset: 0x000EDC4C
	public void BackToMainMenu()
	{
		YSSaveGame.Reset();
		KBEngineApp.app.entities[10] = null;
		KBEngineApp.app.entities.Remove(10);
		this.needClose = true;
		Tools.instance.loadOtherScenes("MainMenu");
	}

	// Token: 0x040016C4 RID: 5828
	public static UIDeath Inst;

	// Token: 0x040016C5 RID: 5829
	public GameObject ScaleObj;

	// Token: 0x040016C6 RID: 5830
	public GameObject PressAnyKey;

	// Token: 0x040016C7 RID: 5831
	public Text PressAnyKeyReturnText;

	// Token: 0x040016C8 RID: 5832
	public List<GameObject> DeathAnimList;

	// Token: 0x040016C9 RID: 5833
	public List<Color> DeathColorList;

	// Token: 0x040016CA RID: 5834
	private bool isOpen;

	// Token: 0x040016CB RID: 5835
	private bool needClose;

	// Token: 0x040016CC RID: 5836
	private bool needShow;

	// Token: 0x040016CD RID: 5837
	private bool checkPress;

	// Token: 0x040016CE RID: 5838
	private DeathType showDeathType;
}
