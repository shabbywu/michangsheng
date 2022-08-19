using System;

namespace KBEngine
{
	// Token: 0x02000B87 RID: 2951
	public class EntityBaseEntityCall_ShopBase : EntityCall
	{
		// Token: 0x0600521B RID: 21019 RVA: 0x0022371D File Offset: 0x0022191D
		public EntityBaseEntityCall_ShopBase(int eid, string ename) : base(eid, ename)
		{
			this.type = EntityCall.ENTITYCALL_TYPE.ENTITYCALL_TYPE_BASE;
		}

		// Token: 0x0600521C RID: 21020 RVA: 0x00224015 File Offset: 0x00222215
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

		// Token: 0x0600521D RID: 21021 RVA: 0x00224045 File Offset: 0x00222245
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

		// Token: 0x0600521E RID: 21022 RVA: 0x00224075 File Offset: 0x00222275
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
