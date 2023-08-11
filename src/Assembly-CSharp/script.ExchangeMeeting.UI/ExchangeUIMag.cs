using System;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using JSONClass;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using script.ExchangeMeeting.Logic.Interface;
using script.ExchangeMeeting.UI.Base;
using script.ExchangeMeeting.UI.Ctr;
using script.ExchangeMeeting.UI.Interface;

namespace script.ExchangeMeeting.UI;

public class ExchangeUIMag : IExchangeUIMag
{
	private TweenerCore<float, float, FloatOptions> tween;

	private List<int> _list;

	private float time = 3f;

	public ExchangeUIMag(GameObject gameObject)
	{
		_go = gameObject;
	}

	public void Init()
	{
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Expected O, but got Unknown
		if (IExchangeMag.Inst.ExchangeIO.NeedUpdateNpcExchange())
		{
			IExchangeMag.Inst.ExchangeIO.CreateNpcExchange();
		}
		PublishCtr = new PublishCtr(Get("发布界面"));
		ExchangeCtr = new ExchangeCtr(Get("交易浏览界面"));
		sayContent = Get<Text>("Say/Value");
		Get<FpBtn>("Close").mouseUpEvent.AddListener(new UnityAction(base.Close));
		NeedBag = Get<ExchangeBag>("需求背包");
		PlayerBag = Get<ExchangeBag>("玩家背包");
		SubmitBag = Get<ExchangeBagB>("提交背包");
		OpenExchange();
	}

	public override void Say(int id)
	{
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Expected O, but got Unknown
		TweenerCore<float, float, FloatOptions> obj = tween;
		if (obj != null)
		{
			TweenExtensions.Kill((Tween)(object)obj, false);
		}
		if (!TipsExchangeData.DataDict.ContainsKey(id))
		{
			Debug.LogError((object)("TipsExchangeData不存在Id:" + id));
			return;
		}
		float timer = 0f;
		sayContent.SetText(TipsExchangeData.DataDict[id].TiShi);
		Get("Say").SetActive(true);
		tween = TweenSettingsExtensions.OnComplete<TweenerCore<float, float, FloatOptions>>(DOTween.To((DOGetter<float>)(() => timer), (DOSetter<float>)delegate(float x)
		{
			timer = x;
		}, 1f, time), new TweenCallback(CloseSay));
	}

	private void CloseSay()
	{
		if (!((Object)(object)_go == (Object)null))
		{
			Get("Say").SetActive(false);
		}
	}

	public override void OpenPublish()
	{
		PublishCtr.UI.Show();
		ExchangeCtr.UI.Hide();
	}

	public override void OpenExchange()
	{
		PublishCtr.UI.Hide();
		ExchangeCtr.UI.Show();
	}

	public override List<int> GetCanGetItemList()
	{
		if (_list == null)
		{
			try
			{
				_list = new List<int>();
				Dictionary<int, ItemTypeExchangeData> dataDict = ItemTypeExchangeData.DataDict;
				foreach (_ItemJsonData data in _ItemJsonData.DataList)
				{
					if (data.id < jsonData.QingJiaoItemIDSegment)
					{
						if (dataDict.ContainsKey(data.type) && dataDict[data.type].quality.Contains(data.quality))
						{
							_list.Add(data.id);
						}
						continue;
					}
					break;
				}
				foreach (DisableExchangeData data2 in DisableExchangeData.DataList)
				{
					_list.Remove(data2.id);
				}
			}
			catch (Exception ex)
			{
				Debug.LogError((object)ex);
				Debug.LogError((object)"GetCanGetItemList初始化失败");
			}
		}
		return _list;
	}
}
