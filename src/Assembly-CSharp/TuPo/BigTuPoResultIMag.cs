using System.Collections.Generic;
using DG.Tweening;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace TuPo;

public class BigTuPoResultIMag : MonoBehaviour
{
	public static BigTuPoResultIMag Inst;

	[SerializeField]
	private Transform SuccessPanel;

	[SerializeField]
	private FpBtn OkBtn;

	[SerializeField]
	private Text Desc;

	[SerializeField]
	private GameObject ZhuJiPanel;

	[SerializeField]
	private Text ZhuJiDesc;

	[SerializeField]
	private GameObject JinDanPanel;

	[SerializeField]
	private Text JinDanDesc;

	[SerializeField]
	private List<string> JinDanColorList;

	[SerializeField]
	private GameObject YuanYingPanel;

	[SerializeField]
	private Text YuanYingDesc1;

	[SerializeField]
	private Text YuanYingDesc2;

	[SerializeField]
	private Transform FailPanel;

	[SerializeField]
	private Text FailDesc;

	[SerializeField]
	private GameObject ZhuJiFailPanel;

	[SerializeField]
	private GameObject JinDanFailPanel;

	[SerializeField]
	private GameObject JieYingFailPanel;

	[SerializeField]
	private Image JieYingFailImage;

	[SerializeField]
	private List<Sprite> JieYingFailSpriteList;

	[SerializeField]
	private Text JieYingFailTips1;

	[SerializeField]
	private Text JieYingFailTips2;

	[SerializeField]
	private Text JieYingFailDesc1;

	[SerializeField]
	private Text JieYingFailDesc2;

	[SerializeField]
	private GameObject HuaShenPanel;

	[SerializeField]
	private GameObject HuaShenFailPanel;

	[SerializeField]
	private Text HuaShenDesc1;

	[SerializeField]
	private Text HuaShenDesc2;

	private void Awake()
	{
		Inst = this;
	}

	private void OnDestroy()
	{
		Inst = null;
	}

	public void ShowSuccess(int type)
	{
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Expected O, but got Unknown
		Avatar avatar = Tools.instance.getPlayer();
		((UnityEventBase)OkBtn.mouseUpEvent).RemoveAllListeners();
		OkBtn.mouseUpEvent.AddListener((UnityAction)delegate
		{
			avatar.exp = 0uL;
			Tools.instance.getPlayer().levelUp();
			Close();
		});
		switch (type)
		{
		case 1:
			Desc.text = "\u3000\u3000体内翻涌的灵气逐渐平息，原本呈气体状态下的灵气逐渐凝实液化，剩余的灵力则被肉体与经脉吸收。你只觉得全身舒畅，一种从未有过的力量感开始凝聚起来。";
			ZhuJiDesc.text = $"额外提升生命值+{ZhuJiManager.inst.AddHp}";
			ZhuJiPanel.SetActive(true);
			break;
		case 2:
		{
			JieDanBiao jieDanBiao = JieDanBiao.DataDict[JieDanManager.instence.getJinDanID()];
			string jinDanColor = GetJinDanColor(jieDanBiao.JinDanQuality);
			Desc.text = "\u3000\u3000周边天地的灵气突然开始涌入你的体内，你感到体内的真元犹如沸腾的开水一般迅速流动起来，体内的灵气在你的不断压缩下，逐渐凝聚为实体，结成：<color=#" + jinDanColor + ">“" + JieDanManager.instence.getJinDanPingZhi().ToCNNumber() + "品" + jieDanBiao.name + "”</color>";
			JinDanDesc.text = $"气血+{avatar.GetLeveUpAddHPMax(jieDanBiao.HP)}、修炼速度+{jieDanBiao.EXP}%";
			int num8 = 0;
			foreach (int item in jieDanBiao.LinGengType)
			{
				Text jinDanDesc = JinDanDesc;
				jinDanDesc.text += string.Format("、{0}灵根权重+{1}", Tools.getStr("xibieFight" + item), jieDanBiao.LinGengZongShu[num8]);
				num8++;
			}
			if (jieDanBiao.desc != "")
			{
				Text jinDanDesc2 = JinDanDesc;
				jinDanDesc2.text = jinDanDesc2.text + "\n" + jieDanBiao.desc;
			}
			JinDanPanel.SetActive(true);
			break;
		}
		case 3:
			Desc.text = "\u3000\u3000你在心魔的幻象中不知迷失了多久，仿若经历过数世的大喜大悲后，才猛然醒悟过来，摆脱了心魔的迷惑，将元婴凝结成型。刚刚凝结而成的元婴脱离丹室，飞腾而出，显于头颅之上。你只觉得心境祥和，仿佛重新回到了无忧无虑年幼时期。";
			YuanYingDesc1.text = "碎丹化婴：金丹提供的血量上限与修炼速度加成翻倍";
			YuanYingDesc2.text = "第二元神：元婴能够单独修炼一门功法，并根据功法属性解锁独有特性。";
			YuanYingPanel.SetActive(true);
			break;
		case 4:
		{
			Desc.text = "\u3000\u3000你的神魂自天灵脱壳而出，融于周身天地之间。你开始清晰的感知到周天大道法则的力量，仿佛举手投足间便可调动这天地的力量为己所用。但下一秒，另一个无比强大的意志将你的神魂重新压制回肉身之中。你顿时不寒而栗，随即便意识到，这是天道...";
			float num = 0.9f;
			float num2 = 0.1f;
			int buffSum = avatar.buffmag.GetBuffSum(3134);
			int buffSum2 = avatar.buffmag.GetBuffSum(3135);
			int num3 = (int)(2000f * (1f - Mathf.Pow(num, num2 * (float)buffSum)) / (1f - num));
			int num4 = (int)(5f * (1f - Mathf.Pow(num, num2 * (float)buffSum)) / (1f - num));
			int num5 = (int)(10f * (1f - Mathf.Pow(num, num2 * (float)buffSum2)) / (1f - num));
			int num6 = (int)(5f * (1f - Mathf.Pow(num, num2 * (float)buffSum2)) / (1f - num));
			int num7 = (int)(5f * (1f - Mathf.Pow(num, num2 * (float)buffSum2)) / (1f - num));
			avatar.HP_Max += num3;
			avatar.dunSu += num4;
			avatar.shengShi += num5;
			avatar.ZiZhi += num6;
			avatar.wuXin += (uint)num7;
			HuaShenDesc1.text = $"淬体：生命上限+{num3} 遁速+{num4}";
			HuaShenDesc2.text = $"塑魂：神识+{num5} 资质+{num6} 悟性+{num7}";
			HuaShenPanel.SetActive(true);
			break;
		}
		}
		SceneEx.CloseYSFight();
		((Component)this).gameObject.SetActive(true);
		InitSuccess();
	}

	public void ShowFail(int type, int jieYingFail = 0)
	{
		Avatar player = Tools.instance.getPlayer();
		switch (type)
		{
		case 1:
			FailDesc.text = "\u3000\u3000体内翻涌的灵气逐渐平息，你最终功亏一篑，未能成功突破。";
			ZhuJiFailPanel.SetActive(true);
			break;
		case 2:
			FailDesc.text = "\u3000\u3000体内沸腾的灵气逐渐平息下来，经过整整数个时辰的努力后，你仍然没有能够成功结丹...";
			JinDanFailPanel.SetActive(true);
			break;
		case 3:
			switch (jieYingFail)
			{
			case 1:
				FailDesc.text = "\u3000\u3000体内翻涌的灵气不断冲击你的周身穴位，一阵阵的剧痛传来，你全身的经脉被慢慢撕裂，你再也支撑不住，眼前一黑，昏死了过去。";
				JieYingFailImage.sprite = JieYingFailSpriteList[0];
				JieYingFailTips1.text = "【修为大退】";
				JieYingFailTips2.text = "【经脉淬炼】";
				JieYingFailDesc1.text = "【修为】-1500000";
				JieYingFailDesc2.text = "【经脉】上限+5";
				JieYingFailPanel.SetActive(true);
				break;
			case 2:
				FailDesc.text = "\u3000\u3000在心魔幻象之中，你内心的种种恐惧与渴望在你眼前不断浮现，即便以你如今金丹巅峰修为，也沉沦与幻象之中无法自拔。就当你将要永远地迷失于心魔幻象中时，丹田之中突然冲出一股力量，将你硬生生地从幻象之中拉扯出来。你只觉得眼前一黑，昏死了过去。";
				JieYingFailImage.sprite = JieYingFailSpriteList[1];
				JieYingFailTips1.text = "【修为大退】";
				JieYingFailTips2.text = "【黄泉再生】";
				JieYingFailDesc1.text = "【修为】-1500000";
				JieYingFailDesc2.text = "【意志】上限+5";
				JieYingFailPanel.SetActive(true);
				break;
			}
			break;
		case 4:
			FailDesc.text = "\u3000\u3000体内翻滚的灵气不断冲击你的周身经脉，一阵阵的剧痛袭来，你再也支撑不住，眼前一黑，昏死了过去。";
			if (player.exp >= 10000000)
			{
				Debug.Log((object)"化神失败扣除10000000经验，下次化神初始仙性+1");
				player.addEXP(-10000000);
				PlayerEx.AddHuaShenStartXianXing(1);
			}
			else
			{
				player.exp = 0uL;
			}
			HuaShenFailPanel.SetActive(true);
			break;
		}
		SceneEx.CloseYSFight();
		((Component)this).gameObject.SetActive(true);
		InitFail();
	}

	private void InitSuccess()
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		((Component)SuccessPanel).gameObject.SetActive(true);
		Init();
		SuccessPanel.localScale = new Vector3(0.3f, 0.3f, 0.3f);
		ShortcutExtensions.DOScale(SuccessPanel, Vector3.one, 0.5f);
	}

	private void InitFail()
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		((Component)FailPanel).gameObject.SetActive(true);
		Init();
		FailPanel.localScale = new Vector3(0.3f, 0.3f, 0.3f);
		ShortcutExtensions.DOScale(FailPanel, Vector3.one, 0.5f);
	}

	private void Init()
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		((Component)this).transform.SetParent(((Component)NewUICanvas.Inst).gameObject.transform);
		((Component)this).transform.localPosition = Vector3.zero;
		((Component)this).transform.localScale = Vector3.one;
		((Component)this).transform.SetAsLastSibling();
		PanelMamager.CanOpenOrClose = false;
		Tools.canClickFlag = false;
	}

	private string GetJinDanColor(int lv)
	{
		int index = 0;
		if (lv <= 3)
		{
			index = 0;
		}
		else if (lv <= 6)
		{
			index = 1;
		}
		else if (lv <= 9)
		{
			index = 2;
		}
		return JinDanColorList[index];
	}

	public void Close()
	{
		PanelMamager.CanOpenOrClose = true;
		Tools.canClickFlag = true;
		Tools.instance.loadMapScenes(Tools.instance.FinalScene);
		Object.Destroy((Object)(object)((Component)this).gameObject);
	}
}
