using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200052C RID: 1324
public class WuDaoToggle : MonoBehaviour
{
	// Token: 0x060021DE RID: 8670 RVA: 0x0001BCD3 File Offset: 0x00019ED3
	private void Start()
	{
		this.toggle = base.GetComponent<Toggle>();
	}

	// Token: 0x060021DF RID: 8671 RVA: 0x00119580 File Offset: 0x00117780
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

	// Token: 0x060021E0 RID: 8672 RVA: 0x00119618 File Offset: 0x00117818
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

	// Token: 0x04001D4E RID: 7502
	private Toggle toggle;

	// Token: 0x04001D4F RID: 7503
	public Text wuDaoname;

	// Token: 0x04001D50 RID: 7504
	public Image iconType;
}
