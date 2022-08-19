using System;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;

// Token: 0x0200044F RID: 1103
public class BaseAddScript : MonoBehaviour
{
	// Token: 0x060022D6 RID: 8918 RVA: 0x000EE204 File Offset: 0x000EC404
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

	// Token: 0x060022D7 RID: 8919 RVA: 0x000EE2B8 File Offset: 0x000EC4B8
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

	// Token: 0x060022D8 RID: 8920 RVA: 0x00004095 File Offset: 0x00002295
	public virtual void setBuff()
	{
	}

	// Token: 0x060022D9 RID: 8921 RVA: 0x000EE380 File Offset: 0x000EC580
	public void displayBuff(int buffid)
	{
		string str = jsonData.instance.BuffJsonData[string.Concat(buffid)]["skillEffect"].str;
		if (str != "")
		{
			Vector3 position = base.transform.position;
			Object.Destroy(Object.Instantiate(ResManager.inst.LoadSkillEffect(str), position, Quaternion.identity), jsonData.instance.BuffJsonData[string.Concat(buffid)]["totaltime"].n);
		}
	}

	// Token: 0x060022DA RID: 8922 RVA: 0x000EE414 File Offset: 0x000EC614
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

	// Token: 0x060022DB RID: 8923 RVA: 0x000EE62C File Offset: 0x000EC82C
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

	// Token: 0x04001C0C RID: 7180
	public int nowRoleType;

	// Token: 0x04001C0D RID: 7181
	public int nowRoleFace;

	// Token: 0x04001C0E RID: 7182
	public Entity entity;
}
