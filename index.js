"use strict";
var AYLIENTextAPI = require('aylien_textapi');
var textapi = new AYLIENTextAPI({
    application_id: "dffda57f",
    application_key: " 6ed4bd1ffddb8722cf4b27176c10fae5"
});
const express = require("express");
const bodyParser = require("body-parser");

const restService = express();

restService.use(
  bodyParser.urlencoded({
    extended: true
  })
);

restService.use(bodyParser.json());

restService.post("/echo", function (req, res) {
    textapi.classify({ 'text': 'John is a very bad football player!' }, function (error, response) {
        if (error === null) {
            //var speech = response
            return res.json({
                speech: 'hi',
                displayText: 'hellospeech',
                source: "bot"
            });
        }
        else{
            return res.json({
                speech: 'hiiiii',
                displayText: 'hello',
                source: "bot"
            });
    });
  //var speech =
  //  req.body.result &&
  //  req.body.result.parameters &&
  //  req.body.result.parameters.echoText
  //    ? req.body.result.parameters.echoText
  //    : "Seems like some problem. Speak again.";
  //return res.json({
  //  speech: speech,
  //  displayText: speech,
  //  source: "webhook-echo-sample"
  //});
});


restService.listen(process.env.PORT || 8000, function() {
  console.log("Server up and listening");
});
