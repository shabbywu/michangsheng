using System;
using System.Collections.Generic;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Tab
{
	// Token: 0x020006FB RID: 1787
	[Serializable]
	public class TabShuXingPanel : ITabPanelBase
	{
		// Token: 0x0600396B RID: 14699 RVA: 0x001888D8 File Offset: 0x00186AD8
		public TabShuXingPanel(GameObject gameObject)
		{
			this.HasHp = true;
			this._go = gameObject;
			this._player = Tools.instance.getPlayer();
		}

		// Token: 0x0600396C RID: 14700 RVA: 0x00188900 File Offset: 0x00186B00
		private void Init()
		{
			this._levelImgDict1 = ResManager.inst.LoadSpriteAtlas("NewTab/LevelImg1");
			this._levelImgDict2 = ResManager.inst.LoadSpriteAtlas("NewTab/LevelImg2");
			this._xinJingImgDict = ResManager.inst.LoadSpriteAtlas("NewTab/XinJingImg");
			this._xinJingColor = new Dictionary<int, Color32>();
			this._xinJingColor.Add(1, new Color32(byte.MaxValue, 142, 100, byte.MaxValue));
			this._xinJingColor.Add(2, new Color32(227, 199, 108, byte.MaxValue));
			this._xinJingColor.Add(3, new Color32(218, 227, 108, byte.MaxValue));
			this._xinJingColor.Add(4, new Color32(197, 224, 91, byte.MaxValue));
			this._xinJingColor.Add(5, new Color32(124, 234, 150, byte.MaxValue));
			this._xinJingColor.Add(6, new Color32(124, 234, 232, byte.MaxValue));
			this._xinJingColor.Add(7, new Color32(159, 201, byte.MaxValue, byte.MaxValue));
			this._danDuImgDict = ResManager.inst.LoadSpriteAtlas("NewTab/DanDuImg");
			this._danDuImgColor = new Dictionary<int, Color32>();
			this._danDuImgColor.Add(1, new Color32(233, 228, 164, byte.MaxValue));
			this._danDuImgColor.Add(2, new Color32(233, 228, 164, byte.MaxValue));
			this._danDuImgColor.Add(3, new Color32(233, 211, 164, byte.MaxValue));
			this._danDuImgColor.Add(4, new Color32(244, 180, 123, byte.MaxValue));
			this._danDuImgColor.Add(5, new Color32(211, 135, 178, byte.MaxValue));
			this._lingGanImgDict = ResManager.inst.LoadSpriteAtlas("NewTab/LingGanImg");
			this._lingGanColor = new Dictionary<int, Color32>();
			this._lingGanColor.Add(4, new Color32(170, 171, 150, byte.MaxValue));
			this._lingGanColor.Add(3, new Color32(233, 228, 164, byte.MaxValue));
			this._lingGanColor.Add(2, new Color32(100, 201, 122, byte.MaxValue));
			this._lingGanColor.Add(1, new Color32(141, 241, 236, byte.MaxValue));
			base.Get<Text>("PlayerName/Name_Text").text = this._player.name;
			this._levelName = base.Get<Text>("Level/Level_Name");
			this._levelImg1 = base.Get<Image>("Level/Level_Name/Level_Img1");
			this._levelImg2 = base.Get<Image>("Level/Level_Img2");
			this._levelBtn = base.Get<FpBtn>("Level/Level_Img2/Listener");
			LevelTips levelTips = new LevelTips(base.Get("Level/LevelTips", true));
			this._levelBtn.mouseEnterEvent.AddListener(new UnityAction(levelTips.Show));
			this._levelBtn.mouseOutEvent.AddListener(new UnityAction(levelTips.Hide));
			this._age = base.Get<Text>("BaseData/年龄/Value");
			this._shouYuan = base.Get<Text>("BaseData/寿元/Value");
			this._ziZi = base.Get<Text>("BaseData/资质/Value");
			this._shenShi = base.Get<Text>("BaseData/神识/Value");
			this._wuXing = base.Get<Text>("BaseData/悟性/Value");
			this._dunSu = base.Get<Text>("BaseData/遁速/Value");
			this._xiuLianSpeed = base.Get<Text>("BaseData/修炼速度/Value");
			this._xinJingImg = base.Get<Image>("BaseData/心境/BG");
			this._xinJingValueName = base.Get<Text>("BaseData/心境/ValueName");
			this._xinJingValue = base.Get<Text>("BaseData/心境/Value");
			this._danduImg = base.Get<Image>("BaseData/丹毒/BG");
			this._danduValueName = base.Get<Text>("BaseData/丹毒/ValueName");
			this._danduValue = base.Get<Text>("BaseData/丹毒/Value");
			this._lingganImg = base.Get<Image>("BaseData/灵感/BG");
			this._lingganValueName = base.Get<Text>("BaseData/灵感/ValueName");
			this._lingganValue = base.Get<Text>("BaseData/灵感/Value");
			BaseDataTips dataTips = new BaseDataTips(base.Get("BaseData/BaseDataTips", true));
			using (List<TianFuDescJsonData>.Enumerator enumerator = TianFuDescJsonData.DataList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					TianFuDescJsonData data = enumerator.Current;
					GameObject gameObject = base.Get("BaseData/" + data.Title, false);
					if (gameObject != null)
					{
						UIListener uilistener = gameObject.AddComponent<UIListener>();
						Vector3 position = gameObject.transform.position;
						uilistener.mouseEnterEvent.AddListener(delegate()
						{
							dataTips.Show(data.Desc, position);
						});
						uilistener.mouseOutEvent.AddListener(new UnityAction(dataTips.Hide));
					}
				}
			}
			this._menPaiValue = base.Get<Text>("BaseData/门派/Value");
			this._menPaiShengWangValue = base.Get<Text>("BaseData/门派声望/Value");
			this._zhiWeiValue = base.Get<Text>("BaseData/职位/Value");
			this._fengLuValue = base.Get<Text>("BaseData/俸禄/Value");
			this._jin = base.Get<Text>("LingGen/金灵根/Value");
			this._mu = base.Get<Text>("LingGen/木灵根/Value");
			this._shui = base.Get<Text>("LingGen/水灵根/Value");
			this._huo = base.Get<Text>("LingGen/火灵根/Value");
			this._tu = base.Get<Text>("LingGen/土灵根/Value");
		}

		// Token: 0x0600396D RID: 14701 RVA: 0x00188F24 File Offset: 0x00187124
		private void UpdateUI()
		{
			this._levelName.text = LevelUpDataJsonData.DataDict[(int)this._player.level].Name.Insert(2, "   ");
			this._levelImg1.sprite = this._levelImgDict1[this._player.level.ToString()];
			if (this._player.level < 13)
			{
				string str = string.Format("{0}-", this._player.level);
				this._levelImg2.sprite = this._levelImgDict2[str + "1"];
				this._levelBtn.nomalSprite = this._levelImgDict2[str + "1"];
				this._levelBtn.mouseEnterSprite = this._levelImgDict2[str + "2"];
			}
			else
			{
				string arg = (this._player.Sex == 1) ? "huashen-nan" : "huashen-nv";
				int num = this._player.HuaShenWuDao.I;
				num = Mathf.Max(num, 1);
				string str2 = string.Format("{0}-{1}-", arg, num);
				this._levelImg2.sprite = this._levelImgDict2[str2 + "1"];
				this._levelBtn.nomalSprite = this._levelImgDict2[str2 + "1"];
				this._levelBtn.mouseEnterSprite = this._levelImgDict2[str2 + "2"];
			}
			this._age.text = this._player.age.ToString();
			this._shouYuan.text = this._player.shouYuan.ToString();
			this._ziZi.text = this._player.ZiZhi.ToString();
			this._shenShi.text = this._player.shengShi.ToString();
			this._wuXing.text = this._player.wuXin.ToString();
			this._dunSu.text = this._player.dunSu.ToString();
			this._xiuLianSpeed.text = string.Format("{0}/月", (int)this._player.getTimeExpSpeed());
			int xinJingLevel = this._player.GetXinJingLevel();
			if (XinJinJsonData.DataDict[xinJingLevel].Max > 0)
			{
				if (xinJingLevel == 7)
				{
					this._xinJingValue.text = string.Format("{0}/-", this._player.xinjin);
				}
				else
				{
					this._xinJingValue.text = string.Format("{0}/{1}", this._player.xinjin, XinJinJsonData.DataDict[xinJingLevel].Max);
				}
			}
			else
			{
				this._xinJingValue.text = string.Format("{0}/0", this._player.xinjin);
			}
			this._xinJingValueName.text = XinJinJsonData.DataDict[xinJingLevel].Text;
			this._xinJingValueName.color = this._xinJingColor[xinJingLevel];
			this._xinJingImg.sprite = this._xinJingImgDict[xinJingLevel.ToString()];
			int danDuLevel = this._player.GetDanDuLevel();
			this._danduValue.text = string.Format("{0}/120", this._player.Dandu);
			this._danduImg.sprite = this._danDuImgDict[(danDuLevel + 1).ToString()];
			this._danduValueName.color = this._danDuImgColor[danDuLevel + 1];
			this._danduValueName.text = DanduMiaoShu.DataDict[danDuLevel + 1].name;
			this._lingganValueName.text = LunDaoStateData.DataDict[this._player.LunDaoState].ZhuangTaiInfo;
			this._lingganValueName.color = this._lingGanColor[this._player.LunDaoState];
			this._lingganValue.text = string.Format("{0}/{1}", this._player.LingGan, this._player.GetLingGanMax());
			this._lingganImg.sprite = this._lingGanImgDict[this._player.LunDaoState.ToString()];
			this._menPaiValue.text = Tools.getStr("menpai" + this._player.menPai);
			this._menPaiShengWangValue.text = PlayerEx.GetMenPaiShengWang().ToString();
			this._zhiWeiValue.text = PlayerEx.GetMenPaiChengHao();
			this._fengLuValue.text = this._player.chenghaomag.GetOneYearAddMoney() + "灵石/年";
			this._jin.text = this._player.GetLingGeng[0].ToString();
			this._mu.text = this._player.GetLingGeng[1].ToString();
			this._shui.text = this._player.GetLingGeng[2].ToString();
			this._huo.text = this._player.GetLingGeng[3].ToString();
			this._tu.text = this._player.GetLingGeng[4].ToString();
		}

		// Token: 0x0600396E RID: 14702 RVA: 0x0018950E File Offset: 0x0018770E
		public override void Show()
		{
			if (!this._isInit)
			{
				this.Init();
				this._isInit = true;
			}
			this._go.SetActive(true);
			this.UpdateUI();
		}

		// Token: 0x04003169 RID: 12649
		private Dictionary<string, Sprite> _levelImgDict1;

		// Token: 0x0400316A RID: 12650
		private Dictionary<string, Sprite> _levelImgDict2;

		// Token: 0x0400316B RID: 12651
		private Dictionary<string, Sprite> _xinJingImgDict;

		// Token: 0x0400316C RID: 12652
		private Dictionary<int, Color32> _xinJingColor;

		// Token: 0x0400316D RID: 12653
		private Dictionary<string, Sprite> _danDuImgDict;

		// Token: 0x0400316E RID: 12654
		private Dictionary<int, Color32> _danDuImgColor;

		// Token: 0x0400316F RID: 12655
		private Dictionary<string, Sprite> _lingGanImgDict;

		// Token: 0x04003170 RID: 12656
		private Dictionary<int, Color32> _lingGanColor;

		// Token: 0x04003171 RID: 12657
		private Avatar _player;

		// Token: 0x04003172 RID: 12658
		private bool _isInit;

		// Token: 0x04003173 RID: 12659
		private Text _levelName;

		// Token: 0x04003174 RID: 12660
		private Image _levelImg1;

		// Token: 0x04003175 RID: 12661
		private Image _levelImg2;

		// Token: 0x04003176 RID: 12662
		private FpBtn _levelBtn;

		// Token: 0x04003177 RID: 12663
		private Text _age;

		// Token: 0x04003178 RID: 12664
		private Text _shouYuan;

		// Token: 0x04003179 RID: 12665
		private Text _ziZi;

		// Token: 0x0400317A RID: 12666
		private Text _shenShi;

		// Token: 0x0400317B RID: 12667
		private Text _wuXing;

		// Token: 0x0400317C RID: 12668
		private Text _dunSu;

		// Token: 0x0400317D RID: 12669
		private Text _xiuLianSpeed;

		// Token: 0x0400317E RID: 12670
		private Text _xinJingValueName;

		// Token: 0x0400317F RID: 12671
		private Text _xinJingValue;

		// Token: 0x04003180 RID: 12672
		private Image _xinJingImg;

		// Token: 0x04003181 RID: 12673
		private Text _danduValueName;

		// Token: 0x04003182 RID: 12674
		private Text _danduValue;

		// Token: 0x04003183 RID: 12675
		private Image _danduImg;

		// Token: 0x04003184 RID: 12676
		private Text _lingganValueName;

		// Token: 0x04003185 RID: 12677
		private Text _lingganValue;

		// Token: 0x04003186 RID: 12678
		private Image _lingganImg;

		// Token: 0x04003187 RID: 12679
		private Text _menPaiValue;

		// Token: 0x04003188 RID: 12680
		private Text _menPaiShengWangValue;

		// Token: 0x04003189 RID: 12681
		private Text _zhiWeiValue;

		// Token: 0x0400318A RID: 12682
		private Text _fengLuValue;

		// Token: 0x0400318B RID: 12683
		private Text _jin;

		// Token: 0x0400318C RID: 12684
		private Text _mu;

		// Token: 0x0400318D RID: 12685
		private Text _shui;

		// Token: 0x0400318E RID: 12686
		private Text _huo;

		// Token: 0x0400318F RID: 12687
		private Text _tu;
	}
}
