using System.Collections;
using GUIPackage;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
	private void Start()
	{
		switch (Tools.instance.loadSceneType)
		{
		case 0:
			SceneManager.LoadSceneAsync(Tools.instance.ohtherSceneName);
			PanelMamager.inst.destoryUIGameObjet();
			Object.Destroy((Object)(object)PanelMamager.inst.UIBlackMaskGameObject);
			break;
		case 1:
			((MonoBehaviour)this).StartCoroutine(LoadScene());
			break;
		}
	}

	private IEnumerator LoadScene()
	{
		AsyncOperation op = SceneManager.LoadSceneAsync(Tools.jumpToName);
		op.allowSceneActivation = false;
		if ((Object)(object)PanelMamager.inst.UISceneGameObject == (Object)null)
		{
			Object.Instantiate<GameObject>(ResManager.inst.LoadPrefab("GameObject"));
		}
		if ((Object)(object)PanelMamager.inst.UISceneGameObject != (Object)null)
		{
			Transform val = PanelMamager.inst.UISceneGameObject.transform.Find("ThreeSceneNpcCanvas");
			if ((Object)(object)val != (Object)null)
			{
				((Component)val).gameObject.SetActive(true);
			}
		}
		op.allowSceneActivation = true;
		while (op.progress <= 0.9f)
		{
			yield return (object)new WaitForEndOfFrame();
			if (!(op.progress >= 0.85f))
			{
				continue;
			}
			op.allowSceneActivation = false;
			if ((Object)(object)UI_Manager.inst != (Object)null)
			{
				if ((Object)(object)ThreeSceernUIFab.inst != (Object)null)
				{
					Object.Destroy((Object)(object)((Component)ThreeSceernUIFab.inst).gameObject);
				}
				if ((Object)(object)ThreeSceneMagFab.inst != (Object)null)
				{
					Object.Destroy((Object)(object)((Component)ThreeSceneMagFab.inst).gameObject);
				}
			}
			break;
		}
		op.allowSceneActivation = true;
	}
}
