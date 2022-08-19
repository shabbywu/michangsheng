using System;
using UnityEngine;

// Token: 0x02000494 RID: 1172
public class BiljkaEvents : MonoBehaviour
{
	// Token: 0x06002508 RID: 9480 RVA: 0x0010185E File Offset: 0x000FFA5E
	private void OnTriggerEnter2D(Collider2D col)
	{
		col.tag.Equals("Monkey");
	}
}
