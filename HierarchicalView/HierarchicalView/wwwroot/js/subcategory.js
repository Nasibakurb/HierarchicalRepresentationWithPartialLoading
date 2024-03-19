$(document).ready(function () {
    $('.showSubcButn').on('click', function () {
        var subcategoryList = $(this).closest('.card-body').next('.subcategoryList');
        subcategoryList.slideToggle();
    });
});


$(".addButn").on("click", function () { /*���������� ������������*/
    var categoryId = $(this).data("id");
    $.get('@Url.Action("CreateSubcategoryPartial", "Subcategory")', { categoryId: categoryId }, function (data) {
        $("#addSubcategoryModal .modal-body").html(data);
        $("#addSubcategoryModal").modal("show");
    });
});
$(document).on("submit", "#createSubcategoryForm", function (e) {
    e.preventDefault();
    var formData = $(this).serialize();
    var categoryId = $(this).find("#categoryId").val(); // �������� categoryId �� �������� ���� �����
    formData += "&categoryId=" + categoryId; // ��������� categoryId � ������ �������
    $.ajax({
        type: "POST",
        url: '@Url.Action("Create", "Subcategory")',
        data: formData,
        success: function (response) {
            location.reload(); // ������������ �������� ����� ��������� �������� ������������
        }
    });
});





$(".deleteSubcButn").on("click", function (e) { /*�������� ������������*/
    e.preventDefault();
    var categoryId = $(this).attr("data-categoryId");
    var subcategoryId = $(this).attr("data-id");

    $.ajax({
        type: "POST",
        url: '@Url.Action("Delete", "Subcategory")',
        data: { id: subcategoryId, categoryId: categoryId },
        success: function (response) {
            location.reload();
        },
        error: function (xhr, status, error) {
            console.error("������ ��� �������� ������������:", error);
        }
    });
});


$(document).ready(function () {
    // �������������� ������������
    $(".editSubcButn").on("click", function (e) {
        e.preventDefault();
        var subcategoryId = $(this).data("id");
        var categoryId = $(this).data("categoryid");

        // ��������� ��������� ������������� � ��������� ����
        $.get('@Url.Action("EditSubcategoryPartial", "Subcategory")', { subcategoryId: subcategoryId, categoryId: categoryId })
            .done(function (data) {
                $('#editSubcategoryModal .modal-body').html(data);
                $('#editSubcategoryModal').modal('show');
            })
            .fail(function (xhr, status, error) {
                console.error(xhr.responseText);
            });
    });

    // �������� ������ ����� �������������� ������������ ����� AJAX
    $('#editSubcategoryModal').on('submit', '#EditSubcategoryForm', function (e) {
        e.preventDefault();
        var subcategoryId = $('#subcategoryId').val();
        var categoryId = $('#categoryId').val();
        var formData = $(this).serialize();
        formData += "&CategoryId=" + categoryId + "&Id=" + subcategoryId;
        $.ajax({
            type: 'POST',
            url: '@Url.Action("Edit", "Subcategory")',
            data: formData,
            success: function (response) {
                $('#addSubcategoryModal').modal('hide');
                location.reload();
                // �������������� �������� ����� ��������� ��������������
            },
            error: function (xhr, status, error) {
                console.error(xhr.responseText);
            }
        });
    });
});

