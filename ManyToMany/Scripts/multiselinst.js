$('#btnRightInst').click(function (e) {
    var selectedOpts = $('#selectedOptionsInst option:selected');
    if (selectedOpts.length == 0) {
        alert("Nothing to move.");
        e.preventDefault();
    }

    $('#availOptionsInst').append($(selectedOpts).clone());
    $(selectedOpts).remove();
    e.preventDefault();
});

$('#btnLeftInst').click(function (e) {
    var selectedOpts = $('#availOptionsInst option:selected');
    if (selectedOpts.length == 0) {
        alert("Nothing to move.");
        e.preventDefault();
    }

    $('#selectedOptionsInst').append($(selectedOpts).clone());
    $(selectedOpts).remove();
    e.preventDefault();
});

$('#btnSubmitInst').click(function (e) {
    $('#selectedOptionsInst option').prop('selected', true);
});
