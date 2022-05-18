using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200045F RID: 1119
public class XiaoGuoTips : MonoBehaviour
{
	// Token: 0x06001DF5 RID: 7669 RVA: 0x00011B82 File Offset: 0x0000FD82
	public void show()
	{
		base.gameObject.SetActive(true);
	}

	// Token: 0x06001DF6 RID: 7670 RVA: 0x00104FCC File Offset: 0x001031CC
	public void setPosition(float y)
	{
		float x = base.gameObject.transform.position.x;
		float z = base.gameObject.transform.position.z;
		base.gameObject.transform.position = new Vector3(x, y, z);
	}

	// Token: 0x06001DF7 RID: 7671 RVA: 0x00017C2D File Offset: 0x00015E2D
	public void close()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001DF8 RID: 7672 RVA: 0x00018E85 File Offset: 0x00017085
	public void setContent(string str)
	{
		this.content.text = Tools.Code64(str);
	}

	// Token: 0x04001999 RID: 6553
	[SerializeField]
	private Text content;
}
