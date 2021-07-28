$(document).on("click", "#deleteUserRole", function () {
    var id = $(this).attr("data-id");
    var tr = $(this).closest("tr");
    swal.fire({
        title: 'Emin misiniz?',
        text: "Bu işlemi geri alamayacaksınız!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Evet',
        cancelButtonColor: "#e74c3c",
        cancelButtonText: 'Hayır',
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: "POST",
                url: "/UserRole/Delete",
                data: { id: id },
                dataType: "json",
                success: function (result) {
                    if (result.isSuccessful == true) {
                        tr.remove();
                        Swal.fire(
                            'Başarılı',
                            'Silme işlemi başarılı şekilde gerçekleştirildi.',
                            'success'
                        );
                    }
                    else {
                        Swal.fire(
                            'Dikkat',
                            'Silme işlemi sırasında hata oluştu',
                            'error'
                        );
                    }
                }, error: function () {
                    Swal.fire(
                        'Dikkat',
                        'Silme işlemi sırasında hata oluştu',
                        'error'
                    );
                }
            });

        }
    })
});
