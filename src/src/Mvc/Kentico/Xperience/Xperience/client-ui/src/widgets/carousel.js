import "swiper/swiper-bundle.css";
import "./carousel.scss";
import Swiper, { Navigation } from "swiper";

Swiper.use([Navigation]);

const init = () => {
  // const swiperButtonNext = document.querySelector(".swiper-button-next");
  // const swiperButtonPrev = document.querySelector(".swiper-button-prev");

  // const swiper = new Swiper(this, {
  //   direction: "horizontal",
  //   slidesPerView: 1,

  //   navigation: {
  //     nextEl: swiperButtonNext,
  //     prevEl: swiperButtonPrev,
  //   },
  // });

  console.log("carousel-widget");
};

init();
