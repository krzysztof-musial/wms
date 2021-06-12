const Sequelize = require('sequelize');
module.exports = function(sequelize, DataTypes) {
  return sequelize.define('Warehouses', {
    warehouse_id: {
      autoIncrement: true,
      type: DataTypes.INTEGER,
      allowNull: false,
      primaryKey: true
    },
    warehouse_name: {
      type: DataTypes.TEXT,
      allowNull: false
    }
  }, {
    sequelize,
    tableName: 'Warehouses',
    schema: 'dbo',
    timestamps: false,
    indexes: [
      {
        name: "PK_Warehouses",
        unique: true,
        fields: [
          { name: "warehouse_id" },
        ]
      },
    ]
  });
};
