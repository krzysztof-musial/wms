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
//LOAD LOG MODULE 

// const Logs = require('../WMS.LogsModule')(models);
// //TERST LOG MODULE 
// (async () => {
//    const d = await Logs.log(1, 2, { dasdas: "asdsdfqkhwjbedoi123" })
//    console.log(`Just got new log id: ${d.log_id}`) })();
module.exports = models;