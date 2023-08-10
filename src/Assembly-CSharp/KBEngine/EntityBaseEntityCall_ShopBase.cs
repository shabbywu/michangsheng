namespace KBEngine;

public class EntityBaseEntityCall_ShopBase : EntityCall
{
	public EntityBaseEntityCall_ShopBase(int eid, string ename)
		: base(eid, ename)
	{
		type = ENTITYCALL_TYPE.ENTITYCALL_TYPE_BASE;
	}

	public void buyItem(byte[] arg1, ulong arg2)
	{
		if (newCall("buyItem", 0) != null)
		{
			bundle.writeEntitycall(arg1);
			bundle.writeUint64(arg2);
			sendCall(null);
		}
	}

	public void deliverGoods(byte[] arg1, ulong arg2)
	{
		if (newCall("deliverGoods", 0) != null)
		{
			bundle.writeEntitycall(arg1);
			bundle.writeUint64(arg2);
			sendCall(null);
		}
	}

	public void getShopList(byte[] arg1)
	{
		if (newCall("getShopList", 0) != null)
		{
			bundle.writeEntitycall(arg1);
			sendCall(null);
		}
	}
}
