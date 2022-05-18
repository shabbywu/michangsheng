using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200069F RID: 1695
[RequireComponent(typeof(TextMesh))]
public class FadeText : MonoBehaviour
{
	// Token: 0x06002A5E RID: 10846 RVA: 0x00020F0D File Offset: 0x0001F10D
	private void Start()
	{
		this.tm = base.GetComponent<TextMesh>();
	}

	// Token: 0x06002A5F RID: 10847 RVA: 0x00020F1B File Offset: 0x0001F11B
	private void Update()
	{
		if (Input.GetKeyUp(113))
		{
			base.StartCoroutine(this.FadeOut(0.005f));
		}
	}

	// Token: 0x06002A60 RID: 10848 RVA: 0x00020F38 File Offset: 0x0001F138
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

	// Token: 0x04002426 RID: 9254
	private TextMesh tm;
}
