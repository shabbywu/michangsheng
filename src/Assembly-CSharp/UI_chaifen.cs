using System;
using GUIPackage;
using UnityEngine;

// Token: 0x020005B3 RID: 1459
public class UI_chaifen : MonoBehaviour
{
	// Token: 0x060024D3 RID: 9427 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x060024D4 RID: 9428 RVA: 0x000042DD File Offset: 0x000024DD
	private void Update()
	{
	}

	// Token: 0x060024D5 RID: 9429 RVA: 0x00129A18 File Offset: 0x00127C18
	public void reduceLabelNum()
	{
		try
		{
			int num = int.Parse(this.inputNum.value);
			this.inputNum.value = string.Concat(num - 1);
			this.InputOnChenge();
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x060024D6 RID: 9430 RVA: 0x00129A6C File Offset: 0x00127C6C
	public void addLabelNum()
	{
		try
		{
			int num = int.Parse(this.inputNum.value);
			this.inputNum.value = string.Concat(num + 1);
			this.InputOnChenge();
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x060024D7 RID: 9431 RVA: 0x00129AC0 File Offset: 0x00127CC0
	public void InputOnChenge()
	{
		try
		{
			int num = int.Parse(this.inputNum.value);
			if (num < 1)
			{
				this.inputNum.value = "1";
			}
			else if (this.Item.itemNum < num)
			{
				this.inputNum.value = string.Concat(this.Item.itemNum);
			}
		}
		catch (Exception)
		{
			this.inputNum.value = "1";
		}
	}

	// Token: 0x04001F8F RID: 8079
	[SerializeField]
	public UIInput inputNum;

	// Token: 0x04001F90 RID: 8080
	public item Item;
}
