using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000286 RID: 646
public class GanWuSelect : MonoBehaviour, IESCClose
{
	// Token: 0x0600174F RID: 5967 RVA: 0x0009FED4 File Offset: 0x0009E0D4
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

	// Token: 0x06001750 RID: 5968 RVA: 0x0009FF84 File Offset: 0x0009E184
	public void Init(string uuid, string name, int maxTime)
	{
		JSONObject jsonobject = this.player.LingGuang.list.Find((JSONObject aa) => aa["uuid"].str == uuid);
		this.TotalExp = (int)((float)jsonobject["studyTime"].I * jsonData.instance.WuDaoExBeiLuJson["1"]["lingguang" + jsonobject["quality"].I].n);
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

	// Token: 0x06001751 RID: 5969 RVA: 0x000A0081 File Offset: 0x0009E281
	public void OnDragSlider(float data)
	{
		this.curDay = (int)(data * (float)this.maxDay);
		if (this.curDay < 1)
		{
			this.curDay = 1;
		}
		this.updateData();
	}

	// Token: 0x06001752 RID: 5970 RVA: 0x000A00AC File Offset: 0x0009E2AC
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

	// Token: 0x06001753 RID: 5971 RVA: 0x000A01D4 File Offset: 0x0009E3D4
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

	// Token: 0x06001754 RID: 5972 RVA: 0x000A0229 File Offset: 0x0009E429
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

	// Token: 0x06001755 RID: 5973 RVA: 0x000A0269 File Offset: 0x0009E469
	public void Close()
	{
		GanWuSelect.inst = null;
		ESCCloseManager.Inst.UnRegisterClose(this);
		Object.Destroy(base.gameObject);
	}

	// Token: 0x06001756 RID: 5974 RVA: 0x000A0288 File Offset: 0x0009E488
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

	// Token: 0x06001757 RID: 5975 RVA: 0x000A0328 File Offset: 0x0009E528
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

	// Token: 0x06001758 RID: 5976 RVA: 0x000A0390 File Offset: 0x0009E590
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

	// Token: 0x06001759 RID: 5977 RVA: 0x000A03E5 File Offset: 0x0009E5E5
	public bool TryEscClose()
	{
		this.Close();
		return true;
	}

	// Token: 0x04001201 RID: 4609
	public static GanWuSelect inst;

	// Token: 0x04001202 RID: 4610
	[SerializeField]
	private Text year;

	// Token: 0x04001203 RID: 4611
	[SerializeField]
	private Text month;

	// Token: 0x04001204 RID: 4612
	[SerializeField]
	private Text day;

	// Token: 0x04001205 RID: 4613
	[SerializeField]
	private CanvasGroup tipsCanvasGroup;

	// Token: 0x04001206 RID: 4614
	[SerializeField]
	private Text curExpText;

	// Token: 0x04001207 RID: 4615
	private TweenerCore<float, float, FloatOptions> _dotween;

	// Token: 0x04001208 RID: 4616
	[SerializeField]
	private Slider slider;

	// Token: 0x04001209 RID: 4617
	private string _uid;

	// Token: 0x0400120A RID: 4618
	public bool IsShowTips;

	// Token: 0x0400120B RID: 4619
	public bool IsShow;

	// Token: 0x0400120C RID: 4620
	public int TotalExp;

	// Token: 0x0400120D RID: 4621
	public string Name;

	// Token: 0x0400120E RID: 4622
	public int CurExp;

	// Token: 0x0400120F RID: 4623
	private int curDay;

	// Token: 0x04001210 RID: 4624
	private int maxDay = 360;

	// Token: 0x04001211 RID: 4625
	public Avatar player;
}
