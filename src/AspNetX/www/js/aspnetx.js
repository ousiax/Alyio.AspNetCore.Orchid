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

// Api Groups -- index.html
function loadApiGroupsHtml() {
    $.getJSON("apigroups/", function (data, status) {
        var html = "<section id=\"main-content\">";
        html += "<div class=\"bg-primary well-sm api-banner\">";
        html += "<h3><small class=\"api-doc api-doc-sm\">" + htmlEncode(data.description || '') + "</small></h3>";
        html += "</div>";

        data.items.forEach(function (group) {
            html += "<div class=\"panel panel-info\">";
            html += "<div class=\"panel-heading\">";
            html += "<h1 class=\"panel-title\">";
            html += "<a data-toggle=\"collapse\" href=\"#api-" + group.groupName + "\">" + group.groupName + " <sup>(" + group.items.length + ")</sup></a>";
            html += "<span class=\"api-doc\">" + htmlEncode(group.description || '') + "</span>";
            html += "</h1>";
            html += "</div>";
            html += "<div id=\"api-" + group.groupName + "\" class=\"panel-collapse collapse in\">";
            html += "<div class=\"panel-body\">";
            html += "<table class=\"table table-hover\">";
            html += "<tbody>";
            group.items.forEach(function (api) {
                html += "<tr>";
                html += "<td><a class=\"" + getHttpMethodCssClass(api.httpMethod) + "\" href=\"api.html?id=" + api.id + "\">" + api.httpMethod + "</a></td>";
                html += "<td><a href=\"api.html?id=" + api.id + "\">" + api.relativePath + "</a></td>";
                html += "<td>" + htmlEncode(api.description || '') + "</td>";
                html += "</tr>";
            });
            html += "</tbody>";
            html += "</table>";
            html += "</div>";
            html += "</div>";
            html += "</div>";
        });
        $("#main-content").html(html);
    });
}

function getHttpMethodCssClass(httpMethod) {
    if (httpMethod == "POST") {
        return 'http-method btn btn-warning';
    }
    if (httpMethod == "PUT") {
        return 'http-method btn btn-info';
    }
    if (httpMethod == "DELETE") {
        return 'http-method btn btn-danger';
    }
    return 'http-method btn btn-primary';
}

// Api -- api.html
function loadApiHtml() {
    var query = parseQuery();
    var id = query.id || '';
    $.getJSON('api/' + id, function (data, status) {
        var html = getBannerHtml(data);
        if (data.requestInformation.uriParameterDescriptions.length || data.requestInformation.bodyParameterDescriptions.length) {
            html += getRequestInformationHtml(data.requestInformation);
        }
        if (data.responseInformation.supportedResponseTypeMetadatas.length) {
            html += getResponseInformationHtml(data.responseInformation);
        }
        $("#main-content").html(html);
    });
}

function getBannerHtml(data) {
    var html = "<div class=\"bg-primary well-sm api-banner\">";
    html += "<h3 data-toggle=\"tooltip\" title=\"" + data.description + "\">";
    html += "<span class=\"http-method\">" + data.httpMethod + "</span>";
    html += "<span class=\"relative-path\">" + data.relativePath + "</span></h3></div>"
    return html;
}

function getRequestInformationHtml(requestInformation) {
    var html = "<div class=\"panel panel-info\">";
    html += "<div class=\"panel-heading\">";
    html += "<a class=\"panel-title\" data-toggle=\"collapse\" href=\"#request-information\">Request Information</a>";
    html += "</div>";
    html += "<div id=\"request-information\" class=\"panel-collapse collapse in\">";
    html += "<div class=\"panel-body\">";
    html += "<div class=\"panel-group\">";
    if (requestInformation.uriParameterDescriptions.length) {
        html += getUriParametersHtml(requestInformation.uriParameterDescriptions);
    }
    if (requestInformation.bodyParameterDescriptions.length) {
        requestInformation.bodyParameterDescriptions.forEach(function (parameterDescription) {
            html += getBodyParametersHtml(parameterDescription);
        });
    }
    if (!$.isEmptyObject(requestInformation.supportedRequestSamples)) {
        html += getSupportedRequestSamplesHtml(requestInformation.supportedRequestSamples);
    }
    html += "</div>";
    html += "</div>";
    html += "</div>";
    html += "</div>";
    return html;
}

function getUriParametersHtml(uriParameterDescriptions) {
    var html = "<div class=\"panel panel-info\">";
    html += "<div class=\"panel-heading\">";
    html += "<a class=\"panel-title\" data-toggle=\"collapse\" href=\"#uri-parameter\">URI Parameters</a>";
    html += "</div>";
    html += "<div id=\"uri-parameter\" class=\"panel-collapse collapse in\">";
    html += "<div class=\"panel-body\">";
    html += "<table class=\"table table-hover\">";
    html += "<thead>";
    html += "<tr>";
    html += "<th>Name</th>";
    html += "<th>Type</th>";
    html += "<th>Description</th>";
    html += "</tr>";
    html += "</thead>";
    uriParameterDescriptions.forEach(function (parameter) {
        html += "<tr>";
        html += "<td>" + parameter.name + "</td>";
        html += "<td>" + htmlEncode(parameter.type) + "</td>";
        html += "<td>" + htmlEncode(parameter.description) || '' + "</td>";
        html += "</tr>";
    });
    html += "</table>";
    html += "</div>";
    html += "</div>";
    html += "</div>";
    return html;
}

function getBodyParametersHtml(bodyParameterDescription) {
    var html = "<div class=\"panel panel-info\">";
    html += "<div class=\"panel-heading\">";
    html += "<a class=\"panel-title\" data-toggle=\"collapse\" href=\"#body-parameters\">Body Parameters</a>";
    html += "</div>";
    html += "<div id=\"body-parameters\" class=\"panel-collapse collapse in\">";
    html += "<div class=\"panel-body\">";
    var meta = bodyParameterDescription.metadata;
    html += "<a href=\"meta.html?id=" + meta.modelTypeId + "\" data-toggle=\"tooltip\" title=\"" + bodyParameterDescription.description + "\">" + htmlEncode(meta.modelType) + "</a>";
    html += "<table class=\"table table-hover\">";
    html += "<thead>";
    html += "<tr>";
    html += "<th>Name</th>";
    html += "<th>Type</th>";
    html += "<th>Description</th>";
    html += "</tr>";
    html += "</thead>";
    html += "<tbody>";
    if (meta.properties) {
        meta.properties.forEach(function (property) {
            html += "<tr>";
            html += "<td>" + property.propertyName + "</td>";
            html += "<td><a href=\"meta.html?id=" + property.modelTypeId + "\">" + htmlEncode(property.modelType) + "</a></td>";
            html += "<td>" + htmlEncode(property.description) + "</td>";
            html += "</tr>";
        });
    }
    html += "</tbody>";
    html += "</table>";
    html += "</div>";
    html += "</div>";
    html += "</div>";
    return html;
}

function getSupportedRequestSamplesHtml(supportedRequestSamples) {
    var html = "<div class=\"panel panel-info\">";
    html += "<div class=\"panel-heading\">";
    html += "<a class=\"panel-title\" data-toggle=\"collapse\" href=\"#request-formats\">Request Formats</a>";
    html += "</div>";
    html += "<div id=\"request-formats\" class=\"panel-collapse collapse in\">";
    html += "<div class=\"panel-body\">";
    for (var mimeTypeName in supportedRequestSamples) {
        html += "<h4 class=\"mime-type-name\"><span class=\"label label-primary\">" + mimeTypeName + "</span></h4>";
        html += "<pre><code>" + JSON.stringify(supportedRequestSamples[mimeTypeName], null, 4) + "</code></pre>";
    }
    html += "</div>";
    html += "</div>";
    html += "</div>";
    return html;
}

function getResponseInformationHtml(responseInformation) {
    var html = "<div class=\"panel panel-info\">";
    html += "<div class=\"panel-heading\">";
    html += "<a class=\"panel-title\" data-toggle=\"collapse\" href=\"#response-information\">Response Information</a>";
    html += "</div>";
    html += "<div id=\"response-information\" class=\"panel-collapse collapse in\">";
    html += "<div class=\"panel-body\">";
    html += "<div class=\"panel-group\">";
    responseInformation.supportedResponseTypeMetadatas.forEach(function (metadata) {
        html += "<div class=\"panel panel-info\">";
        html += "<div class=\"panel-heading\">";
        html += "<a class=\"panel-title\" data-toggle=\"collapse\" href=\"#resource-description\">Resource Description</a>";
        html += "</div>";
        html += "<div id=\"resource-description\" class=\"panel-collapse collapse in\">";
        html += "<div class=\"panel-body\">";
        if (metadata.isEnumerableType || metadata.isCollectionType) {
            html += getMetadataTableHtml(metadata, metadata.elementMetadata);
        } else {
            html += getMetadataTableHtml(metadata, metadata);
        }
        html += "</div>";
        html += "</div>";
        html += "</div>";
    });
    if (!$.isEmptyObject(responseInformation.supportedResponseSamples)) {
        html += getSupportedResponseSamplesHtml(responseInformation.supportedResponseSamples);
    }
    html += "</div>";
    html += "</div>";
    html += "</div>";
    html += "</div>";
    return html;
}

function getSupportedResponseSamplesHtml(supportedResponseSamples) {
    var html = "<div class=\"panel panel-info\">";
    html += "<div class=\"panel-heading\">";
    html += "<a class=\"panel-title\" data-toggle=\"collapse\" href=\"#reponse-formats\">Reponse Formats</a>";
    html += "</div>";
    html += "<div id=\"reponse-formats\" class=\"panel-collapse collapse in\">";
    html += "<div class=\"panel-body\">";
    for (var mimeTypeName in supportedResponseSamples) {
        html += "<h4 class=\"mime-type-name\"><span class=\"label label-primary\">" + mimeTypeName + "</span></h4>";
        html += "<pre><code>" + JSON.stringify(supportedResponseSamples[mimeTypeName], null, 4) + "</code></pre>";
    }
    html += "</div>";
    html += "</div>";
    html += "</div>";
    return html;
}

// Metadata - meta.html
function loadMetadataHtml() {
    var query = parseQuery();
    var id = query.id || '';
    $.getJSON('meta/' + id, function (metadata, status) {
        var html = "<div class=\"bg-primary well-sm api-banner\">";
        html += "<h3>" + htmlEncode(metadata.modelType) + "<small class=\"api-doc api-doc-sm\">" + htmlEncode(metadata.description) + "</small></h3>";
        html += "</div>";

        if (metadata.properties && metadata.properties.length || metadata.isEnum) {
            html += "<div class=\"panel panel-info\">";
            html += "<div class=\"panel-heading\">";
            html += "<a class=\"panel-title\" data-toggle=\"collapse\" href=\"#resource-description\">Resource Description</a>";
            html += "</div>";
            html += "<div id=\"resource-description\" class=\"panel-collapse collapse in\">";
            html += "<div class=\"panel-body\">";
            if (metadata.isEnumerableType || metadata.isCollectionType) {
                html += getMetadataTableHtml(metadata, metadata.elementMetadata);
            } else {
                html += getMetadataTableHtml(metadata, metadata);
            }
            html += "</div>";
            html += "</div>";
            html += "</div>";
        }
        $("#main-content").html(html);
    });
}

function getMetadataTableHtml(containerMetadata, metadata) {
    var html = "<a href=\"meta.html?id=" + containerMetadata.modelTypeId + "\" data-toggle=\"tooltip\" title=\"" + htmlEncode(containerMetadata.description) + "\">" + htmlEncode(containerMetadata.modelType) + "</a>";

    if (metadata.properties && metadata.properties.length || metadata.isEnum) {
        html += "<table class=\"table table-hover\">";
        html += "<thead>";
        html += "<tr>";
        html += "<th>Name</th>";
        html += "<th>Type</th>";
        html += "<th>Description</th>";
        html += "</tr>";
        html += "</thead>";
        html += "<tbody>";
        if (metadata.properties && metadata.properties.length) {
            metadata.properties.forEach(function (property) {
                html += "<tr>";
                html += "<td>" + property.propertyName + "</td>";
                html += "<td><a href=\"meta.html?id=" + property.modelTypeId + "\">" + htmlEncode(property.modelType) + "</a></td>";
                html += "<td>" + htmlEncode(property.description) + "</td>";
                html += "</tr>";
            });
        } else if (metadata.isEnum) {
            for (var enumName in metadata.enumNamesAndValues) {
                html += "<tr>";
                html += "<td>" + enumName + "</td>";
                html += "<td>" + metadata.enumNamesAndValues[enumName] + "</td>";
                html += "<td></td>";
                html += "</tr>";
            }
        }
        html += "</tbody>";
        html += "</table>";
    }
    return html;
}

// About -- about.html

function loadAbout() {
    $.get("about", function (data, status) {
        var reader = new commonmark.Parser();
        var writer = new commonmark.HtmlRenderer();
        var parsed = reader.parse(data); // parsed is a 'Node' tree
        // transform parsed if you like...
        var result = writer.render(parsed); // result is a String
        $("#main-content").html(result);
    });
}