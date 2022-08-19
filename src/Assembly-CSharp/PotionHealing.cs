using System;
using UnityEngine;

// Token: 0x020000EC RID: 236
public class PotionHealing : MonoBehaviour
{
	// Token: 0x06000B77 RID: 2935 RVA: 0x00045B6C File Offset: 0x00043D6C
	private void Start()
	{
		if (base.gameObject.transform.parent && base.gameObject.transform.parent.gameObject.GetComponent<CharacterStatus>())
		{
			base.gameObject.transform.parent.gameObject.GetComponent<CharacterStatus>().HP += this.HPheal;
		}
		Object.Destroy(base.gameObject, 3f);
	}

	// Token: 0x040007AF RID: 1967
	public int HPheal = 10;
}
