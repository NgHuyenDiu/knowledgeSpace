*** Settings ***
Library    SeleniumLibrary
Variables    ../Locations/Home.py
Variables    ../Locations/Login.py
*** Keywords ***

 Click btn Login
    click element    ${btn_login}

Fill username
    [Arguments]    ${data}
    input text    ${txt_usernamelg}   ${data}

Fill password
    [Arguments]    ${data}
    input text    ${txt_passwordlg}   ${data}

Click submit login
    click element    ${btn_login_submit}

Verify login successfully
    element should be visible    ${btn_my_kb}