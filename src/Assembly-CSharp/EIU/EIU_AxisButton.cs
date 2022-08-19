using System;
using UnityEngine;
using UnityEngine.UI;

namespace EIU
{
	// Token: 0x02000B29 RID: 2857
	public class EIU_AxisButton : MonoBehaviour
	{
		// Token: 0x06004FA1 RID: 20385 RVA: 0x0021A1CA File Offset: 0x002183CA
		public void init(string axisName, string buttonDescription, string key, bool nKey = false)
		{
			this.axisName = axisName;
			this.axisName_text.text = buttonDescription;
			this.keyName_text.text = key;
			this.negativeKey = nKey;
		}

		// Token: 0x06004FA2 RID: 20386 RVA: 0x0021A1F3 File Offset: 0x002183F3
		public void ChangeKeyText(string key)
		{
			this.keyName_text.text = key;
		}

		// Token: 0x06004FA3 RID: 20387 RVA: 0x0021A201 File Offset: 0x00218401
		public void RebindAxis()
		{
			EIU_ControlsMenu.Instance().OpenRebindButtonDialog(this.axisName, this.negativeKey);
		}

		// Token: 0x06004FA4 RID: 20388 RVA: 0x00047A15 File Offset: 0x00045C15
		public void HoverClip()
		{
			EasyAudioUtility.instance.Play("Hover");
		}

		// Token: 0x04004EA4 RID: 20132
		[Header("Child Text Objects")]
		public Text axisName_text;

		// Token: 0x04004EA5 RID: 20133
		public Text keyName_text;

		// Token: 0x04004EA6 RID: 20134
		[Space(10f)]
		[Header("Axis Button Info")]
		public string axisName;

		// Token: 0x04004EA7 RID: 20135
		public bool negativeKey;
	}
}
