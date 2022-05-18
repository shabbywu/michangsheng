using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020003C9 RID: 969
public class CyChildSelect : MonoBehaviour
{
	// Token: 0x06001AC6 RID: 6854 RVA: 0x00016B95 File Offset: 0x00014D95
	public void Init(string msg)
	{
		this.Content.text = msg;
		base.gameObject.SetActive(true);
	}

	// Token: 0x04001640 RID: 5696
	[SerializeField]
	private Text Content;

	// Token: 0x04001641 RID: 5697
	public FpBtn Btn;

	// Token: 0x04001642 RID: 5698
	public GameObject Line;
}
