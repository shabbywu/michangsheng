using System;

namespace KBEngine
{
	// Token: 0x02000EE9 RID: 3817
	public class EntityCall
	{
		// Token: 0x06005BFA RID: 23546 RVA: 0x00040A5B File Offset: 0x0003EC5B
		public EntityCall(int eid, string ename)
		{
			this.id = eid;
			this.className = ename;
		}

		// Token: 0x06005BFB RID: 23547 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void __init__()
		{
		}

		// Token: 0x06005BFC RID: 23548 RVA: 0x00040A7C File Offset: 0x0003EC7C
		public virtual bool isBase()
		{
			return this.type == EntityCall.ENTITYCALL_TYPE.ENTITYCALL_TYPE_BASE;
		}

		// Token: 0x06005BFD RID: 23549 RVA: 0x00040A87 File Offset: 0x0003EC87
		public virtual bool isCell()
		{
			return this.type == EntityCall.ENTITYCALL_TYPE.ENTITYCALL_TYPE_CELL;
		}

		// Token: 0x06005BFE RID: 23550 RVA: 0x00252620 File Offset: 0x00250820
		public Bundle newCall()
		{
			if (this.bundle == null)
			{
				this.bundle = ObjectPool<Bundle>.createObject();
			}
			if (this.isCell())
			{
				this.bundle.newMessage(Messages.messages["Baseapp_onRemoteCallCellMethodFromClient"]);
			}
			else
			{
				this.bundle.newMessage(Messages.messages["Entity_onRemoteMethodCall"]);
			}
			this.bundle.writeInt32(this.id);
			return this.bundle;
		}

		// Token: 0x06005BFF RID: 23551 RVA: 0x00040A92 File Offset: 0x0003EC92
		public void sendCall(Bundle inbundle)
		{
			if (inbundle == null)
			{
				inbundle = this.bundle;
			}
			inbundle.send(KBEngineApp.app.networkInterface());
			if (inbundle == this.bundle)
			{
				this.bundle = null;
			}
		}

		// Token: 0x06005C00 RID: 23552 RVA: 0x00252698 File Offset: 0x00250898
		public Bundle newCall(string methodName, ushort entitycomponentPropertyID = 0)
		{
			if (KBEngineApp.app.currserver == "loginapp")
			{
				Dbg.ERROR_MSG(string.Concat(new string[]
				{
					this.className,
					"::newCall(",
					methodName,
					"), currserver=!",
					KBEngineApp.app.currserver
				}));
				return null;
			}
			ScriptModule scriptModule = null;
			if (!EntityDef.moduledefs.TryGetValue(this.className, out scriptModule))
			{
				Dbg.ERROR_MSG(this.className + "::newCall: entity-module(" + this.className + ") error, can not find from EntityDef.moduledefs");
				return null;
			}
			Method method;
			if (this.isCell())
			{
				method = scriptModule.cell_methods[methodName];
			}
			else
			{
				method = scriptModule.base_methods[methodName];
			}
			ushort methodUtype = method.methodUtype;
			this.newCall();
			this.bundle.writeUint16(entitycomponentPropertyID);
			this.bundle.writeUint16(methodUtype);
			return this.bundle;
		}

		// Token: 0x04005A34 RID: 23092
		public int id;

		// Token: 0x04005A35 RID: 23093
		public string className = "";

		// Token: 0x04005A36 RID: 23094
		public EntityCall.ENTITYCALL_TYPE type;

		// Token: 0x04005A37 RID: 23095
		public Bundle bundle;

		// Token: 0x02000EEA RID: 3818
		public enum ENTITYCALL_TYPE
		{
			// Token: 0x04005A39 RID: 23097
			ENTITYCALL_TYPE_CELL,
			// Token: 0x04005A3A RID: 23098
			ENTITYCALL_TYPE_BASE
		}
	}
}
