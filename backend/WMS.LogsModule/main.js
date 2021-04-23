var models;
class Logs {
    constructor(dbConn) {
        models = dbConn
    }
    async log(userID, warehouseID, data) {
        // check user id 
        // check warehouse id 
        // dropa data 
        // if (typeof userID === "string") {
        //     const d = await knex('user').where({
        //         user_id: userID
        //     }).select("1").limit(1);
        //     if (d != "1") {
        //         throw new Error(`There is no user with id ${userID}`)
        //     }
        // }
        // if (typeof warehouseID === "string") {
        //     const e = await knex('warehouse').where({
        //         warehouse_id: warehouseID
        //     }).select("1").limit(1);
        //     if (e != "1") {
        //         throw new Error(`There is no warehouse with id ${warehouseID}`)
        //     }
        // }
        return await models.log.create({user_id:userID,warehouse_id:warehouseID,log_message:JSON.stringify(data)})
    }
}

module.exports = (models) => new Logs(models);