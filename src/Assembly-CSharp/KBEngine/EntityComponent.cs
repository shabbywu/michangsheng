using System;

namespace KBEngine
{
	// Token: 0x02000B91 RID: 2961
	public class EntityComponent
	{
		// Token: 0x06005228 RID: 21032 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onAttached(Entity ownerEntity)
		{
		}

		// Token: 0x06005229 RID: 21033 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onDetached(Entity ownerEntity)
		{
		}

		// Token: 0x0600522A RID: 21034 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onEnterworld()
		{
		}

		// Token: 0x0600522B RID: 21035 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onLeaveworld()
		{
		}

		// Token: 0x0600522C RID: 21036 RVA: 0x000306E7 File Offset: 0x0002E8E7
		public virtual ScriptModule getScriptModule()
		{
			return null;
		}

		// Token: 0x0600522D RID: 21037 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onRemoteMethodCall(ushort methodUtype, MemoryStream stream)
		{
		}

		// Token: 0x0600522E RID: 21038 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onUpdatePropertys(ushort propUtype, MemoryStream stream, int maxCount)
		{
		}

		// Token: 0x0600522F RID: 21039 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void callPropertysSetMethods()
		{
		}

		// Token: 0x06005230 RID: 21040 RVA: 0x0022409C File Offset: 0x0022229C
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

		// Token: 0x04004FAD RID: 20397
		public ushort entityComponentPropertyID;

		// Token: 0x04004FAE RID: 20398
		public ushort componentType;

		// Token: 0x04004FAF RID: 20399
		public int ownerID;

		// Token: 0x04004FB0 RID: 20400
		public Entity owner;
	}
}
