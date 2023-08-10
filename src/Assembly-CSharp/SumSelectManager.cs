using GUIPackage;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SumSelectManager : MonoBehaviour
{
	public enum SpecialType
	{
		空,
		炼丹
	}

	private string Desc;

	private string Name;

	[SerializeField]
	private Text DescText;

	[SerializeField]
	private Text LianDanDescText;

	[HideInInspector]
	public float itemSum;

	private float Max;

	[SerializeField]
	private Slider slider;

	[SerializeField]
	private Button Btn_Add;

	[SerializeField]
	private Button Btn_Reduce;

	[SerializeField]
	private Button Btn_Cancel;

	[SerializeField]
	private Button Btn_OK;

	[SerializeField]
	private GameObject mask;

	public bool isShowMask = true;

	[SerializeField]
	public item Item;

	private SpecialType type;

	private void Awake()
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Expected O, but got Unknown
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Expected O, but got Unknown
		((UnityEvent<float>)(object)slider.onValueChanged).AddListener((UnityAction<float>)moveSlider);
		((UnityEvent)Btn_Add.onClick).AddListener(new UnityAction(addSum));
		((UnityEvent)Btn_Reduce.onClick).AddListener(new UnityAction(reduiceSum));
		((Component)this).gameObject.SetActive(false);
	}

	public void showSelect(string desc, int itemID, float maxSum, UnityAction OK, UnityAction Cancel, SpecialType specialType = SpecialType.空)
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Expected O, but got Unknown
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Expected O, but got Unknown
		type = specialType;
		((Component)this).gameObject.SetActive(true);
		((UnityEvent)Btn_OK.onClick).AddListener(new UnityAction(close));
		((UnityEvent)Btn_Cancel.onClick).AddListener(new UnityAction(close));
		Desc = Tools.Code64(desc) + " ";
		if (!isShowMask)
		{
			mask.SetActive(false);
		}
		if (type == SpecialType.空)
		{
			string name = Tools.Code64(jsonData.instance.ItemJsonData[itemID.ToString()]["name"].str);
			Name = Tools.Code64(Tools.setColorByID(name, itemID)) + " ";
		}
		else
		{
			Name = LianDanSystemManager.inst.lianDanPageManager.getLianDanName() + " ";
		}
		Max = maxSum;
		((UnityEvent)Btn_OK.onClick).AddListener(OK);
		if (Cancel != null)
		{
			((UnityEvent)Btn_Cancel.onClick).AddListener(Cancel);
		}
		if (maxSum >= 1f)
		{
			itemSum = 1f;
		}
		else
		{
			itemSum = 0f;
		}
		slider.value = itemSum / Max;
		type = specialType;
		if (type == SpecialType.空)
		{
			((Component)DescText).gameObject.SetActive(true);
			((Component)LianDanDescText).gameObject.SetActive(false);
			DescText.text = Tools.Code64($"{Desc}{Name}x{(int)itemSum}");
			return;
		}
		((Component)DescText).gameObject.SetActive(false);
		((Component)LianDanDescText).gameObject.SetActive(true);
		LianDanDescText.text = Tools.Code64($"{Desc}{Name}x{(int)itemSum}\n预计花费{LianDanSystemManager.inst.lianDanResultManager.getCostTime((int)itemSum)}");
	}

	private void close()
	{
		Object.Destroy((Object)(object)((Component)this).gameObject);
	}

	public void moveSlider(float arg0)
	{
		float num = Max * arg0;
		if (num < 0f)
		{
			num = 0f;
		}
		if (num > Max)
		{
			num = Max;
		}
		itemSum = num;
		if (type == SpecialType.空)
		{
			DescText.text = Tools.Code64($"{Desc}{Name}x{(int)itemSum}");
			return;
		}
		LianDanDescText.text = Tools.Code64($"{Desc}{Name}x{(int)itemSum}\n预计花费{LianDanSystemManager.inst.lianDanResultManager.getCostTime((int)num)}");
	}

	private void addSum()
	{
		itemSum += 1f;
		if (itemSum > Max)
		{
			itemSum = Max;
		}
		slider.value = itemSum / Max;
	}

	private void reduiceSum()
	{
		itemSum -= 1f;
		if (itemSum < 0f)
		{
			itemSum = 0f;
		}
		slider.value = itemSum / Max;
	}
}
