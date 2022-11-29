*** Settings ***
Library    SeleniumLibrary
Variables    ../Locations/Login.py
Variables    ../Locations/Register.py
Resource    ../Resources/Login.Resource.robot
*** Keywords ***
Click btn register
    scroll element into view    ${btn_register}
    click element    ${btn_register}

Fill txt_username
    execute javascript    document.getElementById("Input_username").value = Math.round(Math.random()*100000)

Fill txt_firstname
    [Arguments]    ${data}
    input text    ${txt_firstname}  ${data}

Fill txt_lastname
    [Arguments]    ${data}
    input text    ${txt_lastname}   ${data}

Fill txt_dob
    [Arguments]    ${data}
    input text    ${txt_dob}    ${data}

Fill txt_phonenumber
    [Arguments]    ${data}
    input text    ${txt_phonenumber}    ${data}

Fill txt_email
#    [Arguments]    ${data}
#    input text    ${txt_email}  ${data}
    execute javascript    document.getElementById("Input_Email").value = Math.round(Math.random()*100000)+"@gmail.com"



Fill txt_password
    [Arguments]    ${data}
    input text    ${txt_password}   ${data}

Fill txt_confirmpassword
    [Arguments]    ${data}
    scroll element into view    ${txt_confirmpassword}
    input text    ${txt_confirmpassword}    ${data}

Click btn submit register
    click element    ${btn_submit_register}

Click confirm link
    wait until element is visible    ${confirm_link}
    click element    ${confirm_link}

Verify input_username_error
    [Arguments]    ${mes}
    ${error}    get value   ${input_username_error}
    should be equal    ${mes}       ${error}

Verify Input_FirstName_error
    [Arguments]    ${mes}
    ${error}    get value   ${Input_FirstName_error}
    should be equal    ${mes}       ${error}

Verify Input_LastName_error
    [Arguments]    ${mes}
    ${error}    get value   ${Input_LastName_error}
    should be equal    ${mes}       ${error}


Verify Input_Dob_error
    [Arguments]    ${mes}
    ${error}    get value   ${Input_Dob_error}
    should be equal    ${mes}       ${error}

Verify Input_PhoneNumber_error
    [Arguments]    ${mes}
    ${error}    get value   ${Input_PhoneNumber_error}
    should be equal    ${mes}       ${error}

Verify Input_Email_error
    [Arguments]    ${mes}
    ${error}    get value   ${Input_Email_error}
    should be equal    ${mes}       ${error}

Verify Input_Password_error
    [Arguments]    ${mes}
    ${error}    get value   ${Input_Password_error}
    should be equal    ${mes}       ${error}

Verify Input_ConfirmPassword_error
    [Arguments]    ${mes}
    ${error}    get value   ${Input_ConfirmPassword_error}
    should be equal    ${mes}       ${error}
Get username
    ${username_output}      get value    ${txt_username}
    [Return]    ${username_output}

Get password
     ${password_output}      get value    ${txt_password}
     [Return]    ${password_output}

Verify_can_login_with_account_just_register
    [Arguments]    ${username_output}   ${password_output}
    Click btn Login
    Fill username   ${username_output}
    Fill password   ${password_output}
    Click submit login
