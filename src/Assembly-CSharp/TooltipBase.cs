using System;
using UnityEngine;

// Token: 0x020005DF RID: 1503
public class TooltipBase : MonoBehaviour
{
	// Token: 0x060025C2 RID: 9666 RVA: 0x0001E37C File Offset: 0x0001C57C
	protected virtual void Start()
	{
		this.childTexture = base.transform.GetChild(0).GetComponent<UITexture>();
	}

	// Token: 0x060025C3 RID: 9667 RVA: 0x0001E395 File Offset: 0x0001C595
	protected virtual void Update()
	{
		if (this.shoudSetPos)
		{
			if (this.showTooltip)
			{
				this.MobileSetPosition();
				return;
			}
			base.transform.position = new Vector3(0f, 10000f, 0f);
		}
	}

	// Token: 0x060025C4 RID: 9668 RVA: 0x0012B8DC File Offset: 0x00129ADC
	public virtual void MobileSetPosition()
	{
		if (this.showType == 2)
		{
			this.PCSetPosition();
			return;
		}
		if (this.showType == 3)
		{
			float num = (float)(Screen.height / 2) * 1.24f;
			base.transform.position = UICamera.currentCamera.ScreenToWorldPoint(new Vector3((float)(Screen.width / 2), num, 0f));
			return;
		}
		int num2 = Screen.width / 2;
		float num3 = (Input.mousePosition.x < (float)num2) ? ((float)num2 * 1.35f) : ((float)num2 * 0.52f);
		float num4 = (float)(Screen.height / 2) * 1.24f;
		base.transform.position = UICamera.currentCamera.ScreenToWorldPoint(new Vector3(num3, num4, 0f));
	}

	// Token: 0x060025C5 RID: 9669 RVA: 0x0001E3CD File Offset: 0x0001C5CD
	public Vector3 getMousePosition()
	{
		Vector3 mousePosition = Input.mousePosition;
		return Input.mousePosition;
	}

	// Token: 0x060025C6 RID: 9670 RVA: 0x0012B994 File Offset: 0x00129B94
	public virtual void PCSetPosition()
	{
		Vector3 vector;
		vector..ctor(this.getMousePosition().x, this.getMousePosition().y, this.getMousePosition().z);
		vector.x += (float)(this.childTexture.width / 2);
		vector.y -= (float)(this.childTexture.height / 2);
		if (Input.mousePosition.x > (float)(Screen.width / 2))
		{
			vector.x -= (float)this.childTexture.width;
		}
		if (Input.mousePosition.y < (float)(Screen.height / 2))
		{
			vector.y += (float)this.childTexture.height;
		}
		base.transform.position = UICamera.currentCamera.ScreenToWorldPoint(vector);
	}

	// Token: 0x04002037 RID: 8247
	public bool showTooltip;

	// Token: 0x04002038 RID: 8248
	public UITexture childTexture;

	// Token: 0x04002039 RID: 8249
	public int showType = 1;

	// Token: 0x0400203A RID: 8250
	protected Vector3 NowClickPositon = Vector3.zero;

	// Token: 0x0400203B RID: 8251
	public bool shoudSetPos = true;
}
