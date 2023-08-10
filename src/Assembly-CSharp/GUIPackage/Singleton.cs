using System.IO;
using Tab;
using UltimateSurvival;
using UnityEngine;

namespace GUIPackage;

public class Singleton : MonoBehaviour, IESCClose
{
	public static Inventory2 inventory;

	public static Equip_Manager equip;

	public static Store store;

	public static Key key;

	public static Key key2;

	public static Skill_UI skillUI;

	public static Skill_UIST skillUI2;

	public static Money money;

	public static UI_Manager UI;

	public static XiuLian biguan;

	public static Fader fader;

	public PaiMaiHang paiMaiHang;

	public ExchangePlan exchengePlan;

	public TuJianMag TuJIanPlan;

	public static ShopExchenge_UI shopExchenge_UI;

	public GameObject Draggable;

	public SeaMapUI seaMapUI;

	public static Singleton ints;

	public static TooltipsBackgroundi ToolTipsBackGround;

	private bool OpenDeBug;

	private GameObject DebugCanvas;

	private void Awake()
	{
		ints = this;
		getObjComponent(ref inventory, "Inventory");
		getObjComponent(ref equip, "Equipment");
		getObjComponent(ref store, "store");
		getObjComponent(ref equip, "Equipment");
		getObjComponent(ref skillUI, "skill_UI");
		getObjComponent(ref UI, "UI Root (2D)");
		getObjComponent(ref biguan, "UI Root (2D)/XiuLian");
		getObjComponent(ref fader, "Fader");
		getObjComponent(ref shopExchenge_UI, "exchengShop");
		getObjComponent(ref ToolTipsBackGround, "ToolTipBackground");
		GameObject val = GameObject.Find("UI Root (2D)/Draggable/LagPosition/DragTilt/Window/Content 3/skill_UI2");
		if ((Object)(object)val != (Object)null)
		{
			skillUI2 = val.gameObject.GetComponent<Skill_UIST>();
		}
		Transform val2 = ((Component)this).transform.Find("Draggable");
		if ((Object)(object)val2 != (Object)null)
		{
			Draggable = ((Component)val2).gameObject;
		}
		Transform val3 = ((Component)this).transform.Find("Draggable/LagPosition/DragTilt/Window/Content 2/short cut");
		if ((Object)(object)val3 != (Object)null)
		{
			key = ((Component)((Component)val3).transform.Find("Key")).GetComponent<Key>();
		}
		if ((Object)(object)key == (Object)null)
		{
			GameObject val4 = GameObject.Find("short cut");
			if ((Object)(object)val4 != (Object)null)
			{
				key = ((Component)val4.transform.Find("Key")).GetComponent<Key>();
			}
		}
		Transform val5 = ((Component)this).transform.Find("Draggable/LagPosition/DragTilt/Window/Content 3/short cut2");
		if ((Object)(object)val5 != (Object)null)
		{
			key2 = ((Component)((Component)val5).transform.Find("Key")).GetComponent<Key>();
		}
	}

	public void ShowSeaMapUI()
	{
		if ((Object)(object)seaMapUI == (Object)null)
		{
			Object obj = Resources.Load("MapPrefab/SeaAI/SeaMapUI");
			SeaMapUI component = ((GameObject)((obj is GameObject) ? obj : null)).GetComponent<SeaMapUI>();
			seaMapUI = Object.Instantiate<SeaMapUI>(component);
		}
		((Component)seaMapUI).gameObject.SetActive(true);
		Tools.canClickFlag = false;
	}

	public void getObjComponent<T>(ref T obj, string name)
	{
		GameObject val = GameObject.Find(name);
		if (Object.op_Implicit((Object)(object)val))
		{
			obj = val.GetComponent<T>();
		}
	}

	public void showRootUI()
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		LingGanMag.inst.UpdateData();
		UltimateSurvival.MonoSingleton<UI_Backgroud>.Instance.Value = true;
		Draggable.transform.localPosition = new Vector3(0f, 0f, 0f);
		Draggable.transform.localScale = Vector3.one * 0.81f;
		UINPCJiaoHu.AllShouldHide = true;
		ESCCloseManager.Inst.RegisterClose(this);
	}

	private void OnDestroy()
	{
		UINPCJiaoHu.AllShouldHide = false;
	}

	public void ClickTab()
	{
		if ((Object)(object)SingletonMono<TabUIMag>.Instance != (Object)null)
		{
			SingletonMono<TabUIMag>.Instance.TryEscClose();
		}
		else
		{
			TabUIMag.OpenTab();
		}
	}

	public bool TryEscClose()
	{
		if ((Object)(object)SingletonMono<TabUIMag>.Instance != (Object)null)
		{
			SingletonMono<TabUIMag>.Instance.TryEscClose();
		}
		return true;
	}

	public void Update()
	{
		if (Input.GetKeyUp((KeyCode)9))
		{
			if (UINPCJiaoHu.Inst.NowIsJiaoHu)
			{
				return;
			}
			if ((Object)(object)SingletonMono<TabUIMag>.Instance == (Object)null)
			{
				TabUIMag.OpenTab(4);
			}
			else
			{
				SingletonMono<TabUIMag>.Instance.TryEscClose();
			}
		}
		if (ESCCloseManager.Inst.ReadyESC && Input.GetKeyUp((KeyCode)27))
		{
			if (UINPCJiaoHu.Inst.NowIsJiaoHu)
			{
				return;
			}
			TabUIMag.OpenTab(6);
		}
		if (!Input.GetKey((KeyCode)8) || !Input.GetKeyUp((KeyCode)97) || !new FileInfo("C:/YuSui/Debug.txt").Exists)
		{
			return;
		}
		if (!OpenDeBug)
		{
			OpenDeBug = true;
			if ((Object)(object)DebugCanvas == (Object)null)
			{
				GameObject val = Resources.Load<GameObject>("uiPrefab/DebugCanvas");
				DebugCanvas = Object.Instantiate<GameObject>(val);
				((Object)DebugCanvas).name = "DebugCanvas";
			}
			DebugCanvas.gameObject.SetActive(true);
		}
		else
		{
			if ((Object)(object)DebugCanvas != (Object)null)
			{
				DebugCanvas.SetActive(false);
			}
			OpenDeBug = false;
		}
	}
}
