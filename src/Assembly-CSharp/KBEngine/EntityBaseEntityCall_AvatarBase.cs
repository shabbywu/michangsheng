using System;
using UnityEngine;

namespace KBEngine
{
	// Token: 0x02000EED RID: 3821
	public class EntityBaseEntityCall_AvatarBase : EntityCall
	{
		// Token: 0x06005C1F RID: 23583 RVA: 0x00040ABF File Offset: 0x0003ECBF
		public EntityBaseEntityCall_AvatarBase(int eid, string ename) : base(eid, ename)
		{
			this.type = EntityCall.ENTITYCALL_TYPE.ENTITYCALL_TYPE_BASE;
		}

		// Token: 0x06005C20 RID: 23584 RVA: 0x00040EAD File Offset: 0x0003F0AD
		public void BaseSetPlayerTime(uint arg1)
		{
			if (base.newCall("BaseSetPlayerTime", 0) == null)
			{
				return;
			}
			this.bundle.writeUint32(arg1);
			base.sendCall(null);
		}

		// Token: 0x06005C21 RID: 23585 RVA: 0x00040ED1 File Offset: 0x0003F0D1
		public void CancelCrafting()
		{
			if (base.newCall("CancelCrafting", 0) == null)
			{
				return;
			}
			base.sendCall(null);
		}

		// Token: 0x06005C22 RID: 23586 RVA: 0x00040EE9 File Offset: 0x0003F0E9
		public void CreateAvaterCall(int arg1)
		{
			if (base.newCall("CreateAvaterCall", 0) == null)
			{
				return;
			}
			this.bundle.writeInt32(arg1);
			base.sendCall(null);
		}

		// Token: 0x06005C23 RID: 23587 RVA: 0x00040F0D File Offset: 0x0003F10D
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

		// Token: 0x06005C24 RID: 23588 RVA: 0x00040F3D File Offset: 0x0003F13D
		public void UnEquipItemRequest(ulong arg1)
		{
			if (base.newCall("UnEquipItemRequest", 0) == null)
			{
				return;
			}
			this.bundle.writeUint64(arg1);
			base.sendCall(null);
		}

		// Token: 0x06005C25 RID: 23589 RVA: 0x00252784 File Offset: 0x00250984
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

		// Token: 0x06005C26 RID: 23590 RVA: 0x00040F61 File Offset: 0x0003F161
		public void backToHome()
		{
			if (base.newCall("backToHome", 0) == null)
			{
				return;
			}
			base.sendCall(null);
		}

		// Token: 0x06005C27 RID: 23591 RVA: 0x00040F79 File Offset: 0x0003F179
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

		// Token: 0x06005C28 RID: 23592 RVA: 0x00040FB5 File Offset: 0x0003F1B5
		public void createCell(byte[] arg1)
		{
			if (base.newCall("createCell", 0) == null)
			{
				return;
			}
			this.bundle.writeEntitycall(arg1);
			base.sendCall(null);
		}

		// Token: 0x06005C29 RID: 23593 RVA: 0x00040FD9 File Offset: 0x0003F1D9
		public void dropRequest(ulong arg1)
		{
			if (base.newCall("dropRequest", 0) == null)
			{
				return;
			}
			this.bundle.writeUint64(arg1);
			base.sendCall(null);
		}

		// Token: 0x06005C2A RID: 23594 RVA: 0x00040FFD File Offset: 0x0003F1FD
		public void equipItemRequest(ulong arg1)
		{
			if (base.newCall("equipItemRequest", 0) == null)
			{
				return;
			}
			this.bundle.writeUint64(arg1);
			base.sendCall(null);
		}

		// Token: 0x06005C2B RID: 23595 RVA: 0x00041021 File Offset: 0x0003F221
		public void reqItemList()
		{
			if (base.newCall("reqItemList", 0) == null)
			{
				return;
			}
			base.sendCall(null);
		}

		// Token: 0x06005C2C RID: 23596 RVA: 0x00041039 File Offset: 0x0003F239
		public void resetAvaterType()
		{
			if (base.newCall("resetAvaterType", 0) == null)
			{
				return;
			}
			base.sendCall(null);
		}

		// Token: 0x06005C2D RID: 23597 RVA: 0x00041051 File Offset: 0x0003F251
		public void sendChatMessage(string arg1)
		{
			if (base.newCall("sendChatMessage", 0) == null)
			{
				return;
			}
			this.bundle.writeUnicode(arg1);
			base.sendCall(null);
		}

		// Token: 0x06005C2E RID: 23598 RVA: 0x00041075 File Offset: 0x0003F275
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

		// Token: 0x06005C2F RID: 23599 RVA: 0x000410A5 File Offset: 0x0003F2A5
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
