$(document).ready(() => {
  // Swipe Right
  $('form#rightForm').on("submit", function (event) {
    event.preventDefault();
    var form = this;
    $("#profile-card").addClass("animate__animated animate__fadeOutRight")
    setTimeout( function () {
      form.submit();
    }, 600);
  });

  // Swipe Left
  $('form#leftForm').on("submit", function (event) {
    event.preventDefault();
    var form = this;
    $("#profile-card").addClass("animate__animated animate__fadeOutLeft")
    setTimeout( function () {
      form.submit();
    }, 600);
  });
});