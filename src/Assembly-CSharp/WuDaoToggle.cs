using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020003A3 RID: 931
public class WuDaoToggle : MonoBehaviour
{
	// Token: 0x06001E5D RID: 7773 RVA: 0x000D5D38 File Offset: 0x000D3F38
	private void Start()
	{
		this.toggle = base.GetComponent<Toggle>();
	}

	// Token: 0x06001E5E RID: 7774 RVA: 0x000D5D48 File Offset: 0x000D3F48
	public void OnClick()
	{
		if (this.toggle.isOn)
		{
			this.wuDaoname.color = new Color(255f, 252f, 167f);
			this.iconType.color = new Color(255f, 252f, 167f);
			return;
		}
		this.wuDaoname.color = new Color(173f, 87f, 35f);
		this.iconType.color = new Color(189f, 101f, 33f);
	}

	// Token: 0x06001E5F RID: 7775 RVA: 0x000D5DE0 File Offset: 0x000D3FE0
	private void Update()
	{
		if (this.toggle != null)
		{
			if (this.toggle.isOn)
			{
				this.wuDaoname.color = new Color(1f, 0.9882353f, 0.654902f);
				this.iconType.color = new Color(1f, 0.9882353f, 0.654902f);
				return;
			}
			this.wuDaoname.color = new Color(0.6784314f, 0.34117648f, 0.13725491f);
			this.iconType.color = new Color(0.7411765f, 0.39607844f, 0.12941177f);
		}
	}

	// Token: 0x040018E5 RID: 6373
	private Toggle toggle;

	// Token: 0x040018E6 RID: 6374
	public Text wuDaoname;

	// Token: 0x040018E7 RID: 6375
	public Image iconType;
}
