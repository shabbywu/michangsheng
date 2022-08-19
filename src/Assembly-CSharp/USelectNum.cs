using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000369 RID: 873
public class USelectNum : MonoBehaviour
{
	// Token: 0x06001D40 RID: 7488 RVA: 0x000CF324 File Offset: 0x000CD524
	private void Awake()
	{
		if (USelectNum.inst != null)
		{
			Object.Destroy(base.transform.parent.gameObject);
		}
		USelectNum.inst = this;
		USelectNum.myRT = (base.transform as RectTransform);
		Object.DontDestroyOnLoad(base.transform.parent.gameObject);
	}

	// Token: 0x06001D41 RID: 7489 RVA: 0x000CF380 File Offset: 0x000CD580
	private void Update()
	{
		if (USelectNum.needShow)
		{
			USelectNum.IsShow = true;
			USelectNum.needShow = false;
			USelectNum.myRT.GetChild(0).gameObject.SetActive(true);
			this.RefreshText(1f);
			this.NumSlider.maxValue = (float)USelectNum.MaxNum;
			this.NumSlider.minValue = (float)USelectNum.MinNum;
			this.NumSlider.onValueChanged.RemoveAllListeners();
			this.NumSlider.value = 1f;
			this.NumSlider.onValueChanged.AddListener(new UnityAction<float>(this.RefreshText));
			this.OkBtn.onClick.RemoveAllListeners();
			this.OkBtn.onClick.AddListener(delegate()
			{
				USelectNum.Close();
			});
			this.OkBtn.onClick.AddListener(delegate()
			{
				if (USelectNum.OKAction != null)
				{
					USelectNum.OKAction.Invoke(this.NowNum);
				}
			});
			this.CancelBtn.onClick.RemoveAllListeners();
			this.CancelBtn.onClick.AddListener(delegate()
			{
				USelectNum.Close();
			});
			this.CancelBtn.onClick.AddListener(delegate()
			{
				if (USelectNum.CancelAction != null)
				{
					USelectNum.CancelAction.Invoke();
				}
			});
			this.AddBtn.onClick.RemoveAllListeners();
			this.AddBtn.onClick.AddListener(delegate()
			{
				Slider numSlider = this.NumSlider;
				float value = numSlider.value;
				numSlider.value = value + 1f;
			});
			this.SubBtn.onClick.RemoveAllListeners();
			this.SubBtn.onClick.AddListener(delegate()
			{
				Slider numSlider = this.NumSlider;
				float value = numSlider.value;
				numSlider.value = value - 1f;
			});
		}
	}

	// Token: 0x06001D42 RID: 7490 RVA: 0x000CF543 File Offset: 0x000CD743
	public static void Close()
	{
		USelectNum.IsShow = false;
		if (USelectNum.myRT != null)
		{
			USelectNum.myRT.GetChild(0).gameObject.SetActive(false);
		}
	}

	// Token: 0x06001D43 RID: 7491 RVA: 0x000CF56E File Offset: 0x000CD76E
	private void RefreshText(float v)
	{
		this.NowNum = (int)v;
		this.DescText.text = USelectNum.Decs.Replace("{num}", this.NowNum.ToString());
	}

	// Token: 0x06001D44 RID: 7492 RVA: 0x000CF5A0 File Offset: 0x000CD7A0
	public static void Show(string desc, int minNum, int maxNum, UnityAction<int> OK = null, UnityAction Cancel = null)
	{
		if (USelectNum.inst == null)
		{
			Object.Instantiate<GameObject>(Resources.Load<GameObject>(USelectNum.prefabPath));
		}
		USelectNum.Decs = desc;
		USelectNum.MinNum = minNum;
		USelectNum.MaxNum = maxNum;
		USelectNum.OKAction = OK;
		USelectNum.CancelAction = Cancel;
		USelectNum.needShow = true;
	}

	// Token: 0x040017D9 RID: 6105
	private static USelectNum inst;

	// Token: 0x040017DA RID: 6106
	private static string prefabPath = "USelectNum";

	// Token: 0x040017DB RID: 6107
	private static RectTransform myRT;

	// Token: 0x040017DC RID: 6108
	private static bool needShow;

	// Token: 0x040017DD RID: 6109
	private static string Decs;

	// Token: 0x040017DE RID: 6110
	private static int MinNum;

	// Token: 0x040017DF RID: 6111
	private static int MaxNum;

	// Token: 0x040017E0 RID: 6112
	private static UnityAction<int> OKAction;

	// Token: 0x040017E1 RID: 6113
	private static UnityAction CancelAction;

	// Token: 0x040017E2 RID: 6114
	public static bool IsShow;

	// Token: 0x040017E3 RID: 6115
	public Text DescText;

	// Token: 0x040017E4 RID: 6116
	public Slider NumSlider;

	// Token: 0x040017E5 RID: 6117
	public Button SubBtn;

	// Token: 0x040017E6 RID: 6118
	public Button AddBtn;

	// Token: 0x040017E7 RID: 6119
	public Button OkBtn;

	// Token: 0x040017E8 RID: 6120
	public Button CancelBtn;

	// Token: 0x040017E9 RID: 6121
	private int NowNum;
}
