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

// Token: 0x020002A5 RID: 677
public class UIDeath : MonoBehaviour
{
	// Token: 0x0600181A RID: 6170 RVA: 0x000A87B1 File Offset: 0x000A69B1
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

	// Token: 0x0600181B RID: 6171 RVA: 0x000A87CA File Offset: 0x000A69CA
	private void Update()
	{
		if (this.isOpen && this.checkPress && Input.anyKeyDown)
		{
			this.BackToMainMenu();
		}
	}

	// Token: 0x0600181C RID: 6172 RVA: 0x000A87E9 File Offset: 0x000A69E9
	public void Show(DeathType deathType)
	{
		ESCCloseManager.Inst.CloseAll();
		TuJianManager.Inst.UnlockDeath((int)deathType);
		this.showDeathType = deathType;
		this.needShow = true;
		Tools.instance.loadOtherScenes("DeathScene");
	}

	// Token: 0x0600181D RID: 6173 RVA: 0x000A8820 File Offset: 0x000A6A20
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

	// Token: 0x0600181E RID: 6174 RVA: 0x000A8974 File Offset: 0x000A6B74
	private IEnumerator ShowPressAnyKey()
	{
		yield return new WaitForSeconds(3f);
		this.PressAnyKey.SetActive(true);
		this.checkPress = true;
		yield break;
	}

	// Token: 0x0600181F RID: 6175 RVA: 0x000A8983 File Offset: 0x000A6B83
	private void Close()
	{
		this.needClose = false;
		this.isOpen = false;
		this.ScaleObj.SetActive(false);
	}

	// Token: 0x06001820 RID: 6176 RVA: 0x000A89A0 File Offset: 0x000A6BA0
	public void BackToMainMenu()
	{
		YSSaveGame.Reset();
		KBEngineApp.app.entities[10] = null;
		KBEngineApp.app.entities.Remove(10);
		this.needClose = true;
		Tools.instance.loadOtherScenes("MainMenu");
	}

	// Token: 0x04001326 RID: 4902
	public static UIDeath Inst;

	// Token: 0x04001327 RID: 4903
	public GameObject ScaleObj;

	// Token: 0x04001328 RID: 4904
	public GameObject PressAnyKey;

	// Token: 0x04001329 RID: 4905
	public Text PressAnyKeyReturnText;

	// Token: 0x0400132A RID: 4906
	public List<GameObject> DeathAnimList;

	// Token: 0x0400132B RID: 4907
	public List<Color> DeathColorList;

	// Token: 0x0400132C RID: 4908
	private bool isOpen;

	// Token: 0x0400132D RID: 4909
	private bool needClose;

	// Token: 0x0400132E RID: 4910
	private bool needShow;

	// Token: 0x0400132F RID: 4911
	private bool checkPress;

	// Token: 0x04001330 RID: 4912
	private DeathType showDeathType;
}
