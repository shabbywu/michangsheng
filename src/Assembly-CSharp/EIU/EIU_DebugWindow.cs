using System;
using System.Collections.Generic;
using UnityEngine;

namespace EIU
{
	// Token: 0x02000B2C RID: 2860
	public class EIU_DebugWindow : MonoBehaviour
	{
		// Token: 0x06004FBC RID: 20412 RVA: 0x0021AA8D File Offset: 0x00218C8D
		private void Start()
		{
			if (this.debug && EasyInputUtility.instance)
			{
				this.populateDebugWindow();
			}
			if (EasyInputUtility.instance == null)
			{
				base.gameObject.SetActive(false);
			}
		}

		// Token: 0x06004FBD RID: 20413 RVA: 0x0021AAC4 File Offset: 0x00218CC4
		private void populateDebugWindow()
		{
			this.Axes = EasyInputUtility.instance.Axes;
			foreach (EIU_AxisBase eiu_AxisBase in this.Axes)
			{
				GameObject gameObject = Object.Instantiate<GameObject>(this.DebugItemPrefab);
				gameObject.name = "positiveAxis";
				gameObject.transform.SetParent(this.DebugWindow);
				gameObject.transform.localScale = Vector3.one;
				gameObject.GetComponent<EIU_DebugItem>().Init(eiu_AxisBase.pKeyDescription, eiu_AxisBase.positiveKey.ToString());
				if (eiu_AxisBase.nKeyDescription != "")
				{
					GameObject gameObject2 = Object.Instantiate<GameObject>(this.DebugItemPrefab);
					gameObject2.name = "negativeAxis";
					gameObject2.transform.SetParent(this.DebugWindow);
					gameObject2.transform.localScale = Vector3.one;
					gameObject2.GetComponent<EIU_DebugItem>().Init(eiu_AxisBase.nKeyDescription, eiu_AxisBase.negativeKey.ToString());
				}
			}
		}

		// Token: 0x04004EB4 RID: 20148
		public bool debug = true;

		// Token: 0x04004EB5 RID: 20149
		public List<EIU_AxisBase> Axes = new List<EIU_AxisBase>();

		// Token: 0x04004EB6 RID: 20150
		[Header("UI References")]
		public Transform DebugWindow;

		// Token: 0x04004EB7 RID: 20151
		public GameObject DebugItemPrefab;
	}
}
