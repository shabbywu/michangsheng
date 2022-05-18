using System;
using System.Collections.Generic;
using UnityEngine;

namespace EIU
{
	// Token: 0x02000EA0 RID: 3744
	public class EIU_DebugWindow : MonoBehaviour
	{
		// Token: 0x060059DB RID: 23003 RVA: 0x0003FC0D File Offset: 0x0003DE0D
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

		// Token: 0x060059DC RID: 23004 RVA: 0x0024A71C File Offset: 0x0024891C
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

		// Token: 0x0400592B RID: 22827
		public bool debug = true;

		// Token: 0x0400592C RID: 22828
		public List<EIU_AxisBase> Axes = new List<EIU_AxisBase>();

		// Token: 0x0400592D RID: 22829
		[Header("UI References")]
		public Transform DebugWindow;

		// Token: 0x0400592E RID: 22830
		public GameObject DebugItemPrefab;
	}
}
