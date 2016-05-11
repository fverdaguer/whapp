$('#btnRight').click(function (e) {
    var selectedOpts = $('#selectedOptions option:selected');
    if (selectedOpts.length == 0) {
        alert("Nothing to move.");
        e.preventDefault();
    }

    $('#availOptions').append($(selectedOpts).clone());
    $(selectedOpts).remove();
    e.preventDefault();
});

$('#btnLeft').click(function (e) {
    var selectedOpts = $('#availOptions option:selected');
    if (selectedOpts.length == 0) {
        alert("Nothing to move.");
        e.preventDefault();
    }

    $('#selectedOptions').append($(selectedOpts).clone());
    $(selectedOpts).remove();
    e.preventDefault();
});

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


$('#btnSubmit').click(function (e) {
    $('#selectedOptions option').prop('selected', true);
    $('#selectedOptionsInst option').prop('selected', true);
});
