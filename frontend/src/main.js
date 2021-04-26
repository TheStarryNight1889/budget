import Vue from "vue";
import App from "./App.vue";
import router from "./router";
import store from "./store";
import axios from "./backend/vue-axios";
import VueCookies from "vue-cookies";
import "./main.css";

Vue.config.productionTip = false;

new Vue({
  router,
  store,
  axios,
  render: h => h(App)
}).$mount("#app");

Vue.use(VueCookies);
Vue.$cookies.config("7d");
