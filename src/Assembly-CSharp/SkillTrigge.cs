using System;
using KBEngine;
using UnityEngine;

// Token: 0x02000244 RID: 580
public class SkillTrigge : MonoBehaviour
{
	// Token: 0x060011D8 RID: 4568 RVA: 0x0001128B File Offset: 0x0000F48B
	private void Start()
	{
		this.renderEntity = (GameObject)this.caster.renderObj;
	}

	// Token: 0x060011D9 RID: 4569 RVA: 0x000042DD File Offset: 0x000024DD
	private void Update()
	{
	}

	// Token: 0x060011DA RID: 4570 RVA: 0x000ACDE8 File Offset: 0x000AAFE8
	private void OnTriggerEnter(Collider collider)
	{
		Debug.Log("开始接触");
		if (KBEngineApp.app.player() == this.caster)
		{
			Avatar avatar = (Avatar)KBEngineApp.app.player();
			GameEntity component = collider.GetComponent<Collider>().GetComponent<GameEntity>();
			if (avatar != null && component && component.canAttack)
			{
				int postInt = Utility.getPostInt(collider.GetComponent<Collider>().name);
				avatar.cellCall("SkillDamage", new object[]
				{
					this.skillID,
					postInt
				});
			}
		}
	}

	// Token: 0x060011DB RID: 4571 RVA: 0x000112A3 File Offset: 0x0000F4A3
	private void OnTriggerExit(Collider collider)
	{
		Debug.Log("接触结束");
	}

	// Token: 0x060011DC RID: 4572 RVA: 0x000112AF File Offset: 0x0000F4AF
	private void OnTriggerStay(Collider collider)
	{
		Debug.Log("接触持续中");
	}

	// Token: 0x04000E70 RID: 3696
	public int skillID;

	// Token: 0x04000E71 RID: 3697
	public Entity caster;

	// Token: 0x04000E72 RID: 3698
	private GameObject renderEntity;
}
