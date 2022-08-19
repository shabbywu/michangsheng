using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000299 RID: 665
public class CyChildSelect : MonoBehaviour
{
	// Token: 0x060017E6 RID: 6118 RVA: 0x000A5ECE File Offset: 0x000A40CE
	public void Init(string msg)
	{
		this.Content.text = msg;
		base.gameObject.SetActive(true);
	}

	// Token: 0x040012B7 RID: 4791
	[SerializeField]
	private Text Content;

	// Token: 0x040012B8 RID: 4792
	public FpBtn Btn;

	// Token: 0x040012B9 RID: 4793
	public GameObject Line;
}
