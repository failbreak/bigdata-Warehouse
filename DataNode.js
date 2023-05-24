const { MongoClient } = require('mongodb');
const axios = require('axios')
const uri = "mongodb+srv://Failbreak420:cAdSBx6sEggFvBI3@morsur.qggowzp.mongodb.net/";
const client = new MongoClient(uri);

const db = client.db("WebberDB");
const custCollections = db.collection("weather");

function Ingestion(){
    axios(
        {
            method: 'get',url: 'https://dmigw.govcloud.dk/v2/metObs/collections/observation/items?period=latest-10-minutes&api-key=1fa153cf-5edb-40f7-996b-14adcdc497f4'
            
        }).then(function (response)
        { 
            custCollections.insertOne(response.data)
            console.log("logging")
        })
    }

setInterval(Ingestion, 600000);