$(document).ready(function () { 
    $(".editButn").on("click", function (e) {
        e.preventDefault();
        var categoryId = $(this).data("id");
        $("#editModal").modal("show");
        $("#editCategoryForm").data("categoryId", categoryId);    
    });

 
    $("#editCategoryForm").submit(function (e) {
        e.preventDefault();
        var categoryId = $(this).data("categoryId"); // Получаем ID категории из data атрибута формы
        var formData = $(this).serialize(); // Получаем данные формы
        formData += "&Id=" + categoryId; // Добавляем ID категории к данным формы
        $.ajax({
            type: "POST",
            url: '@Url.Action("EditCategory", "Home")',
            data: formData,
            success: function (response) {
                location.reload(); // Перезагрузка страницы после успешного редактирования
            },
        });
    });
});

$("#createCategory").on("click", function (e) {
    e.preventDefault();
    $.ajax({
        type: "POST",
        url: '@Url.Action("CreateCategory", "Home")',
        data: $("#categoryForm").serialize(),
        success: function (response) {
            location.reload(); // Перезагрузка страницы
        },
    });
});

