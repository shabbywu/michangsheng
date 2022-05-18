using System;
using JSONClass;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000324 RID: 804
public class UILingLiChongNeng : MonoBehaviour, IESCClose
{
	// Token: 0x060017AE RID: 6062 RVA: 0x000CEC94 File Offset: 0x000CCE94
	public static void Show()
	{
		GameObject gameObject = Object.Instantiate<GameObject>(ResManager.inst.LoadPrefab("UILinTianSelectTips"));
		ESCCloseManager.Inst.RegisterClose(gameObject.GetComponent<UILingLiChongNeng>());
	}

	// Token: 0x060017AF RID: 6063 RVA: 0x00014EC9 File Offset: 0x000130C9
	private void Awake()
	{
		this.Init();
	}

	// Token: 0x060017B0 RID: 6064 RVA: 0x000CECC8 File Offset: 0x000CCEC8
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

	// Token: 0x060017B1 RID: 6065 RVA: 0x00014ED1 File Offset: 0x000130D1
	private void OnvalueChanged(float arg0)
	{
		this.addLingShi = (int)((float)this.maxLinshi * arg0);
		this.UpdateUI();
	}

	// Token: 0x060017B2 RID: 6066 RVA: 0x000CEE28 File Offset: 0x000CD028
	private void UpdateUI()
	{
		this.curLingShi.text = (this.baseLingShi + this.addLingShi).ToString();
		UIDongFu.Inst.LingTian.CalcSpeed(this.addLingShi);
		int num = UIDongFu.Inst.LingTian.CuiShengTime;
		int num2 = num / 12;
		num -= num2 * 12;
		this.time.text = string.Format("{0}年{1}月", num2, num);
	}

	// Token: 0x060017B3 RID: 6067 RVA: 0x000CEEA8 File Offset: 0x000CD0A8
	public void AddMonth()
	{
		if (this.addLingShi + this.monthCost > this.maxLinshi)
		{
			return;
		}
		this.addLingShi += this.monthCost;
		this.slider.value = (float)this.addLingShi / (float)this.maxLinshi;
	}

	// Token: 0x060017B4 RID: 6068 RVA: 0x000CEEF8 File Offset: 0x000CD0F8
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

	// Token: 0x060017B5 RID: 6069 RVA: 0x000CEF4C File Offset: 0x000CD14C
	public void Ok()
	{
		this.df.CuiShengLingLi += this.addLingShi;
		this.df.Save();
		UIDongFu.Inst.InitData();
		UIDongFu.Inst.LingTian.RefreshUI();
		Tools.instance.getPlayer().AddMoney(-this.addLingShi);
		this.Close();
	}

	// Token: 0x060017B6 RID: 6070 RVA: 0x00014EE9 File Offset: 0x000130E9
	public void Close()
	{
		Object.Destroy(base.gameObject);
		ESCCloseManager.Inst.UnRegisterClose(this);
	}

	// Token: 0x060017B7 RID: 6071 RVA: 0x00014F01 File Offset: 0x00013101
	public bool TryEscClose()
	{
		this.Close();
		return true;
	}

	// Token: 0x04001310 RID: 4880
	[SerializeField]
	private Text time;

	// Token: 0x04001311 RID: 4881
	[SerializeField]
	private Text curLingShi;

	// Token: 0x04001312 RID: 4882
	[SerializeField]
	private Text totalLingShi;

	// Token: 0x04001313 RID: 4883
	[SerializeField]
	private Slider slider;

	// Token: 0x04001314 RID: 4884
	private DongFuData df;

	// Token: 0x04001315 RID: 4885
	private int baseLingShi;

	// Token: 0x04001316 RID: 4886
	private int addLingShi;

	// Token: 0x04001317 RID: 4887
	private int maxLinshi;

	// Token: 0x04001318 RID: 4888
	private int monthCost;
}
