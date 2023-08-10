using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class InitScene : MonoBehaviour
{
	private GameObject newCanvas;

	private void Start()
	{
		Debug.unityLogger.filterLogType = (LogType)0;
		SceneManager.LoadScene("MainMenu");
		InitCustomAttributes.Init();
		GameObject val = Resources.Load<GameObject>("NewUICanvas");
		newCanvas = Object.Instantiate<GameObject>(val);
		Object.Instantiate<GameObject>(Resources.Load<GameObject>("NewUI/TuJian/TuJianPanel"));
		CreateUIToNewCanvas("NewUI/Head/UIHeadPanel");
		CreateUIToNewCanvas("NewUI/Head/UIMiniTaskPanel");
		CreateUIToNewCanvas("NewUI/Head/UIFuBenShengYuTimePanel");
		CreateUIToNewCanvas("NewUI/NPCJiaoHu/Prefab/UINPCJiaoHu");
		CreateUIToNewCanvas("NewUI/BiGuan/UIBiGuan");
		CreateUIToNewCanvas("NewUI/DongFu/UIDongFu");
		CreateUIToNewCanvas("NewUI/GaoShi/UIGaoShi");
		CreateUIToNewCanvas("NewUI/MenPaiShop/UIMenPaiShop");
		CreateUIToNewCanvas("NewUI/MenPaiShop/UIDuiHuanShop");
		CreateUIToNewCanvas("PlayTutorial/UITutorialSeaMove");
		CreateUIToNewCanvas("NewUI/HuaShen/UIHuaShenRuDaoSelect");
		CreateUIToNewCanvas("Sea/Prefab/UISeaTanSuoPanel");
		CreateUIToNewCanvas("NewUI/UIDeath");
		CreateUIToNewCanvas("NewUI/Map/UIMap");
		Object.DontDestroyOnLoad((Object)(object)Object.Instantiate<GameObject>(Resources.Load<GameObject>("UIPopTip")));
		CreateUIToNewCanvas("Prefab/StoryManager/StoryManager");
		SceneManager.sceneLoaded += delegate
		{
			NpcJieSuanManager.inst.isUpDateNpcList = true;
		};
		SceneManager.sceneLoaded += delegate
		{
			PlayerEx.CheckChuHai();
		};
		SceneManager.sceneLoaded += delegate
		{
			UISeaTanSuoPanel.Inst.RefreshUI();
		};
		SceneManager.sceneLoaded += delegate
		{
			EventSystem[] array = Object.FindObjectsOfType<EventSystem>();
			for (int i = 0; i < array.Length; i++)
			{
				array[i].sendNavigationEvents = false;
			}
		};
	}

	public void CreateUIToNewCanvas(string path)
	{
		Object.Instantiate<GameObject>(Resources.Load<GameObject>(path), newCanvas.transform);
	}
}
