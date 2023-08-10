using System.Collections.Generic;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

public class UILingTianPanel : MonoBehaviour, IESCClose
{
	public static UILingTianPanel Inst;

	public List<UILingTianCell> LingTianList;

	public Text LingLiSpeedText;

	public UIInventory PlayerInventory;

	public GameObject ShouGeBtn1;

	public GameObject ShouGeBtn2;

	public Texture2D ShouGeCur;

	public GameObject LvPingBtn1;

	public GameObject LvPingbtn2;

	public Text LvPingShengYuTime;

	[HideInInspector]
	public float BaseSpeedPer;

	[HideInInspector]
	public float CuiShengSpeedPer;

	[HideInInspector]
	public int CuiShengTime;

	[HideInInspector]
	public int CuiShengLingShi50Year;

	private bool isShouGe;

	[HideInInspector]
	public bool IsShouGe
	{
		get
		{
			return isShouGe;
		}
		set
		{
			//IL_0022: Unknown result type (might be due to invalid IL or missing references)
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			isShouGe = value;
			if (isShouGe)
			{
				Cursor.SetCursor(ShouGeCur, Vector2.zero, (CursorMode)0);
			}
			else
			{
				Cursor.SetCursor((Texture2D)null, Vector2.zero, (CursorMode)0);
			}
		}
	}

	private void Awake()
	{
		Inst = this;
	}

	public void RefreshUI()
	{
		IsShouGe = false;
		CalcSpeed();
		foreach (UILingTianCell lingTian in LingTianList)
		{
			lingTian.RefreshUI();
		}
		PlayerInventory.RefreshUI();
		Avatar player = PlayerEx.Player;
		int i = player.DongFuData[$"DongFu{DongFuManager.NowDongFuID}"]["LingYanLevel"].I;
		int i2 = player.DongFuData[$"DongFu{DongFuManager.NowDongFuID}"]["JuLingZhenLevel"].I;
		int lingtiansudu = DFLingYanLevel.DataDict[i].lingtiansudu;
		int lingtiansudu2 = DFZhenYanLevel.DataDict[i2].lingtiansudu;
		int lingtiancuishengsudu = DFZhenYanLevel.DataDict[i2].lingtiancuishengsudu;
		int i3 = player.DongFuData[$"DongFu{DongFuManager.NowDongFuID}"]["LingTian"]["CuiShengLingLi"].I;
		int num = lingtiansudu + lingtiansudu2;
		if (i3 > 0)
		{
			num += lingtiancuishengsudu;
			LingLiSpeedText.text = $"灵气供给 <color=#99d0d1>{num}({lingtiansudu + lingtiansudu2}+{lingtiancuishengsudu})</color>/月";
			LvPingBtn1.SetActive(false);
			LvPingbtn2.SetActive(true);
			int num2 = CuiShengTime / 12;
			int num3 = CuiShengTime % 12;
			if (num2 > 0)
			{
				LvPingShengYuTime.text = $"剩{num2}年{num3}月";
			}
			else
			{
				LvPingShengYuTime.text = $"剩{num3}月";
			}
		}
		else
		{
			LingLiSpeedText.text = $"灵气供给 <color=#99d0d1>{num}</color>/月";
			LvPingBtn1.SetActive(true);
			LvPingbtn2.SetActive(false);
		}
		if (!HasLvPing())
		{
			LvPingBtn1.SetActive(false);
			LvPingbtn2.SetActive(false);
		}
	}

	public void CalcSpeed(int exLingShi = 0)
	{
		Avatar player = PlayerEx.Player;
		string index = $"DongFu{DongFuManager.NowDongFuID}";
		int num = 0;
		for (int i = 0; i < DongFuManager.LingTianCount; i++)
		{
			if (player.DongFuData[index]["LingTian"][$"Slot{i}"]["ID"].I > 0)
			{
				num++;
			}
		}
		int i2 = player.DongFuData[index]["LingYanLevel"].I;
		int i3 = player.DongFuData[index]["JuLingZhenLevel"].I;
		int lingtiansudu = DFLingYanLevel.DataDict[i2].lingtiansudu;
		int lingtiansudu2 = DFZhenYanLevel.DataDict[i3].lingtiansudu;
		int lingtiancuishengsudu = DFZhenYanLevel.DataDict[i3].lingtiancuishengsudu;
		int num2 = player.DongFuData[index]["LingTian"]["CuiShengLingLi"].I + exLingShi;
		if (num > 0)
		{
			BaseSpeedPer = (float)(lingtiansudu + lingtiansudu2) / (float)num;
			CuiShengSpeedPer = (float)lingtiancuishengsudu / (float)num;
		}
		CuiShengTime = num2 / lingtiancuishengsudu;
		CuiShengLingShi50Year = 600 * lingtiancuishengsudu;
	}

	public void OnShouGeBtnClick()
	{
		IsShouGe = !IsShouGe;
		if (IsShouGe)
		{
			ShouGeBtn1.SetActive(false);
			ShouGeBtn2.SetActive(true);
		}
		else
		{
			ShouGeBtn1.SetActive(true);
			ShouGeBtn2.SetActive(false);
		}
	}

	public bool HasLvPing()
	{
		return PlayerEx.Player.hasItem(10046);
	}

	public void OnLiKaiBtnClick()
	{
		IsShouGe = false;
		UIDongFu.Inst.HideLingTianPanel();
	}

	public void OnLvPingBtnClick()
	{
		if (PlayerEx.Player.money != 0)
		{
			UILingLiChongNeng.Show();
		}
		else
		{
			UIPopTip.Inst.Pop("没有灵石无法使用");
		}
	}

	public bool TryEscClose()
	{
		OnLiKaiBtnClick();
		return true;
	}
}
