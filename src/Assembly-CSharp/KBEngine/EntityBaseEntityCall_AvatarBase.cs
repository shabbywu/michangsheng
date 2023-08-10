using UnityEngine;

namespace KBEngine;

public class EntityBaseEntityCall_AvatarBase : EntityCall
{
	public EntityBaseEntityCall_AvatarBase(int eid, string ename)
		: base(eid, ename)
	{
		type = ENTITYCALL_TYPE.ENTITYCALL_TYPE_BASE;
	}

	public void BaseSetPlayerTime(uint arg1)
	{
		if (newCall("BaseSetPlayerTime", 0) != null)
		{
			bundle.writeUint32(arg1);
			sendCall(null);
		}
	}

	public void CancelCrafting()
	{
		if (newCall("CancelCrafting", 0) != null)
		{
			sendCall(null);
		}
	}

	public void CreateAvaterCall(int arg1)
	{
		if (newCall("CreateAvaterCall", 0) != null)
		{
			bundle.writeInt32(arg1);
			sendCall(null);
		}
	}

	public void StartCrafting(int arg1, uint arg2)
	{
		if (newCall("StartCrafting", 0) != null)
		{
			bundle.writeInt32(arg1);
			bundle.writeUint32(arg2);
			sendCall(null);
		}
	}

	public void UnEquipItemRequest(ulong arg1)
	{
		if (newCall("UnEquipItemRequest", 0) != null)
		{
			bundle.writeUint64(arg1);
			sendCall(null);
		}
	}

	public void addExpAndGoods(uint arg1, ITEM_INFO_LIST arg2)
	{
		if (newCall("addExpAndGoods", 0) != null)
		{
			bundle.writeUint32(arg1);
			((DATATYPE_ITEM_INFO_LIST)EntityDef.id2datatypes[38]).addToStreamEx(bundle, arg2);
			sendCall(null);
		}
	}

	public void backToHome()
	{
		if (newCall("backToHome", 0) != null)
		{
			sendCall(null);
		}
	}

	public void createBuild(ulong arg1, Vector3 arg2, Vector3 arg3)
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		if (newCall("createBuild", 0) != null)
		{
			bundle.writeUint64(arg1);
			bundle.writeVector3(arg2);
			bundle.writeVector3(arg3);
			sendCall(null);
		}
	}

	public void createCell(byte[] arg1)
	{
		if (newCall("createCell", 0) != null)
		{
			bundle.writeEntitycall(arg1);
			sendCall(null);
		}
	}

	public void dropRequest(ulong arg1)
	{
		if (newCall("dropRequest", 0) != null)
		{
			bundle.writeUint64(arg1);
			sendCall(null);
		}
	}

	public void equipItemRequest(ulong arg1)
	{
		if (newCall("equipItemRequest", 0) != null)
		{
			bundle.writeUint64(arg1);
			sendCall(null);
		}
	}

	public void reqItemList()
	{
		if (newCall("reqItemList", 0) != null)
		{
			sendCall(null);
		}
	}

	public void resetAvaterType()
	{
		if (newCall("resetAvaterType", 0) != null)
		{
			sendCall(null);
		}
	}

	public void sendChatMessage(string arg1)
	{
		if (newCall("sendChatMessage", 0) != null)
		{
			bundle.writeUnicode(arg1);
			sendCall(null);
		}
	}

	public void swapItemRequest(int arg1, int arg2)
	{
		if (newCall("swapItemRequest", 0) != null)
		{
			bundle.writeInt32(arg1);
			bundle.writeInt32(arg2);
			sendCall(null);
		}
	}

	public void useItemRequest(ulong arg1)
	{
		if (newCall("useItemRequest", 0) != null)
		{
			bundle.writeUint64(arg1);
			sendCall(null);
		}
	}
}
