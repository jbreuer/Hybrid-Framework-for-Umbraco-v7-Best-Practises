
var currentSelectedProvider = '';
var queryParametersField = '';


$(document).ready(function () {

    $(".addQueryParameterOption").click(function () {
        UmbClientMgr.openModalWindow('/app_plugins/bulkmanager/pages/selectfieldsdialog.aspx?id=' + currentSelectedProvider + '&fields=' + getSelectedProviderFields(), 'Add filter criteria ', true, 500, 400);
        return false;
    });

    $(".removeQueryParameterOption").click(function () {
        var provider = $(this).closest(".queryParameterPlaceHolder").attr('queryProvider');
        removeSelectedQueryParameter(provider);
        showButtons();
        return false;
    });

});

$(document).ready(function () {
    $(".bulkItemIdCheckbox").change(function () {
        resetActionDropdown();
    });
});

$(document).ready(function () {
    $(".bulkItemSelectCheckbox").change(function () {
        resetActionDropdown();
        var selected = $('.bulkItemSelectCheckbox').is(':checked');
        $('.bulkItemIdCheckbox').attr('checked', selected);
    });
});

$(document).ready(function () {
    $(".bulkActionDropdown").change(function () {
        var action = $(".bulkActionDropdown").val();
        openBulkActionDialog(action);
        return false;
    });
});

$(document).ready(function () {
    $(".bulkActionOption").click(function () {
        var action = $(this).attr('provider');
        openBulkActionDialog(action);
        return false;
    });
});

function openBulkActionDialog(bulkActionProvider) {
    $('.btn-group').removeClass('open');
    if (bulkActionProvider != '' && getSelectedRecordIds() != '') {
        UmbClientMgr.openModalWindow('/app_plugins/BulkManager/pages/bulkactiondialog.aspx?actionId=' + bulkActionProvider, $(".bulkActionDropdown :selected").text(), true, 500, 400);
    }
    return false;
}

function getSelectedRecordIds() {
    var selected = new Array();
    $('.bulkItemIdCheckbox:checked').each(function () {
        selected.push($(this).val());
    });
    var s = selected.join();
    return s;
}

function setSelectedProvider(provider) {
    currentSelectedProvider = provider;
}

function setQueryParametersField(field) {
    queryParametersField = field;
}

function getSelectedProviderFields() {
    return $('#' + queryParametersField).val();
}

function setSelectedFields(selectedFields) {
    $('#' + queryParametersField).val(selectedFields);
    displaySelectedQueryParameters();
}

function removeSelectedQueryParameter(queryParameterId) {
    var arr = getSelectedProviderFields().split(',');
    arr.splice($.inArray(queryParameterId, arr), 1);
    setSelectedFields(arr.join(','));
}

function clickSearchButton() {
    UmbClientMgr.mainTree().clearTreeCache();
    $('.btn-Search').trigger('click');
}


function displaySelectedQueryParameters() {
    var array = $('#' + queryParametersField).val().split(',');
    $(".queryParameterPlaceHolder").each(function () {
        var id = $(this).attr('queryprovider');
        if ($.inArray(id, array) > -1) {
            //Selected
            $(this).show();
            $(this).attr("selected", "true");
        } else {
            //Hidden
            $(this).hide();
        }
        showButtons();
    });
}


function showButtons() {
    enableAddButtonOnLastParameter();
    showDeleteButton();
}

function enableAddButtonOnLastParameter() {
    $('.addQueryParameterOption').hide();

    var container = $('.queryParameterPlaceHolder:visible:last');
    container.find('.addQueryParameterOption').show();
}

function showDeleteButton() {
    if ($('.queryParameterPlaceHolder:visible').size() < 2) {
        $(".removeQueryParameterOption").hide();
    } else {
        $(".removeQueryParameterOption").show();
    }

}

function intializeBulkManagerForm() {
    displaySelectedQueryParameters();
    showButtons();

}

function resetActionDropdown() {
    $('.bulkActionDropdown').val('');;
}

/*BulkActions*/

$(document).ready(function () {
    $(".bulkActionCancelButton").click(function () {
        UmbClientMgr.contentFrame().focus();
        UmbClientMgr.contentFrame().resetActionDropdown();
        UmbClientMgr.closeModalWindow();
        return false;
    });
});

$(document).ready(function () {
    $(".bulkActionCloseButton").click(function () {
        UmbClientMgr.contentFrame().focus();
        UmbClientMgr.contentFrame().resetActionDropdown();
        UmbClientMgr.contentFrame().clickSearchButton();
        UmbClientMgr.closeModalWindow();
        
        return false;
    });
});

function setSelectedIds(selectedFieldId) {
    $('#' + selectedFieldId).val(UmbClientMgr.contentFrame().getSelectedRecordIds());
}


function executeBulk(guid) {
    window.setTimeout(function () {
        startBulk(guid);
    }, 10);
}

function startBulk(guid) {
    var jsonData = { id: guid };
    $.ajax({
        type: "POST",
        url: "bulkactiondialog.aspx/Start",
        data: JSON.stringify(jsonData),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            window.setTimeout(function () {
                getBulkStatus(guid);
            }, 100);
        },
        error: function (response) {
            window.setTimeout(function () {
                getBulkStatus(guid);
            }, 100);
        },
        timeout: 50
    });
}

function getBulkStatus(guid) {

    var jsonData = { id: guid };
    $.ajax({
        type: "POST",
        url: "bulkactiondialog.aspx/GetStatus",
        data: JSON.stringify(jsonData),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var model = data.d;
            var finished = model.Finished;
            if (finished == true) {
                RenderResultsMessage(model);
            } else {

                $('#dialogExecutingMessage').text(model.StatusMessage);
                window.setTimeout(function () {
                    getBulkStatus(guid);
                }, 200);
            }

        },
        error: function (response) {
            showError();
        }
    });
}

function RenderResultsMessage(model) {
    $('#dialogExecutingMessage').text(model.StatusMessage);
    $('.progressBar').hide();
    $('.bulkActionFinished').show();
    
}

function showError() {
    $('#dialogExecutingMessage').text('Error executing actions');
    $('.progressBar').hide();
    $('.bulkActionFinished').show();
}

//Overview options
function OpenDocumentDialog(umbracoUrl) {
    OpenUrl(umbracoUrl);
    return false;
}

function OpenMediaDialog(umbracoUrl) {
    OpenUrl(umbracoUrl);
    return false;
}

function OpenMemberDialog(umbracoUrl) {
    OpenUrl(umbracoUrl);
    return false;
}

function OpenUrl(url) {
    window.open(url, '_blank', 'width = 1024, height = 800');
}