
let context = {
    device: "Desktop"
};

let userAgent = {};

var personalizerCallResult = "";
var personalizedTourActivities = "";

function getRecommendation() {
    const requestContext = {
        device: context.device
    };

    return fetch("/Tour/Recommendation", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(requestContext)
    }).then(r => r.json());
}


$(document).ready(function () {
    getRecommendation().then(result => {
        personalizerCallResult = result;
        UpdateDataWithPersonalizer(personalizerCallResult);
    });

    getPersonalizedTourActivities().then(result => {
        personalizedTourActivities = result;
        ShowPersonalizedTourActivities(personalizedTourActivities);
    });
});

function UpdateDataWithPersonalizer(personalizerCallResult) {
    getTourDetails(personalizerCallResult.rewardActionId).then(res => {
        var img = res.image;
        $(".img-responsive").attr("src", img);
        $("#featureTourTitle").html(res.title);
    });
}

function ShowPersonalizedTourActivities(personalizedTourActivities) {
    personalizedTourActivities.forEach(function (item, i) {
        $("#promotour" + i + " img").attr("src", item.image);
        $("#promotour" + i + " div").html(item.title);
    });
}

function getTourDetails(tourId) {
    const requestBody = {
        Id: tourId
    };

    return fetch("/Tour/GetTour", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(requestBody)
    }).then(r => r.json());
}

function getPersonalizedTourActivities() {
    return fetch("/Tour/GetPersonalizedTourActivities", {
        method: "GET",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify()
    }).then(r => r.json());
}

function GetClusterData() {
    return fetch("/Tour/GetCluster", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify()
    }).then(r => r.json());
}


$("#tourLink").click(function (event) {
    event.preventDefault();
    sendReward(personalizerCallResult.eventId, 1).then(() => {
        location.href = "/Home/Tour/"+personalizerCallResult.rewardActionId;
    });
});


function sendReward(eventid, value) {
    return fetch("/Tour/Reward", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({
            eventid: eventid,
            value: value
        })
    });
}
