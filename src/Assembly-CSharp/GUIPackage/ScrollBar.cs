using System;
using UnityEngine;

namespace GUIPackage
{
	// Token: 0x02000D92 RID: 3474
	public class ScrollBar : MonoBehaviour
	{
		// Token: 0x060053CD RID: 21453 RVA: 0x0003BEAF File Offset: 0x0003A0AF
		private void OnScroll(float delta)
		{
			MonoBehaviour.print("sb");
			this.scrollView.Scroll(delta);
			base.transform.parent.parent.GetComponentInChildren<UIScrollBar>().value -= delta;
		}

		// Token: 0x0400537E RID: 21374
		public UIScrollView scrollView;
	}
}
