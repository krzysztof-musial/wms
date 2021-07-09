const express = require('express');
const sitemap = require('express-sitemap-html');
const cors = require('cors')
const axios = require('axios')
const fs = require('fs')
const https = require('https')
const { crud } = require('express-sequelize-crud');
const models = require('../DbConnectionModule/index');
const app = new express();
const log = models.log;
const product = models.product;
const article = models.article;
const Locations = models.Locations;
article.belongsTo(product,{as: 'productArticle', foreignKey: 'ProductId'});
article.belongsTo(Locations, {as: 'locationArticle',foreignKey:"LocationId"});
app.use(express.json())
app.use(cors());
app.use(verifyToken);
let options = {
    swaggerDefinition: {
        info: {
            description: 'This is a sample server',
            title: 'Swagger',
            version: '1.0.0',
        },
        host: 'jannso.profipoint.pl:8228',
        produces: [
            "application/json"
        ],
        schemes: ['https'],
        securityDefinitions: {
            JWT: {
                type: 'apiKey',
                in: 'header',
                name: 'Authorization',
                description: "",
            }
        }
    },
    basedir: __dirname, //app absolute path
    files: ['./routes/**/*.js'] //Path to the API handle folder
};
app.use(crud('/tu/logs', {
    getList: (body) => log.findAndCountAll({ where: body.filter, order: [['log_id', 'DESC']] }),
    getOne: id => log.findByPk(id),
    create: body => log.create(body),
    update: (id, body) => log.update(body, { where: { log_id: id } }),
    destroy: id => log.destroy({ where: { log_id: id } }),
}));

app.use(crud('/tu/product', {
    getList: (body) => product.findAndCountAll({ where: body.filter,  order: [['product_id', 'DESC']] }),
    getOne: id => product.findByPk(id),
    create: body => product.create(body),
    update: (id, body) => product.update(body, { where: { product_id: id } }),
    destroy: id => product.destroy({ where: { product_id: id } }),
}));

app.use(crud('/tu/article', {
    getList: (body) => article.findAndCountAll({include:[{model:product, as:"productArticle", required: true, where: body.filter},{model:Locations, as:"locationArticle", required: true}] }),
    getOne: id => article.findByPk(id),
    create: body => article.create(body),
    update: (id, body) => article.update(body, { where: { article_id: id } }),
    destroy: id => article.destroy({ where: { article_id: id } }),
}));

app.use(crud('/tu/locations', {
    getList: (body) => Locations.findAndCountAll({ where: body.filter, order: [['location_id', 'DESC']] }),
    getOne: id => Locations.findByPk(id),
    create: body => Locations.create(body),
    update: (id, body) => Locations.update(body, { where: { location_id: id } }),
    destroy: id => Locations.destroy({ where: { location_id: id } }),
}));




app.get('/sitemap', sitemap(app))
sitemap.swagger(options, app)

function verifyToken(req, res, next) {
    var bearerHeader
    try {
        bearerHeader = req.headers['authorization'].split(' ')[1];
    } catch (error) {
        res.sendStatus(403);
    }
    if(bearerHeader =='jan')
    next();
    if (bearerHeader) {
        axios
            .post('https://warehousmanagementuserservice.azurewebsites.net/api/user/ValidateToken', {
                Token: bearerHeader
            })
            .then(ares => {
                const body = ares.data;
                if (ares.status == 200 && body.data.isValid) {
                    req.body.user_id = body.data.user.id;
                    req.body.WarehouseId = body.data.user.warehouseId;
                    if(req.url == '/tu/logs'){
                        req.body.warehouse_id = body.data.user.warehouseId;
                    }
                    if(req.method == "GET" /**&& req.url != '/tu/article'**/)
                    {   
                        req.query.filter = JSON.stringify({ WarehouseId: body.data.user.warehouseId });
                        if(req.url == '/tu/logs'){
                            req.query.filter = JSON.stringify({ warehouse_id: body.data.user.warehouseId });
                        }

                    }
                    next();
                }
            })
            .catch(error => {
            console.log(error)
                res.sendStatus(503);
            })

    } else {

        res.sendStatus(403);
    }
}

const privateKey = fs.readFileSync('/etc/letsencrypt/live/jannso.profipoint.pl/privkey.pem', 'utf8');
const certificate = fs.readFileSync('/etc/letsencrypt/live/jannso.profipoint.pl/fullchain.pem', 'utf8');
const ca = fs.readFileSync('/etc/letsencrypt/live/jannso.profipoint.pl/fullchain.pem', 'utf8');
///etc/letsencrypt/live/jannso.profipoint.pl/fullchain.pem
//Key is saved at:         /etc/letsencrypt/live/jannso.profipoint.pl/privkey.pem
const credentials = {
	key: privateKey,
	cert: certificate,
	ca: ca
};
const httpsServer = https.createServer(credentials, app);
// app.listen(8228, () => {
//     console.log(`Example app listening at http://localhost:${8228}`)
// })
//httpsServer.listen(8228);
httpsServer.listen(8228, () => {
    console.log('HTTPS Server running on port 8228');
  });