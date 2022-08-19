using System;
using UnityEngine;

// Token: 0x02000429 RID: 1065
public class TooltipBase : MonoBehaviour
{
	// Token: 0x06002206 RID: 8710 RVA: 0x000EA55C File Offset: 0x000E875C
	protected virtual void Start()
	{
		this.childTexture = base.transform.GetChild(0).GetComponent<UITexture>();
	}

	// Token: 0x06002207 RID: 8711 RVA: 0x000EA575 File Offset: 0x000E8775
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

	// Token: 0x06002208 RID: 8712 RVA: 0x000EA5B0 File Offset: 0x000E87B0
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

	// Token: 0x06002209 RID: 8713 RVA: 0x000EA667 File Offset: 0x000E8867
	public Vector3 getMousePosition()
	{
		Vector3 mousePosition = Input.mousePosition;
		return Input.mousePosition;
	}

	// Token: 0x0600220A RID: 8714 RVA: 0x000EA674 File Offset: 0x000E8874
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

	// Token: 0x04001B6D RID: 7021
	public bool showTooltip;

	// Token: 0x04001B6E RID: 7022
	public UITexture childTexture;

	// Token: 0x04001B6F RID: 7023
	public int showType = 1;

	// Token: 0x04001B70 RID: 7024
	protected Vector3 NowClickPositon = Vector3.zero;

	// Token: 0x04001B71 RID: 7025
	public bool shoudSetPos = true;
}
