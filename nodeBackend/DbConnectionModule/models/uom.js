const Sequelize = require('sequelize');
module.exports = function(sequelize, DataTypes) {
  return sequelize.define('uom', {
    uom_id: {
      autoIncrement: true,
      type: DataTypes.INTEGER,
      allowNull: false,
      primaryKey: true
    },
    uom_name: {
      type: DataTypes.TEXT,
      allowNull: true
    }
  }, {
    sequelize,
    tableName: 'uom',
    schema: 'dbo',
    timestamps: false,
    indexes: [
      {
        name: "PK_uom",
        unique: true,
        fields: [
          { name: "uom_id" },
        ]
      },
    ]
  });
};
