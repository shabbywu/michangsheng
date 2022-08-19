using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020003F2 RID: 1010
public class ToggelScaleUI : MonoBehaviour
{
	// Token: 0x060020A1 RID: 8353 RVA: 0x00004095 File Offset: 0x00002295
	private void Start()
	{
	}

	// Token: 0x060020A2 RID: 8354 RVA: 0x000E5F00 File Offset: 0x000E4100
	public void setison()
	{
		if (base.GetComponent<Toggle>().isOn)
		{
			iTween.ScaleTo(base.gameObject, iTween.Hash(new object[]
			{
				"x",
				this.ison,
				"y",
				this.ison,
				"z",
				this.ison,
				"time",
				0.3f,
				"islocal",
				true
			}));
			return;
		}
		iTween.ScaleTo(base.gameObject, iTween.Hash(new object[]
		{
			"x",
			this.nomel,
			"y",
			this.nomel,
			"z",
			this.nomel,
			"time",
			0.3f,
			"islocal",
			true
		}));
	}

	// Token: 0x060020A3 RID: 8355 RVA: 0x00004095 File Offset: 0x00002295
	private void Update()
	{
	}

	// Token: 0x04001A88 RID: 6792
	public float nomel = 1f;

	// Token: 0x04001A89 RID: 6793
	public float ison = 1f;
}
