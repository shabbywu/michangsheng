using System;
using KBEngine;
using UnityEngine;

public class UIBiGuan : MonoBehaviour
{
	public int biguanType = 1;

	[SerializeField]
	private UISlider slider;

	[SerializeField]
	private UIInput inputYear;

	[SerializeField]
	private UIInput inputMonth;

	public BiGuanYinfo biGuanYinfo;

	private Avatar avatar;

	[SerializeField]
	public int MaxTime = 100;

	[SerializeField]
	private int MinTime = 1;

	private bool flagSwitch = true;

	public UIInput getInputYear => inputYear;

	public UIInput getInputMonth => inputMonth;

	private void Start()
	{
		avatar = Tools.instance.getPlayer();
		slider.onChange.Add(new EventDelegate(sliderOnChenge));
		inputYear.onChange.Add(new EventDelegate(InputOnChenge));
		inputMonth.onChange.Add(new EventDelegate(InputOnChenge));
		setInputPercent(0, 1);
		InputOnChenge();
	}

	public void sliderOnChenge()
	{
		int num = (int)((slider.value + 1E-06f) * (float)MaxTime);
		setInputPercent(num / 12, num % 12);
	}

	public void InputOnChenge()
	{
		try
		{
			int num = int.Parse(inputYear.value);
			int num2 = int.Parse(inputMonth.value);
			int num3 = num * 12 + num2;
			if (num3 < MaxTime)
			{
				setSliderPercent((float)num3 / (float)MaxTime);
				return;
			}
			setInputPercent(MaxTime / 12, MaxTime % 12);
			setSliderPercent(1f);
		}
		catch (Exception)
		{
			inputYear.value = "0";
			inputMonth.value = "0";
		}
	}

	public void setInputPercent(int year, int month)
	{
		inputYear.value = string.Concat(year);
		inputMonth.value = string.Concat(month);
	}

	public void setSliderPercent(float value)
	{
		slider.value = value;
	}

	public void init()
	{
		UILabel component = ((Component)((Component)this).transform.Find("Panel/desc")).GetComponent<UILabel>();
		_ = jsonData.instance.LevelUpDataJsonData[string.Concat(avatar.level)]["MaxExp"].n;
		_ = avatar.exp;
		component.text = "[000000]" + Tools.instance.Code64ToString(jsonData.instance.BiguanJsonData[string.Concat(biguanType)]["Text"].str);
		setSliderPercent(0.5f);
	}

	public float getBiguanSpeed()
	{
		int staticID = avatar.getStaticID();
		if (staticID != 0)
		{
			float num = jsonData.instance.XinJinGuanLianJsonData[string.Concat(avatar.getXinJinGuanlianType())]["speed"].n / 100f;
			float n = jsonData.instance.StaticSkillJsonData[string.Concat(staticID)]["Skill_Speed"].n;
			float num2 = (n * (jsonData.instance.BiguanJsonData[string.Concat(biguanType)]["speed"].n / 100f) * num + avatar.AddZiZhiSpeed(n)) * avatar.getJieDanSkillAddExp();
			if (avatar.TianFuID.HasField(string.Concat(12)))
			{
				float num3 = avatar.TianFuID["12"].n / 100f;
				num2 += num2 * num3;
			}
			return num2;
		}
		return 0f;
	}

	private void OkEvent()
	{
		flagSwitch = true;
		int num = int.Parse(inputYear.value);
		int num2 = int.Parse(inputMonth.value);
		avatar.AddTime(0, num2, num);
		int staticID = avatar.getStaticID();
		if (staticID != 0)
		{
			_ = jsonData.instance.StaticSkillJsonData[string.Concat(staticID)]["Skill_Speed"].n;
			_ = jsonData.instance.XinJinGuanLianJsonData[string.Concat(avatar.getXinJinGuanlianType())]["speed"].n / 100f;
			avatar.addEXP((int)((float)(num * 12 + num2) * getBiguanSpeed()));
		}
		if ((Object)(object)biGuanYinfo != (Object)null)
		{
			biGuanYinfo.setEventShiJian();
		}
		avatar.HP = avatar.HP_Max;
		try
		{
			avatar.wuDaoMag.autoAddBiGuanLingGuang(num2 + num * 12);
		}
		catch (Exception ex)
		{
			Debug.LogError((object)ex);
		}
	}

	public void OK()
	{
		if (Tools.canClickFlag)
		{
			int num = int.Parse(inputYear.value);
			int num2 = int.Parse(inputMonth.value);
			if ((num != 0 || num2 != 0) && flagSwitch)
			{
				flagSwitch = false;
				Tools.instance.playFader("正在闭关苦修...");
				((MonoBehaviour)this).Invoke("OkEvent", 0.5f);
			}
		}
	}

	public void close()
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		((Component)this).transform.localPosition = new Vector3(0f, 1000f, 0f);
	}

	private void Update()
	{
	}
}
