using System;
using UnityEngine;

namespace Fungus.Examples
{
	// Token: 0x0200146A RID: 5226
	public class SpriteColorChangeFromFungusMouseEvent : MonoBehaviour
	{
		// Token: 0x06007DEA RID: 32234 RVA: 0x000551E3 File Offset: 0x000533E3
		private void Start()
		{
			this.rend = base.GetComponent<SpriteRenderer>();
		}

		// Token: 0x06007DEB RID: 32235 RVA: 0x000551F1 File Offset: 0x000533F1
		private void OnMouseEventFromFungus()
		{
			this.rend.color = Color.HSVToRGB(Random.value, Random.Range(0.7f, 0.9f), Random.Range(0.7f, 0.9f));
		}

		// Token: 0x04006B5F RID: 27487
		private SpriteRenderer rend;
	}
}
