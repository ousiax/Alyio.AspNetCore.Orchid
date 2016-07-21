"use strict";

// parseQuery parses the query string to a json object.
function parseQuery() {
    var json = {};
    var query = window.location.search.slice(1);
    if (query) {
        var pairs = query.split('&');
        pairs.forEach(function (pair) {
            var nameValue = pair.split('=');
            json[nameValue[0]] = decodeURIComponent(nameValue[1] || '');
        });
    }
    return json;
}

// reference from http://stackoverflow.com/questions/1219860/html-encoding-in-javascript-jquery
function htmlEncode(value) {
    //create a in-memory div, set it's inner text(which jQuery automatically encodes)
    //then grab the encoded contents back out.  The div never exists on the page.
    return $('<div/>').text(value).html();
}

function htmlDecode(value) {
    return $('<div/>').html(value).text();
}