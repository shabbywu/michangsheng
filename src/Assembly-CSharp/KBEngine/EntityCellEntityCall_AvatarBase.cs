using UnityEngine;

namespace KBEngine;

public class EntityCellEntityCall_AvatarBase : EntityCall
{
	public EntityCellEntityCall_AvatarBase(int eid, string ename)
		: base(eid, ename)
	{
		type = ENTITYCALL_TYPE.ENTITYCALL_TYPE_CELL;
	}

	public void BuildNotify(ulong arg1, Vector3 arg2, Vector3 arg3)
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		if (newCall("BuildNotify", 0) != null)
		{
			bundle.writeUint64(arg1);
			bundle.writeVector3(arg2);
			bundle.writeVector3(arg3);
			sendCall(null);
		}
	}

	public void DayZombie()
	{
		if (newCall("DayZombie", 0) != null)
		{
			sendCall(null);
		}
	}

	public void SkillDamage(int arg1, int arg2)
	{
		if (newCall("SkillDamage", 0) != null)
		{
			bundle.writeInt32(arg1);
			bundle.writeInt32(arg2);
			sendCall(null);
		}
	}

	public void addSkill(int arg1)
	{
		if (newCall("addSkill", 0) != null)
		{
			bundle.writeInt32(arg1);
			sendCall(null);
		}
	}

	public void changeAvaterType(uint arg1, uint arg2)
	{
		if (newCall("changeAvaterType", 0) != null)
		{
			bundle.writeUint32(arg1);
			bundle.writeUint32(arg2);
			sendCall(null);
		}
	}

	public void dialog(int arg1, uint arg2)
	{
		if (newCall("dialog", 0) != null)
		{
			bundle.writeInt32(arg1);
			bundle.writeUint32(arg2);
			sendCall(null);
		}
	}

	public void gameFinsh()
	{
		if (newCall("gameFinsh", 0) != null)
		{
			sendCall(null);
		}
	}

	public void relive(byte arg1)
	{
		if (newCall("relive", 0) != null)
		{
			bundle.writeUint8(arg1);
			sendCall(null);
		}
	}

	public void removeSkill(int arg1)
	{
		if (newCall("removeSkill", 0) != null)
		{
			bundle.writeInt32(arg1);
			sendCall(null);
		}
	}

	public void requestPull()
	{
		if (newCall("requestPull", 0) != null)
		{
			sendCall(null);
		}
	}

	public void usePostionSkill(int arg1)
	{
		if (newCall("usePostionSkill", 0) != null)
		{
			bundle.writeInt32(arg1);
			sendCall(null);
		}
	}

	public void useSelfSkill(int arg1)
	{
		if (newCall("useSelfSkill", 0) != null)
		{
			bundle.writeInt32(arg1);
			sendCall(null);
		}
	}

	public void useTargetSkill(int arg1, int arg2)
	{
		if (newCall("useTargetSkill", 0) != null)
		{
			bundle.writeInt32(arg1);
			bundle.writeInt32(arg2);
			sendCall(null);
		}
	}
}
