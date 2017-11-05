/* ----------------------------------------------------------------------------------------
* Author        : Ambidextrousbd
* Template Name : Novamedi | One Page Medical Html Template
* File          : Novamedi main JS file
* Version       : 1.0
* ---------------------------------------------------------------------------------------- */





/* INDEX
----------------------------------------------------------------------------------------

01. Preloader js

02. change Menu background on scroll js 

03. Navigation js

04. Smooth scroll to anchor

05. Portfolio js

06. Magnific Popup js

07. Testimonial js

08. Google Map js

9. Ajax Contact Form js

10. Mailchimp js

-------------------------------------------------------------------------------------- */





(function ($) {
    'use strict';


    /*-------------------------------------------------------------------------*
     *                  01. Preloader js                                       *
     *-------------------------------------------------------------------------*/
    $(window).on('load', function () {

        $('#preloader').delay(300).fadeOut('slow', function () {
            $(this).remove();
        });

    }); // $(window).on end



    $(document).ready(function () {



        /*-------------------------------------------------------------------------*
         *             02. change Menu background on scroll js                     *
         *-------------------------------------------------------------------------*/
        $(window).on('scroll', function () {
            var menu_area = $('.menu-area');
            if ($(window).scrollTop() > 0) {
                menu_area.addClass('sticky-menu');
            } else {
                menu_area.removeClass('sticky-menu');
            }
        }); // $(window).on('scroll' end




        /*-------------------------------------------------------------------------*
         *                   03. Navigation js                                     *
         *-------------------------------------------------------------------------*/
        $(document).on('click', '.navbar-collapse.in', function (e) {
            if ($(e.target).is('a') && $(e.target).attr('class') != 'dropdown-toggle') {
                $(this).collapse('hide');
            }
        });

        $('body').scrollspy({
            target: '.navbar-collapse',
            offset: 195
        });



        /*-------------------------------------------------------------------------*
         *                   04. Smooth scroll to anchor                           *
         *-------------------------------------------------------------------------*/
        $('a.smooth_scroll').on("click", function (e) {
            e.preventDefault();
            var anchor = $(this);
            $('html, body').stop().animate({
                scrollTop: $(anchor.attr('href')).offset().top - 50
            }, 1000);
        });



        /*-------------------------------------------------------------------------*
         *                  05. Portfolio js                                       *
         *-------------------------------------------------------------------------*/
        $('.portfolio').mixItUp();



        /*-------------------------------------------------------------------------*
         *                  06. Magnific Popup js                                  *
         *-------------------------------------------------------------------------*/
        $('.work-popup').magnificPopup({
            type: 'image',
            gallery: {
                enabled: true
            },
            zoom: {
                enabled: true,
                duration: 300, // don't foget to change the duration also in CSS
                opener: function (element) {
                    return element.find('img');
                }
            }

        });


        $('.popup-youtube, .popup-vimeo, .popup-gmaps').magnificPopup({
            disableOn: 700,
            type: 'iframe',
            mainClass: 'mfp-fade',
            removalDelay: 160,
            preloader: false,
            fixedContentPos: false
        });


        /*-------------------------------------------------------------------------*
         *                  07. Testimonial js                                     *
         *-------------------------------------------------------------------------*/
        $('.testimonial-list').owlCarousel({
            slideSpeed: 1000,
            paginationSpeed: 500,
            singleItem: false,
            lazyLoad: false,
            pagination: true,
            navigation: false,
            autoPlay: false,
            items: 3,
            itemsDesktop: [1199, 3],
            itemsDesktopSmall: [991, 2],
            itemsTablet: [768, 2],
            itemsMobile: [479, 1],

        });


        /*-------------------------------------------------------------------------*
         *                       08. Datepicker js                                 *
         *-------------------------------------------------------------------------*/

        {
            $("#datepicker").datepicker();
        };


        /*-------------------------------------------------------------------------*
             *                       09. Google Map js                                 *
             *-------------------------------------------------------------------------*/

        var myCenter = new google.maps.LatLng(23.779908, 90.3669903);
        function initialize() {
            var mapProp = {
                center: myCenter,
                scrollwheel: false,
                zoom: 15,
                mapTypeId: google.maps.MapTypeId.ROADMAP
            };

            var map = new google.maps.Map(document.getElementById("contactgoogleMap"), mapProp);
            var marker = new google.maps.Marker({
                position: myCenter,
                animation: google.maps.Animation.BOUNCE,
                icon: 'assets/img/map-marker.png',
                map: map,
            });
            var styles = [
                {
                    stylers: [
                        { hue: "#c5c5c5" },
                        { saturation: -100 }
                    ]
                },
            ];
            map.setOptions({ styles: styles });
            marker.setMap(map);
        }
        google.maps.event.addDomListener(window, 'load', initialize);

        /*-------------------------------------------------------------------------*
         *                  10. Ajax Contact Form js                               *
         *-------------------------------------------------------------------------*/
        // Get the form.
        var form = $('#ajax-contact');

        if (form == undefined)
            form = $('#appointment-form');

        // Get the messages div.
        var formMessages = $('#form-messages');

        if (formMessages == undefined)
            formMessages = $('#aform-messages');

        // Set up an event listener for the contact form.
        $(form).on('submit', function (e) {
            // Stop the browser from submitting the form.
            e.preventDefault();

            // Serialize the form data.
            var formData = $(form).serialize();

            // Submit the form using AJAX.
            $.ajax({
                type: 'POST',
                url: $(form).attr('action'),
                data: formData
            })
            .done(function (response) {
                // Make sure that the formMessages div has the 'success' class.
                $(formMessages).removeClass('error');
                $(formMessages).addClass('success');

                // Set the message text.
                $(formMessages).text(response);

                // Clear the form.
                $('#name').val('');
                $('#email').val('');
                $('#subject').val('');
                $('#message').val('');

            })
            .fail(function (data) {
                // Make sure that the formMessages div has the 'error' class.
                $(formMessages).removeClass('success');
                $(formMessages).addClass('error');

                // Set the message text.
                if (data.responseText !== '') {
                    $(formMessages).text(data.responseText);
                } else {
                    $(formMessages).text('Oops! An error occurred and your message could not be sent.');
                }
            });

        });


        /* === Tab to Collapse === */
        if ($('.nav-tabs').length > 0) {
            $('.nav-tabs').tabCollapse();
        }


        /*-------------------------------------------------------------------------*
         *                   11. MailChimp js                                    *
         *-------------------------------------------------------------------------*/
        $('#mc-form').ajaxChimp({
            language: 'en',
            callback: mailChimpResponse,

            // ADD YOUR MAILCHIMP URL BELOW HERE!
            url: 'https://aashrayhospital.us17.list-manage.com/subscribe/post?u=5c01a2d6fb3fcf24f9a0c6477&amp;id=4a70eced43'

        });

        function mailChimpResponse(resp) {
            if (resp.result === 'success') {
                $('.mailchimp-success').html('' + resp.msg).fadeIn(900);
                $('.mailchimp-error').fadeOut(400);

            } else if (resp.result === 'error') {
                $('.mailchimp-error').html('' + resp.msg).fadeIn(900);
            }
        }


    }); // $(document).ready end

})(jQuery);

