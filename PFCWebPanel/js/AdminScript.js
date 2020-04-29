function openNav() {
    document.getElementById("mysidebar").style.width = "250px";
    document.getElementById("main-mobile").style.marginRight = "250px";
    document.getElementById("btn").style.display = "none";
}

function closeNav() {
    document.getElementById("mysidebar").style.width = "0";
    document.getElementById("main-mobile").style.marginRight = "0";

}



var dropdown = document.getElementsByClassName("dropdown-btn");
var i;

for (i = 0; i < dropdown.length; i++) {
    dropdown[i].addEventListener("click", function () {
        
        var dropdownContent = this.nextElementSibling;
        if (dropdownContent.style.display === "block") {
            dropdownContent.style.display = "none";
        } else {
            dropdownContent.style.display = "block";
        }
    });
}