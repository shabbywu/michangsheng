using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020004E6 RID: 1254
public class USelectNum : MonoBehaviour
{
	// Token: 0x060020AA RID: 8362 RVA: 0x00113B44 File Offset: 0x00111D44
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

	// Token: 0x060020AB RID: 8363 RVA: 0x00113BA0 File Offset: 0x00111DA0
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

	// Token: 0x060020AC RID: 8364 RVA: 0x0001AE12 File Offset: 0x00019012
	public static void Close()
	{
		USelectNum.IsShow = false;
		if (USelectNum.myRT != null)
		{
			USelectNum.myRT.GetChild(0).gameObject.SetActive(false);
		}
	}

	// Token: 0x060020AD RID: 8365 RVA: 0x0001AE3D File Offset: 0x0001903D
	private void RefreshText(float v)
	{
		this.NowNum = (int)v;
		this.DescText.text = USelectNum.Decs.Replace("{num}", this.NowNum.ToString());
	}

	// Token: 0x060020AE RID: 8366 RVA: 0x00113D64 File Offset: 0x00111F64
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

	// Token: 0x04001C2B RID: 7211
	private static USelectNum inst;

	// Token: 0x04001C2C RID: 7212
	private static string prefabPath = "USelectNum";

	// Token: 0x04001C2D RID: 7213
	private static RectTransform myRT;

	// Token: 0x04001C2E RID: 7214
	private static bool needShow;

	// Token: 0x04001C2F RID: 7215
	private static string Decs;

	// Token: 0x04001C30 RID: 7216
	private static int MinNum;

	// Token: 0x04001C31 RID: 7217
	private static int MaxNum;

	// Token: 0x04001C32 RID: 7218
	private static UnityAction<int> OKAction;

	// Token: 0x04001C33 RID: 7219
	private static UnityAction CancelAction;

	// Token: 0x04001C34 RID: 7220
	public static bool IsShow;

	// Token: 0x04001C35 RID: 7221
	public Text DescText;

	// Token: 0x04001C36 RID: 7222
	public Slider NumSlider;

	// Token: 0x04001C37 RID: 7223
	public Button SubBtn;

	// Token: 0x04001C38 RID: 7224
	public Button AddBtn;

	// Token: 0x04001C39 RID: 7225
	public Button OkBtn;

	// Token: 0x04001C3A RID: 7226
	public Button CancelBtn;

	// Token: 0x04001C3B RID: 7227
	private int NowNum;
}
