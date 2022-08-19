using System;
using System.IO;
using Tab;
using UltimateSurvival;
using UnityEngine;

namespace GUIPackage
{
	// Token: 0x02000A67 RID: 2663
	public class Singleton : MonoBehaviour, IESCClose
	{
		// Token: 0x06004AC5 RID: 19141 RVA: 0x001FC7E4 File Offset: 0x001FA9E4
		private void Awake()
		{
			Singleton.ints = this;
			this.getObjComponent<Inventory2>(ref Singleton.inventory, "Inventory");
			this.getObjComponent<Equip_Manager>(ref Singleton.equip, "Equipment");
			this.getObjComponent<Store>(ref Singleton.store, "store");
			this.getObjComponent<Equip_Manager>(ref Singleton.equip, "Equipment");
			this.getObjComponent<Skill_UI>(ref Singleton.skillUI, "skill_UI");
			this.getObjComponent<UI_Manager>(ref Singleton.UI, "UI Root (2D)");
			this.getObjComponent<XiuLian>(ref Singleton.biguan, "UI Root (2D)/XiuLian");
			this.getObjComponent<Fader>(ref Singleton.fader, "Fader");
			this.getObjComponent<ShopExchenge_UI>(ref Singleton.shopExchenge_UI, "exchengShop");
			this.getObjComponent<TooltipsBackgroundi>(ref Singleton.ToolTipsBackGround, "ToolTipBackground");
			GameObject gameObject = GameObject.Find("UI Root (2D)/Draggable/LagPosition/DragTilt/Window/Content 3/skill_UI2");
			if (gameObject != null)
			{
				Singleton.skillUI2 = gameObject.gameObject.GetComponent<Skill_UIST>();
			}
			Transform transform = base.transform.Find("Draggable");
			if (transform != null)
			{
				this.Draggable = transform.gameObject;
			}
			Transform transform2 = base.transform.Find("Draggable/LagPosition/DragTilt/Window/Content 2/short cut");
			if (transform2 != null)
			{
				Singleton.key = transform2.transform.Find("Key").GetComponent<Key>();
			}
			if (Singleton.key == null)
			{
				GameObject gameObject2 = GameObject.Find("short cut");
				if (gameObject2 != null)
				{
					Singleton.key = gameObject2.transform.Find("Key").GetComponent<Key>();
				}
			}
			Transform transform3 = base.transform.Find("Draggable/LagPosition/DragTilt/Window/Content 3/short cut2");
			if (transform3 != null)
			{
				Singleton.key2 = transform3.transform.Find("Key").GetComponent<Key>();
			}
		}

		// Token: 0x06004AC6 RID: 19142 RVA: 0x001FC988 File Offset: 0x001FAB88
		public void ShowSeaMapUI()
		{
			if (this.seaMapUI == null)
			{
				SeaMapUI component = (Resources.Load("MapPrefab/SeaAI/SeaMapUI") as GameObject).GetComponent<SeaMapUI>();
				this.seaMapUI = Object.Instantiate<SeaMapUI>(component);
			}
			this.seaMapUI.gameObject.SetActive(true);
			Tools.canClickFlag = false;
		}

		// Token: 0x06004AC7 RID: 19143 RVA: 0x001FC9DC File Offset: 0x001FABDC
		public void getObjComponent<T>(ref T obj, string name)
		{
			GameObject gameObject = GameObject.Find(name);
			if (gameObject)
			{
				obj = gameObject.GetComponent<T>();
			}
		}

		// Token: 0x06004AC8 RID: 19144 RVA: 0x001FCA04 File Offset: 0x001FAC04
		public void showRootUI()
		{
			LingGanMag.inst.UpdateData();
			UltimateSurvival.MonoSingleton<UI_Backgroud>.Instance.Value = true;
			this.Draggable.transform.localPosition = new Vector3(0f, 0f, 0f);
			this.Draggable.transform.localScale = Vector3.one * 0.81f;
			UINPCJiaoHu.AllShouldHide = true;
			ESCCloseManager.Inst.RegisterClose(this);
		}

		// Token: 0x06004AC9 RID: 19145 RVA: 0x001FCA7A File Offset: 0x001FAC7A
		private void OnDestroy()
		{
			UINPCJiaoHu.AllShouldHide = false;
		}

		// Token: 0x06004ACA RID: 19146 RVA: 0x001FCA82 File Offset: 0x001FAC82
		public void ClickTab()
		{
			if (SingletonMono<TabUIMag>.Instance != null)
			{
				SingletonMono<TabUIMag>.Instance.TryEscClose();
				return;
			}
			TabUIMag.OpenTab(0, null);
		}

		// Token: 0x06004ACB RID: 19147 RVA: 0x001FCAA4 File Offset: 0x001FACA4
		public bool TryEscClose()
		{
			if (SingletonMono<TabUIMag>.Instance != null)
			{
				SingletonMono<TabUIMag>.Instance.TryEscClose();
			}
			return true;
		}

		// Token: 0x06004ACC RID: 19148 RVA: 0x001FCAC0 File Offset: 0x001FACC0
		public void Update()
		{
			if (Input.GetKeyUp(9))
			{
				if (UINPCJiaoHu.Inst.NowIsJiaoHu)
				{
					return;
				}
				if (SingletonMono<TabUIMag>.Instance == null)
				{
					TabUIMag.OpenTab(4, null);
				}
				else
				{
					SingletonMono<TabUIMag>.Instance.TryEscClose();
				}
			}
			if (ESCCloseManager.Inst.ReadyESC && Input.GetKeyUp(27))
			{
				if (UINPCJiaoHu.Inst.NowIsJiaoHu)
				{
					return;
				}
				TabUIMag.OpenTab(6, null);
			}
			if (Input.GetKey(8) && Input.GetKeyUp(97) && new FileInfo("C:/YuSui/Debug.txt").Exists)
			{
				if (!this.OpenDeBug)
				{
					this.OpenDeBug = true;
					if (this.DebugCanvas == null)
					{
						GameObject gameObject = Resources.Load<GameObject>("uiPrefab/DebugCanvas");
						this.DebugCanvas = Object.Instantiate<GameObject>(gameObject);
						this.DebugCanvas.name = "DebugCanvas";
					}
					this.DebugCanvas.gameObject.SetActive(true);
					return;
				}
				if (this.DebugCanvas != null)
				{
					this.DebugCanvas.SetActive(false);
				}
				this.OpenDeBug = false;
			}
		}

		// Token: 0x040049E2 RID: 18914
		public static Inventory2 inventory;

		// Token: 0x040049E3 RID: 18915
		public static Equip_Manager equip;

		// Token: 0x040049E4 RID: 18916
		public static Store store;

		// Token: 0x040049E5 RID: 18917
		public static Key key;

		// Token: 0x040049E6 RID: 18918
		public static Key key2;

		// Token: 0x040049E7 RID: 18919
		public static Skill_UI skillUI;

		// Token: 0x040049E8 RID: 18920
		public static Skill_UIST skillUI2;

		// Token: 0x040049E9 RID: 18921
		public static Money money;

		// Token: 0x040049EA RID: 18922
		public static UI_Manager UI;

		// Token: 0x040049EB RID: 18923
		public static XiuLian biguan;

		// Token: 0x040049EC RID: 18924
		public static Fader fader;

		// Token: 0x040049ED RID: 18925
		public PaiMaiHang paiMaiHang;

		// Token: 0x040049EE RID: 18926
		public ExchangePlan exchengePlan;

		// Token: 0x040049EF RID: 18927
		public TuJianMag TuJIanPlan;

		// Token: 0x040049F0 RID: 18928
		public static ShopExchenge_UI shopExchenge_UI;

		// Token: 0x040049F1 RID: 18929
		public GameObject Draggable;

		// Token: 0x040049F2 RID: 18930
		public SeaMapUI seaMapUI;

		// Token: 0x040049F3 RID: 18931
		public static Singleton ints;

		// Token: 0x040049F4 RID: 18932
		public static TooltipsBackgroundi ToolTipsBackGround;

		// Token: 0x040049F5 RID: 18933
		private bool OpenDeBug;

		// Token: 0x040049F6 RID: 18934
		private GameObject DebugCanvas;
	}
}
