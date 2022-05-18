using System;
using UnityEngine;

// Token: 0x0200066D RID: 1645
public class BiljkaEvents : MonoBehaviour
{
	// Token: 0x0600291E RID: 10526 RVA: 0x0001FF75 File Offset: 0x0001E175
	private void OnTriggerEnter2D(Collider2D col)
	{
		col.tag.Equals("Monkey");
	}
}
