// Decode protected string to clear string
function decodeStr(encrypted) {
    var separatorIndex = encrypted.indexOf("*");
    var key = parseInt(encrypted.substring(0, separatorIndex));
    var encryptedData = encrypted.substring(separatorIndex + 1);

    var clr = "";
    for (var i = 0; i < encryptedData.length; i += 2) {
        var hexChar = encryptedData.substr(i, 2);
        var decryptedChar = parseInt(hexChar, 16) ^ key;
        clr += String.fromCharCode(decryptedChar);
    }
    return clr;
}


// Parse protected-lnk to clear text a mailto:email
function parse_protectedLnk(clname) {
    var allElements = document.getElementsByClassName(clname);
    for (var i = 0; i < allElements.length; i++) {
        var el = allElements[i];
        var decoded = decodeStr(el.dataset.protected);
        el.textContent = decoded;
        el.attributes.removeNamedItem("data-protected");
        el.href = 'mailto:' + decoded;
    }
}
parse_protectedLnk("protected-lnk");

// Parse protected-str to clear text span
function parse_protectedStr(clname) {
    var allElements = document.getElementsByClassName(clname);
    for (var i = 0; i < allElements.length; i++) {
        var el = allElements[i];
        var newTag = document.createElement("span");
        newTag.textContent = decodeStr(el.dataset.protected);
        [...el.attributes].forEach(attr => {
            if (attr.nodeName != "data-protected")
                newTag.setAttribute(attr.nodeName, attr.nodeValue)
        })

        el.parentElement.replaceChild(newTag, el);
    }
}
parse_protectedStr("protected-str");