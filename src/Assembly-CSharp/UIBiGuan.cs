using System;
using KBEngine;
using UnityEngine;

// Token: 0x020002A8 RID: 680
public class UIBiGuan : MonoBehaviour
{
	// Token: 0x17000265 RID: 613
	// (get) Token: 0x060014B6 RID: 5302 RVA: 0x0001309A File Offset: 0x0001129A
	public UIInput getInputYear
	{
		get
		{
			return this.inputYear;
		}
	}

	// Token: 0x17000266 RID: 614
	// (get) Token: 0x060014B7 RID: 5303 RVA: 0x000130A2 File Offset: 0x000112A2
	public UIInput getInputMonth
	{
		get
		{
			return this.inputMonth;
		}
	}

	// Token: 0x060014B8 RID: 5304 RVA: 0x000BB728 File Offset: 0x000B9928
	private void Start()
	{
		this.avatar = Tools.instance.getPlayer();
		this.slider.onChange.Add(new EventDelegate(new EventDelegate.Callback(this.sliderOnChenge)));
		this.inputYear.onChange.Add(new EventDelegate(new EventDelegate.Callback(this.InputOnChenge)));
		this.inputMonth.onChange.Add(new EventDelegate(new EventDelegate.Callback(this.InputOnChenge)));
		this.setInputPercent(0, 1);
		this.InputOnChenge();
	}

	// Token: 0x060014B9 RID: 5305 RVA: 0x000BB7B8 File Offset: 0x000B99B8
	public void sliderOnChenge()
	{
		int num = (int)((this.slider.value + 1E-06f) * (float)this.MaxTime);
		this.setInputPercent(num / 12, num % 12);
	}

	// Token: 0x060014BA RID: 5306 RVA: 0x000BB7F0 File Offset: 0x000B99F0
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

	// Token: 0x060014BB RID: 5307 RVA: 0x000130AA File Offset: 0x000112AA
	public void setInputPercent(int year, int month)
	{
		this.inputYear.value = string.Concat(year);
		this.inputMonth.value = string.Concat(month);
	}

	// Token: 0x060014BC RID: 5308 RVA: 0x000130D8 File Offset: 0x000112D8
	public void setSliderPercent(float value)
	{
		this.slider.value = value;
	}

	// Token: 0x060014BD RID: 5309 RVA: 0x000BB898 File Offset: 0x000B9A98
	public void init()
	{
		UILabel component = base.transform.Find("Panel/desc").GetComponent<UILabel>();
		float n = jsonData.instance.LevelUpDataJsonData[string.Concat(this.avatar.level)]["MaxExp"].n;
		ulong exp = this.avatar.exp;
		component.text = "[000000]" + Tools.instance.Code64ToString(jsonData.instance.BiguanJsonData[string.Concat(this.biguanType)]["Text"].str);
		this.setSliderPercent(0.5f);
	}

	// Token: 0x060014BE RID: 5310 RVA: 0x000BB94C File Offset: 0x000B9B4C
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

	// Token: 0x060014BF RID: 5311 RVA: 0x000BBA70 File Offset: 0x000B9C70
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

	// Token: 0x060014C0 RID: 5312 RVA: 0x000BBBA0 File Offset: 0x000B9DA0
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

	// Token: 0x060014C1 RID: 5313 RVA: 0x000122F6 File Offset: 0x000104F6
	public void close()
	{
		base.transform.localPosition = new Vector3(0f, 1000f, 0f);
	}

	// Token: 0x060014C2 RID: 5314 RVA: 0x000042DD File Offset: 0x000024DD
	private void Update()
	{
	}

	// Token: 0x04000FF4 RID: 4084
	public int biguanType = 1;

	// Token: 0x04000FF5 RID: 4085
	[SerializeField]
	private UISlider slider;

	// Token: 0x04000FF6 RID: 4086
	[SerializeField]
	private UIInput inputYear;

	// Token: 0x04000FF7 RID: 4087
	[SerializeField]
	private UIInput inputMonth;

	// Token: 0x04000FF8 RID: 4088
	public BiGuanYinfo biGuanYinfo;

	// Token: 0x04000FF9 RID: 4089
	private Avatar avatar;

	// Token: 0x04000FFA RID: 4090
	[SerializeField]
	public int MaxTime = 100;

	// Token: 0x04000FFB RID: 4091
	[SerializeField]
	private int MinTime = 1;

	// Token: 0x04000FFC RID: 4092
	private bool flagSwitch = true;
}
