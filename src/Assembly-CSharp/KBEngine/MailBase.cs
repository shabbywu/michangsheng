using System;
using System.Collections.Generic;
using UnityEngine;

namespace KBEngine
{
	// Token: 0x02000BD7 RID: 3031
	public abstract class MailBase : Entity
	{
		// Token: 0x0600541A RID: 21530 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onMailTypeChanged(byte oldValue)
		{
		}

		// Token: 0x0600541B RID: 21531 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onStatusChanged(byte oldValue)
		{
		}

		// Token: 0x0600541C RID: 21532 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void onUserIDChanged(ulong oldValue)
		{
		}

		// Token: 0x0600541D RID: 21533 RVA: 0x00220682 File Offset: 0x0021E882
		public MailBase()
		{
		}

		// Token: 0x0600541E RID: 21534 RVA: 0x00004095 File Offset: 0x00002295
		public override void onComponentsEnterworld()
		{
		}

		// Token: 0x0600541F RID: 21535 RVA: 0x00004095 File Offset: 0x00002295
		public override void onComponentsLeaveworld()
		{
		}

		// Token: 0x06005420 RID: 21536 RVA: 0x002341A1 File Offset: 0x002323A1
		public override void onGetBase()
		{
			this.baseEntityCall = new EntityBaseEntityCall_MailBase(this.id, this.className);
		}

		// Token: 0x06005421 RID: 21537 RVA: 0x002341BA File Offset: 0x002323BA
		public override void onGetCell()
		{
			this.cellEntityCall = new EntityCellEntityCall_MailBase(this.id, this.className);
		}

		// Token: 0x06005422 RID: 21538 RVA: 0x002341D3 File Offset: 0x002323D3
		public override void onLoseCell()
		{
			this.cellEntityCall = null;
		}

		// Token: 0x06005423 RID: 21539 RVA: 0x002341DC File Offset: 0x002323DC
		public override EntityCall getBaseEntityCall()
		{
			return this.baseEntityCall;
		}

		// Token: 0x06005424 RID: 21540 RVA: 0x002341E4 File Offset: 0x002323E4
		public override EntityCall getCellEntityCall()
		{
			return this.cellEntityCall;
		}

		// Token: 0x06005425 RID: 21541 RVA: 0x00004095 File Offset: 0x00002295
		public override void attachComponents()
		{
		}

		// Token: 0x06005426 RID: 21542 RVA: 0x00004095 File Offset: 0x00002295
		public override void detachComponents()
		{
		}

		// Token: 0x06005427 RID: 21543 RVA: 0x002341EC File Offset: 0x002323EC
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

		// Token: 0x06005428 RID: 21544 RVA: 0x0023426C File Offset: 0x0023246C
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

		// Token: 0x06005429 RID: 21545 RVA: 0x002344A0 File Offset: 0x002326A0
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

		// Token: 0x04005075 RID: 20597
		public EntityBaseEntityCall_MailBase baseEntityCall;

		// Token: 0x04005076 RID: 20598
		public EntityCellEntityCall_MailBase cellEntityCall;

		// Token: 0x04005077 RID: 20599
		public byte MailType;

		// Token: 0x04005078 RID: 20600
		public byte status;

		// Token: 0x04005079 RID: 20601
		public ulong userID;
	}
}
