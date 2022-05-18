using System;
using UnityEngine;
using UnityEngine.UI;

namespace EIU
{
	// Token: 0x02000E9D RID: 3741
	public class EIU_AxisButton : MonoBehaviour
	{
		// Token: 0x060059C0 RID: 22976 RVA: 0x0003FB0C File Offset: 0x0003DD0C
		public void init(string axisName, string buttonDescription, string key, bool nKey = false)
		{
			this.axisName = axisName;
			this.axisName_text.text = buttonDescription;
			this.keyName_text.text = key;
			this.negativeKey = nKey;
		}

		// Token: 0x060059C1 RID: 22977 RVA: 0x0003FB35 File Offset: 0x0003DD35
		public void ChangeKeyText(string key)
		{
			this.keyName_text.text = key;
		}

		// Token: 0x060059C2 RID: 22978 RVA: 0x0003FB43 File Offset: 0x0003DD43
		public void RebindAxis()
		{
			EIU_ControlsMenu.Instance().OpenRebindButtonDialog(this.axisName, this.negativeKey);
		}

		// Token: 0x060059C3 RID: 22979 RVA: 0x0000E9B5 File Offset: 0x0000CBB5
		public void HoverClip()
		{
			EasyAudioUtility.instance.Play("Hover");
		}

		// Token: 0x0400591B RID: 22811
		[Header("Child Text Objects")]
		public Text axisName_text;

		// Token: 0x0400591C RID: 22812
		public Text keyName_text;

		// Token: 0x0400591D RID: 22813
		[Space(10f)]
		[Header("Axis Button Info")]
		public string axisName;

		// Token: 0x0400591E RID: 22814
		public bool negativeKey;
	}
}
