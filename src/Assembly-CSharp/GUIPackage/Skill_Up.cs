using System;
using UnityEngine;

namespace GUIPackage
{
	// Token: 0x02000D96 RID: 3478
	public class Skill_Up : MonoBehaviour
	{
		// Token: 0x060053F8 RID: 21496 RVA: 0x0003C00C File Offset: 0x0003A20C
		private void OnPress()
		{
			if (Input.GetMouseButtonDown(0))
			{
				Singleton.skillUI2.SkillUP(base.transform.parent.GetComponent<SkillStaticCell>().skillID);
			}
		}
	}
}
