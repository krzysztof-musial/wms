const Sequelize = require('sequelize');
module.exports = function(sequelize, DataTypes) {
  return sequelize.define('AspNetUsers', {
    Id: {
      type: DataTypes.STRING(450),
      allowNull: false,
      primaryKey: true
    },
    FirstName: {
      type: DataTypes.TEXT,
      allowNull: true
    },
    LastName: {
      type: DataTypes.TEXT,
      allowNull: true
    },
    WarehouseId: {
      type: DataTypes.INTEGER,
      allowNull: true,
      references: {
        model: 'Warehouses',
        key: 'warehouse_id'
      }
    },
    UserName: {
      type: DataTypes.STRING(256),
      allowNull: true
    },
    NormalizedUserName: {
      type: DataTypes.STRING(256),
      allowNull: true
    },
    Email: {
      type: DataTypes.STRING(256),
      allowNull: true
    },
    NormalizedEmail: {
      type: DataTypes.STRING(256),
      allowNull: true
    },
    EmailConfirmed: {
      type: DataTypes.BOOLEAN,
      allowNull: false
    },
    PasswordHash: {
      type: DataTypes.TEXT,
      allowNull: true
    },
    SecurityStamp: {
      type: DataTypes.TEXT,
      allowNull: true
    },
    ConcurrencyStamp: {
      type: DataTypes.TEXT,
      allowNull: true
    },
    PhoneNumber: {
      type: DataTypes.TEXT,
      allowNull: true
    },
    PhoneNumberConfirmed: {
      type: DataTypes.BOOLEAN,
      allowNull: false
    },
    TwoFactorEnabled: {
      type: DataTypes.BOOLEAN,
      allowNull: false
    },
    LockoutEnd: {
      type: DataTypes.DATE,
      allowNull: true
    },
    LockoutEnabled: {
      type: DataTypes.BOOLEAN,
      allowNull: false
    },
    AccessFailedCount: {
      type: DataTypes.INTEGER,
      allowNull: false
    }
  }, {
    sequelize,
    tableName: 'AspNetUsers',
    schema: 'dbo',
    timestamps: false,
    indexes: [
      {
        name: "EmailIndex",
        fields: [
          { name: "NormalizedEmail" },
        ]
      },
      {
        name: "IX_AspNetUsers_WarehouseId",
        fields: [
          { name: "WarehouseId" },
        ]
      },
      {
        name: "PK_AspNetUsers",
        unique: true,
        fields: [
          { name: "Id" },
        ]
      },
      {
        name: "UserNameIndex",
        unique: true,
        fields: [
          { name: "NormalizedUserName" },
        ]
      },
    ]
  });
};
