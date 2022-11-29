*** Settings ***
Library     SeleniumLibrary
Library     Dialogs
Resource    ../Resources/CommonResource.robot
Resource    ../Resources/Login.Resource.robot
Test Setup       Open my browser     ${url}   ${browser}    ${chromedriver}
Test Teardown    Close my browser
Force Tags    Login
*** Variables ***
${url}    https://localhost:5002/
${browser}      chrome
${chromedriver}      driver/chromedriver.exe
*** Test Cases ***
Login successfully
    Click btn Login
    Fill username   diu1
    Fill password   Diu@123456
    Click submit login
    Verify login successfully