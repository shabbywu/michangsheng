using System;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using JSONClass;
using script.ExchangeMeeting.Logic.Interface;
using script.ExchangeMeeting.UI.Base;
using script.ExchangeMeeting.UI.Ctr;
using script.ExchangeMeeting.UI.Interface;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace script.ExchangeMeeting.UI
{
	// Token: 0x02000A20 RID: 2592
	public class ExchangeUIMag : IExchangeUIMag
	{
		// Token: 0x06004789 RID: 18313 RVA: 0x001E3F44 File Offset: 0x001E2144
		public ExchangeUIMag(GameObject gameObject)
		{
			this._go = gameObject;
		}

		// Token: 0x0600478A RID: 18314 RVA: 0x001E3F60 File Offset: 0x001E2160
		public void Init()
		{
			if (IExchangeMag.Inst.ExchangeIO.NeedUpdateNpcExchange())
			{
				IExchangeMag.Inst.ExchangeIO.CreateNpcExchange();
			}
			this.PublishCtr = new PublishCtr(base.Get("发布界面", true));
			this.ExchangeCtr = new ExchangeCtr(base.Get("交易浏览界面", true));
			this.sayContent = base.Get<Text>("Say/Value");
			base.Get<FpBtn>("Close").mouseUpEvent.AddListener(new UnityAction(base.Close));
			this.NeedBag = base.Get<ExchangeBag>("需求背包");
			this.PlayerBag = base.Get<ExchangeBag>("玩家背包");
			this.SubmitBag = base.Get<ExchangeBagB>("提交背包");
			this.OpenExchange();
		}

		// Token: 0x0600478B RID: 18315 RVA: 0x001E4028 File Offset: 0x001E2228
		public override void Say(int id)
		{
			TweenerCore<float, float, FloatOptions> tweenerCore = this.tween;
			if (tweenerCore != null)
			{
				TweenExtensions.Kill(tweenerCore, false);
			}
			if (!TipsExchangeData.DataDict.ContainsKey(id))
			{
				Debug.LogError("TipsExchangeData不存在Id:" + id);
				return;
			}
			float timer = 0f;
			this.sayContent.SetText(TipsExchangeData.DataDict[id].TiShi);
			base.Get("Say", true).SetActive(true);
			this.tween = TweenSettingsExtensions.OnComplete<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => timer, delegate(float x)
			{
				timer = x;
			}, 1f, this.time), new TweenCallback(this.CloseSay));
		}

		// Token: 0x0600478C RID: 18316 RVA: 0x001E40E7 File Offset: 0x001E22E7
		private void CloseSay()
		{
			if (this._go == null)
			{
				return;
			}
			base.Get("Say", true).SetActive(false);
		}

		// Token: 0x0600478D RID: 18317 RVA: 0x001E410A File Offset: 0x001E230A
		public override void OpenPublish()
		{
			this.PublishCtr.UI.Show();
			this.ExchangeCtr.UI.Hide();
		}

		// Token: 0x0600478E RID: 18318 RVA: 0x001E412C File Offset: 0x001E232C
		public override void OpenExchange()
		{
			this.PublishCtr.UI.Hide();
			this.ExchangeCtr.UI.Show();
		}

		// Token: 0x0600478F RID: 18319 RVA: 0x001E4150 File Offset: 0x001E2350
		public override List<int> GetCanGetItemList()
		{
			if (this._list == null)
			{
				try
				{
					this._list = new List<int>();
					Dictionary<int, ItemTypeExchangeData> dataDict = ItemTypeExchangeData.DataDict;
					foreach (_ItemJsonData itemJsonData in _ItemJsonData.DataList)
					{
						if (itemJsonData.id >= jsonData.QingJiaoItemIDSegment)
						{
							break;
						}
						if (dataDict.ContainsKey(itemJsonData.type) && dataDict[itemJsonData.type].quality.Contains(itemJsonData.quality))
						{
							this._list.Add(itemJsonData.id);
						}
					}
					foreach (DisableExchangeData disableExchangeData in DisableExchangeData.DataList)
					{
						this._list.Remove(disableExchangeData.id);
					}
				}
				catch (Exception ex)
				{
					Debug.LogError(ex);
					Debug.LogError("GetCanGetItemList初始化失败");
				}
			}
			return this._list;
		}

		// Token: 0x0400488C RID: 18572
		private TweenerCore<float, float, FloatOptions> tween;

		// Token: 0x0400488D RID: 18573
		private List<int> _list;

		// Token: 0x0400488E RID: 18574
		private float time = 3f;
	}
}
