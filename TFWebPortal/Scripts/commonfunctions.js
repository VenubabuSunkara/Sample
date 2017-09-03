function ValidateEmail(Email) {
    var emailR = /^[_a-z0-9-A-Z-]+(\.[_a-z0-9-A-Z-]+)*@[a-z0-9-A-Z-]+(\.[a-z0-9-A-Z-]+)*(\.[a-z]{2,4})$/;
    return emailR.test(Email);
}

function CheckLength(Param) {
    return Param.trim().length;
}
function ValidateSize(file) {
    var FileSize = file.files[0].size / 1024 / 1024; // in MB
    if (FileSize => 1) {
        return false;
    } else {
        return true;
    }
}

//function roleBasedShow(data) {
//    var Status;
//    if (data == true) {
//        Status = "";
//    }
//    else {
//        Status = "disabled";
//    }
//    return Status;
//}



//validate files
function Validate(oInput, _validFileExtensions) {
    if (oInput.type == "file") {
        var sFileName = oInput.value;
        if (sFileName.length > 0) {
            var blnValid = false;
            for (var j = 0; j < _validFileExtensions.length; j++) {
                var sCurExtension = _validFileExtensions[j];
                if (sFileName.substr(sFileName.length - sCurExtension.length, sCurExtension.length).toLowerCase() == sCurExtension.toLowerCase()) {
                    blnValid = true;
                    break;
                }
            }
            if (!blnValid) {
                return false;
            }
        }
    }
    return true;
}

//Change date format for UNIX time
function dateConverter(UNIX_timestamp) {
    UNIX_timestamp = UNIX_timestamp.replace("/Date(", "").replace(")/", "").trim();
    date = getDateFormat(parseInt(UNIX_timestamp));
    return date;
}

//Change time format for UNIX time
function timeConverter(UNIX_timestamp) {
    UNIX_timestamp = UNIX_timestamp.replace("/Date(", "").replace(")/", "").trim();
    var d = new Date(parseInt(UNIX_timestamp));
    time = getDateTimeFormat(parseInt(UNIX_timestamp));
    return time;
}

//Date format in mm/dd/yyyy
function getDateFormat(date) {

    var d = new Date(date);
    var year = d.getFullYear();
    var month = d.getMonth() + 1 < 10 ? '0' + (d.getMonth() + 1) : (d.getMonth() + 1);
    var day = d.getDate() < 10 ? '0' + d.getDate() : d.getDate();
    var date = month + '/' + day + '/' + year;
    //var date = day + '/' + month + '/' + year;
    return date;
}

//Time format in hh:mm:ss

function getDateTimeFormat(date) {
    var d = new Date(date);
    var hour = d.getHours() == 0 ? 12 : (d.getHours() > 12 ? d.getHours() - 12 : d.getHours());
    var min = d.getMinutes() < 10 ? '0' + d.getMinutes() : d.getMinutes();
    var sec = d.getSeconds() < 10 ? '0' + d.getSeconds() : d.getSeconds();
    var ampm = d.getHours() < 12 ? 'AM' : 'PM';
    var time = hour + ':' + min + ':' + sec + ' ' + ampm;
    return time;
}



function alphanumeric(inputtxt) {
    var letterNumber = /^[0-9a-zA-Z]+$/;
    if (inputtxt.value.match(letterNumber)) {
        return true;
    }
    else {
        return false;
    }
}

function isNumeric(n) {
    return !isNaN(parseFloat(n)) && isFinite(n);
}