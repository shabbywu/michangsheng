using System;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002BD RID: 701
public static class GaoShiManager
{
	// Token: 0x0600189F RID: 6303 RVA: 0x000B0908 File Offset: 0x000AEB08
	public static void OnAddTime()
	{
		Avatar player = PlayerEx.Player;
		foreach (GaoShiLeiXing gaoShiLeiXing in GaoShiLeiXing.DataList)
		{
			if (!player.GaoShi.HasField(gaoShiLeiXing.id))
			{
				GaoShiManager.RefreshGaoShi(gaoShiLeiXing.id);
			}
			else
			{
				DateTime dateTime = DateTime.Parse(player.GaoShi[gaoShiLeiXing.id]["LastTime"].str);
				if (player.worldTimeMag.getNowTime() >= dateTime.AddMonths(gaoShiLeiXing.cd))
				{
					GaoShiManager.RefreshGaoShi(gaoShiLeiXing.id);
				}
			}
		}
	}

	// Token: 0x060018A0 RID: 6304 RVA: 0x000B09CC File Offset: 0x000AEBCC
	public static void RefreshGaoShi(string id)
	{
		Avatar player = PlayerEx.Player;
		GaoShiLeiXing gaoShiLeiXing = GaoShiLeiXing.DataDict[id];
		JSONObject jsonobject = new JSONObject(JSONObject.Type.OBJECT);
		jsonobject.SetField("LastTime", player.worldTimeMag.nowTime);
		List<GaoShi> list = new List<GaoShi>();
		List<GaoShi> list2 = new List<GaoShi>();
		List<GaoShi> list3 = new List<GaoShi>();
		for (int i = 0; i < gaoShiLeiXing.qujian.Count; i += 2)
		{
			int num = gaoShiLeiXing.qujian[i];
			int num2 = gaoShiLeiXing.qujian[i + 1];
			for (int j = num; j <= num2; j++)
			{
				GaoShi gaoShi = GaoShi.DataDict[j];
				if (gaoShi.type == 1)
				{
					list.Add(gaoShi);
				}
				else if (gaoShi.type == 2)
				{
					list2.Add(gaoShi);
				}
				else if (gaoShi.type == 3)
				{
					list3.Add(gaoShi);
				}
			}
		}
		JSONObject jsonobject2 = new JSONObject(JSONObject.Type.ARRAY);
		int num3 = gaoShiLeiXing.num[1];
		for (int k = 0; k < num3; k++)
		{
			if (list2.Count <= 0)
			{
				Debug.LogError("随机任务告示时出现随机库数量不足错误，地区ID" + id + "，请检查配表");
				break;
			}
			GaoShi gaoShi2 = list2[player.RandomSeedNext() % list2.Count];
			jsonobject2.Add(GaoShiManager.CreateRenWuData(gaoShi2));
			list2.Remove(gaoShi2);
		}
		int num4 = gaoShiLeiXing.num[2];
		for (int l = 0; l < num4; l++)
		{
			if (list3.Count <= 0)
			{
				Debug.LogError("随机情报告示时出现随机库数量不足错误，地区ID" + id + "，请检查配表");
				break;
			}
			GaoShi gaoShi3 = list3[player.RandomSeedNext() % list3.Count];
			jsonobject2.Add(GaoShiManager.CreateQingBaoData(gaoShi3));
			list3.Remove(gaoShi3);
		}
		int num5 = gaoShiLeiXing.num[0];
		if (num5 > list.Count)
		{
			Debug.LogError("随机收购告示时出现随机库数量不足错误，地区ID" + id + "，请检查配表");
		}
		else
		{
			List<int> list4 = new List<int>();
			for (int m = 0; m < list.Count; m++)
			{
				list4.Add(m);
			}
			for (int n = 0; n < num5; n++)
			{
				int num6 = list4[player.RandomSeedNext() % list4.Count];
				GaoShi gaoshi = list[num6];
				jsonobject2.Add(GaoShiManager.CreateShouGouData(gaoshi));
				list4.Remove(num6);
			}
		}
		jsonobject.SetField("GaoShiList", jsonobject2);
		player.GaoShi.SetField(id, jsonobject);
	}

	// Token: 0x060018A1 RID: 6305 RVA: 0x000B0C54 File Offset: 0x000AEE54
	public static JSONObject CreateShouGouData(GaoShi gaoshi)
	{
		JSONObject jsonobject = new JSONObject(JSONObject.Type.OBJECT);
		jsonobject.SetField("GaoShiID", gaoshi.id);
		jsonobject.SetField("Pos", GaoShiManager.CreateRandomPositionAndRotate());
		jsonobject.SetField("YiShouGou", false);
		bool val = PlayerEx.Player.RandomSeedNext() % 100 < 5;
		jsonobject.SetField("JiaJi", val);
		return jsonobject;
	}

	// Token: 0x060018A2 RID: 6306 RVA: 0x000B0CB1 File Offset: 0x000AEEB1
	public static JSONObject CreateRenWuData(GaoShi gaoshi)
	{
		JSONObject jsonobject = new JSONObject(JSONObject.Type.OBJECT);
		jsonobject.SetField("GaoShiID", gaoshi.id);
		jsonobject.SetField("Pos", GaoShiManager.CreateRandomPositionAndRotate());
		jsonobject.SetField("YiLingQu", false);
		return jsonobject;
	}

	// Token: 0x060018A3 RID: 6307 RVA: 0x000B0CE6 File Offset: 0x000AEEE6
	public static JSONObject CreateQingBaoData(GaoShi gaoshi)
	{
		JSONObject jsonobject = new JSONObject(JSONObject.Type.OBJECT);
		jsonobject.SetField("GaoShiID", gaoshi.id);
		jsonobject.SetField("Pos", GaoShiManager.CreateRandomPositionAndRotate());
		return jsonobject;
	}

	// Token: 0x060018A4 RID: 6308 RVA: 0x000B0D10 File Offset: 0x000AEF10
	public static JSONObject CreateRandomPositionAndRotate()
	{
		float val = (float)Random.Range(-50, 50);
		float val2 = (float)Random.Range(-50, 50);
		float val3 = (float)Random.Range(-5, 15);
		JSONObject jsonobject = new JSONObject(JSONObject.Type.OBJECT);
		jsonobject.SetField("x", val);
		jsonobject.SetField("y", val2);
		jsonobject.SetField("z", val3);
		return jsonobject;
	}

	// Token: 0x060018A5 RID: 6309 RVA: 0x000B0D68 File Offset: 0x000AEF68
	public static void SetYinZhangShow(RectMask2D YinZhangMask, RectTransform YinZhang, JSONObject pos, bool anim = false)
	{
		Vector2 vector = new Vector3(pos["x"].n, pos["y"].n);
		Vector3 localEulerAngles;
		localEulerAngles..ctor(0f, 0f, pos["z"].n);
		YinZhang.anchoredPosition += vector;
		YinZhang.localEulerAngles = localEulerAngles;
		if (anim)
		{
			YinZhangMask.enabled = false;
			YinZhang.localScale = new Vector3(1.5f, 1.5f, 1f);
			YinZhang.gameObject.SetActive(true);
			TweenerCore<Vector3, Vector3, VectorOptions> tweenerCore = TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOScale(YinZhang, Vector3.one, 0.5f), 28);
			tweenerCore.onComplete = (TweenCallback)Delegate.Combine(tweenerCore.onComplete, new TweenCallback(delegate()
			{
				YinZhangMask.enabled = true;
			}));
			return;
		}
		YinZhangMask.enabled = true;
		YinZhang.localScale = Vector3.one;
		YinZhang.gameObject.SetActive(true);
	}
}
