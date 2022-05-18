using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000128 RID: 296
public class FlickerLight : MonoBehaviour
{
	// Token: 0x06000B7A RID: 2938 RVA: 0x0009291C File Offset: 0x00090B1C
	private void Start()
	{
		this.lamp = base.gameObject;
		this.intens = this.lamp.GetComponent<Light>().intensity;
		this.range = this.lamp.GetComponent<Light>().range;
		this.lamp.GetComponent<Light>().color = this.col_Main;
		base.StartCoroutine(this.Timer());
	}

	// Token: 0x06000B7B RID: 2939 RVA: 0x00092984 File Offset: 0x00090B84
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

	// Token: 0x06000B7C RID: 2940 RVA: 0x0000D8A0 File Offset: 0x0000BAA0
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

	// Token: 0x04000836 RID: 2102
	public float lampSpeed = 0.1f;

	// Token: 0x04000837 RID: 2103
	public float intens_Speed = 9f;

	// Token: 0x04000838 RID: 2104
	public bool timung;

	// Token: 0x04000839 RID: 2105
	public float minIntens = 0.8f;

	// Token: 0x0400083A RID: 2106
	public float maxIntens = 3.5f;

	// Token: 0x0400083B RID: 2107
	public bool loopEnd;

	// Token: 0x0400083C RID: 2108
	public float range_Speed = 12f;

	// Token: 0x0400083D RID: 2109
	public float minRange = 2.8f;

	// Token: 0x0400083E RID: 2110
	public float maxRange = 13.5f;

	// Token: 0x0400083F RID: 2111
	public Color col_Main = Color.white;

	// Token: 0x04000840 RID: 2112
	public float col_Speed = 1.5f;

	// Token: 0x04000841 RID: 2113
	public Color col_Blend1 = Color.yellow;

	// Token: 0x04000842 RID: 2114
	public Color col_Blend2 = Color.red;

	// Token: 0x04000843 RID: 2115
	private Color refCol;

	// Token: 0x04000844 RID: 2116
	private float intens;

	// Token: 0x04000845 RID: 2117
	private float randomIntens;

	// Token: 0x04000846 RID: 2118
	private float range;

	// Token: 0x04000847 RID: 2119
	private float randomRange;

	// Token: 0x04000848 RID: 2120
	private GameObject lamp;
}
