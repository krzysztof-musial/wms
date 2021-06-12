const { Sequelize } = require('sequelize');
const sequelize = new Sequelize('wmstest', 'dkornat', 'DSAewq321', {
  host: 'warehousemanagement.database.windows.net',
  encrypt: true,
  dialect: 'mssql'
});
// TEST CONNECTION 
(async () => {
  try {
    await sequelize.authenticate();
    console.log('Connection has been established successfully.');
  } catch (error) {
    console.error('Unable to connect to the database:', error);
  }
})();
// LOAD MODELS 
var initModels = require("./models/init-models");
var models = initModels(sequelize);

module.exports = models;