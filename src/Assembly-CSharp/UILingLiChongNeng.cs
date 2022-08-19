using System;
using JSONClass;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x0200020C RID: 524
public class UILingLiChongNeng : MonoBehaviour, IESCClose
{
	// Token: 0x060014FD RID: 5373 RVA: 0x000864B4 File Offset: 0x000846B4
	public static void Show()
	{
		GameObject gameObject = Object.Instantiate<GameObject>(ResManager.inst.LoadPrefab("UILinTianSelectTips"));
		ESCCloseManager.Inst.RegisterClose(gameObject.GetComponent<UILingLiChongNeng>());
	}

	// Token: 0x060014FE RID: 5374 RVA: 0x000864E6 File Offset: 0x000846E6
	private void Awake()
	{
		this.Init();
	}

	// Token: 0x060014FF RID: 5375 RVA: 0x000864F0 File Offset: 0x000846F0
	private void Init()
	{
		base.transform.SetParent(NewUICanvas.Inst.gameObject.transform);
		base.transform.localScale = Vector3.one;
		base.transform.SetAsLastSibling();
		base.transform.GetComponent<RectTransform>().offsetMin = new Vector2(0f, 0f);
		base.transform.GetComponent<RectTransform>().offsetMax = new Vector2(0f, 0f);
		this.slider.onValueChanged.AddListener(new UnityAction<float>(this.OnvalueChanged));
		this.df = new DongFuData(DongFuManager.NowDongFuID);
		this.df.Load();
		this.addLingShi = 0;
		this.baseLingShi = this.df.CuiShengLingLi;
		UIDongFu.Inst.LingTian.CalcSpeed(0);
		this.maxLinshi = Mathf.Min((int)Tools.instance.getPlayer().money, UIDongFu.Inst.LingTian.CuiShengLingShi50Year);
		this.totalLingShi.text = Tools.instance.getPlayer().money.ToString();
		this.slider.value = 0f;
		this.monthCost = DFZhenYanLevel.DataDict[this.df.JuLingZhenLevel].lingtiancuishengsudu;
		this.UpdateUI();
	}

	// Token: 0x06001500 RID: 5376 RVA: 0x0008664E File Offset: 0x0008484E
	private void OnvalueChanged(float arg0)
	{
		this.addLingShi = (int)((float)this.maxLinshi * arg0);
		this.UpdateUI();
	}

	// Token: 0x06001501 RID: 5377 RVA: 0x00086668 File Offset: 0x00084868
	private void UpdateUI()
	{
		this.curLingShi.text = (this.baseLingShi + this.addLingShi).ToString();
		UIDongFu.Inst.LingTian.CalcSpeed(this.addLingShi);
		int num = UIDongFu.Inst.LingTian.CuiShengTime;
		int num2 = num / 12;
		num -= num2 * 12;
		this.time.text = string.Format("{0}年{1}月", num2, num);
	}

	// Token: 0x06001502 RID: 5378 RVA: 0x000866E8 File Offset: 0x000848E8
	public void AddMonth()
	{
		if (this.addLingShi + this.monthCost > this.maxLinshi)
		{
			return;
		}
		this.addLingShi += this.monthCost;
		this.slider.value = (float)this.addLingShi / (float)this.maxLinshi;
	}

	// Token: 0x06001503 RID: 5379 RVA: 0x00086738 File Offset: 0x00084938
	public void ReduceMonth()
	{
		if (this.addLingShi <= this.monthCost)
		{
			this.addLingShi = 0;
		}
		else
		{
			this.addLingShi -= this.monthCost;
		}
		this.slider.value = (float)this.addLingShi / (float)this.maxLinshi;
	}

	// Token: 0x06001504 RID: 5380 RVA: 0x0008678C File Offset: 0x0008498C
	public void Ok()
	{
		this.df.CuiShengLingLi += this.addLingShi;
		this.df.Save();
		UIDongFu.Inst.InitData();
		UIDongFu.Inst.LingTian.RefreshUI();
		Tools.instance.getPlayer().AddMoney(-this.addLingShi);
		this.Close();
	}

	// Token: 0x06001505 RID: 5381 RVA: 0x000867F1 File Offset: 0x000849F1
	public void Close()
	{
		Object.Destroy(base.gameObject);
		ESCCloseManager.Inst.UnRegisterClose(this);
	}

	// Token: 0x06001506 RID: 5382 RVA: 0x00086809 File Offset: 0x00084A09
	public bool TryEscClose()
	{
		this.Close();
		return true;
	}

	// Token: 0x04000FC0 RID: 4032
	[SerializeField]
	private Text time;

	// Token: 0x04000FC1 RID: 4033
	[SerializeField]
	private Text curLingShi;

	// Token: 0x04000FC2 RID: 4034
	[SerializeField]
	private Text totalLingShi;

	// Token: 0x04000FC3 RID: 4035
	[SerializeField]
	private Slider slider;

	// Token: 0x04000FC4 RID: 4036
	private DongFuData df;

	// Token: 0x04000FC5 RID: 4037
	private int baseLingShi;

	// Token: 0x04000FC6 RID: 4038
	private int addLingShi;

	// Token: 0x04000FC7 RID: 4039
	private int maxLinshi;

	// Token: 0x04000FC8 RID: 4040
	private int monthCost;
}
