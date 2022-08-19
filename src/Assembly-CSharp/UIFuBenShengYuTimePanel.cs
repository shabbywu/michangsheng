using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002C1 RID: 705
public class UIFuBenShengYuTimePanel : MonoBehaviour
{
	// Token: 0x060018B6 RID: 6326 RVA: 0x000B181E File Offset: 0x000AFA1E
	private void Awake()
	{
		UIFuBenShengYuTimePanel.Inst = this;
	}

	// Token: 0x040013CB RID: 5067
	public static UIFuBenShengYuTimePanel Inst;

	// Token: 0x040013CC RID: 5068
	public GameObject ScaleObj;

	// Token: 0x040013CD RID: 5069
	public Text TimeText;
}
