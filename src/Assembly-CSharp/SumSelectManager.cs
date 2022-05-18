using System;
using GUIPackage;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000508 RID: 1288
public class SumSelectManager : MonoBehaviour
{
	// Token: 0x0600213D RID: 8509 RVA: 0x00115DB4 File Offset: 0x00113FB4
	private void Awake()
	{
		this.slider.onValueChanged.AddListener(new UnityAction<float>(this.moveSlider));
		this.Btn_Add.onClick.AddListener(new UnityAction(this.addSum));
		this.Btn_Reduce.onClick.AddListener(new UnityAction(this.reduiceSum));
		base.gameObject.SetActive(false);
	}

	// Token: 0x0600213E RID: 8510 RVA: 0x00115E24 File Offset: 0x00114024
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

	// Token: 0x0600213F RID: 8511 RVA: 0x000111B3 File Offset: 0x0000F3B3
	private void close()
	{
		Object.Destroy(base.gameObject);
	}

	// Token: 0x06002140 RID: 8512 RVA: 0x00116060 File Offset: 0x00114260
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

	// Token: 0x06002141 RID: 8513 RVA: 0x0011612C File Offset: 0x0011432C
	private void addSum()
	{
		this.itemSum += 1f;
		if (this.itemSum > this.Max)
		{
			this.itemSum = this.Max;
		}
		this.slider.value = this.itemSum / this.Max;
	}

	// Token: 0x06002142 RID: 8514 RVA: 0x00116180 File Offset: 0x00114380
	private void reduiceSum()
	{
		this.itemSum -= 1f;
		if (this.itemSum < 0f)
		{
			this.itemSum = 0f;
		}
		this.slider.value = this.itemSum / this.Max;
	}

	// Token: 0x04001CBD RID: 7357
	private string Desc;

	// Token: 0x04001CBE RID: 7358
	private string Name;

	// Token: 0x04001CBF RID: 7359
	[SerializeField]
	private Text DescText;

	// Token: 0x04001CC0 RID: 7360
	[SerializeField]
	private Text LianDanDescText;

	// Token: 0x04001CC1 RID: 7361
	[HideInInspector]
	public float itemSum;

	// Token: 0x04001CC2 RID: 7362
	private float Max;

	// Token: 0x04001CC3 RID: 7363
	[SerializeField]
	private Slider slider;

	// Token: 0x04001CC4 RID: 7364
	[SerializeField]
	private Button Btn_Add;

	// Token: 0x04001CC5 RID: 7365
	[SerializeField]
	private Button Btn_Reduce;

	// Token: 0x04001CC6 RID: 7366
	[SerializeField]
	private Button Btn_Cancel;

	// Token: 0x04001CC7 RID: 7367
	[SerializeField]
	private Button Btn_OK;

	// Token: 0x04001CC8 RID: 7368
	[SerializeField]
	private GameObject mask;

	// Token: 0x04001CC9 RID: 7369
	public bool isShowMask = true;

	// Token: 0x04001CCA RID: 7370
	[SerializeField]
	public item Item;

	// Token: 0x04001CCB RID: 7371
	private SumSelectManager.SpecialType type;

	// Token: 0x02000509 RID: 1289
	public enum SpecialType
	{
		// Token: 0x04001CCD RID: 7373
		空,
		// Token: 0x04001CCE RID: 7374
		炼丹
	}
}
