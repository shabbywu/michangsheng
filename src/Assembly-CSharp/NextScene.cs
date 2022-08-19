using System;
using System.Collections;
using GUIPackage;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x0200034A RID: 842
public class NextScene : MonoBehaviour
{
	// Token: 0x06001CBE RID: 7358 RVA: 0x000CDAC8 File Offset: 0x000CBCC8
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

	// Token: 0x06001CBF RID: 7359 RVA: 0x000CDB1F File Offset: 0x000CBD1F
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
