$('#Cover').on('change', function () {
    $('.cover-preview').attr('src', window.URL.createObjectURL(this.files[0])).removeClass('d-none');
})

// by js
// function addCover() {
//     let cov = document.getElementById("Cover");
//     let prev = document.querySelector(".cover-preview");

//     prev.setAttribute("src", URL.createObjectURL(cov.files[0]));

//     prev.classList.remove("d-none");
// }