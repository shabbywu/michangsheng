using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using YSGame;

public class TianJieManager : MonoBehaviour
{
	[Serializable]
	[CompilerGenerated]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9 = new _003C_003Ec();

		public static UnityAction _003C_003E9__3_0;

		internal void _003COnAddTime_003Eb__3_0()
		{
			YSSaveGame.Reset();
			KBEngineApp.app.entities[10] = null;
			KBEngineApp.app.entities.Remove(10);
			Tools.instance.loadOtherScenes("MainMenu");
		}
	}

	public static Dictionary<string, string> LeiJieNames = new Dictionary<string, string>
	{
		{ "天雷劫", "tianlei" },
		{ "阴阳劫", "yinyang" },
		{ "风火劫", "fenghuo" },
		{ "心魔劫", "xinmo" },
		{ "罡雷劫", "ganglei" },
		{ "造化劫", "zaohua" },
		{ "混元劫", "hunyuan" },
		{ "五行劫", "wuxing" },
		{ "乾天劫", "qiantian" },
		{ "生死劫", "shengsi" },
		{ "天地劫", "tiandi" },
		{ "灭世劫", "mieshi" }
	};

	public static TianJieManager Inst;

	public TianJieEffectManager EffectManager;

	public int LeiJieIndex;

	public Text LeiJieNameText;

	public Text LeiJieDamageText;

	[HideInInspector]
	public bool YiXuLi;

	[HideInInspector]
	public int LeiJieCount;

	[HideInInspector]
	public List<string> LeiJieList = new List<string>();

	[HideInInspector]
	public string NowLeiJie => LeiJieList[LeiJieIndex];

	[HideInInspector]
	public int XuLiCount => PlayerEx.Player.OtherAvatar.buffmag.GetBuffSum(3150) + 1;

	public static void StartTianJieCD()
	{
		Avatar player = PlayerEx.Player;
		player.TianJie.SetField("HuaShenTime", player.worldTimeMag.nowTime);
		player.TianJie.SetField("JiaSuYear", 0);
		OnAddTime();
		player.TianJie.SetField("ShowTianJieCD", val: true);
	}

	public static void TianJieJiaSu(int year)
	{
		int i = PlayerEx.Player.TianJie["JiaSuYear"].I;
		PlayerEx.Player.TianJie.SetField("JiaSuYear", i + year);
		OnAddTime();
	}

	public static void OnAddTime()
	{
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Expected O, but got Unknown
		Avatar player = PlayerEx.Player;
		if (player == null)
		{
			return;
		}
		DateTime nowTime = player.worldTimeMag.getNowTime();
		DateTime dateTime = DateTime.Parse(player.TianJie["HuaShenTime"].str).AddYears(1000 - player.TianJie["JiaSuYear"].I);
		if (nowTime >= dateTime)
		{
			player.TianJie.SetField("ShengYuTime", "0年0月0日");
			player.TianJie.SetField("ShowTianJieCD", val: false);
			Debug.Log((object)"开始天劫");
			ESCCloseManager.Inst.CloseAll();
			object obj = _003C_003Ec._003C_003E9__3_0;
			if (obj == null)
			{
				UnityAction val = delegate
				{
					YSSaveGame.Reset();
					KBEngineApp.app.entities[10] = null;
					KBEngineApp.app.entities.Remove(10);
					Tools.instance.loadOtherScenes("MainMenu");
				};
				_003C_003Ec._003C_003E9__3_0 = val;
				obj = (object)val;
			}
			UCheckBox.Show("后续内容正在开发中，尚未开放\n确定后将返回主界面", (UnityAction)obj);
		}
		else
		{
			int num = (int)(dateTime.Subtract(nowTime).TotalDays / 365.0);
			player.TianJie.SetField("ShengYuTimeValue", num);
			if (num > 0)
			{
				player.TianJie.SetField("ShengYuTime", $"{num}年");
			}
			else
			{
				player.TianJie.SetField("ShengYuTime", "不足1年");
			}
		}
	}

	private void Awake()
	{
		Inst = this;
	}

	private void OnDestroy()
	{
		Inst = null;
	}

	private void OnGUI()
	{
		GUILayout.BeginVertical(GUI.skin.box, Array.Empty<GUILayoutOption>());
		GUILayout.Label($"雷劫血量:{PlayerEx.Player.OtherAvatar.HP}/{PlayerEx.Player.OtherAvatar.HP_Max}", Array.Empty<GUILayoutOption>());
		GUILayout.Label($"当前为第{RoundManager.instance.StaticRoundNum}回合", Array.Empty<GUILayoutOption>());
		for (int i = 0; i < LeiJieList.Count; i++)
		{
			GUILayout.Label($"第{i + 1}道雷劫:{LeiJieList[i]}", Array.Empty<GUILayoutOption>());
		}
		GUILayout.EndVertical();
	}

	public void InitTianJieData()
	{
		EffectManager.Init();
		LeiJieIndex = 0;
		LeiJieCount = TianJieLeiJieShangHai.DataList.Count;
		for (int i = 0; i < LeiJieCount; i++)
		{
			LeiJieList.Add(RollLeiJie(i));
		}
		EffectManager.SetLeiJieBG(LeiJieList[LeiJieIndex]);
		EffectManager.SetLeiJieSprite(LeiJieList[LeiJieIndex]);
	}

	public string RollLeiJie(int leiJieIndex)
	{
		List<QuanZhongItem> list = new List<QuanZhongItem>();
		foreach (TianJieLeiJieType data in TianJieLeiJieType.DataList)
		{
			int num = 0;
			if (data.id == "心魔劫")
			{
				if (PlayerEx.Player.xinjin < 1000)
				{
					num = data.QuanZhongTiSheng[leiJieIndex];
				}
			}
			else if (data.id == "灭世劫")
			{
				int ningZhouShengWang = PlayerEx.GetNingZhouShengWang();
				int seaShengWang = PlayerEx.GetSeaShengWang();
				if (ningZhouShengWang <= -1000 || seaShengWang <= -1000)
				{
					num = data.QuanZhongTiSheng[leiJieIndex];
				}
			}
			int weight = data.QuanZhong[leiJieIndex] + num;
			QuanZhongItem item = new QuanZhongItem(data, weight);
			list.Add(item);
		}
		return (QuanZhongItem.Roll(list, log: true).Obj as TianJieLeiJieType).id;
	}

	public void DuJieSuccess(bool win)
	{
		if (win)
		{
			Debug.Log((object)"以战斗胜利方式渡劫成功");
		}
		else
		{
			Debug.Log((object)"以抗住9道雷劫方式渡劫成功");
		}
	}
}
