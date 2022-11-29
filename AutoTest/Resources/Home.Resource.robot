*** Settings ***
Library    SeleniumLibrary
Library    String
Variables    ../Locations/Home.py
*** Keywords ***
Fill input search
    [Arguments]    ${keyword}
    input text    ${input_search}   ${keyword}

Click btn search
    click element    ${btn_search}

Verify search successfully
    [Arguments]    ${keyword}
    ${url}=     get location
    should contain      ${url}      ${keyword}

Click btn my kb
    click element    ${btn_my_kb}

Verify go to my kb successful
    ${url}  get location
    should be equal    ${url}     https://localhost:5002/my-kbs