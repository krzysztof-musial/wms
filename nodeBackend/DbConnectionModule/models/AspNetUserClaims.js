const Sequelize = require('sequelize');
module.exports = function(sequelize, DataTypes) {
  return sequelize.define('AspNetUserClaims', {
    Id: {
      autoIncrement: true,
      type: DataTypes.INTEGER,
      allowNull: false,
      primaryKey: true
    },
    UserId: {
      type: DataTypes.STRING(450),
      allowNull: false,
      references: {
        model: 'AspNetUsers',
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
    tableName: 'AspNetUserClaims',
    schema: 'dbo',
    timestamps: false,
    indexes: [
      {
        name: "IX_AspNetUserClaims_UserId",
        fields: [
          { name: "UserId" },
        ]
      },
      {
        name: "PK_AspNetUserClaims",
        unique: true,
        fields: [
          { name: "Id" },
        ]
      },
    ]
  });
};
