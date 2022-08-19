using System;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000459 RID: 1113
public class AvatarShowSkill : AvatarShowHpDamage
{
	// Token: 0x060022F9 RID: 8953 RVA: 0x000EED00 File Offset: 0x000ECF00
	public void setText(string _demage, Avatar avatar)
	{
		GameObject gameObject = Object.Instantiate<GameObject>(this.DamageTemp);
		gameObject.transform.SetParent(base.transform);
		gameObject.transform.localPosition = new Vector3(0f, 3.49f, 2.97f);
		gameObject.transform.localScale = new Vector3(0.0007f, 0.0007f, 0.0007f);
		Text componentInChildren = gameObject.GetComponentInChildren<Text>();
		if (!avatar.isPlayer())
		{
			Transform transform = gameObject.GetComponentInChildren<Image>().transform;
			transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
		}
		componentInChildren.text = _demage;
	}
}
