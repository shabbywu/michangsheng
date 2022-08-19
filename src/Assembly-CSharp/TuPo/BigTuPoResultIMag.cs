using System;
using System.Collections.Generic;
using DG.Tweening;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

namespace TuPo
{
	// Token: 0x0200072B RID: 1835
	public class BigTuPoResultIMag : MonoBehaviour
	{
		// Token: 0x06003A76 RID: 14966 RVA: 0x0019186C File Offset: 0x0018FA6C
		private void Awake()
		{
			BigTuPoResultIMag.Inst = this;
		}

		// Token: 0x06003A77 RID: 14967 RVA: 0x00191874 File Offset: 0x0018FA74
		private void OnDestroy()
		{
			BigTuPoResultIMag.Inst = null;
		}

		// Token: 0x06003A78 RID: 14968 RVA: 0x0019187C File Offset: 0x0018FA7C
		public void ShowSuccess(int type)
		{
			Avatar avatar = Tools.instance.getPlayer();
			this.OkBtn.mouseUpEvent.RemoveAllListeners();
			this.OkBtn.mouseUpEvent.AddListener(delegate()
			{
				avatar.exp = 0UL;
				Tools.instance.getPlayer().levelUp();
				this.Close();
			});
			if (type == 1)
			{
				this.Desc.text = "\u3000\u3000体内翻涌的灵气逐渐平息，原本呈气体状态下的灵气逐渐凝实液化，剩余的灵力则被肉体与经脉吸收。你只觉得全身舒畅，一种从未有过的力量感开始凝聚起来。";
				this.ZhuJiDesc.text = string.Format("额外提升生命值+{0}", ZhuJiManager.inst.AddHp);
				this.ZhuJiPanel.SetActive(true);
			}
			else if (type == 2)
			{
				JieDanBiao jieDanBiao = JieDanBiao.DataDict[JieDanManager.instence.getJinDanID()];
				string jinDanColor = this.GetJinDanColor(jieDanBiao.JinDanQuality);
				this.Desc.text = string.Concat(new string[]
				{
					"\u3000\u3000周边天地的灵气突然开始涌入你的体内，你感到体内的真元犹如沸腾的开水一般迅速流动起来，体内的灵气在你的不断压缩下，逐渐凝聚为实体，结成：<color=#",
					jinDanColor,
					">“",
					JieDanManager.instence.getJinDanPingZhi().ToCNNumber(),
					"品",
					jieDanBiao.name,
					"”</color>"
				});
				this.JinDanDesc.text = string.Format("气血+{0}、修炼速度+{1}%", avatar.GetLeveUpAddHPMax(jieDanBiao.HP), jieDanBiao.EXP);
				int num = 0;
				foreach (int num2 in jieDanBiao.LinGengType)
				{
					Text jinDanDesc = this.JinDanDesc;
					jinDanDesc.text += string.Format("、{0}灵根权重+{1}", Tools.getStr("xibieFight" + num2), jieDanBiao.LinGengZongShu[num]);
					num++;
				}
				if (jieDanBiao.desc != "")
				{
					Text jinDanDesc2 = this.JinDanDesc;
					jinDanDesc2.text = jinDanDesc2.text + "\n" + jieDanBiao.desc;
				}
				this.JinDanPanel.SetActive(true);
			}
			else if (type == 3)
			{
				this.Desc.text = "\u3000\u3000你在心魔的幻象中不知迷失了多久，仿若经历过数世的大喜大悲后，才猛然醒悟过来，摆脱了心魔的迷惑，将元婴凝结成型。刚刚凝结而成的元婴脱离丹室，飞腾而出，显于头颅之上。你只觉得心境祥和，仿佛重新回到了无忧无虑年幼时期。";
				this.YuanYingDesc1.text = "碎丹化婴：金丹提供的血量上限与修炼速度加成翻倍";
				this.YuanYingDesc2.text = "第二元神：元婴能够单独修炼一门功法，并根据功法属性解锁独有特性。";
				this.YuanYingPanel.SetActive(true);
			}
			else if (type == 4)
			{
				this.Desc.text = "\u3000\u3000你的神魂自天灵脱壳而出，融于周身天地之间。你开始清晰的感知到周天大道法则的力量，仿佛举手投足间便可调动这天地的力量为己所用。但下一秒，另一个无比强大的意志将你的神魂重新压制回肉身之中。你顿时不寒而栗，随即便意识到，这是天道...";
				float num3 = 0.9f;
				float num4 = 0.1f;
				int buffSum = avatar.buffmag.GetBuffSum(3134);
				int buffSum2 = avatar.buffmag.GetBuffSum(3135);
				int num5 = (int)(2000f * (1f - Mathf.Pow(num3, num4 * (float)buffSum)) / (1f - num3));
				int num6 = (int)(5f * (1f - Mathf.Pow(num3, num4 * (float)buffSum)) / (1f - num3));
				int num7 = (int)(10f * (1f - Mathf.Pow(num3, num4 * (float)buffSum2)) / (1f - num3));
				int num8 = (int)(5f * (1f - Mathf.Pow(num3, num4 * (float)buffSum2)) / (1f - num3));
				int num9 = (int)(5f * (1f - Mathf.Pow(num3, num4 * (float)buffSum2)) / (1f - num3));
				avatar.HP_Max += num5;
				avatar.dunSu += num6;
				avatar.shengShi += num7;
				avatar.ZiZhi += num8;
				avatar.wuXin += (uint)num9;
				this.HuaShenDesc1.text = string.Format("淬体：生命上限+{0} 遁速+{1}", num5, num6);
				this.HuaShenDesc2.text = string.Format("塑魂：神识+{0} 资质+{1} 悟性+{2}", num7, num8, num9);
				this.HuaShenPanel.SetActive(true);
			}
			SceneEx.CloseYSFight();
			base.gameObject.SetActive(true);
			this.InitSuccess();
		}

		// Token: 0x06003A79 RID: 14969 RVA: 0x00191CC8 File Offset: 0x0018FEC8
		public void ShowFail(int type, int jieYingFail = 0)
		{
			Avatar player = Tools.instance.getPlayer();
			if (type == 1)
			{
				this.FailDesc.text = "\u3000\u3000体内翻涌的灵气逐渐平息，你最终功亏一篑，未能成功突破。";
				this.ZhuJiFailPanel.SetActive(true);
			}
			else if (type == 2)
			{
				this.FailDesc.text = "\u3000\u3000体内沸腾的灵气逐渐平息下来，经过整整数个时辰的努力后，你仍然没有能够成功结丹...";
				this.JinDanFailPanel.SetActive(true);
			}
			else if (type == 3)
			{
				if (jieYingFail == 1)
				{
					this.FailDesc.text = "\u3000\u3000体内翻涌的灵气不断冲击你的周身穴位，一阵阵的剧痛传来，你全身的经脉被慢慢撕裂，你再也支撑不住，眼前一黑，昏死了过去。";
					this.JieYingFailImage.sprite = this.JieYingFailSpriteList[0];
					this.JieYingFailTips1.text = "【修为大退】";
					this.JieYingFailTips2.text = "【经脉淬炼】";
					this.JieYingFailDesc1.text = "【修为】-1500000";
					this.JieYingFailDesc2.text = "【经脉】上限+5";
					this.JieYingFailPanel.SetActive(true);
				}
				else if (jieYingFail == 2)
				{
					this.FailDesc.text = "\u3000\u3000在心魔幻象之中，你内心的种种恐惧与渴望在你眼前不断浮现，即便以你如今金丹巅峰修为，也沉沦与幻象之中无法自拔。就当你将要永远地迷失于心魔幻象中时，丹田之中突然冲出一股力量，将你硬生生地从幻象之中拉扯出来。你只觉得眼前一黑，昏死了过去。";
					this.JieYingFailImage.sprite = this.JieYingFailSpriteList[1];
					this.JieYingFailTips1.text = "【修为大退】";
					this.JieYingFailTips2.text = "【黄泉再生】";
					this.JieYingFailDesc1.text = "【修为】-1500000";
					this.JieYingFailDesc2.text = "【意志】上限+5";
					this.JieYingFailPanel.SetActive(true);
				}
			}
			else if (type == 4)
			{
				this.FailDesc.text = "\u3000\u3000体内翻滚的灵气不断冲击你的周身经脉，一阵阵的剧痛袭来，你再也支撑不住，眼前一黑，昏死了过去。";
				if (player.exp >= 10000000UL)
				{
					Debug.Log("化神失败扣除10000000经验，下次化神初始仙性+1");
					player.addEXP(-10000000);
					PlayerEx.AddHuaShenStartXianXing(1);
				}
				else
				{
					player.exp = 0UL;
				}
				this.HuaShenFailPanel.SetActive(true);
			}
			SceneEx.CloseYSFight();
			base.gameObject.SetActive(true);
			this.InitFail();
		}

		// Token: 0x06003A7A RID: 14970 RVA: 0x00191E94 File Offset: 0x00190094
		private void InitSuccess()
		{
			this.SuccessPanel.gameObject.SetActive(true);
			this.Init();
			this.SuccessPanel.localScale = new Vector3(0.3f, 0.3f, 0.3f);
			ShortcutExtensions.DOScale(this.SuccessPanel, Vector3.one, 0.5f);
		}

		// Token: 0x06003A7B RID: 14971 RVA: 0x00191EF0 File Offset: 0x001900F0
		private void InitFail()
		{
			this.FailPanel.gameObject.SetActive(true);
			this.Init();
			this.FailPanel.localScale = new Vector3(0.3f, 0.3f, 0.3f);
			ShortcutExtensions.DOScale(this.FailPanel, Vector3.one, 0.5f);
		}

		// Token: 0x06003A7C RID: 14972 RVA: 0x00191F4C File Offset: 0x0019014C
		private void Init()
		{
			base.transform.SetParent(NewUICanvas.Inst.gameObject.transform);
			base.transform.localPosition = Vector3.zero;
			base.transform.localScale = Vector3.one;
			base.transform.SetAsLastSibling();
			PanelMamager.CanOpenOrClose = false;
			Tools.canClickFlag = false;
		}

		// Token: 0x06003A7D RID: 14973 RVA: 0x00191FAC File Offset: 0x001901AC
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
			return this.JinDanColorList[index];
		}

		// Token: 0x06003A7E RID: 14974 RVA: 0x00191FDE File Offset: 0x001901DE
		public void Close()
		{
			PanelMamager.CanOpenOrClose = true;
			Tools.canClickFlag = true;
			Tools.instance.loadMapScenes(Tools.instance.FinalScene, true);
			Object.Destroy(base.gameObject);
		}

		// Token: 0x0400329F RID: 12959
		public static BigTuPoResultIMag Inst;

		// Token: 0x040032A0 RID: 12960
		[SerializeField]
		private Transform SuccessPanel;

		// Token: 0x040032A1 RID: 12961
		[SerializeField]
		private FpBtn OkBtn;

		// Token: 0x040032A2 RID: 12962
		[SerializeField]
		private Text Desc;

		// Token: 0x040032A3 RID: 12963
		[SerializeField]
		private GameObject ZhuJiPanel;

		// Token: 0x040032A4 RID: 12964
		[SerializeField]
		private Text ZhuJiDesc;

		// Token: 0x040032A5 RID: 12965
		[SerializeField]
		private GameObject JinDanPanel;

		// Token: 0x040032A6 RID: 12966
		[SerializeField]
		private Text JinDanDesc;

		// Token: 0x040032A7 RID: 12967
		[SerializeField]
		private List<string> JinDanColorList;

		// Token: 0x040032A8 RID: 12968
		[SerializeField]
		private GameObject YuanYingPanel;

		// Token: 0x040032A9 RID: 12969
		[SerializeField]
		private Text YuanYingDesc1;

		// Token: 0x040032AA RID: 12970
		[SerializeField]
		private Text YuanYingDesc2;

		// Token: 0x040032AB RID: 12971
		[SerializeField]
		private Transform FailPanel;

		// Token: 0x040032AC RID: 12972
		[SerializeField]
		private Text FailDesc;

		// Token: 0x040032AD RID: 12973
		[SerializeField]
		private GameObject ZhuJiFailPanel;

		// Token: 0x040032AE RID: 12974
		[SerializeField]
		private GameObject JinDanFailPanel;

		// Token: 0x040032AF RID: 12975
		[SerializeField]
		private GameObject JieYingFailPanel;

		// Token: 0x040032B0 RID: 12976
		[SerializeField]
		private Image JieYingFailImage;

		// Token: 0x040032B1 RID: 12977
		[SerializeField]
		private List<Sprite> JieYingFailSpriteList;

		// Token: 0x040032B2 RID: 12978
		[SerializeField]
		private Text JieYingFailTips1;

		// Token: 0x040032B3 RID: 12979
		[SerializeField]
		private Text JieYingFailTips2;

		// Token: 0x040032B4 RID: 12980
		[SerializeField]
		private Text JieYingFailDesc1;

		// Token: 0x040032B5 RID: 12981
		[SerializeField]
		private Text JieYingFailDesc2;

		// Token: 0x040032B6 RID: 12982
		[SerializeField]
		private GameObject HuaShenPanel;

		// Token: 0x040032B7 RID: 12983
		[SerializeField]
		private GameObject HuaShenFailPanel;

		// Token: 0x040032B8 RID: 12984
		[SerializeField]
		private Text HuaShenDesc1;

		// Token: 0x040032B9 RID: 12985
		[SerializeField]
		private Text HuaShenDesc2;
	}
}
