using System;
using System.Collections.Generic;
using UnityEngine;

namespace KBEngine
{
	// Token: 0x02000F5A RID: 3930
	public abstract class MailBase : Entity
	{
		// Token: 0x06005E58 RID: 24152 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onMailTypeChanged(byte oldValue)
		{
		}

		// Token: 0x06005E59 RID: 24153 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onStatusChanged(byte oldValue)
		{
		}

		// Token: 0x06005E5A RID: 24154 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void onUserIDChanged(ulong oldValue)
		{
		}

		// Token: 0x06005E5B RID: 24155 RVA: 0x00040112 File Offset: 0x0003E312
		public MailBase()
		{
		}

		// Token: 0x06005E5C RID: 24156 RVA: 0x000042DD File Offset: 0x000024DD
		public override void onComponentsEnterworld()
		{
		}

		// Token: 0x06005E5D RID: 24157 RVA: 0x000042DD File Offset: 0x000024DD
		public override void onComponentsLeaveworld()
		{
		}

		// Token: 0x06005E5E RID: 24158 RVA: 0x0004225C File Offset: 0x0004045C
		public override void onGetBase()
		{
			this.baseEntityCall = new EntityBaseEntityCall_MailBase(this.id, this.className);
		}

		// Token: 0x06005E5F RID: 24159 RVA: 0x00042275 File Offset: 0x00040475
		public override void onGetCell()
		{
			this.cellEntityCall = new EntityCellEntityCall_MailBase(this.id, this.className);
		}

		// Token: 0x06005E60 RID: 24160 RVA: 0x0004228E File Offset: 0x0004048E
		public override void onLoseCell()
		{
			this.cellEntityCall = null;
		}

		// Token: 0x06005E61 RID: 24161 RVA: 0x00042297 File Offset: 0x00040497
		public override EntityCall getBaseEntityCall()
		{
			return this.baseEntityCall;
		}

		// Token: 0x06005E62 RID: 24162 RVA: 0x0004229F File Offset: 0x0004049F
		public override EntityCall getCellEntityCall()
		{
			return this.cellEntityCall;
		}

		// Token: 0x06005E63 RID: 24163 RVA: 0x000042DD File Offset: 0x000024DD
		public override void attachComponents()
		{
		}

		// Token: 0x06005E64 RID: 24164 RVA: 0x000042DD File Offset: 0x000024DD
		public override void detachComponents()
		{
		}

		// Token: 0x06005E65 RID: 24165 RVA: 0x00261AD0 File Offset: 0x0025FCD0
		public override void onRemoteMethodCall(MemoryStream stream)
		{
			ScriptModule scriptModule = EntityDef.moduledefs["Mail"];
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

		// Token: 0x06005E66 RID: 24166 RVA: 0x00261B50 File Offset: 0x0025FD50
		public override void onUpdatePropertys(MemoryStream stream)
		{
			ScriptModule scriptModule = EntityDef.moduledefs["Mail"];
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
				ushort properUtype2 = property.properUtype;
				switch (properUtype2)
				{
				case 154:
				{
					ulong oldValue = this.userID;
					this.userID = stream.readUint64();
					if (property.isBase())
					{
						if (this.inited)
						{
							this.onUserIDChanged(oldValue);
						}
					}
					else if (this.inWorld)
					{
						this.onUserIDChanged(oldValue);
					}
					break;
				}
				case 155:
				{
					byte mailType = this.MailType;
					this.MailType = stream.readUint8();
					if (property.isBase())
					{
						if (this.inited)
						{
							this.onMailTypeChanged(mailType);
						}
					}
					else if (this.inWorld)
					{
						this.onMailTypeChanged(mailType);
					}
					break;
				}
				case 156:
				{
					byte oldValue2 = this.status;
					this.status = stream.readUint8();
					if (property.isBase())
					{
						if (this.inited)
						{
							this.onStatusChanged(oldValue2);
						}
					}
					else if (this.inWorld)
					{
						this.onStatusChanged(oldValue2);
					}
					break;
				}
				default:
					switch (properUtype2)
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
					break;
				}
			}
		}

		// Token: 0x06005E67 RID: 24167 RVA: 0x00261D84 File Offset: 0x0025FF84
		public override void callPropertysSetMethods()
		{
			Dictionary<ushort, Property> idpropertys = EntityDef.moduledefs["Mail"].idpropertys;
			byte mailType = this.MailType;
			Property property = idpropertys[4];
			if (property.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onMailTypeChanged(mailType);
				}
			}
			else if (this.inWorld && (!property.isOwnerOnly() || base.isPlayer()))
			{
				this.onMailTypeChanged(mailType);
			}
			Vector3 direction = this.direction;
			Property property2 = idpropertys[2];
			if (property2.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onDirectionChanged(direction);
				}
			}
			else if (this.inWorld && (!property2.isOwnerOnly() || base.isPlayer()))
			{
				this.onDirectionChanged(direction);
			}
			Vector3 position = this.position;
			Property property3 = idpropertys[1];
			if (property3.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onPositionChanged(position);
				}
			}
			else if (this.inWorld && (!property3.isOwnerOnly() || base.isPlayer()))
			{
				this.onPositionChanged(position);
			}
			byte oldValue = this.status;
			Property property4 = idpropertys[5];
			if (property4.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onStatusChanged(oldValue);
				}
			}
			else if (this.inWorld && (!property4.isOwnerOnly() || base.isPlayer()))
			{
				this.onStatusChanged(oldValue);
			}
			ulong oldValue2 = this.userID;
			Property property5 = idpropertys[6];
			if (property5.isBase())
			{
				if (this.inited && !this.inWorld)
				{
					this.onUserIDChanged(oldValue2);
					return;
				}
			}
			else if (this.inWorld && (!property5.isOwnerOnly() || base.isPlayer()))
			{
				this.onUserIDChanged(oldValue2);
			}
		}

		// Token: 0x04005B16 RID: 23318
		public EntityBaseEntityCall_MailBase baseEntityCall;

		// Token: 0x04005B17 RID: 23319
		public EntityCellEntityCall_MailBase cellEntityCall;

		// Token: 0x04005B18 RID: 23320
		public byte MailType;

		// Token: 0x04005B19 RID: 23321
		public byte status;

		// Token: 0x04005B1A RID: 23322
		public ulong userID;
	}
}
