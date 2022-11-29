*** Settings ***
Library     SeleniumLibrary
Resource    ../Resources/CommonResource.robot
Resource    ../Resources/Login.Resource.robot
Resource    ../Resources/Profile.Resource.robot
Test Setup       Open my browser     ${url}   ${browser}    ${chromedriver}
Test Teardown    Close my browser
Force Tags    Login
*** Variables ***
${url}    https://localhost:5002/
${browser}      chrome
${chromedriver}      driver/chromedriver.exe
*** Test Cases ***
Update profile
    Click btn Login
    Fill username   diu1
    Fill password   Diu@123456
    Click submit login
    Click to update account
    Click btn go to change profile
    sleep    2
    ${handles}=  Get Window Handles
    Switch Window   ${handles}[1]
    update firstname    nguyễn nguyễn
    update lastname    diu dịu
    update dob    12/12/1212
    Click button submit change profile
    Verify change profile successfully

Change password
    Click btn Login
    Fill username   diu1
    Fill password   Diu@123456
    Click submit login
    Click to update account
    Click btn go to change profile
    sleep    2
    ${handles}=  Get Window Handles
    Switch Window   ${handles}[1]
    Click btn change password
    Fill current password       Diu@123456
    Fill new password    Diu@123456
    Fill confirm password    Diu@123456
    Click button submit change password
    Verify change password successfully

Change email
    Click btn Login
    Fill username   diu1
    Fill password   Diu@123456
    Click submit login
    Click to update account
    Click btn go to change profile
    sleep    2
    ${handles}=  Get Window Handles
    Switch Window   ${handles}[1]
    Click btn change email
    update email    diu@gmail.com
    Click btn submit change email
    Verify send email confirm new email