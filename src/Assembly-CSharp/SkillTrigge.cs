using System;
using KBEngine;
using UnityEngine;

// Token: 0x02000168 RID: 360
public class SkillTrigge : MonoBehaviour
{
	// Token: 0x06000F7A RID: 3962 RVA: 0x0005D1F4 File Offset: 0x0005B3F4
	private void Start()
	{
		this.renderEntity = (GameObject)this.caster.renderObj;
	}

	// Token: 0x06000F7B RID: 3963 RVA: 0x00004095 File Offset: 0x00002295
	private void Update()
	{
	}

	// Token: 0x06000F7C RID: 3964 RVA: 0x0005D20C File Offset: 0x0005B40C
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

	// Token: 0x06000F7D RID: 3965 RVA: 0x0005D29D File Offset: 0x0005B49D
	private void OnTriggerExit(Collider collider)
	{
		Debug.Log("接触结束");
	}

	// Token: 0x06000F7E RID: 3966 RVA: 0x0005D2A9 File Offset: 0x0005B4A9
	private void OnTriggerStay(Collider collider)
	{
		Debug.Log("接触持续中");
	}

	// Token: 0x04000BA0 RID: 2976
	public int skillID;

	// Token: 0x04000BA1 RID: 2977
	public Entity caster;

	// Token: 0x04000BA2 RID: 2978
	private GameObject renderEntity;
}
