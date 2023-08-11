using System;
using System.Runtime.CompilerServices;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GanWuSelect : MonoBehaviour, IESCClose
{
	[Serializable]
	[CompilerGenerated]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9 = new _003C_003Ec();

		public static UnityAction _003C_003E9__24_0;

		internal void _003CQueDing_003Eb__24_0()
		{
			UIBiGuanGanWuPanel.Inst.RefreshUI();
		}
	}

	public static GanWuSelect inst;

	[SerializeField]
	private Text year;

	[SerializeField]
	private Text month;

	[SerializeField]
	private Text day;

	[SerializeField]
	private CanvasGroup tipsCanvasGroup;

	[SerializeField]
	private Text curExpText;

	private TweenerCore<float, float, FloatOptions> _dotween;

	[SerializeField]
	private Slider slider;

	private string _uid;

	public bool IsShowTips;

	public bool IsShow;

	public int TotalExp;

	public string Name;

	public int CurExp;

	private int curDay;

	private int maxDay = 360;

	public Avatar player;

	private void Awake()
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		((Component)this).transform.SetParent(((Component)NewUICanvas.Inst).gameObject.transform);
		((Component)this).transform.localScale = Vector3.one;
		((Component)this).transform.SetAsLastSibling();
		((Component)((Component)this).transform).GetComponent<RectTransform>().offsetMin = new Vector2(0f, 0f);
		((Component)((Component)this).transform).GetComponent<RectTransform>().offsetMax = new Vector2(0f, 0f);
		inst = this;
		player = Tools.instance.getPlayer();
		((Component)this).gameObject.SetActive(false);
		ESCCloseManager.Inst.RegisterClose(this);
	}

	public void Init(string uuid, string name, int maxTime)
	{
		JSONObject jSONObject = player.LingGuang.list.Find((JSONObject aa) => aa["uuid"].str == uuid);
		TotalExp = (int)((float)jSONObject["studyTime"].I * jsonData.instance.WuDaoExBeiLuJson["1"]["lingguang" + jSONObject["quality"].I].n);
		CurExp = TotalExp;
		slider.value = 1f;
		_uid = uuid;
		Name = name;
		maxDay = maxTime;
		curDay = maxTime;
		updateData();
		((UnityEvent<float>)(object)slider.onValueChanged).AddListener((UnityAction<float>)OnDragSlider);
		((Component)this).gameObject.SetActive(true);
	}

	public void OnDragSlider(float data)
	{
		curDay = (int)(data * (float)maxDay);
		if (curDay < 1)
		{
			curDay = 1;
		}
		updateData();
	}

	private void updateData()
	{
		if (maxDay > curDay)
		{
			IsShowTips = true;
		}
		else
		{
			IsShowTips = false;
		}
		if (IsShow != IsShowTips)
		{
			if (IsShowTips)
			{
				ShowTips();
			}
			else
			{
				HideTips();
			}
		}
		int num = curDay / 365;
		int num2 = (curDay - 365 * num) / 30;
		int num3 = curDay - 365 * num - 30 * num2;
		year.SetText(num);
		month.SetText(num2);
		day.SetText(num3);
		CurExp = (int)((float)curDay / (float)maxDay * (float)TotalExp);
		curExpText.SetText("对" + Name + "的感悟提升");
		curExpText.AddText($"<size=40>{CurExp}</size>", "#f0e7b8");
		curExpText.AddText("经验");
	}

	public void AddDay()
	{
		curDay++;
		if (curDay > maxDay)
		{
			curDay = maxDay;
		}
		slider.value = (float)curDay / (float)maxDay;
		updateData();
	}

	public void ReduceDay()
	{
		curDay--;
		if (curDay < 1)
		{
			curDay = 1;
		}
		slider.value = (float)curDay / (float)maxDay;
		updateData();
	}

	public void Close()
	{
		inst = null;
		ESCCloseManager.Inst.UnRegisterClose(this);
		Object.Destroy((Object)(object)((Component)this).gameObject);
	}

	public void QueDing()
	{
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Expected O, but got Unknown
		if (!XiuLian.CheckCanUseDay(curDay))
		{
			UIPopTip.Inst.Pop("房间剩余时间不足，无法感悟");
			return;
		}
		Tools instance = Tools.instance;
		object obj = _003C_003Ec._003C_003E9__24_0;
		if (obj == null)
		{
			UnityAction val = delegate
			{
				UIBiGuanGanWuPanel.Inst.RefreshUI();
			};
			_003C_003Ec._003C_003E9__24_0 = val;
			obj = (object)val;
		}
		instance.playFader("正在感悟天道...", (UnityAction)obj);
		player.HP = player.HP_Max;
		player.wuDaoMag.StudyLingGuang(_uid, curDay, (float)curDay / (float)maxDay);
		Close();
	}

	public void ShowTips()
	{
		if (_dotween != null)
		{
			TweenExtensions.Kill((Tween)(object)_dotween, false);
		}
		_dotween = DOTween.To((DOGetter<float>)(() => tipsCanvasGroup.alpha), (DOSetter<float>)delegate(float x)
		{
			tipsCanvasGroup.alpha = x;
		}, 1f, 0.3f);
		IsShow = true;
		((Component)tipsCanvasGroup).gameObject.SetActive(true);
	}

	public void HideTips()
	{
		if (_dotween != null)
		{
			TweenExtensions.Kill((Tween)(object)_dotween, false);
		}
		_dotween = DOTween.To((DOGetter<float>)(() => tipsCanvasGroup.alpha), (DOSetter<float>)delegate(float x)
		{
			tipsCanvasGroup.alpha = x;
		}, 0f, 0.3f);
		IsShow = false;
	}

	public bool TryEscClose()
	{
		Close();
		return true;
	}
}
