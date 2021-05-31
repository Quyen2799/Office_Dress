window.onscroll = function() {myFunction()};

var header = document.getElementById("navbar");
var sticky = header.offsetTop +200;

function myFunction() {
  if (window.pageYOffset > sticky) {
    header.classList.add("sticky");
  } else {
    header.classList.remove("sticky");
  }
}


//  owl carousel, san pham ban chay nhat
$('.owl-carousel').owlCarousel({
  stagePadding: 50,
  loop:true,
  margin:10,
  nav:true,
  autoplay:true,
  autoplayTimeout:1000,
  autoplayHoverPause:true,
  responsive:{
      0:{
          items:1
      },
      600:{
          items:3
      },
      1000:{
          items:5
      }
  }
})

