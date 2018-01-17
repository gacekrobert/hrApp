$(document).ready(function () {
//    $(".monthbackground_hide").hide();

    $("#toggle_button").addClass("expanded");

    $(".divbeckground").toggle(function () {
        $(this).addClass("active");
    }, function () {
        $(this).removeClass("active");
    });

    jQuery(".divbeckground").click(function () {
        jQuery(this).next(".monthbackground").slideToggle(500);
    });

    $("#toggle_button").toggle(function () {
        $(this).addClass("expanded");
    }, function () {
        $(this).removeClass("expanded");
    });

    $("#toggle_button").click(function () {

        if ($(this).hasClass("expanded")) {
            $(".monthbackground").slideDown();
        }
        else {
            $(".monthbackground").slideUp();
        }
    });

    $("a[href*='hr/']").addClass('menuhr');
    $("a[href*='menager/']").addClass('menumenager');
})

