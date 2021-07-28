$(document).on("change", "#approval", function () {
    var value = $(this).val();
    if (value == 1) {
        $("#EffortApprovalLimit").prop('disabled', false);
    }
    else {
        $("#EffortApprovalLimit").prop('disabled', true);
        $("#EffortApprovalLimit").val('');
    }
});

$(document).ready(function () {
    var value = $("#approvalid").val();
    if (value == "True") {
        $("#EffortApprovalLimit").prop('disabled', false);
    }
    else {
        $("#EffortApprovalLimit").prop('disabled', true);
    }
})