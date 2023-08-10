using System;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

public static class GaoShiManager
{
	public static void OnAddTime()
	{
		Avatar player = PlayerEx.Player;
		foreach (GaoShiLeiXing data in GaoShiLeiXing.DataList)
		{
			if (!player.GaoShi.HasField(data.id))
			{
				RefreshGaoShi(data.id);
				continue;
			}
			DateTime dateTime = DateTime.Parse(player.GaoShi[data.id]["LastTime"].str);
			if (player.worldTimeMag.getNowTime() >= dateTime.AddMonths(data.cd))
			{
				RefreshGaoShi(data.id);
			}
		}
	}

	public static void RefreshGaoShi(string id)
	{
		Avatar player = PlayerEx.Player;
		GaoShiLeiXing gaoShiLeiXing = GaoShiLeiXing.DataDict[id];
		JSONObject jSONObject = new JSONObject(JSONObject.Type.OBJECT);
		jSONObject.SetField("LastTime", player.worldTimeMag.nowTime);
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
		JSONObject jSONObject2 = new JSONObject(JSONObject.Type.ARRAY);
		int num3 = gaoShiLeiXing.num[1];
		for (int k = 0; k < num3; k++)
		{
			if (list2.Count > 0)
			{
				GaoShi gaoShi2 = list2[player.RandomSeedNext() % list2.Count];
				jSONObject2.Add(CreateRenWuData(gaoShi2));
				list2.Remove(gaoShi2);
				continue;
			}
			Debug.LogError((object)("随机任务告示时出现随机库数量不足错误，地区ID" + id + "，请检查配表"));
			break;
		}
		int num4 = gaoShiLeiXing.num[2];
		for (int l = 0; l < num4; l++)
		{
			if (list3.Count > 0)
			{
				GaoShi gaoShi3 = list3[player.RandomSeedNext() % list3.Count];
				jSONObject2.Add(CreateQingBaoData(gaoShi3));
				list3.Remove(gaoShi3);
				continue;
			}
			Debug.LogError((object)("随机情报告示时出现随机库数量不足错误，地区ID" + id + "，请检查配表"));
			break;
		}
		int num5 = gaoShiLeiXing.num[0];
		if (num5 > list.Count)
		{
			Debug.LogError((object)("随机收购告示时出现随机库数量不足错误，地区ID" + id + "，请检查配表"));
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
				jSONObject2.Add(CreateShouGouData(gaoshi));
				list4.Remove(num6);
			}
		}
		jSONObject.SetField("GaoShiList", jSONObject2);
		player.GaoShi.SetField(id, jSONObject);
	}

	public static JSONObject CreateShouGouData(GaoShi gaoshi)
	{
		JSONObject jSONObject = new JSONObject(JSONObject.Type.OBJECT);
		jSONObject.SetField("GaoShiID", gaoshi.id);
		jSONObject.SetField("Pos", CreateRandomPositionAndRotate());
		jSONObject.SetField("YiShouGou", val: false);
		bool val = PlayerEx.Player.RandomSeedNext() % 100 < 5;
		jSONObject.SetField("JiaJi", val);
		return jSONObject;
	}

	public static JSONObject CreateRenWuData(GaoShi gaoshi)
	{
		JSONObject jSONObject = new JSONObject(JSONObject.Type.OBJECT);
		jSONObject.SetField("GaoShiID", gaoshi.id);
		jSONObject.SetField("Pos", CreateRandomPositionAndRotate());
		jSONObject.SetField("YiLingQu", val: false);
		return jSONObject;
	}

	public static JSONObject CreateQingBaoData(GaoShi gaoshi)
	{
		JSONObject jSONObject = new JSONObject(JSONObject.Type.OBJECT);
		jSONObject.SetField("GaoShiID", gaoshi.id);
		jSONObject.SetField("Pos", CreateRandomPositionAndRotate());
		return jSONObject;
	}

	public static JSONObject CreateRandomPositionAndRotate()
	{
		float val = Random.Range(-50, 50);
		float val2 = Random.Range(-50, 50);
		float val3 = Random.Range(-5, 15);
		JSONObject jSONObject = new JSONObject(JSONObject.Type.OBJECT);
		jSONObject.SetField("x", val);
		jSONObject.SetField("y", val2);
		jSONObject.SetField("z", val3);
		return jSONObject;
	}

	public static void SetYinZhangShow(RectMask2D YinZhangMask, RectTransform YinZhang, JSONObject pos, bool anim = false)
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Expected O, but got Unknown
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Expected O, but got Unknown
		Vector2 val = Vector2.op_Implicit(new Vector3(pos["x"].n, pos["y"].n));
		Vector3 localEulerAngles = default(Vector3);
		((Vector3)(ref localEulerAngles))._002Ector(0f, 0f, pos["z"].n);
		YinZhang.anchoredPosition += val;
		((Transform)YinZhang).localEulerAngles = localEulerAngles;
		if (anim)
		{
			((Behaviour)YinZhangMask).enabled = false;
			((Transform)YinZhang).localScale = new Vector3(1.5f, 1.5f, 1f);
			((Component)YinZhang).gameObject.SetActive(true);
			TweenerCore<Vector3, Vector3, VectorOptions> obj = TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOScale((Transform)(object)YinZhang, Vector3.one, 0.5f), (Ease)28);
			((Tween)obj).onComplete = (TweenCallback)Delegate.Combine((Delegate?)(object)((Tween)obj).onComplete, (Delegate?)(TweenCallback)delegate
			{
				((Behaviour)YinZhangMask).enabled = true;
			});
		}
		else
		{
			((Behaviour)YinZhangMask).enabled = true;
			((Transform)YinZhang).localScale = Vector3.one;
			((Component)YinZhang).gameObject.SetActive(true);
		}
	}
}
