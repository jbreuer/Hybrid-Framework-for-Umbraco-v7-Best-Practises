var resizeTimer;    // Global access

$(document).ready(function () {

    initHomeSlider();
    initContentSlider();
    initAjaxPaging();
    initGoogleMaps();
    initMenu();

    google.maps.event.addDomListener(window, 'load', initGoogleMaps);
});

function initMenu() {
    $(document).on('click', 'nav > a', function (e) {

        e.preventDefault();

        $('#wrapper').toggleClass('toggled');

        return false;
    });
}

function initHomeSlider() {
    if ($('.bxslider').length) {
        var slider;
        var sliderActive = false;
        window.slimmage.readyCallback = function () {
            if (!sliderActive) {
                sliderActive = true;
                slider = $('.bxslider').bxSlider({
                    adaptiveHeight: true
                });
            }
            else {
                slider.reloadSlider();
            }
        }
    }
}

function initContentSlider() {
    if ($('.content-slider').length) {
        var slider;
        var thumbs;
        var sliderActive = false;
        window.slimmage.readyCallback = function () {
            var thumbsVisible = $('.content-slider').attr('data-thumbs');
            var initSlider = function (sliderSel, thumbsSel, showNum) {
                showNum = showNum ? showNum : thumbsVisible;

                if (!sliderActive) {
                    sliderActive = true;

                    slider = $(sliderSel).bxSlider({
                        adaptiveHeight: true,
                        mode: 'fade',
                        infiniteLoop: false,
                        controls: false,
                        pager: false,
                        auto: false,
                        onSliderLoad: function (currentIndex) {
                            $('li:not(.bx-clone)', sliderSel).eq(currentIndex).addClass('current-slide');

                            // Make sure that each time the viewport changes, each slide has its slimmage SRC altered
                            // to take advantage of the full available width of the parent container
                            $(window).on('resize', function () {
                                // Only take action after a slight delay in case the previous call wasn't finished yet
                                clearTimeout(resizeTimer);
                                resizeTimer = setTimeout(function () {
                                    // The call is made inside an anonymous function as otherwise it would fire
                                    // instantly due to adding parentheses for passing the instance as an argument
                                    sliderSlimmageMaxWidth(slider);
                                }, 200);
                            });
                        },
                        onSlideBefore: function ($slideElement, oldIndex, newIndex) {
                            $('.current-slide').removeClass('current-slide');
                            $slideElement.addClass('current-slide');
                            thumbs.goToSlide(Math.min(Math.max(0, newIndex - Math.floor(showNum / 2)), thumbs.getSlideCount() - showNum));
                            $('a:not(.bx-clone)', thumbsSel).eq(newIndex).addClass('active').siblings().removeClass('active');
                        }
                    });


                    thumbs = $(thumbsSel).bxSlider({
                        adaptiveHeight: true,
                        slideWidth: 150,
                        infiniteLoop: false,
                        minSlides: showNum,
                        maxSlides: showNum,
                        moveSlides: 1,
                        hideControlOnEnd: true,
                        slideMargin: 15,
                        pager: false,
                        onSliderLoad: function (currentIndex) {
                            $(thumbsSel).closest('.row').find('.content-slider-paging a').off('click').on('click', function (e) {
                                e.preventDefault();
                                var dir = $(this).is('.bx-next') ? 1 : -1;
                                var thumbsCurrentSlide = $('a.active', thumbsSel).index(thumbsSel + ' a:not(.bx-clone)');
                                var newIndex = Math.max(0, Math.min(thumbs.getSlideCount() - 1, thumbsCurrentSlide + showNum * dir));
                                thumbs.goToSlide(Math.min(Math.max(0, newIndex - Math.floor(showNum / 2)), thumbs.getSlideCount() - showNum));
                                $('a:not(.bx-clone)', thumbsSel).eq(newIndex).addClass('active').siblings().removeClass('active');
                                slider.goToSlide(newIndex);
                            });

                            $('a', thumbsSel).on('click', function (e) {
                                var newIndex = $(this).index(thumbsSel + ' a:not(.bx-clone)');
                                thumbs.goToSlide(Math.min(Math.max(0, newIndex - Math.floor(showNum / 2)), thumbs.getSlideCount() - showNum));
                                $(this).addClass('active').siblings().removeClass('active');
                                slider.goToSlide(newIndex);
                                e.preventDefault();
                            });

                            var current = slider.getCurrentSlide();
                            $('.slide').eq(current).addClass('current-slide');

                        }
                    });
                }
                else {
                    var iCurrSlide = slider.getCurrentSlide();
                    var iCurrThumb = thumbs.getCurrentSlide();

                    slider.reloadSlider();
                    thumbs.reloadSlider();

                    // Make sure the active slide from before reloading the sliders becomes active again
                    slider.goToSlide(iCurrSlide);
                    thumbs.goToSlide(iCurrThumb);
                }
            };

            var slidesLength = $('.content-slider .slide').length;

            if (slidesLength > 1) {
                initSlider('.content-slider', '.content-slider-pager');
            } else if (slidesLength = 1) {
                initSlider('.content-slider');
                $('.content-slider-pager').hide();
            }
        }
    }
}

function initAjaxPaging() {
    if ($('.pagination').length) {
        // Setup content
        initContent();
        // Setup loader
        $(".vacancy-layer").prepend("<div class='do-loader abslt' style='display:none;'><img src='/css/images/bx_loader.gif' border='0' /></div>");

        $(document).ajaxComplete(function () {
            //Update the url's again.
            initContent();
            //Show the new content with a fade effect.
            $(".do-loader").fadeOut(250);
        });

        $.address.change(function (event) {
            var page = event.parameters['page'];
            
            if (page) {
                var qs = "?page=" + page

                var level = event.parameters['level'];
                if (level){
                    qs += "&level=" + level
                }

                //Build the ajax url.
                var ajaxUrl = $.address.baseURL().split('?')[0];
                if (!endsWith(ajaxUrl, "/")) {
                    ajaxUrl += "/";
                }
                ajaxUrl += "ajaxvacancyoverview/";

                // Load content
                $(".do-loader").fadeTo(0, 0.8, function () {
                    $(".vacancy-overview").load(ajaxUrl + qs);
                });
            }
        });
    }
}

//Get a querystring value.
function getQueryString(name) {
    name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
    var regexS = "[\\?&]" + name + "=([^&#]*)";
    var regex = new RegExp(regexS);
    var results = regex.exec(window.location.href);
    if (results == null) {
        return "";
    }
    else {
        return results[1];
    }
}

function initContent() {

    //Update the links.
    $(".vacancy-overview .pagination li a").click(function (e) {
        e.preventDefault();

        var href = $(this).attr("href");
        if (href) {
            var page = href.match(/page=([0-9]+|all)/)[1];
            $.address.parameter('page', page, false);
        }

        var level = getQueryString('level');
        if (level) {
            $.address.parameter('level', level, false);
        }
    });
}

function endsWith(str, suffix) {
    return str.indexOf(suffix, str.length - suffix.length) !== -1;
}

function initGoogleMaps() {
    var mapCanvas = $("#map_canvas");
    if (mapCanvas.length > 0) {
        var map_canvas = document.getElementById('map_canvas');
        var map_options = {
            center: new google.maps.LatLng(mapCanvas.data("lat"), mapCanvas.data("lng")),
            zoom: parseInt(mapCanvas.data("zoom")),
            mapTypeId: google.maps.MapTypeId.ROADMAP
        }
        var map = new google.maps.Map(map_canvas, map_options)
    }
}


// This function is triggered on pages with sliders when the viewport of the browser is altered
// Result is that images within slides are set to use all of the space the parent container provides
// @oSlider - the bxSlider instance to manipulate
function sliderSlimmageMaxWidth(oSlider) {
    // Select images which are using the Slimmage plugin
    var oSlideImgs = oSlider.find('img[data-slimmage="true"]');

    if (oSlideImgs.length > 0) {
        oSlideImgs.each(function () {
            var oCurrImg = $(this);
            var iCurrWidth = parseInt(oCurrImg.attr('data-pixel-width'));
            var iMaxWidth = oCurrImg.parents('.bx-viewport').first().width();

            if (iCurrWidth < iMaxWidth) {
                // console.log(iCurrWidth, iMaxWidth);

                // If the image isn't using up all of its potential space, invoke the Slimmage 
                // function which updates the src attribute and pass the maximum width to it
                window.slimmage.adjustImageSrcWithWidth(oCurrImg[0], oCurrImg.attr('src'), iMaxWidth);
            }
        });
    }
}