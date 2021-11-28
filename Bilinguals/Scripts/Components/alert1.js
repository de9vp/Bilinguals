function createAlert(title, summary, details, severity, dismissible, autoDismiss, appendToId) {
    var iconMap = {
        info: "bi bi-info-circle-fill me-2",
        success: "bi bi-hand-thumbs-up-fill me-2",
        warning: "bi bi-exclamation-circle-fill me-2",
        danger: "bi bi-exclamation-triangle-fill me-2"
    };

    var iconAdded = false;

    var alertClasses = ["alert", "animated", "flipInX", "rounded-pill1", "pb-1"];
    alertClasses.push("alert-" + severity.toLowerCase());

    if (dismissible) {
        alertClasses.push("alert-dismissible");
    }

    var msgIcon = $("<i />", {
        "class": iconMap[severity] // you need to quote "class" since it's a reserved keyword
    });

    var msg = $("<div />", {
        "class": alertClasses.join(" ") // you need to quote "class" since it's a reserved keyword
    });

    if (title) {
        var msgTitle = $("<h4 />", {
            html: title
        }).appendTo(msg);

        if (!iconAdded) {
            msgTitle.prepend(msgIcon);
            iconAdded = true;
        }
    }

    if (summary) {
        var msgSummary = $("<strong />", {
            html: summary
        }).appendTo(msg);

        if (!iconAdded) {
            msgSummary.prepend(msgIcon);
            iconAdded = true;
        }
    }

    if (details) {
        var msgDetails = $("<p />", {
            html: details
        }).appendTo(msg);

        if (!iconAdded) {
            msgDetails.prepend(msgIcon);
            iconAdded = true;
        }
    }


    if (dismissible) {
        var msgClose = $("<span />", {
            "class": "close", // you need to quote "class" since it's a reserved keyword
            "data-dismiss": "alert",
            html: "<i class='bi bi-x-circle-fill'></i>"
        }).appendTo(msg);
    }

    $('#' + appendToId).prepend(msg);

    if (autoDismiss) {
        setTimeout(function () {
            msg.addClass("flipOutX");
            setTimeout(function () {
                msg.remove();
            }, 1000);
        }, 2000);
    }
}
