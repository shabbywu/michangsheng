using System;
using System.Collections;
using GUIPackage;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x020004C5 RID: 1221
public class NextScene : MonoBehaviour
{
	// Token: 0x0600201F RID: 8223 RVA: 0x00112A48 File Offset: 0x00110C48
	private void Start()
	{
		int loadSceneType = Tools.instance.loadSceneType;
		if (loadSceneType == 0)
		{
			SceneManager.LoadSceneAsync(Tools.instance.ohtherSceneName);
			PanelMamager.inst.destoryUIGameObjet();
			Object.Destroy(PanelMamager.inst.UIBlackMaskGameObject);
			return;
		}
		if (loadSceneType != 1)
		{
			return;
		}
		base.StartCoroutine(this.LoadScene());
	}

	// Token: 0x06002020 RID: 8224 RVA: 0x0001A5BD File Offset: 0x000187BD
	private IEnumerator LoadScene()
	{
		AsyncOperation op = SceneManager.LoadSceneAsync(Tools.jumpToName);
		op.allowSceneActivation = false;
		if (PanelMamager.inst.UISceneGameObject == null)
		{
			Object.Instantiate<GameObject>(ResManager.inst.LoadPrefab("GameObject"));
		}
		if (PanelMamager.inst.UISceneGameObject != null)
		{
			Transform transform = PanelMamager.inst.UISceneGameObject.transform.Find("ThreeSceneNpcCanvas");
			if (transform != null)
			{
				transform.gameObject.SetActive(true);
			}
		}
		op.allowSceneActivation = true;
		while (op.progress <= 0.9f)
		{
			yield return new WaitForEndOfFrame();
			if (op.progress >= 0.85f)
			{
				op.allowSceneActivation = false;
				if (!(UI_Manager.inst != null))
				{
					break;
				}
				if (ThreeSceernUIFab.inst != null)
				{
					Object.Destroy(ThreeSceernUIFab.inst.gameObject);
				}
				if (ThreeSceneMagFab.inst != null)
				{
					Object.Destroy(ThreeSceneMagFab.inst.gameObject);
					break;
				}
				break;
			}
		}
		op.allowSceneActivation = true;
		yield break;
	}
}
