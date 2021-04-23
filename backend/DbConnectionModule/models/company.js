const Sequelize = require('sequelize');
module.exports = function(sequelize, DataTypes) {
  return sequelize.define('company', {
    company_id: {
      autoIncrement: true,
      type: DataTypes.INTEGER,
      allowNull: false,
      primaryKey: true
    },
    company_name: {
      type: DataTypes.TEXT,
      allowNull: false
    },
    column_tin: {
      type: DataTypes.INTEGER,
      allowNull: false
    },
    company_street: {
      type: DataTypes.TEXT,
      allowNull: false
    },
    company_country: {
      type: DataTypes.TEXT,
      allowNull: false
    },
    company_postal_code: {
      type: DataTypes.TEXT,
      allowNull: false
    }
  }, {
    sequelize,
    tableName: 'company',
    schema: 'dbo',
    timestamps: false,
    indexes: [
      {
        name: "PK_company",
        unique: true,
        fields: [
          { name: "company_id" },
        ]
      },
    ]
  });
};
