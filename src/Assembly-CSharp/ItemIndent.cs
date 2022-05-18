using System;
using UnityEngine;

// Token: 0x020006A7 RID: 1703
public class ItemIndent : MonoBehaviour
{
	// Token: 0x06002A94 RID: 10900 RVA: 0x00147FDC File Offset: 0x001461DC
	private void Start()
	{
		this.parentScript = base.transform.parent.parent.GetComponent<VerticalScroll>();
		this.myTransform = base.transform;
		this.centerOfScreen = Camera.main.ViewportToWorldPoint(Vector3.one / 2f).y;
		this.startPosX = this.myTransform.position.x;
		Debug.Log("CenterOfScreen:" + this.centerOfScreen);
	}

	// Token: 0x06002A95 RID: 10901 RVA: 0x00148064 File Offset: 0x00146264
	private void Update()
	{
		if (this.parentScript.canScroll)
		{
			if (this.myTransform.position.y >= this.centerOfScreen - 2.5f && this.myTransform.position.y <= this.centerOfScreen + 2.5f)
			{
				this.myTransform.position = new Vector3(Mathf.Clamp(Mathf.MoveTowards(this.myTransform.position.x, this.startPosX + 0.5f - this.myTransform.position.y, 1f), this.startPosX, this.startPosX + 0.5f), this.myTransform.position.y, this.myTransform.position.z);
				return;
			}
			this.myTransform.position = new Vector3(Mathf.Clamp(Mathf.MoveTowards(this.myTransform.position.x, this.startPosX, 1f), this.startPosX, this.startPosX + 0.5f), this.myTransform.position.y, this.myTransform.position.z);
		}
	}

	// Token: 0x04002440 RID: 9280
	private VerticalScroll parentScript;

	// Token: 0x04002441 RID: 9281
	private Transform myTransform;

	// Token: 0x04002442 RID: 9282
	private float centerOfScreen;

	// Token: 0x04002443 RID: 9283
	private float startPosX;

	// Token: 0x04002444 RID: 9284
	private bool regulate;
}
