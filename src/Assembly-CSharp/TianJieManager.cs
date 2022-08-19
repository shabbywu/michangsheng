using System;
using System.Collections.Generic;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;
using YSGame;

// Token: 0x02000487 RID: 1159
public class TianJieManager : MonoBehaviour
{
	// Token: 0x0600246B RID: 9323 RVA: 0x000FB8FC File Offset: 0x000F9AFC
	public static void StartTianJieCD()
	{
		Avatar player = PlayerEx.Player;
		player.TianJie.SetField("HuaShenTime", player.worldTimeMag.nowTime);
		player.TianJie.SetField("JiaSuYear", 0);
		TianJieManager.OnAddTime();
		player.TianJie.SetField("ShowTianJieCD", true);
	}

	// Token: 0x0600246C RID: 9324 RVA: 0x000FB954 File Offset: 0x000F9B54
	public static void TianJieJiaSu(int year)
	{
		int i = PlayerEx.Player.TianJie["JiaSuYear"].I;
		PlayerEx.Player.TianJie.SetField("JiaSuYear", i + year);
		TianJieManager.OnAddTime();
	}

	// Token: 0x0600246D RID: 9325 RVA: 0x000FB998 File Offset: 0x000F9B98
	public static void OnAddTime()
	{
		Avatar player = PlayerEx.Player;
		if (player != null)
		{
			DateTime nowTime = player.worldTimeMag.getNowTime();
			DateTime t = DateTime.Parse(player.TianJie["HuaShenTime"].str).AddYears(1000 - player.TianJie["JiaSuYear"].I);
			if (nowTime >= t)
			{
				player.TianJie.SetField("ShengYuTime", "0年0月0日");
				player.TianJie.SetField("ShowTianJieCD", false);
				Debug.Log("开始天劫");
				ESCCloseManager.Inst.CloseAll();
				UCheckBox.Show("后续内容正在开发中，尚未开放\n确定后将返回主界面", delegate
				{
					YSSaveGame.Reset();
					KBEngineApp.app.entities[10] = null;
					KBEngineApp.app.entities.Remove(10);
					Tools.instance.loadOtherScenes("MainMenu");
				});
				return;
			}
			int num = (int)(t.Subtract(nowTime).TotalDays / 365.0);
			player.TianJie.SetField("ShengYuTimeValue", num);
			if (num > 0)
			{
				player.TianJie.SetField("ShengYuTime", string.Format("{0}年", num));
				return;
			}
			player.TianJie.SetField("ShengYuTime", "不足1年");
		}
	}

	// Token: 0x17000290 RID: 656
	// (get) Token: 0x0600246E RID: 9326 RVA: 0x000FBAD4 File Offset: 0x000F9CD4
	[HideInInspector]
	public string NowLeiJie
	{
		get
		{
			return this.LeiJieList[this.LeiJieIndex];
		}
	}

	// Token: 0x17000291 RID: 657
	// (get) Token: 0x0600246F RID: 9327 RVA: 0x000FBAE7 File Offset: 0x000F9CE7
	[HideInInspector]
	public int XuLiCount
	{
		get
		{
			return PlayerEx.Player.OtherAvatar.buffmag.GetBuffSum(3150) + 1;
		}
	}

	// Token: 0x06002470 RID: 9328 RVA: 0x000FBB04 File Offset: 0x000F9D04
	private void Awake()
	{
		TianJieManager.Inst = this;
	}

	// Token: 0x06002471 RID: 9329 RVA: 0x000FBB0C File Offset: 0x000F9D0C
	private void OnDestroy()
	{
		TianJieManager.Inst = null;
	}

	// Token: 0x06002472 RID: 9330 RVA: 0x000FBB14 File Offset: 0x000F9D14
	private void OnGUI()
	{
		GUILayout.BeginVertical(GUI.skin.box, Array.Empty<GUILayoutOption>());
		GUILayout.Label(string.Format("雷劫血量:{0}/{1}", PlayerEx.Player.OtherAvatar.HP, PlayerEx.Player.OtherAvatar.HP_Max), Array.Empty<GUILayoutOption>());
		GUILayout.Label(string.Format("当前为第{0}回合", RoundManager.instance.StaticRoundNum), Array.Empty<GUILayoutOption>());
		for (int i = 0; i < this.LeiJieList.Count; i++)
		{
			GUILayout.Label(string.Format("第{0}道雷劫:{1}", i + 1, this.LeiJieList[i]), Array.Empty<GUILayoutOption>());
		}
		GUILayout.EndVertical();
	}

	// Token: 0x06002473 RID: 9331 RVA: 0x000FBBD8 File Offset: 0x000F9DD8
	public void InitTianJieData()
	{
		this.EffectManager.Init();
		this.LeiJieIndex = 0;
		this.LeiJieCount = TianJieLeiJieShangHai.DataList.Count;
		for (int i = 0; i < this.LeiJieCount; i++)
		{
			this.LeiJieList.Add(this.RollLeiJie(i));
		}
		this.EffectManager.SetLeiJieBG(this.LeiJieList[this.LeiJieIndex]);
		this.EffectManager.SetLeiJieSprite(this.LeiJieList[this.LeiJieIndex]);
	}

	// Token: 0x06002474 RID: 9332 RVA: 0x000FBC64 File Offset: 0x000F9E64
	public string RollLeiJie(int leiJieIndex)
	{
		List<QuanZhongItem> list = new List<QuanZhongItem>();
		foreach (TianJieLeiJieType tianJieLeiJieType in TianJieLeiJieType.DataList)
		{
			int num = 0;
			if (tianJieLeiJieType.id == "心魔劫")
			{
				if (PlayerEx.Player.xinjin < 1000)
				{
					num = tianJieLeiJieType.QuanZhongTiSheng[leiJieIndex];
				}
			}
			else if (tianJieLeiJieType.id == "灭世劫")
			{
				int ningZhouShengWang = PlayerEx.GetNingZhouShengWang();
				int seaShengWang = PlayerEx.GetSeaShengWang();
				if (ningZhouShengWang <= -1000 || seaShengWang <= -1000)
				{
					num = tianJieLeiJieType.QuanZhongTiSheng[leiJieIndex];
				}
			}
			int weight = tianJieLeiJieType.QuanZhong[leiJieIndex] + num;
			QuanZhongItem item = new QuanZhongItem(tianJieLeiJieType, weight);
			list.Add(item);
		}
		return (QuanZhongItem.Roll(list, true).Obj as TianJieLeiJieType).id;
	}

	// Token: 0x06002475 RID: 9333 RVA: 0x000FBD64 File Offset: 0x000F9F64
	public void DuJieSuccess(bool win)
	{
		if (win)
		{
			Debug.Log("以战斗胜利方式渡劫成功");
			return;
		}
		Debug.Log("以抗住9道雷劫方式渡劫成功");
	}

	// Token: 0x04001D19 RID: 7449
	public static Dictionary<string, string> LeiJieNames = new Dictionary<string, string>
	{
		{
			"天雷劫",
			"tianlei"
		},
		{
			"阴阳劫",
			"yinyang"
		},
		{
			"风火劫",
			"fenghuo"
		},
		{
			"心魔劫",
			"xinmo"
		},
		{
			"罡雷劫",
			"ganglei"
		},
		{
			"造化劫",
			"zaohua"
		},
		{
			"混元劫",
			"hunyuan"
		},
		{
			"五行劫",
			"wuxing"
		},
		{
			"乾天劫",
			"qiantian"
		},
		{
			"生死劫",
			"shengsi"
		},
		{
			"天地劫",
			"tiandi"
		},
		{
			"灭世劫",
			"mieshi"
		}
	};

	// Token: 0x04001D1A RID: 7450
	public static TianJieManager Inst;

	// Token: 0x04001D1B RID: 7451
	public TianJieEffectManager EffectManager;

	// Token: 0x04001D1C RID: 7452
	public int LeiJieIndex;

	// Token: 0x04001D1D RID: 7453
	public Text LeiJieNameText;

	// Token: 0x04001D1E RID: 7454
	public Text LeiJieDamageText;

	// Token: 0x04001D1F RID: 7455
	[HideInInspector]
	public bool YiXuLi;

	// Token: 0x04001D20 RID: 7456
	[HideInInspector]
	public int LeiJieCount;

	// Token: 0x04001D21 RID: 7457
	[HideInInspector]
	public List<string> LeiJieList = new List<string>();
}
