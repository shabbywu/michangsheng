using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace YSGame.TuJian
{
	// Token: 0x02000A9F RID: 2719
	public class TuJianManager : MonoBehaviour, IESCClose
	{
		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x06004C23 RID: 19491 RVA: 0x002083E6 File Offset: 0x002065E6
		// (set) Token: 0x06004C24 RID: 19492 RVA: 0x002083EE File Offset: 0x002065EE
		public string LastHyperlink { get; set; }

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x06004C25 RID: 19493 RVA: 0x002083F7 File Offset: 0x002065F7
		// (set) Token: 0x06004C26 RID: 19494 RVA: 0x002083FF File Offset: 0x002065FF
		public string NowHyperlink { get; set; }

		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x06004C27 RID: 19495 RVA: 0x00208408 File Offset: 0x00206608
		// (set) Token: 0x06004C28 RID: 19496 RVA: 0x00208410 File Offset: 0x00206610
		public string NowPageHyperlink { get; set; }

		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x06004C29 RID: 19497 RVA: 0x00208419 File Offset: 0x00206619
		// (set) Token: 0x06004C2A RID: 19498 RVA: 0x00208421 File Offset: 0x00206621
		public TuJianTabType NowTuJianTab
		{
			get
			{
				return this._NowTuJianTab;
			}
			set
			{
				this._NowTuJianTab = value;
				this.ChangeTuJianTab(this._NowTuJianTab);
			}
		}

		// Token: 0x06004C2B RID: 19499 RVA: 0x00208438 File Offset: 0x00206638
		private void Awake()
		{
			if (TuJianManager.Inst == null)
			{
				TuJianManager.Inst = this;
				this.scaler = base.GetComponentInChildren<CanvasScaler>();
				Object.DontDestroyOnLoad(base.gameObject);
				this._Canvas = base.transform.Find("Canvas").GetComponent<Canvas>();
				this.Searcher = base.transform.Find("Canvas/Panel/TopTabBtns/Seacher").GetComponent<TuJianSearcher>();
				TuJianDB.InitDB();
				this.TuJianSave = YSSaveGame.GetJsonObject("TuJianSave", null);
				if (this.TuJianSave == null)
				{
					this.TuJianSave = new JSONObject(JSONObject.Type.OBJECT);
				}
				this.CloseTuJian();
				base.StartCoroutine("InitTab");
				return;
			}
			Object.Destroy(base.gameObject);
		}

		// Token: 0x06004C2C RID: 19500 RVA: 0x002084F0 File Offset: 0x002066F0
		private IEnumerator InitTab()
		{
			this.ChangeTuJianTab(TuJianTabType.Item);
			yield return new WaitForSeconds(0.1f);
			this.ChangeTuJianTab(TuJianTabType.Rule);
			yield return new WaitForSeconds(0.1f);
			this.ChangeTuJianTab(TuJianTabType.Map);
			yield return new WaitForSeconds(0.1f);
			this.ChangeTuJianTab(TuJianTabType.Item);
			yield return 0;
			yield break;
		}

		// Token: 0x06004C2D RID: 19501 RVA: 0x00208500 File Offset: 0x00206700
		private void Update()
		{
			if (!this._IsInited)
			{
				if (TuJianDB._IsInited)
				{
					this.NowTuJianTab = TuJianTabType.Item;
				}
				this._IsInited = true;
			}
			float num = (float)Screen.height / (float)Screen.width;
			this.scaler.referenceResolution = new Vector2(1080f / num, 1080f);
		}

		// Token: 0x06004C2E RID: 19502 RVA: 0x00208554 File Offset: 0x00206754
		public void OnHyperlink(string link)
		{
			try
			{
				link = link.Replace("\r", "").Replace("\n", "");
				string[] array = link.Split(new char[]
				{
					'_'
				});
				if (array.Length >= 3)
				{
					this.LastHyperlink = this.NowPageHyperlink;
					this.NowPageHyperlink = link;
					this.NowHyperlink = link;
					int[] array2 = new int[array.Length];
					for (int i = 0; i < array.Length; i++)
					{
						array2[i] = int.Parse(array[i]);
					}
					TuJianTabType tuJianTabType = (TuJianTabType)(array2[0] - 1);
					this.OpenTuJian();
					this.NowTuJianTab = tuJianTabType;
					TuJianManager.TabDict[tuJianTabType].OnHyperlink(array2);
				}
			}
			catch (Exception arg)
			{
				Debug.LogError(string.Format("解析超链接时出错，超链接:{0} \n{1}", link, arg));
			}
		}

		// Token: 0x06004C2F RID: 19503 RVA: 0x00208624 File Offset: 0x00206824
		public void ReturnHyperlink()
		{
			if (!string.IsNullOrEmpty(this.LastHyperlink))
			{
				this.OnHyperlink(this.LastHyperlink);
			}
		}

		// Token: 0x06004C30 RID: 19504 RVA: 0x0020863F File Offset: 0x0020683F
		public bool CanReturn()
		{
			return !string.IsNullOrEmpty(this.LastHyperlink) && this.LastHyperlink.Length >= 5;
		}

		// Token: 0x06004C31 RID: 19505 RVA: 0x00208660 File Offset: 0x00206860
		private void ChangeTuJianTab(TuJianTabType tabType)
		{
			foreach (TuJianTab tuJianTab in TuJianManager.TabDict.Values)
			{
				if (tuJianTab.TabType == tabType)
				{
					tuJianTab.Show();
				}
				else
				{
					tuJianTab.Hide();
				}
			}
		}

		// Token: 0x06004C32 RID: 19506 RVA: 0x002086C8 File Offset: 0x002068C8
		public void OnButtonClick()
		{
			TuJianManager.TabDict[this.NowTuJianTab].OnButtonClick();
		}

		// Token: 0x06004C33 RID: 19507 RVA: 0x002086E0 File Offset: 0x002068E0
		public void OpenTuJian()
		{
			if (!this._Canvas.enabled)
			{
				this._Canvas.enabled = true;
				if (LianDanSystemManager.inst != null && LianDanSystemManager.inst.isActiveAndEnabled)
				{
					this.OnHyperlink("2_203_0");
				}
				else if (LianQiTotalManager.inst != null && LianQiTotalManager.inst.checkIsInLianQiPage())
				{
					this.OnHyperlink("2_204_0");
				}
			}
			this._Canvas.enabled = true;
			base.transform.position = Vector3.zero;
			Tools.canClickFlag = false;
			this.UnlockHongDian(1);
			ESCCloseManager.Inst.RegisterClose(this);
		}

		// Token: 0x06004C34 RID: 19508 RVA: 0x00208784 File Offset: 0x00206984
		public void CloseTuJian()
		{
			base.transform.position = new Vector3(10000f, 10000f, 10000f);
			this._Canvas.enabled = false;
			Tools.canClickFlag = true;
			PanelMamager.inst.nowPanel = PanelMamager.PanelType.空;
			ESCCloseManager.Inst.UnRegisterClose(this);
		}

		// Token: 0x06004C35 RID: 19509 RVA: 0x002087D8 File Offset: 0x002069D8
		public void ClearSearch()
		{
			this.Searcher.input.text = "";
			TuJianItemTab.Inst.PinJieDropdown.value = 0;
			TuJianItemTab.Inst.ShuXingDropdown.value = 0;
		}

		// Token: 0x06004C36 RID: 19510 RVA: 0x0020880F File Offset: 0x00206A0F
		public void Save()
		{
			YSSaveGame.save("TuJianSave", this.TuJianSave, "-1");
		}

		// Token: 0x06004C37 RID: 19511 RVA: 0x00208828 File Offset: 0x00206A28
		private bool CheckStringSave(string saveName, string str)
		{
			if (this.TuJianSave.HasField(saveName))
			{
				using (List<JSONObject>.Enumerator enumerator = this.TuJianSave[saveName].list.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.str == str)
						{
							return true;
						}
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06004C38 RID: 19512 RVA: 0x002088A0 File Offset: 0x00206AA0
		private void WriteStringSave(string saveName, string str)
		{
			if (!this.TuJianSave.HasField(saveName))
			{
				this.TuJianSave.AddField(saveName, new JSONObject(JSONObject.Type.ARRAY));
			}
			this.TuJianSave[saveName].Add(str);
		}

		// Token: 0x06004C39 RID: 19513 RVA: 0x002088D4 File Offset: 0x00206AD4
		private bool CheckIntSave(string saveName, int i)
		{
			if (this.TuJianSave.HasField(saveName))
			{
				using (List<JSONObject>.Enumerator enumerator = this.TuJianSave[saveName].list.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.I == i)
						{
							return true;
						}
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06004C3A RID: 19514 RVA: 0x00208948 File Offset: 0x00206B48
		private void WriteIntSave(string saveName, int i)
		{
			if (!this.TuJianSave.HasField(saveName))
			{
				this.TuJianSave.AddField(saveName, new JSONObject(JSONObject.Type.ARRAY));
			}
			this.TuJianSave[saveName].Add(i);
		}

		// Token: 0x06004C3B RID: 19515 RVA: 0x0020897C File Offset: 0x00206B7C
		public void UnlockMap(string mapName)
		{
			if (!this.IsUnlockedMap(mapName))
			{
				this.WriteStringSave("UnlockedMap", mapName);
			}
		}

		// Token: 0x06004C3C RID: 19516 RVA: 0x00208993 File Offset: 0x00206B93
		public void UnlockItem(int id)
		{
			if (!this.IsUnlockedItem(id))
			{
				this.WriteIntSave("UnlockedItem", id);
			}
		}

		// Token: 0x06004C3D RID: 19517 RVA: 0x002089AA File Offset: 0x00206BAA
		public void UnlockZhuYao(int id)
		{
			PlayTutorial.CheckCaoYao2();
			if (!this.IsUnlockedZhuYao(id))
			{
				this.WriteIntSave("UnlockedCaoYaoZhuYao", id);
			}
		}

		// Token: 0x06004C3E RID: 19518 RVA: 0x002089C6 File Offset: 0x00206BC6
		public void UnlockFuYao(int id)
		{
			PlayTutorial.CheckCaoYao2();
			if (!this.IsUnlockedFuYao(id))
			{
				this.WriteIntSave("UnlockedCaoYaoFuYao", id);
			}
		}

		// Token: 0x06004C3F RID: 19519 RVA: 0x002089E2 File Offset: 0x00206BE2
		public void UnlockYaoYin(int id)
		{
			PlayTutorial.CheckCaoYao2();
			if (!this.IsUnlockedYaoYin(id))
			{
				this.WriteIntSave("UnlockedCaoYaoYaoYin", id);
			}
		}

		// Token: 0x06004C40 RID: 19520 RVA: 0x002089FE File Offset: 0x00206BFE
		public void UnlockSkill(int id)
		{
			if (!this.IsUnlockedSkill(id))
			{
				this.WriteIntSave("UnlockedSkill", id);
			}
		}

		// Token: 0x06004C41 RID: 19521 RVA: 0x00208A15 File Offset: 0x00206C15
		public void UnlockGongFa(int id)
		{
			if (!this.IsUnlockedGongFa(id))
			{
				this.WriteIntSave("UnlockedGongFa", id);
			}
		}

		// Token: 0x06004C42 RID: 19522 RVA: 0x00208A2C File Offset: 0x00206C2C
		public void UnlockHongDian(int id)
		{
			if (!this.IsUnlockedHongDian(id))
			{
				this.WriteIntSave("UnlockedHongDian", id);
			}
		}

		// Token: 0x06004C43 RID: 19523 RVA: 0x00208A43 File Offset: 0x00206C43
		public void UnlockDeath(int id)
		{
			if (!this.IsUnlockedDeath(id))
			{
				this.WriteIntSave("UnlockedDeath", id);
			}
		}

		// Token: 0x06004C44 RID: 19524 RVA: 0x00208A5A File Offset: 0x00206C5A
		public bool IsUnlockedMap(string mapName)
		{
			return this.CheckStringSave("UnlockedMap", mapName);
		}

		// Token: 0x06004C45 RID: 19525 RVA: 0x00208A68 File Offset: 0x00206C68
		public bool IsUnlockedItem(int id)
		{
			return this.CheckIntSave("UnlockedItem", id);
		}

		// Token: 0x06004C46 RID: 19526 RVA: 0x00208A76 File Offset: 0x00206C76
		public bool IsUnlockedZhuYao(int id)
		{
			return this.CheckIntSave("UnlockedCaoYaoZhuYao", id);
		}

		// Token: 0x06004C47 RID: 19527 RVA: 0x00208A84 File Offset: 0x00206C84
		public bool IsUnlockedFuYao(int id)
		{
			return this.CheckIntSave("UnlockedCaoYaoFuYao", id);
		}

		// Token: 0x06004C48 RID: 19528 RVA: 0x00208A92 File Offset: 0x00206C92
		public bool IsUnlockedYaoYin(int id)
		{
			return this.CheckIntSave("UnlockedCaoYaoYaoYin", id);
		}

		// Token: 0x06004C49 RID: 19529 RVA: 0x00208AA0 File Offset: 0x00206CA0
		public bool IsUnlockedSkill(int id)
		{
			return this.CheckIntSave("UnlockedSkill", id);
		}

		// Token: 0x06004C4A RID: 19530 RVA: 0x00208AAE File Offset: 0x00206CAE
		public bool IsUnlockedGongFa(int id)
		{
			return this.CheckIntSave("UnlockedGongFa", id);
		}

		// Token: 0x06004C4B RID: 19531 RVA: 0x00208ABC File Offset: 0x00206CBC
		public bool IsUnlockedHongDian(int id)
		{
			return this.CheckIntSave("UnlockedHongDian", id);
		}

		// Token: 0x06004C4C RID: 19532 RVA: 0x00208ACA File Offset: 0x00206CCA
		public bool IsUnlockedDeath(int id)
		{
			return this.CheckIntSave("UnlockedDeath", id);
		}

		// Token: 0x06004C4D RID: 19533 RVA: 0x00208AD8 File Offset: 0x00206CD8
		public bool TryEscClose()
		{
			if (this._Canvas.enabled)
			{
				this.CloseTuJian();
				return true;
			}
			return false;
		}

		// Token: 0x04004B6C RID: 19308
		[HideInInspector]
		public static TuJianManager Inst;

		// Token: 0x04004B6D RID: 19309
		[HideInInspector]
		public static Dictionary<TuJianTabType, TuJianTab> TabDict = new Dictionary<TuJianTabType, TuJianTab>();

		// Token: 0x04004B6E RID: 19310
		[HideInInspector]
		public static bool IsDebugMode = false;

		// Token: 0x04004B72 RID: 19314
		[HideInInspector]
		public Canvas _Canvas;

		// Token: 0x04004B73 RID: 19315
		private TuJianTabType _NowTuJianTab;

		// Token: 0x04004B74 RID: 19316
		private bool _IsInited;

		// Token: 0x04004B75 RID: 19317
		[HideInInspector]
		public TuJianSearcher Searcher;

		// Token: 0x04004B76 RID: 19318
		[HideInInspector]
		public bool NeedRefreshDataList;

		// Token: 0x04004B77 RID: 19319
		private CanvasScaler scaler;

		// Token: 0x04004B78 RID: 19320
		public JSONObject TuJianSave;
	}
}
