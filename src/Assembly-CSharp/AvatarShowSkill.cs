using KBEngine;
using UnityEngine;
using UnityEngine.UI;

public class AvatarShowSkill : AvatarShowHpDamage
{
	public void setText(string _demage, Avatar avatar)
	{
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		GameObject val = Object.Instantiate<GameObject>(DamageTemp);
		val.transform.SetParent(((Component)this).transform);
		val.transform.localPosition = new Vector3(0f, 3.49f, 2.97f);
		val.transform.localScale = new Vector3(0.0007f, 0.0007f, 0.0007f);
		Text componentInChildren = val.GetComponentInChildren<Text>();
		if (!avatar.isPlayer())
		{
			Transform transform = ((Component)val.GetComponentInChildren<Image>()).transform;
			transform.localScale = new Vector3(0f - transform.localScale.x, transform.localScale.y, transform.localScale.z);
		}
		componentInChildren.text = _demage;
	}
}
