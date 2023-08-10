using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BagItemSelect : MonoBehaviour, ISelectNum
{
	public Slider Slider;

	public string ItemName;

	public int CurNum;

	public int MaxNum;

	public int MinNum;

	public Text Content;

	public FpBtn OkBtn;

	public FpBtn CancelBtn;

	private void Awake()
	{
		((UnityEvent<float>)(object)Slider.onValueChanged).AddListener((UnityAction<float>)UpdateUI);
	}

	public virtual void Init(string itemName, int maxNum, UnityAction Ok = null, UnityAction Cancel = null)
	{
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Expected O, but got Unknown
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Expected O, but got Unknown
		if (maxNum == 1)
		{
			CurNum = 1;
			if (Ok != null)
			{
				Ok.Invoke();
			}
			Close();
			return;
		}
		Clear();
		CurNum = 1;
		MinNum = 1;
		MaxNum = maxNum;
		Slider.maxValue = maxNum;
		Slider.minValue = 1f;
		ItemName = itemName;
		Slider.value = CurNum;
		UpdateUI(0f);
		((Component)this).gameObject.SetActive(true);
		OkBtn.mouseUpEvent.AddListener((UnityAction)delegate
		{
			if (Ok != null)
			{
				Ok.Invoke();
			}
			Close();
		});
		CancelBtn.mouseUpEvent.AddListener((UnityAction)delegate
		{
			if (Cancel != null)
			{
				Cancel.Invoke();
			}
			Close();
		});
	}

	public void Clear()
	{
		CurNum = 0;
		MinNum = 0;
		MinNum = 0;
		Slider.value = 0f;
		((UnityEventBase)OkBtn.mouseUpEvent).RemoveAllListeners();
		((UnityEventBase)CancelBtn.mouseUpEvent).RemoveAllListeners();
	}

	public void Close()
	{
		((Component)this).gameObject.SetActive(false);
	}

	public virtual void UpdateUI(float call)
	{
		CurNum = (int)Slider.value;
		Content.SetText($"{ItemName}x{CurNum}");
	}

	public void AddNum()
	{
		CurNum++;
		if (CurNum > MaxNum)
		{
			CurNum = MaxNum;
		}
		Slider.value = CurNum;
	}

	public void ReduceNum()
	{
		CurNum--;
		if (CurNum < MinNum)
		{
			CurNum = MinNum;
		}
		Slider.value = CurNum;
	}
}
