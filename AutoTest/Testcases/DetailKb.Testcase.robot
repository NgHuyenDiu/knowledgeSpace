*** Settings ***
Library     SeleniumLibrary
Library     Dialogs
Resource    ../Resources/CommonResource.robot
Resource    ../Resources/DetailKb.Resource.robot
Resource    ../Resources/Login.resource.robot
Test Setup       Open my browser     ${url}   ${browser}    ${chromedriver}
Test Teardown    Close my browser
Force Tags    CreateKnowledgeBase
*** Variables ***
${url}    https://localhost:5002/
${browser}      chrome
${chromedriver}      driver/chromedriver.exe
*** Test Cases ***
Report kb
    Click btn Login
    Fill username   diu1
    Fill password   Diu@123456
    Click submit login
    sleep    1
    Choose kb 1 in kb popular
    Post report kb

Like kb
    Click btn Login
    Fill username   diu1
    Fill password   Diu@123456
    Click submit login
    sleep    1
    Choose kb 1 in kb popular
    Post like kb

Post comment
    Click btn Login
    Fill username   diu1
    Fill password   Diu@123456
    Click submit login
    sleep    1
    Choose kb 1 in kb popular
    Fill new comment
    Click btn send comment