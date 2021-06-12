const Sequelize = require('sequelize');
module.exports = function(sequelize, DataTypes) {
  return sequelize.define('product', {
    product_id: {
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
    CompanyId: {
      type: DataTypes.INTEGER,
      allowNull: true,
      references: {
        model: 'company',
        key: 'company_id'
      }
    },
    UnitOfMessureId: {
      type: DataTypes.INTEGER,
      allowNull: true,
      references: {
        model: 'uom',
        key: 'uom_id'
      }
    },
    product_name: {
      type: DataTypes.TEXT,
      allowNull: false
    },
    product_code: {
      type: DataTypes.TEXT,
      allowNull: false
    },
    product_description: {
      type: DataTypes.TEXT,
      allowNull: true
    }
  }, {
    sequelize,
    tableName: 'product',
    schema: 'dbo',
    timestamps: false,
    indexes: [
      {
        name: "IX_product_CompanyId",
        fields: [
          { name: "CompanyId" },
        ]
      },
      {
        name: "IX_product_UnitOfMessureId",
        fields: [
          { name: "UnitOfMessureId" },
        ]
      },
      {
        name: "IX_product_WarehouseId",
        fields: [
          { name: "WarehouseId" },
        ]
      },
      {
        name: "PK_product",
        unique: true,
        fields: [
          { name: "product_id" },
        ]
      },
    ]
  });
};
