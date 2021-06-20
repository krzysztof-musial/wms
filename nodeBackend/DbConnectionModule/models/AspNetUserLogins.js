const Sequelize = require('sequelize');
module.exports = function(sequelize, DataTypes) {
  return sequelize.define('AspNetUserLogins', {
    LoginProvider: {
      type: DataTypes.STRING(450),
      allowNull: false,
      primaryKey: true
    },
    ProviderKey: {
      type: DataTypes.STRING(450),
      allowNull: false,
      primaryKey: true
    },
    ProviderDisplayName: {
      type: DataTypes.TEXT,
      allowNull: true
    },
    UserId: {
      type: DataTypes.STRING(450),
      allowNull: false,
      references: {
        model: 'AspNetUsers',
        key: 'Id'
      }
    }
  }, {
    sequelize,
    tableName: 'AspNetUserLogins',
    schema: 'dbo',
    timestamps: false,
    indexes: [
      {
        name: "IX_AspNetUserLogins_UserId",
        fields: [
          { name: "UserId" },
        ]
      },
      {
        name: "PK_AspNetUserLogins",
        unique: true,
        fields: [
          { name: "LoginProvider" },
          { name: "ProviderKey" },
        ]
      },
    ]
  });
};
