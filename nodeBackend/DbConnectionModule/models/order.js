const Sequelize = require('sequelize');
module.exports = function(sequelize, DataTypes) {
  return sequelize.define('order', {
    order_id: {
      autoIncrement: true,
      type: DataTypes.INTEGER,
      allowNull: false,
      primaryKey: true
    },
    CompanyId: {
      type: DataTypes.INTEGER,
      allowNull: true,
      references: {
        model: 'company',
        key: 'company_id'
      }
    },
    order_name: {
      type: DataTypes.TEXT,
      allowNull: true
    }
  }, {
    sequelize,
    tableName: 'order',
    schema: 'dbo',
    timestamps: false,
    indexes: [
      {
        name: "IX_order_CompanyId",
        fields: [
          { name: "CompanyId" },
        ]
      },
      {
        name: "PK_order",
        unique: true,
        fields: [
          { name: "order_id" },
        ]
      },
    ]
  });
};
