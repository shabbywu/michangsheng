using System;
using GUIPackage;
using UnityEngine;

// Token: 0x02000461 RID: 1121
public class LianDanFinsh : MonoBehaviour
{
	// Token: 0x0600231E RID: 8990 RVA: 0x000B5E62 File Offset: 0x000B4062
	public void close()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x0600231F RID: 8991 RVA: 0x0005FDE2 File Offset: 0x0005DFE2
	public void Open()
	{
		base.gameObject.SetActive(true);
	}

	// Token: 0x06002320 RID: 8992 RVA: 0x000F012C File Offset: 0x000EE32C
	public void succes(int index, int num)
	{
		this.Open();
		this.text.text = Tools.Code64(jsonData.instance.LianDanSuccessItemLeiXin[index.ToString()]["desc"].str).Replace("{Num}", num.ToString());
		this.SuccessPlan.SetActive(true);
		this.FailPlan.SetActive(false);
	}

	// Token: 0x06002321 RID: 8993 RVA: 0x000F01A0 File Offset: 0x000EE3A0
	public void fail(int index, int num)
	{
		this.Open();
		this.text.text = Tools.Code64(jsonData.instance.LianDanSuccessItemLeiXin[index.ToString()]["desc"].str).Replace("{Num}", num.ToString());
		this.SuccessPlan.SetActive(false);
		this.FailPlan.SetActive(true);
	}

	// Token: 0x06002322 RID: 8994 RVA: 0x000B5E62 File Offset: 0x000B4062
	public void zhalue()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x06002323 RID: 8995 RVA: 0x0005FDE2 File Offset: 0x0005DFE2
	public void SHowZhalu()
	{
		base.gameObject.SetActive(true);
	}

	// Token: 0x06002324 RID: 8996 RVA: 0x000F0211 File Offset: 0x000EE411
	private void Awake()
	{
		this.close();
	}

	// Token: 0x06002325 RID: 8997 RVA: 0x00004095 File Offset: 0x00002295
	private void Start()
	{
	}

	// Token: 0x06002326 RID: 8998 RVA: 0x00004095 File Offset: 0x00002295
	private void Update()
	{
	}

	// Token: 0x04001C49 RID: 7241
	public Inventory2 inventory2show;

	// Token: 0x04001C4A RID: 7242
	public GameObject SuccessPlan;

	// Token: 0x04001C4B RID: 7243
	public GameObject FailPlan;

	// Token: 0x04001C4C RID: 7244
	public UILabel text;
}
