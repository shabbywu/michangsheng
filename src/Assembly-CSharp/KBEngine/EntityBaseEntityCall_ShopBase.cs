using System;

namespace KBEngine
{
	// Token: 0x02000F05 RID: 3845
	public class EntityBaseEntityCall_ShopBase : EntityCall
	{
		// Token: 0x06005C57 RID: 23639 RVA: 0x00040ABF File Offset: 0x0003ECBF
		public EntityBaseEntityCall_ShopBase(int eid, string ename) : base(eid, ename)
		{
			this.type = EntityCall.ENTITYCALL_TYPE.ENTITYCALL_TYPE_BASE;
		}

		// Token: 0x06005C58 RID: 23640 RVA: 0x00041309 File Offset: 0x0003F509
		public void buyItem(byte[] arg1, ulong arg2)
		{
			if (base.newCall("buyItem", 0) == null)
			{
				return;
			}
			this.bundle.writeEntitycall(arg1);
			this.bundle.writeUint64(arg2);
			base.sendCall(null);
		}

		// Token: 0x06005C59 RID: 23641 RVA: 0x00041339 File Offset: 0x0003F539
		public void deliverGoods(byte[] arg1, ulong arg2)
		{
			if (base.newCall("deliverGoods", 0) == null)
			{
				return;
			}
			this.bundle.writeEntitycall(arg1);
			this.bundle.writeUint64(arg2);
			base.sendCall(null);
		}

		// Token: 0x06005C5A RID: 23642 RVA: 0x00041369 File Offset: 0x0003F569
		public void getShopList(byte[] arg1)
		{
			if (base.newCall("getShopList", 0) == null)
			{
				return;
			}
			this.bundle.writeEntitycall(arg1);
			base.sendCall(null);
		}
	}
}
