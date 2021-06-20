var DataTypes = require("sequelize").DataTypes;
var _AspNetRoleClaims = require("./AspNetRoleClaims");
var _AspNetRoles = require("./AspNetRoles");
var _AspNetUserClaims = require("./AspNetUserClaims");
var _AspNetUserLogins = require("./AspNetUserLogins");
var _AspNetUserRoles = require("./AspNetUserRoles");
var _AspNetUserTokens = require("./AspNetUserTokens");
var _AspNetUsers = require("./AspNetUsers");
var _Locations = require("./Locations");
var _Warehouses = require("./Warehouses");
var ___EFMigrationsHistory = require("./__EFMigrationsHistory");
var _article = require("./article");
var _company = require("./company");
var _log = require("./log");
var _order = require("./order");
var _order_line = require("./order_line");
var _product = require("./product");
var _uom = require("./uom");

function initModels(sequelize) {
  var AspNetRoleClaims = _AspNetRoleClaims(sequelize, DataTypes);
  var AspNetRoles = _AspNetRoles(sequelize, DataTypes);
  var AspNetUserClaims = _AspNetUserClaims(sequelize, DataTypes);
  var AspNetUserLogins = _AspNetUserLogins(sequelize, DataTypes);
  var AspNetUserRoles = _AspNetUserRoles(sequelize, DataTypes);
  var AspNetUserTokens = _AspNetUserTokens(sequelize, DataTypes);
  var AspNetUsers = _AspNetUsers(sequelize, DataTypes);
  var Locations = _Locations(sequelize, DataTypes);
  var Warehouses = _Warehouses(sequelize, DataTypes);
  var __EFMigrationsHistory = ___EFMigrationsHistory(sequelize, DataTypes);
  var article = _article(sequelize, DataTypes);
  var company = _company(sequelize, DataTypes);
  var log = _log(sequelize, DataTypes);
  var order = _order(sequelize, DataTypes);
  var order_line = _order_line(sequelize, DataTypes);
  var product = _product(sequelize, DataTypes);
  var uom = _uom(sequelize, DataTypes);

  AspNetRoles.belongsToMany(AspNetUsers, { as: 'Users', through: AspNetUserRoles, foreignKey: "RoleId", otherKey: "UserId" });
  AspNetUsers.belongsToMany(AspNetRoles, { as: 'Roles', through: AspNetUserRoles, foreignKey: "UserId", otherKey: "RoleId" });
  AspNetRoleClaims.belongsTo(AspNetRoles, { as: "Role", foreignKey: "RoleId"});
  AspNetRoles.hasMany(AspNetRoleClaims, { as: "AspNetRoleClaims", foreignKey: "RoleId"});
  AspNetUserRoles.belongsTo(AspNetRoles, { as: "Role", foreignKey: "RoleId"});
  AspNetRoles.hasMany(AspNetUserRoles, { as: "AspNetUserRoles", foreignKey: "RoleId"});
  AspNetUserClaims.belongsTo(AspNetUsers, { as: "User", foreignKey: "UserId"});
  AspNetUsers.hasMany(AspNetUserClaims, { as: "AspNetUserClaims", foreignKey: "UserId"});
  AspNetUserLogins.belongsTo(AspNetUsers, { as: "User", foreignKey: "UserId"});
  AspNetUsers.hasMany(AspNetUserLogins, { as: "AspNetUserLogins", foreignKey: "UserId"});
  AspNetUserRoles.belongsTo(AspNetUsers, { as: "User", foreignKey: "UserId"});
  AspNetUsers.hasMany(AspNetUserRoles, { as: "AspNetUserRoles", foreignKey: "UserId"});
  AspNetUserTokens.belongsTo(AspNetUsers, { as: "User", foreignKey: "UserId"});
  AspNetUsers.hasMany(AspNetUserTokens, { as: "AspNetUserTokens", foreignKey: "UserId"});
  article.belongsTo(Locations, { as: "Location", foreignKey: "LocationId"});
  Locations.hasMany(article, { as: "articles", foreignKey: "LocationId"});
  AspNetUsers.belongsTo(Warehouses, { as: "Warehouse", foreignKey: "WarehouseId"});
  Warehouses.hasMany(AspNetUsers, { as: "AspNetUsers", foreignKey: "WarehouseId"});
  Locations.belongsTo(Warehouses, { as: "Warehouse", foreignKey: "WarehouseId"});
  Warehouses.hasMany(Locations, { as: "Locations", foreignKey: "WarehouseId"});
  product.belongsTo(Warehouses, { as: "Warehouse", foreignKey: "WarehouseId"});
  Warehouses.hasMany(product, { as: "products", foreignKey: "WarehouseId"});
  order.belongsTo(company, { as: "Company", foreignKey: "CompanyId"});
  company.hasMany(order, { as: "orders", foreignKey: "CompanyId"});
  product.belongsTo(company, { as: "Company", foreignKey: "CompanyId"});
  company.hasMany(product, { as: "products", foreignKey: "CompanyId"});
  order_line.belongsTo(order, { as: "Order", foreignKey: "OrderId"});
  order.hasMany(order_line, { as: "order_lines", foreignKey: "OrderId"});
  article.belongsTo(product, { as: "Product", foreignKey: "ProductId"});
  product.hasMany(article, { as: "articles", foreignKey: "ProductId"});
  order_line.belongsTo(product, { as: "Product", foreignKey: "ProductId"});
  product.hasMany(order_line, { as: "order_lines", foreignKey: "ProductId"});
  product.belongsTo(uom, { as: "UnitOfMessure", foreignKey: "UnitOfMessureId"});
  uom.hasMany(product, { as: "products", foreignKey: "UnitOfMessureId"});

  return {
    AspNetRoleClaims,
    AspNetRoles,
    AspNetUserClaims,
    AspNetUserLogins,
    AspNetUserRoles,
    AspNetUserTokens,
    AspNetUsers,
    Locations,
    Warehouses,
    __EFMigrationsHistory,
    article,
    company,
    log,
    order,
    order_line,
    product,
    uom,
  };
}
module.exports = initModels;
module.exports.initModels = initModels;
module.exports.default = initModels;
