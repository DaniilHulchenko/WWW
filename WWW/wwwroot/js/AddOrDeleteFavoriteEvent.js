console.log("addEventToFavorite was connected ");
function AddOrDeleteFavoriteEvent(eventid) {
    fetch('/Article/AddOrDeleteFavoriteEvent?ArticleId=' + eventid)
        .then(response => {
        if (response.ok) {
            // Processing a Successful Response
            if (document.getElementById(eventid).textContent == '★') { document.getElementById(eventid).innerHTML = '&#9734;'; }
            else{ document.getElementById(eventid).innerHTML = '&#9733;'; }

            console.log('The event has been added to favorites.');
        } else {
            // Processing an error
            console.error('Error when adding an event to favorites.');
        }
    })
        .catch(error => {
            console.error('Error while executing request: ', error);
        });



}
