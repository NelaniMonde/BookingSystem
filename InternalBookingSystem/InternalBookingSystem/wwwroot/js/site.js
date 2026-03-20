
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
//Nav variable diclarations 
var toggleBtn = document.getElementById("navToggle");
var menu = document.getElementById("navMenu");
var count = 0;

//View variables declarations
const indexText = document.getElementById("indexDisplayText");
const bookingView = document.getElementById("bookingView");
const viewBookings = document.getElementById("viewBookings");
const editResource = document.getElementById("editResource");
const deleteResource = document.getElementById("deleteResource");
const addResource = document.getElementById("addResource");
const viewResources = document.getElementById("viewResources");




//nav button even listener
toggleBtn.addEventListener("click", () => {
    menu.classList.toggle("active");
    menu.hidden = false;

    
    //element event conditions
    if (indexText != null) {
        indexText.style.marginTop = '210px';
    }
    if (bookingView != null) {
        bookingView.style.marginTop = '259px';
    }
    if (viewBookings != null) {
        
        viewBookings.style.marginTop = '259px';
    }

    if (editResource != null) {
        editResource.style.marginTop = '259px';
    }

    if (deleteResource != null) {
        deleteResource.style.marginTop = '259px';
    }

    if (addResource != null) {
        addResource.style.marginTop = '259px';
    }

    if (viewResources != null) {
        viewResources.style.marginTop = '259px';
    }

   

    //doing a count for when the nav button is clicked once more
    count += 1;

    if (count > 1) {
        //going back to normal condition 
        if (indexText != null) {
            
            indexText.style.marginTop = "50px";
        }

        if (bookingView != null) {
            bookingView.style.marginTop = '50px';
        }
        if (viewBookings != null) {
            viewBookings.style.marginTop = '25px';
        }

        if (editResource != null) {
            editResource.style.marginTop = '30px';
        }

        if (deleteResource != null) {
            deleteResource.style.marginTop = '30px';
        }

        if (addResource != null) {
            addResource.style.marginTop = '30px';
        }

        if (viewResources != null) {
            viewResources.style.marginTop = '30px';
        }

    

        count = 0;
    }

});
//Nav toggler listener end 
