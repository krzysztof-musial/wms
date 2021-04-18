const Sequelize = require('sequelize');
module.exports = function(sequelize, DataTypes) {
  return sequelize.define('article', {
    article_id: {
      autoIncrement: true,
      type: DataTypes.INTEGER,
      allowNull: false,
      primaryKey: true
    },
    ProductId: {
      type: DataTypes.STRING(450),
      allowNull: true,
      references: {
        model: 'product',
        key: 'product_id'
      }
    },
    LocationId: {
      type: DataTypes.INTEGER,
      allowNull: true,
      references: {
        model: 'Locations',
        key: 'location_id'
      }
    },
    article_quantity: {
      type: DataTypes.DECIMAL(18,2),
      allowNull: false
    }
  }, {
    sequelize,
    tableName: 'article',
    schema: 'dbo',
    timestamps: false,
    indexes: [
      {
        name: "IX_article_LocationId",
        fields: [
          { name: "LocationId" },
        ]
      },
      {
        name: "IX_article_ProductId",
        fields: [
          { name: "ProductId" },
        ]
      },
      {
        name: "PK_article",
        unique: true,
        fields: [
          { name: "article_id" },
        ]
      },
    ]
  });
};
