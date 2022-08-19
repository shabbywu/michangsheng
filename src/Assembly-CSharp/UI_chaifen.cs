using System;
using GUIPackage;
using UnityEngine;

// Token: 0x02000403 RID: 1027
public class UI_chaifen : MonoBehaviour
{
	// Token: 0x06002121 RID: 8481 RVA: 0x00004095 File Offset: 0x00002295
	private void Start()
	{
	}

	// Token: 0x06002122 RID: 8482 RVA: 0x00004095 File Offset: 0x00002295
	private void Update()
	{
	}

	// Token: 0x06002123 RID: 8483 RVA: 0x000E7D1C File Offset: 0x000E5F1C
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

	// Token: 0x06002124 RID: 8484 RVA: 0x000E7D70 File Offset: 0x000E5F70
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

	// Token: 0x06002125 RID: 8485 RVA: 0x000E7DC4 File Offset: 0x000E5FC4
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

	// Token: 0x04001AD3 RID: 6867
	[SerializeField]
	public UIInput inputNum;

	// Token: 0x04001AD4 RID: 6868
	public item Item;
}
