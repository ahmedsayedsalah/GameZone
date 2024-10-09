
$('.js-delete').on('click', function () {

    const swal = Swal.mixin({
        customClass: {
            confirmButton: "btn btn-danger mx-2",
            cancelButton: "btn btn-success"
        },
        buttonsStyling: false
    });

    swal.fire({
        title: "Are you sure that you need to delete this game",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonText: "Yes, delete it!",
        cancelButtonText: "No, cancel!",
        reverseButtons: true
    }).then((result) => {
        if (result.isConfirmed) {

            $.ajax({
                url: `/games/delete/${$(this).data('id')}`,
                method: "DELETE",
                success: function () {
                    swal.fire({
                        title: "Deleted!",
                        text: "Game has been deleted.",
                        icon: "success"
                    });

                    $(this).parents('tr').fadeOut();
                },
                error: function () {
                    swal.fire({
                        title: "Oooops!",
                        text: "Somethings went wrong",
                        icon: "error"
                    });
                }
            });

            
        }
    });

});