"use strict";

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

function loadApis() {
    $.getJSON("/aspnetx/apigroups", function (data, status) {
        var text = "";
        $.each(data.Items, function (_, apiGroup) {
            text += "<h2 id=\"" + apiGroup["GroupName"] + "\">" + apiGroup["GroupName"] + "</h2>";
            text += "<p>No documentation available.</p>";
            text += "<table class=\"aspnetx-table\">";
            text += "<thead>";
            text += "<tr><th>API</th><th>Description</th></tr>";
            text += "</thead>";
            text += "<tbody>";
            $.each(apiGroup.Items, function (_, api) {
                text += "<tr>";
                text += "<td class=\"api-name\">";
                var query = {};
                query.id = api.Id;
                query.method = api.HttpMethod;
                query.relativePath = api.RelativePath;
                text += "<a href=\"api.html?" + $.param(query) + "\">" + api.HttpMethod + " " + api.RelativePath + "</a>";
                text += "</td>";
                text += "<td class=\"api-documentation\">";
                text += "<p>No documentation available.</p>";
                text += "</td>";
                text += "</tr>";
            });
            text += "</tbody>";
            text += "</table>";
        });
        $("#aspnetx-content").html(text);
    });
}

function loadApi() {
    var query = parseQuery();
    var url = "/aspnetx/api/" + query.id;
    $.getJSON(url, function (data, status) {
        var text = "";
        text += "<h1>" + query.method + " " + query.relativePath + "</h1>";
        text += "<div>";
        text += "<p>None</p>";
        text += "<h2>Request Information</h2>";
        text += "<h3>URI Parameters</h3>"
        text += getUriParameterHtml(data.UriParameters);
        text += "<h3>Body Parameters</h3>";
        text += "<p>" + (data.RequestDocumentation || '') + "</p>";
        if (data.BodyParameter) {
            text += getMetadataHtml(data.BodyParameter.MetadataWrapper);
        } else {
            text += "<p>None.</p>";
        }
        if (data.SampleRequests) {
            text += "<h3>Request Formats</h3>";
            text += "<div>";
            for (var mediaType in data.SampleRequests) {
                text += "<h4 class=\"sample-header\">" + mediaType + "</h4>";
                text += "<div class=\"sample-content\">";
                text += "<span><b>Sample:</b></span>";
                var sample = data.SampleRequests[mediaType];
                if (!sample) {
                    text += "<p>Sample not available.</p>";
                }
                else {
                    text += "<pre class=\"wrapped\"><code>" + JSON.stringify(sample, null, 4) + "</code></pre>";
                }
                text += "</div>";
            }
            text += "</div>";
        }
        text += "<h2>Response Information</h2>";
        text += "<h3>Resource Description</h3>";
        text += "<p>" + 'None' + "</p>"; // data.ResponseDescription.Documentation
        if (data.ResponseModelMetadataWrapper) {
            text += getMetadataHtml(data.ResponseModelMetadataWrapper);
        } else {
            text += "<p>None.</p>";
        }
        if (data.SampleResponses) {
            text += "<h3>Response Formats</h3>";
            text += "<div>";
            for (var mediaType in data.SampleResponses) {
                text += "<h4 class=\"sample-header\">" + mediaType + "</h4>";
                text += "<div class=\"sample-content\">";
                text += "<span><b>Sample:</b></span>";
                var sample = data.SampleResponses[mediaType];
                if (!sample) {
                    text += "<p>Sample not available.</p>";
                }
                else {
                    text += "<pre class=\"wrapped\"><code>" + JSON.stringify(sample, null, 4) + "</code></pre>";
                }
                text += "</div>";
            }
            text += "</div>";
        }
        text += "</div>";
        $("#aspnetx-content").html(text);
    });
}

function loadMeta() {
    var query = parseQuery();
    var url = "/aspnetx/meta/" + query.id;
    $.getJSON(url, function (data, status) {
        var text = getMetaHtml(data);
        $("#aspnetx-model-name").html(htmlEncode(data.ModelType));
        $("#aspnetx-model-doc").html(htmlEncode((data.documentation || 'None')));
        $("#aspnetx-content").html(text);
    });
}

function getMetaHtml(meta) {
    var text = "NotSupported.";
    if (meta.IsEnum) {
        text = getEnumMetaHtml(meta);
    } else if (meta.IsEnumerableType) {
        text = getComplexMetaHtml(meta.ElementMetadataWrapper);
    } else {
        text = getComplexMetaHtml(meta);
    }
    return text;
}

function getEnumMetaHtml(meta) {
    var text = "";
    if (meta) {
        text += "<p>Possible enumeration values:</p>";
        text += "";
        text += "<table class=\"aspnetx-table\">";
        text += "<thead>";
        text += "<tr><th>Name</th><th>Value</th><th>Description</th></tr>";
        text += "</thead>";
        text += "<tbody>";
        if (meta.EnumNamesAndValues) {
            for (var name in meta.EnumNamesAndValues) {
                text += "<tr>";
                text += "<td class=\"enum-name\"><b>" + name + "</b></td>";
                text += "<td class=\"enum-value\">";
                text += "<p>" + meta.EnumNamesAndValues[name] + "</p>";
                text += "</td>";
                text += "<td class=\"enum-description\">";
                text += "<p>" + "None" + "</p>";
                text += "</td>";
                text += "</tr>";
            };
        }
        text += "</tbody>";
        text += "</table>";
    }
    return text;
}

function getComplexMetaHtml(meta) {
    var text = "";
    if (meta) {
        text += "<table class=\"aspnetx-table\">";
        text += "<thead>";
        text += "<tr><th>Name</th><th>Description</th><th>Type</th><th>Additional information</th></tr>";
        text += "</thead>";
        text += "<tbody>";
        if (meta.Properties) {
            meta.Properties.forEach(function (property) {
                text += "<tr>";
                text += "<td class=\"parameter-name\">" + property.PropertyName + "</td>";
                text += "<td class=\"parameter-documentation\">";
                text += "<p>" + (property.Documentation || "None") + "</p>";
                text += "</td>";
                text += "<td class=\"parameter-type\">";
                text += "<a href=\"meta.html?id=" + encodeURIComponent(property.Id) + "\">" + htmlEncode(property.ModelType) + "</a>";
                text += "</td>";
                text += "<td class=\"parameter-annotations\">";
                text += "<p>None.</p>";
                text += "</td>";
                text += "</tr>";
            });
        }
        text += "</tbody>";
        text += "</table>";
    } else {
        text = "<p>None.</p>";
    }
    return text;
}

function getUriParameterHtml(parameters) {
    var text = "";
    if (parameters) {
        text += "<table class=\"aspnetx-table\">";
        text += "<thead>";
        text += "<tr><th>Name</th><th>Description</th><th>Type</th><th>Additional information</th></tr>";
        text += "</thead>";
        text += "<tbody>";
        parameters.forEach(function (parameter) {
            text += "<tr>";
            text += "<td class=\"parameter-name\">" + parameter.Name + "</td>";
            text += "<td class=\"parameter-documentation\">";
            text += "<p>" + (parameter.Documentation || "None") + "</p>";
            text += "</td>";
            text += "<td class=\"parameter-type\">";
            text += "<a href=\"meta.html?id=" + encodeURIComponent(parameter.MetadataWrapper.Id) + "\">" + htmlEncode(parameter.MetadataWrapper.ModelType) + "</a>";
            text += "</td>";
            text += "<td class=\"parameter-annotations\">";
            text += "<p>None.</p>";
            text += "</td>";
            text += "</tr>";
        });
        text += "</tbody>";
        text += "</table>";
    } else {
        text = "<p>None.</p>";
    }
    return text;
}

function getMetadataHtml(meta) {
    var text = "";
    if (meta) {
        text += "<a href=\"meta.html?id=" + encodeURIComponent(meta.Id) + "\">" + htmlEncode(meta.ModelType) + "</a>";
        text += "<table class=\"aspnetx-table\">";
        text += "<thead>";
        text += "<tr><th>Name</th><th>Description</th><th>Type</th><th>Additional information</th></tr>";
        text += "</thead>";
        text += "<tbody>";
        var properties = meta.Properties;
        if (meta.IsCollectionType || meta.IsEnumerableType) { //TODO meta.IsEnumerableType ?
            properties = meta.ElementMetadataWrapper.Properties;
        }
        if (properties) {
            properties.forEach(function (property) {
                text += "<tr>";
                text += "<td class=\"parameter-name\">" + property.PropertyName + "</td>";
                text += "<td class=\"parameter-documentation\">";
                text += "<p>" + (property.Documentation || "None") + "</p>";
                text += "</td>";
                text += "<td class=\"parameter-type\">";
                text += "<a href=\"meta.html?id=" + encodeURIComponent(property.Id) + "\">" + htmlEncode(property.ModelType) + "</a>";
                text += "</td>";
                text += "<td class=\"parameter-annotations\">";
                text += "<p>None.</p>";
                text += "</td>";
                text += "</tr>";
            });
        }
        text += "</tbody>";
        text += "</table>";
    }
    return text;
}
