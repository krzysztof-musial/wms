const Sequelize = require('sequelize');
module.exports = function(sequelize, DataTypes) {
  return sequelize.define('order_line', {
    order_line_id: {
      autoIncrement: true,
      type: DataTypes.INTEGER,
      allowNull: false,
      primaryKey: true
    },
    OrderId: {
      type: DataTypes.INTEGER,
      allowNull: true,
      references: {
        model: 'order',
        key: 'order_id'
      }
    },
    ProductId: {
      type: DataTypes.STRING(450),
      allowNull: true,
      references: {
        model: 'product',
        key: 'product_id'
      }
    },
    order_line_quantity: {
      type: DataTypes.DECIMAL(18,2),
      allowNull: false
    }
  }, {
    sequelize,
    tableName: 'order_line',
    schema: 'dbo',
    timestamps: false,
    indexes: [
      {
        name: "IX_order_line_OrderId",
        fields: [
          { name: "OrderId" },
        ]
      },
      {
        name: "IX_order_line_ProductId",
        fields: [
          { name: "ProductId" },
        ]
      },
      {
        name: "PK_order_line",
        unique: true,
        fields: [
          { name: "order_line_id" },
        ]
      },
    ]
  });
};
