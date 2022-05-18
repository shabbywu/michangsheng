using System;
using System.IO;
using Tab;
using UltimateSurvival;
using UnityEngine;

namespace GUIPackage
{
	// Token: 0x02000D93 RID: 3475
	public class Singleton : MonoBehaviour, IESCClose
	{
		// Token: 0x060053CF RID: 21455 RVA: 0x0022E968 File Offset: 0x0022CB68
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

		// Token: 0x060053D0 RID: 21456 RVA: 0x0022EB0C File Offset: 0x0022CD0C
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

		// Token: 0x060053D1 RID: 21457 RVA: 0x0022EB60 File Offset: 0x0022CD60
		public void getObjComponent<T>(ref T obj, string name)
		{
			GameObject gameObject = GameObject.Find(name);
			if (gameObject)
			{
				obj = gameObject.GetComponent<T>();
			}
		}

		// Token: 0x060053D2 RID: 21458 RVA: 0x0022EB88 File Offset: 0x0022CD88
		public void showRootUI()
		{
			LingGanMag.inst.UpdateData();
			UltimateSurvival.MonoSingleton<UI_Backgroud>.Instance.Value = true;
			this.Draggable.transform.localPosition = new Vector3(0f, 0f, 0f);
			this.Draggable.transform.localScale = Vector3.one * 0.81f;
			UINPCJiaoHu.AllShouldHide = true;
			ESCCloseManager.Inst.RegisterClose(this);
		}

		// Token: 0x060053D3 RID: 21459 RVA: 0x0003BEE9 File Offset: 0x0003A0E9
		private void OnDestroy()
		{
			UINPCJiaoHu.AllShouldHide = false;
		}

		// Token: 0x060053D4 RID: 21460 RVA: 0x0003BEF1 File Offset: 0x0003A0F1
		public void ClickTab()
		{
			if (SingletonMono<TabUIMag>.Instance != null)
			{
				SingletonMono<TabUIMag>.Instance.TryEscClose();
				return;
			}
			TabUIMag.OpenTab(0, null);
		}

		// Token: 0x060053D5 RID: 21461 RVA: 0x0003BF13 File Offset: 0x0003A113
		public bool TryEscClose()
		{
			if (SingletonMono<TabUIMag>.Instance != null)
			{
				SingletonMono<TabUIMag>.Instance.TryEscClose();
			}
			return true;
		}

		// Token: 0x060053D6 RID: 21462 RVA: 0x0022EC00 File Offset: 0x0022CE00
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

		// Token: 0x0400537F RID: 21375
		public static Inventory2 inventory;

		// Token: 0x04005380 RID: 21376
		public static Equip_Manager equip;

		// Token: 0x04005381 RID: 21377
		public static Store store;

		// Token: 0x04005382 RID: 21378
		public static Key key;

		// Token: 0x04005383 RID: 21379
		public static Key key2;

		// Token: 0x04005384 RID: 21380
		public static Skill_UI skillUI;

		// Token: 0x04005385 RID: 21381
		public static Skill_UIST skillUI2;

		// Token: 0x04005386 RID: 21382
		public static Money money;

		// Token: 0x04005387 RID: 21383
		public static UI_Manager UI;

		// Token: 0x04005388 RID: 21384
		public static XiuLian biguan;

		// Token: 0x04005389 RID: 21385
		public static Fader fader;

		// Token: 0x0400538A RID: 21386
		public PaiMaiHang paiMaiHang;

		// Token: 0x0400538B RID: 21387
		public ExchangePlan exchengePlan;

		// Token: 0x0400538C RID: 21388
		public TuJianMag TuJIanPlan;

		// Token: 0x0400538D RID: 21389
		public static ShopExchenge_UI shopExchenge_UI;

		// Token: 0x0400538E RID: 21390
		public GameObject Draggable;

		// Token: 0x0400538F RID: 21391
		public SeaMapUI seaMapUI;

		// Token: 0x04005390 RID: 21392
		public static Singleton ints;

		// Token: 0x04005391 RID: 21393
		public static TooltipsBackgroundi ToolTipsBackGround;

		// Token: 0x04005392 RID: 21394
		private bool OpenDeBug;

		// Token: 0x04005393 RID: 21395
		private GameObject DebugCanvas;
	}
}
