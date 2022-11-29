*** Settings ***
Library     SeleniumLibrary
Library     Dialogs
Resource    ../Resources/CommonResource.robot
Resource    ../Resources/Login.Resource.robot
Resource    ../Resources/Home.Resource.robot
Test Setup       Open my browser     ${url}   ${browser}    ${chromedriver}
Test Teardown    Close my browser
Force Tags    Home
*** Variables ***
${url}    https://localhost:5002/
${browser}      chrome
${chromedriver}      driver/chromedriver.exe
*** Test Cases ***
search kb
    Fill input search    test
    Click btn search
    Verify search successfully      test

Go to my knowledge base
    Click btn Login
    Fill username   diu1
    Fill password   Diu@123456
    Click submit login
    Click btn my kb
    Verify go to my kb successful
