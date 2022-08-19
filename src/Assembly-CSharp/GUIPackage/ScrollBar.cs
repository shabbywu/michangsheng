using System;
using UnityEngine;

namespace GUIPackage
{
	// Token: 0x02000A66 RID: 2662
	public class ScrollBar : MonoBehaviour
	{
		// Token: 0x06004AC3 RID: 19139 RVA: 0x001FC7AA File Offset: 0x001FA9AA
		private void OnScroll(float delta)
		{
			MonoBehaviour.print("sb");
			this.scrollView.Scroll(delta);
			base.transform.parent.parent.GetComponentInChildren<UIScrollBar>().value -= delta;
		}

		// Token: 0x040049E1 RID: 18913
		public UIScrollView scrollView;
	}
}
