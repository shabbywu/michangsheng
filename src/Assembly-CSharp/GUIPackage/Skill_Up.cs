using System;
using UnityEngine;

namespace GUIPackage
{
	// Token: 0x02000A6A RID: 2666
	public class Skill_Up : MonoBehaviour
	{
		// Token: 0x06004AEE RID: 19182 RVA: 0x001FE42D File Offset: 0x001FC62D
		private void OnPress()
		{
			if (Input.GetMouseButtonDown(0))
			{
				Singleton.skillUI2.SkillUP(base.transform.parent.GetComponent<SkillStaticCell>().skillID);
			}
		}
	}
}
