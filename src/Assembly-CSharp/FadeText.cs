using System;
using System.Collections;
using UnityEngine;

// Token: 0x020004AD RID: 1197
[RequireComponent(typeof(TextMesh))]
public class FadeText : MonoBehaviour
{
	// Token: 0x060025EC RID: 9708 RVA: 0x00106C11 File Offset: 0x00104E11
	private void Start()
	{
		this.tm = base.GetComponent<TextMesh>();
	}

	// Token: 0x060025ED RID: 9709 RVA: 0x00106C1F File Offset: 0x00104E1F
	private void Update()
	{
		if (Input.GetKeyUp(113))
		{
			base.StartCoroutine(this.FadeOut(0.005f));
		}
	}

	// Token: 0x060025EE RID: 9710 RVA: 0x00106C3C File Offset: 0x00104E3C
	private IEnumerator FadeOut(float step)
	{
		float t = 0f;
		while (t < 1f)
		{
			this.tm.GetComponent<Renderer>().material.color = new Color(this.tm.GetComponent<Renderer>().material.color.r, this.tm.GetComponent<Renderer>().material.color.g, this.tm.GetComponent<Renderer>().material.color.b, Mathf.Lerp(1f, 0f, t));
			t += step;
			yield return null;
		}
		yield break;
	}

	// Token: 0x04001EBC RID: 7868
	private TextMesh tm;
}
