using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020003AD RID: 941
public class GanWuSelect : MonoBehaviour, IESCClose
{
	// Token: 0x06001A1E RID: 6686 RVA: 0x000E74CC File Offset: 0x000E56CC
	private void Awake()
	{
		base.transform.SetParent(NewUICanvas.Inst.gameObject.transform);
		base.transform.localScale = Vector3.one;
		base.transform.SetAsLastSibling();
		base.transform.GetComponent<RectTransform>().offsetMin = new Vector2(0f, 0f);
		base.transform.GetComponent<RectTransform>().offsetMax = new Vector2(0f, 0f);
		GanWuSelect.inst = this;
		this.player = Tools.instance.getPlayer();
		base.gameObject.SetActive(false);
		ESCCloseManager.Inst.RegisterClose(this);
	}

	// Token: 0x06001A1F RID: 6687 RVA: 0x000E757C File Offset: 0x000E577C
	public void Init(string uuid, string name, int maxTime)
	{
		JSONObject jsonobject = this.player.LingGuang.list.Find((JSONObject aa) => aa["uuid"].str == uuid);
		this.TotalExp = jsonobject["studyTime"].I * jsonData.instance.WuDaoExBeiLuJson["1"]["lingguang" + jsonobject["quality"].I].I;
		this.CurExp = this.TotalExp;
		this.slider.value = 1f;
		this._uid = uuid;
		this.Name = name;
		this.maxDay = maxTime;
		this.curDay = maxTime;
		this.updateData();
		this.slider.onValueChanged.AddListener(new UnityAction<float>(this.OnDragSlider));
		base.gameObject.SetActive(true);
	}

	// Token: 0x06001A20 RID: 6688 RVA: 0x0001658B File Offset: 0x0001478B
	public void OnDragSlider(float data)
	{
		this.curDay = (int)(data * (float)this.maxDay);
		if (this.curDay < 1)
		{
			this.curDay = 1;
		}
		this.updateData();
	}

	// Token: 0x06001A21 RID: 6689 RVA: 0x000E7678 File Offset: 0x000E5878
	private void updateData()
	{
		if (this.maxDay > this.curDay)
		{
			this.IsShowTips = true;
		}
		else
		{
			this.IsShowTips = false;
		}
		if (this.IsShow != this.IsShowTips)
		{
			if (this.IsShowTips)
			{
				this.ShowTips();
			}
			else
			{
				this.HideTips();
			}
		}
		int num = this.curDay / 365;
		int num2 = (this.curDay - 365 * num) / 30;
		int num3 = this.curDay - 365 * num - 30 * num2;
		this.year.SetText(num);
		this.month.SetText(num2);
		this.day.SetText(num3);
		this.CurExp = (int)((float)this.curDay / (float)this.maxDay * (float)this.TotalExp);
		this.curExpText.SetText("对" + this.Name + "的感悟提升");
		this.curExpText.AddText(string.Format("<size=40>{0}</size>", this.CurExp), "#f0e7b8");
		this.curExpText.AddText("经验");
	}

	// Token: 0x06001A22 RID: 6690 RVA: 0x000E77A0 File Offset: 0x000E59A0
	public void AddDay()
	{
		this.curDay++;
		if (this.curDay > this.maxDay)
		{
			this.curDay = this.maxDay;
		}
		this.slider.value = (float)this.curDay / (float)this.maxDay;
		this.updateData();
	}

	// Token: 0x06001A23 RID: 6691 RVA: 0x000165B3 File Offset: 0x000147B3
	public void ReduceDay()
	{
		this.curDay--;
		if (this.curDay < 1)
		{
			this.curDay = 1;
		}
		this.slider.value = (float)this.curDay / (float)this.maxDay;
		this.updateData();
	}

	// Token: 0x06001A24 RID: 6692 RVA: 0x000165F3 File Offset: 0x000147F3
	public void Close()
	{
		GanWuSelect.inst = null;
		ESCCloseManager.Inst.UnRegisterClose(this);
		Object.Destroy(base.gameObject);
	}

	// Token: 0x06001A25 RID: 6693 RVA: 0x000E77F8 File Offset: 0x000E59F8
	public void QueDing()
	{
		if (!XiuLian.CheckCanUseDay(this.curDay))
		{
			UIPopTip.Inst.Pop("房间剩余时间不足，无法感悟", PopTipIconType.叹号);
			return;
		}
		Tools.instance.playFader("正在感悟天道...", delegate
		{
			UIBiGuanGanWuPanel.Inst.RefreshUI();
		});
		this.player.HP = this.player.HP_Max;
		this.player.wuDaoMag.StudyLingGuang(this._uid, this.curDay, (float)this.curDay / (float)this.maxDay);
		this.Close();
	}

	// Token: 0x06001A26 RID: 6694 RVA: 0x000E7898 File Offset: 0x000E5A98
	public void ShowTips()
	{
		if (this._dotween != null)
		{
			TweenExtensions.Kill(this._dotween, false);
		}
		this._dotween = DOTween.To(() => this.tipsCanvasGroup.alpha, delegate(float x)
		{
			this.tipsCanvasGroup.alpha = x;
		}, 1f, 0.3f);
		this.IsShow = true;
		this.tipsCanvasGroup.gameObject.SetActive(true);
	}

	// Token: 0x06001A27 RID: 6695 RVA: 0x000E7900 File Offset: 0x000E5B00
	public void HideTips()
	{
		if (this._dotween != null)
		{
			TweenExtensions.Kill(this._dotween, false);
		}
		this._dotween = DOTween.To(() => this.tipsCanvasGroup.alpha, delegate(float x)
		{
			this.tipsCanvasGroup.alpha = x;
		}, 0f, 0.3f);
		this.IsShow = false;
	}

	// Token: 0x06001A28 RID: 6696 RVA: 0x00016611 File Offset: 0x00014811
	public bool TryEscClose()
	{
		this.Close();
		return true;
	}

	// Token: 0x04001574 RID: 5492
	public static GanWuSelect inst;

	// Token: 0x04001575 RID: 5493
	[SerializeField]
	private Text year;

	// Token: 0x04001576 RID: 5494
	[SerializeField]
	private Text month;

	// Token: 0x04001577 RID: 5495
	[SerializeField]
	private Text day;

	// Token: 0x04001578 RID: 5496
	[SerializeField]
	private CanvasGroup tipsCanvasGroup;

	// Token: 0x04001579 RID: 5497
	[SerializeField]
	private Text curExpText;

	// Token: 0x0400157A RID: 5498
	private TweenerCore<float, float, FloatOptions> _dotween;

	// Token: 0x0400157B RID: 5499
	[SerializeField]
	private Slider slider;

	// Token: 0x0400157C RID: 5500
	private string _uid;

	// Token: 0x0400157D RID: 5501
	public bool IsShowTips;

	// Token: 0x0400157E RID: 5502
	public bool IsShow;

	// Token: 0x0400157F RID: 5503
	public int TotalExp;

	// Token: 0x04001580 RID: 5504
	public string Name;

	// Token: 0x04001581 RID: 5505
	public int CurExp;

	// Token: 0x04001582 RID: 5506
	private int curDay;

	// Token: 0x04001583 RID: 5507
	private int maxDay = 360;

	// Token: 0x04001584 RID: 5508
	public Avatar player;
}
