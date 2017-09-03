var emailR = /^[_a-z0-9-A-Z-]+(\.[_a-z0-9-A-Z-]+)*@[a-z0-9-A-Z-]+(\.[a-z0-9-A-Z-]+)*(\.[a-z]{2,4})$/;
var passwordR = /^(?=.{6,})(?=.*[a-z])(?=.*[0-9])(?=.*[A-Z])(?=.*[@#$%^&+=]).*$/;
var alphabeticR = /^[A-z]+$/;
var alphawithspace = /^[a-zA-Z-,]+(\s{0,1}[a-zA-Z-, ])*$/

function validateCustomer(customerName, accountOwnerName, accountOwnerEmail, companyLogo, zipCode, city, state) {
    var message = "";
    var zipcodeR = /^([a-zA-Z0-9_-]){4,10}$/;
    var letterNumber = /^[0-9a-zA-Z]+$/;
    $('.Customer-error').html('');
    if ($.trim(customerName).length == 0) {
        message = "Please enter company name <br />";
        $("#lblNameError").html('Please enter company name');
    }
    //if (!alphawithspace.test(customerName) && $.trim(customerName).length != 0) {
    //    message = "Please enter customer name <br />";
    //    $("#lblNameError").html('Please enter valid company name');
    //}
    if ($.trim(companyLogo).length <= 1) {
        message = message + "Please select customer logo <br />";
        $("#lblLogoError").html('Please select customer logo');
    }
    if (!zipcodeR.test(zipCode) && $.trim(zipCode).length != 0) {
        message = message + "Please enter valid zipcode <br />";
        $("#lblZipError").html('Please enter valid zipcode ');
    }
    if ($.trim(accountOwnerName).length == 0) {
        message = message + "Please enter account owner name <br />";
        $("#lblONameError").html('Please enter account owner name ');
    }
    if ($.trim(accountOwnerEmail).length == 0) {
        message = message + "Please enter account owner email <br />";
        $("#lblOEmailError").html('Please enter account owner email ');
    }
    if (!emailR.test(accountOwnerEmail) && $.trim(accountOwnerEmail).length != 0) {
        message = message + "please enter a valid email<br />";
        $("#lblOEmailError").html('please enter a valid email');
    }
    if (!alphawithspace.test(city) && $.trim(city).length != 0) {
        message = message + "Please enter valid city <br />";
        $("#lblCityError").html('Please enter valid city');
    }
    if (!alphawithspace.test(state) && $.trim(state).length != 0) {
        message = message + "Please enter valid state <br />";
        $("#lblStateError").html('Please enter valid state');
    }
    if (message != "") {
        return false;
    }
    else {
        $('.Customer-error').html('');
        return true;
    }
}

function validateDriver(dOTNumber, firstName, lastName, licenseState, licenseNumber, dob) {
    var re = /^[ A-Za-z0-9'-]*$/
    var message = "";
    $('.Driver-error').html('');
    if ($.trim(dOTNumber).length == 0) {
        message = "Please enter a DOT number <br />";
        $("#lblDotError").html('Please enter DOT number');
    }

    if ($.trim(firstName).length == 0) {
        message = message + "Please enter first name <br />";
        $("#lblFNameError").html('Please enter first name');
    }
    if (!re.test(firstName) && $.trim(firstName).length != 0) {
        message = message + "Please enter valid first name <br />";
        $("#lblFNameError").html('Please enter valid first name ');
    }
    if ($.trim(lastName).length == 0) {
        message = message + "Please enter last name <br />";
        $("#lblLNameError").html('Please enter last name ');
    }
    if (!re.test(lastName) && $.trim(lastName).length != 0) {
        message = message + "Please enter valid lastName <br />";
        $("#lblLNameError").html('Please enter valid last name ');
    }
    if ($.trim(licenseState).length == 0) {
        message = message + "Please enter license state <br />";
        $("#lblLstateError").html('Please enter license state ');
    }
    if ($.trim(licenseNumber).length == 0) {
        message = message + "please enter license number<br />";
        $("#lblLnumberError").html('please enter license number');
    }
    if ($.trim(dob).length == 0) {
        message = message + "Please select dob <br />";
        $("#lblDOBError").html('Please select dob');
    }
    if (message != "") {
        return false;
    }
    else {
        $('.Driver-error').html('');
        return true;
    }
}








function ValidateUser(userFirstName, userLastName, userRole, userEmail, userPassword, userConfirmPassword, actionValue) {
    var message = "";
    if (actionValue == "CreateUser") {
        $('.User-error').html('');
        if ($.trim(userFirstName).length == 0) {
            message = "Please enter first name <br />";
            $("#lblFnameError").html('Please enter first name');
        }
        //if (!alphabeticR.test(userFirstName) && $.trim(userFirstName).length != 0) {
        //    message = "Please enter a valid first name <br />";
        //    $("#lblFnameError").html('please enter a valid first name');
        //}
        if ($.trim(userLastName).length == 0) {
            message = "Please enter last name <br />";
            $("#lblLnameError").html('Please enter last name');
        }
        //if (!alphabeticR.test(userLastName) && $.trim(userLastName).length != 0) {
        //    message = "Please enter a valid last name <br />";
        //    $("#lblLnameError").html('please enter a valid last name');
        //}
        if ($.trim(userRole).length == 0) {
            message = message + "Please select the role <br />";
            $("#lblRoleError").html('Please select the role');
        }
        if ($.trim(userEmail).length == 0) {
            message = message + "Please enter email <br />";
            $("#lblEmailError").html('Please enter email ');
        }
        if (!emailR.test(userEmail) && $.trim(userEmail).length != 0) {
            message = message + "please enter a valid email <br />";
            $("#lblEmailError").html('please enter a valid email');
        }
        if ($.trim(userPassword).length == 0) {
            message = message + "Please enter password <br />";
            $("#lblPassError").html('Please enter password ');
        }
        if (!passwordR.test(userPassword) && $.trim(userPassword).length != 0) {
            message = message + "Passwords must have at least one special character, one uppercase, one number,one lowercase letter <br />";
            $("#lblPassError").html('Passwords must have at least one special character, one uppercase, one number,one lowercase letter');
        }
        if ($.trim(userConfirmPassword).length == 0) {
            message = message + "password and confirm password do not match <br />";
            $("#lblConPassError").html('Please enter confirm password ');
        }
        if (userConfirmPassword != userPassword) {
            message = message + "password and confirm password do not match <br />";
            $("#lblConPassError").html('password and confirm password do not match ');
        }
    }
    else {
        $('.User-error').html('');
        if ($.trim(userFirstName).length == 0) {
            message = "Please enter first name <br />";
            $("#lblFnameError").html('Please enter first name');
        }
        //if (!alphabeticR.test(userFirstName) && $.trim(userFirstName).length != 0) {
        //    message = "Please enter a valid first name <br />";
        //    $("#lblFnameError").html('please enter a valid first name');
        //}
        if ($.trim(userLastName).length == 0) {
            message = "Please enter last name <br />";
            $("#lblLnameError").html('Please enter last name');
        }
        //if (!alphabeticR.test(userLastName) && $.trim(userLastName).length != 0) {
        //    message = "Please enter a valid last name <br />";
        //    $("#lblLnameError").html('please enter a valid last name');
        //}
        if ($.trim(userRole).length == 0) {
            message = message + "Please select the role <br />";
            $("#lblRoleError").html('Please select the role');
        }
    }

    if (message != "") {
        return false;
    }
    else {
        $('.User-error').html('');
        return true;
    }
}

function validateLocation(companyID, driver) {
    var message = "";
    $('.driver-error').html('');
    if ($.trim(companyID).length == 0) {
        message = message + "Please select the Company <br />";
        $("#lblDriverError").html('Please select the company');
    }
    if ($.trim(location).length == 0) {
        message = message + "Please select file <br />";
        $("#lblDriverError").html('Please select file');
    }
    if (message != "") {
        return false;
    }
    else {
        $('.driver-error').html('');
        return true;
    }
}

function validateLocation(companyID, location) {
    var message = "";
    $('.location-error').html('');
    if ($.trim(companyID).length == 0) {
        message = message + "Please select the Company <br />";
        $("#lblCompanyError").html('Please select the company');
    }
    if ($.trim(location).length == 0) {
        message = message + "Please select file <br />";
        $("#lblLocationError").html('Please select file');
    }
    if (message != "") {
        return false;
    }
    else {
        $('.location-error').html('');
        return true;
    }
}

//$("#DOTNumber").on('keypress', function (e) {
//    //debugger;
//    if ($(this).val().length < 1) {
//        if (e.which == 32) {
//            //alert(e.which);
//            return false;
//        }
//        if (e.which == 45) {
//            return false;
//        }
//    }
//    else {
//        if (e.which == 32) {
//            if (i != 0) {
//                return false;
//            }
//            i++;
//        }
//        else {
//            i = 0;
//        }
//        if (e.which == 45) {
//            if (j != 0) {
//                return false;
//            }
//            j++;
//        }
//        else {
//            j = 0;
//        }
//    }
//});