function ReloadPage() {
    __doPostBack('seochecker-bulkactioncontrol', '');
    
}

$(document).ready(function () {
    $(".bulkActionSelectAll").click(function () {
        $('.bulkItemSelectCheckbox').attr('checked', '');
        return false;
    });
});

$(document).ready(function () {
    $(".bulkActionDeSelectAll").click(function () {
        $('.bulkItemSelectCheckbox').attr('checked', false);
        return false;
    });
});

$(document).ready(function () {
    $(".bulkActionTrigger").click(function () {
        var action = $(".bulkActionDropdown").val();
        if (action != '') {

            var selected = new Array();
            $('.bulkItemSelectCheckbox:checked').each(function () {
                selected.push($(this).val());
            });
            var s = selected.join();
            if (s != '') 
            {
                UmbClientMgr.openModalWindow('plugins/SEOChecker/pages/dialogs/bulkaction.aspx?alias=' + action + '&ids=' + s, $(".bulkActionDropdown :selected").text(), true, 500, 400);
                return false;
            }
        }
        return false;
    });
});