using System;
using KBEngine;
using UnityEngine;

// Token: 0x020001AB RID: 427
public class UIBiGuan : MonoBehaviour
{
	// Token: 0x1700021D RID: 541
	// (get) Token: 0x0600120F RID: 4623 RVA: 0x0006D8A2 File Offset: 0x0006BAA2
	public UIInput getInputYear
	{
		get
		{
			return this.inputYear;
		}
	}

	// Token: 0x1700021E RID: 542
	// (get) Token: 0x06001210 RID: 4624 RVA: 0x0006D8AA File Offset: 0x0006BAAA
	public UIInput getInputMonth
	{
		get
		{
			return this.inputMonth;
		}
	}

	// Token: 0x06001211 RID: 4625 RVA: 0x0006D8B4 File Offset: 0x0006BAB4
	private void Start()
	{
		this.avatar = Tools.instance.getPlayer();
		this.slider.onChange.Add(new EventDelegate(new EventDelegate.Callback(this.sliderOnChenge)));
		this.inputYear.onChange.Add(new EventDelegate(new EventDelegate.Callback(this.InputOnChenge)));
		this.inputMonth.onChange.Add(new EventDelegate(new EventDelegate.Callback(this.InputOnChenge)));
		this.setInputPercent(0, 1);
		this.InputOnChenge();
	}

	// Token: 0x06001212 RID: 4626 RVA: 0x0006D944 File Offset: 0x0006BB44
	public void sliderOnChenge()
	{
		int num = (int)((this.slider.value + 1E-06f) * (float)this.MaxTime);
		this.setInputPercent(num / 12, num % 12);
	}

	// Token: 0x06001213 RID: 4627 RVA: 0x0006D97C File Offset: 0x0006BB7C
	public void InputOnChenge()
	{
		try
		{
			int num = int.Parse(this.inputYear.value);
			int num2 = int.Parse(this.inputMonth.value);
			int num3 = num * 12 + num2;
			if (num3 < this.MaxTime)
			{
				this.setSliderPercent((float)num3 / (float)this.MaxTime);
			}
			else
			{
				this.setInputPercent(this.MaxTime / 12, this.MaxTime % 12);
				this.setSliderPercent(1f);
			}
		}
		catch (Exception)
		{
			this.inputYear.value = "0";
			this.inputMonth.value = "0";
		}
	}

	// Token: 0x06001214 RID: 4628 RVA: 0x0006DA24 File Offset: 0x0006BC24
	public void setInputPercent(int year, int month)
	{
		this.inputYear.value = string.Concat(year);
		this.inputMonth.value = string.Concat(month);
	}

	// Token: 0x06001215 RID: 4629 RVA: 0x0006DA52 File Offset: 0x0006BC52
	public void setSliderPercent(float value)
	{
		this.slider.value = value;
	}

	// Token: 0x06001216 RID: 4630 RVA: 0x0006DA60 File Offset: 0x0006BC60
	public void init()
	{
		UILabel component = base.transform.Find("Panel/desc").GetComponent<UILabel>();
		float n = jsonData.instance.LevelUpDataJsonData[string.Concat(this.avatar.level)]["MaxExp"].n;
		ulong exp = this.avatar.exp;
		component.text = "[000000]" + Tools.instance.Code64ToString(jsonData.instance.BiguanJsonData[string.Concat(this.biguanType)]["Text"].str);
		this.setSliderPercent(0.5f);
	}

	// Token: 0x06001217 RID: 4631 RVA: 0x0006DB14 File Offset: 0x0006BD14
	public float getBiguanSpeed()
	{
		int staticID = this.avatar.getStaticID();
		if (staticID != 0)
		{
			float num = jsonData.instance.XinJinGuanLianJsonData[string.Concat(this.avatar.getXinJinGuanlianType())]["speed"].n / 100f;
			float n = jsonData.instance.StaticSkillJsonData[string.Concat(staticID)]["Skill_Speed"].n;
			float num2 = (n * (jsonData.instance.BiguanJsonData[string.Concat(this.biguanType)]["speed"].n / 100f) * num + this.avatar.AddZiZhiSpeed(n)) * this.avatar.getJieDanSkillAddExp();
			if (this.avatar.TianFuID.HasField(string.Concat(12)))
			{
				float num3 = this.avatar.TianFuID["12"].n / 100f;
				num2 += num2 * num3;
			}
			return num2;
		}
		return 0f;
	}

	// Token: 0x06001218 RID: 4632 RVA: 0x0006DC38 File Offset: 0x0006BE38
	private void OkEvent()
	{
		this.flagSwitch = true;
		int num = int.Parse(this.inputYear.value);
		int num2 = int.Parse(this.inputMonth.value);
		this.avatar.AddTime(0, num2, num);
		int staticID = this.avatar.getStaticID();
		if (staticID != 0)
		{
			float n = jsonData.instance.StaticSkillJsonData[string.Concat(staticID)]["Skill_Speed"].n;
			float num3 = jsonData.instance.XinJinGuanLianJsonData[string.Concat(this.avatar.getXinJinGuanlianType())]["speed"].n / 100f;
			this.avatar.addEXP((int)((float)(num * 12 + num2) * this.getBiguanSpeed()));
		}
		if (this.biGuanYinfo != null)
		{
			this.biGuanYinfo.setEventShiJian();
		}
		this.avatar.HP = this.avatar.HP_Max;
		try
		{
			this.avatar.wuDaoMag.autoAddBiGuanLingGuang(num2 + num * 12);
		}
		catch (Exception ex)
		{
			Debug.LogError(ex);
		}
	}

	// Token: 0x06001219 RID: 4633 RVA: 0x0006DD68 File Offset: 0x0006BF68
	public void OK()
	{
		if (!Tools.canClickFlag)
		{
			return;
		}
		bool flag = int.Parse(this.inputYear.value) != 0;
		int num = int.Parse(this.inputMonth.value);
		if (!flag && num == 0)
		{
			return;
		}
		if (!this.flagSwitch)
		{
			return;
		}
		this.flagSwitch = false;
		Tools.instance.playFader("正在闭关苦修...", null);
		base.Invoke("OkEvent", 0.5f);
	}

	// Token: 0x0600121A RID: 4634 RVA: 0x00065233 File Offset: 0x00063433
	public void close()
	{
		base.transform.localPosition = new Vector3(0f, 1000f, 0f);
	}

	// Token: 0x0600121B RID: 4635 RVA: 0x00004095 File Offset: 0x00002295
	private void Update()
	{
	}

	// Token: 0x04000CCC RID: 3276
	public int biguanType = 1;

	// Token: 0x04000CCD RID: 3277
	[SerializeField]
	private UISlider slider;

	// Token: 0x04000CCE RID: 3278
	[SerializeField]
	private UIInput inputYear;

	// Token: 0x04000CCF RID: 3279
	[SerializeField]
	private UIInput inputMonth;

	// Token: 0x04000CD0 RID: 3280
	public BiGuanYinfo biGuanYinfo;

	// Token: 0x04000CD1 RID: 3281
	private Avatar avatar;

	// Token: 0x04000CD2 RID: 3282
	[SerializeField]
	public int MaxTime = 100;

	// Token: 0x04000CD3 RID: 3283
	[SerializeField]
	private int MinTime = 1;

	// Token: 0x04000CD4 RID: 3284
	private bool flagSwitch = true;
}
