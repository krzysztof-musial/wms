const Sequelize = require('sequelize');
module.exports = function(sequelize, DataTypes) {
  return sequelize.define('AspNetRoles', {
    Id: {
      type: DataTypes.STRING(450),
      allowNull: false,
      primaryKey: true
    },
    Discriminator: {
      type: DataTypes.TEXT,
      allowNull: false
    },
    Name: {
      type: DataTypes.STRING(256),
      allowNull: true
    },
    NormalizedName: {
      type: DataTypes.STRING(256),
      allowNull: true
    },
    ConcurrencyStamp: {
      type: DataTypes.TEXT,
      allowNull: true
    }
  }, {
    sequelize,
    tableName: 'AspNetRoles',
    schema: 'dbo',
    timestamps: false,
    indexes: [
      {
        name: "PK_AspNetRoles",
        unique: true,
        fields: [
          { name: "Id" },
        ]
      },
      {
        name: "RoleNameIndex",
        unique: true,
        fields: [
          { name: "NormalizedName" },
        ]
      },
    ]
  });
};
