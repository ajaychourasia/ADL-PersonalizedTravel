
let context = {
    device: "mobile"
    //TripType: ""
};

let userAgent = {};
//const tripTypeOptions = ["Trek", "Camping", "RoadTrip", "Beach", "BagPacking"];
var personalizerCallResult = "";
function getRecommendation() {
    const requestContext = {
        device: context.device
        //tripType: context.TripType
     };

    return fetch("/Tour/Recommendation", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(requestContext)
    }).then(r => r.json());
}


//context.tripType = getRandomOption(tripTypeOptions);

//function getRandomOption(options) {
//    var randomNumber = Math.floor(Math.random() * options.length);

//    return options[randomNumber];
//}

var dict = {
    "1": "https://cdn.pixabay.com/photo/2020/10/12/19/10/mountaineers-5649828_960_720.jpg",
    "2": "https://cdn.pixabay.com/photo/2019/06/28/03/07/camping-4303357_960_720.jpg",
    "3": "https://cdn.pixabay.com/photo/2019/04/04/09/11/cycling-4102251_960_720.jpg",
    "4": "https://cdn.pixabay.com/photo/2016/03/04/19/36/beach-1236581__340.jpg",
    "5": "https://cdn.pixabay.com/photo/2016/03/26/22/16/nature-1281574_960_720.jpg"
};

$(document).ready(function () {
    getRecommendation().then(result => {
        personalizerCallResult = result;
        UpdateDataWithPersonalizer(personalizerCallResult);
    });

});

function UpdateDataWithPersonalizer(personalizerCallResult) {
    getTourDetails(personalizerCallResult.rewardActionId).then(res => {
        var img = res.Image;
        $(".img-responsive").attr("src", img);
    });
  }

function getTourDetails(tourId) {
    const requestContext = {
        id: tourId
    };

    return fetch("/Tour/GetTour", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(requestContext)
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
