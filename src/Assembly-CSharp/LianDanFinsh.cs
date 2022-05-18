using System;
using GUIPackage;
using UnityEngine;

// Token: 0x0200061B RID: 1563
public class LianDanFinsh : MonoBehaviour
{
	// Token: 0x060026D1 RID: 9937 RVA: 0x00017C2D File Offset: 0x00015E2D
	public void close()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x060026D2 RID: 9938 RVA: 0x00011B82 File Offset: 0x0000FD82
	public void Open()
	{
		base.gameObject.SetActive(true);
	}

	// Token: 0x060026D3 RID: 9939 RVA: 0x00130638 File Offset: 0x0012E838
	public void succes(int index, int num)
	{
		this.Open();
		this.text.text = Tools.Code64(jsonData.instance.LianDanSuccessItemLeiXin[index.ToString()]["desc"].str).Replace("{Num}", num.ToString());
		this.SuccessPlan.SetActive(true);
		this.FailPlan.SetActive(false);
	}

	// Token: 0x060026D4 RID: 9940 RVA: 0x001306AC File Offset: 0x0012E8AC
	public void fail(int index, int num)
	{
		this.Open();
		this.text.text = Tools.Code64(jsonData.instance.LianDanSuccessItemLeiXin[index.ToString()]["desc"].str).Replace("{Num}", num.ToString());
		this.SuccessPlan.SetActive(false);
		this.FailPlan.SetActive(true);
	}

	// Token: 0x060026D5 RID: 9941 RVA: 0x00017C2D File Offset: 0x00015E2D
	public void zhalue()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x060026D6 RID: 9942 RVA: 0x00011B82 File Offset: 0x0000FD82
	public void SHowZhalu()
	{
		base.gameObject.SetActive(true);
	}

	// Token: 0x060026D7 RID: 9943 RVA: 0x0001EE63 File Offset: 0x0001D063
	private void Awake()
	{
		this.close();
	}

	// Token: 0x060026D8 RID: 9944 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x060026D9 RID: 9945 RVA: 0x000042DD File Offset: 0x000024DD
	private void Update()
	{
	}

	// Token: 0x04002119 RID: 8473
	public Inventory2 inventory2show;

	// Token: 0x0400211A RID: 8474
	public GameObject SuccessPlan;

	// Token: 0x0400211B RID: 8475
	public GameObject FailPlan;

	// Token: 0x0400211C RID: 8476
	public UILabel text;
}
