    Promises in AngularJS

Controller: FatherCtrl
The father is controlling the situation here:

        // function somewhere in father-controller.js
        var makePromiseWithSon = function() {
            // This service's function returns a promise, but we'll deal with that shortly
            SonService.getWeather()
                // then() called when son gets back
                .then(function(data) {
                    // promise fulfilled
                    if (data.forecast==='good') {
                        prepareFishingTrip();
                    } else {
                        prepareSundayRoastDinner();
                    }
                }, function(error) {
                    // promise rejected, could log the error with: console.log('error', error);
                    prepareSundayRoastDinner();
                });
        };
    

Service: SonService
The Son is being used as a service, he climbs the hill and tried to see the weather.
We'll suppose when the son is looking through his telescope and looking for the approaching weather, it's analogous to using a weather API,
in the sense that it's an asynchronous operation, he may get a variable answer, and there may be a problem (say, a 500 response, foggy skies).
The response from the 'Fishing Weather API' will be returned with the promise, if it was fulfilled. It will be in the format: { "forecast": "good" }

    app.factory('SonService', function ($http, $q) {
        return {
            getWeather: function() {
                // the $http API is based on the deferred/promise APIs exposed by the $q service
                // so it returns a promise for us by default
                return $http.get('http://fishing-weather-api.com/sunday/afternoon')
                    .then(function(response) {
                        if (typeof response.data === 'object') {
                            return response.data;
                        } else {
                            // invalid response
                            return $q.reject(response.data);
                        }

                    }, function(response) {
                        // something went wrong
                        return $q.reject(response.data);
                    });
            }
        };
    });
    

Summary

    This analogy demonstrates the asynchronous nature of the request the dad makes to his son, for the weather forecast.
    The dad doesn't want to wait at the door in anticipation when the son leaves, because he has other stuff to do.
    Instead, he makes a promise at the door, and decides what will happen in either of the 3 scenarios (good weather/bad weather/no forecast).
    The son immediately gives a promise to his dad when he leaves, and will resolve or reject it on his return.
    
    The son is dealing with an asynchronous service (searching the sky with his telescope/using a weather API) to get data,
    but all of this is correctly abstracted away from his old man, who doesn't really understand technology!
