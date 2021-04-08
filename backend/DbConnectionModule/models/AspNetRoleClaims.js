const Sequelize = require('sequelize');
module.exports = function(sequelize, DataTypes) {
  return sequelize.define('AspNetRoleClaims', {
    Id: {
      autoIncrement: true,
      type: DataTypes.INTEGER,
      allowNull: false,
      primaryKey: true
    },
    RoleId: {
      type: DataTypes.STRING(450),
      allowNull: false,
      references: {
        model: 'AspNetRoles',
        key: 'Id'
      }
    },
    ClaimType: {
      type: DataTypes.TEXT,
      allowNull: true
    },
    ClaimValue: {
      type: DataTypes.TEXT,
      allowNull: true
    }
  }, {
    sequelize,
    tableName: 'AspNetRoleClaims',
    schema: 'dbo',
    timestamps: false,
    indexes: [
      {
        name: "IX_AspNetRoleClaims_RoleId",
        fields: [
          { name: "RoleId" },
        ]
      },
      {
        name: "PK_AspNetRoleClaims",
        unique: true,
        fields: [
          { name: "Id" },
        ]
      },
    ]
  });
};
