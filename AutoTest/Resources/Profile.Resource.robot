*** Settings ***
Library    SeleniumLibrary
Library    String
Variables    ../Locations/Home.py
Variables    ../Locations/Login.py
Variables    ../Locations/Profile.py
*** Keywords ***
Click to update account
    click element    ${my_profile}

Click btn go to change profile
    click element    ${btn_change}

Update firstname
    [Arguments]    ${Firstname}
    input text    ${txt_firstname_profile}      ${Firstname}

Update lastname
    [Arguments]     ${lastname}
    input text    ${txt_lastname_profile}      ${lastname}

Update dob
    [Arguments]    ${dob}
    input text    ${txt_dob_profile}        ${dob}

Update phonenumber
    [Arguments]    ${phonenumber}
    input text    ${txt_phonenumber_profile}        ${phonenumber}

Click button submit change profile
    click element    ${btn_change_account}

Click btn change email
    click element    ${btn_change_email}

Update email
    [Arguments]    ${new_email}
    input text    ${txt_new_email}      ${new_email}

Click btn submit change email
    click element    ${btn_submit_change_email}

Verify send email confirm new email
    ${result}   get text    ${alert_display_successfully}
    ${result}=       Strip String    ${result.replace('×','')}
    should be equal    ${result}    Confirmation link to change email sent. Please check your email.

Verify change profile successfully
    ${result}   get text    ${alert_display_successfully}
    ${result}=       Strip String    ${result.replace('×','')}
    should be equal    ${result}    Your profile has been updated

Click btn change password
    click element    ${btn_change_password}

Fill current password
    [Arguments]    ${currentpassword}
    input text   ${txt_current_password}    ${currentpassword}

Fill new password
    [Arguments]    ${newpassword}
    input text   ${txt_new_password}    ${newpassword}

Fill confirm password
    [Arguments]    ${confirmpassword}
    input text   ${txt_confirm_new_password}    ${confirmpassword}

Click button submit change password
    click element    ${btn_update_password}

Verify change password successfully
    ${result}   get text    ${alert_display_successfully}
    ${result}=       Strip String    ${result.replace('×','')}
    should be equal    ${result}    Mật khẩu của bạn đã được thay đổi.