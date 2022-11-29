*** Settings ***
Library     SeleniumLibrary
*** Variables ***
${url}    https://localhost:5002/
${browser}      chrome
${chromedriver}      driver/chromedriver.exe
*** Test Cases ***
open test
    Open Browser     ${url}   chrome   executable_path=${chromedriver}
