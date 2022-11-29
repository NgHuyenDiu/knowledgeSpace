*** Settings ***
Library     SeleniumLibrary
Library     Dialogs
Resource    ../Resources/CommonResource.robot
Resource    ../Resources/Register.Resource.robot
Resource    ../Resources/Login.Resource.robot
Test Setup       Open my browser     ${url}   ${browser}    ${chromedriver}
Test Teardown    Close my browser
Force Tags    Register
*** Variables ***
${url}    https://localhost:5002/
${browser}      chrome
${chromedriver}      driver/chromedriver.exe
*** Test Cases ***
Register successfully
    Click btn Login
    Click btn register
    sleep    2
    Fill txt_username
    ${username_in}=     Get username
    Fill txt_firstname  huyen2
    Fill txt_lastname   diu2
    Fill txt_dob    12/12/1212
    Fill txt_phonenumber    09876543456
    Fill txt_email
    Fill txt_password   Diu@123456
    ${Password_in}=     Get password
    Fill txt_confirmpassword    Diu@123456
    Click btn submit register
    sleep    3
    Click confirm link
    Execute Javascript   window.open('https://localhost:5002/');
    go to    https://localhost:5002/
    ${handles}=  Get Window Handles
    Switch Window   ${handles}[1]
    Verify_can_login_with_account_just_register     ${username_in}      ${Password_in}
    Verify login successfully


