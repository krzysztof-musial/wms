const Sequelize = require('sequelize');
module.exports = function(sequelize, DataTypes) {
  return sequelize.define('AspNetUserRoles', {
    UserId: {
      type: DataTypes.STRING(450),
      allowNull: false,
      primaryKey: true,
      references: {
        model: 'AspNetUsers',
        key: 'Id'
      }
    },
    RoleId: {
      type: DataTypes.STRING(450),
      allowNull: false,
      primaryKey: true,
      references: {
        model: 'AspNetRoles',
        key: 'Id'
      }
    }
  }, {
    sequelize,
    tableName: 'AspNetUserRoles',
    schema: 'dbo',
    timestamps: false,
    indexes: [
      {
        name: "IX_AspNetUserRoles_RoleId",
        fields: [
          { name: "RoleId" },
        ]
      },
      {
        name: "PK_AspNetUserRoles",
        unique: true,
        fields: [
          { name: "UserId" },
          { name: "RoleId" },
        ]
      },
    ]
  });
};
