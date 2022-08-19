using System;
using UnityEngine;

namespace KBEngine
{
	// Token: 0x02000B6F RID: 2927
	public class EntityBaseEntityCall_AvatarBase : EntityCall
	{
		// Token: 0x060051E3 RID: 20963 RVA: 0x0022371D File Offset: 0x0022191D
		public EntityBaseEntityCall_AvatarBase(int eid, string ename) : base(eid, ename)
		{
			this.type = EntityCall.ENTITYCALL_TYPE.ENTITYCALL_TYPE_BASE;
		}

		// Token: 0x060051E4 RID: 20964 RVA: 0x00223B0B File Offset: 0x00221D0B
		public void BaseSetPlayerTime(uint arg1)
		{
			if (base.newCall("BaseSetPlayerTime", 0) == null)
			{
				return;
			}
			this.bundle.writeUint32(arg1);
			base.sendCall(null);
		}

		// Token: 0x060051E5 RID: 20965 RVA: 0x00223B2F File Offset: 0x00221D2F
		public void CancelCrafting()
		{
			if (base.newCall("CancelCrafting", 0) == null)
			{
				return;
			}
			base.sendCall(null);
		}

		// Token: 0x060051E6 RID: 20966 RVA: 0x00223B47 File Offset: 0x00221D47
		public void CreateAvaterCall(int arg1)
		{
			if (base.newCall("CreateAvaterCall", 0) == null)
			{
				return;
			}
			this.bundle.writeInt32(arg1);
			base.sendCall(null);
		}

		// Token: 0x060051E7 RID: 20967 RVA: 0x00223B6B File Offset: 0x00221D6B
		public void StartCrafting(int arg1, uint arg2)
		{
			if (base.newCall("StartCrafting", 0) == null)
			{
				return;
			}
			this.bundle.writeInt32(arg1);
			this.bundle.writeUint32(arg2);
			base.sendCall(null);
		}

		// Token: 0x060051E8 RID: 20968 RVA: 0x00223B9B File Offset: 0x00221D9B
		public void UnEquipItemRequest(ulong arg1)
		{
			if (base.newCall("UnEquipItemRequest", 0) == null)
			{
				return;
			}
			this.bundle.writeUint64(arg1);
			base.sendCall(null);
		}

		// Token: 0x060051E9 RID: 20969 RVA: 0x00223BC0 File Offset: 0x00221DC0
		public void addExpAndGoods(uint arg1, ITEM_INFO_LIST arg2)
		{
			if (base.newCall("addExpAndGoods", 0) == null)
			{
				return;
			}
			this.bundle.writeUint32(arg1);
			((DATATYPE_ITEM_INFO_LIST)EntityDef.id2datatypes[38]).addToStreamEx(this.bundle, arg2);
			base.sendCall(null);
		}

		// Token: 0x060051EA RID: 20970 RVA: 0x00223C0C File Offset: 0x00221E0C
		public void backToHome()
		{
			if (base.newCall("backToHome", 0) == null)
			{
				return;
			}
			base.sendCall(null);
		}

		// Token: 0x060051EB RID: 20971 RVA: 0x00223C24 File Offset: 0x00221E24
		public void createBuild(ulong arg1, Vector3 arg2, Vector3 arg3)
		{
			if (base.newCall("createBuild", 0) == null)
			{
				return;
			}
			this.bundle.writeUint64(arg1);
			this.bundle.writeVector3(arg2);
			this.bundle.writeVector3(arg3);
			base.sendCall(null);
		}

		// Token: 0x060051EC RID: 20972 RVA: 0x00223C60 File Offset: 0x00221E60
		public void createCell(byte[] arg1)
		{
			if (base.newCall("createCell", 0) == null)
			{
				return;
			}
			this.bundle.writeEntitycall(arg1);
			base.sendCall(null);
		}

		// Token: 0x060051ED RID: 20973 RVA: 0x00223C84 File Offset: 0x00221E84
		public void dropRequest(ulong arg1)
		{
			if (base.newCall("dropRequest", 0) == null)
			{
				return;
			}
			this.bundle.writeUint64(arg1);
			base.sendCall(null);
		}

		// Token: 0x060051EE RID: 20974 RVA: 0x00223CA8 File Offset: 0x00221EA8
		public void equipItemRequest(ulong arg1)
		{
			if (base.newCall("equipItemRequest", 0) == null)
			{
				return;
			}
			this.bundle.writeUint64(arg1);
			base.sendCall(null);
		}

		// Token: 0x060051EF RID: 20975 RVA: 0x00223CCC File Offset: 0x00221ECC
		public void reqItemList()
		{
			if (base.newCall("reqItemList", 0) == null)
			{
				return;
			}
			base.sendCall(null);
		}

		// Token: 0x060051F0 RID: 20976 RVA: 0x00223CE4 File Offset: 0x00221EE4
		public void resetAvaterType()
		{
			if (base.newCall("resetAvaterType", 0) == null)
			{
				return;
			}
			base.sendCall(null);
		}

		// Token: 0x060051F1 RID: 20977 RVA: 0x00223CFC File Offset: 0x00221EFC
		public void sendChatMessage(string arg1)
		{
			if (base.newCall("sendChatMessage", 0) == null)
			{
				return;
			}
			this.bundle.writeUnicode(arg1);
			base.sendCall(null);
		}

		// Token: 0x060051F2 RID: 20978 RVA: 0x00223D20 File Offset: 0x00221F20
		public void swapItemRequest(int arg1, int arg2)
		{
			if (base.newCall("swapItemRequest", 0) == null)
			{
				return;
			}
			this.bundle.writeInt32(arg1);
			this.bundle.writeInt32(arg2);
			base.sendCall(null);
		}

		// Token: 0x060051F3 RID: 20979 RVA: 0x00223D50 File Offset: 0x00221F50
		public void useItemRequest(ulong arg1)
		{
			if (base.newCall("useItemRequest", 0) == null)
			{
				return;
			}
			this.bundle.writeUint64(arg1);
			base.sendCall(null);
		}
	}
}
