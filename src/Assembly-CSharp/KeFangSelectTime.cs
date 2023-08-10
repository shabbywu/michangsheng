using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class KeFangSelectTime : MonoBehaviour, IESCClose
{
	public static KeFangSelectTime inst;

	[SerializeField]
	private Text needLingShi;

	[SerializeField]
	private Text year;

	[SerializeField]
	private Text month;

	[SerializeField]
	private Slider slider;

	private int curMonth;

	private int maxMonth = 360;

	public int price = 10;

	public string screenName = "";

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
		curMonth = 1;
	}

	public void Init()
	{
		if ((int)player.money / price < maxMonth)
		{
			maxMonth = (int)player.money / price;
		}
		slider.value = 1f / (float)maxMonth;
		updateData();
		((UnityEvent<float>)(object)slider.onValueChanged).AddListener((UnityAction<float>)OnDragSlider);
		ESCCloseManager.Inst.RegisterClose(inst);
	}

	public void OnDragSlider(float data)
	{
		curMonth = (int)(data * (float)maxMonth);
		if (curMonth < 1)
		{
			curMonth = 1;
		}
		updateData();
	}

	private void updateData()
	{
		year.text = (curMonth / 12).ToString();
		month.text = (curMonth % 12).ToString();
		needLingShi.text = (curMonth * price).ToString();
	}

	public void AddMonth()
	{
		curMonth++;
		if (curMonth > maxMonth)
		{
			curMonth = maxMonth;
		}
		slider.value = (float)curMonth / (float)maxMonth;
		updateData();
	}

	public void ReduceMonth()
	{
		curMonth--;
		if (curMonth < 1)
		{
			curMonth = 1;
		}
		slider.value = (float)curMonth / (float)maxMonth;
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
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		player.zulinContorl.KZAddTime(screenName, 0, curMonth, 0);
		player.money -= (ulong)(curMonth * price);
		Scene activeScene = SceneManager.GetActiveScene();
		if (((Scene)(ref activeScene)).name != screenName)
		{
			Tools.instance.loadMapScenes(screenName);
		}
		Close();
	}

	public bool TryEscClose()
	{
		Close();
		return true;
	}
}
