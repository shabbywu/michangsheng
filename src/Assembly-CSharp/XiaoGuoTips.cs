using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000302 RID: 770
public class XiaoGuoTips : MonoBehaviour
{
	// Token: 0x06001ACF RID: 6863 RVA: 0x0005FDE2 File Offset: 0x0005DFE2
	public void show()
	{
		base.gameObject.SetActive(true);
	}

	// Token: 0x06001AD0 RID: 6864 RVA: 0x000BF370 File Offset: 0x000BD570
	public void setPosition(float y)
	{
		float x = base.gameObject.transform.position.x;
		float z = base.gameObject.transform.position.z;
		base.gameObject.transform.position = new Vector3(x, y, z);
	}

	// Token: 0x06001AD1 RID: 6865 RVA: 0x000B5E62 File Offset: 0x000B4062
	public void close()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001AD2 RID: 6866 RVA: 0x000BF3C1 File Offset: 0x000BD5C1
	public void setContent(string str)
	{
		this.content.text = Tools.Code64(str);
	}

	// Token: 0x0400158C RID: 5516
	[SerializeField]
	private Text content;
}
