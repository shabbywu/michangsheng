using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020005A1 RID: 1441
public class ToggelScaleUI : MonoBehaviour
{
	// Token: 0x06002453 RID: 9299 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x06002454 RID: 9300 RVA: 0x001281D4 File Offset: 0x001263D4
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

	// Token: 0x06002455 RID: 9301 RVA: 0x000042DD File Offset: 0x000024DD
	private void Update()
	{
	}

	// Token: 0x04001F41 RID: 8001
	public float nomel = 1f;

	// Token: 0x04001F42 RID: 8002
	public float ison = 1f;
}
