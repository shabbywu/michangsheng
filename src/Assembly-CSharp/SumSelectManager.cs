using System;
using GUIPackage;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000385 RID: 901
public class SumSelectManager : MonoBehaviour
{
	// Token: 0x06001DC4 RID: 7620 RVA: 0x000D1FA4 File Offset: 0x000D01A4
	private void Awake()
	{
		this.slider.onValueChanged.AddListener(new UnityAction<float>(this.moveSlider));
		this.Btn_Add.onClick.AddListener(new UnityAction(this.addSum));
		this.Btn_Reduce.onClick.AddListener(new UnityAction(this.reduiceSum));
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001DC5 RID: 7621 RVA: 0x000D2014 File Offset: 0x000D0214
	public void showSelect(string desc, int itemID, float maxSum, UnityAction OK, UnityAction Cancel, SumSelectManager.SpecialType specialType = SumSelectManager.SpecialType.空)
	{
		this.type = specialType;
		base.gameObject.SetActive(true);
		this.Btn_OK.onClick.AddListener(new UnityAction(this.close));
		this.Btn_Cancel.onClick.AddListener(new UnityAction(this.close));
		this.Desc = Tools.Code64(desc) + " ";
		if (!this.isShowMask)
		{
			this.mask.SetActive(false);
		}
		if (this.type == SumSelectManager.SpecialType.空)
		{
			string name = Tools.Code64(jsonData.instance.ItemJsonData[itemID.ToString()]["name"].str);
			this.Name = Tools.Code64(Tools.setColorByID(name, itemID)) + " ";
		}
		else
		{
			this.Name = LianDanSystemManager.inst.lianDanPageManager.getLianDanName() + " ";
		}
		this.Max = maxSum;
		this.Btn_OK.onClick.AddListener(OK);
		if (Cancel != null)
		{
			this.Btn_Cancel.onClick.AddListener(Cancel);
		}
		if (maxSum >= 1f)
		{
			this.itemSum = 1f;
		}
		else
		{
			this.itemSum = 0f;
		}
		this.slider.value = this.itemSum / this.Max;
		this.type = specialType;
		if (this.type == SumSelectManager.SpecialType.空)
		{
			this.DescText.gameObject.SetActive(true);
			this.LianDanDescText.gameObject.SetActive(false);
			this.DescText.text = Tools.Code64(string.Format("{0}{1}x{2}", this.Desc, this.Name, (int)this.itemSum));
			return;
		}
		this.DescText.gameObject.SetActive(false);
		this.LianDanDescText.gameObject.SetActive(true);
		this.LianDanDescText.text = Tools.Code64(string.Format("{0}{1}x{2}\n预计花费{3}", new object[]
		{
			this.Desc,
			this.Name,
			(int)this.itemSum,
			LianDanSystemManager.inst.lianDanResultManager.getCostTime((int)this.itemSum)
		}));
	}

	// Token: 0x06001DC6 RID: 7622 RVA: 0x0005C928 File Offset: 0x0005AB28
	private void close()
	{
		Object.Destroy(base.gameObject);
	}

	// Token: 0x06001DC7 RID: 7623 RVA: 0x000D2250 File Offset: 0x000D0450
	public void moveSlider(float arg0)
	{
		float num = this.Max * arg0;
		if (num < 0f)
		{
			num = 0f;
		}
		if (num > this.Max)
		{
			num = this.Max;
		}
		this.itemSum = num;
		if (this.type == SumSelectManager.SpecialType.空)
		{
			this.DescText.text = Tools.Code64(string.Format("{0}{1}x{2}", this.Desc, this.Name, (int)this.itemSum));
			return;
		}
		this.LianDanDescText.text = Tools.Code64(string.Format("{0}{1}x{2}\n预计花费{3}", new object[]
		{
			this.Desc,
			this.Name,
			(int)this.itemSum,
			LianDanSystemManager.inst.lianDanResultManager.getCostTime((int)num)
		}));
	}

	// Token: 0x06001DC8 RID: 7624 RVA: 0x000D231C File Offset: 0x000D051C
	private void addSum()
	{
		this.itemSum += 1f;
		if (this.itemSum > this.Max)
		{
			this.itemSum = this.Max;
		}
		this.slider.value = this.itemSum / this.Max;
	}

	// Token: 0x06001DC9 RID: 7625 RVA: 0x000D2370 File Offset: 0x000D0570
	private void reduiceSum()
	{
		this.itemSum -= 1f;
		if (this.itemSum < 0f)
		{
			this.itemSum = 0f;
		}
		this.slider.value = this.itemSum / this.Max;
	}

	// Token: 0x04001862 RID: 6242
	private string Desc;

	// Token: 0x04001863 RID: 6243
	private string Name;

	// Token: 0x04001864 RID: 6244
	[SerializeField]
	private Text DescText;

	// Token: 0x04001865 RID: 6245
	[SerializeField]
	private Text LianDanDescText;

	// Token: 0x04001866 RID: 6246
	[HideInInspector]
	public float itemSum;

	// Token: 0x04001867 RID: 6247
	private float Max;

	// Token: 0x04001868 RID: 6248
	[SerializeField]
	private Slider slider;

	// Token: 0x04001869 RID: 6249
	[SerializeField]
	private Button Btn_Add;

	// Token: 0x0400186A RID: 6250
	[SerializeField]
	private Button Btn_Reduce;

	// Token: 0x0400186B RID: 6251
	[SerializeField]
	private Button Btn_Cancel;

	// Token: 0x0400186C RID: 6252
	[SerializeField]
	private Button Btn_OK;

	// Token: 0x0400186D RID: 6253
	[SerializeField]
	private GameObject mask;

	// Token: 0x0400186E RID: 6254
	public bool isShowMask = true;

	// Token: 0x0400186F RID: 6255
	[SerializeField]
	public item Item;

	// Token: 0x04001870 RID: 6256
	private SumSelectManager.SpecialType type;

	// Token: 0x0200135D RID: 4957
	public enum SpecialType
	{
		// Token: 0x04006833 RID: 26675
		空,
		// Token: 0x04006834 RID: 26676
		炼丹
	}
}
