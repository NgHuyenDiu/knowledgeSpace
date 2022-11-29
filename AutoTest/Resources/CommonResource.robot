*** Settings ***
Library    SeleniumLibrary
*** Keywords ***
Open my browser
    [Arguments]    ${url}   ${browser}  ${driver}
    SeleniumLibrary.open browser    ${url}   ${browser}     executable_path=${driver}
    SeleniumLibrary.maximize browser window

Close my browser
    SeleniumLibrary.close all browsers