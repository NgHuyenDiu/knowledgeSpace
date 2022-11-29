*** Settings ***
Library    SeleniumLibrary
Library    Dialogs
Variables    ../Locations/Login.py
Variables    ../Locations/Home.py
Variables    ../Locations/Detail_kb.py
*** Keywords ***
Choose kb 1 in kb popular
    click element    ${kb_popular_1}

Post report kb

    click element    ${btn_report}
    wait until element is visible    ${txt_report}
    input text  ${txt_report}    abc
    Execute Manual Step    Vui lòng điền capcha vào form.
    click element    ${btn_send_report}

Post like kb
    scroll element into view    ${like_it}
    click element    ${like_it}

Fill new comment
    scroll element into view    ${txt_new_comment}
    input text    ${txt_new_comment}    comment1

Click btn send comment
    scroll element into view    ${btn_send_commnet}
    click element    ${btn_send_commnet}