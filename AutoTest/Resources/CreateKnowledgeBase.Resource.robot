*** Settings ***
Library    SeleniumLibrary
Library  OperatingSystem
Variables    ../Locations/Home.py
Variables    ../Locations/CreateKB.py
*** Keywords ***
Click btn add kb
    click element    ${btn_add_kb}

select category
    scroll element into view    ${select_category}
    click element    ${select_category}
    select from list by value    ${select_category}     1
Fill title
    input text    ${txt_title}      tieu de 1

Fill description
    input text    ${txt_description}      mô tả 1

Fill txt_environment
    input text    ${txt_environment}      môi trường

Fill txt_problem
    select frame    ${iframe_txt_problem}
    input text    ${txt_problem}      vấn đề

Fill txt_step_reproduce
    unselect frame
    sleep    2
    scroll element into view    ${txt_step_reproduce}


    input text    ${txt_step_reproduce}      các bước tái hiện

Fill txt_ErrorMessage
    input text    ${txt_ErrorMessage}      thông báo lỗi hiển thị

Fill txt_workaround
    input text    ${txt_workaround}      cách giải quyết nhanh

Fill txt_note
    select frame    ${iframe_txt_note}
    input text    ${txt_note}      cách giải quyết

Fill txt_tag
    unselect frame
    input text    ${txt_tag}      tag1,tag2

Fill txt_file
    @{file}=    OperatingSystem.get file    todo.txt
    choose file     ${txt_file}     @{file}


Click btn submit create kb
    click element    ${btn_submit}

Verify taget url create kb
    location should be    https://localhost:5002/my-kbs
