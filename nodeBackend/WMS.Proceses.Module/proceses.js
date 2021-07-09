const express = require('express');
const cors = require('cors');
const axios = require('axios');
const fs = require('fs');
const https = require('https');
const models = require('../DbConnectionModule/index');
const pino = require('pino');
const logger = pino({ prettyPrint: true }, pino.destination('./logs.log'));
const app = new express();
const log = models.log;
const product = models.product;
const article = models.article;
const Locations = models.Locations;
article.belongsTo(product, { as: 'productArticle', foreignKey: 'ProductId' });
article.belongsTo(Locations, { as: 'locationArticle', foreignKey: "LocationId" });
app.use(express.json())
app.use(cors());
app.use(verifyToken);


app.post('/tam/import', async (req, res) => {
    logger.info(`${req.url} ${JSON.stringify(req.body)}`)

    const body = req.body;
    const l = await Locations.findByPk(body.location);
    const p = await product.findByPk(body.product);
    const qty = body.amount;
    const a = await article.findOne({ where: { LocationId: l.location_id, ProductId: p.product_id } });
    if (a == null || a.article_id == null || a.article_id == undefined) {
        await article.create({ ProductId: p.product_id, LocationId: l.location_id, article_quantity: qty })
        res.status(200)
        return res.json({ ok: true });
    }
    else {
        article.update({ article_quantity: (Number(qty) + Number(a.article_quantity)) }, {
            where: {
                article_id: a.article_id
            }
        });
    }

    res.status(200)
    return res.json({ ok: true });
});

app.post('/tam/move', async (req, res) => {
    logger.info(`${req.url} ${JSON.stringify(req.body)}`)

    const body = req.body;
    const location_src = await Locations.findByPk(body.locationFrom);
    const location_dst = await Locations.findByPk(body.locationTo);
    const p = await product.findByPk(body.product);
    const src_a = await article.findOne({ where: { LocationId: location_src.location_id, ProductId: p.product_id } });
    const dst_a = await article.findOne({ where: { LocationId: location_dst.location_id, ProductId: p.product_id } });
    const qty = body.amount;
    if (src_a.article_quantity == qty) {
        if (dst_a == null || dst_a.article_id == null || dst_a.article_id == undefined) {
            await article.update({ LocationId: location_dst.location_id }, {
                where: {
                    article_id: src_a.article_id
                }
            });
        }
        else {
            await article.update({ article_quantity: (Number(qty) + Number(dst_a.article_quantity)) }, {
                where: {
                    article_id: dst_a.article_id
                }
            });
            await article.destroy({ where: { article_id: src_a.article_id } });
        }
        res.status(200)
        return res.json({ ok: true });
    }
    if (Number(src_a.article_quantity) > Number(qty)) {
        if (dst_a == null || dst_a.article_id == null || dst_a.article_id == undefined) {
            await article.create({ ProductId: p.product_id, LocationId: location_dst.location_id, article_quantity: qty })
            await article.update({ article_quantity: Number(src_a.article_quantity) - (Number(qty)) }, {
                where: {
                    article_id: src_a.article_id
                }
            });
        }
        else {
            await article.update({ article_quantity: (Number(qty) + Number(dst_a.article_quantity)) }, {
                where: {
                    article_id: dst_a.article_id
                }
            });
            await article.update({ article_quantity: Number(src_a.article_quantity) - (Number(qty)) }, {
                where: {
                    article_id: src_a.article_id
                }
            });
        }
        res.status(200)
        return res.json({ ok: true });
    }
    if (Number(src_a.article_quantity) < Number(qty)) {
        res.status(503)
        return res.json({ error: "amount is bigger than article qty" });
    }
});

app.post('/tam/export', async (req, res) => {
    logger.info(`${req.url} ${JSON.stringify(req.body)}`)

    const body = req.body;
    const l = await Locations.findByPk(body.location);
    const p = await product.findByPk(body.product);
    const qty = body.amount;
    const a = await article.findOne({ where: { LocationId: l.location_id, ProductId: p.product_id } });
    if (a == null || a.article_id == null || a.article_id == undefined || (Number(a.article_quantity) < Number(qty))) {
        res.status(503)
        return res.json({ error: "amount is bigger than article qty" });
    }
    if (Number(a.article_quantity) == Number(qty)) {
        await article.destroy({ where: { article_id: a.article_id } });
        res.status(200)
        return res.json({ ok: true });
    }
    if (Number(a.article_quantity) > Number(qty)) {
        await article.update({ article_quantity: Number(a.article_quantity) - (Number(qty)) }, {
            where: {
                article_id: a.article_id
            }
        });
    }
    res.status(200)
    return res.json({ ok: true });
});


function verifyToken(req, res, next) {

    var bearerHeader
    try {
        bearerHeader = req.headers['authorization'].split(' ')[1];
    } catch (error) {
        res.sendStatus(403);
    }

    if (bearerHeader) {
        axios
            .post('https://warehousmanagementuserservice.azurewebsites.net/api/user/ValidateToken', {
                Token: bearerHeader
            })
            .then(ares => {
                const body = ares.data;
                if (ares.status == 200 && body.data.isValid) {

                    req.body.WarehouseId = body.data.user.warehouseId;
                    if (req.url == '/tu/logs') {
                        req.body.warehouse_id = body.data.user.warehouseId;
                    }
                    if (req.method == "GET" /**&& req.url != '/tu/article'**/) {
                        req.query.filter = JSON.stringify({ WarehouseId: body.data.user.warehouseId });
                        if (req.url == '/tu/logs') {
                            req.query.filter = JSON.stringify({ warehouse_id: body.data.user.warehouseId });
                        }

                    }
                    return next();
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

httpsServer.listen(8282, () => {
    console.log('HTTPS Server running on port 8282');
});