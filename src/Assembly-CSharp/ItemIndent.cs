using System;
using UnityEngine;

// Token: 0x020004B3 RID: 1203
public class ItemIndent : MonoBehaviour
{
	// Token: 0x06002616 RID: 9750 RVA: 0x00107C5C File Offset: 0x00105E5C
	private void Start()
	{
		this.parentScript = base.transform.parent.parent.GetComponent<VerticalScroll>();
		this.myTransform = base.transform;
		this.centerOfScreen = Camera.main.ViewportToWorldPoint(Vector3.one / 2f).y;
		this.startPosX = this.myTransform.position.x;
		Debug.Log("CenterOfScreen:" + this.centerOfScreen);
	}

	// Token: 0x06002617 RID: 9751 RVA: 0x00107CE4 File Offset: 0x00105EE4
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

	// Token: 0x04001ECD RID: 7885
	private VerticalScroll parentScript;

	// Token: 0x04001ECE RID: 7886
	private Transform myTransform;

	// Token: 0x04001ECF RID: 7887
	private float centerOfScreen;

	// Token: 0x04001ED0 RID: 7888
	private float startPosX;

	// Token: 0x04001ED1 RID: 7889
	private bool regulate;
}
