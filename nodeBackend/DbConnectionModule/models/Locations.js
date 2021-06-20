const Sequelize = require('sequelize');
module.exports = function(sequelize, DataTypes) {
  return sequelize.define('Locations', {
    location_id: {
      autoIncrement: true,
      type: DataTypes.INTEGER,
      allowNull: false,
      primaryKey: true
    },
    WarehouseId: {
      type: DataTypes.INTEGER,
      allowNull: true,
      references: {
        model: 'Warehouses',
        key: 'warehouse_id'
      }
    },
    location_code: {
      type: DataTypes.TEXT,
      allowNull: false
    }
  }, {
    sequelize,
    tableName: 'location',
    schema: 'dbo',
    timestamps: false,
    indexes: [
      {
        name: "IX_Locations_WarehouseId",
        fields: [
          { name: "WarehouseId" },
        ]
      },
      {
        name: "PK_Locations",
        unique: true,
        fields: [
          { name: "location_id" },
        ]
      },
    ]
  });
};
