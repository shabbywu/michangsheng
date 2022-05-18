using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

// Token: 0x020002FC RID: 764
public class InitScene : MonoBehaviour
{
	// Token: 0x060016FD RID: 5885 RVA: 0x000CBF70 File Offset: 0x000CA170
	private void Start()
	{
		Debug.unityLogger.filterLogType = 0;
		SceneManager.LoadScene("MainMenu");
		InitCustomAttributes.Init();
		GameObject gameObject = Resources.Load<GameObject>("NewUICanvas");
		this.newCanvas = Object.Instantiate<GameObject>(gameObject);
		Object.Instantiate<GameObject>(Resources.Load<GameObject>("NewUI/TuJian/TuJianPanel"));
		this.CreateUIToNewCanvas("NewUI/Head/UIHeadPanel");
		this.CreateUIToNewCanvas("NewUI/Head/UIMiniTaskPanel");
		this.CreateUIToNewCanvas("NewUI/Head/UIFuBenShengYuTimePanel");
		this.CreateUIToNewCanvas("NewUI/NPCJiaoHu/Prefab/UINPCJiaoHu");
		this.CreateUIToNewCanvas("NewUI/BiGuan/UIBiGuan");
		this.CreateUIToNewCanvas("NewUI/DongFu/UIDongFu");
		this.CreateUIToNewCanvas("NewUI/GaoShi/UIGaoShi");
		this.CreateUIToNewCanvas("NewUI/MenPaiShop/UIMenPaiShop");
		this.CreateUIToNewCanvas("NewUI/MenPaiShop/UIDuiHuanShop");
		this.CreateUIToNewCanvas("PlayTutorial/UITutorialSeaMove");
		this.CreateUIToNewCanvas("NewUI/HuaShen/UIHuaShenRuDaoSelect");
		this.CreateUIToNewCanvas("Sea/Prefab/UISeaTanSuoPanel");
		this.CreateUIToNewCanvas("NewUI/UIDeath");
		this.CreateUIToNewCanvas("NewUI/Map/UIMap");
		Object.DontDestroyOnLoad(Object.Instantiate<GameObject>(Resources.Load<GameObject>("UIPopTip")));
		this.CreateUIToNewCanvas("Prefab/StoryManager/StoryManager");
		SceneManager.sceneLoaded += delegate(Scene s, LoadSceneMode m)
		{
			NpcJieSuanManager.inst.isUpDateNpcList = true;
		};
		SceneManager.sceneLoaded += delegate(Scene s, LoadSceneMode m)
		{
			PlayerEx.CheckChuHai();
		};
		SceneManager.sceneLoaded += delegate(Scene s, LoadSceneMode m)
		{
			UISeaTanSuoPanel.Inst.RefreshUI();
		};
		SceneManager.sceneLoaded += delegate(Scene s, LoadSceneMode m)
		{
			EventSystem[] array = Object.FindObjectsOfType<EventSystem>();
			for (int i = 0; i < array.Length; i++)
			{
				array[i].sendNavigationEvents = false;
			}
		};
	}

	// Token: 0x060016FE RID: 5886 RVA: 0x00014588 File Offset: 0x00012788
	public void CreateUIToNewCanvas(string path)
	{
		Object.Instantiate<GameObject>(Resources.Load<GameObject>(path), this.newCanvas.transform);
	}

	// Token: 0x0400125C RID: 4700
	private GameObject newCanvas;
}
