using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CaiJi;

public class CaiJiTimeSelect : MonoBehaviour
{
	[SerializeField]
	private Slider TimeSlider;

	[SerializeField]
	private Text Year;

	[SerializeField]
	private Text Month;

	[SerializeField]
	private int MinValue;

	[SerializeField]
	private int MaxValue;

	private bool IsLingHeCaiJi;

	private void Awake()
	{
		TimeSlider.maxValue = MaxValue;
		TimeSlider.minValue = MinValue;
		TimeSlider.wholeNumbers = true;
	}

	public void Init(bool isLingHeCaiJi = false)
	{
		IsLingHeCaiJi = isLingHeCaiJi;
		UpdateValue();
		((UnityEvent<float>)(object)TimeSlider.onValueChanged).AddListener((UnityAction<float>)delegate
		{
			UpdateValue();
		});
	}

	public void Add()
	{
		Slider timeSlider = TimeSlider;
		timeSlider.value += 1f;
		if (TimeSlider.value > (float)MaxValue)
		{
			TimeSlider.value = MaxValue;
		}
		UpdateValue();
	}

	public void Reduce()
	{
		Slider timeSlider = TimeSlider;
		timeSlider.value -= 1f;
		if (TimeSlider.value < 0f)
		{
			TimeSlider.value = MinValue;
		}
		UpdateValue();
	}

	private void UpdateValue()
	{
		if (IsLingHeCaiJi)
		{
			LingHeCaiJiUIMag.inst.CostTime = (int)TimeSlider.value;
			Month.text = (LingHeCaiJiUIMag.inst.CostTime % 12).ToString();
			Year.text = (LingHeCaiJiUIMag.inst.CostTime / 12).ToString();
			LingHeCaiJiUIMag.inst.UpdateItemShow();
		}
		else
		{
			CaiJiUIMag.inst.CostTime = (int)TimeSlider.value;
			Month.text = (CaiJiUIMag.inst.CostTime % 12).ToString();
			Year.text = (CaiJiUIMag.inst.CostTime / 12).ToString();
			CaiJiUIMag.inst.UpdateMayGetItem();
		}
	}
}
