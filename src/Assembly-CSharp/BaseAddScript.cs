using System;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;

// Token: 0x02000608 RID: 1544
public class BaseAddScript : MonoBehaviour
{
	// Token: 0x0600268D RID: 9869 RVA: 0x0012EA38 File Offset: 0x0012CC38
	public void Start()
	{
		if (base.gameObject.GetComponent<CharacterController>() == null)
		{
			base.gameObject.AddComponent<CharacterController>();
		}
		base.gameObject.GetComponent<CharacterController>().height = base.gameObject.transform.GetChild(0).GetComponent<CharacterController>().height;
		float y = base.gameObject.transform.GetChild(0).GetComponent<CharacterController>().center.y;
		base.gameObject.GetComponent<CharacterController>().center = new Vector3(0f, y, 0f);
		Object.Destroy(base.gameObject.transform.GetChild(0).GetComponent<CharacterController>());
	}

	// Token: 0x0600268E RID: 9870 RVA: 0x0012EAEC File Offset: 0x0012CCEC
	public void resetRotation()
	{
		Transform child = base.transform.GetChild(0);
		if (Math.Abs(child.localRotation.eulerAngles.y) > 30f)
		{
			child.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
		}
		if ((double)Math.Abs(child.localPosition.x) > 0.4 || (double)Math.Abs(child.localPosition.y) > 0.3 || (double)Math.Abs(child.localPosition.z) > 0.4)
		{
			child.localPosition = new Vector3(0f, 0f, 0f);
		}
	}

	// Token: 0x0600268F RID: 9871 RVA: 0x000042DD File Offset: 0x000024DD
	public virtual void setBuff()
	{
	}

	// Token: 0x06002690 RID: 9872 RVA: 0x0012EBB4 File Offset: 0x0012CDB4
	public void displayBuff(int buffid)
	{
		string str = jsonData.instance.BuffJsonData[string.Concat(buffid)]["skillEffect"].str;
		if (str != "")
		{
			Vector3 position = base.transform.position;
			Object.Destroy(Object.Instantiate(ResManager.inst.LoadSkillEffect(str), position, Quaternion.identity), jsonData.instance.BuffJsonData[string.Concat(buffid)]["totaltime"].n);
		}
	}

	// Token: 0x06002691 RID: 9873 RVA: 0x0012EC48 File Offset: 0x0012CE48
	private void Update()
	{
		int num = (int)((uint)this.entity.getDefinedProperty("roleTypeCell"));
		int num2 = (int)((ushort)this.entity.getDefinedProperty("roleSurfaceCall"));
		if (this.nowRoleType != num || this.nowRoleFace != num2)
		{
			this.nowRoleType = num;
			this.nowRoleFace = num2;
			float y = this.entity.position.y;
			GameObject gameObject = (GameObject)Resources.Load(string.Concat(new object[]
			{
				"Effect/Prefab/gameEntity/Avater/Avater",
				num,
				"/Avater",
				num,
				"_",
				num2
			}));
			Object.Destroy(((GameObject)this.entity.renderObj).transform.GetChild(0).gameObject);
			GameObject gameObject2 = Object.Instantiate<GameObject>(gameObject, new Vector3(this.entity.position.x, y, this.entity.position.z), Quaternion.Euler(new Vector3(this.entity.direction.y, this.entity.direction.z, this.entity.direction.x)));
			gameObject2.transform.parent = ((GameObject)this.entity.renderObj).transform;
			gameObject2.transform.SetSiblingIndex(0);
			CharacterController component = gameObject2.GetComponent<CharacterController>();
			if (component != null)
			{
				base.gameObject.GetComponent<CharacterController>().height = component.height;
				float y2 = component.center.y;
				base.gameObject.GetComponent<CharacterController>().center = new Vector3(0f, y2, 0f);
				Object.Destroy(component);
			}
			if (this.entity.className == "Avatar")
			{
				Avatar avatar = (Avatar)KBEngineApp.app.player();
				if (this.entity.id == avatar.id)
				{
					this.changeCanAttak();
					return;
				}
				Tools.instance.setAvaterCanAttack(this.entity);
			}
		}
	}

	// Token: 0x06002692 RID: 9874 RVA: 0x0012EE60 File Offset: 0x0012D060
	public void changeCanAttak()
	{
		foreach (KeyValuePair<int, Entity> keyValuePair in KBEngineApp.app.entities)
		{
			if (keyValuePair.Value.className == "Avatar")
			{
				Tools.instance.setAvaterCanAttack(keyValuePair.Value);
			}
		}
	}

	// Token: 0x040020DC RID: 8412
	public int nowRoleType;

	// Token: 0x040020DD RID: 8413
	public int nowRoleFace;

	// Token: 0x040020DE RID: 8414
	public Entity entity;
}
