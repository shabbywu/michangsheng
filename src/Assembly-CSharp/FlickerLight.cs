using System;
using System.Collections;
using UnityEngine;

// Token: 0x020000B9 RID: 185
public class FlickerLight : MonoBehaviour
{
	// Token: 0x06000A9D RID: 2717 RVA: 0x000405E8 File Offset: 0x0003E7E8
	private void Start()
	{
		this.lamp = base.gameObject;
		this.intens = this.lamp.GetComponent<Light>().intensity;
		this.range = this.lamp.GetComponent<Light>().range;
		this.lamp.GetComponent<Light>().color = this.col_Main;
		base.StartCoroutine(this.Timer());
	}

	// Token: 0x06000A9E RID: 2718 RVA: 0x00040650 File Offset: 0x0003E850
	private void LateUpdate()
	{
		if (this.loopEnd)
		{
			base.StartCoroutine(this.Timer());
		}
		this.intens = Mathf.SmoothStep(this.intens, this.randomIntens, Time.deltaTime * this.intens_Speed);
		this.range = Mathf.SmoothStep(this.range, this.randomRange, Time.deltaTime * this.range_Speed);
		this.lamp.GetComponent<Light>().intensity = this.intens;
		this.lamp.GetComponent<Light>().range = this.range;
		this.col_Main = Color.Lerp(this.col_Main, this.refCol, Time.deltaTime * this.col_Speed);
		this.lamp.GetComponent<Light>().color = this.col_Main;
	}

	// Token: 0x06000A9F RID: 2719 RVA: 0x0004071D File Offset: 0x0003E91D
	private IEnumerator Timer()
	{
		this.timung = false;
		this.randomIntens = Random.Range(this.minIntens, this.maxIntens);
		this.randomRange = Random.Range(this.minRange, this.maxRange);
		this.refCol = Color.Lerp(this.col_Blend1, this.col_Blend2, Random.value);
		yield return new WaitForSeconds(this.lampSpeed);
		this.timung = true;
		this.randomIntens = Random.Range(this.minIntens, this.maxIntens);
		this.randomRange = Random.Range(this.minRange, this.maxRange);
		this.refCol = Color.Lerp(this.col_Blend1, this.col_Blend2, Random.value);
		yield return new WaitForSeconds(this.lampSpeed);
		this.loopEnd = true;
		this.randomIntens = Random.Range(this.minIntens, this.maxIntens);
		this.randomRange = Random.Range(this.minRange, this.maxRange);
		this.refCol = Color.Lerp(this.col_Blend1, this.col_Blend2, Random.value);
		yield return null;
		yield break;
	}

	// Token: 0x04000692 RID: 1682
	public float lampSpeed = 0.1f;

	// Token: 0x04000693 RID: 1683
	public float intens_Speed = 9f;

	// Token: 0x04000694 RID: 1684
	public bool timung;

	// Token: 0x04000695 RID: 1685
	public float minIntens = 0.8f;

	// Token: 0x04000696 RID: 1686
	public float maxIntens = 3.5f;

	// Token: 0x04000697 RID: 1687
	public bool loopEnd;

	// Token: 0x04000698 RID: 1688
	public float range_Speed = 12f;

	// Token: 0x04000699 RID: 1689
	public float minRange = 2.8f;

	// Token: 0x0400069A RID: 1690
	public float maxRange = 13.5f;

	// Token: 0x0400069B RID: 1691
	public Color col_Main = Color.white;

	// Token: 0x0400069C RID: 1692
	public float col_Speed = 1.5f;

	// Token: 0x0400069D RID: 1693
	public Color col_Blend1 = Color.yellow;

	// Token: 0x0400069E RID: 1694
	public Color col_Blend2 = Color.red;

	// Token: 0x0400069F RID: 1695
	private Color refCol;

	// Token: 0x040006A0 RID: 1696
	private float intens;

	// Token: 0x040006A1 RID: 1697
	private float randomIntens;

	// Token: 0x040006A2 RID: 1698
	private float range;

	// Token: 0x040006A3 RID: 1699
	private float randomRange;

	// Token: 0x040006A4 RID: 1700
	private GameObject lamp;
}
