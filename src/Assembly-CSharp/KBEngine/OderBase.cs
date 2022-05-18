using System;
using System.Collections.Generic;
using UnityEngine;

namespace KBEngine
{
	// Token: 0x02000FDD RID: 4061
	public abstract class OderBase : Entity
	{
		// Token: 0x06006009 RID: 24585 RVA: 0x00040112 File Offset: 0x0003E312
		public OderBase()
		{
		}

		// Token: 0x0600600A RID: 24586 RVA: 0x000042DD File Offset: 0x000024DD
		public override void onComponentsEnterworld()
		{
		}

		// Token: 0x0600600B RID: 24587 RVA: 0x000042DD File Offset: 0x000024DD
		public override void onComponentsLeaveworld()
		{
		}

		// Token: 0x0600600C RID: 24588 RVA: 0x00042CEB File Offset: 0x00040EEB
		public override void onGetBase()
		{
			this.baseEntityCall = new EntityBaseEntityCall_OderBase(this.id, this.className);
		}

		// Token: 0x0600600D RID: 24589 RVA: 0x00042D04 File Offset: 0x00040F04
		public override void onGetCell()
		{
			this.cellEntityCall = new EntityCellEntityCall_OderBase(this.id, this.className);
		}

		// Token: 0x0600600E RID: 24590 RVA: 0x00042D1D File Offset: 0x00040F1D
		public override void onLoseCell()
		{
			this.cellEntityCall = null;
		}

		// Token: 0x0600600F RID: 24591 RVA: 0x00042D26 File Offset: 0x00040F26
		public override EntityCall getBaseEntityCall()
		{
			return this.baseEntityCall;
		}

		// Token: 0x06006010 RID: 24592 RVA: 0x00042D2E File Offset: 0x00040F2E
		public override EntityCall getCellEntityCall()
		{
			return this.cellEntityCall;
		}

		// Token: 0x06006011 RID: 24593 RVA: 0x000042DD File Offset: 0x000024DD
		public override void attachComponents()
		{
		}

		// Token: 0x06006012 RID: 24594 RVA: 0x000042DD File Offset: 0x000024DD
		public override void detachComponents()
		{
		}

		// Token: 0x06006013 RID: 24595 RVA: 0x00266F8C File Offset: 0x0026518C
		public override void onRemoteMethodCall(MemoryStream stream)
		{
			ScriptModule scriptModule = EntityDef.moduledefs["Oder"];
			ushort num;
			if (scriptModule.usePropertyDescrAlias)
			{
				num = (ushort)stream.readUint8();
			}
			else
			{
				num = stream.readUint16();
			}
			ushort key;
			if (scriptModule.useMethodDescrAlias)
			{
				key = (ushort)stream.readUint8();
			}
			else
			{
				key = stream.readUint16();
			}
			if (num == 0)
			{
				Method method = scriptModule.idmethods[key];
				ushort methodUtype = method.methodUtype;
				return;
			}
			ushort properUtype = scriptModule.idpropertys[num].properUtype;
		}

		// Token: 0x06006014 RID: 24596 RVA: 0x0026700C File Offset: 0x0026520C
		public override void onUpdatePropertys(MemoryStream stream)
		{
			ScriptModule scriptModule = EntityDef.moduledefs["Oder"];
			Dictionary<ushort, Property> idpropertys = scriptModule.idpropertys;
			while (stream.length() > 0U)
			{
				ushort num;
				ushort key;
				if (scriptModule.usePropertyDescrAlias)
				{
					num = (ushort)stream.readUint8();
					key = (ushort)stream.readUint8();
				}
				else
				{
					num = stream.readUint16();
					key = stream.readUint16();
				}
				if (num != 0)
				{
					ushort properUtype = idpropertys[num].properUtype;
					return;
				}
				Property property = idpropertys[key];
				switch (property.properUtype)
				{
				case 40000:
				{
					Vector3 position = this.position;
					this.position = stream.readVector3();
					if (property.isBase())
					{
						if (this.inited)
						{
							this.onPositionChanged(position);
						}
					}
					else if (this.inWorld)
					{
						this.onPositionChanged(position);
					}
					break;
				}
				case 40001:
				{
					Vector3 direction = this.direction;
					this.direction = stream.readVector3();
					if (property.isBase())
					{
						if (this.inited)
						{
							this.onDirectionChanged(direction);
						}
					}
					else if (this.inWorld)
					{
						this.onDirectionChanged(direction);
					}
					break;
				}
				case 40002:
					stream.readUint32();
					break;
				}
			}
		}

		// Token: 0x06006015 RID: 24597 RVA: 0x0026713C File Offset: 0x0026533C
		public override void callPropertysSetMethods()
		{
			Dictionary<ushort, Property> idpropertys = EntityDef.moduledefs["Oder"].idpropertys;
			Vector3 direction = this.direction;
			Property property = idpropertys[2];
			if (property.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onDirectionChanged(direction);
				}
			}
			else if (this.inWorld && (!property.isOwnerOnly() || base.isPlayer()))
			{
				this.onDirectionChanged(direction);
			}
			Vector3 position = this.position;
			Property property2 = idpropertys[1];
			if (property2.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onPositionChanged(position);
					return;
				}
			}
			else if (this.inWorld && (!property2.isOwnerOnly() || base.isPlayer()))
			{
				this.onPositionChanged(position);
			}
		}

		// Token: 0x04005B76 RID: 23414
		public EntityBaseEntityCall_OderBase baseEntityCall;

		// Token: 0x04005B77 RID: 23415
		public EntityCellEntityCall_OderBase cellEntityCall;
	}
}
