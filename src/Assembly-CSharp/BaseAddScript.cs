using System;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;

public class BaseAddScript : MonoBehaviour
{
	public int nowRoleType;

	public int nowRoleFace;

	public Entity entity;

	public void Start()
	{
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)((Component)this).gameObject.GetComponent<CharacterController>() == (Object)null)
		{
			((Component)this).gameObject.AddComponent<CharacterController>();
		}
		((Component)this).gameObject.GetComponent<CharacterController>().height = ((Component)((Component)this).gameObject.transform.GetChild(0)).GetComponent<CharacterController>().height;
		float y = ((Component)((Component)this).gameObject.transform.GetChild(0)).GetComponent<CharacterController>().center.y;
		((Component)this).gameObject.GetComponent<CharacterController>().center = new Vector3(0f, y, 0f);
		Object.Destroy((Object)(object)((Component)((Component)this).gameObject.transform.GetChild(0)).GetComponent<CharacterController>());
	}

	public void resetRotation()
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		Transform child = ((Component)this).transform.GetChild(0);
		Quaternion localRotation = child.localRotation;
		if (Math.Abs(((Quaternion)(ref localRotation)).eulerAngles.y) > 30f)
		{
			child.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
		}
		if ((double)Math.Abs(child.localPosition.x) > 0.4 || (double)Math.Abs(child.localPosition.y) > 0.3 || (double)Math.Abs(child.localPosition.z) > 0.4)
		{
			child.localPosition = new Vector3(0f, 0f, 0f);
		}
	}

	public virtual void setBuff()
	{
	}

	public void displayBuff(int buffid)
	{
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		string str = jsonData.instance.BuffJsonData[string.Concat(buffid)]["skillEffect"].str;
		if (str != "")
		{
			Vector3 position = ((Component)this).transform.position;
			Object.Destroy(Object.Instantiate(ResManager.inst.LoadSkillEffect(str), position, Quaternion.identity), jsonData.instance.BuffJsonData[string.Concat(buffid)]["totaltime"].n);
		}
	}

	private void Update()
	{
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Expected O, but got Unknown
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		int num = (int)(uint)entity.getDefinedProperty("roleTypeCell");
		int num2 = (ushort)entity.getDefinedProperty("roleSurfaceCall");
		if (nowRoleType == num && nowRoleFace == num2)
		{
			return;
		}
		nowRoleType = num;
		nowRoleFace = num2;
		float y = entity.position.y;
		GameObject val = (GameObject)Resources.Load("Effect/Prefab/gameEntity/Avater/Avater" + num + "/Avater" + num + "_" + num2);
		Object.Destroy((Object)(object)((Component)((GameObject)entity.renderObj).transform.GetChild(0)).gameObject);
		GameObject obj = Object.Instantiate<GameObject>(val, new Vector3(entity.position.x, y, entity.position.z), Quaternion.Euler(new Vector3(entity.direction.y, entity.direction.z, entity.direction.x)));
		obj.transform.parent = ((GameObject)entity.renderObj).transform;
		obj.transform.SetSiblingIndex(0);
		CharacterController component = obj.GetComponent<CharacterController>();
		if ((Object)(object)component != (Object)null)
		{
			((Component)this).gameObject.GetComponent<CharacterController>().height = component.height;
			float y2 = component.center.y;
			((Component)this).gameObject.GetComponent<CharacterController>().center = new Vector3(0f, y2, 0f);
			Object.Destroy((Object)(object)component);
		}
		if (entity.className == "Avatar")
		{
			Avatar avatar = (Avatar)KBEngineApp.app.player();
			if (entity.id == avatar.id)
			{
				changeCanAttak();
			}
			else
			{
				Tools.instance.setAvaterCanAttack(entity);
			}
		}
	}

	public void changeCanAttak()
	{
		foreach (KeyValuePair<int, Entity> entity in KBEngineApp.app.entities)
		{
			if (entity.Value.className == "Avatar")
			{
				Tools.instance.setAvaterCanAttack(entity.Value);
			}
		}
	}
}
