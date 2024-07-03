function rememberUser() {
    const rmCheck = document.getElementById("rememberMe");
    const usernameInput = document.getElementById("username");

    if (localStorage.checkbox && localStorage.checkbox !== "") {
        rmCheck.checked = true;
        usernameInput.value = localStorage.username;
    } else {
        rmCheck.checked = false;
        usernameInput.value = "";
    }
}

function lsRememberMe() {
    const rmCheck = document.getElementById("rememberMe");
    const usernameInput = document.getElementById("username");

    if (rmCheck.checked && usernameInput.value !== "") {
        localStorage.username = usernameInput.value;
        localStorage.checkbox = "checked";
    } else {
        localStorage.username = "";
        localStorage.checkbox = "";
    }
}