using System;
using UnityEngine;

// Token: 0x02000163 RID: 355
public class PotionHealing : MonoBehaviour
{
	// Token: 0x06000C66 RID: 3174 RVA: 0x0009765C File Offset: 0x0009585C
	private void Start()
	{
		if (base.gameObject.transform.parent && base.gameObject.transform.parent.gameObject.GetComponent<CharacterStatus>())
		{
			base.gameObject.transform.parent.gameObject.GetComponent<CharacterStatus>().HP += this.HPheal;
		}
		Object.Destroy(base.gameObject, 3f);
	}

	// Token: 0x0400098A RID: 2442
	public int HPheal = 10;
}
