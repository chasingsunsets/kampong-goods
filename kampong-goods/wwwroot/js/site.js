// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

    function initialiseFirstname() {
        let firstname = $('#firstname').val();
    let titleArr = [];
    let initTitle = '';
    if (firstname) {
        titleArr = firstname.trim().split(' ');
    for (let i = 0; i < titleArr.length; i++) {
        initTitle += titleArr[i].charAt(0).toUpperCase() + titleArr[i].slice(1) + (i == titleArr.length - 1 ? '' : ' ');
                }
    $('#firstname').val(initTitle);
            }
        }

    function initialiseLastname() {
        let lastname = $('#lastname').val();
    let titleArr = [];
    let initTitle = '';
    if (lastname) {
        titleArr = lastname.trim().split(' ');
    for (let i = 0; i < titleArr.length; i++) {
        initTitle += titleArr[i].charAt(0).toUpperCase() + titleArr[i].slice(1) + (i == titleArr.length - 1 ? '' : ' ');
                }
    $('#lastname').val(initTitle);
            }
        }
