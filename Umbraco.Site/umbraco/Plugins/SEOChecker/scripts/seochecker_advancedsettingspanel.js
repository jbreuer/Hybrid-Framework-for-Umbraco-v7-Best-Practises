$(document).ready(function ($) {
    $(".SEOCheckerToggleAdvancedSettings").click(function () {
        var toggle = $('#SEOCheckerToggleAdvancedSettings').val() == "1" ? "0" : "1";
        $('#SEOCheckerToggleAdvancedSettings').val(toggle);
        setToggleOptions();
    });

    function setToggleOptions() {
        var toggle = $('#SEOCheckerToggleAdvancedSettings').val();
        if (toggle == "1") {
            $('.SEOCheckerAdvancedSettings').show();
        }
        else {
            $('.SEOCheckerAdvancedSettings').hide();
        }
    }

    setToggleOptions();
});

