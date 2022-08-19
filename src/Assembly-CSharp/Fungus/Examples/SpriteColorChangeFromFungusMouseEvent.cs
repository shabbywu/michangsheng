using System;
using UnityEngine;

namespace Fungus.Examples
{
	// Token: 0x02000FB2 RID: 4018
	public class SpriteColorChangeFromFungusMouseEvent : MonoBehaviour
	{
		// Token: 0x06006FF0 RID: 28656 RVA: 0x002A8A1C File Offset: 0x002A6C1C
		private void Start()
		{
			this.rend = base.GetComponent<SpriteRenderer>();
		}

		// Token: 0x06006FF1 RID: 28657 RVA: 0x002A8A2A File Offset: 0x002A6C2A
		private void OnMouseEventFromFungus()
		{
			this.rend.color = Color.HSVToRGB(Random.value, Random.Range(0.7f, 0.9f), Random.Range(0.7f, 0.9f));
		}

		// Token: 0x04005C67 RID: 23655
		private SpriteRenderer rend;
	}
}
