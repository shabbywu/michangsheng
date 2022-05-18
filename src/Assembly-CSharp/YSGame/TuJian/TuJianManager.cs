using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace YSGame.TuJian
{
	// Token: 0x02000DDB RID: 3547
	public class TuJianManager : MonoBehaviour, IESCClose
	{
		// Token: 0x170007FC RID: 2044
		// (get) Token: 0x0600556A RID: 21866 RVA: 0x0003D0CA File Offset: 0x0003B2CA
		// (set) Token: 0x0600556B RID: 21867 RVA: 0x0003D0D2 File Offset: 0x0003B2D2
		public string LastHyperlink { get; set; }

		// Token: 0x170007FD RID: 2045
		// (get) Token: 0x0600556C RID: 21868 RVA: 0x0003D0DB File Offset: 0x0003B2DB
		// (set) Token: 0x0600556D RID: 21869 RVA: 0x0003D0E3 File Offset: 0x0003B2E3
		public string NowHyperlink { get; set; }

		// Token: 0x170007FE RID: 2046
		// (get) Token: 0x0600556E RID: 21870 RVA: 0x0003D0EC File Offset: 0x0003B2EC
		// (set) Token: 0x0600556F RID: 21871 RVA: 0x0003D0F4 File Offset: 0x0003B2F4
		public string NowPageHyperlink { get; set; }

		// Token: 0x170007FF RID: 2047
		// (get) Token: 0x06005570 RID: 21872 RVA: 0x0003D0FD File Offset: 0x0003B2FD
		// (set) Token: 0x06005571 RID: 21873 RVA: 0x0003D105 File Offset: 0x0003B305
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

		// Token: 0x06005572 RID: 21874 RVA: 0x002398D4 File Offset: 0x00237AD4
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

		// Token: 0x06005573 RID: 21875 RVA: 0x0003D11A File Offset: 0x0003B31A
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

		// Token: 0x06005574 RID: 21876 RVA: 0x0023998C File Offset: 0x00237B8C
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

		// Token: 0x06005575 RID: 21877 RVA: 0x002399E0 File Offset: 0x00237BE0
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

		// Token: 0x06005576 RID: 21878 RVA: 0x0003D129 File Offset: 0x0003B329
		public void ReturnHyperlink()
		{
			if (!string.IsNullOrEmpty(this.LastHyperlink))
			{
				this.OnHyperlink(this.LastHyperlink);
			}
		}

		// Token: 0x06005577 RID: 21879 RVA: 0x0003D144 File Offset: 0x0003B344
		public bool CanReturn()
		{
			return !string.IsNullOrEmpty(this.LastHyperlink) && this.LastHyperlink.Length >= 5;
		}

		// Token: 0x06005578 RID: 21880 RVA: 0x00239AB0 File Offset: 0x00237CB0
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

		// Token: 0x06005579 RID: 21881 RVA: 0x0003D164 File Offset: 0x0003B364
		public void OnButtonClick()
		{
			TuJianManager.TabDict[this.NowTuJianTab].OnButtonClick();
		}

		// Token: 0x0600557A RID: 21882 RVA: 0x00239B18 File Offset: 0x00237D18
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

		// Token: 0x0600557B RID: 21883 RVA: 0x00239BBC File Offset: 0x00237DBC
		public void CloseTuJian()
		{
			base.transform.position = new Vector3(10000f, 10000f, 10000f);
			this._Canvas.enabled = false;
			Tools.canClickFlag = true;
			PanelMamager.inst.nowPanel = PanelMamager.PanelType.空;
			ESCCloseManager.Inst.UnRegisterClose(this);
		}

		// Token: 0x0600557C RID: 21884 RVA: 0x0003D17B File Offset: 0x0003B37B
		public void ClearSearch()
		{
			this.Searcher.input.text = "";
			TuJianItemTab.Inst.PinJieDropdown.value = 0;
			TuJianItemTab.Inst.ShuXingDropdown.value = 0;
		}

		// Token: 0x0600557D RID: 21885 RVA: 0x0003D1B2 File Offset: 0x0003B3B2
		public void Save()
		{
			YSSaveGame.save("TuJianSave", this.TuJianSave, "-1");
		}

		// Token: 0x0600557E RID: 21886 RVA: 0x00239C10 File Offset: 0x00237E10
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

		// Token: 0x0600557F RID: 21887 RVA: 0x0003D1C9 File Offset: 0x0003B3C9
		private void WriteStringSave(string saveName, string str)
		{
			if (!this.TuJianSave.HasField(saveName))
			{
				this.TuJianSave.AddField(saveName, new JSONObject(JSONObject.Type.ARRAY));
			}
			this.TuJianSave[saveName].Add(str);
		}

		// Token: 0x06005580 RID: 21888 RVA: 0x00239C88 File Offset: 0x00237E88
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

		// Token: 0x06005581 RID: 21889 RVA: 0x0003D1FD File Offset: 0x0003B3FD
		private void WriteIntSave(string saveName, int i)
		{
			if (!this.TuJianSave.HasField(saveName))
			{
				this.TuJianSave.AddField(saveName, new JSONObject(JSONObject.Type.ARRAY));
			}
			this.TuJianSave[saveName].Add(i);
		}

		// Token: 0x06005582 RID: 21890 RVA: 0x0003D231 File Offset: 0x0003B431
		public void UnlockMap(string mapName)
		{
			if (!this.IsUnlockedMap(mapName))
			{
				this.WriteStringSave("UnlockedMap", mapName);
			}
		}

		// Token: 0x06005583 RID: 21891 RVA: 0x0003D248 File Offset: 0x0003B448
		public void UnlockItem(int id)
		{
			if (!this.IsUnlockedItem(id))
			{
				this.WriteIntSave("UnlockedItem", id);
			}
		}

		// Token: 0x06005584 RID: 21892 RVA: 0x0003D25F File Offset: 0x0003B45F
		public void UnlockZhuYao(int id)
		{
			PlayTutorial.CheckCaoYao2();
			if (!this.IsUnlockedZhuYao(id))
			{
				this.WriteIntSave("UnlockedCaoYaoZhuYao", id);
			}
		}

		// Token: 0x06005585 RID: 21893 RVA: 0x0003D27B File Offset: 0x0003B47B
		public void UnlockFuYao(int id)
		{
			PlayTutorial.CheckCaoYao2();
			if (!this.IsUnlockedFuYao(id))
			{
				this.WriteIntSave("UnlockedCaoYaoFuYao", id);
			}
		}

		// Token: 0x06005586 RID: 21894 RVA: 0x0003D297 File Offset: 0x0003B497
		public void UnlockYaoYin(int id)
		{
			PlayTutorial.CheckCaoYao2();
			if (!this.IsUnlockedYaoYin(id))
			{
				this.WriteIntSave("UnlockedCaoYaoYaoYin", id);
			}
		}

		// Token: 0x06005587 RID: 21895 RVA: 0x0003D2B3 File Offset: 0x0003B4B3
		public void UnlockSkill(int id)
		{
			if (!this.IsUnlockedSkill(id))
			{
				this.WriteIntSave("UnlockedSkill", id);
			}
		}

		// Token: 0x06005588 RID: 21896 RVA: 0x0003D2CA File Offset: 0x0003B4CA
		public void UnlockGongFa(int id)
		{
			if (!this.IsUnlockedGongFa(id))
			{
				this.WriteIntSave("UnlockedGongFa", id);
			}
		}

		// Token: 0x06005589 RID: 21897 RVA: 0x0003D2E1 File Offset: 0x0003B4E1
		public void UnlockHongDian(int id)
		{
			if (!this.IsUnlockedHongDian(id))
			{
				this.WriteIntSave("UnlockedHongDian", id);
			}
		}

		// Token: 0x0600558A RID: 21898 RVA: 0x0003D2F8 File Offset: 0x0003B4F8
		public void UnlockDeath(int id)
		{
			if (!this.IsUnlockedDeath(id))
			{
				this.WriteIntSave("UnlockedDeath", id);
			}
		}

		// Token: 0x0600558B RID: 21899 RVA: 0x0003D30F File Offset: 0x0003B50F
		public bool IsUnlockedMap(string mapName)
		{
			return this.CheckStringSave("UnlockedMap", mapName);
		}

		// Token: 0x0600558C RID: 21900 RVA: 0x0003D31D File Offset: 0x0003B51D
		public bool IsUnlockedItem(int id)
		{
			return this.CheckIntSave("UnlockedItem", id);
		}

		// Token: 0x0600558D RID: 21901 RVA: 0x0003D32B File Offset: 0x0003B52B
		public bool IsUnlockedZhuYao(int id)
		{
			return this.CheckIntSave("UnlockedCaoYaoZhuYao", id);
		}

		// Token: 0x0600558E RID: 21902 RVA: 0x0003D339 File Offset: 0x0003B539
		public bool IsUnlockedFuYao(int id)
		{
			return this.CheckIntSave("UnlockedCaoYaoFuYao", id);
		}

		// Token: 0x0600558F RID: 21903 RVA: 0x0003D347 File Offset: 0x0003B547
		public bool IsUnlockedYaoYin(int id)
		{
			return this.CheckIntSave("UnlockedCaoYaoYaoYin", id);
		}

		// Token: 0x06005590 RID: 21904 RVA: 0x0003D355 File Offset: 0x0003B555
		public bool IsUnlockedSkill(int id)
		{
			return this.CheckIntSave("UnlockedSkill", id);
		}

		// Token: 0x06005591 RID: 21905 RVA: 0x0003D363 File Offset: 0x0003B563
		public bool IsUnlockedGongFa(int id)
		{
			return this.CheckIntSave("UnlockedGongFa", id);
		}

		// Token: 0x06005592 RID: 21906 RVA: 0x0003D371 File Offset: 0x0003B571
		public bool IsUnlockedHongDian(int id)
		{
			return this.CheckIntSave("UnlockedHongDian", id);
		}

		// Token: 0x06005593 RID: 21907 RVA: 0x0003D37F File Offset: 0x0003B57F
		public bool IsUnlockedDeath(int id)
		{
			return this.CheckIntSave("UnlockedDeath", id);
		}

		// Token: 0x06005594 RID: 21908 RVA: 0x0003D38D File Offset: 0x0003B58D
		public bool TryEscClose()
		{
			if (this._Canvas.enabled)
			{
				this.CloseTuJian();
				return true;
			}
			return false;
		}

		// Token: 0x04005547 RID: 21831
		[HideInInspector]
		public static TuJianManager Inst;

		// Token: 0x04005548 RID: 21832
		[HideInInspector]
		public static Dictionary<TuJianTabType, TuJianTab> TabDict = new Dictionary<TuJianTabType, TuJianTab>();

		// Token: 0x04005549 RID: 21833
		[HideInInspector]
		public static bool IsDebugMode = false;

		// Token: 0x0400554D RID: 21837
		[HideInInspector]
		public Canvas _Canvas;

		// Token: 0x0400554E RID: 21838
		private TuJianTabType _NowTuJianTab;

		// Token: 0x0400554F RID: 21839
		private bool _IsInited;

		// Token: 0x04005550 RID: 21840
		[HideInInspector]
		public TuJianSearcher Searcher;

		// Token: 0x04005551 RID: 21841
		[HideInInspector]
		public bool NeedRefreshDataList;

		// Token: 0x04005552 RID: 21842
		private CanvasScaler scaler;

		// Token: 0x04005553 RID: 21843
		public JSONObject TuJianSave;
	}
}
