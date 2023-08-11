using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace YSGame.TuJian;

public class TuJianManager : MonoBehaviour, IESCClose
{
	[HideInInspector]
	public static TuJianManager Inst;

	[HideInInspector]
	public static Dictionary<TuJianTabType, TuJianTab> TabDict = new Dictionary<TuJianTabType, TuJianTab>();

	[HideInInspector]
	public static bool IsDebugMode = false;

	[HideInInspector]
	public Canvas _Canvas;

	private TuJianTabType _NowTuJianTab;

	private bool _IsInited;

	[HideInInspector]
	public TuJianSearcher Searcher;

	[HideInInspector]
	public bool NeedRefreshDataList;

	private CanvasScaler scaler;

	public JSONObject TuJianSave;

	public string LastHyperlink { get; set; }

	public string NowHyperlink { get; set; }

	public string NowPageHyperlink { get; set; }

	public TuJianTabType NowTuJianTab
	{
		get
		{
			return _NowTuJianTab;
		}
		set
		{
			_NowTuJianTab = value;
			ChangeTuJianTab(_NowTuJianTab);
		}
	}

	private void Awake()
	{
		if ((Object)(object)Inst == (Object)null)
		{
			Inst = this;
			scaler = ((Component)this).GetComponentInChildren<CanvasScaler>();
			Object.DontDestroyOnLoad((Object)(object)((Component)this).gameObject);
			_Canvas = ((Component)((Component)this).transform.Find("Canvas")).GetComponent<Canvas>();
			Searcher = ((Component)((Component)this).transform.Find("Canvas/Panel/TopTabBtns/Seacher")).GetComponent<TuJianSearcher>();
			TuJianDB.InitDB();
			TuJianSave = YSSaveGame.GetJsonObject("TuJianSave");
			if (TuJianSave == null)
			{
				TuJianSave = new JSONObject(JSONObject.Type.OBJECT);
			}
			CloseTuJian();
			((MonoBehaviour)this).StartCoroutine("InitTab");
		}
		else
		{
			Object.Destroy((Object)(object)((Component)this).gameObject);
		}
	}

	private IEnumerator InitTab()
	{
		ChangeTuJianTab(TuJianTabType.Item);
		yield return (object)new WaitForSeconds(0.1f);
		ChangeTuJianTab(TuJianTabType.Rule);
		yield return (object)new WaitForSeconds(0.1f);
		ChangeTuJianTab(TuJianTabType.Map);
		yield return (object)new WaitForSeconds(0.1f);
		ChangeTuJianTab(TuJianTabType.Item);
		yield return 0;
	}

	private void Update()
	{
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		if (!_IsInited)
		{
			if (TuJianDB._IsInited)
			{
				NowTuJianTab = TuJianTabType.Item;
			}
			_IsInited = true;
		}
		float num = (float)Screen.height / (float)Screen.width;
		scaler.referenceResolution = new Vector2(1080f / num, 1080f);
	}

	public void OnHyperlink(string link)
	{
		try
		{
			link = link.Replace("\r", "").Replace("\n", "");
			string[] array = link.Split(new char[1] { '_' });
			if (array.Length >= 3)
			{
				LastHyperlink = NowPageHyperlink;
				NowPageHyperlink = link;
				NowHyperlink = link;
				int[] array2 = new int[array.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array2[i] = int.Parse(array[i]);
				}
				TuJianTabType tuJianTabType = (TuJianTabType)(array2[0] - 1);
				OpenTuJian();
				NowTuJianTab = tuJianTabType;
				TabDict[tuJianTabType].OnHyperlink(array2);
			}
		}
		catch (Exception arg)
		{
			Debug.LogError((object)$"解析超链接时出错，超链接:{link} \n{arg}");
		}
	}

	public void ReturnHyperlink()
	{
		if (!string.IsNullOrEmpty(LastHyperlink))
		{
			OnHyperlink(LastHyperlink);
		}
	}

	public bool CanReturn()
	{
		if (!string.IsNullOrEmpty(LastHyperlink) && LastHyperlink.Length >= 5)
		{
			return true;
		}
		return false;
	}

	private void ChangeTuJianTab(TuJianTabType tabType)
	{
		foreach (TuJianTab value in TabDict.Values)
		{
			if (value.TabType == tabType)
			{
				value.Show();
			}
			else
			{
				value.Hide();
			}
		}
	}

	public void OnButtonClick()
	{
		TabDict[NowTuJianTab].OnButtonClick();
	}

	public void OpenTuJian()
	{
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		if (!((Behaviour)_Canvas).enabled)
		{
			((Behaviour)_Canvas).enabled = true;
			if ((Object)(object)LianDanSystemManager.inst != (Object)null && ((Behaviour)LianDanSystemManager.inst).isActiveAndEnabled)
			{
				OnHyperlink("2_203_0");
			}
			else if ((Object)(object)LianQiTotalManager.inst != (Object)null && LianQiTotalManager.inst.checkIsInLianQiPage())
			{
				OnHyperlink("2_204_0");
			}
		}
		((Behaviour)_Canvas).enabled = true;
		((Component)this).transform.position = Vector3.zero;
		Tools.canClickFlag = false;
		UnlockHongDian(1);
		ESCCloseManager.Inst.RegisterClose(this);
	}

	public void CloseTuJian()
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		((Component)this).transform.position = new Vector3(10000f, 10000f, 10000f);
		((Behaviour)_Canvas).enabled = false;
		Tools.canClickFlag = true;
		PanelMamager.inst.nowPanel = PanelMamager.PanelType.空;
		ESCCloseManager.Inst.UnRegisterClose(this);
	}

	public void ClearSearch()
	{
		Searcher.input.text = "";
		TuJianItemTab.Inst.PinJieDropdown.value = 0;
		TuJianItemTab.Inst.ShuXingDropdown.value = 0;
	}

	public void Save()
	{
		YSSaveGame.save("TuJianSave", TuJianSave);
	}

	private bool CheckStringSave(string saveName, string str)
	{
		if (TuJianSave.HasField(saveName))
		{
			foreach (JSONObject item in TuJianSave[saveName].list)
			{
				if (item.str == str)
				{
					return true;
				}
			}
		}
		return false;
	}

	private void WriteStringSave(string saveName, string str)
	{
		if (!TuJianSave.HasField(saveName))
		{
			TuJianSave.AddField(saveName, new JSONObject(JSONObject.Type.ARRAY));
		}
		TuJianSave[saveName].Add(str);
	}

	private bool CheckIntSave(string saveName, int i)
	{
		if (TuJianSave.HasField(saveName))
		{
			foreach (JSONObject item in TuJianSave[saveName].list)
			{
				if (item.I == i)
				{
					return true;
				}
			}
		}
		return false;
	}

	private void WriteIntSave(string saveName, int i)
	{
		if (!TuJianSave.HasField(saveName))
		{
			TuJianSave.AddField(saveName, new JSONObject(JSONObject.Type.ARRAY));
		}
		TuJianSave[saveName].Add(i);
	}

	public void UnlockMap(string mapName)
	{
		if (!IsUnlockedMap(mapName))
		{
			WriteStringSave("UnlockedMap", mapName);
		}
	}

	public void UnlockItem(int id)
	{
		if (!IsUnlockedItem(id))
		{
			WriteIntSave("UnlockedItem", id);
		}
	}

	public void UnlockZhuYao(int id)
	{
		PlayTutorial.CheckCaoYao2();
		if (!IsUnlockedZhuYao(id))
		{
			WriteIntSave("UnlockedCaoYaoZhuYao", id);
		}
	}

	public void UnlockFuYao(int id)
	{
		PlayTutorial.CheckCaoYao2();
		if (!IsUnlockedFuYao(id))
		{
			WriteIntSave("UnlockedCaoYaoFuYao", id);
		}
	}

	public void UnlockYaoYin(int id)
	{
		PlayTutorial.CheckCaoYao2();
		if (!IsUnlockedYaoYin(id))
		{
			WriteIntSave("UnlockedCaoYaoYaoYin", id);
		}
	}

	public void UnlockSkill(int id)
	{
		if (!IsUnlockedSkill(id))
		{
			WriteIntSave("UnlockedSkill", id);
		}
	}

	public void UnlockGongFa(int id)
	{
		if (!IsUnlockedGongFa(id))
		{
			WriteIntSave("UnlockedGongFa", id);
		}
	}

	public void UnlockHongDian(int id)
	{
		if (!IsUnlockedHongDian(id))
		{
			WriteIntSave("UnlockedHongDian", id);
		}
	}

	public void UnlockDeath(int id)
	{
		if (!IsUnlockedDeath(id))
		{
			WriteIntSave("UnlockedDeath", id);
		}
	}

	public bool IsUnlockedMap(string mapName)
	{
		return CheckStringSave("UnlockedMap", mapName);
	}

	public bool IsUnlockedItem(int id)
	{
		return CheckIntSave("UnlockedItem", id);
	}

	public bool IsUnlockedZhuYao(int id)
	{
		return CheckIntSave("UnlockedCaoYaoZhuYao", id);
	}

	public bool IsUnlockedFuYao(int id)
	{
		return CheckIntSave("UnlockedCaoYaoFuYao", id);
	}

	public bool IsUnlockedYaoYin(int id)
	{
		return CheckIntSave("UnlockedCaoYaoYaoYin", id);
	}

	public bool IsUnlockedSkill(int id)
	{
		return CheckIntSave("UnlockedSkill", id);
	}

	public bool IsUnlockedGongFa(int id)
	{
		return CheckIntSave("UnlockedGongFa", id);
	}

	public bool IsUnlockedHongDian(int id)
	{
		return CheckIntSave("UnlockedHongDian", id);
	}

	public bool IsUnlockedDeath(int id)
	{
		return CheckIntSave("UnlockedDeath", id);
	}

	public bool TryEscClose()
	{
		if (((Behaviour)_Canvas).enabled)
		{
			CloseTuJian();
			return true;
		}
		return false;
	}
}
