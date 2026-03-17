
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
//Nav variable diclarations 
var toggleBtn = document.getElementById("navToggle");
var menu = document.getElementById("navMenu");
var count = 0;

//View variables declarations
const indexText = document.getElementById("indexDisplayText");
const loginVar = document.getElementById("LoginID");
const regiVar = document.getElementById("regID");
//const dashboardView = document.getElementById("dashBoardView");
//const showsuspectView = document.getElementById("showsuspectsView");
//const updateCriminalRecView = document.getElementById("updateCriminalRec");
//const viewCasesView = document.getElementById("viewCases");
//const updateSuspectView = document.getElementById("updateSuspect");



//nav button even listener
toggleBtn.addEventListener("click", () => {
    menu.classList.toggle("active");
    menu.hidden = false;

    
    //element event conditions
    if (indexText != null) {
        indexText.style.marginTop = '210px';
    }
    if (loginVar != null) {
        loginVar.style.marginTop = '259px';
    }
    if (regiVar != null) {
        regiVar.style.marginTop = '259px';
    }

    //if (dashboardView != null) {
    //    dashboardView.style.marginTop = '259px';
    //}

    //if (showsuspectView != null) {
    //    showsuspectView.style.marginTop = '259px';
    //}

    //if (updateCriminalRecView != null) {
    //    updateCriminalRecView.style.marginTop = '259px';
    //}

    //if (viewCasesView != null) {
    //    viewCasesView.style.marginTop = '259px';
    //}

    //if (updateSuspectView != null) {
    //    updateSuspectView.style.marginTop = '259px';
    //}

    //doing a count for when the nav button is clicked once more
    count += 1;

    if (count > 1) {
        //going back to normal condition 
        if (indexText != null) {
            
            indexText.style.marginTop = "50px";
        }

        if (loginVar != null) {
            loginVar.style.marginTop = '20px';
        }
        if (regiVar != null) {
            regiVar.style.marginTop = '20px';
        }

    //    if (dashboardView != null) {
    //        dashboardView.style.marginTop = '8%';
    //    }

    //    if (showsuspectView != null) {
    //        showsuspectView.style.marginTop = '8%';
    //    }

    //    if (updateCriminalRecView != null) {
    //        updateCriminalRecView.style.marginTop = '12%';
    //    }

    //    if (viewCasesView != null) {
    //        viewCasesView.style.marginTop = '8%';
    //    }

    //    if (updateSuspectView != null) {
    //        updateSuspectView.style.marginTop = '8%';
    //    }

        count = 0;
    }

});
//Nav toggler listener end 
