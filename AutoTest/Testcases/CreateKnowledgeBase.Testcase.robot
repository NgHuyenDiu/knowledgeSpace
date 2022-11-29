*** Settings ***
Library     SeleniumLibrary
Library     Dialogs
Resource    ../Resources/CommonResource.robot
Resource    ../Resources/CreateKnowledgeBase.Resource.robot
Resource    ../Resources/Login.resource.robot
Test Setup       Open my browser     ${url}   ${browser}    ${chromedriver}
Test Teardown    Close my browser
Force Tags    CreateKnowledgeBase
*** Variables ***
${url}    https://localhost:5002/
${browser}      chrome
${chromedriver}      driver/chromedriver.exe
*** Test Cases ***
Create successfully new knowledge
    sleep    5
    Click btn Login
    Fill username   diu1
    Fill password   Diu@123456
    Click submit login
    Click btn add kb
    sleep    5
    select category
    Fill title
    Fill description
    Fill txt_environment
    Fill txt_problem
    Fill txt_step_reproduce
    Fill txt_ErrorMessage
    Fill txt_workaround
    Fill txt_note
    Fill txt_tag
    Execute Manual Step    Vui lòng điền capcha vào form.
    Click btn submit create kb
    sleep     2
    Verify taget url create kb