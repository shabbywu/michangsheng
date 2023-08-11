using JSONClass;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UILingLiChongNeng : MonoBehaviour, IESCClose
{
	[SerializeField]
	private Text time;

	[SerializeField]
	private Text curLingShi;

	[SerializeField]
	private Text totalLingShi;

	[SerializeField]
	private Slider slider;

	private DongFuData df;

	private int baseLingShi;

	private int addLingShi;

	private int maxLinshi;

	private int monthCost;

	public static void Show()
	{
		GameObject val = Object.Instantiate<GameObject>(ResManager.inst.LoadPrefab("UILinTianSelectTips"));
		ESCCloseManager.Inst.RegisterClose(val.GetComponent<UILingLiChongNeng>());
	}

	private void Awake()
	{
		Init();
	}

	private void Init()
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		((Component)this).transform.SetParent(((Component)NewUICanvas.Inst).gameObject.transform);
		((Component)this).transform.localScale = Vector3.one;
		((Component)this).transform.SetAsLastSibling();
		((Component)((Component)this).transform).GetComponent<RectTransform>().offsetMin = new Vector2(0f, 0f);
		((Component)((Component)this).transform).GetComponent<RectTransform>().offsetMax = new Vector2(0f, 0f);
		((UnityEvent<float>)(object)slider.onValueChanged).AddListener((UnityAction<float>)OnvalueChanged);
		df = new DongFuData(DongFuManager.NowDongFuID);
		df.Load();
		addLingShi = 0;
		baseLingShi = df.CuiShengLingLi;
		UIDongFu.Inst.LingTian.CalcSpeed();
		maxLinshi = Mathf.Min((int)Tools.instance.getPlayer().money, UIDongFu.Inst.LingTian.CuiShengLingShi50Year);
		totalLingShi.text = Tools.instance.getPlayer().money.ToString();
		slider.value = 0f;
		monthCost = DFZhenYanLevel.DataDict[df.JuLingZhenLevel].lingtiancuishengsudu;
		UpdateUI();
	}

	private void OnvalueChanged(float arg0)
	{
		addLingShi = (int)((float)maxLinshi * arg0);
		UpdateUI();
	}

	private void UpdateUI()
	{
		curLingShi.text = (baseLingShi + addLingShi).ToString();
		UIDongFu.Inst.LingTian.CalcSpeed(addLingShi);
		int cuiShengTime = UIDongFu.Inst.LingTian.CuiShengTime;
		int num = cuiShengTime / 12;
		cuiShengTime -= num * 12;
		time.text = $"{num}年{cuiShengTime}月";
	}

	public void AddMonth()
	{
		if (addLingShi + monthCost <= maxLinshi)
		{
			addLingShi += monthCost;
			slider.value = (float)addLingShi / (float)maxLinshi;
		}
	}

	public void ReduceMonth()
	{
		if (addLingShi <= monthCost)
		{
			addLingShi = 0;
		}
		else
		{
			addLingShi -= monthCost;
		}
		slider.value = (float)addLingShi / (float)maxLinshi;
	}

	public void Ok()
	{
		df.CuiShengLingLi += addLingShi;
		df.Save();
		UIDongFu.Inst.InitData();
		UIDongFu.Inst.LingTian.RefreshUI();
		Tools.instance.getPlayer().AddMoney(-addLingShi);
		Close();
	}

	public void Close()
	{
		Object.Destroy((Object)(object)((Component)this).gameObject);
		ESCCloseManager.Inst.UnRegisterClose(this);
	}

	public bool TryEscClose()
	{
		Close();
		return true;
	}
}
