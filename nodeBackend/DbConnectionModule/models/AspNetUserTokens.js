const Sequelize = require('sequelize');
module.exports = function(sequelize, DataTypes) {
  return sequelize.define('AspNetUserTokens', {
    UserId: {
      type: DataTypes.STRING(450),
      allowNull: false,
      primaryKey: true,
      references: {
        model: 'AspNetUsers',
        key: 'Id'
      }
    },
    LoginProvider: {
      type: DataTypes.STRING(450),
      allowNull: false,
      primaryKey: true
    },
    Name: {
      type: DataTypes.STRING(450),
      allowNull: false,
      primaryKey: true
    },
    Value: {
      type: DataTypes.TEXT,
      allowNull: true
    }
  }, {
    sequelize,
    tableName: 'AspNetUserTokens',
    schema: 'dbo',
    timestamps: false,
    indexes: [
      {
        name: "PK_AspNetUserTokens",
        unique: true,
        fields: [
          { name: "UserId" },
          { name: "LoginProvider" },
          { name: "Name" },
        ]
      },
    ]
  });
};
