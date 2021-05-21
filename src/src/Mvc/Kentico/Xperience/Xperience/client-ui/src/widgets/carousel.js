import "swiper/swiper-bundle.css";
import "./carousel.scss";
import Swiper, { Navigation } from "swiper";

Swiper.use([Navigation]);

const init = () => {
  const sliders = document.querySelectorAll(".swiper-container");

  for (let i = 0; i < sliders.length; i++) {
    const slider = sliders[i];

    const swiperButtonNext = slider.querySelector(".swiper-button-next");
    const swiperButtonPrev = slider.querySelector(".swiper-button-prev");

    const swiper = new Swiper(slider, {
      navigation: {
        nextEl: swiperButtonNext,
        prevEl: swiperButtonPrev,
      },
    });
  }
};

init();
