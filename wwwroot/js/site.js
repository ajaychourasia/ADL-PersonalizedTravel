
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
        if (result == null) {
            return;
        }
        personalizedTourActivities = result;
        ShowPersonalizedTourActivities(personalizedTourActivities);
    });
});

function UpdateDataWithPersonalizer(personalizerCallResult) {
    if (personalizerCallResult == null) {
        //If Azure Personalizer Service connection failed, display default Tour category (fallback option)
        $(".img-responsive").attr("src", 'https://cdn.pixabay.com/photo/2020/10/12/19/10/mountaineers-5649828_960_720.jpg');
        $("#featureTourTitle").html(" Explore Best Of  " + 'Trek' + " Experiences");
        return;
    }
    getTourDetails(personalizerCallResult.rewardActionId).then(res => {
        if (res == null || res.image == " " || res == 'undefined' || res == " ") {
            //If Server returns empty result, display default Tour category
            res.image = 'https://cdn.pixabay.com/photo/2020/10/12/19/10/mountaineers-5649828_960_720.jpg',
            res.title ='Trek'
        }
        var img = res.image;
        $(".img-responsive").attr("src", img);
        $("#featureTourTitle").html(" Explore Best Of  " + res.title+ " Experiences");
    });
}

function ShowPersonalizedTourActivities(personalizedTourActivities) {
    personalizedTourActivities.forEach(function (item, i) {
        $("#promotour" + i + " img").attr("src", item.image);
        $("#promotour" + i + " div").html(item.title);
        $("#activityLink" + i).attr("data-id", item.id);
    });
   
}

function getTourDetails(tourId) {
    const requestBody = {
        Id: tourId
    };

    return fetch("/Tour/GetTourCategory", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(requestBody)
    }).then(r => r.json());
}

function getActivityDetails(tourId, tourCategoryId) {
    const requestBody = {
        Id: tourId,
        TourCategoryId: tourCategoryId
    };

    return fetch("/Tour/GetTourActivity", {
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
    if (personalizerCallResult == null || event == null) {
        //If Azure Personalizer Service connection failed, redirect to default Tour category
        location.href = "/Tour/TourCategoryDetail/1";
    }
    sendReward(personalizerCallResult.eventId, 1).then(() => {
        location.href = "/Tour/TourCategoryDetail/"+personalizerCallResult.rewardActionId;
    });
});

$(".tourInterest a").click(function (event) {
    event.preventDefault();
    var dataId = $(this).attr("data-id");
    location.href = "/Tour/TourActivityDetail/" + dataId;
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
