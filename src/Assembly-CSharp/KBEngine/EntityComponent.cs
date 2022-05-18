using System;

namespace KBEngine
{
	// Token: 0x02000F0F RID: 3855
	public class EntityComponent
	{
		// Token: 0x06005C64 RID: 23652 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onAttached(Entity ownerEntity)
		{
		}

		// Token: 0x06005C65 RID: 23653 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onDetached(Entity ownerEntity)
		{
		}

		// Token: 0x06005C66 RID: 23654 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onEnterworld()
		{
		}

		// Token: 0x06005C67 RID: 23655 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onLeaveworld()
		{
		}

		// Token: 0x06005C68 RID: 23656 RVA: 0x0000B171 File Offset: 0x00009371
		public virtual ScriptModule getScriptModule()
		{
			return null;
		}

		// Token: 0x06005C69 RID: 23657 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onRemoteMethodCall(ushort methodUtype, MemoryStream stream)
		{
		}

		// Token: 0x06005C6A RID: 23658 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onUpdatePropertys(ushort propUtype, MemoryStream stream, int maxCount)
		{
		}

		// Token: 0x06005C6B RID: 23659 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void callPropertysSetMethods()
		{
		}

		// Token: 0x06005C6C RID: 23660 RVA: 0x00252834 File Offset: 0x00250A34
		public virtual void createFromStream(MemoryStream stream)
		{
			this.componentType = (ushort)stream.readInt32();
			this.ownerID = stream.readInt32();
			stream.readUint16();
			ushort num = stream.readUint16();
			if (num > 0)
			{
				this.onUpdatePropertys(0, stream, (int)num);
			}
		}

		// Token: 0x04005A3B RID: 23099
		public ushort entityComponentPropertyID;

		// Token: 0x04005A3C RID: 23100
		public ushort componentType;

		// Token: 0x04005A3D RID: 23101
		public int ownerID;

		// Token: 0x04005A3E RID: 23102
		public Entity owner;
	}
}
