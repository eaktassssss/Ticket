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
function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57))
        return false;
    return true;
}